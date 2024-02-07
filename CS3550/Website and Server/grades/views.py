from django.contrib.auth import authenticate, login, logout
from django.shortcuts import render, redirect
from django.http import Http404, HttpResponse
from django.core.exceptions import ValidationError
from . import models
from django.views.decorators.http import require_GET, require_POST
from django.contrib.auth.decorators import login_required, user_passes_test
from django.core.files import File
from django.core.files.base import ContentFile
from django.db.models import Count, Q
from django.core.exceptions import PermissionDenied

def is_ta_or_superuser(user):
    if (user.groups.filter(name="Teaching Assistants").exists() or user.is_superuser):
        return True
    else:
        return False
def is_student(user):
    return user.groups.filter(name="Students").exists()
def is_ta(user):
    return user.groups.filter(name="Teaching Assistants").exists()

# Create your views here.
@login_required
def assignments(request):
    assignments = models.Assignment.objects.all()
    return render(request, "assignments.html", dict(assignments = assignments))

@login_required
def index(request, assignment_id):
    try:
        if is_ta(request.user):
            assignments = models.Assignment.objects.get(id=assignment_id)
            submissions = models.Submission.objects.filter(assignment__id=assignment_id, grader__username=request.user)
        if request.user.is_superuser:
            assignments = models.Assignment.objects.get(id=assignment_id)
            submissions = models.Submission.objects.filter(assignment__id=assignment_id)
        elif is_student(request.user):
            assignments = models.Assignment.objects.get(id=assignment_id)
            submissions = models.Submission.objects.filter(assignment__id=assignment_id, author__username = request.user).first()


        user = userType(request.user)
        context = {
              'assignments': assignments,
              'submissions': submissions,
              'user': user,
       }
        return render(request, "index.html", context)
    except models.Assignment.DoesNotExist:
        raise Http404("...")
    except models.Submission.DoesNotExist:
        return render(request, "index.html", {'assignments': assignments}, {'submissions': submissions}, {'user': user})
    
@login_required
@user_passes_test(is_ta_or_superuser)
def submissions(request, assignment_id):
    try: 
        assignments = models.Assignment.objects.get(id=assignment_id)
        if request.user.is_superuser:
            submissions = models.Submission.objects.filter(assignment__id=assignment_id)
        else:
            submissions = models.Submission.objects.filter(assignment__id=assignment_id, grader__username=request.user)

        context = {
              'assignments': assignments,
              'submissions': submissions,
       }
        return render(request, "submissions.html", context)
    except models.Assignment.DoesNotExist:
        raise Http404("...")
    except models.Submission.DoesNotExist:
        return render(request, "submissions.html", {'assignments': assignments})
@login_required
def profile(request):
    assignments = models.Assignment.objects.all()
    dictionary = ({})
    isStudent = False
    totalWeight = 0
    totalPoints = 0
    totalScore = 0
    if (is_ta(request.user)):
        for i in assignments:
            total = models.Assignment.get_grader(i, request.user)
            graded = models.Assignment.check_if_graded(i, request.user)
            fraction = str(graded) + "/" + str(total)
            dictionary[i] = fraction
    elif (request.user.is_superuser):
        for i in assignments:
            total = models.Assignment.get_all_graders(i)
            graded = models.Assignment.get_all_graded(i)
            fraction = str(graded) + "/" + str(total)
            dictionary[i] = fraction
    elif (is_student(request.user)):
        for i in assignments:
            grade = models.Assignment.get_assignment_author_grade(i, request.user)
            if isinstance(grade, float):
                dictionary[i] = str(grade) + "%"
                totalPoints += grade
            else:
                dictionary[i] = grade
            isStudent = True
            totalWeight += models.Assignment.get_weight(i)
        totalScore = totalPoints / totalWeight
        totalScore = str(round(totalScore * 100, 3)) + "%"
    context = {
              'dictionary': dictionary,
              'isStudent' : isStudent,
              'totalScore': totalScore,
       }
    return render(request, "profile.html", context)
    
def login_form(request):
    if request.method == 'GET':
        next_param = request.GET.get('next', '/profile/')
        if next_param:
            return render(request, "login.html", {'next': next_param})
        else: 
            return render(request, "login.html")
    elif request.method == 'POST':
        username = request.POST.get("username", "")
        password = request.POST.get("password", "")
        next_param = request.POST.get('next', '/profile/')
        user = authenticate(request, username=username, password=password)
        if user is not None:
            login(request, user)
            return redirect(next_param)
        else:
            error = "Username and password do not match."
            if (next_param):
                return render(request, "login.html", {'next': next_param,'error': error})   
            else:
                return render(request, "login.html", {'error': error})
def logout_form(request):
        logout(request)
        return render(request, "login.html")

@require_POST
@user_passes_test(is_ta_or_superuser)
def grade(request, assignment_id):
    inputs = request.POST

    for i in inputs:
        if i.startswith("grade-"):
            id_str = i.split("-")
            try:
                sub = models.Submission.objects.get(id= int(id_str[1]))
                inputted_score = request.POST["grade-" + id_str[1]]
                try:
                    if (not str(inputted_score).isnumeric()):
                        float_score = None
                    else: 
                        if (float(inputted_score) >= 0):
                            float_score = float(inputted_score)
                        else:
                            float_score = None
                    sub.score = float_score
                except ValidationError as e:
                    error_404(request, e)
            except ValidationError as e:
                error_404(request, e)

            sub.save()
            
    return redirect(f"/{assignment_id}/submissions")

@user_passes_test(is_student)
def submit(request, assignment_id):
    assign = models.Assignment.objects.get(id=assignment_id)
    if(assign.get_datetime):
        file_input = request.FILES
        textfile = file_input['file']
        file = File(file = textfile)
        submission = models.Submission.objects.filter(assignment__id=assignment_id, author__username=request.user).first()
        if (submission != None):
            submission.file = textfile
            submission.save()
        else:
            grader = pick_grader(assign)
            sub = models.Submission.objects.create(assignment_id = assign.pk, author_id = request.user.pk, grader_id = grader.pk, file = file, score = None)
    return redirect(f"/{assignment_id}/")

def show_upload(request, filename):
    if is_student(request.user):
        submission = models.Submission.objects.filter(file=filename,  author__username=request.user).first()
    elif is_ta(request.user):
        submission = models.Submission.objects.filter(file=filename,  grader__username=request.user).first()
    elif (request.user.is_superuser):
        submission = models.Submission.objects.filter(file=filename,  grader__username=request.user).first()
    else:
        return PermissionDenied
    
    with submission.file.open() as fd:
        response = HttpResponse(fd)
        response["Content-Disposition"] = \
            f'attachment; filename="{submission.file.name}"'
        return response

        

    

    

   

def error_404(request, exception):
    raise  Http404("Invalid Input")

def pick_grader(assign):
    group = models.Group.objects.get(id=1).user_set.all()
    annotated_tas = group.annotate(total_assigned=Count("user_grader", filter=Q(user_grader__assignment=assign))).order_by('total_assigned').first()
    return annotated_tas


    

def userType(user):
        userType = 0
        if user.is_authenticated:
            if is_student(user):
                userType = 1
            elif is_ta(user):
                userType = 2
            elif user.is_superuser:
                userType = 3
            return userType
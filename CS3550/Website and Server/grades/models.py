from django.db import models
from django.contrib.auth.models import User, Group
from datetime import date

# Create your models here.

class Assignment(models.Model):
    title = models.CharField(max_length=200)
    description = models.TextField()
    deadline = models.DateField(null = False, blank = True)
    weight = models.IntegerField()
    points = models.IntegerField()
    
    def __str__(self):
        return self.title
    
    def num_students(self):
        return Group.objects.get(name="Students").user_set.count()
     
    def get_grader(self, user):
        query = Submission.objects.filter(assignment__id = self.id)
        count = query.filter(grader__username = user).count()
        return count
    
    def check_if_graded(self, user):
        query = Submission.objects.filter(assignment__id = self.id)
        count = query.filter(grader__username=user).exclude(score=None).count()
        return count
    
    def get_datetime(self):
          return date.today() < self.deadline
    
    def get_pastdue(self):
          return date.today() >= self.deadline
    
    def get_all_graders(self):
        count = Submission.objects.filter(assignment__id = self.id).count()
        return count
    
    def get_all_graded(self):
        query = Submission.objects.filter(assignment__id = self.id)
        count = query.exclude(score=None).count()
        return count
    
    def get_assignment_author_grade(self, user):
        try:
            sub = Submission.objects.get(assignment__id = self.id, author__username=user)
            points = sub.assignment
            if sub.score is None:
                score = "Ungraded"
            else:
                score = sub.score / points.points
                score = score * 100
        except Submission.DoesNotExist:
            score = "Missing"
        return score
    
    def get_weight(self):
        assignment = Assignment.objects.filter(id = self.id).first()
        return assignment.weight
    




class Submission(models.Model):
    assignment = models.ForeignKey(Assignment, on_delete=models.CASCADE)
    author = models.ForeignKey(User, on_delete=models.PROTECT, related_name='user_author')
    grader = models.ForeignKey(User, on_delete=models.PROTECT, related_name='user_grader')
    file = models.FileField()
    score = models.FloatField(null = True, blank = False)

    def get_percentage(self):
        score = self.score / self.assignment.points
        return  str(round(score, 3) * 100) + "%"
    
    def total_submissions(self):
        return self.submission.count()

    def __str__(self):
        return self.assignment.title

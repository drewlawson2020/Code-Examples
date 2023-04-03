```
Author:    Peter Bruns
Partner:   None
Date:      20-Jan-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: brunsp10
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/spreadsheet-brunsp10/tree/main
Commit #:  245418185360ee7b53511cf93900fb002d6306b2
Project:   Formula Evaluator
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```
# Project description
This project contains methods that can be used to evaluate an input formula to provide an integer result. The formula is input as a string, and can contain any integer, variables whose names contain one or more letters followed by one or more numbers, and the operators +, -, \*, /, (, and ). All division is integer division. Values for variables are provided by a delegate method that is provided as a parameter to the Evaluate method.

# Assignment time estimate and actual time
I estimated that it would take about 8 hours to complete the assignment. It actually took about 7 hours. This is broken up
into .5 hours to read instructions and setup the files, 2 hours to get a working Evaluate method, 4 hours for debugging and
writing tests, and .5 hours to add comments and headers.

# Comments to Evaluators:
I initially had the multiplication/division and addition/subtraction methods repeated in Evaluate before moving them to
helper methods. I think that Evaluate still seems a bit long, but I could not think of anything else to remove. In 1410 and
2420 I tended to over comment my code. I tried to cut down on the comments for this assignment while still having every point
of the algorithm included in a comment.

# Assignment specific topics
This assignment served as an introduction to C# and Visual Studio, and introduced the concept of delegates. A delegate method was used to define the structure of methods that would be used to pass values for variables to the Evaluate method.

# Consulted peers
I did consult any classmates about this assignment. I felt like I understood the instructions and how to implement the methods well enough, but I did check Piazza every so often to see if there were any questions I had not thought about.

# References
I consulted the following webpages while coding:
https://kodify.net/csharp/strings/remove-whitespace/#remove-all-whitespace-from-a-c-string
https://docs.microsoft.com/en-us/dotnet/api/system.string.trim?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/how-to-determine-whether-a-string-represents-a-numeric-value
https://stackoverflow.com/questions/3581741/c-sharp-equivalent-to-javas-charat

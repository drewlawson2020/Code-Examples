```
Author:    Peter Bruns
Partner:   None
Date:      4-Feb-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: brunsp10
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/spreadsheet-brunsp10/tree/main
Commit #:  632796ccb8f0a9bdfcd1d6ad837bd508d71134d9
Project:   FormulaTests
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```
# Project description
This project contains unit tests for the public methods in the Formula class. The tests test method output along with errors and exceptions where applicable. Also contains simple methods to be passed as Func arguments to the class constructor and Evaluate.

# Assignment time estimate and actual time
I estimated that it would take about 7 hours to complete because that is how long the first two assignments took. It actually took about 8 hours. This includes 1 hour for an initial attempt that was scrapped, 3 hours to reread instructions and code the program, 3 hours to write tests and debug, and 1 hour to add comments.

# Comments to Evaluators:
I split the tests into a few different test classes for organization. I thought that it would be easier to find a specific test if they were split up by category of test. Most of the categories are for Evaluate, and the other two are for the remaining methods along with exceptions and errors. I thought that the best way to test for double equality after Evaluate was to pass the expression to a double. I figured that this would go through a similar process to Evaluate so the double variable should store the same value as Evluate produces.

# Assignment specific topics
This assignment required testing that exceptions were thrown and errors were returned when expected. This is the first time that either of these has been required for unit tests in this class.

# Consulted peers
I did consult any classmates about this assignment. I felt like I understood the instructions and how to implement the methods well enough. I did check Piazza to see if there were any questions I had not thought about.

# References
I consulted the following webpages while coding:
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast
https://stackoverflow.com/questions/4287537/checking-if-the-object-is-of-same-type
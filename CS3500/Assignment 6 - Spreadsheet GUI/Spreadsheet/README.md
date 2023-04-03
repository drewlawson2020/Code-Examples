```
Author:    Peter Bruns
Partner:   None
Date:      18-Feb-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: brunsp10
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/spreadsheet-brunsp10/tree/main
Commit #:  1f05b6f632bdd2dc99f26ad73527b168f8b51fab
Project:   Spreadsheet
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```
# Project description
Contains methods needed to represent cell names, content, and values in a spreadsheet. Valid cell names begin with one or more letters followed by one or more digits. Spreadsheets may specify normalizers and  validators for variable names. Normalizers convert names (for example from lower to uppercase) and validatorscan specify valid normalized variable names for individual spreadsheets. The validator may be less restrictivethan thespreadsheet variable name specification, but all names must be valid according to the overall Spreadsheetspecification (for example, "A_1" will never be valid even if the validator allows underscores).

Cell content may be a string, double, or Formula object. This content is used to compute the cell's value atthe time of cell creation. Cell values may be a double, string, or FormulaError. The formulas contained in a Spreadsheet can never result in a circular dependency without an exception being thrown.

The methods in this file return the names of nonempty cells, return the content of a cell, return the value of a cell,set the content of a cell and update the values of all cells that are dependent on that cell. The Spreadsheet also contains methods to read and write XML files containing data about nonempty cells.

The project also contains a Cell class that is used to represent the cells in the spreadsheet. Cell objects contain a name, contents, and value.

# Assignment time estimate and actual time
I estimated that this assignment would take 10 hours. It ended up taking about 10 which was broken up as 3 hours to read the API and code the program, 1 hour for intitial testing and debugging, 5 hours for more rigorous testing and debugging, and 1 hour to add comments.

# Comments to Evaluators:
I kept the program mostly the same as it was in the last assignment but upgraded the Cell class to be stored in the dictionary as a value associated with a nonempty cell name. I used inheritdoc whenever possible to implement method headers. There are several private helper methods that I added to (I think) help make the code easier to follow. I do think that I might have over commented in spots, but I tried to keep comments to only the places where I thought explanation would be helpful.

# Assignment specific topics
This assignment provided an opportunity to implement API instructions while refactoring existing code. It also served as an introduction to reading and writing files in XML format.

# Examples of Good Software Practice (GSP)
1. Because all of the methods that take a name as an argument throw an exception for invalid names, I made a new method called NameIsValid that checks if the input name is valid and throws an exception if the name is invalid. I did this so that I would only have to write the code for checking the name and throwing the exception once then reuse it for each method. This should make it easier to update if the pattern of valid names changes.

2. The SetCellContents methods update a dictionary that contains the cell names as keys with the cell contents as values. Instead of checking if the dictionary contains the key everytime before updating the mapped value, I wrote an extension that would do this. I did this so that I would not have to repeat the code to check for a key before adding it or updating its value. This should make the code easier to maintain because there is only one method that would need to be changed if the dictionary's behavior changes.

3. I think that my implementation of GetDirectDependents is a good example of code reuse and of a short, well commented method. I used inheritdoc to inherit the summary from AbstractSpreadsheet. The body of the method is one line which contains a return statement then a call to GetDependents from the DependencyGraph class. Because GetDependents has the same behavior as required by GetDirectDependents, no other code was needed. I did add a comment above the line to make sure that anybody reading the code would know what GetDependents does. I thought it was important to specify that GetDependents returns a HeshSet that contains only direct dependents.

# Consulted peers
I did not consult anyone directly, but there was a post on Piazza (question @456) that was helpful. It made me realize that the regular expression I used for Formula was not going to work properly. I updated the expression for this assignment and in Formula.

# References
Code for reading and writing XML files was adapted from example code provided in the ForStudents repository.
https://codeasy.net/lesson/properties
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.add?view=net-6.0
https://stackoverflow.com/questions/31333136/c-sharp-regex-to-match-end-of-string?rq=1
https://regexr.com/
https://tunnelvisionlabs.github.io/SHFB/docs-master/SandcastleBuilder/html/79897974-ffc9-4b84-91a5-e50c66a0221d.htm
https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sequenceequal?view=net-6.0#system-linq-enumerable-sequenceequal-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))
https://stackoverflow.com/questions/47382087/regular-expression-to-match-non-whitespace-characters
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags#example
https://docs.microsoft.com/en-us/visualstudio/ide/reference/implement-interface?view=vs-2022
https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2011
https://docs.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/general/read-write-text-file
https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlwritersettings?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.string.substring?view=net-6.0
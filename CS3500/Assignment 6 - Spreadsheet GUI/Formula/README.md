```
Author:    Peter Bruns
Partner:   None
Date:      4-Feb-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: brunsp10
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/spreadsheet-brunsp10/tree/main
Commit #:  632796ccb8f0a9bdfcd1d6ad837bd508d71134d9
Project:   Formula
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```
# Project description
Contains a definitions for construction of a Formula object and associated methods. Each Formula has an associated expression that can contain string representations of doubles, variables, and the operators +, -, *, /, (, and ). Variable names consist of an underscore or letter followed by zero or more letters,underscores, or floating point digits. Any spaces in the expression are treated as delimiters for tokens. The Formula constructor takes the expression, a normalizer for variable names, and a validator for variable names. The normalizer can alter the string representation of the variables, and the validator ensures that the names are acceptable by the definition in the validator. Methods associated with each Formula object can evaluate the expression, return a list of variables, return a string representation of the normalized expression, check for equality of two Formulas, and produce a hash code for the Formula. The project also contains definitions for a FormulaFormatException and a FormulaError struct.

# Assignment time estimate and actual time
I estimated that it would take about 7 hours to complete because that is how long the first two assignments took. It actually took about 8 hours. This includes 1 hour for an initial attempt that was scrapped, 3 hours to reread instructions and code the program, 3 hours to write tests and debug, and 1 hour to add comments.

# Comments to Evaluators:
I think that this class is immutable. Both of the fields are private without a setter, so the fields should not be changeable after the Formula is constructed. Header comments used the API headers as reference. The code in Evaluate is very similar to the code in FormulaEvaluator with some syntax checks removed.

# Assignment specific topics
This assignment introduced overloading operator function, and the use of Funcs and Structs in code.

# Consulted peers
I did consult any classmates about this assignment. I felt like I understood the instructions and how to implement the methods well enough. I did check Piazza to see if there were any questions I had not thought about.

# References
The class and method header comments used the API as reference.
I consulted the following webpages while coding:
https://www.codeproject.com/Articles/1043301/Immutable-objects-in-Csharp
https://www.journaldev.com/129/how-to-create-immutable-class-in-java
https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-6.0
https://stackoverflow.com/questions/1377864/what-is-the-performance-of-the-last-extension-method-for-listt
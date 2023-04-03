```
Author:     Drew Lawson
Partner:    None
Date:       3-Feb-2022
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  drewlawson2020
Repo:       https://github.com/drewlawson2020/
Commit #:   bf5310b444204702202f081a3e311d851ca4a6d0
Solution:   Spreadsheet
Copyright:  CS 3500 and Drew Lawson- This work may not be copied for use in Academic Coursework.
```

# Overview of the Spreadsheet functionality

The Spreadsheet program is currently capable of evaluating functions using arithmetic and delegation for variables. It has the ability to
take in formulas, verify them, and then correctly and accurately calculate them out for the user. It can also normalize and validate variables, as well
as assign values to them using the Formula and Evaluate classes. As of this writing, the FormulaEvaluator is effectively deprecated, but 
will be included for now until I am told that it is okay to remove. The Formula class file now contains pretty much all of the necessary functions and classes
to be able to effectively do these tasks.
It has been updated now to also implement cells that can be validiated, recaculated to check dependency relations, and also has a dedicated cell class.

# Sources Mostly Used for Research:
    -TA Suggestions and advice for cycling through the tokens in a indexed fashion
    -Microsoft Documentation of C#
    -Regex Docs, also from Microsoft (https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference)

# Time Expenditures:

    1. Assignment One:   Predicted Hours:          10        Actual Hours:       13
    2. Assignment Two:   Predicted Hours:          10        Actual Hours:       9
    3. Assignment Three: Predicted Hours:          12        Actual Hours:       14
    4. Assignment Four: Predicted Hours:            9        Actual Hours:        9
    5. Assignment Five: Predicted Hours:            6       Actual Hours:        10

# Examples of Good Software Practice
- Utilizing helper methods to assist with code base and functions (Such as Is_name and ReadFile)
- Getting advice from TAs in regards to how I should go about implementation of code
- Updating old tests to function with new code base
- Comments in necessary areas
- Simple and coherent tests to ensure good overall coverage

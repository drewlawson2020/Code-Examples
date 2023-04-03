```
Author:    Drew Lawson and Peter Bruns
Partner:   None
Date:      March 3, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: EatSleepCode
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-six---spreadsheet-and-gui-eatsleepcode
Commit #:  1b87b46323dd3331c5d5d4fde265bf1be2966700
Solution:  Spreadsheet
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# Overview of the Spreadsheet functionality

The spreadsheet program can currently evaluate formulas in two ways. The first class (FormulaEvaluator) evaluates formulas containing integers, variables, and the operators +,-,/,*,(, and ) using a static method. The second class (Formula) evaluates expressions containing doubles, variables, and the operators specified previously. This second class creates immutable Formula objects that can be reused to produce their result repeatedly. The Spreadsheet can currently keep track of cell names, cell contents, and the relationships between cells. The relationships are tracked with a DependencyGraph class. There is now a GUI application that is capable of displaying spreadsheets with values on a grid. The GUI also has text boxes for contents entry and to display cell name and value.

# Time Expenditures:

	1. Assignment One:	Predicted Hours: 8		Actual Hours: 7
	2. Assignment Two:	Predicted Hours: 9		Acutal Hours: 7
	3. Assignment Three:	Predicted Hours: 7		Actual Hours: 8
	4. Assignment Four:	Predicted Hours: 8		Actual Hours: 8
	5. Assignment Five:	Predicted Hours: 10		Actual Hours: 10
	6. Assignment Six:	Predicted Hours: 8		Actual Hours: 8

# Time Management and Estimation
We were very accurate in our estimate of time required to complete the assignment. We went back and forth between making progress and debugging so do not have estimates of the time requried for either individually. The partnership made the assignment go faster than it otherwise would have.

# Software Practices

	1. We made sure that all variables and GUI components had descriptive names. This way the function of any variable or component should be relatively clear without any additional comments.
	2. We condensed reused code into helper methods. This is especially important for the methods that change cell selection with mouse clicks or key presses. There was a large block of very similar code that each used that we put into a helper method. This way, only one method will need to be updated if future changes break the cell change functionality.
	3. Because all previous projects were updated to pass grading tests and had been tested thoroughly beforehand, we were confident that all code in them worked. Because of this, we did not alter previous assignment code and used the previously implemented methods for spreadsheet whenever possible. We manually tested the spreadsheet to make sure that everything worked as expected and did not have to change any previous code after fixing grading test errors.
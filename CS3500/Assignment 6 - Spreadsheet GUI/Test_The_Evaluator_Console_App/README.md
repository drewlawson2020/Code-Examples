```
Author:    Peter Bruns
Partner:   None
Date:      20-Jan-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: brunsp10
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/spreadsheet-brunsp10/tree/main
Commit #:  245418185360ee7b53511cf93900fb002d6306b2
Project:   Test the Formula Evaluator Console App
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```
# Project description
This project contains tests for the output of the Evaluate method in the Formula Evaluator project. It also contains tests to ensure that exceptions are thrown where expected. The tests will only print to the console if they produce an unexpected result. If all tests pass, messages indicating this will be printed to the console.

# Assignment time estimate and actual time
This is the same as the Formula Evaluator README information. I estimated that the assignment would take about 8 hours, it
actually took about 7 hours.

# Comments to Evaluators:
I thought it would be cleaner to not have the evaluation tests print any message unless they failed. After the class discussion, I also removed the messages printed by the exception tests when the exception was caught successfully. For both test sections (evaluation and exceptions), I had a Boolean that tracked that each test passed and printed a message saying so at the end.

# Assignment specific topics
This part of the assignment served more as a refresher for coding. The method that was passed to Evaluate through the delegate method is defined in this class. This method provides values for a few example variables and throws an exception if the variable is not defined.

# Consulted peers
I did consult any classmates about this test file. Like the Formula Evaluator, I did check Piazza every so often to see if anyone had any insights that I had missed.

# References
Besides the class page and notes, I did not consult any resources for the test file. I included the example test from the assignment
page at the end of the evaluation tests.

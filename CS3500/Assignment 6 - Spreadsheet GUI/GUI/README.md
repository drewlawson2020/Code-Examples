```
Authors:    Drew Lawson and Peter Bruns
Team Name:   EatSleepCode
Date:      3-Mar-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: EatSleepCode
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-six---spreadsheet-and-gui-eatsleepcode
Commit #:  1b87b46323dd3331c5d5d4fde265bf1be2966700
Project:   GUI
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# File contents
Implementation of a spreadsheet GUI. Contains all essential methods needed to open file, save file, create new spreadsheet,
close file, set contents of cell, compute and display value of cell in textbox and on a grid, display cell name, change selected
cell on grid, and open README text files. Also contains methods to display direct cell dependencies and change selected cell with
arrow keys. Cells names start with a single letter A-Z followed by a number 1-99.

# Comments to Evaluators
Cell contents changes that result in many FormulaErrors freeze the application
for a few seconds.


# Consulted Peers
Outside of Piazza for ProcessCmdKey, there were no consulted peers.

# Partnership
Peter Bruns was the driver and Drew Lawson was the navigator. This was
Peter's choice. The partnership went smoothly. The project was completed
efficiently and to the project's requirements. Peter did the work in the 
side branches.


# Branching
The EnableArrowKeys branch as used to enable cell navigation with
key presses. The AddAThread branch was used to add threading to
cell value recalculation and spreadsheet opening. There were no
challenges with branching or merging. Branches were merged back to
main in commit ed37cd412e27ced7006d473fe49b1a502e9c6fbd.

# Additional Features and Design Decisions
We added a DataGridView to display direct cell dependencies when
a button is pressed. We also added navigation functionality to arrow
and enter key presses. Threading is used when the contents of a cell
are reset or a file is loaded and many cell values need to be displayed.
Arrow key presses when the value box is selected scroll through the 
displayed text so that long messages can be viewed in full. Most design
choices for text boxes were based on the provided example GUI. The decision
to allow enter to change cell selection when the contents box is selected
was based on the way that Excel behaves.

# Time tracking
We estimated that the program would take 8 hours to complete. This was an
accurate estimation. Time spent making progress and debugging was not 
separated. We switched back and forth as needed.

# Best Team Practices
Partnership was very effective when programming the cell dependency display.
Partnering allowed one of us to look at documentationa at the same time the
other typed. This allowed us to try things then fix errors as necessary.

Partnership went well. I (Peter) probably went beyond what I said I would do
when branching and it would have better to check in first. This would have helped
ensure we were both on the same page.

# References
The example code was referenced heavily. The GUI_Application example was used directly
for the SpreadsheetWindow program in our solution. Along with Microsoft documentation, the
following URLs were referenced:
https://stackoverflow.com/questions/3360555/how-to-pass-parameters-to-threadstart-method-in-thread
https://simple.wikipedia.org/wiki/ASCII
https://stackoverflow.com/questions/3414900/how-to-get-a-char-from-an-ascii-character-code-in-c-sharp
https://stackoverflow.com/questions/18059306/windows-forms-textbox-enter-key
https://stackoverflow.com/questions/168550/display-a-tooltip-over-a-button-using-windows-forms
https://www.geeksforgeeks.org/c-sharp-tooltip-class/
https://stackoverflow.com/questions/29492832/save-as-file-dialog-how-to-not-allow-overwrite
https://www.youtube.com/watch?v=LydFPT5eyXE
https://stackoverflow.com/questions/16846573/using-dialogresult-correctly
https://stackoverflow.com/questions/18242473/how-retrieve-only-filename-from-save-file-dialog
https://stackoverflow.com/questions/16811859/how-to-display-textbox-control-in-messagebox
https://stackoverflow.com/questions/17796151/close-form-button-event
https://stackoverflow.com/questions/2683679/how-to-know-user-has-clicked-x-or-the-close-button
https://www.youtube.com/watch?v=_4Dpp_b6tKU
https://stackoverflow.com/questions/48537187/c-sharp-winforms-datagridview-add-row
https://stackoverflow.com/questions/29576525/get-keycode-value-in-the-keypress-event
https://stackoverflow.com/questions/8245706/overriding-windows-forms-default-behavior-for-some-key-events
https://gist.github.com/akfish/1190519
https://stackoverflow.com/questions/8997880/opening-an-already-existing-txt-file-on-button-click-in-c-sharp-windows-form
https://stackoverflow.com/questions/14899422/how-to-navigate-a-few-folders-up
https://stackoverflow.com/questions/60018792/how-to-open-a-textfile-with-the-default-editor-in-net-core
https://stackoverflow.com/questions/5718541/check-if-keys-is-letter-digit-special-symbol
https://stackoverflow.com/questions/20423211/setting-cursor-at-the-end-of-any-text-of-a-textbox

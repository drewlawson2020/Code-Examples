```
Authors:    Drew Lawson and Peter Bruns
Team Name:   NotSoSuperMario
Date:      14-Apr-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NotSoSuperMario
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-notsosupermario
Commit #:  433f2341577aabaaf646e0981432a8327c8f72f9
Project:   ClientGUI
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# Project description
This project contains the view and controller portions of the Agario solution. It defines how the game is displayed on the GUI and communicates with the server by sending commands and receiving information about the game world. It also interacts with AgarioModels to pass along the information from the server and to extract relevant information for game display.

# Assignment time estimate and actual time
The overall assignment took 10 hours to complete. We estimated it would take 8 hours. We worked on this project for about 8 hours in total. About 1 hour was spent setting up the GUI layout and interface. The remaining time was spent mostly figuring out how to draw a zoomed in portal view of the world, and track the mouse location in the game world relative to the display.

# Comments to Evaluators:
Game objects sometimes draw partially outside of the gray display section. We could not figure out how to clip them to the edge of that section. The client remains operational if the server closes, but we could not figure out how to display and error message when that occurs.

# Assignment specific topics
This project provided more practice in MVC architecture implementation because it was the view and controller for the Agario game. The control portion involved learning how to track a cursor position, while the view involved learning to draw a zoomed in portion of a world where everything is in its relative position and scaled size. It also provided some practice in deciding where to implement logging. It also provided experience using a timer to call a method at specified intervals.

# Consulted peers
We did not consult any peers for this project outside of Piazza.

# References
Along with course material, we consulted the following websites:
https://www.youtube.com/watch?v=Szs3USHeu3s
https://www.youtube.com/watch?v=w2A488eL6vQ
https://stackoverflow.com/questions/16376191/measuring-code-execution-time
https://stackoverflow.com/questions/2453951/c-sharp-double-tostring-formatting-with-two-decimal-places-but-no-rounding
https://salesforce.stackexchange.com/questions/257291/elegantly-handle-the-null-object-check-after-json-deserialize
https://docs.microsoft.com/en-us/dotnet/api/system.drawing.color.fromargb?view=net-6.0#system-drawing-color-fromargb(system-int32)
https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.mouseposition?view=windowsdesktop-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.cursor.position?redirectedfrom=MSDN&view=windowsdesktop-6.0#System_Windows_Forms_Cursor_Position
https://social.msdn.microsoft.com/Forums/en-US/91fe8993-220e-45b3-bb27-a755a08534b9/plotting-graphics-in-a-separate-thread-using-c-winform-applications-need-help?forum=winforms
https://stackoverflow.com/questions/10714358/draw-on-a-form-by-a-separate-thread
https://stackoverflow.com/questions/9509147/label-word-wrapping
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements
https://docs.microsoft.com/en-us/dotnet/api/system.string.split?view=net-6.0#system-string-split(system-char-system-stringsplitoptions)
https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.messagebox?view=windowsdesktop-6.0
https://stackoverflow.com/questions/26244336/the-given-key-was-not-present-in-the-dictionary-which-key
https://stackoverflow.com/questions/3172731/forms-not-responding-to-keydown-events
https://stackoverflow.com/questions/3853700/c-sharp-switch-case-string-starting-with
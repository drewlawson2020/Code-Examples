```
Authors:    Drew Lawson and Peter Bruns
Team Name:   NoSleep
Date:      April 4, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NoSleep
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-seven---chatting-nosleep
Commit #:  e843b4b1de6d153c0ba497e9605d3fd884cbcbd0
Project:   ChatClientGUI
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# File Contents
This file contains a chat client GUI that can be used to communicate over a chat server. The client can send messages, receive messages, and allows user to set their display name. Contains the GUI design code and the code needed to use the GUI for chat functionality.

# Comments to Evaluators
We kept all of the code in the GUI file instead of spearating it into MVC architecture. We planned to work on that if there was time, but then did not want to accidentally break the code when it started working.

# Consulted Peers
Outside of Piazza, we did not consult any other peers.

# Partnership
Peter Bruns was the driver and Drew Lawson was the navigator for most of the assignment. The partnership went smoothly, but both of us had difficulties understanding a lot of the assignment. Still, the partnership probably ended up saving time since we could both try to figure out what was wrong.

# Branching
Peter made two side branches to mess around with the program individually. We reviewed the code in the branches together before merging them back into the master branch.

# Design Decisions
We decided to give the client a box to set the server address for connection, a box to enter a name, a box to enter text to send, a chat log, and a participant list. The participant list updates automatically whenever a new participant joins, a participant leaves, or a participant changes names. When the name box is empty, the message box and connect button are disabled so that the client cannot have an empty string as their name. The name in the box sends automatically upon sevrer connection.

# Time Tracking
This assignment took much longer than anticipated due to difficulties understanding what was required for much of it. We predicited that it would take 8 hours and it actually took 12. Of this time, only about 4 hours were spent actually making progress and the rest was devoted to debugging and looking up information.

# Best Team Practices
The partnership went smoothly and likely helped us finish faster than we would have individually. This was a tough assignment to understand, so having a partnership probably helped greatly in that regard. All individual work was done on separate branches and code was reviewed befor ebeing merged back to the main branch.

# References
We heavily referenced the provided chat client example along with the following sources:
https://kodify.net/csharp/strings/remove-whitespace/
https://techcommunity.microsoft.com/t5/iis-support-blog/no-such-host-is-known-error-in-visual-studio/ba-p/334369
https://docs.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2022
https://stackoverflow.com/questions/13318561/adding-new-line-of-data-to-textbox
https://stackoverflow.com/questions/898307/how-do-i-automatically-scroll-to-the-bottom-of-a-multiline-text-box
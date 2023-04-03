﻿```
Authors:    Drew Lawson and Peter Bruns
Team Name:   NoSleep
Date:      April 4, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NoSleep
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-seven---chatting-nosleep
Commit #:  e843b4b1de6d153c0ba497e9605d3fd884cbcbd0
Project:   FileLogger
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# File Contents
This project contains the classes needed to provide a custom file logger to objects that require one (CustomFileLogProvider) and to define how lines are added to the file (CustomFileLogger). It also contains an extension class that is used to add date, time, and thread information to logged lines.

# Comments to Evaluators
We chose to leave the logger based on the lab example. We were not getting anywhere in attempts to implement the more efficient solution described on the assignment page, and we did not want to accidentally break the code once it was working for both the chat client and the server.

# Consulted Peers
Outside of Piazza, we did not consult any other peers.

# Partnership
Peter Bruns was the driver and Drew Lawson was the navigator for most of the assignment. The partnership went smoothly, but both of us had difficulties understanding a lot of the assignment. Still, the partnership probably ended up saving time since we could both try to figure out what was wrong.

# Branching
Peter made two side branches to mess around with the program individually. We reviewed the code in the branches together before merging them back into the master branch.

# Time Tracking
This assignment took much longer than anticipated due to difficulties understanding what was required for much of it. We predicited that it would take 8 hours and it actually took 12. Of this time, only about 4 hours were spent actually making progress and the rest was devoted to debugging and looking up information.

# Best Team Practices
The partnership went smoothly and likely helped us finish faster than we would have individually. This was a tough assignment to understand, so having a partnership probably helped greatly in that regard. All individual work was done on separate branches and code was reviewed befor ebeing merged back to the main branch.

# References
We heavily referenced the provided lab logger examples along with the following sources:
https://nblumhardt.com/2016/11/ilogger-beginscope/
https://stackoverflow.com/questions/2406794/how-do-i-free-objects-in-c-sharp
https://stackoverflow.com/questions/5156254/closing-a-file-after-file-create
https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals
https://stackoverflow.com/questions/22340993/how-to-write-log-file-when-no-need-to-close-file-in-running-application
https://docs.microsoft.com/en-us/dotnet/api/system.io.filemode?view=net-6.0
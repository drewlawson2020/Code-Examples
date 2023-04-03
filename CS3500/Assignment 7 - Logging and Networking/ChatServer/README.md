```
Authors:    Drew Lawson and Peter Bruns
Team Name:   NoSleep
Date:      April 4, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NoSleep
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-seven---chatting-nosleep
Commit #:  be42b59718bdb759333d7f2116bc198de441ae83
Project:   ChatServerGUI
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# File Contents
This file contains an implementation of a chat server. This server can accept multiple chat clients and allows messages to be sent between them. The server itself can also send messages to all connected clients, shut itself down, and start itself back up. The file contains the server GUI and code needed to implement it with the use of the Networking project. The sever also contains an associated logger to log server events.

# Comments to Evaluators
We kept all of the code in the GUI file instead of spearating it into MVC architecture. We planned to work on that if there was time, but then did not want to accidentally break the code when it started working.

# Consulted Peers
Outside of Piazza, we did not consult any other peers.

# Partnership
Peter Bruns was the driver and Drew Lawson was the navigator for most of the assignment. The partnership went smoothly, but both of us had difficulties understanding a lot of the assignment. Still, the partnership probably ended up saving time since we could both try to figure out what was wrong.

# Branching
Peter made two side branches to mess around with the program individually. We reviewed the code in the branches together before merging them back into the master branch.

# Design Decisions
The server's address box displays its IP address so that this information can be distributed to clients. The server also has a participant list that automatically updates when a client connects, disconnects, or changes their name. The server's chat log displays all messages sent to clients as well as information about the server status. We also provided a button that can be used to shut the server down or re-establish it.

# Time Tracking
This assignment took much longer than anticipated due to difficulties understanding what was required for much of it. We predicited that it would take 8 hours and it actually took 12. Of this time, only about 4 hours were spent actually making progress and the rest was devoted to debugging and looking up information.

# Best Team Practices
The partnership went smoothly and likely helped us finish faster than we would have individually. This was a tough assignment to understand, so having a partnership probably helped greatly in that regard. All individual work was done on separate branches and code was reviewed befor ebeing merged back to the main branch.

# References
We heavily referenced the provided chat server example along with the following sources:
https://www.delftstack.com/howto/csharp/get-local-ip-address-in-csharp/
https://techcommunity.microsoft.com/t5/iis-support-blog/no-such-host-is-known-error-in-visual-studio/ba-p/334369
https://docs.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2022
https://stackoverflow.com/questions/13318561/adding-new-line-of-data-to-textbox
https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-6.0
https://stackoverflow.com/questions/898307/how-do-i-automatically-scroll-to-the-bottom-of-a-multiline-text-box
```
Authors:    Drew Lawson and Peter Bruns
Team Name:   NoSleep
Date:      April 4, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NoSleep
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-seven---chatting-nosleep
Commit #:  e843b4b1de6d153c0ba497e9605d3fd884cbcbd0
Project:   Communications
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# File Contents
This project contains code needed to run a client and server application. Contains methods to connect a client to a server, wait for messages from the connected client or server, wait for clients to connect to a server, shut a server down, send messages back and forth, and check for messages in received data. Callback method definitions are provided so that the client or server can specify their own protocol for handling connection, disconnection, and received messages. The connection uses TCP protocol to transmit the data between client and server.

# Comments to Evaluators
This code was heavily based on the provided example client and server methods. The class itself does not define thrown exceptions to wrapper classes, but can throw exceptions from the methods contained in it. These exceptions are handled in the client and server.

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
We heavily referenced the provided chat client and server examples along with the following sources:
https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener.accepttcpclientasync?view=net-6.0#system-net-sockets-tcplistener-accepttcpclientasync
https://techcommunity.microsoft.com/t5/iis-support-blog/no-such-host-is-known-error-in-visual-studio/ba-p/334369
https://www.codeproject.com/Articles/12893/TCP-IP-Chat-Application-Using-C
https://stackoverflow.com/questions/425235/how-to-properly-and-completely-close-reset-a-tcpclient-connection
https://stackoverflow.com/questions/10341301/get-remote-port-of-a-connected-tcpclient
https://stackoverflow.com/questions/6569486/creating-a-copy-of-an-object-in-c-sharp
https://codeasy.net/lesson/properties

```
Authors:    Drew Lawson and Peter Bruns
Team Name:   NotSoSuperMario
Date:      14-Apr-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NotSoSuperMario
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-notsosupermario
Commit #:  1f6af15c56d72751a6f5295ff26c19d052cde550
Project:   Communications
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# File Contents
This project contains code needed to run a client and server application. Contains methods to connect a client to a server, wait for messages from the connected client or server, wait for clients to connect to a server, shut a server down, send messages back and forth, and check for messages in received data. Callback method definitions are provided so that the client or server can specify their own protocol for handling connection, disconnection, and received messages. The connection uses TCP protocol to transmit the data between client and server.

# Comments to Evaluators
This code was heavily based on the provided example client and server methods. The class itself does not define thrown exceptions to wrapper classes, but can throw exceptions from the methods contained in it. These exceptions are handled in the client and server. This project was not used for the Agario solution.

# Consulted Peers
Outside of Piazza, we did not consult any other peers.

# Time Tracking
The overall assignment took 10 hours to complete. We estimated it would take 8 hours. This project did not add a significant amount of time to the total.

# References
We heavily referenced the provided chat client and server examples along with the following sources:
https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener.accepttcpclientasync?view=net-6.0#system-net-sockets-tcplistener-accepttcpclientasync
https://techcommunity.microsoft.com/t5/iis-support-blog/no-such-host-is-known-error-in-visual-studio/ba-p/334369
https://www.codeproject.com/Articles/12893/TCP-IP-Chat-Application-Using-C
https://stackoverflow.com/questions/425235/how-to-properly-and-completely-close-reset-a-tcpclient-connection
https://stackoverflow.com/questions/10341301/get-remote-port-of-a-connected-tcpclient
https://stackoverflow.com/questions/6569486/creating-a-copy-of-an-object-in-c-sharp
https://codeasy.net/lesson/properties

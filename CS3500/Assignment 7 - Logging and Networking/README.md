```
Author:    Drew Lawson and Peter Bruns
Date:      April 4, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NoSleep
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-seven---chatting-nosleep
Commit #:  e843b4b1de6d153c0ba497e9605d3fd884cbcbd0
Solution:  Networking_and_Logging
Copyright: CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# Overview of Solution Functionality
This solution allows clients to connect to a server and send messages to other clients using the server. Also allows server to send messages to all clients from the server user. Both use the Networking class to handle some of the finer details of connecting and sending data. Both clients and servers contain associated loggers to log information about their use.

# Time Expenditures:

	1. Assignment One:	Predicted Hours: 8		Actual Hours: 7
	2. Assignment Two:	Predicted Hours: 9		Acutal Hours: 7
	3. Assignment Three:	Predicted Hours: 7		Actual Hours: 8
	4. Assignment Four:	Predicted Hours: 8		Actual Hours: 8
	5. Assignment Five:	Predicted Hours: 10		Actual Hours: 10
	6. Assignment Six:	Predicted Hours: 8		Acutal Hours: 8
	7. Assignment Seven:	Predicted Hours: 8		Actual Hours: 12

# Time Management and Estimation
Until this assignment, we were very accurate on time predictions. We were also much quicker at implementing the code and spent more of the time on making progress. For this assignment, it took a while to really understand what we needed to do. Because of this, our estimate was off for the amount of time that would be required, and we spent much more time debugging and looking things up than in past assignments. It is still unclear what some of the code does even after this time spent working on the assignment.

# Software Practices
	1. We did a good job naming the variables and methods so that their purpose could be determined from the name. We tried to keep comments to a minimum and only use them when these names were not sufficient to describe the purpose of a section of code.
	2. We setup several helper methods in the client and server that contained code that would be needed in multiple places. This helped keep method bodies as short as possible and should help code readability.
	3. The Networking class helped spearate concerns and reuse code in both the client and server. This way methods that both components used were only written once, and the client and server could be more focused on their specific tasks (e.g., sending messages and updating the GUI).
```
Author:    Drew Lawson and Peter Bruns
Date:      April 16, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: Brent-Spiner-Base
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-nine---web-server-sql-brent-spiner-base
Commit #:  98dc4ae576ba21284830affb1fcd6351a9365189
Solution:  Web_Server_SQL
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# Solution functionality
This solution implements a web server used to interact with a database containing player game information for Agario. It can retrieve data for display on the webpage, insert data into the database, and add new tables to the database.

# Agario client location
The updated Agario client is in a new branch on our previous repository. The GUI code is at: https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-notsosupermario/blob/DatabaseInterfaceingAgario/ClientGUI/ClientGUI.cs and the code used to add data to the database is at: https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-notsosupermario/blob/DatabaseInterfaceingAgario/ClientGUI/DatabaseInterface.cs.

# Database Table Summary
Our database contains 7 tables. These tables contain information for maximum player mass by game, maximum player rank by game, information on the player's location and mass at the time of death, information on the player's start and end times by game, the time it took the player to reach rank number one, the color and ID assigned to the player by the Agario server, and the total time the player played before dying. We tried to keep the number of columns in each table to a minimum. Aside from game ID and player name, there should not be any repeated data in the tables. Several tables (e.g., start and stop time and time to death) contain related information and can be joined eadily on GameID. Aside from the required data, we included the following info: player x at death, player y at death, player mass at death, player ID from the Agario server, and player color.


# Extent of work
The server is able to return a homepage as required by the assignment. The server can also return a chart of highscores and take in a request to store new scores. Because we do not have a separate table for high scores, the new scores are not stored as high scores themselves but are inserted into the proper tables. If they are high score values, then they will be returned the next time high scores are requested. The server has the ability to display a graph of player maximum mass data by game. This can be done through individual score pages accessed through links in the player names in other tables, or by sending a "/graph/[name]" command. The server also has the ability to create new tables in the database and seed them when it detects the "create" command. These tables must not already exist in the database for the command to work. If they do already exist, an exception is thrown. Along with the requirements, we included tables to display information about the player at the time of death and player game time. Game scores for individual players can be accessed with the "scores/[player]" command or by clicking the player's name in any other table.


# Partnership information
Aside from a small part described in branching, all code was completed as a pair. The only part not done as a pair was separating database access code into its own class. We both contributed equally to the code.


# Branching
Peter made one separate branch in order to separate the SQL database portion of the code into a separate class from the server portion. This branch was merged back into main at commit 7da233ecc8cec600dff7b6108bef07f12d69b5eb.

# Testing
All testing was done manually. We would start the server and click on every link to make sure that the proper page and data displayed. Seevral times, we removed every table from the database in SSMS to make sure that we could recreate the tables. We also used the insertion scores command to add new data several times. We would then check every link again to make sure that the data inserted correctly. We also played several games of Agario to make sure that the data from the game was being sent to the database.

# Time Expenditures:

	1. Assignment One:	Predicted Hours: 8		Actual Hours: 7
	2. Assignment Two:	Predicted Hours: 9		Acutal Hours: 7
	3. Assignment Three:	Predicted Hours: 7		Actual Hours: 8
	4. Assignment Four:	Predicted Hours: 8		Actual Hours: 8
	5. Assignment Five:	Predicted Hours: 10		Actual Hours: 10
	6. Assignment Six:	Predicted Hours: 8		Acutal Hours: 8
	7. Assignment Seven:	Predicted Hours: 8		Actual Hours: 12
	8. Assignment Eight:	Predicted Hours: 8		Actual Hours: 10
	9. Assignment Nine:	Predicted Hours: 8		Actual Hours: 8

# Time Tracking
We estimated that the assignment would take 8 hours to complete, and it actually took about 8 hours. This was broken up into 5 hours working on this project, 1 hour working on Agario, and 2 hours for commenting, READMEs, and slight changes to the code. There was relatively little time spent debugging when compared to the previous assignments. We feel that we were very efficient in implementing the code for this assignment. The time estimate was much better than it has been. This supports our idea that GUIs take longer to implement than anticipated.
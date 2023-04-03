```
Author:    Drew Lawson and Peter Bruns
Date:      April 16, 2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NotSoSuperMario
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-notsosupermario
Commit #:  92852dc8b07c834f743abde51f96146e8977fe2c
Solution:  Agario
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# Overview of Solution Functionality
This solution provides a simple implementation of an Agario like game. It contains a model of a game world and the methods needed for a GUI to interact with it. The GUI allows a player to move a game object based on mouse location and to split on pressing the space bar. The GUI also handles communications with an Agario server and passes the information along to the game world model.

# User interface and game design decisions
We based our overall design on the provided example GUI. When the GUI starts, the screen displays text boxes that the player can use to input a name and a server address. The server address automatically fills with "localhost" at the start to help speed up connection extablishment during testing. We also gave the user a connect button to press that activates when the name and server boxes both contain at least one non-whitespace character. This was done to help make sure that the player's name cannot be blank and that a server address is provided. Below the connection button, there is a label that will become visibile if any errors occur and display a simple error message. We chose to only display a simplified message to limit the amount of text printed to the screen. When the game starts, the name box, address box, and connect button are hidden and a gray box is drawn to the screen to serve as the game background. We chose gray because we thought that it would be easier to look at for an extened period of time than a white background. For this same reason, we chose to make the GUI backgound a light blue color. While the player is playing, the screen displays information about the player's mass, the number of active players and foods, the player's position in the game world, and the position of the mouse in the game world. We chose to leave off information about the FPS and HPS because we did not think that these were necessary for the player to see and it would start to look too cluttered. Below these stats, there is a disconnect button that returns the player to the startup screen and displays a disconnection message in the label below the connect button. This allows the player to reconnect under a different name or to a different server. When the player dies, a message box is displayed that shows how long the player was alive, their final mass, and the final position. This box also allows the player to begin a new game or to quit the game. Quitting the game will return the player to the startup screen. For gameplay, we chose to base GUI updates on a timer rather than heartbeat messages. This seemed to work better in our tests. The only way for a player to connect to a server is by pressing the connect button. We could not get pressing the enter key to allow for connection for some reason.

# Partnership
Except when specified in the Branching section, all programming was done as a pair. Of the 10 total hours the assignment took, 7 were done as a pair and 3 were done by Peter individually. The GUI setup, AgarioModels implementation, and work on drawing game objects to the screen were done together. Peter worked separately to refine drawing the whole world to scale, on protal drawing, and on mouse tracking.

# Branching
Peter made two separate branches for the assignment. The first was used to work on drawing the whole world to scale on the screen and was merged back to the master branch at commit 7dd450e0a5cad0aed41012cefa4c9d1bfa664769. The second was used to work on drawing the zoomed in portal view and tracking the mouse for movement and splitting. This second branch was merged back into the master branch at commit 8f85d7ae4b3192d9f055a4a1384f56af77bfb33b.

# Testing
All testing was done manually by connecting to the server and playing the game. We tried to make sure that disconnection by the client or server shutting down would not crash the GUI, that movement and splitting seemed to result in reasonable changes in position, that the hitbox and size of world objects seemed reasonable, and that player's could choose to keep playing or quit after death. This was all done through trial and error. We tried to ensure correctness by simulating any player action or server error that we could think of and making sure that the GUI responded properly.

# Time Expenditures:

	1. Assignment One:	Predicted Hours: 8		Actual Hours: 7
	2. Assignment Two:	Predicted Hours: 9		Acutal Hours: 7
	3. Assignment Three:	Predicted Hours: 7		Actual Hours: 8
	4. Assignment Four:	Predicted Hours: 8		Actual Hours: 8
	5. Assignment Five:	Predicted Hours: 10		Actual Hours: 10
	6. Assignment Six:	Predicted Hours: 8		Acutal Hours: 8
	7. Assignment Seven:	Predicted Hours: 8		Actual Hours: 12
	8. Assignment Eight:	Predicted Hours: 8		Actual Hours: 10

# Time Tracking
We estimated that the assignment would take 8 hours to complete. It actually took about 10 hours to complete. We were under by 2 hours but closer to the estimate than we were for Assignment 7. The take away seems to be that GUIs take longer to implement than we expect unless the model is already rigorously tested (like the Spreadsheet). Of the total time, 1 hour was spent on initial setup, 8 hours were spent working on the code, and 1 additional hour was needed to write README content and more comments. The majority of the time spent on the code was spent figuring out how to draw the zoomed in portal view and to get the mouse location in the world from its screen location. Most of this time was spent debugging. Of the 10 total hours the assignment took, 7 were done as a pair and 3 were done by Peter individually.
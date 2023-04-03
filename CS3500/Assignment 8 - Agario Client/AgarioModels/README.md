```
Authors:    Drew Lawson and Peter Bruns
Team Name:   NotSoSuperMario
Date:      14-Apr-2022
Course:    CS 3500, University of Utah, School of Computing
GitHub ID: NotSoSuperMario
Repo:      https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-eight---agario-notsosupermario
Commit #:  5ba61afe847c02d634368dbdcfdf4e8f475bfdd2
Project:   AgarioModels
Copyright: CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
```

# Project description
This project contains the model used by the ClientGUI to store and process game information for Agario. It defines GameObjects (Food and Players) and keeps track of the state of the game world. The game world tracks active food, active players, and the positions of every game object in the world.

# Assignment time estimate and actual time
The overall assignment took 10 hours to complete. We estimated it would take 8 hours. We worked on this project for about an hour in total. This time was split between the intitial definition of the required classes and their properties, and intermittent addition of methods as needed by the GUI.

# Comments to Evaluators:
We decided to use Vector2 over PointF because we felt a vector is a more accurate representation of the game world. In particular, the line between the player and the mouse position is better represented as a vector for movement.

# Assignment specific topics
This project provided more practice in MVC architecture implementation because it was the model for the Agario game. It also provided experience in setting up classes for use with JSON serialization and deserialization.

# Consulted peers
We did not consult any peers for this project.

# References
Along with course material, we consulted the following websites:
https://www.newtonsoft.com/json/help/html/serializationattributes.htm#JsonIgnoreAttribute
https://makolyte.com/csharp-deserialize-a-json-array-to-a-list/
https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-character-casing
https://docs.microsoft.com/en-us/dotnet/api/system.numerics.vector2?view=net-6.0
https://docs.microsoft.com/en-us/dotnet/api/system.drawing.pointf?view=net-6.0
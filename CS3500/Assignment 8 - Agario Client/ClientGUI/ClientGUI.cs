/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: NotSoSuperMario
/// Date: April 14, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// This file contains the code used to implement user interaction with the Agario GUI and communication with the Agario server. It
/// serves as the view and controller in the MVC architecture for the Agario game. Players are able to enter a name and the address of a
/// server to connect to and send game commands (movement and split) to the server. This class passes received information to the 
/// AgarioModels project for processing and storage of information regarding the state of the game world.
/// 
///</summary>

using Communications;
using AgarioModels;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using FileLogger;
using System.Diagnostics;

namespace ClientGUI
{
    ///<summary>
    /// This class contains the code used to implement user interaction with the Agario GUI and communication with the Agario server. It
    /// serves as the view and controller in the MVC architecture for the Agario game. Players are able to enter a name and the address of a
    /// server to connect to and send game commands (movement and split) to the server. This class passes received information to the 
    /// AgarioModels project for processing and storage of information regarding the state of the game world.
    ///</summary>
    public partial class ClientGUI : Form
    {

        private bool connected;
        private int screenWidth;
        private int screenHeight;
        private long clientPlayerID;
        private World gameWorld;
        private Networking clientNetwork;
        private ILogger logger;
        private System.Windows.Forms.Timer invalidateTimer;
        private Stopwatch lifeTimer;
        // Set the displayed client radius to a constant and define the width of the portal
        private readonly int clientRadius = 30;
        private readonly float widthMultiplier = 15f;

        /// <summary>
        /// Constructor method for a new Agario client GUI form.
        /// </summary>
        public ClientGUI()
        {
            InitializeComponent();
            logger = new CustomLogger("Client_GUI");
            connected = false;
            this.DoubleBuffered = true;
            this.Paint += Draw_Scene;
            screenHeight = 900;
            screenWidth = 900;
            clientNetwork = new Networking(NullLogger.Instance, clientConnect, clientDisconnect, messageReceived, '\n');
            gameWorld = new World();
            lifeTimer = new Stopwatch();
            // Set server address to localhost for quicker connection while testing
            serverAddressBox.Text = "localhost";
            invalidateTimer = new System.Windows.Forms.Timer();
            // The next two lines set the timer to tick 30 times a second. Each
            // tick causes the Invalidate method to run and the form to be redrawn.
            invalidateTimer.Interval = 1000 / 30;
            invalidateTimer.Tick += (a, b) => this.Invalidate();
        }

        /// <summary>
        /// Callback method from the Networking class used to handle messages received from the server.
        /// </summary>
        /// <param name="channel">The Networking object that received the message</param>
        /// <param name="message">The message received from the server</param>
        private void messageReceived(Networking channel, string message)
        {
            switch (message)
            {
                // Case for when a server adds the client to a game
                case string receivedMessage when receivedMessage.StartsWith(Protocols.CMD_Player_Object):
                    // Remove protocol identifier and parse the client player's server provided ID number
                    message = message[Protocols.CMD_Player_Object.Length..];
                    clientPlayerID = long.Parse(message);
                    // Start the scene drawing timer and the timer that will show how long the player lived
                    invalidateTimer.Start();
                    lifeTimer.Start();
                    logger.LogInformation($"Received CMD_Player_Object message to start game\n");
                    break;

                // Case for when the server sends a list of food to be added to the world
                case string receivedMessage when receivedMessage.StartsWith(Protocols.CMD_Food):
                    // Remove the message protocol identifier and send the (JSON serialized) message to the 
                    // game world to add active foods
                    message = message[Protocols.CMD_Food.Length..];
                    new Thread(() => gameWorld?.AddFood(message)).Start();
                    logger.LogInformation($"Received CMD_Food message\n");
                    break;
                
                // Case for when the server sends a list of active players to the client
                case string receivedMessage when receivedMessage.StartsWith(Protocols.CMD_Update_Players):
                    // Remove the message protocol identifier and send the (JSON serialized) message to the 
                    // game world to store currently active players
                    message = message[Protocols.CMD_Update_Players.Length..];
                    new Thread (() => gameWorld?.UpdatePlayers(message)).Start();
                    logger.LogInformation($"Received CMD_Update_Players message\n");
                    break;

                // Case for when the server sends a message containg ID of eaten foods to be removed from
                // the world
                case string receivedMessage when receivedMessage.StartsWith(Protocols.CMD_Eaten_Food):
                    // Remove the message protocol identifier and send the message to the 
                    // game world object to remove eaten foods from the world
                    message = message[Protocols.CMD_Eaten_Food.Length..];
                    new Thread(() => gameWorld?.RemoveFood(message)).Start();
                    logger.LogInformation($"Received CMD_Eaten_Food message\n");
                    break;

                // Case for when the server sends a message containing ID's of dead players
                case string receivedMessage when receivedMessage.StartsWith(Protocols.CMD_Dead_Players):
                    // Remove the message protocol identifier and use a helper method to look for the client
                    // player ID in the list of dead players
                    message = message[Protocols.CMD_Dead_Players.Length..];
                    ParseDeadPlayers(message);
                    break;
            }
        }

        /// <summary>
        /// Method used to extract player ID numbers from a list of dead players and look for the client player's ID.
        /// Sends the list of dead players to the game world for player removal.
        /// </summary>
        /// <param name="message">The list of dead players from the server</param>
        private void ParseDeadPlayers(string message)
        {
            // Trim open and closing brackets at either end of the message
            message = message[1..(message.Length - 1)];
            if (message == "")
            {
                return;
            }
            // Split the message into a string array of dead player ID numbers to send to the game world to
            // remove dead players from the collection of active players
            string[] deadPlayers = message.Split(',');
            new Thread(() => gameWorld.RemovePlayers(deadPlayers)).Start();
            // Look for the client player ID in the list of dead players. Call a helper method to handle
            // client player death if it is found.
            foreach (string deadPlayer in deadPlayers)
            {
                long deadID = long.Parse(deadPlayer);
                if (deadID == clientPlayerID)
                {
                    HandlePlayerDeath();
                }
            }
        }

        /// <summary>
        /// Helper method used to update the GUI when the client player has died. Gives the player the option to continue
        /// playing or to quit the game.
        /// </summary>
        private void HandlePlayerDeath()
        {
            logger.LogInformation("Client player died\n");
            invalidateTimer.Stop();
            lifeTimer.Stop();
            // Show a popup box displaying player game stats and asking if the player wishes to continue playing
            var keepPlaying = MessageBox.Show($"You have died. Keep playing?\nTotal time: {lifeTimer.ElapsedMilliseconds/1000} seconds\nFinal mass: {playerMassLabel.Text}" +
                $"\nFinal Position: {positionLabel.Text}", "Death", MessageBoxButtons.YesNo);
            // If the player wishes to continue, send a new game command protocol to the server and start the life timer and
            // timer used to update the display
            if (keepPlaying == DialogResult.Yes)
            {
                logger.LogInformation("Selected to continue playng\n");
                clientNetwork.Send(String.Format(Protocols.CMD_Start_Game, playerNameBox.Text));
                invalidateTimer.Start();
                lifeTimer = new Stopwatch();
                lifeTimer.Start();
            } 
            // If the player does not want to keep playing, disconnect from the server and return to the GUI startup screen
            else
            { 
                clientNetwork.Disconnect();
                connected = false;
                errorMessageLabel.Text = "Disconnected from server";
                this.Invalidate();
            }
        }

        /// <summary>
        /// Callback method from the networking class to handle disconnection from the server. Updates the GUI.
        /// </summary>
        /// <param name="channel"></param>
        private void clientDisconnect(Networking channel)
        {
            connected = false;
            errorMessageLabel.Text = "Disconnected from server";
            logger.LogInformation("Client disconnected from server");
        }

        /// <summary>
        /// Callback method from the networking class used to handle an established connection with the server. Switches the GUI from
        /// the startup screen to the game display.
        /// </summary>
        /// <param name="channel"></param>
        private void clientConnect(Networking channel)
        {
            logger.LogInformation("Connected to server\n");
            try
            {
                // Send the game start request protocol to the server and wait for messages
                clientNetwork.Send(String.Format(Protocols.CMD_Start_Game, playerNameBox.Text));
                clientNetwork.ClientAwaitMessagesAsync();
                // Hide all startup screen text boxes and connect button then enable the disconnect button
                enterNameLabel.Visible = false;
                serverAddressLabel.Visible = false;
                playerNameBox.Visible = false;
                serverAddressBox.Visible = false;
                connectButton.Visible = false;
                connected = true;
                errorMessageLabel.Visible = false;
                disconnectButton.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.LogDebug($"Failed to wait for messages from server. ${ex.Message}\n");
                errorMessageLabel.Text = "Waiting for messages failed.";
            }
        }

        /// <summary>
        /// Method called to update the GUI whenever the invalidation timer ticks. Contains different
        /// instructions depending on whether the client is currently connected and playing a game, or is disconnected.
        /// </summary>
        private void Draw_Scene(object? sender, PaintEventArgs e)
        {
            if (connected)
            {
                // Draws a gray box to server as a game back drop
                DrawBox(e);
                Dictionary<long, Player> activePlayers = gameWorld.GetActivePlayers();
                Dictionary<long, Food> activeFood = gameWorld.GetCurrentFood();
                Player clientPlayer;
                try
                {
                    clientPlayer = gameWorld.GetClientPlayer(clientPlayerID);
                }
                // Don't do anything if the client player has not been added to the collection of
                // active players
                catch
                {
                    return;
                }
                // Helper method to draw players and food onto the game display
                DrawGameWorldObjects(clientPlayer, activePlayers, activeFood, e);
                logger.LogInformation("Drew scene to GUI");
                int playerX = (int)clientPlayer.X;
                int playerY = (int)clientPlayer.Y;
                // Send a movement command to the server
                SendMoveOrSplit(playerX, playerY, false);
            }
            else
            {
                // Method used to clear the game back drop if the player disconnects
                ClearBox(e);
                // Return GUI to startup screen by showing hidden items again
                enterNameLabel.Visible = true;
                serverAddressLabel.Visible = true;
                playerNameBox.Visible = true;
                serverAddressBox.Visible = true;
                connectButton.Visible = true;
                disconnectButton.Enabled = false;
            }
        }

        /// <summary>
        /// Method used to draw food and players onto the game world. Allows for zooming in on the client player and calculation of
        /// the bounds of the zoom portal. Draws all objects to scale based on portal size and in their positition relative to the
        /// client player in the portal. Draws the client player separately from the rest of the players.
        /// </summary>
        /// <param name="clientPlayer">The client player object</param>
        /// <param name="activePlayers">A dictionary containing active players in the game</param>
        /// <param name="activeFood">A dictionary containing active food in the game</param>
        /// <param name="painter">A paint event that is required to draw items to the screen</param>
        private void DrawGameWorldObjects(Player clientPlayer, Dictionary<long, Player> activePlayers, Dictionary<long, Food> activeFood, PaintEventArgs painter)
        {
            // Calculate the bounds of the zoomed in portal view
            float leftBound, rightBound, topBound, bottomBound;
            leftBound = clientPlayer.X - widthMultiplier * (float)clientPlayer.Radius;
            rightBound = clientPlayer.X + widthMultiplier * (float)clientPlayer.Radius;
            topBound = clientPlayer.Y - widthMultiplier * (float)clientPlayer.Radius;
            bottomBound = clientPlayer.Y + widthMultiplier * (float)clientPlayer.Radius;
            // Draw all players that are in the portal region except for the client player
            DrawPlayers(activePlayers, leftBound, rightBound, topBound, bottomBound, clientPlayer.Radius, painter);
            // Draw all foods that are in the portal region
            DrawFood(activeFood, leftBound, rightBound, topBound, bottomBound, clientPlayer.Radius, painter);
            // Draw client player on top with name label
            SolidBrush clientBrush = new(Color.FromArgb(clientPlayer.ARGBColor));
            painter.Graphics.FillEllipse(clientBrush, new Rectangle(screenWidth / 2 + 10 - clientRadius, screenHeight / 2 + 10 - clientRadius, clientRadius * 2, clientRadius * 2));
            SolidBrush nameBrush = new(Color.White);
            painter.Graphics.DrawString(clientPlayer.Name, DefaultFont, nameBrush, screenWidth / 2 - clientRadius / 2, screenHeight / 2 - clientRadius / 2);
            playerMassLabel.Text = ((int)clientPlayer.Mass).ToString();
            positionLabel.Text = $"{(int)clientPlayer.X}, {(int)clientPlayer.Y}";
        }

        /// <summary>
        /// Draws foods that are contained within the zoomed in portal region and updates the label showing the number of foods.
        /// </summary>
        /// <param name="activeFood">A dicitonary of active food</param>
        /// <param name="leftBound">The left bound of the portal in the world</param>
        /// <param name="rightBound">The right bound of the portal in the world</param>
        /// <param name="topBound">The top bound of the portal in the world</param>
        /// <param name="bottomBound">The bottom bound of the portal in the world</param>
        /// <param name="actualClientRadius">The actual radius of the client player (Note: separate from the constant
        /// display radius defined as a field)</param>
        /// <param name="painter">A paint event that is required to draw items to the screen</param>
        private void DrawFood(Dictionary<long, Food> activeFood, float leftBound, float rightBound, float topBound, float bottomBound, double actualClientRadius, PaintEventArgs painter)
        {
            int foodCount = activeFood.Count();
            // There should never be more than 3000 foods. A bug was displaying more than that sometimes even though
            // debug always shows 3000 foods in the dictionary
            switch (foodCount)
            {
                case >= 3000:
                    this.numberOfFoodsLabel.Text = "3000";
                    break;
                default:
                    this.numberOfFoodsLabel.Text = foodCount.ToString();
                    break;

            }
            // Use a helper method to draw the food
            foreach (long id in activeFood.Keys)
            {
                Food food = activeFood[id];
                DrawObject(food, leftBound, rightBound, topBound, bottomBound, actualClientRadius, painter);
            }
        }

        /// <summary>
        /// Draws players that are contained within the zoomed in portal region and updates the label showing the number of players.
        /// </summary>
        /// <param name="activePlayers">A dicitonary of active players</param>
        /// <param name="leftBound">The left bound of the portal in the world</param>
        /// <param name="rightBound">The right bound of the portal in the world</param>
        /// <param name="topBound">The top bound of the portal in the world</param>
        /// <param name="bottomBound">The bottom bound of the portal in the world</param>
        /// <param name="actualClientRadius">The actual radius of the client player (Note: separate from the constant
        /// display radius defined as a field)</param>
        /// <param name="painter">A paint event that is required to draw items to the screen</param>
        private void DrawPlayers(Dictionary<long, Player> activePlayers, float leftBound, float rightBound, float topBound, float bottomBound, double actualClientRadius, PaintEventArgs painter)
        {
            // Make a brush that will be used to label player circles with their names
            SolidBrush nameBrush = new(Color.White);
            this.numberOfPlayersLabel.Text = activePlayers.Count.ToString();
            foreach (long id in activePlayers.Keys)
            {
                // Do not draw the client player here
                if (id == clientPlayerID)
                {
                    continue;
                }
                Player player = activePlayers[id];
                // Helper method used to draw the player circle
                DrawObject(player, leftBound, rightBound, topBound, bottomBound, actualClientRadius, painter);
            }
        }

        /// <summary>
        /// Helper method used to display food and player objects contained in the zoomed in portal. Draws objects in their positon in the 
        /// portal relative to the client player and at a size scaled to the client player display size.
        /// </summary>
        /// <param name="gameObject">The object to be drawn onto the screen if it is in the portal</param>
        /// <param name="leftBound">The left bound of the portal in the world</param>
        /// <param name="rightBound">The right bound of the portal in the world</param>
        /// <param name="topBound">The top bound of the portal in the world</param>
        /// <param name="bottomBound">The bottom bound of the portal in the world</param>
        /// <param name="actualClientRadius">The actual radius of the client player (Note: separate from the constant
        /// display radius defined as a field)</param>
        /// <param name="painter">A paint event that is required to draw items to the screen</param>
        private void DrawObject(GameObject gameObject, float leftBound, float rightBound, float topBound, float bottomBound, double actualClientRadius, PaintEventArgs painter)
        {
            SolidBrush brush = new(Color.FromArgb(gameObject.ARGBColor));
            // Don't draw objects that would not be contained in the zoomed portal
            if ((gameObject.X + gameObject.Radius < leftBound || gameObject.X > rightBound) || (gameObject.Y + gameObject.Radius < topBound || gameObject.Y > bottomBound))
            {
                return;
            }
            // Calculate x and y offset from the left and top bounds of the portal view
            float xOffset = gameObject.X - leftBound;
            float yOffset = gameObject.Y - topBound;
            float regionHeight = bottomBound - topBound;
            float regionWidth = rightBound - leftBound;
            // Get the ratio between the offsets and the portal width and height
            float widthPercentage = xOffset / regionWidth;
            float heightPercentage = yOffset / regionHeight;
            // Calculate the object's location within the screen by multiplying the above ratios by screen dimensions
            float screenXFloat = widthPercentage * screenWidth;
            float screenYFloat = heightPercentage * screenHeight;
            // Cast to int for display with a Rectangle object
            int screenX = (int)screenXFloat;
            int screenY = (int)screenYFloat;
            // Calculate the object's scaled radius for display based on the ratio between the client player's displayed
            // radius and its actual radius in the game world
            float scaledRadiusFloat = (float)gameObject.Radius * (float)clientRadius / (float)actualClientRadius;
            int scaledRadius = (int)scaledRadiusFloat;
            painter.Graphics.FillEllipse(brush, new Rectangle(screenX + 10 - scaledRadius, screenY + 10 - scaledRadius,
               scaledRadius * 2, scaledRadius * 2));
            // Draw the player name on top of player circles
            if (gameObject is Player)
            {
                Player player = (Player)gameObject;
                SolidBrush nameBrush = new(Color.White);
                painter.Graphics.DrawString(player.Name, DefaultFont, nameBrush, screenX, screenY);
            }
        }

        /// <summary>
        /// Removes the game backdrop for returning the GUI to the startup display screen
        /// </summary>
        private void ClearBox(PaintEventArgs e)
        {
            SolidBrush brush = new(Color.LightCyan);
            Pen pen = new(new SolidBrush(Color.LightCyan));
            e.Graphics.DrawRectangle(pen, 10, 10, 10 + screenWidth, 10 + screenHeight);
            e.Graphics.FillRectangle(brush, 10, 10, 10 + screenWidth, 10 + screenHeight);
        }

        /// <summary>
        /// Draws a gray box to serve as a game backdrop
        /// </summary>
        private void DrawBox(PaintEventArgs e)
        {
            SolidBrush brush = new(Color.Gray);
            Pen pen = new(new SolidBrush(Color.Gray));
            e.Graphics.DrawRectangle(pen, 10, 10, 10 + screenWidth, 10 + screenHeight);
            e.Graphics.FillRectangle(brush, 10, 10, 10 + screenWidth, 10 + screenHeight);
        }

        /// <summary>
        /// Method used to handle clicking the connect button. Establishes a connection with the server or displays
        /// an error message.
        /// </summary>
        private void connectButton_Click(object sender, EventArgs e)
        {
            logger.LogInformation($"Attempting to connect to server at {serverAddressBox.Text}\n");
            try
            {
                clientNetwork.Connect(serverAddressBox.Text, 11000);
            }
            catch (Exception ex)
            {
                errorMessageLabel.Text = "Failed to connect to server.";
                errorMessageLabel.Visible = true;
                logger.LogDebug($"Failed to connect to server. ${ex.Message}\n");
            }
        }

        /// <summary>
        /// Handles clicking the disconnect button. Disconnects the client from the server ending the game. Returns
        /// GUI display to the startup screen.
        /// </summary>
        private void disconnectButton_Click(object sender, EventArgs e)
        {

            logger.LogInformation("Disonnected from server\n");
            clientNetwork.Disconnect();
            connected = false;
            errorMessageLabel.Visible = true;
            errorMessageLabel.Text = "Disconnected from server";
            this.Invalidate();
        }

        /// <summary>
        /// This method is used to ensure that the player cannot connect to the server unless their
        /// name contains at least one non-whitespace character. Toggles the connect button between
        /// active and inactive.
        /// </summary>
        private void playerNameBox_TextChanged(object sender, EventArgs e)
        {
            if (serverAddressBox.Text.Trim() == "" || playerNameBox.Text.Trim() == "")
            {
                connectButton.Enabled = false;
            } else
            {
                connectButton.Enabled = true;
            }
        }

        /// <summary>
        /// This method is used to ensure that the player cannot connect to the server unless the
        /// server address given contains at least one non-whitespace character. Toggles the connect
        /// button between active and inactive
        /// </summary>
        private void serverAddressBox_TextChanged(object sender, EventArgs e)
        {
            if (serverAddressBox.Text.Trim() == "" || playerNameBox.Text.Trim() == "")
            {
                connectButton.Enabled = false;
            }
            else
            {
                connectButton.Enabled = true;
            }
        }

        /// <summary>
        /// Used to recognize space bar press in the game. Sends the split command to the server on space press.
        /// </summary>
        private void ClientGUI_KeyUp(object sender, KeyEventArgs e)
        {
            // Send a command to split in the direction of the mouse
            if (e.KeyCode == Keys.Space)
            {
                Player clientPlayer;
                try
                {
                    clientPlayer = gameWorld.GetClientPlayer(clientPlayerID);
                }
                // Don't do anything if the client player has not been added to the collection of
                // active players
                catch
                {
                    return;
                }
                int playerX = (int)clientPlayer.X;
                int playerY = (int)clientPlayer.Y;
                // Send a split command to the server
                SendMoveOrSplit(playerX, playerY,true);
                logger.LogInformation("Sent split command to server\n");
            }
        }

        /// <summary>
        /// Method used to send the mouse position and movement protocol to the server. Sends either a movement command
        /// or a command to split towards a point.
        /// </summary>
        /// <param name="playerX">The client player's x-coordinate in the world</param>
        /// <param name="playerY">The client player's y-coordinate in the world</param>
        /// <param name="split">A flag for whether the command will be to move the player or split the player circle</param>
        private void SendMoveOrSplit(int playerX, int playerY, bool split)
        {
            // Get the player's position on the desktop display
            int playerScreenPositionX = screenWidth / 2 + this.DesktopLocation.X;
            int playerScreenPositionY = screenHeight / 2 + this.DesktopLocation.Y;
            // Get the position of the mouse on the display relative to the location
            // of the player on the display
            int mouseXOffset = Cursor.Position.X - playerScreenPositionX;
            int mouseYOffset = Cursor.Position.Y - playerScreenPositionY;
            // Add the relative mouse x and y positions to the player's x and y
            // positions in the game world
            int mouseX = playerX + mouseXOffset;
            int mouseY = playerY + mouseYOffset;
            // Send command to server depending on split flag
            if (!split)
            {
                clientNetwork.Send(String.Format(Protocols.CMD_Move, mouseX, mouseY));
            }
            else
            {
                clientNetwork.Send(String.Format(Protocols.CMD_Split, mouseX, mouseY));
            }
            logger.LogInformation($"Received CMD_Dead_Players message. Sent movement command to server\n");
            cursorPositionLabel.Text = $"{mouseX}, {mouseY}";
        }
    }
}
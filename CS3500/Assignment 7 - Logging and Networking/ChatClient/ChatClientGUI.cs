/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: NoSleep
/// Date: April 4, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// This file contains the methods needed to implement a simple chat client used to communicate over a server. Has methods
/// for connecting to the server, sending and receiving messages, changing display name, and logging the client's chat activity.
/// 
///</summary>

using Communications;
using Microsoft.Extensions.Logging;

namespace ChatClient
{
    /// <summary>
    /// This class contains the methods needed to implement a simple chat client used to communicate over a server. Has methods
    /// for connecting to the server, sending and receiving messages, changing display name, and logging the client's chat activity.
    /// </summary>
    public partial class ChatClientGUI : Form
    {

        // Networking object is nullable so that a network connection is not created until
        // the client attempts to connect to a server
        private Networking? networkConnection;
        private bool connected;
        private readonly ILogger<ChatClientGUI> clientLogger;

        /// <summary>
        /// Constructor method for a new ChatClientGUI. The required logger object will
        /// be passed in through dependency injection.
        /// </summary>
        public ChatClientGUI(ILogger<ChatClientGUI> logger)
        {
            InitializeComponent();
            connected = false;
            clientLogger = logger;
            // Set server address to localhost initially for easier connection to
            // a server hosted on the local machine
            serverAddressBox.Text = "localhost";
        }

        /// <summary>
        /// Method used to connect the client to a server when the client is not connected and
        /// the Connect button is clicked. If the client is connected, disconnects the client 
        /// when the same button is clicked.
        /// </summary>
        private void connectButton_Click(object sender, EventArgs e)
        {
            // Invoke a callback method with the Networking object to
            // handle the client disconnection if the client is currently connected
            if (connected && networkConnection != null)
            {
                ClientDisconnect(networkConnection);
            }
            else
            {
                try
                {
                    chatLog.AppendText("Trying to connect to server\n");
                    clientLogger.LogInformation($"Trying to connect to server at {serverAddressBox.Text}\n");
                    // Invoke a helper method to connect the client to a server. Use a new thread so
                    // that the client remains otherwise functional while the connection is established
                    new Thread(EstablishConnection).Start();
                }
                // Handle a failure to connect to the specified server
                catch (Exception ex)
                {
                    chatLog.AppendText($"Connection failed with the following message: {ex.Message}\n");
                    clientLogger.LogDebug($"Failed to try to connect to server at: {serverAddressBox.Text} with error message: {ex.Message}\n");
                }
            }
        }


        /// <summary>
        /// Helper method used to connect the client to a chat server.
        /// </summary>
        private void EstablishConnection()
        {
            // Make a new networking object to communicate with the server 
            networkConnection = new Networking(clientLogger, ClientConnect, ClientDisconnect, ClientMessage, '\n');
            // For this assignment, the port number will always be 11,000
            networkConnection.Connect(serverAddressBox.Text, 11000);
        }

        /// <summary>
        /// Method used to handle the receipt of a message from the server. Updates the GUI as required
        /// and logs the received message.
        /// </summary>
        /// <param name="channel">The networking object that </param>
        /// <param name="message">The received message</param>
        private void ClientMessage(Networking channel, string message)
        {
            // Command Participants indicates that a list of participants has been received.
            // Call a helper method too update the participants list text box.
            if (message.StartsWith("Command Participants,"))
            {
                UpdateParticipantsList(message);
            }
            // Server disconnected indicates that the server has been disconnected by the server
            else if (message.StartsWith("Server disconnected"))
            {
                // Reset the connect button and networking object to allow for a new server connection
                Invoke(() => connectButton.Text = "Connect");
                connected = false;
                networkConnection = new Networking(clientLogger, ClientConnect, ClientDisconnect, ClientMessage, '\n');
                // Let the user know the server disconnected and remove all participants
                Invoke(() => chatLog.AppendText("Server disconnected by server\n"));
                Invoke(() => { this.participantsList.Text = ""; });
                // Because the server disconnected on purpose, log this as information
                clientLogger.LogInformation("Server disconnected by server\n");
            }
            // Every other message should be printed to the chat log and log it
            else
            {
                Invoke(() => { chatLog.AppendText(message); });
                clientLogger.LogInformation($"Received message: {message}\n");
            }
        }

        /// <summary>
        /// Method used to update the chat participant list when a participant joins, 
        /// leaves, or changes name.
        /// </summary>
        /// <param name="message">The message containing the names of the current participants</param>
        private void UpdateParticipantsList(string message)
        {
            Invoke(() => this.participantsList.Text = "");
            // The message containing the list of participants will
            // start with Command Participants, trim this off
            message = message.Replace("Command Participants,", "");
            // Each name will be separated by a comma, loop through names and add them to the participants
            // list by finding each comma until there is only a comma left in the string
            int terminatorPosition = message.IndexOf(',');
                while (terminatorPosition >= 0)
                {
                    string name = message.Substring(0, terminatorPosition);
                    message = message.Remove(0, terminatorPosition + 1);
                    Invoke(() => this.participantsList.AppendText($"{name}\n"));
                    terminatorPosition = message.IndexOf(',');
                }
            Invoke(() => this.participantsList.AppendText(message));
            clientLogger.LogInformation($"Updated participants list. Current participants: {participantsList.Text}");
        }

        /// <summary>
        /// Callback method used to disconnect the client from the currently connected server. Enables the
        /// client to connect to a new server.
        /// </summary>
        /// <param name="channel">The Networking object that maintains the connection between the client and
        /// the server.</param>
        private void ClientDisconnect(Networking channel)
        {
            connectButton.Text = "Connect";
            connected = false;
            // The server uses Command Disconnect to handle the connection on its end
            channel.Send("Command Disconnect\n");
            Invoke(() => { this.chatLog.AppendText("Disconnected from server: " + serverAddressBox.Text + "\n"); });
            // Clear the participants list and log the disconnection (as info because it was purposeful)
            Invoke(() => { this.participantsList.Text = ""; });
            clientLogger.LogInformation($"Client disconnected from server: {serverAddressBox.Text}\n");
        }

        /// <summary>
        /// Callback method used to handle the connection of the client to a server.
        /// </summary>
        /// <param name="channel"></param>
        private void ClientConnect(Networking channel)
        {
            clientLogger.LogInformation($"Connected to server at: {serverAddressBox.Text}\n");
            Invoke(() => { this.connectButton.Text = "Disconnect";});
            // Update the networking object's ID and let the server know the participant name
            channel.ID = nameBox.Text.Trim();
            channel.Send($"Command Name {nameBox.Text}\n");
            clientLogger.LogInformation($"Set name to {nameBox.Text}\n");
            // Try to wait for messages from the server. If this fails, let the user know and log the information
            try
            {
              channel.ClientAwaitMessagesAsync();
            } 
            catch (Exception ex)
            {
                Invoke(() => { chatLog.AppendText("Waiting for message failed with the following message: " + ex.Message + "\r\n"); });
                clientLogger.LogDebug("Waiting for message failed with the following message: " + ex.Message + "\r\n");
            }
            connected = true;
        }

        /// <summary>
        /// Method used to send a message to the server when enter is pressed when the text to send box is 
        /// selected. Sends the message to the server for handling.
        /// </summary>
        private void textToSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (connected)
                {
                    // Add a newline to indicate the end of the message to the server
                    networkConnection?.Send(textToSend.Text + "\n");
                    clientLogger.LogInformation($"Sent message: {textToSend.Text}\n");
                    // Mute the beeping sound when the message is sent properly
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else
                {
                    chatLog.AppendText("Please connect to a server\n");
                    clientLogger.LogDebug($"Tried to send {textToSend.Text} but not connected to server.\n");
                }
            }
        }

        /// <summary>
        /// Method used to change the client's display name when the enter key is
        /// pressed with the name box focused
        /// </summary>
        private void nameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (networkConnection != null)
                {
                    networkConnection.ID = nameBox.Text.Trim();
                    // The server uses Command Name as a marker that the client's name has
                    // been changed
                    networkConnection?.Send("Command Name " + nameBox.Text + "\n");
                    clientLogger.LogInformation($"Changed name to {nameBox.Text}.\n");
                    // Mute the beeping sound when enter is pressed and the client is connected
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        /// <summary>
        /// Method used to handle closing the client by clicking on the red x.
        /// </summary>
        private void ChatClient_FormClosing(Object sender, FormClosingEventArgs e)
        {
            clientLogger.LogInformation($"Client shutting down.\n");
            // The server uses the Command Disconnect message to handle the disconnection
            // on its end
            if (connected && networkConnection != null)
            {
                networkConnection.Send("Command Disconnect\n");
            }
        }

        /// <summary>
        /// Method used to ensure that a client cannot connect to a server 
        /// or send a messaege until a name is enetered in the name text box
        /// </summary>
        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (nameBox.Text == "")
            {
                connectButton.Enabled = false;
                textToSend.Enabled = false;
                textToSend.Enabled = false;
            }
            else
            {
                connectButton.Enabled = true;
                textToSend.Enabled = true;
                textToSend.Enabled = true;
            }
        }
    }
}
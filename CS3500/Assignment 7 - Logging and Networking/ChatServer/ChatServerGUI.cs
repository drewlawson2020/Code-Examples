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
/// This file contains the methods needed to implement a simple chat server used to allow clients to chat with each other.
/// Contains methods for adding and removing chat participants, updating participant list, disconnecting server, and messaging
/// all clients.
/// 
///</summary>

using Communications;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging.Abstractions;

namespace ChatServer
{
    /// <summary>
    /// This class contains the methods needed to implement a simple chat server used to allow multiple participants to
    /// chat with each other. Contains methods to connect new clients, maintain a participant list, message all clients, and
    /// shut down/set up the server. Also contains logging capabilities for server activity.
    /// </summary>
    public partial class ChatServerGUI : Form
    {
        // List of clients currently connected to server
        List<Networking> clientList;
        Networking serverNetwork;
        ILogger<ChatServerGUI> serverLogger;
        bool serverUp;

        /// <summary>
        /// Constructor method for a new ChatServerGUI. The required logger object will
        /// be passed in through dependency injection.
        /// </summary>
        public ChatServerGUI(ILogger<ChatServerGUI> logger)
        {
            InitializeComponent();
            serverLogger = logger;
            clientList = new List<Networking>();
            // Make a new Networking object to listen for new client connections
            serverNetwork = new Networking(serverLogger, ServerConnect, ServerDisconnect, ServerMessage, '\n');
            // Start waiting for clients on a new thread. If this fails, log it
            try
            {
                new Thread(() => serverNetwork.WaitForClients(11000, true)).Start();
            } catch (Exception ex)
            {
                chatLog.AppendText($"Waiting for clients failed with message: {ex.Message}\n");
                serverLogger.LogDebug($"Failed to establish server with message {ex.Message}.\n");
            }
            // Display the server's IP address to give to clients
            serverAddress.Text = GetIPAddress();
            serverUp = true;
            serverLogger.LogInformation("Server initialized\n");
        }

        /// <summary>
        /// Method from https://www.delftstack.com/howto/csharp/get-local-ip-address-in-csharp/ to
        /// obtain the IP address of the server.
        /// </summary>
        /// <returns>A string representation of the IP address</returns>
        private string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            // Loop thorugh the host DNS entry points to find the IP address and
            // return it as a string
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "unidentified";
        }

        /// <summary>
        /// Callback method from Networking used to handle the receipt of a message by the server. Processes the message
        /// as needed and calls helper method to send a message to all clients. Updates the participant list as needed.
        /// </summary>
        /// <param name="channel">The Networking object that received the message</param>
        /// <param name="message">The received message</param>
        private void ServerMessage(Networking channel, string message)
        {
            // A message that starts with Command Name indicates that a client has changes their name
            if (message.StartsWith("Command Name")) {
                string oldName = channel.ID;
                string newName = message.Substring(12).Trim();
                // Change the ID of the receiving channel
                channel.ID = newName;
                serverLogger.LogInformation($"{oldName} changed name to {newName}\n");
                // Let connected clients know about the name change and update participant lists
                MessageAllClients($"{oldName} changed name to {newName}");
                UpdateClientParticipantList();
            } 
            // A message starting with Command Participants means that a client wants an updated participant list
            else if (message.StartsWith("Command Participants"))
            {
                // Copy the list of currently connected participants
                List<Networking> clients = new List<Networking>();
                lock (clientList)
                {
                    foreach (Networking client in clientList)
                    {
                        clients.Add(client);
                    }
                }
                // Build a new string containing the names of the connected clients in a way that
                // can be displayed in the client chat log
                StringBuilder clientString = new StringBuilder();
                for (int i = 0; i < clientList.Count() - 1; i++)
                {
                    clientString.Append(clients[i].ID + " ");
                }
                // Don't add a space after the last name. For display purposes only
                clientString.Append(clients[clientList.Count() - 1].ID);
                channel.Send($"Current participants: {clientString}\n");
                // Let the server know that the list was sent and log it
                Invoke(() => this.chatLog.AppendText($"Sent participants list to {channel.ID}\n"));
                serverLogger.LogInformation($"Sent participants list to {channel.ID}\n");
            } 
            // Command Disconnect means that a client has diconnected
            else if (message.StartsWith("Command Disconnect"))
            {
                string disconnectName = channel.ID;
                // Remove the client from the connection list
                lock (clientList)
                {
                    clientList.Remove(channel);
                }
                Invoke(() => this.chatLog.AppendText($"{disconnectName} disconnected\n"));
                // Update the participant list for the server and log the disconnection and current participants
                Invoke(() => this.participantList.Text = this.participantList.Text.Replace(disconnectName, ""));
                serverLogger.LogInformation($"{disconnectName} disconnected\n");
                serverLogger.LogInformation($"Current participants: {participantList.Text}\n");
                // Use helper methods to updates participant list on client GUI and let all clients know
                // about the disconnection
                UpdateClientParticipantList();
                MessageAllClients($"{disconnectName} disconnected\n");
                // Reset the networking object so that it is no longer connected to the client
                channel = new Networking(NullLogger.Instance, ServerConnect, ServerDisconnect, ServerMessage, '\n');
            }
            // For any other message, send the channel ID with the message to all clients
            else
            {
                MessageAllClients($"{channel.ID} - {message.Trim()}");
            }
        }


        /// <summary>
        /// Callback method from Networking to handle server disconnection. Disconnects all connected clients and messages
        /// them so that they can handle the disconnection on their end.
        /// </summary>
        /// <param name="channel">The networking object that is currently waiting for client connections</param>
        private void ServerDisconnect(Networking channel)
        {
            // Clients expect Server disconnected when the server shuts down. This will prevent them
            // from crashing
            MessageAllClients("Server disconnected");
            // Clear the list of connected clients and stop waiting for new ones
            lock(clientList)
            {
                clientList.Clear();
            }
            channel.StopWaitingForClients();
            serverLogger.LogInformation($"Server disconnected by server. Stopped waiting for clients.\n");
        }

        /// <summary>
        /// Callback method used to handle a new client connecting to the server. Updates the client list
        /// and messages all clients that a new connection has been made
        /// </summary>
        /// <param name="channel">The networking object establishing the connection</param>
        private void ServerConnect(Networking channel)
        {
            // Log a new connection
            serverLogger.LogInformation($"New connection from {channel.ID}\n");
            // Add the new client. Lock the list to prevent race conditions while 
            // it is being changed
            lock (clientList)
            {
                clientList.Add(channel);
            }
            Invoke(() => this.chatLog.AppendText($"Connection from {channel.ID}\n"));
            // Call helper methods to let clients know of the new connection and update
            // participant list
            MessageAllClients($"New chat participant at {channel.ID}");
            UpdateClientParticipantList();
            Invoke(() => this.participantList.AppendText($"{channel.ID}\n)"));
            try
            {
                new Thread(() => channel.ClientAwaitMessagesAsync()).Start();
            }catch (Exception ex)
            {
                Invoke(() => this.chatLog.AppendText($"{ex.Message}"));
                serverLogger.LogDebug($"Failed to await messages from {channel.ID}");
            }
        }

        /// <summary>
        /// Helper method used to update the clients listed on the server GUI and the client GUIs. Used
        /// when a user connects, disconnects, or changes name
        /// </summary>
        private void UpdateClientParticipantList()
        {
            Invoke(() => participantList.Text = "");
            // Copy the client list while it is locked to prevent race conditions. Only lock
            // during copy so that new clients can be added sooner
            List<Networking> clients = new List<Networking>();
            lock (clientList)
            {
                foreach (Networking client in clientList)
                {
                    clients.Add(client);
                }
            }
            // If all clients are disconnected, let the user know and log the information
            if (clients.Count == 0)
            {
                Invoke(() => this.chatLog.AppendText("All clients disconnected.\n"));
                serverLogger.LogInformation("All clients disconnected.\n");
                return;
            }
            // Make a string builder to add all client names to separated by a comma. The clients
            // expect the message to start with Command Participants,. Add each name to the server
            // participant list
            StringBuilder clientString = new StringBuilder("Command Participants,");
            for (int i = 0; i < clients.Count - 1; i++)
            {
                clientString.Append(clients[i].ID + ",");
                Invoke(() => this.participantList.AppendText(clients[i].ID + "\n"));
            }
            // Add the last client name without a comma so that no extra comma prints for
            // the clients GUI
            clientString.Append(clients[clients.Count - 1].ID);
            Invoke(() => this.participantList.AppendText(clientList[clients.Count-1].ID));
            serverLogger.LogInformation($"Current participants: {participantList.Text}\n");
            serverLogger.LogInformation($"Sent participants list to {clients.Count} clients.\n");
            MessageAllClients(clientString.ToString());
        }

        /// <summary>
        /// Helper method used to send a message from the server to all currently connected clients.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        private void MessageAllClients(string message)
        {
            // Keep track of successful sends for logging purposes
            int successfulSends = 0;
            // Copy the client list while it is locked to prevent race conditions. Only lock
            // during copy so that new clients can be added sooner
            List<Networking> clients = new List<Networking>();
            lock (clientList)
            {
                foreach (Networking client in clientList)
                {
                    clients.Add(client);
                }
            }
            // Try to send a message to all connected clients. Log and let the user know if any
            // attempts to send fail
            foreach (Networking client in clients)
            {
                try
                {
                    client.Send(message + "\n");
                    successfulSends++;
                }
                catch
                {
                    serverLogger.LogDebug($"Failed to send message to {client.ID}");
                    Invoke(() => this.chatLog.AppendText($"Failed to send message to {client.ID}"));
                }
            }
            // Log the sent message and add a line to the chatlog
            serverLogger.LogInformation($"Sent {message} to {successfulSends} participants. Total bytes sent: {new UTF8Encoding().GetBytes(message).Length}\n");
            Invoke(() => this.chatLog.AppendText($"{message}\n"));
        }

        /// <summary>
        /// Used to send a message to all connected clients (if there are any) when enter is pressed
        /// when the message box is selected.
        /// </summary>
        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (clientList.Count > 0)
                {
                    // Call a helper method to send the message to all currently connected clients
                    MessageAllClients($"Server says: {messageBox.Text}");
                    // Mute the beeping noise when the message sends correctly
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                } 
                // If there are no connected clients, let the user know
                else
                {
                    chatLog.AppendText("No one's listening.\n");
                }
            }
        }

        /// <summary>
        /// Method used to handle clicking the server establish and shutdown button. Will either disconnect all clients and
        /// stop waiting for new ones or allow for new connections depending on the current state of the server.
        /// </summary>
        private void connectButton_Click(object sender, EventArgs e)
        {
            // Disconnect all clients and stop waiting for new ones if the server is currently up
            if (serverUp)
            {
                // Call a helper method to let all clients know that the server was disconnected. Also
                // clears the list of connected clients
                ServerDisconnect(serverNetwork);
                serverUp = false;
                connectButton.Text = "Start Server";
                Invoke(() => { this.participantList.Text = ""; });
            } else
            {
                try
                {
                    serverUp = true;
                    connectButton.Text = "Shutdown Server";
                    // Try to start waiting for new clients and establish new connections
                    serverNetwork = new Networking(NullLogger.Instance, ServerConnect, ServerDisconnect, ServerMessage, '\n');
                    new Thread(() => serverNetwork.WaitForClients(11000, true)).Start();
                    Invoke(() => { this.chatLog.AppendText($"Re-established server. Waiting for clients.\n"); });
                    serverLogger.LogInformation($"Re-established server. Waiting for clients.\n");
                } 
                // Catch any exceptions during server establishment, let user know and log them
                catch (Exception ex)
                {
                    Invoke(() => { this.chatLog.AppendText($"Establishing the server failed with message: {ex.ToString()}\n"); });
                    serverLogger.LogDebug($"Failed to re-establish server.");
                }
            }
        }

        /// <summary>
        /// Method used to handle closing the server by pressing the red x.
        /// </summary>
        private void ChatServer_FormClosing(Object sender, FormClosingEventArgs e)
        {
            serverLogger.LogInformation($"Closing server.");
            // If there are clients connected, send them Server disconnected so that
            // they can handle it on their end
            if (clientList.Count > 0)
            {
                MessageAllClients("Server disconnected");
            }
        }
    }
}
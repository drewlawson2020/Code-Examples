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
/// This file contains methods used to establish a network connection between a client and server program. Contains methods to
/// initialize the connection, wait for messages, send messages, wait for client connections, stop waiting for client connections,
/// and handle received messages. Several delegates are defined to allow classes that utilize this one to implement their own behavior
/// for several methods.
/// 
///</summary>

using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Communications
{
    /// <summary>
    /// This class contains methods used to establish a network connection between a client and server program. Contains methods to
    /// initialize the connection, wait for messages, send messages, wait for client connections, stop waiting for client connections,
    /// and handle received messages. Several delegates are defined to allow classes that utilize this one to implement their own behavior
    /// for several methods. Uses TCP connection protocol.
    /// </summary>
    public class Networking
    {
        // The character that will mark the end of the message
        char terminator;
        private TcpClient tcpClient;
        private ILogger fileLogger;
        // Token to allow the server to stopp accepting clients
        private CancellationTokenSource cancellationToken;
        /// The following three are delegate method declarations
        ReportMessageArrived messageArrived;
        ReportConnectionEstablished connectionEstablished;
        ReportDisconnect reportDisconnect;

        /// <summary>
        /// Constructor method for a new Networking object
        /// </summary>
        /// <param name="logger">A logger associated with the utilizing class</param>
        /// <param name="onConnect">Callback method for established connections</param>
        /// <param name="onDisconnect">Callback method for disconnection process</param>
        /// <param name="onMessage">Callback method for handling received messages</param>
        /// <param name="terminationCharacter">The character that marks the end of messages</param>
        public Networking(ILogger logger,
                ReportConnectionEstablished onConnect, ReportDisconnect onDisconnect, ReportMessageArrived onMessage,
                char terminationCharacter)
        {

            fileLogger = logger;
            connectionEstablished = onConnect;
            messageArrived = onMessage;
            reportDisconnect = onDisconnect;
            tcpClient = new TcpClient();
            terminator = terminationCharacter;
            cancellationToken = new CancellationTokenSource();
            // Set the object's ID to the remote endpoint of the TCP client by default
            ID = $"{tcpClient.Client.RemoteEndPoint}";
        }

        /// <summary>
        /// Delegate method for the process to follow when a message is received by a Networking object.
        /// </summary>
        /// <param name="channel">The Networking object receiving the message</param>
        /// <param name="message">The message</param>
        public delegate void ReportMessageArrived(Networking channel, string message);

        /// <summary>
        /// Delegate method for the process to follow when a client disconnects from a server
        /// </summary>
        /// <param name="channel">The Networking object that defines the client-server connection</param>
        public delegate void ReportDisconnect(Networking channel);

        /// <summary>
        /// Delegate method for the process to follow when a connection is established between a client
        /// and server
        /// </summary>
        /// <param name="channel">The Networking object that defines the client-server connection</param>
        public delegate void ReportConnectionEstablished(Networking channel);

        /// <summary>
        /// The ID associated with this networking object. Lets the server see which client is communicating with it.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Established the connection between a client and a server at a specified IP address and port. 
        /// The connection will use TCP protocol. Called by clients only. This implementation is modified
        /// slightly from the provided chat client code.
        /// </summary>
        /// <param name="host">The server's address</param>
        /// <param name="port">The port on which the server is listening</param>
        public void Connect(string host, int port)
        {
            tcpClient = new TcpClient(host, port);
            // Set the ID to the remote end point by default and use callback method to
            // process the connection on the client's end.
            if (tcpClient.Connected)
            {
                ID = $"{tcpClient.Client.RemoteEndPoint}";
                connectionEstablished(this);
            } 
            // If the connection fails, throw an exception indicating as such
            else
            {
                throw new Exception("Network connection failed to establish");
            }
        }

        /// <summary>
        /// Method used by clients to wait for messages coming from the server. Waits infinitely by default. The
        /// implementation here is directly from the provided chat client example code.
        /// </summary>
        /// <param name="infinite">Whether the client will wait infinitely for messages. True by default</param>
        public async void ClientAwaitMessagesAsync(bool infinite = true)
        {
            try
            {
                // Make a new string builder to append received data to
                StringBuilder dataBacklog = new StringBuilder();
                // Make a byte array to hold received data from the TCP stream
                byte[] buffer = new byte[4096];
                NetworkStream stream = tcpClient.GetStream();
                if (stream == null)
                {
                    return;
                }
                while (infinite)
                {
                    // Get data out of the TCP stream and interpret it via UTF-8 encoding
                    int total = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string current_data = Encoding.UTF8.GetString(buffer, 0, total);
                    // Add the data to the stringbuilder and call a helper to see if it
                    // contains at least one message
                    dataBacklog.Append(current_data);
                    CheckForMessage(dataBacklog);
                }
            } catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// Helper method to see if a passed in string builder contains one or more messages. This implementation
        /// is based heavily on the example provided in the example chat client code.
        /// </summary>
        /// <param name="dataBacklog">The string builder to be checked for messages</param>
        private void CheckForMessage(StringBuilder dataBacklog)
        {
            string allData = dataBacklog.ToString();
            int terminatorPosition = allData.IndexOf(terminator);
            bool messageFound = false;
            // Loop over the string of data checking for the termination character
            while (terminatorPosition >= 0)
            {
                messageFound = true;
                // Extract the message from the string and remove the found
                // message from the string builder
                string message = allData.Substring(0, terminatorPosition + 1);
                dataBacklog.Remove(0, terminatorPosition + 1);
                // Use a callback method to handle the received message in the client or server
                messageArrived(this, message);
                allData = dataBacklog.ToString();
                terminatorPosition = allData.IndexOf(terminator);
            }
            // Do nothing if no messages found
            if (!messageFound)
            {
                return;
            }
        }

        /// <summary>
        /// Used by servers to wait for clients to connect at the specified port. Waits infinitely by default.
        /// This implementation is modified slightly from the provided chat server example code.
        /// </summary>
        /// <param name="port">The port to which clients will connect</param>
        /// <param name="infinite">Whether the server waits infinitely for clients. True by default</param>
        public async void WaitForClients(int port, bool infinite)
        {
            // Make a new TCP listener to wait for TCP client connections
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            while (infinite)
            {
                try
                {
                    // Wait for and accept TCP clients until the cancellation token is activated
                    listener.Start();
                    tcpClient = await listener.AcceptTcpClientAsync(cancellationToken.Token);
                    // Make a new networking object that is a copy of this one
                    Networking acceptedConnection = (Networking)this.MemberwiseClone();
                    // Set the new connections ID to its remote end point by default
                    acceptedConnection.ID = $"{tcpClient.Client.RemoteEndPoint}";
                    if (tcpClient != null)
                    {
                        // Send the client a message that indicates a connection has been made
                        acceptedConnection.Send($"Connected to server at: {tcpClient.Client.LocalEndPoint}\n");
                        // Use the callback method to handle the new connection on the server's end
                        connectionEstablished(acceptedConnection);
                    }
                }
                // Stop the listener if the cancellation token has been activated (results in an exception in the try
                // block above)
                catch (Exception ex)
                {
                    listener.Stop();
                }
            }
        }

        /// <summary>
        /// Used by server to stop waiting for new connections. Activates the
        /// cancellation token in WaitForClients.
        /// </summary>
        public void StopWaitingForClients()
        {
            cancellationToken.Cancel();
        }

        /// <summary>
        /// Method used to handle disconnection between client and server. Uses callback
        /// method to andle the disconnection as specified by the client or server calling
        /// the method.
        /// </summary>
        public void Disconnect()
        {
            reportDisconnect(this);
            // Stop the TCP data stream
            tcpClient.Close();
            
        }

        /// <summary>
        /// Used to send a message between clients and servers. This implementation
        /// is directly from the provided chat client example code.
        /// </summary>
        /// <param name="text">The message to send</param>
        public async void Send(string text)
        {
            // Get the byte encoding of the message and send it through
            // the TCP client connection
            byte[] messageBytes = Encoding.UTF8.GetBytes(text);
            await tcpClient.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
        }

    }
}
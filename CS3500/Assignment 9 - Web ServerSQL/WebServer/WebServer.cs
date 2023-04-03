/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: Brent-Spiner-Base
/// Date: April 27, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// This file contains the implementation of a simple html web server. The server is used to display information about
/// Agario games in a web browser. Contains code to send tables of player stats and create a graph of player mass by game.
/// The server can also be used to insert new data into a database using a scors/[name]/[rank]/[start_time]/[end_time] command
/// in the browser.
/// 
///</summary>

using Communications;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.RegularExpressions;

namespace WebServer
{
    /// <summary>
    /// This class contains the implementation of a simple html web server. The server is used to display information about
    /// Agario games in a web browser. Contains code to send tables of player stats and create a graph of player mass by game.
    /// The server can also be used to insert new data into a database using a scores/[name]/[rank]/[start_time]/[end_time] command
    /// in the browser.
    /// </summary>
    internal class WebServer
    {
        private static Networking? serverConnection;
        private static int userCounter;
        private static Regex returnPlayerScores;
        private static Regex insertPlayerScores;
        private static Regex playerGraph;

        /// <summary>
        /// Constructor for a web server. Sets the user count to 0 and creates regular expressions needed
        /// to match specified GET commands.
        /// </summary>
        static WebServer()
        {
            userCounter = 0;
            // Matches a scores/[playerName] input
            returnPlayerScores = new Regex(@"(?<=\/scores\/)[\w\d\s]+(?= HTTP)");
            // Matches a graph/[playerName] input
            playerGraph = new Regex(@"(?<=\/graph)[\w\d\s]+(?= HTTP)");
            // Matches a scores/[name]/[rank]/[start_time]/[end_time] input
            insertPlayerScores = new Regex(@"(?<=\/scores\/)[\w\d\s]+\/\d+\/\d+\/\d+\/\d+(?= HTTP)");
        }

        /// <summary>
        /// Starts the server by establishing a network to listen for clients.
        /// </summary>
        internal static void Main(string[] vs)
        {
            serverConnection = new Networking(NullLogger.Instance, OnClientConnect, OnClientDisconnect, OnMessageReceived, '\n');
            try
            {
                serverConnection.WaitForClients(11001, true);
                Console.WriteLine("Waiting for clients");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to wait for clients with following message: {ex.ToString()}");
                Console.Read();
            }

        }

        /// <summary>
        /// Callback method for the Networking used to handle receiving a message over the server's network.
        /// Interprets the message and sends back an html page as required by the message.
        /// </summary>
        /// <param name="channel">The channel that received the message</param>
        /// <param name="message">The received message</param>
        private static void OnMessageReceived(Networking channel, string message)
        {
            // The following block sends a homepage containing links to data pages
            if (message.StartsWith("GET / HTTP/1.1") || message.StartsWith("GET /index HTTP/1.1") || message.StartsWith("GET /index.html HTTP/1.1"))
            {
                SendHeaderAndStyle(channel);
                channel.Send("<html>\n<body>");
                userCounter++;
                channel.Send($"<h1>Hello! You are user number {userCounter}!</h1>");
                channel.Send($"<a href =/highscores>High Scores</a><br>");
                channel.Send($"<a href =/gametimes>Player Game Times</a><br>");
                channel.Send($"<a href =/serverassigned>Server Assigned Attributes</a><br>");
                channel.Send($"<a href =/PlayerDeathStats>Player Death Stats </a><br>");
                channel.Send("</body>\n</html>");
            }
            // The following block sends a table that contains information about the player's server assigned color and ID
            else if (message.StartsWith("GET /serverassigned"))
            {
                SendHeaderAndStyle(channel);
                SendTableHead(channel);
                channel.Send("<th>Game ID</th>");
                channel.Send("<th>Player Name</th>");
                channel.Send("<th>Player ID</th>");
                channel.Send("<th>Player ARGB Color</th>");
                EndTableHead(channel);
                // Get the attribute data from the database server and send it
                channel.Send(DatabaseInterface.BuildServerAttributeTable());
                EndTablePage(channel);
            }
            // The following block sends a page containing a tables of player start, stop, and elapsed time information
            else if (message.StartsWith("GET /gametimes"))
            {
                SendHeaderAndStyle(channel);
                SendTableHead(channel);
                channel.Send("<th>Game ID</th>");
                channel.Send("<th>Player Name</th>");
                channel.Send("<th>Start Time (millis)</th>");
                channel.Send("<th>End time (millis)</th>");
                channel.Send("<th>Game length (ms)</th>");
                EndTableHead(channel);
                // Get the time data from the database server and send it
                channel.Send(DatabaseInterface.BuildTimeTable());
                EndTablePage(channel);
            }
            // The following block sends a page containing a table of high scores (mass, rank, and life) for
            // players in the database
            else if (message.StartsWith("GET /highscores"))
            {
                SendHeaderAndStyle(channel);
                SendTableHead(channel);
                channel.Send("<th>Player Name</th>");
                channel.Send("<th>Highest Mass</th>");
                channel.Send("<th>Highest Rank</th>");
                channel.Send("<th>Longest Life (ms)</th>");
                EndTableHead(channel);
                // Get the high score data from the database server and send it
                channel.Send(DatabaseInterface.BuildHighScoresTable());
                EndTablePage(channel);
            }
            // The following block matches requests for game scores for an individual player
            // and send a table containing scores for each game the player played
            else if (returnPlayerScores.IsMatch(message))
            {
                // Extract the player name from input
                string playerName = returnPlayerScores.Match(message).Groups[0].Value;
                SendHeaderAndStyle(channel);
                channel.Send($"<h1>Game scores for {playerName}</h1>");
                SendTableHead(channel);
                channel.Send("<th>Game ID</th>");
                channel.Send("<th>Highest Mass</th>");
                channel.Send("<th>Highest Rank</th>");
                channel.Send("<th>Length of Life (ms)</th>");
                channel.Send("<th>Time to Number One (ms)</th>");
                EndTableHead(channel);
                // Get the player's game score data from the database server and send it
                channel.Send(DatabaseInterface.BuildPlayerScoresTable(playerName));
                EndTablePage(channel);
                channel.Send($"<a href = http://localhost:11001/graph{playerName}>Graph player mass</a><br>");
            }
            // The following block matches a request to add player score data to the database.
            else if (insertPlayerScores.IsMatch(message))
            {
                // Get the string containing player data and split it into an array
                string playerScoreString = insertPlayerScores.Match(message).Groups[0].Value;
                string[] playerInfo = playerScoreString.Split('/');
                SendHeaderAndStyle(channel);
                try
                {
                    // Access the database server to add data to tables
                    DatabaseInterface.AddPlayerScores(playerInfo);
                    channel.Send($"<h1>Updated scores for {playerInfo[0]}</h1>");
                }
                // Send a page stating an error occured if something goes wrong
                catch
                {
                    channel.Send("<html>\n<body>");
                    channel.Send($"<h1>Error adding values for {playerInfo[0]}</h1>");
                    channel.Send("</body>\n</html>");
                }
                channel.Send($"<a href = http://localhost:11001>Return to home</a><br>");
            }
            // The following block matches a request to create tables in the database server
            else if (message.StartsWith("GET /create"))
            {
                SendHeaderAndStyle(channel);
                try
                {
                    // Create database tables and seed them with starter data
                    DatabaseInterface.CreateAndSeedTables();
                    channel.Send("<html>\n<body>");
                    channel.Send($"<h1>Tables created and values seeded</h1>");
                    channel.Send($"<a href = http://localhost:11001>Return to home</a><br>");
                    channel.Send("</body>\n</html>");
                } 
                // Send a page stating an error occured if something goes wrong
                catch {
                    channel.Send("<html>\n<body>");
                    channel.Send($"<h1>Error creating tables and seeding values</h1>");
                    channel.Send($"<a href = http://localhost:11001>Return to home</a><br>");
                    channel.Send("</body>\n</html>");
                }
            }
            // The following block matches a request for information about the player when the 
            // game ended
            else if (message.StartsWith("GET /PlayerDeathStats"))
            {
                SendHeaderAndStyle(channel);
                SendTableHead(channel);
                channel.Send("<th>Game ID</th>");
                channel.Send("<th>Player Name</th>");
                channel.Send("<th>Death X</th>");
                channel.Send("<th>Death Y</th>");
                channel.Send("<th>Death Mass</th>");
                EndTableHead(channel);
                // Get the payer death/game end data from the database server and send it
                channel.Send(DatabaseInterface.BuildPlayerDeathStatsTable());
                EndTablePage(channel);
            }
            // The following block is used to build a JavaScript graph of player mass data by game
            else if (playerGraph.IsMatch(message))
            {
                // Extract the player name from input
                string playerName = playerGraph.Match(message).Groups[0].Value;
                SendHeaderAndStyle(channel);
                try
                {
                    channel.Send($"<h1>Graph for {playerName} mass</h1>");
                    // Set chart area and script source for building it. This is from the lecture slides
                    channel.Send("<div>");
                    channel.Send("<canvas id='mass chart'></canvas>");
                    channel.Send("</div>");
                    channel.Send(@$"<script src='https://cdn.jsdelivr.net/npm/chart.js@2.8.0'> </script>");
                    // Get the mass data by game and use it to build the string representing the chart
                    channel.Send(DatabaseInterface.GraphPlayerMass(playerName));
                    channel.Send($"<a href = http://localhost:11001>Return to home</a><br>");
                }
                // Send a page stating an error occured if something goes wrong
                catch (Exception ex)
                {
                    channel.Send("<html>\n<body>");
                    channel.Send($"<h1>Error graphing mass for {playerName}</h1>");
                    channel.Send($"<a href = http://localhost:11001>Return to home</a><br>");
                    channel.Send("</body>\n</html>");
                }
            }
            // The following chunk sends the style information in the server's css file
            else if (message.StartsWith("GET /css/styles"))
            {
                SendHeaderAndStyle(channel);
                channel.Send(GetCSSContentsToShowInBrowser());
            }
            // The following block returns a '404 error' page if any unhandled request is received
            else
            {
                SendHeaderAndStyle(channel);
                channel.Send("<html>\n<body>");
                channel.Send($"<h1>404 Page Not Found</h1>");
                channel.Send($"<a href = http://localhost:11001>Return to home</a><br>");
                channel.Send("</body>\n</html>");
            }
            channel.Disconnect();

        }

        /// <summary>
        /// Helper method to send the tags for the end of a table and the end of the page
        /// with a link to the home page.
        /// </summary>
        /// <param name="channel">The channel to send the html message over</param>
        private static void EndTablePage(Networking channel)
        {
            channel.Send("</tbody>");
            channel.Send("</table>\n");
            channel.Send($"<a href = http://localhost:11001>Return to home</a><br>");
        }

        /// <summary>
        /// Helper method used to send the end of a table header tags
        /// </summary>
        /// <param name="channel">The channel to send the html message over</param>
        private static void EndTableHead(Networking channel)
        {
            channel.Send("</tr>\n");
            channel.Send("</thead>\n");
            channel.Send("<tbody>");
        }

        /// <summary>
        /// Helper method used to send tags for the start of a table header
        /// </summary>
        /// <param name="channel">The channel to send the html message over</param>
        private static void SendTableHead(Networking channel)
        {
            channel.Send("<table>\n");
            channel.Send("<thead>\n");
            channel.Send("<tr>\n");
        }

        /// <summary>
        /// Helper method used to send the header of an html page and the css style. Called whenever
        /// a new page is loaded.
        /// </summary>
        /// <param name="channel">The channel to send the html message over</param>
        private static void SendHeaderAndStyle(Networking channel)
        {
            channel.Send("HTTP / 1.1 200 OK");
            channel.Send("Connection.Close");
            channel.Send("Content-Type: text/html; charset=UTF-8");
            channel.Send("");
            channel.Send(SendCSS());
        }

        /// <summary>
        /// Helper method used to extract css style from file then append data to
        /// string to be sent for use by browser to set page display style
        /// </summary>
        /// <returns>A css style string to set browser display</returns>
        private static string SendCSS()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<head>\n<style>\n");
            stringBuilder.Append(GetCSSContents());
            stringBuilder.Append("</head>\n</style>\n");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Helper method used to return the contents of the css file for use in setting browser display
        /// </summary>
        /// <returns>The contents of the css file for browser display</returns>
        private static string GetCSSContents()
        {
            string cssContents = System.IO.File.ReadAllText("../../../style.css");
            return cssContents;
        }

        /// <summary>
        /// Helper method used to extract the contents of a css file and format them. This allows the actual contents
        /// to be shown in a web browser when requested.
        /// </summary>
        /// <returns>An html formatted string containing the contents of the css file so that they are readable</returns>
        private static string GetCSSContentsToShowInBrowser()
        {
            StringBuilder cssContents = new StringBuilder();
            // Access the css file in the top-level project folder
            string[] cssLines = File.ReadAllLines("../../../style.css");
            foreach (string line in cssLines)
            {
                // Add a <p> tag to break lines up into paragraphs for
                // html
                cssContents.Append($"<p>{line}</p>");
            }
            return cssContents.ToString();
        }

        /// <summary>
        /// Callback method to handle a client disconnecting from the server.
        /// </summary>
        /// <param name="channel">The channel that was communicating with the browser</param>
        private static void OnClientDisconnect(Networking channel)
        {
            Console.WriteLine($"Client at {channel.ID} disconnected");
        }

        /// <summary>
        /// Callback method to handle a client connecting to the server
        /// </summary>
        /// <param name="channel">The channel that established the connection</param>
        private static void OnClientConnect(Networking channel)
        {
            Console.WriteLine($"Sent page to {channel.ID}");
            return;
        }
    }
}

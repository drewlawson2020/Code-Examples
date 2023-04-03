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
/// This file contains code needed to connect to a database server, query it with SQL, and return the requested data
/// in html string format. It also allows for the insertion of new data into the database tables. Other methods allow 
/// for the creation of a predetermined set of tables in the database (if they do not already exist) and the seeding 
/// of these tables with data.
/// 
///</summary>

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace WebServer
{
    /// <summary>
    /// This class contains code needed to connect to a database server, query it with SQL, and return the requested data
    /// in html string format. It also allows for the insertion of new data into the database tables. Other methods allow 
    /// for the creation of a predetermined set of tables in the database (if they do not already exist) and the seeding 
    /// of these tables with data.
    /// </summary>
    internal class DatabaseInterface
    {
        private static readonly string connectionString;

        /// <summary>
        /// Constructor for the class. Accesses the secrets file to build the string that will be used to connect to the
        /// database server.
        /// </summary>
        static DatabaseInterface()
        {
            // Build a new configuration that includes the secrets file
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<WebServer>();
            IConfigurationRoot sqlConfiguration = builder.Build();
            // Build the SQL connection string with information from the secrets file
            connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = sqlConfiguration["ServerURL"],
                InitialCatalog = sqlConfiguration["DBName"],
                UserID = sqlConfiguration["UserName"],
                Password = sqlConfiguration["DBPassword"]
            }.ConnectionString;
        }

        /// <summary>
        /// Method used to extract player high score data from the database. The returned string is used to build an
        /// html table containing the highest mass, rank, and longest life that each player in the database has achieved.
        /// </summary>
        /// <returns>An html string for building a table containing high score data</returns>
        internal static string BuildHighScoresTable()
        {
            StringBuilder highScoreBuilder = new StringBuilder();
            string selectHighScores = $@"SELECT MaximumPlayerMass.PlayerName as PlayerName, MAX(MaximumMass) MaxMass, MIN(MaximumRank) MaxRank, MAX(TimeToDeath) MaxLifeTime
FROM MaximumPlayerMass
JOIN MaximumPlayerRank
ON MaximumPlayerMass.GameID = MaximumPlayerRank.GameID
JOIN TimeToPlayerDeath
ON MaximumPlayerMass.GameID = TimeToPlayerDeath.GameID
GROUP BY MaximumPlayerMass.PlayerName";
            try
            {
                // Connect to the database and use the above query to select high score data for each player
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(selectHighScores, con))
                    {
                        // Read the data returned by the query and insert it into a string builder to build the html script
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                highScoreBuilder.Append("<tr>\n");
                                highScoreBuilder.Append($"<td><a href = /scores/{reader["PlayerName"]}>{reader["PlayerName"]}</a></td><td>{reader["MaxMass"]}</td>\n<td>{reader["MaxRank"]}</td>" +
                                    $"\n<td>{reader["MaxLifeTime"]}</td>\n");
                            }
                        }
                    }
                    highScoreBuilder.Append("</tr>\n");
                }
                return highScoreBuilder.ToString();
            }
            // Return an error message page if something goes wrong
            catch (Exception ex)
            {
                return ("HTTP / 1.1 200 OK\nConnection.Close\nContent-Type: text/html; charset=UTF-8\n\n<h1>Page Not found</h1>\n<a href =localhost:11001>Home </a>\n</body>\n</html>");
            }
        }

        /// <summary>
        /// This method creates tables for maximum mass, maximum rank, player endgame info, game time info, time for player to reach rank number one, Agario server assigned
        /// attributes, and total game length. A helper method is called to seed these tables with data. Note: this method will throw an exception if any of these tables
        /// already exist in the database.
        /// </summary>
        /// <exception cref="Exception">Throws an exception if any of the tables already exist in the database</exception>
        internal static void CreateAndSeedTables()
        {
            string createMaxMassTable = $@"
CREATE TABLE MaximumPlayerMass(
	GameID int IDENTITY(1,1) NOT NULL,
	PlayerName nvarchar(max) NOT NULL,
	MaximumMass float NOT NULL
)";
            string createMaxRankTable = $@"
CREATE TABLE MaximumPlayerRank(
	GameID int IDENTITY(1,1) NOT NULL,
	PlayerName nvarchar(max) NOT NULL,
	MaximumRank int NOT NULL
)";
            string createDeathStatsTable = $@"
CREATE TABLE PlayerDeathStats(
	GameID int IDENTITY(1,1) NOT NULL,
	PlayerName nvarchar(max) NOT NULL,
	PlayerPositionX float NOT NULL,
	PlayerPositionY float NOT NULL,
	PlayerMass float NOT NULL
)";
            string createGameTimeTable = @$"
CREATE TABLE PlayerGameTime(
	GameID int IDENTITY(1,1) NOT NULL,
	PlayerName nvarchar(max) NOT NULL,
	StartMillis float NOT NULL,
	EndMillis float NOT NULL
)";
            string createTimeToNumberOneTable = $@"
CREATE TABLE PlayerTimeToNumberOne(
	GameID int IDENTITY(1,1) NOT NULL,
	PlayerName nvarchar(max) NOT NULL,
	TimeToNumberOne int NOT NULL
)";
            string createServerAssignedAttributesTable = $@"
CREATE TABLE ServerAssignedAttributes(
	GameID int IDENTITY(1,1) NOT NULL,
	PlayerName nvarchar(max) NOT NULL,
	PlayerID int NOT NULL,
	PlayerARGBColor int NOT NULL
)";
            string createTimeToDeathTable = $@"
CREATE TABLE TimeToPlayerDeath(
	GameID int IDENTITY(1,1) NOT NULL,
	PlayerName nvarchar(max) NOT NULL,
	TimeToDeath float NOT NULL
)";
            try
            {
                // Connect to the database server
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Each of the following executes a command to build a new table in the database
                    using (SqlCommand maxMassCommand = new SqlCommand(createMaxMassTable, con))
                    {
                        maxMassCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand maxRankCommand = new SqlCommand(createMaxRankTable, con))
                    {
                        maxRankCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand deathStatsCommand = new SqlCommand(createDeathStatsTable, con))
                    {
                        deathStatsCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand gameTimeCommand = new SqlCommand(createGameTimeTable, con))
                    {
                        gameTimeCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand timeToNumberOneCommand = new SqlCommand(createTimeToNumberOneTable, con))
                    {
                        timeToNumberOneCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand serverAssignedAttrCommand = new SqlCommand(createServerAssignedAttributesTable, con))
                    {
                        serverAssignedAttrCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand timeToDeathCommand = new SqlCommand(createTimeToDeathTable, con))
                    {
                        timeToDeathCommand.ExecuteNonQuery();
                    }
                }
            }
            // Throw an exception if any of the tables already exist or something else goes wrong
            catch (Exception ex)
            {
                throw new Exception();
            }
            AddValuesToTables();
        }

        /// <summary>
        /// This method is used to query the server database for information on player start time (in millis), end time (in millis), and total
        /// game time (in ms). Parses this information to build an html string that is used to construct a table containing the player's time
        /// data.
        /// </summary>
        /// <returns>An html string of player time data</returns>
        internal static string BuildTimeTable()
        {
            StringBuilder timeDataBuilder = new StringBuilder();
            string getGameTimes = $@"SELECT PlayerGameTime.GameID, PlayerGameTime.PlayerName, StartMillis, EndMillis, TimeToDeath
FROM PlayerGameTime
JOIN TimeToPlayerDeath
ON PlayerGameTime.GameID = TimeToPlayerDeath.GameID";
            try
            {
                // Connect to database server and query for time data
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(getGameTimes, con))
                    {
                        // Read the returned data and insert it into an html string as required
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                timeDataBuilder.Append("<tr>\n");
                                timeDataBuilder.Append($"<td>{reader["GameID"]}</td>\n<td><a href = /scores/{reader["PlayerName"]}>{reader["PlayerName"]}</a></td><td>{reader["StartMillis"]}</td>\n" +
                                    $"<td>{reader["EndMillis"]}</td><td>{reader["TimeToDeath"]}</td>\n\n");
                            }
                        }
                    }
                    timeDataBuilder.Append("</tr>\n");
                }
                return timeDataBuilder.ToString();

            }
            // Return an error page if anything goes wrong extracting time data
            catch (Exception ex)
            {
                return ("HTTP / 1.1 200 OK\nConnection.Close\nContent-Type: text/html; charset=UTF-8\n\n<h1>Error building table</h1>\n<a href =localhost:11001>Home </a>\n</body>\n</html>");
            }
        }

        /// <summary>
        /// This method is used to query the server database for information on player ID and color assigned by the server. This data is used
        /// to construct an html table containing this info.
        /// </summary>
        /// <returns>An html string of player server assigned attributes</returns>
        internal static string BuildServerAttributeTable()
        {
            StringBuilder stringBuilder = new StringBuilder();
            // Every column in the queried table is included in the html table
            string getServerAttributes = $@"SELECT * FROM ServerAssignedAttributes";
            try
            {
                // Connect to the database server and query the color and ID data from the Agario server
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(getServerAttributes, con))
                    {
                        // Parse the data and add to the html string
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                stringBuilder.Append("<tr>\n");
                                stringBuilder.Append($"<td>{reader["GameID"]}</td>\n<td><a href = /scores/{reader["PlayerName"]}>{reader["PlayerName"]}</a></td><td>{reader["PlayerID"]}</td>\n" +
                                    $"<td>{reader["PlayerARGBColor"]}</td>\n");
                            }
                        }
                    }
                    stringBuilder.Append("</tr>\n");
                }
                return stringBuilder.ToString();

            }
            // Return an error page if anything goes wrong accessing or reading data
            catch (Exception ex)
            {
                return ("HTTP / 1.1 200 OK\nConnection.Close\nContent-Type: text/html; charset=UTF-8\n\n<h1>Error building table</h1>\n<a href =localhost:11001>Home </a>\n</body>\n</html>");
            }
        }

        /// <summary>
        /// Helper method used to seed newly created tables with 'dummy' data.
        /// </summary>
        /// <exception cref="Exception">Throws an exception if any data fails to be inserted into the table</exception>
        private static void AddValuesToTables()
        {
            try
            {
                // Connect to the database server
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Each of the following adds new data to the specified tables
                    using (SqlCommand maxMassCommand = new SqlCommand(@$"INSERT INTO MaximumPlayerMass VALUES ('Jeff',11000),('Jeff',12000),('Jess',12500),('Jess',13000)", con))
                    {
                        maxMassCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand maxRankCommand = new SqlCommand(@$"INSERT INTO MaximumPlayerRank VALUES ('Jeff',11),('Jeff',2),('Jess',1),('Jess',3)", con))
                    {
                        maxRankCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand playerDeathCommand = new SqlCommand(@$"INSERT INTO PlayerDeathStats VALUES ('Jeff',11,1000,2000),('Jeff',2,0,250),('Jess',1,123,1234),('Jess',3,5,678)", con))
                    {
                        playerDeathCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand gameTimeCommand = new SqlCommand(@$"INSERT INTO PlayerGameTime VALUES ('Jeff',11000,12000),('Jeff',11000,12000),('Jess',11000,12000),('Jess',11000,12000)", con))
                    {
                        gameTimeCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand timeToNumberOneCommand = new SqlCommand(@$"INSERT INTO PlayerTimeToNumberOne VALUES ('Jeff',11000),('Jeff',12000),('Jess',12500),('Jess',13000)", con))
                    {
                        timeToNumberOneCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand serverAssignedCommand = new SqlCommand(@$"INSERT INTO ServerAssignedAttributes VALUES ('Jeff',1,-100),('Jeff',1,-100),('Jess',23,1000),('Jess',23,1000)", con))
                    {
                        serverAssignedCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand timeToDeathCommand = new SqlCommand(@$"INSERT INTO TimeToPlayerDeath VALUES ('Jeff',123),('Jeff',1600),('Jess',10000),('Jess',125000)", con))
                    {
                        timeToDeathCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine($"Error inserting data:\n   - {exception.Message}");
                throw new Exception();
            }
        }

        /// <summary>
        /// Method used to build an html table containg player score information for each game the player has played on the
        /// Agario server. Information included is game ID, maximum mass, maximum rank, time to player death, and time it took
        /// player to reach number one in rank.
        /// </summary>
        /// <param name="playerName">The player whose score data will be queried</param>
        /// <returns>An html string to build a table of player data</returns>
        internal static string BuildPlayerScoresTable(string playerName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string selectHighScores = $@"SELECT MaximumPlayerMass.GameID as GameID, MaximumMass, MaximumRank, TimeToDeath, TimeToNumberOne
FROM MaximumPlayerMass
JOIN MaximumPlayerRank
ON MaximumPlayerMass.GameID = MaximumPlayerRank.GameID
JOIN TimeToPlayerDeath
ON MaximumPlayerMass.GameID = TimeToPlayerDeath.GameID
JOIN PlayerTimeToNumberOne
ON MaximumPlayerMass.GameID = PlayerTimeToNumberOne.GameID
WHERE MaximumPlayerMass.PlayerName = '{playerName}'";
            try
            {
                // Connect to the server and query player score data for each game
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(selectHighScores, con))
                    {
                        // Parse the returned data and insert it into a string used to build an html table
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                stringBuilder.Append("<tr>\n");
                                stringBuilder.Append($"<td>{reader["GameID"]}</td><td>{reader["MaximumMass"]}</td>\n<td>{reader["MaximumRank"]}</td>\n<td>" +
                                    $"{reader["TimeToDeath"]}</td><td>{reader["TimeToNumberOne"]}</td>\n");
                            }
                        }
                    }
                    stringBuilder.Append("</tr>\n");
                }
                return stringBuilder.ToString();
            }
            // Return an error page if anything goes wrong
            catch (Exception ex)
            {
                return ("HTTP / 1.1 200 OK\nConnection.Close\nContent-Type: text/html; charset=UTF-8\n\n<h1>Page Not found</h1>\n<a href =localhost:11001>Home </a>\n</body>\n</html>");
            }
        }

        /// <summary>
        /// Method used to build an html table containg information on the player x location, y location, and mass at the time of player death for each game
        /// contained in the database.
        /// </summary>
        /// <returns>A string representing an html table for player information at the time of player death</returns>
        internal static string BuildPlayerDeathStatsTable()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                // Connect to the database and query for player death information
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM PlayerDeathStats", con))
                    {
                        // Parse returned data and insert it into html table builder string
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                stringBuilder.Append("<tr>\n");
                                stringBuilder.Append($"<td>{reader["GameID"]}</td>\n<td><a href = /scores/{reader["PlayerName"]}>{reader["PlayerName"]}</a></td>\n<td>" +
                                    $"{reader["PlayerPositionX"]}</td>\n<td>{reader["PlayerPositionY"]}</td>\n<td>{reader["PlayerMass"]}</td>\n");
                            }
                        }
                    }
                    stringBuilder.Append("</tr>\n");
                }
                return stringBuilder.ToString();

            }
            // Reutrn an error page if anything goes wrong
            catch (Exception ex)
            {
                return ("HTTP / 1.1 200 OK\nConnection.Close\nContent-Type: text/html; charset=UTF-8\n\n<h1>Page Not found</h1>\n<a href =localhost:11001>Home </a>\n</body>\n</html>");
            }
        }

        /// <summary>
        /// Method used to build a string that represents a JavaScript graph of maximum player mass by game. This method is based almost entirely
        /// on the example shown in lecture.
        /// </summary>
        /// <param name="playerName">The player whose mass will be graphed</param>
        /// <returns>A string used to construct a JavaScript graph for mass</returns>
        /// <exception cref="Exception">Throws an exception if anything goes wrong</exception>
        internal static string GraphPlayerMass(string playerName)
        {
            // Count the number of observations to set x-axis values
            int massCount = 0;
            StringBuilder chartString = new StringBuilder();
            // The following is directly from lecture. End in y-values so that mass data may be added there
            chartString.Append("<canvas id = 'mass chart' style='width: 100 %; max - width:700px'></canvas>\n<script>\nvar yValues = [");
            // Query the database for player maximum mass data for each game
            string selectMaxMass = $@"SELECT MaximumMass FROM MaximumPlayerMass WHERE PlayerName = '{playerName}'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Parse the mass data and append it to the string in the y-values section
                    using (SqlCommand command = new SqlCommand(selectMaxMass, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                chartString.Append(reader["MaximumMass"] + ",");
                                massCount++;
                            }
                        }
                    }
                }
                // End the y-values section and build the x-values section
                chartString.Remove(chartString.Length - 1, 1);
                chartString.Append("]\nvar xValues=[");
                for (int i = 0; i < massCount - 1; i++)
                {
                    chartString.Append($"{i + 1},");
                }
                chartString.Append($"{massCount}]\n");
                // The following line is directly from lecture. It appears to set the chart display
                chartString.Append("new Chart('mass chart',{\ntype:'line',\ndata:{\nlabels:xValues,\ndatasets:[{label:'Mass by game',\nbackgroundColor:'rgba(0,0,0,1.0)',\nborderColor:'rgba(0,0,0,0.2)',\ndata:yValues}]\n},});\n</script>");
                return chartString.ToString();
            }
            // Throw an exception if anything goes wrong
            catch (Exception ex)
            {
                throw new Exception();
                
            }
        }

        /// <summary>
        /// Method used to add player data to the database when requested by the web browser. The added data includes the maximum mass, maximum rank,
        /// start time, and end time in millis of a player's game. All data that is not included in the browser request is set to default values. Input
        /// player data has the form [[name], [maximum mass], [maximum rank], [start time], [end time]].
        /// </summary>
        /// <param name="playerInfo">The array containing player data</param>
        /// <exception cref="Exception"></exception>
        internal static void AddPlayerScores(string[] playerInfo)
        {
            int maxMass = int.Parse(playerInfo[1]);
            int maxRank = int.Parse(playerInfo[2]);
            double startTime = double.Parse(playerInfo[3]);
            double endTime = double.Parse(playerInfo[4]);
            double timeToDeath = endTime - startTime;
            try
            {
                // Establish connection with database and insert data into specified tables
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand maxMassCommand = new SqlCommand(@$"INSERT INTO MaximumPlayerMass VALUES ('{playerInfo[0]}', {maxMass})", con))
                    {
                        maxMassCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand maxRankCommand = new SqlCommand(@$"INSERT INTO MaximumPlayerRank VALUES ('{playerInfo[0]}', {maxRank})", con))
                    {
                        maxRankCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand playerDeathCommand = new SqlCommand(@$"INSERT INTO PlayerDeathStats VALUES ('{playerInfo[0]}',-1,-1,-1)", con))
                    {
                        playerDeathCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand gameTimeCommand = new SqlCommand(@$"INSERT INTO PlayerGameTime VALUES ('{playerInfo[0]}', {startTime}, {endTime})", con))
                    {
                        gameTimeCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand timeToNumberOneCommand = new SqlCommand(@$"INSERT INTO PlayerTimeToNumberOne VALUES ('{playerInfo[0]}',-1)", con))
                    {
                        timeToNumberOneCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand serverAssignedCommand = new SqlCommand(@$"INSERT INTO ServerAssignedAttributes VALUES ('{playerInfo[0]}',-1,0)", con))
                    {
                        serverAssignedCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand timeToDeathCommand = new SqlCommand(@$"INSERT INTO TimeToPlayerDeath VALUES ('{playerInfo[0]}',{timeToDeath})", con))
                    {
                        timeToDeathCommand.ExecuteNonQuery();
                    }
                }
            }
            // Throw an exception if anything goes wrong with data insertion
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}

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
/// This file contains a class that keeps track of the status of an Agario game world. It has information about the world size,
/// the players currently active, and the food currently active. Interfaces with the CLientGUI project to receive information
/// from the server and to provide the game world status when needed for display.
/// 
///</summary>

using Microsoft.Extensions.Logging;
using FileLogger;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;

namespace AgarioModels
{

    ///<summary>
    /// This class keeps track of the status of an Agario game world. It has information about the world size, the players currently
    /// active, and the food currently active. Interfaces with the CLientGUI project to receive information from the server and to 
    /// provide the game world status when needed for display.
    ///</summary>
    public class World
    {

        public const float Width = 5000f;
        public const float Height = 5000f;
        private Dictionary<long, Player> activePlayers;
        private Dictionary<long, Food> currentFood;
        private ILogger logger;

        /// <summary>
        /// Constructor method for an Agario game world. Called each time a new game starts.
        /// </summary>
        public World()
        {
            activePlayers = new Dictionary<long, Player>();
            currentFood = new Dictionary<long, Food>();
            logger = new CustomLogger("World");
        }

        /// <summary>
        /// Method used to add food to the dictionary of active foods.
        /// </summary>
        /// <param name="message">A JSON serialized representaiton of all
        /// active foods in the game world</param>
        public void AddFood(string message)
        {
            List<Food>? foodList = JsonSerializer.Deserialize<List<Food>>(message);
            if (foodList == null)
            {
                return;
            }
            lock (currentFood)
            {
                foreach (Food food in foodList)
                {
                    currentFood[food.ID] = food;
                }
            }
            lock (logger)
            {
                logger.LogInformation($"Added {foodList.Count} foods to world\n");
            }
        }

        /// <summary>
        /// Method used to add players to the dictionary of active players.
        /// </summary>
        /// <param name="message">A JSON serialized representaiton of all
        /// active players in the game world</param>
        public void UpdatePlayers(string message)
        {
            List<Player>? playerList = JsonSerializer.Deserialize<List<Player>>(message);
            if (playerList == null)
            {
                return;
            }
            lock (activePlayers)
            {
                foreach (Player player in playerList)
                {
                    activePlayers[player.ID] = player;
                }
            }
            lock (logger)
            {
                logger.LogInformation($"There are currently {playerList.Count}\n");
            }
        }

        /// <summary>
        /// Method used to remove dead players from the dictionary of active players.
        /// </summary>
        /// <param name="deadPlayers">A string array of dead player ID. Each ID is a string representation
        /// of a long</param>
        public void RemovePlayers(string[] deadPlayers)
        {
            int playersRemoved = 0;
            lock (activePlayers)
            {
                foreach (string playerIDString in deadPlayers)
                {
                    // Each ID is represented as a string in the input, need to parse it to a long to remove players
                    // from the active player dictionary
                    long playerID = long.Parse(playerIDString);
                    activePlayers.Remove(playerID);
                    playersRemoved++;
                }
            }
            lock (logger)
            {
                logger.LogInformation($"Removed {playersRemoved} players from world\n");
            }
        }

        /// <summary>
        /// Method used to remove eaten foods from the dictionary of active foods.
        /// </summary>
        /// <param name="message">A string representing an array of eaten food ID numbers</param>
        public void RemoveFood(string message)
        {
            int removedFoods = 0;
            // The message will have opening and closing brackets, trim those off before processing further
            message = message[1..(message.Length - 1)];
            if (message == "")
            {
                return;
            }
            // If there are food ID strings remainging in the message, split them into an array of strings
            // representing an individual ID each
            string[] foodIdArray = message.Split(',');
            lock(currentFood)
            {
                foreach (string foodIDString in foodIdArray)
                {
                    // Parse the string representation into a long before removing the food from the dictionary
                    currentFood.Remove(long.Parse(foodIDString));
                    removedFoods++;
                }
            }
            lock (logger)
            {
                logger.LogInformation($"Removed {removedFoods} foods from world\n");
            }
        }

        /// <summary>
        /// Returns the dictionary of active foods
        /// </summary>
        /// <returns>The dictionary of active foods</returns>
        public Dictionary<long, Food> GetCurrentFood()
        {
            lock (currentFood)
            {
                lock (logger)
                {
                    logger.LogInformation($"Sent {currentFood.Count} foods to GUI\n");
                }
                return currentFood;
            }
        }

        /// <summary>
        /// Returns the dictionary of active players
        /// </summary>
        /// <returns>The dictionary of active players</returns>
        public Dictionary<long, Player> GetActivePlayers()
        {
            lock (activePlayers) 
            {
                lock (logger)
                {
                    logger.LogInformation($"Sent {activePlayers.Count} players to GUI\n");
                }
                return activePlayers; 
            }
        }

        /// <summary>
        /// Reutrns the client player object from the dictionary of active players
        /// </summary>
        /// <param name="clientPlayerID">The client player object's ID number</param>
        /// <returns>The client player object</returns>
        public Player GetClientPlayer(long clientPlayerID)
        {
            lock (activePlayers)
            {
                lock (logger)
                {
                    logger.LogInformation($"Sent client player object to GUI\n");
                }
                return activePlayers[clientPlayerID];
            }
        }
    }
}
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
/// This file contains the definition of an Agario Player that extends the GameObject class. It contains only one additional property
/// and a specified constructor for use in JSON serialization.
/// 
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{

    /// <summary>
    /// This class contains the definition of an Agario Player that extends the GameObject class. It contains only one additional property
    /// and a specified constructor for use in JSON serialization.
    ///</summary>
    public class Player : GameObject
    {

        public String Name { get; set; }

        /// <summary>
        /// Constructor method for a Player object. This constructor is setup to allow for JSON serialization and deserialization.
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="id">The server assigned ID number</param>
        /// <param name="x">The player's x-coordinate in the world</param>
        /// <param name="y">The player's y-coordinate in the world</param>
        /// <param name="argbcolor">An int specifying the player's ARGB color code</param>
        /// <param name="mass">The player's mass</param>
        [JsonConstructor]
        public Player(string name, long id, float x, float y, int argbcolor, float mass) : base(id, x, y, argbcolor, mass)
        {
            Name = name;
        }

    }
}

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
/// This file contains the definition of an Agario Food that extends the GameObject class. It contains only a 
/// constructor for use in JSON serialization and deserialization.
/// 
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{

    /// <summary>
    /// This class contains the definition of an Agario Food that extends the GameObject class. It contains only a 
    /// constructor for use in JSON serialization and deserialization.
    /// ///</summary>
    public class Food : GameObject
    {
        /// <summary>
        /// Constructor method for an Agario Food object. This constructor is setup to allow for JSON serialization and deserialization.
        /// </summary>
        /// <param name="id">The server assigned ID number</param>
        /// <param name="x">The food's x-coordinate in the world</param>
        /// <param name="y">The food's y-coordinate in the world</param>
        /// <param name="argbcolor">An int specifying the food's ARGB color code</param>
        /// <param name="mass">The food's mass</param>
        [JsonConstructor]
        public Food(long id, float x, float y, int argbcolor, float mass) : base(id, x, y, argbcolor, mass) { 
        }

    }
}

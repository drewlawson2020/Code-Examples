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
/// This file contains the definition of an Agario game object that is extended by the Food and Player classes. Each game
/// object contains a server defined ID, a vector location used to give x and y position, an ARGB color integer, and a mass
/// used to calculate a radius. The constructor is setup to allow extending classes to use JSON serialization and deserialization.
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
    /// This class contains the definition of an Agario game object that is extended by the Food and Player classes. Each game
    /// object contains a server defined ID, a vector location used to give x and y position, an ARGB color integer, and a mass
    /// used to calculate a radius. The constructor is setup to allow extending classes to use JSON serialization and deserialization.
    ///</summary>
    public class GameObject
    {
        // The first two properties are not sent by the Agario server so are ignored for JSON serialization and deserialization
        [JsonIgnore]
        public double Radius { get; set; }
        [JsonIgnore]
        public Vector2 Location { get; set; }
        public long ID { get; set; }
        public float X { get => Location.X; }
        public float Y { get => Location.Y; }
        public int ARGBColor { get; set; }
        public float Mass { get; set; }

        /// <summary>
        /// Constructor for an Agario game object. Setup to allow for JSON serialization and deserialization
        /// by extending classes.
        /// </summary>
        /// <param name="id">The server assigned ID number</param>
        /// <param name="x">The object's x-coordinate in the world</param>
        /// <param name="y">The object's y-coordinate in the world</param>
        /// <param name="argbcolor">An int specifying the object's ARGB color code</param>
        /// <param name="mass">The object's mass</param>
        [JsonConstructor]
        public GameObject(long id, float x, float y, int argbcolor, float mass)
        {
            ID = id;
            Location = new Vector2(x, y);
            ARGBColor = argbcolor;
            Mass = mass;
            // Radius chosen so that mass corresponds to area one-to-one
            Radius = Math.Sqrt(Mass/Math.PI);
        }

    }
}

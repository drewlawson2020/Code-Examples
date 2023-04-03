/// <summary>
/// Author: Peter Bruns
/// Partner: None
/// Date: February 2, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// I, Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from 
/// another source. All references used in the completion of the assignmentare cited in my README file.
/// 
/// File Contents
/// 
/// Contains extension methods for C# built-in classes.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// This class contains extendion methods for C# built-in classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extension of the Peek() method for stacks. First checks to make sure that the
        /// stack is not empty before using Peek() to determine the top item on the stack.
        /// </summary>
        /// <typeparam name="T">Generic placeholder for the type of object in the stack</typeparam>
        /// <param name="stack">The target stack</param>
        /// <param name="item">The target item</param>
        /// <returns>Returns true if item is on the top of the stack. Returns false if
        /// the stack is empty or a different item is on the top of the stack</returns>
        // The method declaration is taken from an announcement made by Dr. de St Germain
        public static bool NextItem<T>(this Stack<T> stack, T item) where T : notnull
        {
            if (stack.Count == 0)
            {
                return false;
            }
            if (stack.Peek().Equals(item))
            {
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Extension method for dictionary that adds a key-value pair if the key is not already in
        /// the dictionary, or updates the value for the key if the dictionary already contains the
        /// key.
        /// </summary>
        /// <typeparam name="TKey">Type of key in the dictionary</typeparam>
        /// <typeparam name="TValue">Type of values in the dictionary</typeparam>
        /// <param name="dict">The target dictionary</param>
        /// <param name="name">The name of the key to be updated</param>
        /// <param name="data">The value for the key</param>
        public static void SetCell<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey name, TValue data) where TKey : notnull
        {
            if (!dict.ContainsKey(name))
            {
                dict.Add(name, data);
            }
            else
            {
                dict[name] = data;
            }
        }
    }
}

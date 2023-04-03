/// <summary>
/// Author: Peter Bruns
/// Partner: None
/// Date: January 27, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// I, Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// Contains methods for creating and updating a dependency graph for use in a spreadsheet application. The
/// graph is represented with two separate dictionaries. One dictionary stores dependent relationships and
/// the other stores dependee relationships. The dictionaries contain HashSets of nodes related to the key
/// node. The methods in this file can return the size of the graph, number of dependees, the set of dependents,
/// and the set of dependees. Other methods are used to add dependencies, remove dependencies, and replace the
/// dependees or dependents of a specified node.
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// This class contains methods for implementation of a dependency graph. The class contains methods for adding
    /// and removing individual dependencies, and for replacing all dependents or dependees of a specified node. The
    /// class also contains methods that return the number of relations in the graph, and the set of dependents or dependees
    /// for a node. Indexing the graph returns the number of dependees of a node.
    /// </summary>
    public class DependencyGraph
    {
        // Make instance variables for dictionaries to hold dependent/dependee relationships
        private Dictionary<string, HashSet<string>> dependents;
        private Dictionary<string, HashSet<string>> dependees;
        private int size;

        /// <summary>
        /// Constructor method for a new DependencyGraph
        /// </summary>
        public DependencyGraph()
        {
            dependents = new Dictionary<string, HashSet<string>>();
            dependees = new Dictionary<string, HashSet<string>>();
            size = 0;
        }

        /// <summary>
        /// Returns number of dependency relationships in the graph
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// Returns the number of dependees for an input node. For example, if g is a dependency graph and
        /// x has dependees b and c, g["x"] returns 2.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>The number of dependees for s or 0 if s is not in the graph/has no dependees</returns>
        public int this[string s]
        {
            get { return GetDependees(s).Count(); }
        }

        /// <summary>
        /// Checks if an input node has any dependents. Returns false if the node is not in the graph.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if s has dependents, false otherwise</returns>
        public bool HasDependents(string s)
        {
            // Call GetSet helper method to get the set of dependents for s
            HashSet<string> nodeDependents = GetSet(s, dependents);
            if (nodeDependents.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if an input node has any dependees. Returns false if the node is not in the graph.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>True if s has dependees, false otherwise</returns>
        public bool HasDependees(string s)
        {
            // Call GetSet helper method to get the set of dependees for s
            HashSet<string> nodeDependees = GetSet(s, dependees);
            if (nodeDependees.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the set of dependents for an input node. Returns an empty set if the node has
        /// no dependents or is not in the graph.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>The set of dependents for s</returns>
        public IEnumerable<string> GetDependents(string s)
        {
            // Call get set helper method to get set of dependents for s
            return GetSet(s, dependents);
        }

        /// <summary>
        /// Returns the set of dependees for an input node. Returns an empty set if the node has
        /// no dependees or is not in the graph.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>The set of dependees for s</returns>
        public IEnumerable<string> GetDependees(string s)
        {
            // Call get set helper method to get set of dependees for s
            return GetSet(s, dependees);
        }

        /// <summary>
        /// Adds a dependency relationship to the graph. For example, AddDependency(s, t) will
        /// add t to the graph as a dependent of s. In this case, s will also be added as a
        /// dependee of t. If the input relationship is already contained in the graph, the
        /// method will not alter the graph.
        /// </summary>
        /// <param name="s">The node for which t is a dependent</param>
        /// <param name="t">The node dependent on s/for which s is a dependee</param>
        public void AddDependency(string s, string t)
        {
            if (dependents.ContainsKey(s))
            {
                // If t is added to the set of dependents for s, call helper
                // method to add s to dependees of t
                if (dependents[s].Add(t))
                {
                    size++;
                    AddDependencyUpdateDependees(s, t);
                }
            }
            // If s is not a key in the dependents dictionary, add it and use helper
            // method to update dependees for t
            else
            {
                dependents.Add(s, new HashSet<string> { t });
                size++;
                AddDependencyUpdateDependees(s, t);
            }
        }

        /// <summary>
        /// Helper method to update dependee dictionary when a new dependency
        /// relationship is added to the graph
        /// </summary>
        private void AddDependencyUpdateDependees(string dependee, string dependent)
        {
            if (dependees.ContainsKey(dependent))
            {
                dependees[dependent].Add(dependee);
            }
            else
            {
                dependees.Add(dependent, new HashSet<string> { dependee });
            }
        }


        /// <summary>
        /// Removes a dependency relation from the graph if it exists in the graph. Does nothing
        /// if the input relationship is not contained in the graph or the graph is empty.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            // If s is a key in dependents and t is removed from the set of
            // dependents for s, remove s from the set of dependees for t
            if (dependents.ContainsKey(s))
            {
                if (dependents[s].Remove(t))
                {
                    dependees[t].Remove(s);
                    size--;
                }
            }
        }


        /// <summary>
        /// First removes all dependents for a specified input node. Next, adds new dependency
        /// relationships based on nodes in an input collection so that all members of the collection
        /// are dependent on the specified node. If the node is not already in the dependents dictionary, 
        /// adds it as a key with the collection of new dependents associated with it. Also updates dependees 
        /// to remove relationships for the old dependents and add relationships for the new dependents.
        /// 
        /// Example: before the method call b and c are dependent on a so a's set of dependents is {b, c},
        /// and a is a dependee of b and of c. If newDependents contains x and y, a's set of dependents after 
        /// the method call will be {x, y}. When the method finishes, b and c will have not have a as a dependee,
        /// and x and y will have a as a dependee.
        /// </summary>
        /// <param name="s">The node for which the set of dependents is to be replaced</param>
        /// <param name="newDependents">The collection of new dependents for s</param>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            // Use the GetSet helper method to get the set of dependents for s
            HashSet<string> oldDependents = GetSet(s, dependents);
            // Iterate over set of old dependents to remove dependency relationships
            foreach (string oldDependent in oldDependents)
            {
                RemoveDependency(s, oldDependent);
            }
            // Iterate over collection of new dependents to add dependency relationships
            foreach (string newDependent in newDependents)
            {
                AddDependency(s, newDependent);
            }
        }


        /// <summary>
        /// First removes all dependees for a specified input node. Next, adds new dependency relationships 
        /// based on nodes in an input collection so that all members of the collection are dependees for the
        /// specified node. If the node is not already in the dependees dictionary, adds it as a key with the 
        /// collection of new dependees associated with it. Also updates dependents dictioanry to remove 
        /// relationships for the old dependees and add relationships for the new dependees.
        /// 
        /// Example: before the method call b and c are dependees of a so a's set of dependees is {b, c}, and a is 
        /// dependent on b and c. If newDependees contains x and y, a's set of dependees after the method call will 
        /// be {x, y}. When the method finishes, b and c will have not have a as a dependent, and x and y will have 
        /// a as a dependent.
        /// </summary>
        /// <param name="s">The node for which the set of dependees is to be replaced</param>
        /// <param name="newDependees">The collection of new dependees for s</param>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            // Use the GetSet helper method to get the set of dependees for s
            HashSet<string> removedDependees = GetSet(s, dependees);
            // Iterate over set of old dependees to remove dependency relationships
            foreach (string oldDependee in removedDependees)
            {
                RemoveDependency(oldDependee, s);
            }
            // Iterate over collection of new dependees to add dependency relationships
            foreach (string newDependee in newDependees)
            {
                AddDependency(newDependee, s);
            }
        }

        /// <summary>
        /// Helper method used to get the set of dependents or dependees associated with an input key.
        /// Returns an empty set if the target dictionary does not contain the input key.
        /// </summary>
        /// <param name="inputKey"></param>
        /// <param name="targetDict">The dictionary from which the set associated with the target key
        /// is accesed and returned</param>
        /// <returns>The set associated with the input in the target dictionary or and
        /// empty set if the input is not a key in the target dictionary</returns>
        private HashSet<string> GetSet(string inputKey, Dictionary<string, HashSet<string>> targetDict)
        {
            // Return the set mapped to the input key or an empty set if the input key
            // is not in the input dictionary
            if (targetDict.ContainsKey(inputKey))
            {
                return targetDict[inputKey];
            }
            else
            {
                return new HashSet<string>();
            }
        }
    }
}
// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)
// Version 1.3 - Drew Lawson 
//               (Implemented necessary methods and changed comments to describe process) 

/// <summary> 
/// Author:    Drew Lawson
/// Partner:   None
/// Date:      27-Jan-2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Drew Lawson - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Drew Lawson, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    This file contains all of the necessary methods and functions to allow the program to handle implied
///    relations between ordered strings, that are dependents, and dependees.
///    
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        private Dictionary<string, HashSet<string>> dependees;
        private Dictionary<string, HashSet<string>> dependents;
        private int size;
        /// <summary>
        /// Creates an empty DependencyGraph. Dictionaries dependees and dependents are created, as well as initializing size as 0.
        /// Dependees and Dependents are constructed with String and HashSets contained within. String acts as the key for
        /// each respective dependent/dependee, and the HashSet stores the implied link with dependees/dependents.
        /// </summary>
        public DependencyGraph()
        {
            dependees = new Dictionary<String, HashSet<String>>();
            dependents = new Dictionary<String, HashSet<String>>();
            size = 0;
        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return size; }
        }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get
            {
                if (dependents.ContainsKey(s))
                {
                    return dependents[s].Count;
                }
                else
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            return dependees.ContainsKey(s);
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            return dependents.ContainsKey(s);
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if (dependees.ContainsKey(s))
            {
                return new HashSet<string>(dependees[s]);
            }
            else
            {
                return new HashSet<string>();
            }
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            //Checks if dependent key exists in dependents Dictionary
            if (dependents.ContainsKey(s))
            {
                return new HashSet<string>(dependents[s]);
            }
            else
            {
                return new HashSet<string>();
            }
        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            //Checks if the dependees does not contain key s, or if dependents does not contain key t. This ensures
            //that no duplicates are added. If overall true, then the size (of pairs) increments by one.
            if (!dependees.ContainsKey(s) || !dependents.ContainsKey(t))
            {
                size++;
            }

            //Checks if dependees already contains key s. If true, then the t key is added to the HashSet of dependees.
            if(dependees.ContainsKey(s))
            {
                dependees[s].Add(t);
            }
            else // If not true, creates a new HashSet that introduces s to the dependent list, while still adding t through dependees.
            {
                HashSet<string> temp_dependents = new HashSet<string>();

                temp_dependents.Add(t);
                dependees.Add(s, temp_dependents);
            }

            //Code below is the same as above, but this time checks for t to add to dependents.
            if (dependents.ContainsKey(t))
            {
                dependents[t].Add(s);
            }
            else //Same as above, but does so for dependees and then the dependents.
            {
                HashSet<string> temp_dependees = new HashSet<string>();

                temp_dependees.Add(s);
                dependents.Add(t, temp_dependees);
            }

        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            //Checks if the pairing does exist within the dependency map. If true, then it decrements the size.
            if (dependees.ContainsKey(s) && dependents.ContainsKey(t))
            {
                size--;
            }

            //Checks if the dependees contains key s. If true, then removes key t from its found HashSet.
            if (dependees.ContainsKey(s))
            {
                dependees[s].Remove(t);

                //If the number of dependees of key s results to be zero, entirely removes s from the Dictionary as it is
                //no longer necessary.
                if(dependees[s].Count == 0)
                {
                    dependees.Remove(s);
                }
            }

            //Code is same as above, but checks for key t from the dependents.
            if (dependents.ContainsKey(t))
            {
                dependents[t].Remove(s);

                if (dependents[t].Count == 0)
                {
                    dependents.Remove(t);
                }
            }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            //Creates a temporary IEnumerable to store the original dependents
            IEnumerable<string> temp_dependents = GetDependents(s);

            // Removes every dependent that was contained within temp_dependents from the Dictionary, using key s.
            foreach (string r in temp_dependents)
                {
                    RemoveDependency(s, r);
                }
            // Adds every new dependent to the Dictionary, using key s.
            foreach (string t in newDependents)
                {
                    AddDependency(s, t);
                }

        }


            /// <summary>
            /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
            /// t in newDependees, adds the ordered pair (t,s).
            /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            //Creates a temporary IEnumerable to store the original dependees
            IEnumerable<string> temp_dependees = GetDependees(s);

            // Removes every dependee that was contained within temp_dependees from the Dictionary, using key s.
            foreach (string r in temp_dependees)
            {
                RemoveDependency(r, s);
            }
            // Adds every new dependee to the Dictionary, using key s.
            foreach (string t in newDependees)
            {
                AddDependency(t, s);
            }

        }

    }

}

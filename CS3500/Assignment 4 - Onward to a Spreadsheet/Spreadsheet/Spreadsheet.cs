// Written by Joe Zachary for CS 3500, September 2013

using System;
using System.IO;
using System.Collections.Generic;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;

namespace SS
{
    /// <summary> 
    /// Author:    Drew 
    /// Partner:   None
    /// Date:      11-Feb-2022
    /// Course:    CS 3500, University of Utah, School of Computing 
    /// Copyright: CS 3500 and Drew Lawson - This work may not be copied for use in Academic Coursework. 
    /// 
    /// I, Drew Lawson, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source.  All references used in the completion of the assignment are cited in my README file. 
    /// 
    /// File Contents 
    /// 
    ///    This file most of the necessary methods and functions to allow the program to store formulas, strings, and doubles
    ///    into a cell system using a custom nest cell class. The file should be able to successfully store this data, as well
    ///    as verify it to ensure that there are no circular formulas (that is, C1 relies on C2, while C2 relies on C1). Further, it 
    ///    is able to change the contents of existing cells that have already been set by using the name as a key within the cellDictionary.
    ///    This file is also capable of getting the names of all non empty cells. In other words, return all of the keys within the
    ///    dictionary. This file also includes a regex expression method to verify that cell names are valid.
    ///    
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        // Declare the cellDictionary and dependencyGraph, both necessary for the cells to function correctly.
        Dictionary<string, SpreadsheetCell> cellDictionary;
        DependencyGraph dependencyGraph;

        public Spreadsheet()
        {
            cellDictionary = new Dictionary<string, SpreadsheetCell>();
            dependencyGraph = new DependencyGraph();
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            return cellDictionary.Keys;
        }


        /// <summary>
        /// Will check the dictionary contents by looking for the name given through string. Throws an InvalidNameException if
        /// given name is null, or if it is not a properly formatted name under the given specifications. 
        /// </summary>
        /// <param name="name"> User inputted name that may or may not exist in the cell dictionary.</param>
        /// <returns>Returns an object class of the contents of the given name, or otherwise returns "" if nothing was found</returns>
        /// <exception cref="InvalidNameException">Thrown if name is invalid or is null</exception>
        public override object GetCellContents(String name)
        {
            // Check if not null and valid
            if (!ReferenceEquals(name, null) && Is_name(name))
            {

                if (cellDictionary.ContainsKey(name))
                {
                    return cellDictionary[name].contents;
                }
                else
                {
                    return ""; // Returns blank as given by specifications
                }
            }
            else
            {
                throw new InvalidNameException();
            }
        }


        /// <summary>
        /// double variant for setting cell contents. Checks if name is not null and is valid, if so, adds to 
        /// to the dictionary. If not, throws InvalidNameException. 
        /// From there, the cell relations are recalculated to ensure consistency.
        /// </summary>
        /// <param name="name">Name of cell</param>
        /// <param name="number">Double to be added</param>
        /// <returns></returns>
        /// <exception cref="InvalidNameException"></exception>
        public override IList<String> SetCellContents(String name, double number)
        {
            // Checks if it is a valid name and is not null
            if (!ReferenceEquals(name, null) && Is_name(name))
            {
                // Creates temp cell to store new number
                SpreadsheetCell temp_cell = new SpreadsheetCell(number);
                //Checks if it is already in the dictionary. If not, adds directly.
                //If it already is, replaces value using name as the key.
                if (!cellDictionary.ContainsKey(name))
                {
                    cellDictionary.Add(name, temp_cell);
                }
                else
                {
                    cellDictionary[name] = temp_cell;
                }
                //Resets dependees for name regardless of if it already existed or not.
                dependencyGraph.ReplaceDependees(name, new HashSet<String>());

                // Then recalculates dependees as a result of resetting it.
                HashSet<String> found_dependees = new HashSet<String>(GetCellsToRecalculate(name));
                IList<String> list_returned = found_dependees.ToList();
                return list_returned;
            }
            else
            {
                throw new InvalidNameException();
            }
        }

        /// <summary>
        /// string variant for setting cell contents. Checks if name is not null and is valid, if so, adds to 
        /// to the dictionary. If not, throws InvalidNameException. It also checks if text is null. If so, throws
        /// ArgumentNullException.
        /// From there, the cell relations are recalculated to ensure consistency.
        /// Pratically works identically to double variant.
        /// </summary>
        /// <param name="name">Name of cell</param>
        /// <param name="text">String to be added</param>
        /// <returns></returns>
        /// <exception cref="InvalidNameException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public override IList<String> SetCellContents(String name, String text)
        {
            // Checks if string is null.
            if (ReferenceEquals(text, null))
            {
                throw new ArgumentNullException();
            }
            // Checks if it is a valid name and is not null
            if (!ReferenceEquals(name, null) && Is_name(name))
            {
                // Creates temp cell to store new number
                SpreadsheetCell temp_cell = new SpreadsheetCell(text);
                //Checks if it is already in the dictionary. If not, adds directly.
                //If it already is, replaces value using name as the key.
                if (!cellDictionary.ContainsKey(name))
                {
                    cellDictionary.Add(name, temp_cell);
                }
                else
                {
                    cellDictionary[name] = temp_cell;
                }
                //Resets dependees for name regardless of if it already existed or not.
                dependencyGraph.ReplaceDependees(name, new HashSet<String>());
                // Then recalculates dependees as a result of resetting it.

                HashSet<String> found_dependees = new HashSet<String>(GetCellsToRecalculate(name));
                IList<String> list_returned = found_dependees.ToList();
                return list_returned;
            }
            else
            {
                throw new InvalidNameException();
            }
        }

        /// <summary>
        /// Formula variant for setting cell contents. Checks if name is not null and is valid, if so, adds to 
        /// to the dictionary. If not, throws InvalidNameException. Also checks if formula is not null. If it is, throws
        /// ArgumentNullException. From there, the formula tries to get added. As it's added, it ensures that there are
        /// no circular exceptions. If one is found, then resets any changes made and reverts back to how the cells
        /// were originally.
        /// </summary>
        /// <param name="name">Name of cell</param>
        /// <param name="formula">Formu;a to be added</param>
        /// <returns></returns>
        /// <exception cref="InvalidNameException"></exception>
        /// /// <exception cref="ArgumentNullException"></exception>
        public override IList<String> SetCellContents(String name, Formula formula)
        {
            // Checks if formula is not null
            if (ReferenceEquals(formula, null))
            {
                throw new ArgumentNullException();
            }
            // Checks if name is not null and is valid name
            if (!ReferenceEquals(name, null) && Is_name(name))
            {
                // Backs up dependendees as we check the validity of formula.
                IEnumerable<String> backup = dependencyGraph.GetDependees(name);

                dependencyGraph.ReplaceDependees(name, formula.GetVariables());
                // Replaces dependees with the formula's variables. Then, tries to see if it
                // is circular or not. 
                try
                {
                    // Recalculates cells.
                    HashSet<String> found_dependees = new HashSet<String>(GetCellsToRecalculate(name));
                    // Temp cell to store formula in.
                    SpreadsheetCell temp_cell = new SpreadsheetCell(formula);

                    //  Checks if it's already present. If not, adds it with the name as key. If it is,
                    //  replaces cell contents with new contents in temp_cell.
                    if (!cellDictionary.ContainsKey(name))
                    {
                        cellDictionary.Add(name, temp_cell);
                    }
                    else
                    {
                        cellDictionary[name] = temp_cell;
                    }
                    //Returns list of dependees
                    IList<String> list_returned = found_dependees.ToList();
                    return list_returned;
                }
                catch (CircularException e)
                {
                    //Restores old dependees before throwing exception
                    dependencyGraph.ReplaceDependees(name, backup);
                    throw new CircularException();
                }

            }
            else
            {
                throw new InvalidNameException();
            }
        }

        /// <summary>
        /// Returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            return dependencyGraph.GetDependents(name);
        }
        private static Boolean Is_name(String token)
        {
            return Regex.IsMatch(token, @"^[A-Za-z_](?: [A-Za-z_]|\d)*$", RegexOptions.Singleline);
        }
        private class SpreadsheetCell
        {
            public Object contents { get; private set; }
            public Object value { get; private set; }

            public SpreadsheetCell(Double name)
            {
                contents = name;
                value = name;
            }
            public SpreadsheetCell(String name)
            {
                contents = name;
                value = name;
            }
            public SpreadsheetCell(Formula name)
            {
                contents = name;
                value = name;
            }
        }

        /// <summary>
        /// SpreadsheetCell class. Defines contents and values for types Double, String, and Formula.
        /// </summary>


    }
}
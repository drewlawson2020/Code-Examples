// Written by Joe Zachary for CS 3500, September 2013

using System;
using System.IO;
using System.Collections.Generic;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Xml;

namespace SS
{
    /// <summary> 
    /// Author:    Drew 
    /// Partner:   None
    /// Date:      17-Feb-2022
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
    ///    This file was recently updated for the AS5 specifications. This allows the file to get closer to the final
    ///    implementation that will come into play soon. This includes the ability to load and save files, which is the most
    ///    integral part to the process.
    ///    
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        // Declare the cellDictionary and dependencyGraph, both necessary for the cells to function correctly.
        Dictionary<string, SpreadsheetCell> cellDictionary;
        DependencyGraph dependencyGraph;
        private bool changed;

        /// <summary>
        /// Changes the changed bool. Important for knowing when Spreadsheet has been changed for saving.
        /// </summary>
        public override bool Changed 
        {
            get
            {
                return changed;
            }
            protected set
            {
                changed = value;
            }
        }

        /// <summary>
        /// Primary Constructor for Spreadsheet.
        /// </summary>
        public Spreadsheet() : base(s => true, s => s, "default")
        {
            cellDictionary = new Dictionary<string, SpreadsheetCell>();
            dependencyGraph = new DependencyGraph();
            Changed = false;
        }
        /// <summary>
        /// Another constructor for Spreadsheet. Implements the isValid, normalize, and version variables for self-explanatory functionality.
        /// </summary>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
        : base(isValid, normalize, version)
        {
            cellDictionary = new Dictionary<string, SpreadsheetCell>();
            dependencyGraph = new DependencyGraph();
            Changed = false;
        }
        /// <summary>
        /// Similar to the class constructor above, except it utilizes the save and ReadFile function to load in a saved XML file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        /// <exception cref="SpreadsheetReadWriteException"></exception>
        public Spreadsheet(string filepath, Func<string, bool> isValid, Func<string, string> normalize, string version)
           : base(isValid, normalize, version)
        {
            cellDictionary = new Dictionary<string, SpreadsheetCell>();
            dependencyGraph = new DependencyGraph();

            // Checks if there is filepath.
            if (GetSavedVersion(filepath).Equals(version))
            {
                // Loads file in and adds it to the cell dictionary and dependency graph.
                ReadFile(filepath, false);
                Changed = false;
            }
            else
            {
                throw new SpreadsheetReadWriteException("");
            }

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
                name = Normalize(name);
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
        protected override IList<String> SetCellContents(String name, double number)
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
        protected override IList<String> SetCellContents(String name, String text)
        {
                // Creates temp cell to store new number
                SpreadsheetCell temp_cell = new SpreadsheetCell(text);
                //Checks if it is already in the dictionary. If not, adds directly.
                //If it already is, replaces value using name as the key.
                if (text == "")
                {
                   
                }
                else if (!cellDictionary.ContainsKey(name))
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
        protected override IList<String> SetCellContents(String name, Formula formula)
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
                    SpreadsheetCell temp_cell = new SpreadsheetCell(formula, Look_up_value);

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
        private Boolean Is_name(String token)
        {
            return (Regex.IsMatch(token, @"^[a-zA-Z]+[\d]+$"));
        }
        /// <summary>
        /// Sets the contents of the cell within the string dictionary. Uses SetCellContents as helper
        /// methods, and checks for "", "=" (formula), or double. If none are found, string is added instead. 
        /// From there, recalcuates the dependees using a helper method.
        /// </summary>
        /// <param name="name">Name of cell</param>
        /// <param name="content">Content of cell</param>
        /// <returns></returns>
        /// <exception cref="InvalidNameException"></exception>
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            if (Is_name(name))
            {
                HashSet<String> these_dependents;
                // Checks for blank
                if (content.Equals(""))
                {
                    these_dependents = new HashSet<String>(SetCellContents(name, content));
                }
                // Checks for formula
                else if (content.Substring(0, 1).Equals("="))
                {
                    Formula temp_formula = new Formula(content.Substring(1, content.Length - 1), Normalize, IsValid);
                    these_dependents = new HashSet<String>(SetCellContents(name, temp_formula));
                }
                // Checks for Double
                else if (Double.TryParse(content, out _))
                {
                    these_dependents = new HashSet<string>(SetCellContents(name, Double.Parse(content)));
                }
                // Otherwise, likely a string
                else
                {
                    these_dependents = new HashSet<string>(SetCellContents(name, content));
                }

                Changed = true;
                
                foreach (string dependent in these_dependents)
                {
                    if(cellDictionary.TryGetValue(dependent, out var cell))
                    {
                        cell.recalculate_dependees(Look_up_value);
                    }
                }
                IList<String> list_returned = these_dependents.ToList();
                return list_returned;
            }
            else
            {
                throw new InvalidNameException();
            }

        }
        /// <summary>
        /// Using ReadFile helper method, loads in the file, reads the version, and returns it.
        /// </summary>
        /// <param name="filename">The name/path of the file.</param>
        /// <returns></returns>
        public override string GetSavedVersion(string filename)
        {
            // Loads in version name using True boolean value
            return ReadFile(filename, true);
        }

        /// <summary>
        /// Will take the existing data that is saved within the cellDictionary, and saves each cell.
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// Throws a SpreadsheetReadWriteException if filename is blank.
        /// </summary>
        /// <param name="filename"></param>
        /// <exception cref="SpreadsheetReadWriteException"></exception>
        public override void Save(string filename)
        {
            /// Checks if filename is blank.
            if (filename.Equals(""))
            {
                throw new SpreadsheetReadWriteException("");
            }

            try
            {
                /// Creates XmlWriter object
                XmlWriterSettings saveSettings = new XmlWriterSettings();
                saveSettings.Indent = true;
                /// Creates a new writer with the filename and saveSettings.
                using (XmlWriter writer = XmlWriter.Create(filename, saveSettings))
                {
                    /// Start the writer process
                    writer.WriteStartDocument(); 
                    /// Header of document
                    writer.WriteStartElement("spreadsheet");
                    /// version attribute for spreadsheet is written in next
                    writer.WriteAttributeString("version", null, Version);
                    /// Loops through all of the cellDictionary to write to file.
                    foreach (string currCell in cellDictionary.Keys)
                    {
                        ///Writes in cell, along with cell name.
                        writer.WriteStartElement("cell");                     
                        writer.WriteElementString("name", currCell);                                                
                        ///Temp variable for cell contents
                        string cell_contents = "";

                        /// Check for what type the data is, and then writes it to the file.
                        if (cellDictionary[currCell].contents is double)
                        {
                            double temp = (double)cellDictionary[currCell].value;
                            cell_contents = temp.ToString();
                        }
                        else if (cellDictionary[currCell].contents is Formula)
                        {   
                            cell_contents = "=" + cellDictionary[currCell].contents.ToString();
                        }
                        else
                        {   
                            cell_contents = (string)cellDictionary[currCell].contents;
                        }
                        /// The actual writing of the cell contents
                        writer.WriteElementString("contents", cell_contents);                          
                        writer.WriteEndElement(); 
                    }
                    ///Closes and ends the writing process.
                    writer.WriteEndElement();              
                    writer.WriteEndDocument(); 

                } 
            }

            catch (XmlException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }
            catch (IOException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }

            Changed = false;
        }

        /// <summary>
        /// Simply returns the cell value. Returns "" if nothing was found. Throws
        /// InvalidCastException if name is invalid.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidNameException"></exception>
        public override object GetCellValue(string name)
        {
            if(Is_name(name))
            {

                if (cellDictionary.TryGetValue(name, out var cell))
                {
                    return cell.value;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                throw new InvalidNameException();
            }
        }

        /// <summary>
        /// SpreadsheetCell class. Defines contents and values for types Double, String, and Formula.
        /// </summary>
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
            public SpreadsheetCell(Formula name, Func<string, double> lookup)
            {
                contents = name;
                value = name;
            }
            /// <summary>
            /// Recalculates the dependees using the assigned lookup.
            /// </summary>
            /// <param name="lookup"></param>
            public void recalculate_dependees(Func<string, double> lookup)
            {
                if(contents is Formula)
                {
                    Formula temp = (Formula)contents;
                    value = temp.Evaluate(lookup);
                }
            }
        }
        /// <summary>
        /// Looks up value contained within the cellDictionary. If not found, throws ArgumentException.
        /// </summary>
        /// <param name="value">Name of value supposedly in cellDictionary.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private double Look_up_value(string value)
        {
            if (cellDictionary.TryGetValue(value, out _))
            {
                SpreadsheetCell temp = cellDictionary[value];
                if (temp.value is double)
                {
                    return (double)temp.value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Helper method that loads in files and writes them to the Spreadsheet.
        /// This method takes a boolean argument to allow for near instant loading of 
        /// the version for GetSavedVersion if get_version is true. Otherwise, loads as normal using
        /// XMLreader.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="get_version"></param>
        /// <returns></returns>
        /// <exception cref="SpreadsheetReadWriteException"></exception>
        private string ReadFile(string filename, bool get_version)
        {
            /// Checks if filename blank.
            if (filename.Equals(""))
            {
                throw new SpreadsheetReadWriteException("");
            }
            try
            {
                /// Reads in filename if not blank. Throws exception if not found.
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    string name = String.Empty;
                    string content = String.Empty;

                    ///While loop to read in everything
                    while (reader.Read())
                    {
                        ///Skips blank elements
                        if (reader.IsStartElement())
                        {
                           ///Flag to check if the contents were changed or not. Important for writing contents to Spreadsheet.
                           bool changed_contents = false;

                            if (reader.Name == "spreadsheet")
                            {
                                reader.MoveToFirstAttribute();
                                ///Checks version. Returns of get_version is true. Otherwise, sets constructor's Version to the read in value.
                                if (get_version)
                                {
                                    return reader.Value;
                                }
                                else
                                {
                                    Version = reader.Value;
                                }
                            }
                            else if (reader.Name == "cell")
                            {
                                continue;
                            }
                            else if (reader.Name == "contents")
                            {
                                ///Reads in contents, saves it to content, and changes changed_contents to true.
                                reader.Read();
                                content = reader.Value;
                                changed_contents = true;
                            }
                            else if (reader.Name == "name")
                            {
                                ///Reads in name, saves it to name string
                                reader.Read();
                                name = reader.Value;
                            }
                            /// If it was updated, sets the contents of the cell using the name and current strings.
                            if(changed_contents)
                            {
                                SetContentsOfCell(name, content);
                            }
                        }

                    }
                }
            }
            catch (XmlException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }
            catch (IOException e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }

            return Version;
        }

       

    }
}
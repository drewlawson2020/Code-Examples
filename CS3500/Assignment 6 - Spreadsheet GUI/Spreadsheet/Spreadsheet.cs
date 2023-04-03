/// <summary>
/// Author: Peter Bruns
/// Partner: None
/// Date: February 18, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// I, Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// Contains methods needed to represent cell names, content, and values in a spreadsheet. Valid cell names
/// begin with one or more letters followed by one or more digits. Spreadsheets may specify normalizers and 
/// validators for variable names. Normalizers convert names (for example from lower to uppercase) and validators
/// can specify valid normalized variable names for individual spreadsheets. The validator may be less restrictive
/// than thespreadsheet variable name specification, but all names must be valid according to the overall Spreadsheet
/// specification (for example, "A_1" will never be valid even if the validator allows underscores).
/// 
/// Cell content may be a string, double, or Formula object. This content is used to compute the cell's value at
/// the time of cell creation. Cell values may be a double, string, or FormulaError. The formulas contained in a 
/// Spreadsheet can never result in a circular dependency without an exception being thrown.
/// 
/// The methods in this file return the names of nonempty cells, return the content of a cell, return the value of a cell,
/// set the content of a cell and update the values of all cells that are dependent on that cell. The Spreadsheet also contains
/// methods to read and write XML files containing data about nonempty cells.
/// 
///</summary>

// Comment to make sure branching worked

using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Extensions;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace SS
{
    /// <summary>
    /// <para>Contains methods needed to represent cell names, content, and values in a spreadsheet. Valid cell names
    /// begin with one or more letters followed by one or more digits. Spreadsheets may specify normalizers and 
    /// validators for variable names. Normalizers convert names (for example from lower to uppercase) and validators
    /// can specify valid normalized variable names for individual spreadsheets. The validator may be less restrictive
    /// than thespreadsheet variable name specification, but all names must be valid according to the overall Spreadsheet
    /// specification (for example, "A_1" will never be valid even if the validator allows underscores).</para>
    /// 
    /// <para>Cell content may be a string, double, or Formula object. This content is used to compute the cell's value at
    /// the time of cell creation. Cell values may be a double, string, or FormulaError. The formulas contained in a 
    /// Spreadsheet can never result in a circular dependency without an exception being thrown.</para>
    /// 
    /// <para>The methods in this class return the names of nonempty cells, return the content of a cell, return the value of a cell,
    /// set the content of a cell and update the values of all cells that are dependent on that cell. The Spreadsheet also contains
    /// methods to read and write XML files containing data about nonempty cells.</para>
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        // DependencyGraph to track cell dependents and dependees
        private DependencyGraph cellDependencyGraph;
        // Dictionary that holds normalized names of nonempty cells as keys, cell objects as values
        private Dictionary<String, Cell> nonemptyCells;
        // Boolean used to track if the spreadsheet has been changed since being created, opened,
        // or saved
        private Boolean sheetChanged;

        /// <summary>
        /// No argument constructor for a Spreadsheet object. This sets the validator to return true no
        /// matter what, sets the normalizer to the identity function, and sets the Version of the Spreadsheet
        /// to "default."
        /// </summary>
        public Spreadsheet() : this(str => true, str => str, "default")
        {
        }

        /// <summary>
        /// Three argument connstructor for a Spreadsheet object. This allows the user to specify the validator,
        /// normalizer, and Version id. The validator may be more or less restrictive than the overall Spreadsheet
        /// specification for cell names.
        /// </summary>
        /// <param name="isValid">Function to determine if a cell name is valid for this Spreadsheet</param>
        /// <param name="normalize">Function to normalize cell names for this Spreadsheet</param>
        /// <param name="version">The specified version id for this Spreadsheet</param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            cellDependencyGraph = new DependencyGraph();
            nonemptyCells = new Dictionary<String, Cell>();
            // Mark new spreadsheets as unchanged
            sheetChanged = false;
        }

        /// <summary>
        /// Four argument connstructor for a Spreadsheet object. This constructor allows for a Spreadsheet to be created 
        /// from data in a saved file and lets the user specify the validator, normalizer, and Version id for the 
        /// Spreadsheet. The specified version must match the version of the saved file. The validator may be more or less
        /// restrictive than the overall Spreadsheet specification for variable names.
        /// </summary>
        /// <param name="PathToFile"></param>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(string PathToFile, Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            cellDependencyGraph = new DependencyGraph();
            nonemptyCells = new Dictionary<String, Cell>();
            // Call helper method to read the saved data from file
            string inputVersion = GetSavedVersion(PathToFile);
            if (inputVersion != this.Version)
            {
                throw new SpreadsheetReadWriteException("The input version does not match the specified version.");
            }
            // Mark new spreadsheets as unchanged
            sheetChanged = false;
        }

        /// <inheritdoc />
        public override bool Changed { get => sheetChanged; protected set => sheetChanged = false; }

        /// <summary>
        /// <para> Determines if a given cell name is valid. Throws an InvalidNameException if it
        /// is not. Names must be valid according to both Spreadsheet specifications and the provided
        /// validator function.</para>
        /// <para>The provided validator may be more or less restrictive than the Spreadsheet specification, 
        /// but names must always be valid according to Spreadsheet (start with one or more letters followed
        /// by one or more digits).</para>
        /// <para>For example, if the validator only accepts uppercase letters, then "a1" and "b1" will be
        /// invalid. Even if the validator accepts underscores, "A_1" would be invalid because Spreadsheet
        /// does not allow variable names to contain underscores.</para>
        /// </summary>
        /// <param name="name">The cell name</param>
        /// <exception cref="InvalidNameException">If the provided name is invalid.</exception>
        private void NameIsValid(String name)
        {
            // Modified from provided code in Formula
            Regex namePattern = new Regex((@"^[a-zA-Z]+[0-9]+$"));
            // Assess name by both the Spreadsheet specifications and validator function
            if (!namePattern.IsMatch(name) || !IsValid(name))
            {
                throw new InvalidNameException();
            }
        }

        /// <inheritdoc />
        public override object GetCellContents(string name)
        {
            // Normalize name and check validity
            string normalizedName = Normalize(name);
            NameIsValid(normalizedName);
            // Return an empty string for empty cells
            if (!nonemptyCells.ContainsKey(normalizedName))
            {
                return "";
            }
            return nonemptyCells[normalizedName].Contents;
        }

        /// <inheritdoc />
        public override object GetCellValue(string name)
        {
            // Normalize name and check validity
            string normalizedName = Normalize(name);
            NameIsValid(normalizedName);
            if (nonemptyCells.ContainsKey(normalizedName))
            {
                return nonemptyCells[normalizedName].Value;
            }
            // Return an empty string for empty cells
            return "";
        }

        /// <inheritdoc />
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            return new HashSet<string>(nonemptyCells.Keys);
        }

        /// <inheritdoc />
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            // Normalize name and check validity
            string normalizedName = Normalize(name);
            NameIsValid(normalizedName);
            // Variable to hold double that could be encoded by content
            double value;
            // Mark that the Spreadsheet was changed, this will be reset in the 
            // string SetContentsOfCell method if an empty string is input as the
            // contents for an empty cell.
            sheetChanged = true;
            List<string> cellsToUpdate;
            // Determine what type of object is encoded in content and call the correct
            // SetCellContents method to process it
            if (double.TryParse(content, out value))
            {
                cellsToUpdate = new List<string>(SetCellContents(normalizedName, value));
            }
            // The content if text if the first character is not a '=' which would indicate
            // a formula
            else if (content == "" || content[0] != '=')
            {
                cellsToUpdate = new List<string>(SetCellContents(normalizedName, content));
            }
            else
            {
                // Substring(1) returns all characters after the start character ('=' here). Pass the 
                // validator and normalizer of this Spreadsheet to the formula constructor to create
                // the Formula object
                cellsToUpdate = new List<string>(SetCellContents(normalizedName, new Formula(content.Substring(1), Normalize, IsValid)));
            }
            // Call helper method to update values of cells
            UpdateCellValues(cellsToUpdate);
            return cellsToUpdate;
        }

        /// <summary>
        /// Helper method to update the values of cells dependent on the cell whose contents
        /// were changed including the cell itself. Sets the Value field of the cell object.
        /// </summary>
        /// <param name="cellsToUpdate">The list containing the cell whose contents changed and
        /// the cells directly or indirectly dependent on that cell.</param>
        private void UpdateCellValues(List<string> cellsToUpdate)
        {
            foreach (string cell in cellsToUpdate)
            {
                // If statement needed for cases where a nonempty cell's contents
                // were set to an empty string (cell removed from dictionary but is
                // in cellsToUpdate list)
                if (nonemptyCells.ContainsKey(cell))
                {
                    nonemptyCells[cell].Value = ComputeCellValue(cell);
                }
            }
        }

        /// <summary>
        /// Helper method to compute the value of a cell from its contents.
        /// </summary>
        /// <param name="normalizedName">The normalized name of the cell</param>
        /// <returns>The cell's value (string, double, or FormulaError)</returns>
        private object ComputeCellValue(string normalizedName)
        {
            if (nonemptyCells[normalizedName].Contents is string || nonemptyCells[normalizedName].Contents is double)
            {
                return nonemptyCells[normalizedName].Contents;
            }
            // If the contents of a cell is a Formula, call the formula evaluate method with lookup delegate
            // defined below
            else
            {
                Formula formula = (Formula)nonemptyCells[normalizedName].Contents;
                return formula.Evaluate(str => GetVariable(str));
            }
        }

        /// <summary>
        /// Method to be used as the lookup delegate for the Formula Evaluate method. Returns values for
        /// variables contained in formulas. If any ArgumentException is thrown, the value of the cell 
        /// containing the original formula will be set to a FormulaError.
        /// </summary>
        /// <param name="varName">The name of the variable (cell)</param>
        /// <returns>The value stored in the cell with the variable name</returns>
        /// <exception cref="ArgumentException">If the variable is not defined or points to a cell
        /// that contains a string</exception>
        private double GetVariable(string varName)
        {
            // Throw exception for variables that point to empty cells
            if (!nonemptyCells.ContainsKey(varName))
            {
                throw new ArgumentException("The variable is not defined");
            }
            object value = nonemptyCells[varName].Value;
            // Throw exception for variables pointing to cells containing strings or FormulaErrors as values
            if (value is string)
            {
                throw new ArgumentException("The specified cell contains a string");
            }
            else if (value is FormulaError)
            {
                throw new ArgumentException("The specified cell contains a FormulaError");
            }
            else
            {
                return (double)value;
            }
        }

        /// <inheritdoc/>
        protected override IList<string> SetCellContents(string name, double number)
        {
            // Call helper method to update cell dependencies
            UpdateDependencyGraphStringDouble(name);
            // Use extension to update the nonempty cell dictionary
            nonemptyCells.SetCell(name, new Cell(name, number));
            // Call helper method to get the list of cells whose values must be updated
            List<string> outputList = new List<string>(GetCellsToRecalculate(name));
            return outputList;
        }

        /// <inheritdoc/>
        protected override IList<string> SetCellContents(string name, string text)
        {
            // Call helper method to update cell dependencies
            UpdateDependencyGraphStringDouble(name);
            // Call helper method to get the list of cells whose values must be updated
            List<string> outputList = new List<string>(GetCellsToRecalculate(name));
            // Remove nonempty cells from dictionary if the new content is an empty string
            if (text == "" && nonemptyCells.ContainsKey(name))
            {
                nonemptyCells.Remove(name);
            }
            // If the new content for an empty cell is an empty string, mark sheet as unchanged
            else if (text == "" && !nonemptyCells.ContainsKey(name))
            {
                sheetChanged = false;
            }
            // Use extension to update dictionary of nonempty cells
            else
            {
                nonemptyCells.SetCell(name, new Cell(name, text));
            }
            return outputList;
        }

        /// <summary>
        /// Helper method used to update the cell dependency graph calls to SetCellContents with
        /// a string or double as the content.
        /// </summary>
        /// <param name="name">The cell whose dependency relations are updated</param>
        private void UpdateDependencyGraphStringDouble(string name)
        {
            // Only the dependees of a cell are known from that cell's contents. ReplaceDependees also
            // removes the cell as a dependent of the old dependees.
            cellDependencyGraph.ReplaceDependees(name, new List<string>());
        }

        /// <inheritdoc/>
        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            // Keep a set of old dependees in case the new formula results in a circular expression and
            // dependencies need to be reset
            HashSet<string> oldDependees = new HashSet<string>(cellDependencyGraph.GetDependees(name));
            // The following try-catch block ensures that the spreadsheet will only update if there
            // is no CircularException thrown by GetCellsToRecalculate
            try
            {
                // Call a helper method to update dependency relationships
                UpdateDependencyGraphFormula(name, formula);
                // Call method from AbstractSpreadsheet to get list of cells to be recalculated
                List<string> outputList = new List<string>(GetCellsToRecalculate(name));
                // Use an extension to add the cell to the dictionary of filled cells or
                // update its value
                nonemptyCells.SetCell(name, new Cell(name, formula));
                return outputList;
            }
            catch (CircularException)
            {
                // Reset the dependency graph, mark sheet as unchanged, and ensure method throws an exception
                // as specified by the API
                cellDependencyGraph.ReplaceDependees(name, oldDependees);
                sheetChanged = false;
                throw new CircularException();
            }
        }


        /// <summary>
        /// Helper method used to update the cell dependency graph when a Formula is set
        /// as a cell's contents.
        /// </summary>
        /// <param name="name">The cell name</param>
        /// <param name="formula">The Formula object contained in the cell</param>
        private void UpdateDependencyGraphFormula(string name, Formula formula)
        {
            // Only the dependees of a cell are known from that cell's contents. The new dependees are
            // the variables contained in the Formula. ReplaceDependees also removes the cell as a
            // dependent of the old dependees.
            List<string> newDependeees = new List<string>(formula.GetVariables());
            cellDependencyGraph.ReplaceDependees(name, newDependeees);
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            string normalizedName = Normalize(name);
            // GetDependents in DependencyGraph returns a HashSet of a
            // cell's direct dependents
            return cellDependencyGraph.GetDependents(normalizedName);
        }

        /// <inheritdoc/>
        public override string GetSavedVersion(string filename)
        {
            // Call helper method to try to read the file. Catch any exception and throw an
            // exception from this method
            try
            {
                return ReadXMLFile(filename);
            } 
            // Try to be as specific as possible about the source of the error
            catch (FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("The file was not found");
            } catch (CircularException)
            {
                throw new SpreadsheetReadWriteException("The spreadsheet contains a circular dependency");
            } catch (InvalidNameException)
            {
                throw new SpreadsheetReadWriteException("The spreadsheet contains an invalid name");
            } catch (FormulaFormatException)
            {
                throw new SpreadsheetReadWriteException("The spreadsheet contains an invalid formula");
            } catch (Exception)
            {
                throw new SpreadsheetReadWriteException("The file was found but could not be read");
            }
            
        }

        /// <summary>
        /// Helper method used to read Spreadsheet data from a file in XML format. When this method is used in the 
        /// four argument constructor, the version of the file must match the version specified in the constructor.
        /// </summary>
        /// <param name="filename">The path to the file</param>
        /// <returns>The version of the file</returns>
        /// <exception cref="SpreadsheetReadWriteException">Throws exception if there are any errors in reading 
        /// the file</exception>
        private string ReadXMLFile(string filename)
        {
            // Make variables to hold file version, and the name and contents of any cell data contained. The
            // version must be declared nullable
            string? readVersion = "";
            string cellName = "";
            string cellContents = "";
            // Ensure that the file is closed upon completion
            using (XmlReader xmlReader = XmlReader.Create(filename))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name)
                        {
                            // When the start of the spreadsheet is found, read version and throw exception if the version does
                            // not match the version specified in the constructor
                            case "spreadsheet":
                                readVersion = xmlReader.GetAttribute("version");
                                break;

                            // Do nothing when the start of a cell is found
                            case "cell":
                                break;

                            // Set cellName variable to identified cell name
                            case "name":
                                xmlReader.Read();
                                cellName = xmlReader.Value;
                                break;

                            // Set cellContents variable to identified cell contents
                            case "contents":
                                xmlReader.Read();
                                cellContents = xmlReader.Value;
                                break;

                        }
                    }
                    // If an end element is reached for a cell, use SetContentsOfCell to add
                    // the cell data to the Spreadsheet
                    else
                    {
                        if (xmlReader.Name.Equals("cell"))
                            this.SetContentsOfCell(Normalize(cellName), cellContents);
                    }
                }
            }
            if (readVersion != null)
            {
                return readVersion;
            }
            else
            {
                return "";
            }
        }

        /// <inheritdoc/>
        public override void Save(string filename)
        {
            // Call helper method to try to save data to file and catch any exception
            try
            {
                WriteXmlFile(filename);
            }
            // Try to be as specific as possible about the source of the error
            catch (DirectoryNotFoundException)
            {
                throw new SpreadsheetReadWriteException("The specified directory was not found.");
            }
            catch (DriveNotFoundException)
            {
                throw new SpreadsheetReadWriteException("The drive that is being saved to was not found.");
            }
            catch (PathTooLongException)
            {
                throw new SpreadsheetReadWriteException("The specified path is longer than the maximum allowed by the system.");
            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("The filepath is valid, but there was an issue saving the file.");
            }
            sheetChanged = false;
        }

        /// <summary>
        /// Helper method to write Spreadsheet data to file in XML format.
        /// </summary>
        /// <param name="filename">The filepath where the file will be saved</param>
        private void WriteXmlFile(string filename)
        {
            // Adjust settings so that elements are indented to improve readability
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "   ";
            // Ensure file is closed upon completion
            using (XmlWriter xmlWriter = XmlWriter.Create(filename, settings))
            {
                // Write the start to identify the file as a Spreadsheet and give the version id
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("spreadsheet");
                xmlWriter.WriteAttributeString("version", Version);
                // Call method in Cell to write cell data
                foreach (string cellName in nonemptyCells.Keys)
                {
                    Cell cell = nonemptyCells[cellName];
                    cell.WriteXmlCell(xmlWriter);
                }
                // End the spreadsheet block and the document
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }
    }
}

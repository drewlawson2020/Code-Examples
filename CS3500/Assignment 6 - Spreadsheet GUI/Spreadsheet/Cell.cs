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
/// Contains a class to represent a Cell in a Spreadsheet.
/// 
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SpreadsheetUtilities;

namespace SS
{
    /// <summary>
    /// Contains a class to represent a cell in a spreadsheet. Cell names begin with a letter or underscore
    /// followed by zero or more letters, digits, or underscores. The cell contents can be any object.
    /// </summary>
    public class Cell
    {
        // Make fields for cell name and content
        private string name;
        private object contents;

        /// <summary>
        /// Constructor method for a new cell.
        /// </summary>
        /// <param name="inputName">The cell's name</param>
        /// <param name="inputContents">The cell's contents</param>
        public Cell(string inputName, object inputContents)
        {
            name = inputName;
            contents = inputContents;
            // Set cell's value to contents initially, it will be updated
            // by Spreadsheet before use
            Value = contents;
        }

        /// <summary>
        /// Returns the cell's name
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Returns the cell's contents
        /// </summary>
        public object Contents
        {
            get { return contents; }
        }

        /// <summary>
        /// Gets and sets the cell's value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Method used to write cell data in XML format. Used by the Spreadsheet class
        /// when saving a spreadsheet to a file.
        /// </summary>
        /// <param name="xmlWriter">XML writer object being used by Spreadsheet to write the file</param>
        internal void WriteXmlCell(XmlWriter xmlWriter)
        {
            // Start the block for the cell and give its name
            xmlWriter.WriteStartElement("cell");
            xmlWriter.WriteElementString("name", name);
            // Write contents for formula with a '=' at the front
            if (contents is Formula)
            {
                xmlWriter.WriteElementString("contents", "=" + contents.ToString());
            } else
            {
                xmlWriter.WriteElementString("contents", contents.ToString());
            }
            // End cell block
            xmlWriter.WriteEndElement();
        }
    }
}

/// <summary>
/// Author: Drew Lawson and Peter Bruns
/// Team: EatSleepCode
/// Date: March 3, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500, Drew Lawson, and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// We, Drew Lawson and Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// Implementation of a spreadsheet GUI. Contains all essential methods needed to open file, save file, create new spreadsheet,
/// close file, set contents of cell, compute and display value of cell in textbox and on a grid, display cell name, change selected
/// cell on grid, and open README text files. Also contains methods to display direct cell dependencies and change selected cell with
/// arrow keys. Cells names start with a single letter A-Z followed by a number 1-99.
/// 
///</summary>

using SS;
using SpreadsheetUtilities;
using SpreadsheetGrid_Core;
using System.Text.RegularExpressions;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace GUI
{
    /// <summary>
    /// Implementation of a spreadsheet GUI. Contains all essential methods needed to open file, save file, create new spreadsheet,
    /// close file, set contents of cell, compute and display value of cell in textbox and on a grid, display cell name, change selected
    /// cell on grid, and open README text files. Also contains methods to display direct cell dependencies and change selected cell with
    /// arrow keys. Cells names start with a single letter A-Z followed by a number 1-99.
    /// </summary>
    public partial class SpreadsheetGUI : Form
    {

        private AbstractSpreadsheet sheet;
        private string fileName;

        /// <summary>
        /// Constructor for an individual spreadsheet GUI window.
        /// </summary>
        public SpreadsheetGUI()
        {
            // Provided validator and normalizer defined below
            sheet = new Spreadsheet(Validator, Normalizer, "six");
            // The next three lines were modified from the provided example
            this.gridWidget = new SpreadsheetGridWidget();
            InitializeComponent();
            gridWidget.SelectionChanged += ChangeSelectionWitClick;
            // Set initial cell to A1, file name to untitled, and start cursor in contents box
            cellNameBox.Text = "A1";
            fileName = "untitled";
            contentsBox.Select();
        }

        /// <summary>
        /// Converts all cell names to uppercase per requriement.
        /// </summary>
        /// <param name="name">Cell name</param>
        private string Normalizer(string name)
        {
            return name.ToUpper();
        }

        /// <summary>
        /// Ensures that entered cell names are valid. Valid cell names start with a single
        /// letter A-Z followed by a number 1-99.
        /// </summary>
        /// <param name="name">Cell name</param>
        /// <returns>True if cell name follows requried pattern, false otherwise</returns>
        private Boolean Validator(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z][1-9][0-9]?$", RegexOptions.Singleline);
        }

        /// <summary>
        /// Used to enable spreadsheet navigation with the arrow and enter keys when the grid is selected.
        /// Also moves the selection to the contents box when a letter, digit, or = is pressed and sets text
        /// in contents box to data from pressed key.
        /// </summary>
        /// <param name="keyData">The identity of the pressed key</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int row, col;
            gridWidget.GetSelection(out col, out row);
            // If the value box or contents bos are selected, do not change selected cell
            if (contentsBox.Focused || valueBox.Focused)
            {
                return false;
            }
            // Select cell above, below, to the left, or to the right when an arrow key or eneter
            // is pressed. Use helper method to change selection.
            else if (keyData == Keys.Down || keyData == Keys.Enter)
            {
                ChangeSelectionWithKey(gridWidget, col, row + 1);
                return true;
            }
            else if (keyData == Keys.Up)
            {
                ChangeSelectionWithKey(gridWidget, col, row - 1);
                return true;
            }
            else if (keyData == Keys.Left)
            {
                ChangeSelectionWithKey(gridWidget, col - 1, row);
                return true;
            }
            else if (keyData == Keys.Right)
            {
                ChangeSelectionWithKey(gridWidget, col + 1, row);
                return true;
            }
            // If shift or caps-lock are pressed, focus on contents box
            else if (keyData.ToString() == "Capital" || keyData.ToString() == "ShiftKey, Shift")
            {
                contentsBox.Focus();
            }
            // If "=" is pressed, set contents box text to = and select box for further editing
            else if (keyData.ToString() == "Oemplus")
            {
                ActivateContentsBox("=");
            }
            // If number is pressed, set contents box text to key data and select box for further editing
            else if (keyData.ToString().Length > 1 && Char.IsDigit(keyData.ToString()[1]))
            {
                ActivateContentsBox(keyData.ToString()[1].ToString());
            }
            // If number or letter is pressed, set contents box text to key data and select box for further editing
            else if (Char.IsLetter(keyData.ToString()[0]) && keyData.ToString().Length == 1)
            {
                ActivateContentsBox(keyData.ToString().ToLower());
            }
            return false;
        }

        /// <summary>
        /// Sets focus to contents box so text can be editied. Sets intitial box text to
        /// input string.
        /// </summary>
        /// <param name="input">The initial text for the contents box</param>
        private void ActivateContentsBox(string input)
        {
            // Sets contents box text to input and sets cursor to the end so that
            // further text may be entered
            // Modified from https://stackoverflow.com/questions/20423211/setting-cursor-at-the-end-of-any-text-of-a-textbox
            contentsBox.Text = input;
            contentsBox.SelectionStart = contentsBox.Text.Length;
            contentsBox.SelectionLength = 0;
            contentsBox.Focus();
        }

        /// <summary>
        /// Helper method to change the selected spreadsheet cell with a key press.
        /// </summary>
        /// <param name="spreadsheetWidget">The spreadsheet panel implementation</param>
        /// <param name="newColumn">The column of the newly selected cell</param>
        /// <param name="newRow">The row of the newly selected cell</param>
        private void ChangeSelectionWithKey(SpreadsheetGridWidget spreadsheetWidget, int newColumn, int newRow)
        {
            // Do not change selection if new selection would be outside range of valid cells
            if (newColumn < 0 || newColumn > 25 || newRow < 0 || newRow > 98)
            {
                return;
            }
            // Change selected cell and update the text box display
            spreadsheetWidget.SetSelection(newColumn, newRow);
            UpdateTextBoxDisplay(newColumn, newRow);
        }

        /// <summary>
        /// Method used to change selected cell with a mouse click on the spreadsheet panel.
        /// </summary>
        /// <param name="spreadsheetWidget">The spreadsheet panel implementation</param>
        private void ChangeSelectionWitClick(SpreadsheetGridWidget spreadsheetWidget)
        {
            int row, col;
            // Get the newly selected cell's coordinates and call helper to update
            // text box display
            spreadsheetWidget.GetSelection(out col, out row);
            UpdateTextBoxDisplay(col, row);
        }

        /// <summary>
        /// Updates the text displayed in the cell name, cell contents, and cell
        /// value boxes to the data associated with the cell at the input coordinates.
        /// </summary>
        /// <param name="column">Input cell column</param>
        /// <param name="row">Input cell row</param>
        private void UpdateTextBoxDisplay(int column, int row)
        {
            // Convert column to char value for cell name letter
            char columnLetter = (char)(column + 65);
            // Cell name rows are 1 indexed, row in input is 0 indexed so add 1
            int displayRow = row + 1;
            // Get cell name and set cell name box text
            String cellName = columnLetter + displayRow.ToString();
            cellNameBox.Text = cellName;
            // If the cell contains a formula, add a = to the string displayed in
            // cell contents box
            if (sheet.GetCellContents(cellName) is Formula)
            {
                contentsBox.Text = "=" + sheet.GetCellContents(cellName).ToString();
            }
            else
            {
                contentsBox.Text = sheet.GetCellContents(cellName).ToString();
            }
            // Call helper to update value displayed in spreadsheet grid
            UpdateGridValueDisplay(sheet.GetCellValue(cellName), cellName);
        }

        /// <summary>
        /// Method used to update value displayed in many cells in the spreadsheet
        /// grid panel. Method contains Invoke statements because it is called by
        /// parallel threads started by GUI.
        /// </summary>
        /// <param name="cellNames">Names of cells whose grid display is updated</param>
        private void UpdateManyCellGridValues(IList<string> cellNames)
        {
            // Disable contents box while grid display is being updated
            Invoke(() => contentsBox.Enabled = false);
            // Call helper to update display for each cell in input list
            foreach (string cellName in cellNames)
            {
                UpdateGridValueDisplay(sheet.GetCellValue(cellName), cellName);
            }
            // Enable contents box text editing
            Invoke(() => contentsBox.Enabled = true);
        }

        /// <summary>
        /// Method used to udate the value displayed in a specified cell in the spreadsheet grid.
        /// Contains Invoke statements because the method is sometimes called from parallel threads
        /// initiated by GUI.
        /// </summary>
        /// <param name="cellValue">Value of cell stored in spreadsheet</param>
        /// <param name="cellName">Name of cell whose value will be updated</param>
        private void UpdateGridValueDisplay(object cellValue, string cellName)
        {
            int col, row;
            gridWidget.GetSelection(out col, out row);
            // Get the letter of the cell from its column value
            char cellLetter = (char)(col + 65);
            // Get the value associated with the cell in the backing spreadsheet
            object displayCellValue = sheet.GetCellValue(cellLetter + (row + 1).ToString());
            // Set display in value text box to currently selected cell
            // If value is FormulaError, show error reason in text box, otherwise
            // show string representation of value
            if (displayCellValue is FormulaError)
            {
                FormulaError formulaError = (FormulaError)displayCellValue;
                Invoke(() => valueBox.Text = formulaError.Reason);
            }
            else
            {
                Invoke(() => valueBox.Text = displayCellValue.ToString());
            }
            // Get coordinates of cell to update on the grid display
            int cellCol = (int)cellName[0] - 65;
            int cellRow = Convert.ToInt32(cellName[1..]) - 1;
            // If the cell value is a FormulaError, show Error in grid. Otherwise show
            // string representation of cell value
            if (cellValue is FormulaError)
            {
                FormulaError formulaError = (FormulaError)cellValue;
                Invoke(() => gridWidget.SetValue(cellCol, cellRow, "Error"));
            }
            else
            {
                Invoke(() => gridWidget.SetValue(cellCol, cellRow, cellValue.ToString()));
            }
        }

        /// <summary>
        /// Processes key-up action when the contents box is selected. When enter key is pressed, sets
        /// contents of selected cell in backing spreadsheet, updates value and contents text boxes, updates
        /// grid value display, and selects cell below currently selected cell.
        /// </summary>
        private void contentsBox_KeyUp(object sender, KeyEventArgs e)
        {
            int row, col;
            // Get the string name of the cell from coordinates
            gridWidget.GetSelection(out col, out row);
            char columnLetter = (char)(col + 65);
            int cellRow = row + 1;
            String cellName = columnLetter + cellRow.ToString();
            // When enter key is pressed, set contents of selected cell in backing spreadsheet
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    IList<string> cellsToUpdate = sheet.SetContentsOfCell(cellName, contentsBox.Text);
                    // If the cell contains a formula, add a = to the cell contents box text. Otherwise
                    // show string representation of contents
                    if (sheet.GetCellContents(cellName) is Formula)
                    {
                        contentsBox.Text = "=" + sheet.GetCellContents(cellName).ToString();
                    }
                    else
                    {
                        contentsBox.Text = sheet.GetCellContents(cellName).ToString();
                    }
                    // Update value text box and use a parallel thread to update the values displayed in
                    // the spreadsheet panel.
                    valueBox.Text = sheet.GetCellValue(cellName).ToString();
                    // Adapted from https://stackoverflow.com/questions/3360555/how-to-pass-parameters-to-threadstart-method-in-thread
                    Thread manyUpdates = new Thread(() => UpdateManyCellGridValues(cellsToUpdate));
                    manyUpdates.Start();
                    // Move selection down one row
                    ChangeSelectionWithKey(gridWidget, col, cellRow);
                }
                // Do not update spreadsheet if changing cell contents would result in an exception
                catch (CircularException)
                {
                    MessageBox.Show("The entered formula resulted in a CircularException. Click OK to reenter.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The entered formula resulted in an exception. Click OK to reenter.");
                }
            }
        }
        /// <summary>
        /// Moves cell selection to next row down when enter pressed if value box is selected.
        /// </summary>
        private void valueBox_KeyUp(object sender, KeyEventArgs e)
        {
            int row, col;
            gridWidget.GetSelection(out col, out row);
            // Get name of new cell from coordinates, then call helper to change cell selection
            char columnLetter = (char)(col + 65);
            int newRow = row + 1;
            if (e.KeyCode == Keys.Enter)
            {
                ChangeSelectionWithKey(gridWidget, col, newRow);
            }
        }

        /// <summary>
        /// Saves spreadsheet in XML format when Save button pressed on dropdown menu.
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make a new SaveFileDialog, set OverwritePrompt to false to allow for
            // custom behavior when potentially overwriting file
            SaveFileDialog fileSaver = new SaveFileDialog();
            fileSaver.OverwritePrompt = false;
            fileSaver.FileName = fileName;
            // Set possible extensions to .sprd or any other file type collectively, set default
            // extension to .sprd
            fileSaver.Filter = "Spreadsheet (*.sprd)|*.sprd|All files (*.*)|*.*";
            fileSaver.DefaultExt = "sprd";
            if (fileSaver.ShowDialog() == DialogResult.OK)
            {
                // Display warning if saving would overwrite a file of a different name. Overwrite if OK
                // selected, show dialog box again if cancel selected
                while (File.Exists(fileSaver.FileName) && fileSaver.FileName != fileName)
                {
                    DialogResult warning = MessageBox.Show("Are you sure you want to overwrite " + fileSaver.FileName + "?",
                        "File overwrite warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (warning == DialogResult.Yes)
                    {
                        break;
                    }
                    else
                    {
                        saveToolStripMenuItem_Click(sender, e);
                        // Return to prevent any further actions from this call
                        return;
                    }
                }
                // Try to save file and display any resulting error messages. Change filename
                try
                {
                    sheet.Save(fileSaver.FileName);
                    fileName = fileSaver.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            // Exit dialog if any exit button or "Cancel" is selected
            else
            {
                return;
            }

        }

        /// <summary>
        /// Opens a saved spreadsheet and replaces displayed contents with loaded save data.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If there are unsaved changes in the current spreadsheet, display warning message before saving.
            // Continue to save option if "No" selected on message box.
            if (sheet.Changed)
            {
                DialogResult warning = MessageBox.Show("There are unsaved changes to the Spreadsheet. Are you sure you want to load a file?",
                    "There are unsaved changes to the Spreadsheet", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (warning == DialogResult.No)
                {
                    return;
                }
            }
            // Create new OpenFileDialog and allow saving with .sprd or any other extension. Set .sprd to
            // default extension type.
            OpenFileDialog fileLoader = new OpenFileDialog();
            fileLoader.Filter = "Spreadsheet (*.sprd)|*.sprd|All files (*.*)|*.*";
            fileLoader.DefaultExt = "sprd";
            if (fileLoader.ShowDialog() == DialogResult.OK)
            {
                // Try to save file, catch any exception and display error message in box
                try
                {
                    // Make a new backing spreadsheet from data saved at file path
                    string filePath = fileLoader.FileName;
                    sheet = new Spreadsheet(filePath, Validator, Normalizer, "six");
                    // Clear all current data from GUI
                    gridWidget.Clear();
                    valueBox.Clear();
                    contentsBox.Clear();
                    // Disable dependency display updates while file loads
                    dependencyButton.Enabled = false;
                    IList<string> nonEmptyCells = new List<string>(sheet.GetNamesOfAllNonemptyCells());
                    // Use a thread to update all cell display values
                    // Adapted from https://stackoverflow.com/questions/3360555/how-to-pass-parameters-to-threadstart-method-in-thread
                    Thread openFileThread = new Thread(() => UpdateManyCellGridValues(nonEmptyCells));
                    openFileThread.Start();
                    dependencyButton.Enabled = true;
                    // Set cell selection to A1 and set value/contents boxes to data stored in A1
                    gridWidget.SetSelection(0, 0);
                    if (sheet.GetCellContents("A1") is Formula)
                    {
                        contentsBox.Text = "=" + sheet.GetCellContents("A1").ToString();
                    }
                    else
                    {
                        contentsBox.Text = sheet.GetCellContents("A1").ToString();
                    }
                    UpdateGridValueDisplay(sheet.GetCellValue("A1"), "A1");
                    // Change filename to name of loaded file
                    fileName = fileLoader.FileName;
                    // Clear any existing data in grid of dependencies
                    dependencyGrid.Rows.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Closes the currently selected spreadsheet window with close button on drop down. Shows warning 
        /// if closing would lead to loss of unsaved data.
        /// </summary>
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If there are unsaved changes to the spreadsheet, show warning before exiting GUI. Do not exit if "No" selected
            if (sheet.Changed)
            {
                DialogResult warning = MessageBox.Show("There are unsaved changes to the Spreadsheet. Are you sure you want to exit?",
                    "There are unsaved changes to the Spreadsheet", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (warning == DialogResult.No)
                {
                    return;
                }
            }
            Close();
        }

        /// <summary>
        /// Closes the currently selected spreadsheet window with red X button on window. Shows warning 
        /// if closing would lead to loss of unsaved data.
        /// </summary>
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (sheet.Changed)
                {
                    DialogResult warning = MessageBox.Show("There are unsaved changes to the Spreadsheet. Are you sure you want to exit?",
                        "There are unsaved changes to the Spreadsheet", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (warning == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// Opens new blank spreadsheet when the new button is selected from dropdown menu. The new
        /// window will be independent of the old window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Use SpreadsheetWindow program to start a new application context
            SpreadsheetWindow.getAppContext().RunForm(new SpreadsheetGUI());
        }

        /// <summary>
        /// Sets GridDataView to update and show current spreadsheet dependencies when button
        /// clicked.
        /// </summary>
        private void dependencyButton_Click(object sender, EventArgs e)
        {
            // Disable contents box so that spreadsheet can't be changed while dependencies found
            contentsBox.Enabled = false;
            // Clear any existing data in grid
            dependencyGrid.Rows.Clear();
            foreach (string cellName in sheet.GetNamesOfAllNonemptyCells())
            {
                // If a cell contains a formula, get any variables it contains and add to a string
                // representation of formula
                if (sheet.GetCellContents(cellName) is Formula)
                {
                    Formula formula = (Formula)sheet.GetCellContents(cellName);
                    string varNames = "";
                    List<string> varNamesList = new List<string>(formula.GetVariables());
                    // If the formula does not contain any variables, do not add cell to grid
                    if (varNamesList.Count == 0)
                    {
                        continue;
                    }
                    // Build string of comma separated cell names from variables then add to dependees
                    // column of grid view
                    for (int i = 0; i < varNamesList.Count - 1; i++)
                    {
                        varNames += varNamesList[i] + ", ";
                    }
                    varNames += varNamesList[varNamesList.Count - 1];
                    dependencyGrid.Rows.Add(cellName, varNames);
                }
            }
            // Re-enable contents box
            contentsBox.Enabled = true;
        }

        /// <summary>
        /// Opens the project README when README option selected from help dropdown menu
        /// </summary>
        private void READMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the directory path to file location
            string directory = GetReadmeDirectory();
            // Call helper to open file
            ShowREADME(directory, "README.md");
        }

        /// <summary>
        /// Opens the show dependencies README when README option selected from help dropdown menu
        /// </summary>
        private void displayDependenciesStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the directory path to file location
            string directory = GetReadmeDirectory();
            // Call helper to open file
            ShowREADME(directory, "ShowDependenciesREADME.md");
        }

        /// <summary>
        /// Opens the key navigation README when README option selected from help dropdown menu
        /// </summary>
        private void keyNavigationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the directory path to file location
            string directory = GetReadmeDirectory();
            // Call helper to open file
            ShowREADME(directory, "KeyNavigationREADME.md");
        }

        /// <summary>
        /// Helper method to display README .md files.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="file"></param>
        private void ShowREADME(string directory, string file)
        {
            // Open .md file with default program or ask for the program to use to open file
            // From https://stackoverflow.com/questions/60018792/how-to-open-a-textfile-with-the-default-editor-in-net-core
            new Process
            {
                StartInfo = new ProcessStartInfo(directory + file)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        /// <summary>
        /// Helper method used to get the current file directory path
        /// </summary>
        /// <returns></returns>
        private string GetReadmeDirectory()
        {
            // Adapted from https://stackoverflow.com/questions/14899422/how-to-navigate-a-few-folders-up
            string applicationDirectory = Path.Combine(Directory.GetCurrentDirectory());
            return Path.GetFullPath(Path.Combine(applicationDirectory, "..\\..\\..\\"));
        }

    }
}
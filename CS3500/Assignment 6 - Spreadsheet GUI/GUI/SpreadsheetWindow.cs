/// <summary>
///  Author:     Joe Zachary
///  Updated by: Jim de St. Germain
///  
///  Dates:      (Original) 2012-ish
///              (Updated for Core) 2020
///              
///  Target: ASP CORE 3.1
///  
///  This program is an example of how to create a GUI application for 
///  a spreadsheet project.
///  
///  It relies on a working Spreadsheet Panel class, but defines other
///  GUI elements, such as the file menu (open and close operations).
///  
///  WARNING: with the current GUI designer, you will not be able to
///           use the toolbox "Drag and Drop" to update this file.
/// </summary>

using System;
using System.Windows.Forms;

namespace GUI
{
    /// <summary>
    /// Class to represent one or more spreadsheet GUI windows. Allows
    /// for opening multiple windows at a time and thread termination
    /// when the last window is closed.
    /// </summary>
    class SpreadsheetWindow : ApplicationContext
    {
        /// <summary>
        ///  Number of open forms
        /// </summary>
        private int formCount = 0;

        /// <summary>
        ///  ApplicationContext for an individual spreadsheet window.
        /// </summary>
        private static SpreadsheetWindow? appContext;

        /// <summary>
        /// Returns the currently selected spreadsheet window.
        /// </summary>
        public static SpreadsheetWindow getAppContext()
        {
            if (appContext == null)
            {
                appContext = new SpreadsheetWindow();
            }
            return appContext;
        }

        /// <summary>
        /// Private constructor for a single window
        /// </summary>
        private SpreadsheetWindow()
        {
        }

        /// <summary>
        /// Adds another window to the thread that is running the spreadsheet GUIs.
        /// </summary>
        public void RunForm(Form form)
        {
            // Increase number of running windows
            formCount++;

            // Assigns an EVENT handler to take an action when the GUI is closed.
            // Decreases number of windows and ends thread if no windows remain.
            form.FormClosed += (o, e) => { if (--formCount <= 0) ExitThread(); };

            // Run the form
            form.Show();
        }

    }

    /// <summary>
    /// Initializes spreadsheet GUI thread.
    /// </summary>
    class GUI_Application
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Start an application context and run one form inside it
            SpreadsheetWindow appContext = SpreadsheetWindow.getAppContext();
            appContext.RunForm(new SpreadsheetGUI());
            Application.Run(appContext);
        }
    }
}
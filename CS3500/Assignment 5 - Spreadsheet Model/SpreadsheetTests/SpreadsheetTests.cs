using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;

namespace SpreadsheetTests
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
    ///    This file most of the necessary methods and functions to test the newly updated AS5 specifications. It contains
    ///    some old PS4 tests updated for the new code, as well as new tests to test the new methods, and the save/load features.
    /// </summary>
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidName1()
        {
            Spreadsheet s = new Spreadsheet();
            object value = s.GetCellValue("@#&^#$^");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidName2()
        {
            Spreadsheet s = new Spreadsheet();
            object value = s.GetCellValue("7323#*@^$");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidName3()
        {
            Spreadsheet sheet = new Spreadsheet();
            object value = sheet.GetCellValue("(*)()(@#&");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameInSpreadsheetCell1()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("3612388231", "failpls");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameInSpreadsheetCell2()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("#$@%^#$", "failpls2");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameInSpreadsheetCell3()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("234#$123@!%", "failpls3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void BadFormulaTest1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A4", "=#@$@$#^3)");
        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void BadFormulaTest2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B2", "=#*630276");
        }
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void BadFormulaTest3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("C6", "=@#&@&)$(");
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularExceptionTest1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("U10", "=U10");
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularExceptionTest2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("U10", "=U11");
            sheet.SetContentsOfCell("U11", "=U10");
        }

        [TestMethod]
        public void SetContentsOfCellTest1()
        {
            Spreadsheet sheet = new Spreadsheet(s => false, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("D2", "12");
            var value = sheet.GetCellValue("D2");
            Assert.AreEqual(12.0, value);
        }
        [TestMethod]
        public void SetContentsOfCellTest2()
        {
            Spreadsheet sheet = new Spreadsheet(s => false, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("E4", "9");
            var value = sheet.GetCellValue("E4");
            Assert.AreEqual(9.0, value);
        }
        [TestMethod]
        public void SetContentsOfCellTest3()
        {
            Spreadsheet sheet = new Spreadsheet(s => false, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("T4", "120");
            var value = sheet.GetCellValue("T4");
            Assert.AreEqual(120.0, value);
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveToBlankError()
        {
            Spreadsheet sheet = new Spreadsheet(s => true, s => s, "1.1");
            sheet.SetContentsOfCell("E1", "1");
            sheet.SetContentsOfCell("G5", "1");
            sheet.SetContentsOfCell("H10", "3");
            sheet.Save("");
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void LoadNonExistentFile()
        {
            Spreadsheet sheet = new Spreadsheet();
            string version = sheet.GetSavedVersion("thispathdoesnotexist");
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void CreateConstructorWithNoFile()
        {
            Spreadsheet sheet1 = new Spreadsheet("thissuckslol", s => true, s => s, ".5");
        }

        [TestMethod]
        public void TestGetCell1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("D3", "hi");
            object value = sheet.GetCellValue("D3");
            Assert.AreEqual("hi", value);
        }
        [TestMethod]
        public void TestGetCell2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("G7", "yo");
            object value = sheet.GetCellValue("G7");
            Assert.AreEqual("yo", value);
        }
        [TestMethod]
        public void TestGetCell3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("H78", "hiya");
            object value = sheet.GetCellValue("H78");
            Assert.AreEqual("hiya", value);
        }

        [TestMethod]
        public void TestGetCell4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("F5", "2534");
            object value = sheet.GetCellValue("F5");
            Assert.AreEqual(2534.0, value);
        }

        [TestMethod]
        public void TestSetContentsAndGet()
        {
            Spreadsheet sheet = new Spreadsheet(s => true, s => s, "default");
            sheet.SetContentsOfCell("E1", "10");
            sheet.SetContentsOfCell("G1", "23");
            sheet.SetContentsOfCell("D7", "400");
            sheet.SetContentsOfCell("H3", "=D7 + E1");
            sheet.SetContentsOfCell("C70", "=G1");
            sheet.SetContentsOfCell("A1", "=E1 + G1");
            object value = sheet.GetCellValue("A1");
            Assert.AreEqual(33.0, value);
        }


        [TestMethod]
        public void TestSaveMethodAndChanged()
        {
            Spreadsheet sheetSaved = new Spreadsheet(s => true, s => s, "1.0");
            sheetSaved.SetContentsOfCell("A1", "1");
            sheetSaved.SetContentsOfCell("A2", "1");
            sheetSaved.SetContentsOfCell("A3", "3");
            sheetSaved.SetContentsOfCell("A4", "=A1 + A2");
            sheetSaved.Save("test.xml");
            Assert.AreEqual(sheetSaved.Changed, false);
            Spreadsheet sheetLoaded = new Spreadsheet("test.xml", s => true, s => s, "1.0");
            Assert.AreEqual(sheetLoaded.Changed, false);
            HashSet<string> names1 = new HashSet<string>(sheetSaved.GetNamesOfAllNonemptyCells());
            HashSet<string> names2 = new HashSet<string>(sheetLoaded.GetNamesOfAllNonemptyCells());

            foreach (string cell_name in names1)
            {
                Assert.AreEqual(sheetSaved.GetCellContents(cell_name), sheetLoaded.GetCellContents(cell_name));
            }
            foreach (string cell_name in names2)
            {
                Assert.AreEqual(sheetSaved.GetCellContents(cell_name), sheetLoaded.GetCellContents(cell_name));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError2()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetContentsOfCell("901AX", "10.0");
        }


        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError4()
        {
            Spreadsheet test = new Spreadsheet();
            test.GetCellContents("B1C");
        }


        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError7()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetContentsOfCell("10000BC", "cavemen");
        }

        [TestMethod()]
        public void TestEmptyCell()
        {
            Spreadsheet test = new Spreadsheet();
            Assert.AreEqual("", test.GetCellContents("F2"));
        }
        [TestMethod()]
        public void TestNonExistentCell()
        {
            Spreadsheet test = new Spreadsheet();
            Assert.IsFalse(test.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }
    }
}
/// <summary>
/// Author: Peter Bruns
/// Partner: None
/// Date: February 18, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// I, Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file. All
/// tests from old grading test suites are identified as such.
/// 
/// File Contents
/// 
/// This file contains tests for the public methods in the spreadsheet class.
/// 
///</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System.Linq;
using SS;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace SpreadSheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        /***** Test constructors *****/
        // The no argument constructer is implicitly tested in many of the remaining tests

        /// <summary>
        /// Test version of spreadsheet made with no argument constructor
        /// </summary>
        [TestMethod]
        public void TestNoArgumentConstructorVersion()
        {
            Form1 sheet = new Form1();
            Assert.AreEqual("default", sheet.Version);
        }

        /// <summary>
        /// Test version of spreadsheet made with 3 argument constructor
        /// </summary>
        [TestMethod]
        public void TestThreeArgumentConstructorVersion()
        {
            Form1 sheet = new Form1(s => true, s => s, "test");
            Assert.AreEqual("test", sheet.Version);
        }

        /// <summary>
        /// Test 3 argument constructor with a validator throws exception for name
        /// it defines as invalid
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestThreeArgumentConstructorValidator()
        {
            Form1 sheet = new Form1(s => ValidatorTest(s), s => s, "test");
            sheet.SetContentsOfCell("a1", "3.0");
        }

        /// <summary>
        /// Test 3 argument constructor with a validator throws exception for name
        /// that validator accepts but Spreadsheet defines as invalid
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestThreeArgumentConstructorValidatorAgain()
        {
            Form1 sheet = new Form1(s => ValidatorTest(s), s => s, "test");
            // The validator accepts underscores, Spreadsheet does not
            sheet.SetContentsOfCell("A_", "3.0");
        }

        /// <summary>
        /// Test 3 argument constructor with a validator and normalizer does not throw
        /// exception if invalid name is normalized
        /// </summary>
        [TestMethod]
        public void TestThreeArgumentConstructorValidatorAndNormalizer()
        {
            // The normalizer will convert letters to uppercase, the validator requires
            // uppercase letters
            Form1 sheet = new Form1(s => ValidatorTest(s), s => s.ToUpperInvariant(), "test");
            // The validator accepts underscores, Spreadsheet does not
            sheet.SetContentsOfCell("a1", "3.0");
        }

        /// <summary>
        /// Test 3 argument constructor with a normalizer leads to methods returning normalized names
        /// for cells and accepts normalized names as inputs
        /// </summary>
        [TestMethod]
        public void TestThreeArgumentConstructorResults()
        {
            // The normalizer will convert letters to uppercase
            Form1 sheet = new Form1(s => true, s => s.ToUpperInvariant(), "test");
            Assert.IsTrue(new List<string>() { "A1" }.SequenceEqual(sheet.SetContentsOfCell("a1", "3.0")));
            Assert.AreEqual((double)3, sheet.GetCellValue("a1"));
            Assert.AreEqual((double)3, sheet.GetCellValue("A1"));
        }

        /// <summary>
        /// :Test that opening a file with a normalizer different from the one in 
        /// the original spreadsheet works
        /// </summary>
        [TestMethod]
        public void TestOpeningFileWithNormalizer()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("a1", "5");
            sheet.SetContentsOfCell("b1", "test");
            sheet.SetContentsOfCell("c1", "=1+2");
            sheet.Save("test1.txt");
            AbstractSpreadsheet ss = new Form1("test1.txt", s => true, s => s.ToUpperInvariant(), "");
            Assert.IsTrue(ss.GetCellContents("A1") is double);
            Assert.IsTrue(ss.GetCellContents("b1") is string);
            Assert.IsTrue(ss.GetCellContents("C1") is Formula);
            Assert.AreEqual((double)5, ss.GetCellValue("A1"));
            Assert.AreEqual("test", ss.GetCellValue("B1"));
            Assert.AreEqual((double)3, ss.GetCellValue("C1"));
        }

        /// <summary>
        /// Example validator for tests. This validator allows underscores following a letter but
        /// requires letters to be uppercase
        /// </summary>
        public Boolean ValidatorTest(string str)
        {
            Regex namePattern = new Regex((@"^[A-Z]+[0-9_]+$"));
            if (namePattern.IsMatch(str))
            {
                return true;
            }
            return false;
        }

        /***** Test Changed *****/

        /// <summary>
        /// Test that a new spreadsheet is not marked changed
        /// </summary>
        [TestMethod]
        public void TestChangedNewSheet()
        {
            Form1 sheet = new Form1();
            Assert.IsFalse(sheet.Changed);
        }

        /// <summary>
        /// Test that spreadsheet is marked as changed after additions
        /// </summary>
        [TestMethod]
        public void TestChangedAfterSetContentsOfCellCall()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("B1", "=A + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("C1", "=B + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("D1", "=B + C");
            Assert.IsTrue(sheet.Changed);
        }

        /// <summary>
        /// Test that spreadsheet is marked as not changed after an empty cell
        /// is set to an empty string
        /// </summary>
        [TestMethod]
        public void TestChangedEmptyCellSetToEmptyString()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("B1", "=A + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("C1", "=B + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("D1", "");
            Assert.IsFalse(sheet.Changed);
        }

        /// <summary>
        /// Test that spreadsheet is marked as changed after a nonempty cell
        /// is set to an empty string
        /// </summary>
        [TestMethod]
        public void TestChangedNonEmptyCellSetToEmptyString()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("B1", "=A + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("C1", "=B + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("C1", "");
            Assert.IsTrue(sheet.Changed);
        }

        /***** Test GetCellContents *****/

        /// <summary>
        /// Test that GetCellContents throws an exception for a name starting with a digit
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsThrowsExceptionNameStartsWithDigit()
        {
            Form1 sheet = new Form1();
            sheet.GetCellContents("1X");
        }


        /// <summary>
        /// Test that GetCellContents throws an exception for a name containing only letter
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsThrowsExceptionOnlyLetterName()
        {
            Form1 sheet = new Form1();
            sheet.GetCellContents("X");
        }

        /// <summary>
        /// Test that GetCellContents throws an exception for a name containing an invalid character
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsThrowsExceptionNameWithInvalidChar()
        {
            Form1 sheet = new Form1();
            sheet.GetCellContents("X1$");
        }

        /// <summary>
        /// Test that GetCellContents throws an exception for a name that is an empty string
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsThrowsExceptionEmptyNameString()
        {
            Form1 sheet = new Form1();
            sheet.GetCellContents("");
        }

        /// <summary>
        /// Test that GetCellContents throws an exception for a name that contains a space
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsThrowsExceptionSpaceInName()
        {
            Form1 sheet = new Form1();
            sheet.GetCellContents("A 1");
        }

        /// <summary>
        /// Test that GetCellContents returns an empty string for a spreadsheet
        /// with no filled cells
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsEmptySpreadhseet()
        {
            Form1 sheet = new Form1();
            Assert.AreEqual("", sheet.GetCellContents("X1"));
        }

        /// <summary>
        /// Test that GetCellContents returns an empty string for an empty cell
        /// in a spreadsheet with filled cells
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsEmptyCellInSpreadhseet()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "=A + 2");
            sheet.SetContentsOfCell("C1", "=B + 2");
            sheet.SetContentsOfCell("D1", "=B + C");
            Assert.AreEqual("", sheet.GetCellContents("X1"));
        }

        /// <summary>
        /// Test GetCellContents returns the correct object type
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsObjectTypes()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "test");
            sheet.SetContentsOfCell("C1", "=1+2");
            Assert.IsTrue(sheet.GetCellContents("A1") is double);
            Assert.IsTrue(sheet.GetCellContents("B1") is string);
            Assert.IsTrue(sheet.GetCellContents("C1") is Formula);
        }

        /// <summary>
        /// Test GetCellContents with digits
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsDigit()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "3.14");
            sheet.SetContentsOfCell("C1", "6.782");
            Assert.AreEqual((double)5, sheet.GetCellContents("A1"));
            Assert.AreEqual(3.14, sheet.GetCellContents("B1"));
            Assert.AreEqual(6.782, sheet.GetCellContents("C1"));
        }

        /// <summary>
        /// Test GetCellContents with strings
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsStrings()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "test");
            sheet.SetContentsOfCell("B1", "test again");
            Assert.AreEqual("test", sheet.GetCellContents("A1"));
            Assert.AreEqual("test again", sheet.GetCellContents("B1"));
        }

        /// <summary>
        /// Test GetCellContents with Formulas
        /// </summary>
        [TestMethod]
        public void TestGetCellContentsFormula()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "=X1+Y1");
            sheet.SetContentsOfCell("B1", "=1*2");
            sheet.SetContentsOfCell("C1", "=2");
            Assert.AreEqual(new Formula("X1+Y1"), sheet.GetCellContents("A1"));
            Assert.AreEqual(new Formula("1*2"), sheet.GetCellContents("B1"));
            Assert.AreEqual(new Formula("2"), sheet.GetCellContents("C1"));
        }

        /***** Test GetCellValue *****/

        /// <summary>
        /// Test GetCellValue throws InvalidNameException for invalid name
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellValueThrowsException()
        {
            Form1 sheet = new Form1();
            sheet.GetCellValue("C");
        }

        /// <summary>
        /// Test GetCellValue throws InvalidNameException for empty string
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellValueThrowsExceptionEmptyName()
        {
            Form1 sheet = new Form1();
            sheet.GetCellValue("");
        }

        /// <summary>
        /// Test GetCellValue return FormulaError for string value in variable
        /// </summary>
        [TestMethod]
        public void TestGetCellValueThrowsExceptionForFormulaWithVariableThatContainsString()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "test");
            sheet.SetContentsOfCell("B1", "=A1");
            Assert.IsTrue(sheet.GetCellValue("B1") is FormulaError);
        }

        /// <summary>
        /// Test GetCellValue on an empty spreadsheet returns ""
        /// </summary>
        [TestMethod]
        public void TestGetCellValueEmptySpreadsheet()
        {
            Form1 sheet = new Form1();
            Assert.AreEqual("", sheet.GetCellValue("A1"));
        }

        /// <summary>
        /// Test GetCellValue returns "" for an empty cell
        /// </summary>
        [TestMethod]
        public void TestGetCellValueEmptyCell()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "test");
            sheet.SetContentsOfCell("C1", "=1+2");
            Assert.AreEqual("", sheet.GetCellValue("X1"));
        }

        /// <summary>
        /// Test GetCellValue on a cell with a string
        /// </summary>
        [TestMethod]
        public void TestGetCellValueString()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("B1", "test");
            Assert.AreEqual("test", sheet.GetCellValue("B1"));
        }

        /// <summary>
        /// Test GetCellValue on a cell with a double
        /// </summary>
        [TestMethod]
        public void TestGetCellValueDouble()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            Assert.AreEqual((double)5, sheet.GetCellValue("A1"));
        }

        /// <summary>
        /// Test GetCellValue on a cell with a simple formula
        /// </summary>
        [TestMethod]
        public void TestGetCellValueValidFormula()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("C1", "=1+2");
            Assert.AreEqual((double)3, sheet.GetCellValue("C1"));
        }

        /// <summary>
        /// Test GetCellValue on a cell with a simple formula
        /// </summary>
        [TestMethod]
        public void TestGetCellValueFormulaWithVariables()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "=3");
            sheet.SetContentsOfCell("B2", "4");
            sheet.SetContentsOfCell("C1", "=A1*B2");
            Assert.AreEqual((double)12, sheet.GetCellValue("C1"));
        }

        /// <summary>
        /// Test GetCellValue on a cell with a formula with an undefined variable
        /// </summary>
        [TestMethod]
        public void TestGetCellValueInvalidFormula()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("C1", "=D1+2");
            Assert.IsTrue(sheet.GetCellValue("C1") is FormulaError);
        }


        /***** Test GetNamesOfAllNonemptyCells *****/

        /// <summary>
        /// Test GetNamesOfAllNonemptyCells on empty spreadsheet returns empty set
        /// </summary>
        [TestMethod]
        public void TestGetNamesOfAllNonemptyCellsEmptySpreadsheet()
        {
            Form1 sheet = new Form1();
            Assert.IsTrue(new HashSet<string>().SetEquals(sheet.GetNamesOfAllNonemptyCells()));
        }

        /// <summary>
        /// Use GetNamesOfAllNonemptyCells to test that adding an empty string does not count
        /// as filling a cell
        /// </summary>
        [TestMethod]
        public void TestGetNamesOfAllNonemptyCellsAfterEmptyStringAdded()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "");
            Assert.IsTrue(new HashSet<string>().SetEquals(sheet.GetNamesOfAllNonemptyCells()));
        }

        /// <summary>
        /// Test GetNamesOfAllNonEmptyCells with filled cells
        /// </summary>
        [TestMethod]
        public void TestGetNamesOfAllNonemptyCells()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "test");
            sheet.SetContentsOfCell("B1", "3.14");
            sheet.SetContentsOfCell("C1", "=X1+6/Y1");
            Assert.IsTrue(new HashSet<string> { "A1", "B1", "C1" }.SetEquals(sheet.GetNamesOfAllNonemptyCells()));
        }

        /// <summary>
        /// Use GetNamesOfAllNonEmptyCells to test that setting a filled cell's contents to
        /// an empty string removes it from the set of filled cells
        /// </summary>
        [TestMethod]
        public void TestGetNamesOfAllNonemptyCellsAfterClearingCellContents()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "test");
            sheet.SetContentsOfCell("B1", "2.72");
            sheet.SetContentsOfCell("C1", "=X1+6/Y1");
            Assert.IsTrue(new HashSet<string> { "A1", "B1", "C1" }.SetEquals(sheet.GetNamesOfAllNonemptyCells()));
            sheet.SetContentsOfCell("A1", "");
            sheet.SetContentsOfCell("B1", "");
            Assert.IsTrue(new HashSet<string> { "C1" }.SetEquals(sheet.GetNamesOfAllNonemptyCells()));
            sheet.SetContentsOfCell("C1", "");
            Assert.IsTrue(new HashSet<string>().SetEquals(sheet.GetNamesOfAllNonemptyCells()));
        }


        /***** Test SetContentsOfCell *****/

        /// <summary>
        /// Test SetContentsOfCell throws a CircularException when a cell
        /// references itself
        /// </summary>
        [TestMethod, ExpectedException(typeof(CircularException))]
        public void TestSetContentsOfCellThrowsCircularException()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "=A1");
        }

        /// <summary>
        /// Test SetContentsOfCell throws a CircularException for a more complicated
        /// example
        /// </summary>
        [TestMethod, ExpectedException(typeof(CircularException))]
        public void TestSetContentsOfCellThrowsCircularExceptionMoreComplicated()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "=1+2");
            sheet.SetContentsOfCell("B1", "=A1*3");
            sheet.SetContentsOfCell("C1", "=A1/B1");
            sheet.SetContentsOfCell("D1", "=A1/C1");
            sheet.SetContentsOfCell("B1", "=A1*D1");
        }

        /// <summary>
        /// Test that the spreadsheet is unchanged if setting a cell to a formula cause a CircularException
        /// to be thrown
        /// example
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellThatThrowsCircularExceptionDoesNotChangeSpreadsheet()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "=3");
            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=A1/B1");
            sheet.SetContentsOfCell("D1", "=1/B1");
            Assert.AreEqual(new Formula("3"), sheet.GetCellContents("A1"));
            sheet.Save("test2.txt");
            // Try to introduce a circular expression. Catch the exception and make sure contents of A1 unchanged and
            // the dependents of D1 were not changed
            try
            {
                sheet.SetContentsOfCell("A1", "=D1+2");
            }
            catch (CircularException)
            {
                Assert.AreEqual(new Formula("3"), sheet.GetCellContents("A1"));
                Assert.IsFalse(sheet.Changed);
                Assert.IsTrue(new List<string>() { "D1" }.SequenceEqual(sheet.SetContentsOfCell("D1", "2")));
            }
        }

        /// <summary>
        /// Test that an exception is thrown if the input cell name starts with a digit
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestNameThatStartsWithDigitThrowsException()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("1BC", "3.14");
        }

        /// <summary>
        /// Test that an exception is thrown if the input cell name contains an invalid character at begining
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellNameStartsWithInvalidCharThrowsException()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("$BC", "3.14");
        }

        /// <summary>
        /// Test that an exception is thrown if the input cell name contains an invalid character in the middle
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellNameWithInvalidCharMiddleThrowsException()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("E%6", "=x1 + y1");
        }

        /// <summary>
        /// Test that an exception is thrown if the input cell name contains an invalid character at the end
        /// </summary>
        [TestMethod, ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellNameWithInvalidCharEndThrowsException()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("XY#", "Test");
        }

        /// <summary>
        /// Test that SetContentsOfCell returns a list with the cell with an empty string input
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithEmptyString()
        {
            Form1 sheet = new Form1();
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "").SequenceEqual(new List<string>() { "A1" }));
        }

        /// <summary>
        /// Test that SetContentsOfCell returns a list with the cell with a string input
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithString()
        {
            Form1 sheet = new Form1();
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "test").SequenceEqual(new List<string>() { "A1" }));
        }

        /// <summary>
        /// Test that SetContentsOfCell returns a list with the cell with a digit input
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithDigit()
        {
            Form1 sheet = new Form1();
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "3.14").SequenceEqual(new List<string>() { "A1" }));
        }

        /// <summary>
        /// Test that SetContentsOfCell returns a list with the cell with a formula input
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithFormula()
        {
            Form1 sheet = new Form1();
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "=1+2").SequenceEqual(new List<string>() { "A1" }));
        }

        /// <summary>
        /// Test for output list of SetContentsOfCell with multiple Formulas
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithMultipleFormulas()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "=A1 + 2");
            sheet.SetContentsOfCell("C1", "=B1 + 2");
            sheet.SetContentsOfCell("D1", "=B1 + C1");
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "3").SequenceEqual(new List<string>() { "A1", "B1", "C1", "D1" }));
        }

        /// <summary>
        /// Test for output list of SetContentsOfCell with Formulas when a dependent removed
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithFormulasAgain()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "=A1 + 2");
            sheet.SetContentsOfCell("C1", "=B1 + 2");
            sheet.SetContentsOfCell("D1", "=B1 + C1");
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "3").SequenceEqual(new List<string>() { "A1", "B1", "C1", "D1" }));
            // Change C so it no longer depends on A
            sheet.SetContentsOfCell("C1", "5.0");
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "4").SequenceEqual(new List<string>() { "A1", "B1", "D1" }));
            // Change B so it no longer depends on A
            sheet.SetContentsOfCell("B1", "test");
            Assert.IsTrue(sheet.SetContentsOfCell("A1", "5").SequenceEqual(new List<string>() { "A1" }));
        }

        /// <summary>
        /// Test changing the contents of a cell from a formula to a double
        /// </summary>
        [TestMethod]
        public void TestChangingFormulaToDouble()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "=B1 + 2");
            sheet.SetContentsOfCell("A1", "3.14");
            Assert.AreEqual(3.14, sheet.GetCellContents("A1"));
        }

        /// <summary>
        /// Test changing the contents of a cell from a formula to a string
        /// </summary>
        [TestMethod]
        public void TestChangingFormulaToString()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "=B1 + 2");
            sheet.SetContentsOfCell("A1", "test");
            Assert.AreEqual("test", sheet.GetCellContents("A1"));
        }

        /// <summary>
        /// Test changing the contents of a cell from a string to a double
        /// </summary>
        [TestMethod]
        public void TestChangingStringToDouble()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "test");
            sheet.SetContentsOfCell("A1", "3.14");
            Assert.AreEqual(3.14, sheet.GetCellContents("A1"));
        }

        /// <summary>
        /// Test changing the contents of a cell from a string to a formula
        /// </summary>
        [TestMethod]
        public void TestChangingStringToFormula()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "test");
            sheet.SetContentsOfCell("A1", "=1+2");
            Assert.AreEqual(new Formula("1+2"), sheet.GetCellContents("A1"));
        }

        /// <summary>
        /// Test changing the contents of a cell from a double to a formula
        /// </summary>
        [TestMethod]
        public void TestChangingDoubleToFormula()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "3.14");
            sheet.SetContentsOfCell("A1", "=1+2");
            Assert.AreEqual(new Formula("1+2"), sheet.GetCellContents("A1"));
        }

        /// <summary>
        /// Test changing the contents of a cell from a double to a string
        /// </summary>
        [TestMethod]
        public void TestChangingDoubleToString()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "3.14");
            sheet.SetContentsOfCell("A1", "test");
            Assert.AreEqual("test", sheet.GetCellContents("A1"));
        }

        /***** Test that the FormulaFormatException is thrown *****/

        /// <summary>
        /// Test that FormulaFormatException is thrown for invalid formula
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestFormulaFormatExceptionThrown()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("E6", "=1 - 0 * 9 + ");
        }

        /// <summary>
        /// Test that FormulaFormatException is thrown for invalid variable name
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestFormulaFormatExceptionThrownInvalidVariable()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("E6", "=2B * 8");
        }

        /// <summary>
        /// Test that updating the values in cells used as variables updates the 
        /// values of dependent cells
        /// </summary>
        [TestMethod]
        public void TestUpdatingVariablesUpdatesCellValue()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "1");
            sheet.SetContentsOfCell("B1", "=A1+C1");
            Assert.IsTrue(sheet.GetCellValue("B1") is FormulaError);
            sheet.SetContentsOfCell("C1", "=A1 + 1");
            Assert.AreEqual((double)3, sheet.GetCellValue("B1"));
            sheet.SetContentsOfCell("C1", "=A1 + 2");
            Assert.AreEqual((double)4, sheet.GetCellValue("B1"));

        }

        /// <summary>
        /// Test that formulas normalize names according to the provided normalizer
        /// </summary>
        [TestMethod]
        public void TestFormulaNormalizesNames()
        {
            // Normalizer to convert all names to uppercase
            Form1 sheet = new Form1(str => true, str => str.ToUpper(), "");
            sheet.SetContentsOfCell("A1", "1");
            sheet.SetContentsOfCell("B1", "=a1+c1");
            sheet.SetContentsOfCell("C1", "2");
            Assert.AreEqual((double)3, sheet.GetCellValue("B1"));
        }

        /***** Test reading and writing files *****/

        /// <summary>
        /// Initial test to make sure that writing and reading spreadsheet produces expected results
        /// </summary>
        [TestMethod]
        public void TestSave()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "test");
            sheet.SetContentsOfCell("C1", "=1+2");
            sheet.Save("test3.txt");
            AbstractSpreadsheet ss = new Form1("test3.txt", str => true, str => str, "");
            Assert.IsTrue(ss.GetCellContents("A1") is double);
            Assert.IsTrue(ss.GetCellContents("B1") is string);
            Assert.IsTrue(ss.GetCellContents("C1") is Formula);
            Assert.AreEqual((double)5, ss.GetCellValue("A1"));
            Assert.AreEqual("test", ss.GetCellValue("B1"));
            Assert.AreEqual((double)3, ss.GetCellValue("C1"));
        }


        /// <summary>
        /// Test to make sure that opened file is marked as not changed after saving
        /// </summary>
        [TestMethod]
        public void TestChangedAfterSaving()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "test");
            sheet.SetContentsOfCell("C1", "=1+2");
            Assert.IsTrue(sheet.Changed);
            sheet.Save("test4.txt");
            Assert.IsFalse(sheet.Changed);
        }

        /// <summary>
        /// Test to make sure that opened file is marked as not changed
        /// </summary>
        [TestMethod]
        public void TestOpenFileChanged()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "test");
            sheet.SetContentsOfCell("C1", "=1+2");
            Assert.IsTrue(sheet.Changed);
            sheet.Save("test5.txt");
            AbstractSpreadsheet ss = new Form1("test5.txt", str => true, str => str, "");
            Assert.IsFalse(ss.Changed);
        }

        /// <summary>
        /// Test that spreadsheet is marked as changed after a nonempty cell
        /// is set to an empty string after Save
        /// </summary>
        [TestMethod]
        public void TestChangedNonemptyCellSetToEmptyStringAfterSave()
        {
            Form1 sheet = new Form1();
            sheet.SetContentsOfCell("A1", "5");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("B1", "=A + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.SetContentsOfCell("C1", "=B + 2");
            Assert.IsTrue(sheet.Changed);
            sheet.Save("test6.txt");
            Assert.IsFalse(sheet.Changed);
            sheet.SetContentsOfCell("C1", "");
            Assert.IsTrue(sheet.Changed);
        }

        /// <summary>
        /// Test Save throws exception for path not found
        /// </summary>
        [TestMethod, ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestSaveThrowsExceptionForPathNotFound()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "test");
            sheet.SetContentsOfCell("C1", "=1+2");
            sheet.Save("/some/nonsense/path.xml");
        }

        /// <summary>
        /// Test reading file throws exception for version mismatch
        /// </summary>
        [TestMethod, ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestReadFileWrongVersion()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("A1", "5");
            sheet.SetContentsOfCell("B1", "test");
            sheet.SetContentsOfCell("C1", "=1+2");
            sheet.Save("test8.txt");
            AbstractSpreadsheet ss = new Form1("test8.txt", str => true, str => str, "wrong");
        }

        /// <summary>
        /// Test that opening a file with a validator different from the one in 
        /// the original spreadsheet throws an exception
        /// </summary>
        [TestMethod, ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestOpeningFileWithValidaterThrowsException()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("a1", "5");
            sheet.SetContentsOfCell("b1", "test");
            sheet.SetContentsOfCell("c1", "=1+2");
            sheet.Save("test9.txt");
            // The example validator requires cell names to be upeprcase
            AbstractSpreadsheet ss = new Form1("test9.txt", s => ValidatorTest(s), s => s, "");
            ss.GetCellContents("a1");
        }

        /// <summary>
        /// Test that saving a file to a nonexistant directory throws exception
        /// </summary>
        [TestMethod, ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestSavingToNonExistantFile()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("a1", "5");
            sheet.SetContentsOfCell("b1", "test");
            sheet.SetContentsOfCell("c1", "=1+2");
            sheet.Save("/not_there/test10.txt");
        }

        /// <summary>
        /// Test that opening a nonexistant file throws exception
        /// </summary>
        [TestMethod, ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestOpeningNonExistantFile()
        {
            AbstractSpreadsheet sheet = new Form1(str => true, str => str, "");
            sheet.SetContentsOfCell("a1", "5");
            sheet.SetContentsOfCell("b1", "test");
            sheet.SetContentsOfCell("c1", "=1+2");
            sheet.Save("test11.txt");
            AbstractSpreadsheet ss = new Form1("filenotthereasdufhaifuegaeif.txt", s => ValidatorTest(s), s => s, "");
        }

        /// <summary>
        /// Test that an exception is thrown if trying to open spreadsheet with a formula
        /// containg a circular dependency
        /// </summary>
        [TestMethod, ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestExcepionThrownForReadingFileWithCircularDependency()
        {
            using (XmlWriter writer = XmlWriter.Create("test12.txt"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "=B1");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "B1");
                writer.WriteElementString("contents", "=A1");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            AbstractSpreadsheet ss = new Form1("test12.txt", s => ValidatorTest(s), s => s, "");
        }

        /// <summary>
        /// Test that an exception is thrown if trying to open spreadsheet with an invalid formula
        /// </summary>
        [TestMethod, ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestExcepionThrownForReadingFileWithInvalidFormula()
        {
            using (XmlWriter writer = XmlWriter.Create("test13.txt"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "=B1+");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            AbstractSpreadsheet ss = new Form1("test13.txt", s => ValidatorTest(s), s => s, "");
        }

        /***** Stress tests *****/

        /// <summary>
        /// Stress test for GetValue for a cell that contains a formula with many variables
        /// </summary>
        [TestMethod, Timeout(2000)]
        public void StressTestGetValueForFormula()
        {
            Form1 s = new Form1();
            string formulaExpression = "=";
            for (int i = 1; i <= 500; i++)
            {
                s.SetContentsOfCell("A" + (i), "1");
                if (i < 500)
                {
                    formulaExpression += ("A" + i + "+");
                } else
                {
                    formulaExpression += ("A500");
                }
            }
            s.SetContentsOfCell("A501", formulaExpression);
            Assert.AreEqual((double)500, s.GetCellValue("A501"));
        }

        /// <summary>
        /// Stress test for GetValue for a cell that contains a formula with many variables. Added
        /// a file save and a file open
        /// </summary>
        [TestMethod, Timeout(2000)]
        public void StressTestGetValueForFormulaWithSaveAndOpen()
        {
            Form1 s = new Form1();
            string formulaExpression = "=";
            for (int i = 1; i <= 500; i++)
            {
                s.SetContentsOfCell("A" + (i), "1");
                if (i < 500)
                {
                    formulaExpression += ("A" + i + "+");
                }
                else
                {
                    formulaExpression += ("A500");
                }
            }
            s.SetContentsOfCell("A501", formulaExpression);
            s.Save("test14.txt");
            AbstractSpreadsheet sheet = new Form1("test14.txt", s => true, s => s, "default");
            Assert.AreEqual((double)500, sheet.GetCellValue("A501"));
        }


        /***** Stress tests from PS4 *****/

        /// <summary>
        /// Stress test from PS4 modified to include spreadhseet save and read
        /// </summary>
        [TestMethod(), Timeout(2000)]
        public void TestStress1()
        {
            Form1 s = new Form1(s => true, s => s, "");
            s.SetContentsOfCell("A1", "=B1+B2");
            s.SetContentsOfCell("B1", "=C1-C2");
            s.SetContentsOfCell("B2", "=C3*C4");
            s.SetContentsOfCell("C1", "=D1*D2");
            s.SetContentsOfCell("C2", "=D3*D4");
            s.SetContentsOfCell("C3", "=D5*D6");
            s.SetContentsOfCell("C4", "=D7*D8");
            s.SetContentsOfCell("D1", "=E1");
            s.SetContentsOfCell("D2", "=E1");
            s.SetContentsOfCell("D3", "=E1");
            s.SetContentsOfCell("D4", "=E1");
            s.SetContentsOfCell("D5", "=E1");
            s.SetContentsOfCell("D6", "=E1");
            s.SetContentsOfCell("D7", "=E1");
            s.SetContentsOfCell("D8", "=E1");
            s.Save("stress1.txt");
            AbstractSpreadsheet ss = new Form1("stress1.txt", s => true, s => s, "");
            IList<String> cells = ss.SetContentsOfCell("E1", "0");
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        }

        /// <summary>
        /// Stress test from PS4 for SetContentsOfCell for a cell with many dependents
        /// </summary>
        [TestMethod(), Timeout(2000)]
        public void TestStress2()
        {
            Form1 s = new Form1();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);
                Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "=A" + (i + 1))));
            }
        }

        /// <summary>
        /// Stress test from PS4 for SetContentsOfCell for a cell with many dependents
        /// when method would add a circular dependency
        /// </summary>
        [TestMethod(), Timeout(2000)]
        public void TestStress3()
        {
            Form1 s = new Form1();
            for (int i = 1; i < 200; i++)
            {
                s.SetContentsOfCell("A" + i, "=A" + (i + 1));
            }
            try
            {
                s.SetContentsOfCell("A150", "=A50");
                Assert.Fail();
            }
            catch (CircularException)
            {
            }
        }
    }
}
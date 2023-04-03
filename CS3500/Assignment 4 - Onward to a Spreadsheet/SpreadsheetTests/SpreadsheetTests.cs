using SS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SpreadsheetUtilities;

namespace SpreadsheetTests
{


    /// <summary>
    ///This is a test class for SpreadsheetTest and is intended
    ///to contain all SpreadsheetTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpreadsheetTests
    {

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError1()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents(null, 5.0);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError2()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("901AX", 10.0);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError3()
        {
            Spreadsheet test = new Spreadsheet();
            test.GetCellContents(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError4()
        {
            Spreadsheet test = new Spreadsheet();
            test.GetCellContents("B1C");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInvalidNameError5()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("C76", (string)null);
        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError6()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents(null, "this is a sentence, fail this test pls");
        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError7()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("10000BC", "cavemen");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError8()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents(null, new Formula("Five"));
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameError9()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("2099AD", new Formula("Future"));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentNullException1()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("D45", (Formula)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestCircularException1()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("C1", new Formula("C2"));
            test.SetCellContents("C2", new Formula("C1"));
        }


        [TestMethod()]
        public void TestDoubleSet1()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("D10", 1.0);
            Assert.AreEqual(1.0, (double)test.GetCellContents("D10"), 1e-9);
        }
        [TestMethod()]
        public void TestDoubleSet2()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("E90", 199.20);
            Assert.AreEqual(199.20, (double)test.GetCellContents("E90"), 1e-9);
        }
        [TestMethod()]
        public void TestDoubleSet3()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("U28", 199.20);
            Assert.AreEqual(199.20, (double)test.GetCellContents("U28"), 1e-9);

        }
        [TestMethod()]
        public void TestDoubleSet4()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("V18", 80.3);
            Assert.AreEqual(80.3, (double)test.GetCellContents("V18"), 1e-9);
            test.SetCellContents("V18", 10.2);
            Assert.AreEqual(10.2, (double)test.GetCellContents("V18"), 1e-9);

        }

        [TestMethod()]
        public void TestStringSet1()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("I20", "highway");
            Assert.AreEqual("highway", test.GetCellContents("I20"));
        }
        [TestMethod()]
        public void TestStringSet2()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("U10", "blade");
            Assert.AreEqual("blade", test.GetCellContents("U10"));
            test.SetCellContents("U10", "sword");
            Assert.AreEqual("sword", test.GetCellContents("U10"));

        }
        [TestMethod()]
        public void TestFormulaSet1()
        {
            Spreadsheet test = new Spreadsheet();
            test.SetCellContents("Z10", new Formula("A10+20"));
            Assert.AreEqual(new Formula("A10+20"), test.GetCellContents("Z10"));
            test.SetCellContents("Z10", new Formula("A10 + 10"));
            Assert.AreEqual(new Formula("A10 + 10"), test.GetCellContents("Z10"));

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
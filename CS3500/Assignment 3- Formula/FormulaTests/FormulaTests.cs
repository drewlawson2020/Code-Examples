using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;

namespace FormulaTester
{
    /// <summary> 
    /// Author:    Drew 
    /// Partner:   None
    /// Date:      3-Feb-2022
    /// Course:    CS 3500, University of Utah, School of Computing 
    /// Copyright: CS 3500 and Drew Lawson - This work may not be copied for use in Academic Coursework. 
    /// 
    /// I, Drew Lawson, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source.  All references used in the completion of the assignment are cited in my README file. 
    /// 
    /// File Contents 
    /// 
    ///    This file contains all of the necessary test methods to test the capabilities and functionality of the Formula class. 
    ///    Most of the tests are self-explanatory as given by the method names. 
    [TestClass()]
    public class FormulaTests
    {
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormulaError()
        {
            Formula f = new Formula("");
        }
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError()
        {
            Formula f = new Formula("*/+");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError2()
        {
            Formula f = new Formula("-2-3");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError3()
        {
            Formula f = new Formula(")(2");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError4()
        {
            Formula f = new Formula("2+9(()");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError5()
        {
            Formula f = new Formula("190B");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError6()
        {
            Formula f = new Formula("10+23+89C");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError7()
        {
            Formula f = new Formula("1 1 1 1 1");
        }
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSyntaxError8()
        {
            Formula f = new Formula("6*/6");
        }
        [TestMethod()]
        public void RuntimeError()
        {
            Formula f = new Formula("9001/0");
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }
        [TestMethod()]
        public void testFormulaWithVariables()
        {
            Formula f = new Formula("X10 + 20");
            Assert.AreEqual(23, (double)f.Evaluate(s => 3), 1e-9);
        }
        [TestMethod()]
        public void testFormulaWithVariable2s()
        {
            Formula f = new Formula("X1 * X2 * X3 * X4");
            Assert.AreEqual(1, (double)f.Evaluate(s => 1), 1e-9);
        }
        [TestMethod()]
        public void testFormulaWithVariables3()
        {
            Formula f = new Formula("(X1 * X2) * (X3 * X4)");
            Assert.AreEqual(81, (double)f.Evaluate(s => 3), 1e-9);
        }

        [TestMethod()]
        public void normalizeTest()
        {
            Formula f = new Formula("20+X2", s => "x2", s => true);
            HashSet<string> variables = new HashSet<string> { "x2" };
            Assert.IsTrue(variables.SetEquals(f.GetVariables()));
        }

        [TestMethod()]
        public void TestEquality()
        {
            Formula f1 = new Formula("A1+B2");
            Formula f2 = new Formula("A1+B2");
            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod()]
        public void TestEquality2()
        {
            Formula f1 = new Formula("1.0*10.0");
            Formula f2 = new Formula("1 * 10");
            Assert.IsTrue(f1.Equals(f2));
        }
        [TestMethod()]
        public void TestEqualityOperators()
        {
            Formula f1 = new Formula("1");
            Formula f2 = new Formula("1");
            Assert.IsTrue(f1 == f2);
        }

        [TestMethod()]
        public void TestEqualityOperators2()
        {
            Formula f1 = new Formula("100");
            Formula f2 = new Formula("99");
            Assert.IsFalse(f1 == f2);
        }
        [TestMethod()]
        public void TestNotEqualityOperators()
        {
            Formula f1 = new Formula("5");
            Formula f2 = new Formula("5");
            Assert.IsFalse(f1 != f2);
        }

        [TestMethod()]
        public void TestNotEquality2Operators()
        {
            Formula f1 = new Formula("1");
            Formula f2 = new Formula("5");
            Assert.IsTrue(f1 != f2);
        }

        [TestMethod()]
        public void toStringTest()
        {
            Formula f = new Formula("10*10");
            Assert.IsTrue(f.Equals(new Formula(f.ToString())));
        }

        [TestMethod()]
        public void HashCodeEqualityTest()
        {
            Formula f1 = new Formula("2+10");
            Formula f2 = new Formula("2+10");
            Assert.IsTrue(f1.GetHashCode() == f2.GetHashCode());
        }

        [TestMethod()]
        public void GetVariablesTest()
        {
            Formula f = new Formula("10+20+30+40");
            Assert.IsFalse(f.GetVariables().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void GetVariablesTest2()
        {
            Formula f = new Formula("2*A2+B2");
            List<string> actual = new List<string>(f.GetVariables());
            HashSet<string> expected = new HashSet<string>() { "A2", "B2" };
            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(expected.SetEquals(actual));
        }


    }
}
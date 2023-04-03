/// <summary>
/// Author: Peter Bruns
/// Partner: None
/// Date: February 2, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// I, Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from 
/// another source. All references used in the completion of the assignmentare cited in my README file.
/// 
/// File Contents
/// 
/// Contains unit tests for methods in the Formula class. Tests method output along with errors and exceptions
/// where applicable. Also contains simple methods to be passed as Func arguments to the constructor and Evaluate.
/// </summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;

namespace FormulaTests
{
    /// <summary>
    /// Contains tests for formulas with single operators. Used as starting point for testing
    /// </summary>
    [TestClass]
    public class FormulaTestsEvaluateSingleOperators
    {
        /// <summary>
        /// Test that a single value formula returns that value
        /// </summary>
        [TestMethod]
        public void TestSingleValueFormula()
        {
            Formula test = new Formula("2");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that adding two numbers works with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleAddition()
        {
            Formula test = new Formula("1+1");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that adding two numbers works with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleAdditionInsideParentheses()
        {
            Formula test = new Formula("(1+1)");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that adding two numbers works with no other operations with extra spaces in formula
        /// </summary>
        [TestMethod]
        public void TestSimpleAdditionWithSpaces()
        {
            Formula test = new Formula("   1      +                   1 ");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that subtraction works with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleSubtraction()
        {
            Formula test = new Formula("3-1");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that subtraction works with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleSubtractionInsideParentheses()
        {
            Formula test = new Formula("(3-1)");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that subtraction works with no other operations with extra spaces in formula
        /// </summary>
        [TestMethod]
        public void TestSimpleSubtractionWithSpaces()
        {
            Formula test = new Formula("   3       -                   1 ");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that multiplication works with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleMultiplication()
        {
            Formula test = new Formula("2*1");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that multiplication works inside parentheses with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleMultiplicationInParentheses()
        {
            Formula test = new Formula("(2*1)");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that multiplication works with no other operations with extra spaces in formula
        /// </summary>
        [TestMethod]
        public void TestSimpleMulitplicationWithSpaces()
        {
            Formula test = new Formula("   2       *                   1 ");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that division works with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleDivision()
        {
            Formula test = new Formula("2/1");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that division works inside parentheses with no other operations
        /// </summary>
        [TestMethod]
        public void TestSimpleDivisionInParentheses()
        {
            Formula test = new Formula("(2/1)");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that division works with no other operations with extra spaces in formula
        /// </summary>
        [TestMethod]
        public void TestSimpleDivisionWithSpaces()
        {
            Formula test = new Formula("   2       /                   1 ");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that Evaluate works with scientific notation
        /// </summary>
        [TestMethod]
        public void TestScientificNotation()
        {
            Formula test = new Formula("1e2 + 1");
            double expected = 101;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// Test that Evaluate works with negative scientific notation
        /// </summary>
        [TestMethod]
        public void TestNegativeScientificNotation()
        {
            Formula test = new Formula("100e-2 + 1");
            double expected = 2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }
    }

    /// <summary>
    /// Contains tests for the Formula class where the input expression contains multiple operators and parentheses
    /// </summary>
    [TestClass]
    public class FormulaTestsEvaluateMultipleOperators
    {
        /// <summary>
        /// A test that the proper order of operations is followed
        /// </summary>
        [TestMethod]
        public void FirstTestForOrderOfOperations()
        {
            Formula test = new Formula("3.1 + 1.4 * 2.5");
            double expected = 3.1 + 1.4 * 2.5;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed
        /// </summary>
        [TestMethod]
        public void SecondTestForOrderOfOperations()
        {
            Formula test = new Formula("7.8 / 1.4 - 2.5 * 8.2");
            double expected = 7.8 / 1.4 - 2.5 * 8.2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed
        /// </summary>
        [TestMethod]
        public void ThirdTestForOrderOfOperations()
        {
            Formula test = new Formula("7.8 / 1.4 - 2.5 * 8.2 + 9.2 / 9.823 * 1.23123 - 2.2134 / 0.12312");
            double expected = 7.8 / 1.4 - 2.5 * 8.2 + 9.2 / 9.823 * 1.23123 - 2.2134 / 0.12312;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed for an expression containing parentheses
        /// </summary>
        [TestMethod]
        public void FirstTestForOrderOfOperationsWithParentheses()
        {
            Formula test = new Formula("(9.32 - 7.5) / (2.3 + 8.723)");
            double expected = (9.32 - 7.5) / (2.3 + 8.723);
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed for an expression containing parentheses
        /// </summary>
        [TestMethod]
        public void SecondTestForOrderOfOperationsWithParentheses()
        {
            Formula test = new Formula("(9.32 - 7.5) * (2.3 + 8.723)");
            double expected = (9.32 - 7.5) * (2.3 + 8.723);
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed for an expression containing parentheses
        /// </summary>
        [TestMethod]
        public void ThirdTestForOrderOfOperationsWithParentheses()
        {
            Formula test = new Formula("(9.32 - 7.5 / 1.343) * (2.3 + 8.723 - 3) + (12.241 * 0.232) / (5.5 / 6.78 + 8.74) ");
            double expected = (9.32 - 7.5 / 1.343) * (2.3 + 8.723 - 3) + (12.241 * 0.232) / (5.5 / 6.78 + 8.74);
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed for an expression containing nested parentheses
        /// </summary>
        [TestMethod]
        public void FirstTestForOrderOfOperationsWithNestedParentheses()
        {
            Formula test = new Formula("((6.3 - 2) * 4.7) + 1.3 ");
            double expected = ((6.3 - 2) * 4.7) + 1.3;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed for an expression containing nested parentheses
        /// </summary>
        [TestMethod]
        public void SecondTestForOrderOfOperationsWithNestedParentheses()
        {
            Formula test = new Formula("(2.4 + 5.67) / (3.56 * (4.5 - 5.2))  ");
            double expected = (2.4 + 5.67) / (3.56 * (4.5 - 5.2));
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

        /// <summary>
        /// A test that the proper order of operations is followed for an expression containing nested parentheses
        /// </summary>
        [TestMethod]
        public void ThirdTestForOrderOfOperationsWithNestedParentheses()
        {
            Formula test = new Formula("(((5.4 - 3.4) * (2.454 / 2.4 * (4.6 + 2)) - 3.14) * (5.32 - ((2.3 + 5.4) / 2.12) + 2) * 6.78)");
            double expected = (((5.4 - 3.4) * (2.454 / 2.4 * (4.6 + 2)) - 3.14) * (5.32 - ((2.3 + 5.4) / 2.12) + 2) * 6.78);
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.ReturnZero));
        }

    }
    
    /// <summary>
    /// Contains tests to ensure that variables work properly in Evaluate
    /// </summary>
    [TestClass]
    public class FormulaTestsEvaluateWithVariables
    {
        /// <summary>
        /// Test that a formula that contains only a variable returns the value of the variable
        /// </summary>
        [TestMethod]
        public void TestVariablesWorkSingleValue()
        {
            Formula test = new Formula("X1");
            double expected = 3.2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.VariableLookup));
        }

        /// <summary>
        /// Test addition with variables
        /// </summary>
        [TestMethod]
        public void TestVariablesWorkAddition()
        {
            Formula test = new Formula("X1 + _y_1");
            double expected = 3.2 + 1.3;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.VariableLookup));
        }

        /// <summary>
        /// Test subtraction with variables
        /// </summary>
        [TestMethod]
        public void TestVariablesWorkSubtraction()
        {
            Formula test = new Formula("X1 - _y_1");
            double expected = 3.2 - 1.3;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.VariableLookup));
        }

        /// <summary>
        /// Test multiplication with variables
        /// </summary>
        [TestMethod]
        public void TestVariablesWorkMultiplication()
        {
            Formula test = new Formula("X1 * _y_1");
            double expected = 3.2 * 1.3;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.VariableLookup));
        }

        /// <summary>
        /// Test division with variables
        /// </summary>
        [TestMethod]
        public void TestVariablesWorkDivision()
        {
            Formula test = new Formula("X1 / _y_1");
            double expected = 3.2 / 1.3;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.VariableLookup));
        }

        /// <summary>
        /// Test order of operations with variables
        /// </summary>
        [TestMethod]
        public void TestVariableOrderOfOperations()
        {
            Formula test = new Formula("X1 + Zz / _y_1 - Zz * 4.2");
            double expected = 3.2 + 4.3 / 1.3 - 4.3 * 4.2;
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.VariableLookup));
        }

        /// <summary>
        /// Test order of operations and parentheses with variables
        /// </summary>
        [TestMethod]
        public void TestVariableOrderOfOperationsAndParentheses()
        {
            Formula test = new Formula("(X1 + Zz) / ((_y_1 - Zz) * 4.2)");
            double expected = (3.2 + 4.3) / ((1.3 - 4.3) * 4.2);
            Assert.AreEqual(expected, test.Evaluate(DelegateMethods.VariableLookup));
        }
    }

    /// <summary>
    /// Contains tests for the methods in the Formula class that are not Evaluate.
    /// </summary>
    [TestClass]
    public class TestFormulaMethodsNotEvaluate
    {
        /// <summary>
        /// Test that get variables works when there are no variables in the formula
        /// </summary>
        [TestMethod]
        public void TestGetVariablesNoVariables()
        {
            Formula test = new Formula("2-1");
            Assert.IsTrue(new HashSet<String> { }.SetEquals(test.GetVariables()));
        }

        /// <summary>
        /// Test that get variables works when there are in the formula
        /// </summary>
        [TestMethod]
        public void TestGetVariablesWithVariables()
        {
            Formula test = new Formula("X1 + Y1 - 9 + Z");
            Assert.IsTrue(new HashSet<String> { "X1", "Y1", "Z" }.SetEquals(test.GetVariables()));
        }

        /// <summary>
        /// Test that get variables works when there are repeated variables in the formula
        /// </summary>
        [TestMethod]
        public void TestGetVariablesWithRepeatedVariables()
        {
            Formula test = new Formula("X1 + Y1 - 9 + Z * X1 - Z + Y1 *2");
            Assert.IsTrue(new HashSet<String> { "X1", "Y1", "Z" }.SetEquals(test.GetVariables()));
        }

        /// <summary>
        /// Test that get variables works with many different possible variable forms. According to the API,
        /// variables are an underscore or letter followed by zero or more letters, underscores, or digits
        /// </summary>
        [TestMethod]
        public void TestGetVariablesWithDifferentVariableForms()
        {
            Formula test = new Formula("x1 + _Yz1 - 8 + __4 * A_1_Arp - Z2 *2 + yY1_");
            Assert.IsTrue(new HashSet<String> { "x1", "_Yz1", "__4", "A_1_Arp", "Z2", "yY1_" }.SetEquals(test.GetVariables()));
        }
        
        /// <summary>
        /// A test of ToString with a simple expression
        /// </summary>
        [TestMethod]
        public void SimpleTestToString()
        {
            Formula test = new Formula("1+1");
            Assert.AreEqual("1+1", test.ToString());
        }

        /// <summary>
        /// A test of ToString with a simple expression with extra spaces
        /// </summary>
        [TestMethod]
        public void SimpleTestToStringWithSpaces()
        {
            Formula test = new Formula(" 1   +  1       ");
            Assert.AreEqual("1+1", test.ToString());
        }

        /// <summary>
        /// A test of ToString with a complex expression including extra spaces
        /// </summary>
        [TestMethod]
        public void ComplexTestToString()
        {
            Formula test = new Formula("((1.3  + 4)-9.2) * ( (   (X1 - __y_2)-2) / 3)      * 9      -1");
            Assert.AreEqual("((1.3+4)-9.2)*(((X1-__y_2)-2)/3)*9-1", test.ToString());
        }

        /// <summary>
        /// Test Equals with the same expression in both formula constructors
        /// </summary>
        [TestMethod]
        public void SimpleTestEquals()
        {
            Formula test1 = new Formula("2/3");
            Formula test2 = new Formula("2/3");
            Assert.IsTrue(test1.Equals(test2));
        }


        /// <summary>
        /// Test Equals with the same expression but different spacing
        /// </summary>
        [TestMethod]
        public void SimpleTestEqualsDifferentSpacing()
        {
            Formula test1 = new Formula(" 2  /   3    ");
            Formula test2 = new Formula("2    / 3  ");
            Assert.IsTrue(test1.Equals(test2));
        }

        /// <summary>
        /// Test Equals with different numbers of trailing zeroes
        /// </summary>
        [TestMethod]
        public void TestEqualsWithTrailingZeroes()
        {
            Formula test1 = new Formula("2.01/3");
            Formula test2 = new Formula("2.0100/3.0000");
            Assert.IsTrue(test1.Equals(test2));
        }


        /// <summary>
        /// Test Equals with non-equal formulas
        /// </summary>
        [TestMethod]
        public void SimpleTestEqualsNotEqual()
        {
            Formula test1 = new Formula("2/3");
            Formula test2 = new Formula("1/3");
            Assert.IsFalse(test1.Equals(test2));
        }


        /// <summary>
        /// Test Equals with non-equal formulas due to differences in decimal values
        /// </summary>
        [TestMethod]
        public void TestEqualsNotEqual()
        {
            Formula test1 = new Formula("2.01/3");
            Formula test2 = new Formula("2.03/3");
            Assert.IsFalse(test1.Equals(test2));
        }

        /// <summary>
        /// Test that Equals works with variables when formulas are equal
        /// </summary>
        [TestMethod]
        public void TestEqualsWithEqualVariables()
        {
            Formula test1 = new Formula("X1 + Y1");
            Formula test2 = new Formula("X1 + Y1");
            Assert.IsTrue(test1.Equals(test2));
        }

        /// <summary>
        /// Test that Equals works with variables when formulas are not equal
        /// </summary>
        [TestMethod]
        public void TestEqualsWithEqualVariablesNotEqual()
        {
            Formula test1 = new Formula("x1 + y1");
            Formula test2 = new Formula("X1 + Y1");
            Assert.IsFalse(test1.Equals(test2));
        }

        /// <summary>
        /// Test that Equals works with variables when formulas are equal after normalization
        /// </summary>
        [TestMethod]
        public void TestEqualsWithEqualVariablesEqualAfterNormalization()
        {
            Formula test1 = new Formula("x1 + y1", DelegateMethods.ExampleNormalizer, s => true);
            Formula test2 = new Formula("X1 + Y1", DelegateMethods.ExampleNormalizer, s => true);
            Assert.IsTrue(test1.Equals(test2));
        }

        /// <summary>
        /// Test that Equals works with null input
        /// </summary>
        [TestMethod]
        public void TestEqualsWithNullInput()
        {
            Formula test1 = new Formula("x1 + y1", DelegateMethods.ExampleNormalizer, s => true);
            Assert.IsFalse(test1.Equals(null));
        }

        /// <summary>
        /// Test that Equals works with a non-formula input
        /// </summary>
        [TestMethod]
        public void TestEqualsWithNonFormulaInput()
        {
            Formula test1 = new Formula("x1 + y1", DelegateMethods.ExampleNormalizer, s => true);
            Assert.IsFalse(test1.Equals(3.1));
        }

        /// <summary>
        /// Test that the same hash code is returned for equal formulas. Here both have the same input string
        /// </summary>
        [TestMethod]
        public void TestGetHashCodeSameExpression()
        {
            Formula test1 = new Formula("2/3");
            Formula test2 = new Formula("2/3");
            Assert.IsTrue(test1.GetHashCode() == test2.GetHashCode());
        }

        /// <summary>
        /// Test that the same hash code is returned for equal formulas with different spacing in input
        /// </summary>
        [TestMethod]
        public void TestGetHashCodeEqualWithDifferentSpacing()
        {
            Formula test1 = new Formula("2 /   3");
            Formula test2 = new Formula("2           / 3");
            Assert.IsTrue(test1.GetHashCode() == test2.GetHashCode());
        }

        /// <summary>
        /// Test that the same hash code is returned for equal formulas with different decimal places
        /// </summary>
        [TestMethod]
        public void TestGetHashCodeEqualWithDifferentDecimalPlaces()
        {
            Formula test1 = new Formula("2.01/3.123");
            Formula test2 = new Formula("2.0100 / 3.12300");
            Assert.IsTrue(test1.GetHashCode() == test2.GetHashCode());
        }
    }

    /// <summary>
    /// Contains tests to ensure that the == and != operators function correctly
    /// </summary>
    [TestClass]
    public class FormulaTestsTestOperators
    {
        /// <summary>
        /// Test == and != with the same expression in both formula constructors
        /// </summary>
        [TestMethod]
        public void SimpleTestEqualsOperators()
        {
            Formula test1 = new Formula("2/3");
            Formula test2 = new Formula("2/3");
            Assert.IsTrue(test1 == test2);
            Assert.IsFalse(test1 != test2);
        }


        /// <summary>
        /// Test == and != with the same expression but different spacing
        /// </summary>
        [TestMethod]
        public void SimpleTestEqualsOperatorsDifferentSpacing()
        {
            Formula test1 = new Formula(" 2  /   3    ");
            Formula test2 = new Formula("2    / 3  ");
            Assert.IsTrue(test1 == test2);
            Assert.IsFalse(test1 != test2);
        }

        /// <summary>
        /// Test == and != with different numbers of trailing zeroes
        /// </summary>
        [TestMethod]
        public void TestEqualsOperatorsWithTrailingZeroes()
        {
            Formula test1 = new Formula("2.01/3");
            Formula test2 = new Formula("2.0100/3.0000");
            Assert.IsTrue(test1==test2);
            Assert.IsFalse(test1 != test2);
        }


        /// <summary>
        /// Test != and == with non-equal formulas
        /// </summary>
        [TestMethod]
        public void SimpleTestEqualsNotEqual()
        {
            Formula test1 = new Formula("2/3");
            Formula test2 = new Formula("1/3");
            Assert.IsFalse(test1 == test2);
            Assert.IsTrue(test1 != test2);
        }


        /// <summary>
        /// Test == and != with non-equal formulas due to differences in decimal values
        /// </summary>
        [TestMethod]
        public void TestEqualsNotEqual()
        {
            Formula test1 = new Formula("2.01/3");
            Formula test2 = new Formula("2.03/3");
            Assert.IsFalse(test1 == test2);
            Assert.IsTrue(test1 != test2);
        }

        /// <summary>
        /// Test that == and != work with variables when formulas are equal
        /// </summary>
        [TestMethod]
        public void TestEqualsOperatorsWithEqualVariables()
        {
            Formula test1 = new Formula("X1 + Y1");
            Formula test2 = new Formula("X1 + Y1");
            Assert.IsTrue(test1 == test2);
            Assert.IsFalse(test1 != test2);
        }

        /// <summary>
        /// Test that == and != work with variables when formulas are not equal
        /// </summary>
        [TestMethod]
        public void TestEqualsOperatorsWithEqualVariablesNotEqual()
        {
            Formula test1 = new Formula("x1 + y1");
            Formula test2 = new Formula("X1 + Y1");
            Assert.IsFalse(test1 == test2);
            Assert.IsTrue(test1 != test2);
        }

        /// <summary>
        /// Test that == and != work with variables when formulas are equal after normalization
        /// </summary>
        [TestMethod]
        public void TestEqualsWithEqualVariablesEqualAfterNormalization()
        {
            Formula test1 = new Formula("x1 + y1", DelegateMethods.ExampleNormalizer, s => true);
            Formula test2 = new Formula("X1 + Y1", DelegateMethods.ExampleNormalizer, s => true);
            Assert.IsTrue(test1 == test2);
            Assert.IsFalse(test1 != test2);
        }
    }


    /// <summary>
    /// Contains tests that ensure that exceptions and errors are being produced properly
    /// </summary>
    [TestClass]
    public class FormulaTestsExceptionsAndErrors
    {
        /// <summary>
        /// Test that the constructor will throw an exception if the input string contains no tokens
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormulaThrowsException()
        {
            Formula test = new Formula("  ");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if the input string contains only an operator
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOnlyOperatorFormulaThrowsException()
        {
            Formula test = new Formula(" + ");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if the input string contains only a parenthesis
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOnlyParenthesisFormulaThrowsException()
        {
            Formula test = new Formula(" ( ");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if the input string contains only parentheses
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOnlyParenthesesFormulaThrowsException()
        {
            Formula test = new Formula(" (())() ");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if the input string contains an invalid token
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidTokenInEputThrowsException()
        {
            Formula test = new Formula(" 1 $ 2");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if the input string contains unbalanced parentheses
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestUnbalancedParenthesesInInputThrowsException()
        {
            Formula test = new Formula(" ((1+2) - 3");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if the number of close parentheses exceeds
        /// the number of open parentheses
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestMoreCloseParenthesesInInputThrowsException()
        {
            Formula test = new Formula(" ((1+2) * 4)) - 3");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is an operator following (
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOperatorFollowingOpenParenthesesThrowsException()
        {
            Formula test = new Formula(" (+2) * 4 - 3");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is an operator following an operator
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOperatorFollowingOperatorThrowsException()
        {
            Formula test = new Formula(" 2 * + 4 - 3");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is a number following a number
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestNumberFollowingNumberThrowsException()
        {
            Formula test = new Formula(" 2 * 7 4 - 3");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is a number following a variable
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestNumberFollowingVariableThrowsException()
        {
            Formula test = new Formula(" 2 * A1 7 - 3");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is a variable following a variable
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestVariableFollowingVariableThrowsException()
        {
            Formula test = new Formula(" 2 * A1 B1 - 3");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is a variable following a number
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestVariableFollowingNumberThrowsException()
        {
            Formula test = new Formula(" 2 * 7 B1 - 3");
        }


        /// <summary>
        /// Test that the constructor will throw an exception if there is a ( following a number
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOpenParenthesisFollowingNumberThrowsException()
        {
            Formula test = new Formula(" 2 * 7 ( 1 - 3)");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is a ( following a variable
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOpenParenthesisFollowingVariableThrowsException()
        {
            Formula test = new Formula(" 2 * A1 ( 1 - 3)");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is a ( following a )
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestOpenParenthesisFollowingCloseParenthesisThrowsException()
        {
            Formula test = new Formula(" (2 * 1) ( 1 - 3)");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if the expression ends in an operator
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestLastTokenOperatorThrowsException()
        {
            Formula test = new Formula(" 2 + 1 -");
        }

        /// <summary>
        /// Test that the constructor will throw an exception if there is an invalid variable. The example
        /// validator here looks for an uppercase letter followed by one or more numbers
        /// </summary>
        [TestMethod, ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidVariableThrowsException()
        {
            Formula test = new Formula("A1 + b1", s=>s, DelegateMethods.ExampleValidator);
        }

        /// <summary>
        /// Test that the constructor will not throw an exception if the normalizer makes an invalid 
        /// variable valid. THe normalizer converts all strings to uppercase, the validator looks for 
        /// an uppercase letter followed by one or more numbers 
        /// </summary>
        [TestMethod]
        public void TestInvalidVariableDoesNotThrowExceptionAfterNormalization()
        {
            Formula test = new Formula("a1 + b1", DelegateMethods.ExampleNormalizer, DelegateMethods.ExampleValidator);
        }

        /// <summary>
        /// Test that Evaluate will return an error if divide by zero occurs
        /// </summary>
        [TestMethod]
        public void TestDivideByZeroReturnsError()
        {
            Formula test = new Formula("7/0");
            Assert.IsTrue(test.Evaluate(DelegateMethods.ReturnZero) is FormulaError);
        }

        /// <summary>
        /// Test that Evaluate will return an error if divide by zero occurs with values in denominator
        /// in parentheses
        /// </summary>
        [TestMethod]
        public void TestDivideByZeroReturnsErrorAgain()
        {
            Formula test = new Formula("7/(3.2 - 3.2)");
            Assert.IsTrue(test.Evaluate(DelegateMethods.ReturnZero) is FormulaError);
        }

        /// <summary>
        /// Test that Evaluate will return an error if divide by zero occurs with a variable in the denominator
        /// </summary>
        [TestMethod]
        public void TestDivideByZeroReturnsErrorWithVariable()
        {
            Formula test = new Formula("7 + 3/w1");
            Assert.IsTrue(test.Evaluate(DelegateMethods.VariableLookup) is FormulaError);
        }

        /// <summary>
        /// Test that Evaluate will return an error if divide by zero occurs with variables in denominator
        /// in parentheses
        /// </summary>
        [TestMethod]
        public void TestDivideByZeroReturnsErrorVariables()
        {
            Formula test = new Formula("7/(X1 - X1)");
            Assert.IsTrue(test.Evaluate(DelegateMethods.VariableLookup) is FormulaError);
        }

        /// <summary>
        /// Test that Evaluate will return an error if it includes undefined variables
        /// </summary>
        [TestMethod]
        public void TestVariablesNotFoundReturnsError()
        {
            Formula test = new Formula("7/(q1 - X1)");
            Assert.IsTrue(test.Evaluate(DelegateMethods.VariableLookup) is FormulaError);
        }
    }

    /// <summary>
    /// Contains the delegate methods used for the unit tests above
    /// </summary>
    public class DelegateMethods
    {
        /// <summary>
        /// Returns 0 no matter what
        /// </summary>
        public static double ReturnZero(String input)
        {
            return 0;
        }

        /// <summary>
        /// An example variable lookup function for use in testing.
        /// </summary>
        /// <param name="str">The variable name</param>
        /// <returns>The value associated with the variable if it exists</returns>
        /// <exception cref="ArgumentException">If the variable is not found</exception>
        public static double VariableLookup(String str)
        {
            if (str == "X1")
            {
                return 3.2;
            }
            else if (str == "_y_1")
            {
                return 1.3;
            }
            else if (str == "Zz")
            {
                return 4.3;
            }
            else if (str == "w1")
            {
                return 0;
            }
            else
            {
                throw new ArgumentException("The variable is not defined");
            }
        }

        /// <summary>
        /// Example normalizing function that converts all characters in string to upper-case
        /// </summary>
        public static String ExampleNormalizer(String expression)
        {
            return expression.ToUpper();
        }

        /// <summary>
        /// Example validator that returns true if all variables consist of one upper-case letter
        /// followed by one or more digits.
        /// </summary>
        public static Boolean ExampleValidator(String str)
        {
            Regex pattern = new Regex(("[A-Z][0-9]+"));
            if (!pattern.IsMatch(str))
            {
                return false;
            }
            return true;
        }
    }

}
/// <summary>
/// Author: Peter Bruns
/// Partner: None
/// Date: January 20, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// I, Peter Bruns, certify that I wrote this code from scratch and did not copy it in part or whole from
/// another source. All references used in the completion of the assignment are cited in my README file.
/// 
/// File Contents
/// 
/// Contains tests for ouput of the Evaluate method in the FormulaEvaluator. Also tests that exceptions are
/// thrown where expected for invalid input formulas.
/// </summary>

using FormulaEvaluator;
using SpreadsheetUtilities;

// Make two booleans that will be used to print a message if all tests pass
bool testsPassed = true;
bool exceptionsDetected = true;

// Test Evaluate output is as expected
Console.WriteLine("Testing evaluator output:");

/// <summary>
/// Test division on its own.
/// </summary>
if (Evaluator.Evaluate("5/5", null) != 1)
{
    Console.WriteLine("5/5 failed");
    testsPassed = false;
}

/// <summary>
/// Test multiplication on its own.
/// </summary>
if (Evaluator.Evaluate("5*2", null) != 10)
{
    Console.WriteLine("5*5 failed");
    testsPassed = false;
}

/// <summary>
/// Test addition on its own.
/// </summary>
if (Evaluator.Evaluate("3+8", null) != 11)
{
    Console.WriteLine("3+8 failed");
    testsPassed = false;
}

/// <summary>
/// Test subtraction on its own.
/// </summary>
if (Evaluator.Evaluate("3-8", null) != -5)
{
    Console.WriteLine("3-8 failed");
    testsPassed = false;
}

/// <summary>
/// Test order of operations with multiple operators in same expression
/// </summary>
if (Evaluator.Evaluate("3-8*2", null) != -13)
{
    Console.WriteLine("3-8*2 failed");
    testsPassed = false;
}

/// <summary>
/// Test order of operations with multiple operators in same expression
/// </summary>
if (Evaluator.Evaluate("3-8/2", null) != -1)
{
    Console.WriteLine("3-8/2 failed");
    testsPassed = false;
}

/// <summary>
/// Test order of operations with multiple operators in same expression
/// </summary>
if (Evaluator.Evaluate("3*8-2*4", null) != 16)
{
    Console.WriteLine("3*8-2*4 failed");
    testsPassed = false;
}

/// <summary>
/// Test order of operations with multiple operators in same expression
/// </summary>
if (Evaluator.Evaluate("8/4-2*4+1", null) != -5)
{
    Console.WriteLine("8/4-2*4+1 failed");
    testsPassed = false;
}

/// <summary>
/// Test order of operations with multiple operators in same expression
/// </summary>
if (Evaluator.Evaluate("8+4/2-4*1", null) != 6)
{
    Console.WriteLine("8+4/2-4*1 failed");
    testsPassed = false;
}

/// <summary>
/// Test expression with parentheses
/// </summary>
if (Evaluator.Evaluate("(5+5)", null) != 10)
{
    Console.WriteLine("(5+5) failed");
    testsPassed = false;
}

/// <summary>
/// Test expression with parentheses
/// </summary>
if (Evaluator.Evaluate("(5+5*2)", null) != 15)
{
    Console.WriteLine("(5+5*2) failed");
    testsPassed = false;
}

/// <summary>
/// Test expression with parentheses
/// </summary>
if (Evaluator.Evaluate("((5+5)*2)", null) != 20)
{
    Console.WriteLine("((5+5)*2) failed");
    testsPassed = false;
}

/// <summary>
/// Test expression with parentheses
/// </summary>
if (Evaluator.Evaluate("(7-5)*(4+3)", null) != 14)
{
    Console.WriteLine("(7-5)*(4+3) failed");
    testsPassed = false;
}

/// <summary>
/// Test expression with parentheses
/// </summary>
if (Evaluator.Evaluate("((7-5)*(4+3))-(1+3*2)", null) != 7)
{
    Console.WriteLine("((7-5)*(4+3))-(1+3*2) failed");
    testsPassed = false;
}

/// <summary>
/// Test expression shown in the lecture slides. Parentheses and order of operations
/// </summary>
if (Evaluator.Evaluate("5 + 3 * 7 - 8 / (4 + 3) - 2 / 2", null) != 24)
{
    Console.WriteLine("5 + 3 * 7 - 8 / (4 + 3) - 2 / 2 failed");
    testsPassed = false;
}

/// <summary>
/// Test variables present in formula
/// </summary>
if (Evaluator.Evaluate("X1 + y1", VariableTest) != 7)
{
    Console.WriteLine("X1 + y1 failed");
    testsPassed = false;
}

/// <summary>
/// Test variables present in formula
/// </summary>
if (Evaluator.Evaluate("X1/2", VariableTest) != 2)
{
    Console.WriteLine("X1/2 failed");
    testsPassed = false;
}

/// <summary>
/// Test variables present in formula
/// </summary>
if (Evaluator.Evaluate("X1 * longName1776", VariableTest) != 12)
{
    Console.WriteLine("X1 * longName1776 failed");
    testsPassed = false;
}

/// <summary>
/// Test expression with extra spaces
/// </summary>
if (Evaluator.Evaluate("  3 -   8/2    ", null) != -1)
{
    Console.WriteLine("  3 -   8/2     failed");
    testsPassed = false;
}

/// <summary>
/// Modified example test from the assignment page. Prints a message if all evaluation tests passed.
/// </summary>
if (Evaluator.Evaluate("5+5", null) == 10 && testsPassed)
{
    Console.WriteLine("Happy Day!\nAll evaluator tests passed");
}

Console.WriteLine();
Console.WriteLine();

// Test exceptions thrown when expected
Console.WriteLine("Testing exceptions:");

/// <summary>
/// Test exception thrown by example from instructions with invalid syntax.
/// </summary>
try
{
    Evaluator.Evaluate(" -A- ", null);
    Console.WriteLine("Did not detect invalid syntax: -A-");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception thrown by expression with invalid token.
/// </summary>
try
{
    Evaluator.Evaluate(" 3 + 2 $ 3 ", null);
    Console.WriteLine("Did not detect invalid token: 3 + 2 $ 3");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception thrown by expression with invalid token.
/// </summary>
try
{
    Evaluator.Evaluate(" 7#4 ", null);
    Console.WriteLine("Did not detect invalid token: 7#4");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception thrown by expression with invalid variable name.
/// </summary>
try
{
    Evaluator.Evaluate(" 3 + 2 + XYZ ", null);
    Console.WriteLine("Did not detect invalid variable name: 3 + 2 + XYZ");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception thrown when value stack empty when * on top of operator stack.
/// </summary>
try
{
    Evaluator.Evaluate("*4", null);
    Console.WriteLine("Did not detect value stack empty: *4");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception thrown when value stack empty when / on top of operator stack.
/// </summary>
try
{
    Evaluator.Evaluate("/4", null);
    Console.WriteLine("Did not detect value stack empty: /4");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception thrown when value stack empty when / on top of operator stack and
/// variable found.
/// </summary>
try
{
    Evaluator.Evaluate("/X1", VariableTest);
    Console.WriteLine("Did not detect value stack empty: /X1");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception thrown when value stack empty when * on top of operator stack and
/// variable found.
/// </summary>
try
{
    Evaluator.Evaluate("*y1", VariableTest);
    Console.WriteLine("Did not detect value stack empty: *y1");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test divide by zero exception thrown.
/// </summary>
try
{
    Evaluator.Evaluate("1/0", null);
    Console.WriteLine("Did not detect divide by zero: 1/0");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test divide by zero exception thrown.
/// </summary>
try
{
    Evaluator.Evaluate("1/(8-4*2)", null);
    Console.WriteLine("Did not detect divide by zero: 1/(8-4*2)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test divide by zero exception thrown with variable.
/// </summary>
try
{
    Evaluator.Evaluate("1/z1", VariableTest);
    Console.WriteLine("Did not detect divide by zero: 1/z1");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test delegate method throws exception when variable not found.
/// </summary>
try
{
    Evaluator.Evaluate("A2/4", VariableTest);
    Console.WriteLine("Did not detect variable not found exception: A2/4");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test value stack contains fewer than 2 values + and - found
/// </summary>
try
{
    Evaluator.Evaluate("+1+", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: +1+");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test value stack contains fewer than 2 values + and - found
/// </summary>
try
{
    Evaluator.Evaluate("+1-", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: +1-");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test fewer than 2 values on stack when ) found
/// </summary>
try
{
    Evaluator.Evaluate("(+1-)", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: (+1-)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test fewer than 2 values on stack when ) found
/// </summary>
try
{
    Evaluator.Evaluate("(+1+)", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: (+1+)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception when ( not found where expected after ) found
/// </summary>
try
{
    Evaluator.Evaluate("1+1)", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: 1+1)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test exception when ( not found where expected after ) found
/// </summary>
try
{
    Evaluator.Evaluate("+1+1)", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: +1+1)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test value stack contains fewer than 2 values when * found after parentheses processed
/// </summary>
try
{
    Evaluator.Evaluate("*(1+1)", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: *(1+1)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}


/// <summary>
/// Test value stack contains fewer than 2 values when / found after parentheses processed
/// </summary>
try
{
    Evaluator.Evaluate("/(1+1)", VariableTest);
    Console.WriteLine("Did not detect fewer than 2 values on stack: /(1+1)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test unmatched parentheses throws exception
/// </summary>
try
{
    Evaluator.Evaluate("((1+1)", VariableTest);
    Console.WriteLine("Did not detect invalid syntax: ((1+1)");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test unmatched parentheses throws exception
/// </summary>
try
{
    Evaluator.Evaluate("(1+1))", VariableTest);
    Console.WriteLine("Did not detect invalid syntax: (1+1))");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test value stack contains more than one number when operator stack empty
/// </summary>
try
{
    Evaluator.Evaluate("1*1   2", VariableTest);
    Console.WriteLine("Did not detect invalid syntax: 1*1   2");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test not exactly 1 operator on operator stack or exactly two numbers on value stack when operator stack not empty
/// </summary>
try
{
    Evaluator.Evaluate("+1*12", VariableTest);
    Console.WriteLine("Did not detect invalid syntax: +1*12");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test not exactly 1 operator on operator stack or exactly two numbers on value stack when operator stack not empty
/// </summary>
try
{
    Evaluator.Evaluate("3+-1*12", VariableTest);
    Console.WriteLine("Did not detect invalid syntax: 3+-1*12");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test null input throws exception
/// </summary>
try
{
    Evaluator.Evaluate(null, null);
    Console.WriteLine("Did not throw exception for null expression");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

/// <summary>
/// Test null method throws exception if variable found
/// </summary>
try
{
    Evaluator.Evaluate("x1 + Y1", null);
    Console.WriteLine("Did not throw exception for null input method");
    exceptionsDetected = false;
}
catch (ArgumentException)
{
}

// Prints a message that all exceptions were detected
if (exceptionsDetected)
{
    Console.WriteLine("All exceptions detected.");
}

/// <summary>
/// Method for providing values for variables during testing. Throws ArgumentException if the variable name is not found.
/// </summary>
static int VariableTest(string varName)
{
    if (varName == "X1")
    {
        return 4;
    }
    else if (varName == "y1")
    {
        return 3;
    }
    else if (varName == "z1")
    {
        return 0;
    }
    else if (varName == "longName1776")
    {
        return 3;
    }
    else
    {
        throw new ArgumentException("Variable name not found.");
    }
}

Formula test1 = new Formula("1+1");
Formula test2 = new Formula("1       +1         ");
Console.WriteLine(test1.GetHashCode().ToString());
Console.WriteLine(test2.GetHashCode());

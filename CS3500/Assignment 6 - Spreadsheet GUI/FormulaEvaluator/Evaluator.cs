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
/// Contains methods for evaluating formulas for use in a spreadsheet application. Formulas are entered as
/// strings and the result is given as an integer (Note: uses integer division during evaluation). Does not
/// support unary operations (e.g. -3). Does support variable input through use of a delegate method as an
/// argument to the Evaluate method.
///</summary>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    /// <summary>
    /// Class contains methods to evaluate a formula given as a string. Results are given as integers. Uses
    /// integer division during evaluation and does not support unary operators (e.g. -3). Supports variable
    /// input through delegate usage.
    /// </summary>
    public static class Evaluator
    {
        /// <summary>
        /// Delegate method to use as argument for Evaluate method. The methods used will provide values for
        /// variables included in input formulas.
        /// </summary>
        /// <param name="variable_name">The variable whose value is to be found</param>
        /// <returns>An integer assigned to the variable</returns>
        public delegate int Lookup(String variable_name);

        /// <summary>
        /// Helper method for performing multiplication and division for the Evaluate method.
        /// </summary>
        /// <param name="value">The int that the loop in the Evaluate method currently points to</param>
        /// <param name="values">The stack of values from Evaluate. A value will be popped from
        /// the stack and a resulting value pushed onto it.</param>
        /// <param name="operators">The stack of operators from Evaluate. One operator will be popped.</param>
        /// <exception cref="ArgumentException">Throws an ArgumentException if there are no values
        /// on the value stack</exception>
        /// <exception cref="DivideByZeroException">Throws DivideByZeroException if method attempts
        /// to divide by zero</exception>
        private static void MultiplyOrDivide(int value, Stack<int> values, Stack<String> operators)
        {
            /// Throw an exception if there is no value on the stack to use as an operand
            if (values.Count == 0)
            {
                throw new ArgumentException("There are no values on the stack to use as operands.");
            }
            /// Perform multiplication or division with the input value and the top stack value. Push
            /// the result onto the values stack
            if (operators.Peek() == "*")
            {
                values.Push(values.Pop() * value);
            }
            else if (operators.Peek() == "/")
            {
                /// Throw an exception if attempting to divide by zero
                if (value == 0)
                {
                    throw new ArgumentException("Attempting to divide by zero. Check input formula.");
                }
                values.Push(values.Pop() / value);
            }
            /// Pop the used operator from the operator stack
            operators.Pop();
        }

        /// <summary>
        /// Helper method to perform addition or subtraction for Evaluate method.
        /// </summary>
        /// <param name="values">The stack of values from Evaluate. Two values will be popped and a
        /// resulting value pushed to the stack.</param>
        /// <param name="operators">The stack of operators from Evaluate. One operator will be popped.</param>
        /// <exception cref="ArgumentException">Thows an ArgumentException if there are not at least two
        /// values on the value stack.</exception>
        private static void AddOrSubtract(Stack<int> values, Stack<String> operators)
        {
            ///Throw an exception if there are fewer than two values on the value stack.
            if (values.Count() < 2)
            {
                throw new ArgumentException();
            }
            ///Pop the top two values and perform addition or subtraction based on the top operator stack entry.
            int rightValue = values.Pop();
            int leftValue = values.Pop();
            if (operators.Peek() == "+")
            {
                values.Push(leftValue + rightValue);
            }
            else if (operators.Peek() == "-")
            {
                values.Push(leftValue - rightValue);
            }
            ///Pop the used operator from the stack.
            operators.Pop();
        }

        /// <summary>
        /// This method takes in a string representation of a formula and provides an integer result.
        /// The formulas must only contain +,-,/,*,(, and ) as operators. All input values must be integers,
        /// and the method uses integer division as its only method of division. The method can evaluate 
        /// variables provided by a delegate method passed as an argument. All variables must be formatted as  
        /// one or more letter followed by one or more numbers (e.g. X1, yz12). The method is not compatible 
        /// with unary operations (e.g. -3).
        /// </summary>
        /// <param name="expression">The string representation of a formula</param>
        /// <param name="variableEvaluator">A delegate method used to provide values for variables</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Throws an ArgumentException if the expression is null, contains invalid
        /// tokens, or has invalid syntax (e.g. unmatched parentheses, unused operators or values)</exception>
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            if (expression == null)
            {
                throw new ArgumentException("Input expression is null");
            }
            /// Split the input into the tokens of the expression (from assignment page)
            string[] tokens = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            /// Make a regular expression that will be used to identify variables
            Regex variablePattern = new Regex("[A-Za-z]+[0-9]+");
            /// Make a character array for whitespace for use in trimming tokens
            char[] whitespace = { ' ' };
            /// Initialize a variable to hold values and stacks for operators and operands
            int value;
            Stack<String> operators = new Stack<string>();
            Stack<int> values = new Stack<int>();
            foreach (String tokenWhitespace in tokens)
            {
                /// Trim leading and trailing whitespace from token
                String token = tokenWhitespace.Trim(whitespace);
                /// Skip empty strings
                if (token == "")
                {
                    continue;
                }
                /// If the token is a number, store it in value
                else if (int.TryParse(token, out value))
                {
                    /// Multiply or divide if * or / is on top of the operator stack. Push value
                    /// to value stack otherwise
                    if (operators.Count > 0 && (operators.Peek() == "*" || operators.Peek() == "/"))
                    {
                        MultiplyOrDivide(value, values, operators);
                    }
                    else
                    {
                        values.Push(value);
                    }
                }
                /// Use the regular expression to determine if the token represents a variable
                else if (variablePattern.IsMatch(token))
                {
                    /// Throw an exception if the input method is null
                    if (variableEvaluator == null)
                    {
                        throw new ArgumentException("Input method required for variable use.");
                    }
                    /// Use the delegate method to retrieve the variable's value
                    value = variableEvaluator(token);
                    /// Multiply or divide if * or / is on top of the operator stack. Push value
                    /// to value stack otherwise
                    if (operators.Count > 0 && (operators.Peek() == "*" || operators.Peek() == "/"))
                    {
                        MultiplyOrDivide(value, values, operators);
                    }
                    else
                    {
                        values.Push(value);
                    }
                }
                /// If the token is a + or -, perform addition or subtraction if either of these tokens
                /// is on top of the operator stack then push the current token to the operator stack
                else if (token == "+" || token == "-")
                {
                    if (operators.Count > 0 && (operators.Peek() == "+" || operators.Peek() == "-"))
                    {
                        AddOrSubtract(values, operators);
                    }
                    operators.Push(token);
                }
                /// If the token is a *, /, or ( push it to the operator stack
                else if (token == "*" || token == "/")
                {
                    operators.Push(token);
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                /// If the token is an ), perform addition or subtraction if + or - is on top of
                /// operator stack
                else if (token == ")")
                {
                    if (operators.Count > 0 && (operators.Peek() == "+" || operators.Peek() == "-"))
                    {
                        AddOrSubtract(values, operators);
                    }
                    /// The next operator on the stack should be a (, pop it or throw an exception
                    /// if the ( is not there
                    if (operators.Count > 0 && operators.Peek() == "(")
                    {
                        operators.Pop();
                    }
                    else
                    {
                        throw new ArgumentException("Invalid syntax. Expected '(' not found.");
                    }
                    /// Perform multiplication or division if the next operator on the stack is a *
                    /// or /. Throw an exception if there are not two values on the stack to use as operands.
                    if (operators.Count > 0 && (operators.Peek() == "*" || operators.Peek() == "/"))
                    {
                        if (values.Count < 2)
                        {
                            throw new ArgumentException("There are not enough operands available for calculation.");
                        }
                        MultiplyOrDivide(values.Pop(), values, operators);
                    }
                }
                /// Throw an exception if the token is not valid
                else
                {
                    throw new ArgumentException("Invalid token: " + token);
                }
            }
            /// If there are no operators remaining on the stack, return the only value left on the values
            /// stack or throw an exception if there is not exactly one value remaining.
            if (operators.Count == 0)
            {
                if (values.Count == 1)
                {
                    return values.Pop();
                }
                else
                {
                    throw new ArgumentException("Not exactly one value remaining after operator stack empty");
                }
            }
            /// If there is one operator remaining on the stack, it should be a + or - and there should be exactly
            /// two values remaining on the value stack. Return the sum or difference if these conditions are met, 
            /// throw an exception otherwise.
            else if (operators.Count == 1)
            {
                if (values.Count() != 2)
                {
                    throw new ArgumentException("There were " + values.Count() + " values remaining. Exactly 2 expected.");
                }
                if (operators.Peek() == "+" || operators.Peek() == "-")
                {
                    AddOrSubtract(values, operators);
                    return values.Pop();
                }
                else
                {
                    throw new ArgumentException("Invalid syntax. There were " + operators.Count() + " operators and " + values.Count() + " values remaining at the end. Expected 1 and 2 respectively.");
                }
            }
            /// Throw an exception if the number of operators remaining is not zero or one.
            else
            {
                throw new ArgumentException("There are " + operators.Count() + " operators remaining. Expected 0 or 1.");
            }
        }
    }
}
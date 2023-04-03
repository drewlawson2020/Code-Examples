using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/// <summary> 
/// Author:    Drew 
/// Partner:   None
/// Date:      21-Jan-2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Drew Lawson - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Drew Lawson, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    This file contains all of the necessary methods and functions to allow the program to calculate out
///    a simple equation/formula, using integers, basic arithmetic operators, and variables.
/// </summary>
namespace FormulaEvaluator
{
    public class Evaluator
    {
        public delegate int Lookup(String variable_name);
        /// <summary>
        ///   The function is self explanatory for the most part. Takes in an expression represented by String,
        ///   where it is split up using Regex into smaller pieces. Then, using two stacks, it offers operators and integers
        ///   to each respective stack. These are then used in a series of if/else statements that determine how to
        ///   calculate the equation based on what is currently being added to the stack.
        /// 
        /// </summary>
        /// <param name="expression"> expression represents the equation, expressed with a String. </param>
        /// <param name="variableEvaluator"> variableEvaluator represents the delegate to be passed off when determining a variable </param>
        /// <returns> The final calculated result from given expression and delegate as an int. </returns>
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            Stack<String> operator_stack = new Stack<String>();
            Stack<int> value_stack = new Stack<int>();
            ///Split expression up into smaller Strings in an array.
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)|(\\s+)");

            foreach (String substring in substrings)
            {
                /// Skips potential "" results in substring.
                if (string.IsNullOrEmpty(substring))
                {
                    continue;
                }
                /// Attempts to see if String is an integer using TryParse.
                if (int.TryParse(substring, out int trash))
                {
                    Mult_Or_Div_Check(operator_stack, value_stack, substring);
                }

                /// If no integer is parsed, attempts to see if it could be a variable.
                else if (Char.IsLetter(substring[0]))
                {
                    /// Bool to catch exception and verify
                    bool finishedWithLetters = false;

                    /// Index starts at 1 as we checked index 0 aleady
                    for (int i = 1; i < (substring.Length); i++)
                    {
                        if (Char.IsLetter(substring[i]))
                        {
                            /// Catch to see if a letter is found after number(s) have been found in variable, then throws an ArgumentException.
                            if (finishedWithLetters)
                            {
                                throw new ArgumentException();
                            }
                            continue;
                        }
                        /// Parses for integers if Char cannot be found, and marks finishedWithLetters as true. If something else is detected, throws ArgumentException.
                        else if (int.TryParse(substring[i].ToString(), out int trash2))
                        {
                            finishedWithLetters = true;
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    } /// If a letter was never found, throws ArgumentException.
                    if (!finishedWithLetters)
                    {
                        throw new ArgumentException();
                    }

                    int temp_substring = variableEvaluator(substring);

                    Mult_Or_Div_Check(operator_stack, value_stack, temp_substring.ToString());
                }

                /// Checks for + or - operation.
                if (substring.Equals("+") || substring.Equals("-"))
                {
                    /// Verifies that the operation can be performed first, by checking if the operator stack has at least 1 operator, and the value has at least 2 or more.
                    if (operator_stack.Count != 0 && value_stack.Count >= 2)
                    {
                        if (operator_stack.Peek().Equals("+") || operator_stack.Peek().Equals("-"))
                        {
                            Add_or_Subtract(operator_stack, value_stack);
                        }
                    }
                    operator_stack.Push(substring);

                /// Checks for (, *, or / operators, then pushes them onto stack.)
                }
                if (substring.Equals("(") || substring.Equals("*") || substring.Equals("/"))
                {
                    operator_stack.Push(substring);
                }

                /// Checks for ")", then goes through processes to determine if expression is valid.
                if (substring.Equals(")"))
                {
                    /// Ensures that expression is valid by checking if both stacks are empty or not. Throws ArgumentException otherwise.
                    if (operator_stack.Count == 0 || value_stack.Count == 0)
                    {
                        throw new ArgumentException();
                    }
                    /// Checks Addition or Subtraction can be performed with 2 or more values in stack. If not, moves onto see if "(" is present.
                    if (operator_stack.Peek().Equals("+") || operator_stack.Peek().Equals("-") && value_stack.Count >= 2)
                    {
                        Add_or_Subtract(operator_stack, value_stack);
                        /// Properly removes "(". Throws ArgumentException if not found.
                        if (operator_stack.Peek().Equals("("))
                        {
                            operator_stack.Pop();
                            /// Another check to ensure that there are operator members in the stack. Throws ArgumentException if none are present.
                            if (operator_stack.Count != 0)
                            {
                                if (operator_stack.Peek().Equals("*") || operator_stack.Peek().Equals("/") && value_stack.Count >= 2)
                                {
                                    Multiply_Or_Divide_With_Parentheses(operator_stack, value_stack, substring);
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    /// Check for when "(" is present and verifies that there are 2 or more values in stack.
                    else if (operator_stack.Peek().Equals("(") && value_stack.Count >= 2)
                    {
                        /// Removes "("
                        operator_stack.Pop();
                        if (operator_stack.Count != 0)
                        {
                            /// Same check to ensure that there are operator members in the stack. Throws ArgumentException if none are present.
                            if (operator_stack.Peek().Equals("*") || operator_stack.Peek().Equals("/") && value_stack.Count >= 2)
                            {
                                Multiply_Or_Divide_With_Parentheses(operator_stack, value_stack, substring);
                            }
                        }
                    }
                }
            }
            /// After loops are finished, attempts to get a finalized result. The first condition is met if the operator stack is empty.
            if (!operator_stack.TryPeek(out String result))
            {
                /// If one value remains, returns it. Otherwise, throws ArgumentException. 
                if (value_stack.Count == 1)
                {
                    return value_stack.Pop();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            /// The second condition is met if there is exactly one operator and two values left in each respective stack, and that operator is a + or -. Throws ArgumentException if there is no + or -, or if exact requirements are not met for stacks.
            else if (operator_stack.Peek().Equals("+") || operator_stack.Peek().Equals("-") && operator_stack.Count == 1 && value_stack.Count == 2)
            {
                Add_or_Subtract(operator_stack, value_stack);
                return value_stack.Pop();

            }
            else
            {
                throw new ArgumentException();
            }

        }
        /// <summary>
        /// Method for addtion or subtraction. Method pops the first two values off of the value stack to be calculated. Then, the operator symbol is checked, and the appropriate equation is applied
        /// to the integers. Then, the result is pushed onto value stack.
        /// </summary>
        /// <param name="operator_stack"> operator_stack is the operator stack that contains all of the current stored operators</param>
        /// <param name="value_stack"> value_stack is the value stack that contains all of the current stored values</param>
        static void Add_or_Subtract(Stack<String> operator_stack, Stack<int> value_stack)
        {
            int popped_value_2 = value_stack.Pop();
            int popped_value_1 = value_stack.Pop();
            int _result = 0;
            String operator_symbol = operator_stack.Pop();
            if (operator_symbol.Equals("+"))
            {
                _result = popped_value_1 + popped_value_2;
            }
            else if (operator_symbol.Equals("-"))
            {
                _result = popped_value_1 - popped_value_2;
            }
            value_stack.Push(_result);
        }
        /// <summary>
        /// Method for multiplication or division. Method pops the first value off of the value stack to be calculated with the parsed int value take from the substring. Then, the operator symbol is checked if it exists, and the appropriate equation is applied
        /// to the integers if possible. Then, the result is pushed onto value stack. Division throws a DivideByZeroException if the parsed int is a 0.
        /// </summary>
        /// <param name="operator_stack"> operator_stack is the operator stack that contains all of the current stored operators </param>
        /// <param name="value_stack"> value_stack is the value stack that contains all of the current stored values </param>
        /// <param name="substring"> substring is a string with the current operator substring on it </param>
        /// <exception cref="DivideByZeroException"> Thrown when the popped int divisor is a 0. </exception>
        static void Multiply_Or_Divide(Stack<String> operator_stack, Stack<int> value_stack, String substring)
        {
            String operator_symbol = operator_stack.Pop();
            int popped_value = value_stack.Pop();
            int _result = 0;
            if (operator_symbol.Equals("*"))
            {
                _result = int.Parse(substring) * popped_value;
                value_stack.Push(_result);
            }
            else if (operator_symbol.Equals("/"))
            {
                if (int.Parse(substring) != 0)
                {
                    _result = popped_value / int.Parse(substring);
                }
                else
                {
                    throw new DivideByZeroException("Attempted to divide by zero");
                }

                value_stack.Push(_result);
            }
        }
        /// <summary>
        /// Similar to Multiply_Or_Divide method, but pops two variables instead to account for the different process with parentheses. 
        /// </summary>
        /// <param name="operator_stack"> See Multiply_Or_Divide</param>
        /// <param name="value_stack"> See Multiply_Or_Divide</param>
        /// <param name="substring"> See Multiply_Or_Divide</param>
        /// <exception cref="DivideByZeroException"> Thrown when the popped int divisor is a 0. </exception>
        static void Multiply_Or_Divide_With_Parentheses(Stack<String> operator_stack, Stack<int> value_stack, String substring)
        {
            int _result = 0;
            int popped_value_2 = value_stack.Pop();
            int popped_value_1 = value_stack.Pop();
            String operator_symbol = operator_stack.Pop();
            if (operator_symbol.Equals("*"))
            {
                _result = popped_value_1 * popped_value_2;
                value_stack.Push(_result);
            }
            else if (operator_symbol.Equals("/"))
            {
                if (int.Parse(substring) != 0)
                {
                    _result = popped_value_1 / popped_value_2;
                }
                else
                {
                    throw new DivideByZeroException("Attempted to divide by zero");
                }

                value_stack.Push(_result);
            }
        }
        /// <summary>
        /// Checks if Multiplication or Division can take place when a new substring integer or variable is being added to stack.
        /// If a * or / is found, does the appropriate operation. If not, just simply pushes the parsed substring into into the value stack.
        /// </summary>
        /// <param name="operator_stack"> operator_stack is the operator stack that contains all of the current stored operators </param>
        /// <param name="value_stack"> value_stack is the value stack that contains all of the current stored values</param>
        /// <param name="substring"> substring is a string with the current operator substring on it</param>
        static void Mult_Or_Div_Check(Stack<String> operator_stack, Stack<int> value_stack, String substring)
        {
            if (operator_stack.Count == 0)
            {
                value_stack.Push(int.Parse(substring));
            }
            else if (operator_stack.Peek().Equals("*") || operator_stack.Peek().Equals("/"))
            {
                Multiply_Or_Divide(operator_stack, value_stack, substring);
            }
            else
            {
                value_stack.Push(int.Parse(substring));
            }
        }

    }
}




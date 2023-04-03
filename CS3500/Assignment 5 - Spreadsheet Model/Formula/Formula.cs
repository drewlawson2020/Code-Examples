
// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens

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
///    This file contains all of the necessary methods and functions to allow the program to calculate out
///    a simple equation/formula, using doubles, basic arithmetic operators, and variables. It also now has
///    the capabilites to check a formula to ensure that it is valid before it is actually calculated. 
///    Further, the way in which variabls are introduced allow for normalization and assignment of values.
///    In short, this project aims to deprecate the FormulaEvaluator by acting as a more thorough and feature-rich
///    evaluation algorithm. In a typical use-case, a new Formula class is defined by the user, containing
///    a string representation of the equation. From there, the Formula class is checked to ensure that it is correct.
///    From there, the user can then use the Formula to Evaluate it, and also define a normalization and values
///    of variables. This class also contains other functions to allow the user and programmer to test
///    functionality of the overall class.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {

        private List<string> parsedTokens;
        private HashSet<string> vars;
        private string formula;
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            if (formula == String.Empty)
            {
                throw new FormulaFormatException("Error: Formula is empty.");
            }
            /// Set up list and HashSet
            parsedTokens = new List<string>(GetTokens(formula));
            vars = new HashSet<string>();
          

            double number;

            string first_ = parsedTokens.First<string>();
            string last_ = parsedTokens.Last<string>();
            
            if (!first_.Equals("(") && !Double.TryParse(first_, out number) && !Is_var(first_))
            {
                throw new FormulaFormatException("Error: First token in formula is not a (, number, or variable.");
            }

            if (!last_.Equals(")") && !Double.TryParse(last_, out number) && !Is_var(last_))
            {
                throw new FormulaFormatException("Error: Last token in formula is not a ), number, or variable.");
            }

            string previous_token = "";

            int left_paren = 0;
            int right_paren = 0;

            /// Loops through each token and determines if they're a valid symbol and also correctly formatted. Also counts the number of
            /// left and right parentheses to ensure the correct amount. 
            for (int i = 0; i < parsedTokens.Count; i++)
            {
                string curr = parsedTokens[i];

                if (curr.Equals("("))
                {
                    left_paren++;                                   
                }

                else if (curr.Equals(")"))
                {
                    right_paren++;
                }

                else if (Double.TryParse(curr, out number))
                {
                    parsedTokens[i] = number.ToString();
                } 
                /// If token is not a +, -, *, /, or variable, throws exception
                else if (!Is_var(curr) && !curr.Equals("+") && !curr.Equals("-") && !curr.Equals("*") && !curr.Equals("/"))
                { 
                throw new FormulaFormatException("Error: Invalid symbol."); 
                }

                if (previous_token.Equals("(") || previous_token.Equals("+") || previous_token.Equals("-") || previous_token.Equals("*") || previous_token.Equals("/"))
                {
                    if (!(Double.TryParse(curr, out number) || Is_var(curr) || curr.Equals("(")))
                    {
                        throw new FormulaFormatException("Error: Must be a number, variable, or ( after (, +, -, *, /");
                    }
                }
                else if (previous_token.Equals(")") || Double.TryParse(previous_token, out number) || Is_var(previous_token))
                {
                    if (!(curr.Equals(")") || curr.Equals("+") || curr.Equals("-") || curr.Equals("*") || curr.Equals("/")))
                    {
                        throw new FormulaFormatException("Error: Must be a ), +, -, *, or / agter ), number, or variable");
                    }
                
                }
                /// Stores previous token for loop.
                previous_token = curr;          

            }

            /// Checks if equal to ensure correctness.
            if (left_paren != right_paren)
            {
                throw new FormulaFormatException("Error: The number of left and right parentheses are not equal.");
            }
            
            /// After everything has been verified, it is now safe to parse for variables and normalize them.
            for (int i = 0; i < parsedTokens.Count; i++)
            {
                string variable = parsedTokens[i];
                if (Is_var(variable))
                {
                    if (!(Is_var(normalize(variable))) || !(isValid(normalize(variable))))
                    {
                        throw new FormulaFormatException("");
                    }
                    else
                    {
                        parsedTokens[i] = normalize(variable);

                        vars.Add(parsedTokens[i]);
                    }

                }
            }

        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// 
        /// 
        /// <return> Returns the final result of the overall formula, or FormulaError during the calculation.</return>
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {

            string[] substrings = parsedTokens.Cast<string>().ToArray<string>();

            Stack<String> operator_stack = new Stack<String>();
            Stack<double> value_stack = new Stack<double>();


            foreach (String substring in substrings)
            {
                /// Attempts to see if String is an integer using TryParse.
                if (Double.TryParse(substring, out Double trash))
                {
                    Object returned_value = new();
                    returned_value = Mult_Or_Div_Check(operator_stack, value_stack, substring);
                    if (returned_value is FormulaError)
                    {
                        return returned_value;
                    }
                }

                /// If no integer is parsed, attempts to see if it could be a variable.
                else if (Is_var(substring))
                {
                    Double temp_substring;
                    try
                    {
                       temp_substring = lookup(substring);
                    }
                    catch
                    {
                       return new FormulaError("No defined varaible");
                    }

                    Object returned_value = new();

                    returned_value = Mult_Or_Div_Check(operator_stack, value_stack, temp_substring.ToString());

                    if (returned_value is FormulaError)
                    {
                        return returned_value;
                    }
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
                    /// Checks Addition or Subtraction can be performed with 2 or more values in stack. If not, moves onto see if "(" is present.
                    if (operator_stack.Peek().Equals("+") || operator_stack.Peek().Equals("-") && value_stack.Count >= 2)
                    {
                        Add_or_Subtract(operator_stack, value_stack);

                        if (operator_stack.Peek().Equals("("))
                        {
                            operator_stack.Pop();
                            /// Another check to ensure that there are operator members in the stack. Throws ArgumentException if none are present.
                            if (operator_stack.Count != 0)
                            {
                                if (operator_stack.Peek().Equals("*") || operator_stack.Peek().Equals("/") && value_stack.Count >= 2)
                                {
                                    Object returned_value = new();
                                    returned_value = Multiply_Or_Divide_With_Parentheses(operator_stack, value_stack, substring);

                                    if (returned_value is FormulaError)
                                    {
                                        return returned_value;
                                    }

                                }
                            }
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
                                Object returned_value = new();
                                returned_value = Multiply_Or_Divide_With_Parentheses(operator_stack, value_stack, substring);
                                if (returned_value is FormulaError)
                                {
                                    return returned_value;
                                }
                            }
                        }
                    }
                    else
                    {
                        operator_stack.Pop();
                    }
                }
            }
            /// After loops are finished, attempts to get a finalized result. The first condition is met if the operator stack is empty.
            if (!operator_stack.TryPeek(out _))
            {
                /// If one value remains, returns it. Otherwise, throws ArgumentException. 
                if (value_stack.Count == 1)
                {
                    return (object)value_stack.Pop();
                }
            }
            /// The second condition is met if there is exactly one operator and two values left in each respective stack, and that operator is a + or -. Throws ArgumentException if there is no + or -, or if exact requirements are not met for stacks.
            else if (operator_stack.Peek().Equals("+") && operator_stack.Count == 1 && value_stack.Count == 2 || operator_stack.Peek().Equals("-") && operator_stack.Count == 1 && value_stack.Count == 2)
            {
                Add_or_Subtract(operator_stack, value_stack);
                return (object)value_stack.Pop();

            }
            return -1;
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            return new HashSet<string>(vars);
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            string formula = "";
            for (int i = 0; i < parsedTokens.Count; i++)
            {
                formula += parsedTokens[i];
            }
            return formula;
        }

        /// <summary>
        ///  <change> make object nullable </change>
        ///
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object? obj)
        {
            Formula formula = (Formula)obj;
            /// Cycles through the tokens to check if it is equal
            for (int i = 0; i < this.parsedTokens.Count; i++)
            {
                string this_curr = this.parsedTokens[i];
                string formula_curr = formula.parsedTokens[i];

                double this_number;
                double formula_number;

                if ((Double.TryParse(this_curr, out this_number)) && (Double.TryParse(formula_curr, out formula_number)))
                {
                    if (this_number != formula_number)
                        return false;
                }
                else
                {
                    if (!(this_curr.Equals(formula_curr)))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        ///   <change> We are now using Non-Nullable objects.  Thus neither f1 nor f2 can be null!</change>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// 
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            return (f1.Equals(f2));
        }

        /// <summary>
        ///   <change> We are now using Non-Nullable objects.  Thus neither f1 nor f2 can be null!</change>
        ///   <change> Note: != should almost always be not ==, if you get my meaning </change>
        ///   Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return (!(f1 == f2));
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode(); ;
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn'curr
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don'curr consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }

        /// <summary>
        /// Method for addtion or subtraction. Method pops the first two values off of the value stack to be calculated. Then, the operator symbol is checked, and the appropriate equation is applied
        /// to the integers. Then, the result is pushed onto value stack.
        /// </summary>
        /// <param name="operator_stack"> operator_stack is the operator stack that contains all of the current stored operators</param>
        /// <param name="value_stack"> value_stack is the value stack that contains all of the current stored values</param>
        static void Add_or_Subtract(Stack<String> operator_stack, Stack<Double> value_stack)
        {
            Double popped_value_2 = value_stack.Pop();
            Double popped_value_1 = value_stack.Pop();
            Double _result = 0;
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
        /// Method for multiplication or division. Method pops the first value off of the value stack to be calculated with the parsed doubles value take from the substring. 
        /// Then, the operator symbol is checked if it exists, and the appropriate equation is applied to the doubles if possible. Then, the result is pushed onto value stack. 
        /// Division returns a FormulaError if the parsed divisor double is a 0.
        /// </summary>
        /// <param name="operator_stack"> operator_stack is the operator stack that contains all of the current stored operators </param>
        /// <param name="value_stack"> value_stack is the value stack that contains all of the current stored values </param>
        /// <param name="substring"> substring is a string with the current operator substring on it </param>
        /// <returns> An object type that either contains the final result, or a formula error. </returns>
        private object Multiply_Or_Divide(Stack<String> operator_stack, Stack<double> value_stack, String substring)
        {
            String operator_symbol = operator_stack.Pop();
            double popped_value = value_stack.Pop();
            object _result = 0.0;
            if (operator_symbol.Equals("*"))
            {
                _result = Double.Parse(substring) * popped_value;
                value_stack.Push((double)_result);
            }
            else if (operator_symbol.Equals("/"))
            {
                if (Double.Parse(substring) != 0)
                {
                    _result = popped_value / Double.Parse(substring);
                }
                else
                {
                    return new FormulaError("Divide By Zero");
                }

                value_stack.Push((double)_result);
            }
            return _result;
        }
        /// <summary>
        /// Similar to Multiply_Or_Divide method, but pops two variables instead to account for the different process with parentheses. 
        /// </summary>
        /// <param name="operator_stack"> See Multiply_Or_Divide</param>
        /// <param name="value_stack"> See Multiply_Or_Divide</param>
        /// <param name="substring"> See Multiply_Or_Divide</param>
        /// <returns> An object type that either contains the final result, or a formula error. </returns>
        private object Multiply_Or_Divide_With_Parentheses(Stack<String> operator_stack, Stack<double> value_stack, String substring)
        {
            object _result = 0.0;
            double popped_value_2 = value_stack.Pop();
            double popped_value_1 = value_stack.Pop();
            String operator_symbol = operator_stack.Pop();
            if (operator_symbol.Equals("*"))
            {
                _result = popped_value_1 * popped_value_2;
                value_stack.Push((double)_result);
            }
            else if (operator_symbol.Equals("/"))
            {
                if (popped_value_2 != 0)
                {
                    _result = popped_value_1 / popped_value_2;
                }
                else
                {
                    return new FormulaError("Divide By Zero");
                }

                value_stack.Push((double)_result);
            }
            return _result;
        }
        /// <summary>
        /// Checks if Multiplication or Division can take place when a new substring integer or variable is being added to stack.
        /// If a * or / is found, does the appropriate operation. If not, just simply pushes the parsed substring into into the value stack.
        /// </summary>
        /// <param name="operator_stack"> operator_stack is the operator stack that contains all of the current stored operators </param>
        /// <param name="value_stack"> value_stack is the value stack that contains all of the current stored values</param>
        /// <param name="substring"> substring is a string with the current operator substring on it</param>
        /// <returns> An object type that contains the final result. </returns>
        private object Mult_Or_Div_Check(Stack<String> operator_stack, Stack<Double> value_stack, String substring)
        {
            if (operator_stack.Count == 0)
            {
                value_stack.Push(Double.Parse(substring));
                return substring;
            }
            else if (operator_stack.Peek().Equals("*") || operator_stack.Peek().Equals("/"))
            {
                return Multiply_Or_Divide(operator_stack, value_stack, substring);
            }
            else
            {
                value_stack.Push(Double.Parse(substring));
                return substring;
            }
        }
        /// <summary>
        /// Verifies if the variable is valid through regex. It checks for a number of chars from a-z and A-Z
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Returns true if formatted correctly, false otherwise.</returns>
        public static Boolean Is_var(String token)
        {
            return Regex.IsMatch(token, @"[A-Za-z_](?: [A-Za-z_]|\d)*$", RegexOptions.Singleline);
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }


    
}



// <change>
//   If you are using Extension methods to deal with common stack operations (e.g., checking for
//   an empty stack before peeking) you will find that the Non-Nullable checking is "biting" you.
//
//   To fix this, you have to use a little special syntax like the following:
//
//       public static bool OnTop<T>(this Stack<T> stack, T element1, T element2) where T : notnull
//
//   Notice that the "where T : notnull" tells the compiler that the Stack can contain any object
//   as long as it doesn'curr allow nulls!
// </change>
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
/// Contains a definitions for construction of a Formula object and associated methods. Each Formula has an 
/// associated expression that can contain string representations of doubles, variables, and the operators +, 
/// -, *, /, (, and ). Variable names consist of an underscore or letter followed by zero or more letters,
/// underscores, or floating point digits. Any spaces in the expression are treated as delimiters for tokens. The 
/// Formula constructor takes the expression, a normalizer for variable names, and a validator for variable names. The
/// normalizer can alter the string representation of the variables, and the validator ensures that the names
/// are acceptable by the definition in the validator. Methods associated with each Formula object can evaluate
/// the expression, return a list of variables, return a string representation of the normalized expression, check
/// for equality of two Formulas, and produce a hash code for the Formula. Below the Formula class, there are
/// definitions for FormulaFormatException and a FormulaError struct.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Extensions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Contains a definitions for construction of a Formula object and associated methods. Each Formula has an 
    /// associated expression that can contain string representations of doubles, variables, and the operators +, 
    /// -, *, /, (, and ). Variable names consist of an underscore or letter followed by zero or more letters,
    /// underscores, or floating point digits. Any spaces in the expression are treated as delimiters for tokens. 
    /// The Formula constructor takes the expression, a normalizer for variable names, and a validator for variable names. 
    /// The normalizer can alter the string representation of the variables, and the validator ensures that the names
    /// are acceptable by the definition in the validator. Methods associated with each Formula object can evaluate
    /// the expression, return a list of variables, return a string representation of the normalized expression, check
    /// for equality of two Formulas, and produce a hash code for the Formula.
    /// </summary>
    public class Formula
    {
        // Fields to hold the input expression and a list of the normalized tokens it contains
        private String expression;
        private List<String> tokens;

        /// <summary>
        /// Creates a Formula from a string that consists of an expression that follows the
        /// pattern described in the class header. For this constructor, the normalizer is
        /// an identity function that does not alter the expression. The validator will 
        /// return true no matter what. The expression is passed to the other constructor with
        /// this normalizer and validator, then the expression is parsed to ensure it is valid.
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an expression that follows the
        /// pattern described in the class header. The normalize Func argument is a function
        /// that takes the expression string and returns a new representation that alters
        /// variables as defined in normalize. The isValid Func checks if the normalized
        /// variables follow the pattern that it defines. For example, if the expression is
        /// "x + Y" and normalize converts letters to uppercase, the new expression would be
        /// "X + Y". If isValid expects letters to be uppercase, then the non-normalized expression
        /// would result in a FormulaFormatException while the normalized version would not. After
        /// the expression is normalized and validated, the tokens in the expression are parsed to
        /// ensure that the expression is syntactically valid.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            // Normalize the variables in expression then make sure they are valid
            expression = normalize(formula);
            foreach (String variable in this.GetVariables())
            {
                if (!isValid(variable))
                {
                    throw new FormulaFormatException("The formula contains invalid variables after normalization.");
                }
            }
            // Call helper methods to get a list of tokens in the expression and parse them. This will
            // throw  FormulaFormatException if the expression is invalid
            tokens = ParseTokens(GetTokens(expression));
        }

        /// <summary>
        /// Parses the tokens of the input expression to ensure that it is syntactically valid. If any invalid
        /// token or invalid ordering is detected, throws a FormulaFormatException. This method converts any
        /// string representation of doubles/floating point digits into a double then back to a string to normalize
        /// any input digit format (ex. 2.01 and 2.0100 should have the same representation afterwards). The output
        /// token list will be used in Evaluate and ToString.
        /// </summary>
        /// <param name="tokenCollection">The input IEnumerable collection of tokens</param>
        /// <returns>A list of valid tokens with digits normalized by parsing string represenation to a double
        /// then conversion of the double back to a string</returns>
        /// <exception cref="FormulaFormatException">If an invalid token or token order is detected or the 
        /// expression contains no tokens</exception>
        private List<String> ParseTokens(IEnumerable<String> tokenCollection)
        {
            List<String> tokens = new List<String>();
            // Regular expression used to check for variables
            Regex variablePattern = new Regex(("^[a-zA-Z_][A-Za-z_0-9]*$"));
            // Double to hold parsed digit values
            Double value;
            if (tokenCollection.Count() == 0)
            {
                throw new FormulaFormatException("At least one token required. Please enter a new formula.");
            }
            // Variables to track parentheses
            int leftPArentheses = 0;
            int rightPArentheses = 0;
            // The set of valid operators (not including parentheses)
            HashSet<String> operators = new HashSet<String> { "+", "-", "*", "/" };
            // The following two variables are used to ensure that the expression has a valid
            // order of tokens
            bool followingOpenParenthesisOrOperator = false;
            bool followingVariableNumberOrCloseParenthesis = false;
            String firstToken = tokenCollection.First();
            String lastToken = tokenCollection.Last();
            // The first token must be a digit, variable, or (. Throw an exception if any other token starts the
            // expression
            if (firstToken != "(" && !Double.TryParse(firstToken, out value) && !variablePattern.IsMatch(firstToken))
            {
                throw new FormulaFormatException("The first token must be a number, variable, or left parenthesis. Please re-enter formula.");
            }
            // The last token must be a digit, variable, or ). Throw an exception if any other token ends the
            // expression
            if (lastToken != ")" && !Double.TryParse(lastToken, out value) && !variablePattern.IsMatch(lastToken))
            {
                throw new FormulaFormatException("The last token must be a number, variable, or right parenthesis. Please re-enter formula.");
            }
            foreach (String token in tokenCollection)
            {
                // Check if the token is an operator. If it is and does not follow a digit or variable, throw an exception.
                // Add token to the list otherwise.
                if (operators.Contains(token))
                {
                    if (followingOpenParenthesisOrOperator)
                    {
                        throw new FormulaFormatException("There is an operator following an opening parenthesis or other operator. Please check that only opening parentheses, variables, or numbers follow operators and opening parentheses.");
                    }
                    else
                    {
                        tokens.Add(token);
                        // Indicate that the last parsed token was an operator
                        followingOpenParenthesisOrOperator = true;
                        followingVariableNumberOrCloseParenthesis = false;
                    }
                }
                // Check if the token is a digit. If it is and does not follow an operator or (, throw an exception.
                // Add token to the list otherwise.
                else if (Double.TryParse(token, out value))
                {
                    if (followingVariableNumberOrCloseParenthesis)
                    {
                        throw new FormulaFormatException("There is a number following a variable, number, or close parenthesis. Please ensure that only operators or close parentheses follow variables, numbers, or closing parentheses.");
                    }
                    else
                    {
                        // Convert the digit back to a string to normalize the floating point representation
                        // before adding it to the token list.
                        tokens.Add(value.ToString());
                        // Indicate that the last parsed token was a digit
                        followingOpenParenthesisOrOperator = false;
                        followingVariableNumberOrCloseParenthesis = true;
                    }
                }
                // Check if the token is a variable. If it is and does not follow an operator or (, throw an exception.
                // Add token to the list otherwise.
                else if (variablePattern.IsMatch(token))
                {
                    if (followingVariableNumberOrCloseParenthesis)
                    {
                        throw new FormulaFormatException("There is variable following a variable, number, or close parenthesis. Please ensure variables only follow operators or open parentheses.");
                    }
                    else
                    {
                        tokens.Add(token);
                        // Indicate that the last parsed token was a variable
                        followingOpenParenthesisOrOperator = false;
                        followingVariableNumberOrCloseParenthesis = true;
                    }
                }
                // If the token is a ( and does not follow an operator or other (, throw an exception. Add it to
                // the list otherwise.
                else if (token == "(")
                {
                    if (followingVariableNumberOrCloseParenthesis)
                    {
                        throw new FormulaFormatException("There is an open parenthesis following a variable, number, or close parenthesis. Please ensure that opening parentheses only follow operators or other opening parentheses.");
                    }
                    else
                    {
                        tokens.Add(token);
                        // Increase ( count in dictionary and indicate that the last token was a (
                        leftPArentheses++;
                        followingOpenParenthesisOrOperator = true;
                        followingVariableNumberOrCloseParenthesis = false;
                    }
                }
                // If the token is a ( and does not follow a digit, variable, or other ), throw an exception. Add it to
                // the list otherwise.
                else if (token == ")")
                {
                    if (followingOpenParenthesisOrOperator)
                    {
                        throw new FormulaFormatException("There is a closing parenthesis following an open parenthesis or operator. Please ensure that closing parentheses only follow variables, numbers, or other closing parentheses.");
                    }
                    else
                    {
                        // Increase the ) count in the dictionary. If the number of ) exceeds the number of ( at any
                        // point, throw an exception.
                        rightPArentheses++;
                        if (rightPArentheses > leftPArentheses)
                        {
                            throw new FormulaFormatException("There are more closing parentheses than opening parentheses. Please ensure that the number of closing parentheses never exceeds the number of opening parentheses.");
                        }
                        tokens.Add(token);
                        // Indicate that the last token was a )
                        followingOpenParenthesisOrOperator = false;
                        followingVariableNumberOrCloseParenthesis = true;
                    }
                }
                // Throw a formula exception for invalid tokens
                else
                {
                    throw new FormulaFormatException("Unrecognized token: " + token + ". Please ensure that only valid tokens as described in the class description are used.");
                }
            }
            // If parentheses are not balanced at the end, throw an exception
            if (rightPArentheses != leftPArentheses)
            {
                throw new FormulaFormatException("Parentheses are unbalaned. Please check that all parentheses are matched and reenter formula.");
            }
            return tokens;
        }

        /// <summary>
        /// Evaluates the normalized, validated expression defined by the Formula input. Expressions
        /// may contain string representations of floating point digits, variables as defined by the
        /// class description, and the operators +, -, *, /, (, and ). Because the constructor ensures
        /// that the expression is syntactically valid, this method will never throw an exception for
        /// an invalid token or operator ordering. The method will return a FormulaError if division 
        /// by zero occurs or if the expression contains an undefined variable. Variables are defined
        /// by the lookup method passed as an argument.
        /// </summary>
        /// <param name="lookup">A function that defines the value for variables.</param>
        /// <returns>A double if the formula is evaluated. A FormulaError if division by zero occurs or
        /// an undefined variable is included in the expression.</returns>
        public object Evaluate(Func<string, double> lookup)
        {
            /// Make a regular expression that will be used to identify variables
            Regex variablePattern = new Regex(("^[a-zA-Z_][A-Za-z_0-9]*$"));
            /// Initialize a variable to hold values and stacks for operators and operands
            double value;
            Stack<String> operators = new Stack<string>();
            Stack<double> values = new Stack<double>();
            foreach (String token in tokens)
            {
                /// If the token is a number, store it in value
                if (double.TryParse(token, out value))
                {
                    /// Multiply or divide if * or / is on top of the operator stack. Push value
                    /// to value stack otherwise
                    if (operators.NextItem("*") || operators.NextItem("/"))
                    {
                        try
                        {
                            MultiplyOrDivide(value, values, operators);
                        }
                        // Return a FormulaError if division by zero occurs
                        catch (ArgumentException)
                        {
                            return new FormulaError("Attempting to divide by 0. Please check formula and reenter.");
                        }
                    }
                    else
                    {
                        values.Push(value);
                    }
                }
                /// Use the regular expression to determine if the token represents a variable
                else if (variablePattern.IsMatch(token))
                {
                    /// Use the delegate method to retrieve the variable's value
                    try
                    {
                        value = lookup(token);
                    }
                    // Return a FormulaError if the variable is not defined by lookup
                    catch (ArgumentException)
                    {
                        return new FormulaError("The variable: " + token + " is not defined by the lookup function. Please check that the variable is entered correctly.");
                    }
                    /// Multiply or divide if * or / is on top of the operator stack. Push value
                    /// to value stack otherwise.
                    if (operators.NextItem("*") || operators.NextItem("/"))
                    {
                        try
                        {
                            MultiplyOrDivide(value, values, operators);
                        }
                        // Return a FormulaError if division by zero occurs
                        catch (ArgumentException)
                        {
                            return new FormulaError("Attempting to divide by 0. Please check formula and reenter.");
                        }
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
                    if (operators.NextItem("+") || operators.NextItem("-"))
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
                    if (operators.NextItem("+") || operators.NextItem("-"))
                    {
                        AddOrSubtract(values, operators);
                    }
                    /// The next operator on the stack will be a (, pop it
                    operators.Pop();
                    /// Perform multiplication or division if the next operator on the stack is a *
                    /// or /.
                    if (operators.NextItem("*") || operators.NextItem("/"))
                    {
                        try
                        {
                            MultiplyOrDivide(values.Pop(), values, operators);
                        }
                        // Return a FormulaError if division by 0 occurs.
                        catch (ArgumentException)
                        {
                            return new FormulaError("Attempting to divide by 0. Please check formula and reenter.");
                        }
                    }
                }
            }
            /// If there are no operators remaining on the stack, return the only value left
            if (operators.Count == 0)
            {
                return values.Pop();
            }
            /// If there is one operator remaining on the stack, it will be a + or - and there should be exactly
            /// two values remaining on the value stack. Return the sum or difference.
            else
            {
                AddOrSubtract(values, operators);
                return values.Pop();
            }
        }

        /// <summary>
        /// Finds and returns a HashSet of variables in the normalized expression. Any
        /// variable in the normalized expression is included only once in the output.
        /// For example, the expression "x + X + Y" with the identity function as the normalizer 
        /// in the constructor will return {x, X, Y}. With a normalizer that converts variables  
        /// to upper-case, the same expression would return {X, Y}.
        /// </summary>
        /// <returns>A HashSet of variables in the normalized expression</returns>
        public IEnumerable<String> GetVariables()
        {
            // The following regular expression matches variables as defined in
            // the class description
            Regex variablePattern = new Regex(("^[a-zA-Z_][A-Za-z_0-9]*$"));
            HashSet<String> variables = new HashSet<String>();
            // Use GetTokens to extract the token from the expression
            foreach (String token in GetTokens(this.expression))
            {
                if (variablePattern.IsMatch(token))
                {
                    variables.Add(token);
                }
            }
            return variables;

        }

        /// <summary>
        /// Returns a string representation of the variable normalized expression associated with 
        /// the Formula. The output string will not contain any whitespace. For example, 
        /// the expression "1+2" and "   1 +  2" will both return the string "1+2". If the variable
        /// normallizer in the constructor is the identity function, the expression "x+y" will 
        /// return "x+y". The same expression will return "X+Y" if the normalizer converts variables
        /// to upper-case.
        /// </summary>
        /// <returns>A string representation of the variable normalized expression.</returns>
        public override string ToString()
        {
            String output = "";
            // The tokens in the list of tokens do not contain whitespace
            // so they can be concatenated to the output string without any
            // processing
            foreach (String token in tokens)
            {
                output += token;
            }
            return output;

        }

        /// <summary>
        /// Checks if this Formula object contains the same information as another object. If
        /// the other object is null or not a formula, returns false. Two Formulas are considered 
        /// equal if they contain the same expression after whitespace has been removed and variables
        /// have been normalized. This is determined by checking if the string representation defined 
        /// by ToString is the same for both Formulas. The output of ToString contains a string representation 
        /// of the expression with normalized variables, no whitespace, and standardized representation 
        /// of floating point numbers.
        /// 
        /// Examples:
        /// new Formula("1.0+2").Equals(new Formula("1  +  2.0") is true
        /// new Formula(" x +  y").Equals(new Formula(" X+Y  ")) is false.
        /// new Formula(" x +  y", N, s => true).Equals(new Formula(" X+Y  ")) where N
        /// converts variables to upper-case is true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Formula))
            {
                return false;
            }
            // Two Formulas are equal if they contain the same variable
            // normalized expression without whitespace. ToString produces
            // this representation.
            return this.ToString().Equals(obj.ToString());
        }

        /// <summary>
        /// Overloads the == operator to return whether two Formulas are equal
        /// as defined by Equals.
        /// </summary>
        /// <returns>True if the Formulas are Equal, false otherwise</returns>
        public static bool operator ==(Formula f1, Formula f2)
        {
            return f1.Equals(f2);
        }

        /// <summary>
        /// Overloads the != operator to return whether two Formulas are not equal
        /// as defined by Equals.
        /// </summary>
        /// <returns>True if the Formulas are not Equal, false otherwise</returns>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return !f1.Equals(f2);
        }

        /// <summary>
        /// Returns a hash code for the Formula object. To ensure that equal
        /// Formulas have the same hash code, the method returns the hash code 
        /// for the output of the Formulas ToString method.
        /// </summary>
        /// <returns>A hash code for the formula</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it. Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns. There are no empty tokens, and no token contains white space.
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

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }

        /// <summary>
        /// Helper method for performing multiplication and division for the Evaluate method.
        /// </summary>
        /// <param name="value">The double that the loop in the Evaluate method currently points to</param>
        /// <param name="values">The stack of values from Evaluate. A value will be popped from
        /// the stack and a resulting value pushed onto it.</param>
        /// <param name="operators">The stack of operators from Evaluate. One operator will be popped.</param>
        /// <exception cref="ArgumentException">Throws an ArgumentException if there are no values
        /// on the value stack</exception>
        /// <exception cref="DivideByZeroException">Throws DivideByZeroException if method attempts
        /// to divide by zero</exception>
        private static void MultiplyOrDivide(double value, Stack<double> values, Stack<String> operators)
        {
            /// Perform multiplication or division with the input value and the top stack value. Push
            /// the result onto the values stack
            if (operators.NextItem("*"))
            {
                values.Push(values.Pop() * value);
            }
            else if (operators.NextItem("/"))
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
        private static void AddOrSubtract(Stack<double> values, Stack<String> operators)
        {
            ///Pop the top two values and perform addition or subtraction based on the top operator stack entry.
            double rightValue = values.Pop();
            double leftValue = values.Pop();
            if (operators.NextItem("+"))
            {
                values.Push(leftValue + rightValue);
            }
            else if (operators.NextItem("-"))
            {
                values.Push(leftValue - rightValue);
            }
            ///Pop the used operator from the stack.
            operators.Pop();
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the expression passed to the Formula constructor. These 
    /// errors include unrecognized tokens, invalid variables, and invalid ordering of tokens.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message to be printed 
        /// when the exception is thrown.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method. This will be returned when 
    /// attempting to divide by zero or if a variable is not defined by the lookup Func passed to
    /// Evaluate.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason for the error.
        /// </summary>
        /// <param name="reason">An explanation of the error that occured</param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  Returns the reason why this FormulaError was created. 
        /// </summary>
        public string Reason { get; private set; }
    }
}


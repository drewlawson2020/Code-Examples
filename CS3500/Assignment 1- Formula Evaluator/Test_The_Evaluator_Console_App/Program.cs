using FormulaEvaluator;
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
///    This file contains all of the necessary methods and functions to properly text Evaluator.cs.
/// </summary>
namespace Tester
{ 
    class TestClass
    {
        /// <summary>
        /// Various truth if statements. If the test(s) return true, nothing happens. If the test(s) return false, 
        /// prints an error out to the console to notify tester to debug.
        /// </summary>
        static void Main(string[] args)
        {
            if (!TestSimpleAddition())
            {
                Console.Error.WriteLine("TestSimpleAddition not correctly evaluated");
            }
            if (!TestSimpleAddition2())
            {
                Console.Error.WriteLine(" TestSimpleAddition2 not correctly evaluated");
            }
            if (!TestSimpleAddition3())
            {
                Console.Error.WriteLine("TestSimpleAddition3 not correctly evaluated");
            }
            if (!TestSimpleSubtraction())
            {
                Console.Error.WriteLine("TestSimpleSubtraction not correctly evaluated");
            }
            if (!TestSimpleSubtraction2())
            {
                Console.Error.WriteLine("TestSimpleSubtraction2 not correctly evaluated");
            }
            if (!TestSimpleSubtraction3())
            {
                Console.Error.WriteLine("TestSimpleSubtraction3 not correctly evaluated");
            }
            if (!TestSimpleMultiplication())
            {
                Console.Error.WriteLine("TestSimpleMultiplication not correctly evaluated");
            }
            if (!TestSimpleMultiplication2())
            {
                Console.Error.WriteLine("TestSimpleMultiplication2 not correctly evaluated");
            }
            if (!TestSimpleMultiplication3())
            {
                Console.Error.WriteLine("TestSimpleMultiplication3 not correctly evaluated");
            }
            if (!TestSimpleDivision())
            {
                Console.Error.WriteLine("TestSimpleDivision not correctly evaluated");
            }
            if (!TestSimpleDivision2())
            {
                Console.Error.WriteLine("TestSimpleDivision2 not correctly evaluated");
            }
            if (!TestSimpleDivision3())
            {
                Console.Error.WriteLine("TestSimpleDivision3 not correctly evaluated");
            }
            if (!TestParentheses())
            {
                Console.Error.WriteLine("TestParentheses not correctly evaluated");
            }
            if (!TestParentheses2())
            {
                Console.Error.WriteLine("TestParentheses2 not correctly evaluated");
            }
            if (!TestParentheses3())
            {
                Console.Error.WriteLine("TestParentheses3 not correctly evaluated");
            }
            if (!TestDivideByZero())
            {
                Console.Error.WriteLine("TestDivideByZero Error not correctly thrown");
            }
            if (!TestBadEquation())
            {
                Console.Error.WriteLine("TestBadEquation Error not correctly thrown");
            }
            if (!TestBadEquation2())
            {
                Console.Error.WriteLine("TestBadEquation2 Error not correctly thrown");
            }
            if (!TestBadEquation3())
            {
                Console.Error.WriteLine("TestBadEquation3 Error not correctly thrown");
            }
            if (!TestBadEquation4())
            {
                Console.Error.WriteLine("TestBadEquation4 Error not correctly thrown");
            }
            if (!TestBadEquation5())
            {
                Console.Error.WriteLine("TestBadEquation5 Error not correctly thrown");
            }
            if (!TestBadEquation6())
            {
                Console.Error.WriteLine("TestBadEquation6 Error not correctly thrown");
            }
            if (!TestBadEquation7())
            {
                Console.Error.WriteLine("TestBadEquation7 Error not correctly thrown");
            }
            int delegateTestResult = Evaluator.Evaluate("A10 + 7", LookMethod1);
            if (delegateTestResult != 27)
            {
                Console.Error.WriteLine("delegateTestResult not correctly evaluated");
            }
            int delegateTestResult2 = Evaluator.Evaluate("A10 + A9", LookMethod1);
            if (delegateTestResult2 != 20)
            {
                Console.Error.WriteLine("delegateTestResult2 not correctly evaluated");
            }

            Console.ReadKey();
        }
        /// Self-explanatory
        static bool TestSimpleAddition()
        {
            return Evaluator.Evaluate("5+5", null) == 10;
        }
        /// Self-explanatory
        static bool TestSimpleAddition2()
        {
            return Evaluator.Evaluate("5+5+10+45", null) == 65;
        }
        /// Self-explanatory
        static bool TestSimpleAddition3()
        {
            return Evaluator.Evaluate("5+5+10", null) == 20;
        }
        /// Self-explanatory
        static bool TestSimpleSubtraction()
        {
            return Evaluator.Evaluate("85-40", null) == 45;
        }
        /// Self-explanatory
        static bool TestSimpleSubtraction2()
        {
            return Evaluator.Evaluate("40-85", null) == -45;
        }
        /// Self-explanatory
        static bool TestSimpleSubtraction3()
        {
            return Evaluator.Evaluate("10-20-30", null) == -40;
        }
        /// Self-explanatory
        static bool TestSimpleMultiplication()
        {
            return Evaluator.Evaluate("1 * 2 * 3", null) == 6;
        }
        /// Self-explanatory
        static bool TestSimpleMultiplication2()
        {
            return Evaluator.Evaluate("100 * 20", null) == 2000;
        }
        /// Self-explanatory
        static bool TestSimpleMultiplication3()
        {
            return Evaluator.Evaluate("7 * 2 * 4", null) == 56;
        }
        /// Self-explanatory
        static bool TestSimpleDivision()
        {
            return Evaluator.Evaluate("6 / 3", null) == 2;
        }
        /// Self-explanatory
        static bool TestSimpleDivision2()
        {
            return Evaluator.Evaluate("1000 / 10", null) == 100;
        }
        /// Self-explanatory
        static bool TestSimpleDivision3()
        {
            return Evaluator.Evaluate("90 / 9", null) == 10;
        }
        /// Self-explanatory
        static bool TestParentheses()
        {
            return Evaluator.Evaluate("10 * (2 + 2)", null) == 40;
        }
        /// Self-explanatory
        static bool TestParentheses2()
        {
            return Evaluator.Evaluate("10 - (2 - 1)", null) == 9;
        }
        /// Self-explanatory
        static bool TestParentheses3()
        {
            return Evaluator.Evaluate("(3+3)*(5/5)", null) == 6;
        }
        /// Tests divsion by zero exception.
        static bool TestDivideByZero()
        {
            try
            {
                Evaluator.Evaluate("9001/0", null);
                return false;
            }
            catch (DivideByZeroException)
            {
                return true;
            }



        }
        /// Tests BadArgument with nonsense equation

        static bool TestBadEquation()
        {
            try
            {
                Evaluator.Evaluate("9//////////", null);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
        /// Tests BadArgument with nonsense equation

        static bool TestBadEquation2()
        {
            try
            {
                Evaluator.Evaluate("-A-", null);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
        /// Tests BadArgument with nonsense equation

        static bool TestBadEquation3()
        {
            try
            {
                Evaluator.Evaluate(")2(-3", null);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }

        }
        /// Tests BadArgument with nonsense equation

        static bool TestBadEquation4()
        {
            try
            {
                Evaluator.Evaluate("2+-3", null);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
        /// Tests BadArgument with nonsense equation

        static bool TestBadEquation5()
        {
            try
            {
                Evaluator.Evaluate("2 4532 232", null);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
        /// Tests BadArgument with nonsense equation

        static bool TestBadEquation6()
        {
            try
            {
                Evaluator.Evaluate("=w==w=w==w=w=w=w=w=w=", null);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
        /// Tests BadArgument with nonsense equation

        static bool TestBadEquation7()
        {
            try
            {
                Evaluator.Evaluate("uwu", null);
                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
        /// Test delegate method, where A10 is 20, and is 0 otherwise.

        public static int LookMethod1(string variable_name)
        {
            if (variable_name.Equals("A10"))
                return 20;
            else
                return 0;
        }
    }
}

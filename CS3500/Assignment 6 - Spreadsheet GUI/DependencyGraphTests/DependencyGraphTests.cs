/// <summary>
/// Author: Peter Bruns
/// Partner: None
/// Date: January 27, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright CS 3500 and Peter Bruns - This work may not be copied for use in Academic Coursework
/// 
/// I, Peter Bruns, certify that, apart from the provided tests, I wrote this code from scratch and did 
/// not copy it in part or whole from another source. All references used in the completion of the assignment
/// are cited in my README file.
/// 
/// File Contents
/// 
/// Contains tests for the methods in the DependencyGraph class. The first test class contains the tests provided
/// on the assignment page. The second test class contains tests that I added.
/// </summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SpreadsheetUtilities;

namespace DevelopmentTests
{
    [TestClass]

    /// <summary>
    /// Contains the tests provided on the assignment page.
    /// <summary>
    public class DependencyGraphTestProvided
    {

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.AreEqual(0, t.Size);
        }


        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyRemoveTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);
        }


        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyEnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            Assert.IsTrue(e2.MoveNext());
            Assert.AreEqual("y", e2.Current);
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.GetDependees("y").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }


        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void SimpleReplaceTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }



        ///<summary>
        ///It should be possibe to have more than one DG at a time.
        ///</summary>
        [TestMethod()]
        public void StaticTest()
        {
            DependencyGraph t1 = new DependencyGraph();
            DependencyGraph t2 = new DependencyGraph();
            t1.AddDependency("x", "y");
            Assert.AreEqual(1, t1.Size);
            Assert.AreEqual(0, t2.Size);
        }




        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.AreEqual(4, t.Size);
        }


        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void EnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }




        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void ReplaceThenEnumerate()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }



        /// <summary>
        ///Using lots of data
        ///</summary>
        [TestMethod()]
        public void StressTest()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();

            // A bunch of strings to use
            const int SIZE = 200;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }

            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }

            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 4; j < SIZE; j += 4)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Add some back
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j += 2)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove some more
            for (int i = 0; i < SIZE; i += 2)
            {
                for (int j = i + 3; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
            }
        }

    }


    [TestClass]

    /// <summary>
    /// Contains additional test methods for methods in the DependencyGraph class.
    /// <summary>
    public class DependencyGraphTestAdded
    {
        /// <summary>
        ///Test size is correct after several calls to AddDependency()
        ///</summary>
        [TestMethod()]
        public void TestSizeAfterAddSeveralTimes()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            Assert.AreEqual(1, testGraph.Size);
            testGraph.AddDependency("x", "z");
            Assert.AreEqual(2, testGraph.Size);
            testGraph.AddDependency("a", "b");
            Assert.AreEqual(3, testGraph.Size);
        }

        /// <summary>
        ///Test size does not change after adding a repeated dependency
        ///</summary>
        [TestMethod()]
        public void TestSizeAddRepeatPair()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            Assert.AreEqual(1, testGraph.Size);
            testGraph.AddDependency("x", "z");
            Assert.AreEqual(2, testGraph.Size);
            testGraph.AddDependency("a", "b");
            Assert.AreEqual(3, testGraph.Size);

            // The following are all repeats
            testGraph.AddDependency("a", "b");
            Assert.AreEqual(3, testGraph.Size);
            testGraph.AddDependency("x", "y");
            Assert.AreEqual(3, testGraph.Size);
            testGraph.AddDependency("x", "z");
            Assert.AreEqual(3, testGraph.Size);
        }

        /// <summary>
        /// Test indexer returns 0 for nodes with no dependees
        /// </summary>
        [TestMethod()]
        public void TestIndexerNoDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            Assert.AreEqual(0, testGraph["x"]);
            Assert.AreEqual(0, testGraph["a"]);
        }

        /// <summary>
        /// Test indexer returns 0 for nodes that are not in the dependency graph
        /// </summary>
        [TestMethod()]
        public void TestIndexernodesNotInGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            Assert.AreEqual(0, testGraph["k"]);
            Assert.AreEqual(0, testGraph["m"]);
        }

        /// <summary>
        /// Test indexer returns 0 for empty dependency graph
        /// </summary>
        [TestMethod()]
        public void TestIndexerEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            Assert.AreEqual(0, testGraph["k"]);
            Assert.AreEqual(0, testGraph["m"]);
        }

        /// <summary>
        /// Test indexer returns the correct number of dependees for nodes with dependees
        /// </summary>
        [TestMethod()]
        public void TestIndexerWithDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("z", "y");
            testGraph.AddDependency("a", "b");
            Assert.AreEqual(2, testGraph["y"]);
            Assert.AreEqual(1, testGraph["b"]);
        }

        /// <summary>
        /// Test HasDependents runs and is false on an empty graph
        /// </summary>
        [TestMethod()]
        public void TestHasDependentsEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            Assert.IsFalse(testGraph.HasDependents("x"));
            Assert.IsFalse(testGraph.HasDependents("y"));
        }

        /// <summary>
        /// Test HasDependents returns true for nodes with dependents
        /// </summary>
        [TestMethod()]
        public void TestHasDependentsForNodesWithDependents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            Assert.IsTrue(testGraph.HasDependents("x"));
            Assert.IsTrue(testGraph.HasDependents("a"));
            Assert.IsTrue(testGraph.HasDependents("b"));
        }

        /// <summary>
        /// Test HasDependents returns false for nodes without dependents
        /// </summary>
        [TestMethod()]
        public void TestHasDependentsForNodesWithoutDependents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            Assert.IsFalse(testGraph.HasDependents("y"));
            Assert.IsFalse(testGraph.HasDependents("z"));
            Assert.IsFalse(testGraph.HasDependents("y"));
            Assert.IsFalse(testGraph.HasDependents("b"));
        }

        /// <summary>
        /// Test HasDependents returns false on non-empty graph for nodes not in the graph
        /// </summary>
        [TestMethod()]
        public void TestHasDependentsFornodesNotInGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            Assert.IsFalse(testGraph.HasDependents("l"));
            Assert.IsFalse(testGraph.HasDependents("m"));
            Assert.IsFalse(testGraph.HasDependents("n"));
        }

        /// <summary>
        /// Test HasDependees runs and returns false on an empty graph
        /// </summary>
        [TestMethod()]
        public void TestHasDependeesEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            Assert.IsFalse(testGraph.HasDependees("x"));
            Assert.IsFalse(testGraph.HasDependees("y"));
        }

        /// <summary>
        /// Test HasDependees runs and returns true for nodes with dependees
        /// </summary>
        [TestMethod()]
        public void TestHasDependeesForNodesWithDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("z", "y");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            Assert.IsTrue(testGraph.HasDependees("y"));
            Assert.IsTrue(testGraph.HasDependees("b"));
            Assert.IsTrue(testGraph.HasDependees("c"));
        }

        /// <summary>
        /// Test HasDependees runs and is false on non-empty graph for nodes without dependees
        /// </summary>
        [TestMethod()]
        public void TestHasDependeesForNodesWithoutDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            Assert.IsFalse(testGraph.HasDependees("x"));
            Assert.IsFalse(testGraph.HasDependees("a"));
        }

        /// <summary>
        /// Test HasDependees runs and is false on non-empty graph for nodes not in the graph
        /// </summary>
        [TestMethod()]
        public void TestHasDependeesFornodesNotInGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            Assert.IsFalse(testGraph.HasDependees("l"));
            Assert.IsFalse(testGraph.HasDependees("m"));
            Assert.IsFalse(testGraph.HasDependees("n"));
        }

        /// <summary>
        /// Test GetDependents runs on an empty graph and returns empty set
        /// </summary>
        [TestMethod()]
        public void TestGetDependentsEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            HashSet<string> empty = new HashSet<string>();
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("s")));
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("a")));
        }

        /// <summary>
        /// Test GetDependents returns the correct set for a node with dependents
        /// </summary>
        [TestMethod()]
        public void TestGetDependentsWithDependents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("c", "x");
            Assert.IsTrue(new HashSet<string> { "z", "y" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "b" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("c")));
        }

        /// <summary>
        /// Test GetDependents returns empty set on a node without dependents
        /// </summary>
        [TestMethod()]
        public void TestGetDependentsWithoutDependents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("w", "y");
            testGraph.AddDependency("a", "b");
            HashSet<string> empty = new HashSet<string>();
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("y")));
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("b")));
        }

        /// <summary>
        /// Test GetDependents returns empty set for a node not in non-empty graph
        /// </summary>
        [TestMethod()]
        public void TestGetDependentsnodeNotInGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("a", "b");
            HashSet<string> empty = new HashSet<string>();
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("m")));
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("n")));
        }

        /// <summary>
        /// Test GetDependees runs and returns empty set on an empty graph
        /// </summary>
        [TestMethod()]
        public void TestGetDependeesEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            HashSet<string> empty = new HashSet<string>();
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependees("s")));
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependees("a")));
        }

        /// <summary>
        /// Test GetDependees returns expected set on a node with dependees
        /// </summary>
        [TestMethod()]
        public void TestGetDependeesWithDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "z");
            testGraph.AddDependency("a", "y");
            testGraph.AddDependency("c", "x");
            Assert.IsTrue(new HashSet<string> { "x", "a" }.SetEquals(testGraph.GetDependees("y")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependees("z")));
            Assert.IsTrue(new HashSet<string> { "c" }.SetEquals(testGraph.GetDependees("x")));
        }

        /// <summary>
        /// Test GetDependees runs and returns empty set on a node without dependees
        /// </summary>
        [TestMethod()]
        public void TestGetDependeesWithoutDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("w", "y");
            testGraph.AddDependency("a", "b");
            HashSet<string> empty = new HashSet<string>();
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("y")));
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("b")));
        }

        /// <summary>
        /// Test GetDependees runs and returns empty set on a node not in graph
        /// </summary>
        [TestMethod()]
        public void TestGetDependeesnodeNotInGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("a", "b");
            HashSet<string> empty = new HashSet<string>();
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependees("m")));
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependees("n")));
        }

        /// <summary>
        /// Test that adding dependencies already in the graph does not alter size or relationship set contents
        /// </summary>
        [TestMethod()]
        public void TestAddDependencyAlreadyInGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("a", "y");
            Assert.AreEqual(4, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "a" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "b", "y" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "a", "x" }.SetEquals(testGraph.GetDependees("y")));

            // The following are all repeated from above
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("a", "y");
            Assert.AreEqual(4, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "a" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "b", "y" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "a", "x" }.SetEquals(testGraph.GetDependees("y")));
        }

        /// <summary>
        /// Test RemoveDependency runs on an empty graph and does not alter it
        /// </summary>
        [TestMethod()]
        public void TestRemoveDependencyEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.RemoveDependency("x", "y");
            testGraph.RemoveDependency("x", "a");
            testGraph.RemoveDependency("a", "b");
            HashSet<string> empty = new HashSet<string>();
            Assert.AreEqual(0, testGraph.Size);
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(empty.SetEquals(testGraph.GetDependees("y")));
        }

        /// <summary>
        /// Test that removing dependencies not in the graph does not alter size or relationship set contents
        /// </summary>
        [TestMethod()]
        public void TestRemoveDependencyNotInGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("a", "y");
            Assert.AreEqual(4, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "a" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "b", "y" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "a", "x" }.SetEquals(testGraph.GetDependees("y")));

            // None of the following dependencies are in the graph
            testGraph.RemoveDependency("x", "b");
            testGraph.RemoveDependency("y", "a");
            testGraph.RemoveDependency("a", "x");
            testGraph.RemoveDependency("b", "y");

            // These are all the same as above
            Assert.AreEqual(4, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "a" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "b", "y" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "a", "x" }.SetEquals(testGraph.GetDependees("y")));
        }

        /// <summary>
        /// Test that removing dependencies changes size and set contents
        /// </summary>
        [TestMethod()]
        public void TestRemoveDependency()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "y");
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("a", "y");
            Assert.AreEqual(4, testGraph.Size);

            testGraph.RemoveDependency("x", "y");
            Assert.AreEqual(3, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("y")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependents("x")));

            testGraph.RemoveDependency("x", "a");
            Assert.AreEqual(2, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("a")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("x")));

            testGraph.RemoveDependency("a", "b");
            Assert.AreEqual(1, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> {"y"}.SetEquals(testGraph.GetDependents("a")));

            testGraph.RemoveDependency("a", "y");
            Assert.AreEqual(0, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("y")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("a")));
        }

        /// <summary>
        /// Test ReplaceDependents on an empty graph with an empty set of new dependents does not alter graph
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependentsEmptyGraphEmptySet()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependents("x", new HashSet<string> { });
            testGraph.ReplaceDependents("a", new HashSet<string> { });
            Assert.AreEqual(0, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("a")));
        }

        /// <summary>
        /// Test ReplaceDependents on an empty graph with a set of new dependents runs, adds all dependents, 
        /// and updates dependees
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependentsEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependents("x", new HashSet<string> { "a", "y", "c" });
            testGraph.ReplaceDependents("a", new HashSet<string> { "b", "c" });
            Assert.AreEqual(5, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "a", "y", "c" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "b", "c" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "x", "a" }.SetEquals(testGraph.GetDependees("c")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependees("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependees("y")));
        }

        /// <summary>
        /// Test ReplaceDependents on an empty graph with a set of new dependents with repeats does not add the
        /// repeats multiple times
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependentsEmptyGraphRepeatsInNewDependents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            // Add (x, y), (x, a), and (a, b)
            testGraph.ReplaceDependents("x", new HashSet<string> { "a", "y", "a" });
            testGraph.ReplaceDependents("a", new HashSet<string> { "b", "b" });
            Assert.AreEqual(3, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "a", "y" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "b" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependees("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependees("y")));
        }

        /// <summary>
        /// Test ReplaceDependents updates size of graph and relationship sets correctly
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependentsUpdatesSizeAndRelationships()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("x", "b");
            testGraph.AddDependency("x", "c");
            testGraph.AddDependency("x", "d");
            testGraph.AddDependency("a", "m");
            testGraph.AddDependency("a", "n");
            testGraph.AddDependency("k", "x");
            Assert.AreEqual(7, testGraph.Size);
            testGraph.ReplaceDependents("x", new HashSet<string> { "y", "z" });
            testGraph.ReplaceDependents("a", new List<string> { "y", "p", "b" });
            Assert.AreEqual(6, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "z" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "y", "p", "b" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("k")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "a", "x" }.SetEquals(testGraph.GetDependees("y")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("c")));
        }

        /// <summary>
        /// Test ReplaceDependents runs on non-empty graph with empty input results in correct size and
        /// relationship sets
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependentsWithEmptyInputs()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("x", "b");
            testGraph.AddDependency("x", "c");
            testGraph.AddDependency("x", "d");
            testGraph.AddDependency("a", "m");
            testGraph.AddDependency("a", "n");
            testGraph.AddDependency("k", "x");
            Assert.AreEqual(7, testGraph.Size);
            testGraph.ReplaceDependents("x", new HashSet<string> { });
            testGraph.ReplaceDependents("a", new List<string> { });
            Assert.AreEqual(1, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("k")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("a")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("y")));
        }


        /// <summary>
        /// Test ReplaceDependents updates size and relationship sets correctly with repeats in new dependents input
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependentsUpdatesSizeAndRelationshipsRepeatedInput()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("x", "b");
            testGraph.AddDependency("x", "c");
            testGraph.AddDependency("x", "d");
            testGraph.AddDependency("a", "m");
            testGraph.AddDependency("a", "n");
            testGraph.AddDependency("k", "x");
            Assert.AreEqual(7, testGraph.Size);

            testGraph.ReplaceDependents("x", new HashSet<string> { "y", "z", "y" });
            testGraph.ReplaceDependents("a", new List<string> { "p", "b", "b", "p" });
            Assert.AreEqual(5, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "z" }.SetEquals(testGraph.GetDependents("x")));
            Assert.IsTrue(new HashSet<string> { "p", "b" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("k")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("p")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("c")));
        }

        /// <summary>
        /// Test ReplaceDependees on an empty graph with an empty set of new dependents runs and does not alter graph
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependeesEmptyGraphEmptySet()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependees("x", new HashSet<string> { });
            testGraph.ReplaceDependees("a", new HashSet<string> { });
            Assert.AreEqual(0, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("x")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("a")));
        }

        /// <summary>
        /// Test ReplaceDependees runs on an empty graph with a set of new dependees, updates size, and updates
        /// the relationship sets
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependeesEmptyGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependees("x", new HashSet<string> { "a", "y", "c" });
            testGraph.ReplaceDependees("a", new HashSet<string> { "b", "c" });
            Assert.AreEqual(5, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "a", "y", "c" }.SetEquals(testGraph.GetDependees("x")));
            Assert.IsTrue(new HashSet<string> { "b", "c" }.SetEquals(testGraph.GetDependees("a")));
            Assert.IsTrue(new HashSet<string> { "x", "a" }.SetEquals(testGraph.GetDependents("c")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("y")));
        }

        /// <summary>
        /// Test that ReplaceDependees on an empty graph with a set of new dependees with repeats in new dependee
        /// input does not add repeats of dependency
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependeesEmptyGraphRepeatsInNewDependents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            // Ad (a, x), (y, x), (b, a)
            testGraph.ReplaceDependees("x", new HashSet<string> { "a", "y", "a" });
            testGraph.ReplaceDependees("a", new HashSet<string> { "b", "b" });
            Assert.AreEqual(3, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "a", "y" }.SetEquals(testGraph.GetDependees("x")));
            Assert.IsTrue(new HashSet<string> { "b" }.SetEquals(testGraph.GetDependees("a")));
            Assert.IsTrue(new HashSet<string> { "a", "y" }.SetEquals(testGraph.GetDependees("x")));
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependents("b")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("a")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("y")));
        }

        /// <summary>
        /// Test ReplaceDependees updates graph size and relationship sets correctly
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependeesUpdatesSizeAndRelationships()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("x", "b");
            testGraph.AddDependency("x", "c");
            testGraph.AddDependency("x", "d");
            testGraph.AddDependency("a", "m");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("k", "x");
            Assert.AreEqual(7, testGraph.Size);

            testGraph.ReplaceDependees("x", new HashSet<string> { "y", "z" });
            testGraph.ReplaceDependees("c", new List<string> { "y", "p", "q" });
            Assert.AreEqual(9, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "z" }.SetEquals(testGraph.GetDependees("x")));
            Assert.IsTrue(new HashSet<string> { "y", "p", "q" }.SetEquals(testGraph.GetDependees("c")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("k")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "c", "x" }.SetEquals(testGraph.GetDependents("y")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("c")));
        }

        /// <summary>
        /// Test ReplaceDependees works on non-empty graph with empty new dependees input and updates
        /// graph size and relationship sets correctly
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependeesWithEmptyInputs()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("w", "a");
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("y", "a");
            testGraph.AddDependency("x", "d");
            testGraph.AddDependency("a", "d");
            testGraph.AddDependency("a", "n");
            testGraph.AddDependency("k", "x");
            Assert.AreEqual(7, testGraph.Size);

            testGraph.ReplaceDependees("a", new HashSet<string> { });
            testGraph.ReplaceDependees("d", new List<string> { });
            Assert.AreEqual(2, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "a" }.SetEquals(testGraph.GetDependees("n")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("d")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependents("k")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependees("a")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("y")));
        }


        /// <summary>
        /// Test ReplaceDependees updates size and relationship sets correctly with repeats in new dependee input
        /// </summary>
        [TestMethod()]
        public void TestReplaceDependeesUpdatesSizeAndRelationshipsRepeatedInput()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("x", "a");
            testGraph.AddDependency("x", "b");
            testGraph.AddDependency("x", "c");
            testGraph.AddDependency("x", "d");
            testGraph.AddDependency("a", "m");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("k", "x");
            Assert.AreEqual(7, testGraph.Size);

            testGraph.ReplaceDependees("x", new HashSet<string> { "y", "y", "z" });
            testGraph.ReplaceDependees("c", new List<string> { "y", "p", "q", "y", "p", "q" });
            Assert.AreEqual(9, testGraph.Size);
            Assert.IsTrue(new HashSet<string> { "y", "z" }.SetEquals(testGraph.GetDependees("x")));
            Assert.IsTrue(new HashSet<string> { "y", "p", "q" }.SetEquals(testGraph.GetDependees("c")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("k")));
            Assert.IsTrue(new HashSet<string> { "x" }.SetEquals(testGraph.GetDependees("b")));
            Assert.IsTrue(new HashSet<string> { "c", "x" }.SetEquals(testGraph.GetDependents("y")));
            Assert.IsTrue(new HashSet<string> { }.SetEquals(testGraph.GetDependents("c")));
        }
    }
}
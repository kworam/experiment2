using System;
using System.Collections.Generic;
using Experiment;
using Experiment.CartesianTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.CartesianTree
{
    [TestClass]
    public class CartesianTreeUnitTest
    {
        [TestCategory("CartesianTree"), TestMethod]
        public void EmptyTreeCount()
        {
            Tree t = new Tree();
            Assert.AreEqual(t.Count(), 0);
        }

        [TestCategory("CartesianTree"), TestMethod]
        public void EmptyTreeTraverse()
        {
            Tree t = new Tree();
            List<int> l = t.Traverse();
            Assert.AreEqual(l.Count, 0);
        }

        [TestCategory("CartesianTree"), TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyTreeRange()
        {
            Tree t = new Tree();
            int min = t.GetMinimumForRange(0, 1);
        }

        [TestCategory("CartesianTree"), TestMethod]
        public void TreeCountOne()
        {
            List<int> values = new List<int>() { 7 };
            Tree t = BuildTreeFromList(values);

            Assert.AreEqual(t.GetMinimumForRange(0, 0), 7);
        }

        [TestCategory("CartesianTree"), TestMethod]
        public void TreeCountTen()
        {
            List<int> values = new List<int>() { 7, 2, 1, 33, 0, 3, 16, 2, 6, 2 };
            Tree t = BuildTreeFromList(values);

            Assert.AreEqual(t.GetMinimumForRange(0, 3), 1);
            Assert.AreEqual(t.GetMinimumForRange(5, 9), 2);
            Assert.AreEqual(t.GetMinimumForRange(0, 9), 0);
        }

        private static Tree BuildTreeFromList(List<int> values)
        {
            Tree t = new Tree();
            for (int i = 0; i < values.Count; i++)
            {
                t.Insert(values[i]);
            }

            Assert.AreEqual(t.Count(), values.Count);
            List<int> traversal = t.Traverse();
            Assert.AreEqual(traversal.Count, t.Count());
            ArrayUtility.AreIntegerEnumerablesEqual(traversal, values);

            return t;
        }
    }
}

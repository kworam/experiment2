using System.Collections.Generic;
using Experiment.AlgorithmDesignManual;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.AlgorithmDesignManual
{
    [TestClass]
    public class LongestIncreasingSequenceUnitTest
    {
        [TestCategory("LongestIncreasingSequence"),TestMethod]
        public void NullList()
        {
            int[] a = null;
            List<int> result = LongestIncreasingSequence.Find(a);
            Assert.AreEqual(result.Count, 0);
        }

        [TestCategory("LongestIncreasingSequence"), TestMethod]
        public void EmptyList()
        {
            int[] a = new int[] { };
            List<int> result = LongestIncreasingSequence.Find(a);
            Assert.AreEqual(result.Count, 0);
        }

        [TestCategory("LongestIncreasingSequence"), TestMethod]
        public void SingletonList()
        {
            int[] a = new int[] { 1 };
            List<int> result = LongestIncreasingSequence.Find(a);
            List<int> expected = new List<int>(a);
            ArrayUtility.AreIntegerEnumerablesEqual(result, expected);
        }

        //
        [TestCategory("LongestIncreasingSequence"), TestMethod]
        public void LongerList()
        {
            int[] a = new int[] { 98, 99, 1, 2, 3, 4, 0, 100 };
            List<int> result = LongestIncreasingSequence.Find(a);
            List<int> expected = new List<int>() { 1, 2, 3, 4, 100 };
            ArrayUtility.AreIntegerEnumerablesEqual(result, expected);
        }
    }
}

using System.Collections.Generic;
using Experiment.Permutation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Permutation
{
    [TestClass]
    public class HeapAlgorithmUnitTest
    {
        [TestCategory("PermutationHeapAlgorithm"), TestMethod]
        public void NullString()
        {
            Assert.IsNull(HeapAlgorithm.GetPerms(null));
        }

        [TestCategory("PermutationHeapAlgorithm"), TestMethod]
        public void EmptyString()
        {
            List<string> perms = HeapAlgorithm.GetPerms(string.Empty);
            Assert.AreEqual(1, perms.Count);
            Assert.AreEqual(string.Empty, perms[0]);
        }

        [TestCategory("PermutationHeapAlgorithm"), TestMethod]
        public void SingletonString()
        {
            string singleton = "a";
            List<string> perms = HeapAlgorithm.GetPerms(singleton);
            Assert.AreEqual(1, perms.Count);
            Assert.AreEqual(singleton, perms[0]);
        }

        [TestCategory("PermutationHeapAlgorithm"), TestMethod]
        public void StringLengthTwo()
        {
            string s = "ab";
            List<string> perms = HeapAlgorithm.GetPerms(s);
            Assert.AreEqual(2, perms.Count);
            //Assert.AreEqual(singleton, perms[0]);
        }

        [TestCategory("PermutationHeapAlgorithm"), TestMethod]
        public void StringLengthThree()
        {
            string s = "abc";
            List<string> perms = HeapAlgorithm.GetPerms(s);
            Assert.AreEqual(6, perms.Count);
            //Assert.AreEqual(singleton, perms[0]);
        }

    }
}

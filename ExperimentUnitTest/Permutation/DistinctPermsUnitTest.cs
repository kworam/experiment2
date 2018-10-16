using System.Collections.Generic;
using Experiment.Permutation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Permutation
{
    [TestClass]
    public class DistinctPermsUnitTest
    {
        [TestCategory("DistinctPermsUnitTest"), TestMethod]
        public void NullString()
        {
            Assert.IsNull(DistinctPerms.GetPerms(null));
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void EmptyString()
        {
            List<string> perms = DistinctPerms.GetPerms(string.Empty);
            Assert.AreEqual(1, perms.Count);
            Assert.AreEqual(string.Empty, perms[0]);
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void SingletonString()
        {
            string singleton = "a";
            List<string> perms = DistinctPerms.GetPerms(singleton);
            Assert.AreEqual(1, perms.Count);
            Assert.AreEqual(singleton, perms[0]);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void StringLengthTwo()
        {
            string s = "ab";
            List<string> perms = DistinctPerms.GetPerms(s);
            Assert.AreEqual(2, perms.Count);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void StringLengthThree()
        {
            string s = "abc";
            List<string> perms = DistinctPerms.GetPerms(s);
            Assert.AreEqual(6, perms.Count);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void StringLengthThreeDups()
        {
            string s = "aab";
            List<string> perms = DistinctPerms.GetPerms(s);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void StringLengthFourDups()
        {
            string s = "aabb";
            List<string> perms = DistinctPerms.GetPerms(s);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void StringLengthFiveDups()
        {
            string s = "aabbc";
            List<string> perms = DistinctPerms.GetPerms(s);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
        }

        // NOTE: the number of distinct permutations of string 's' of length 'n'
        // with duplicate letters a1 ... am  with counts  k1 ... km
        // is n! / k1! * k2! * ...  * km!
        [TestCategory("DistinctPerms"), TestMethod]
        public void StringMoon()
        {
            string s = "moon";
            List<string> perms = DistinctPerms.GetPerms(s);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
            Assert.AreEqual(set.Count, 12);
        }

        [TestCategory("DistinctPerms"), TestMethod]
        public void StringRadar()
        {
            string s = "radar";
            List<string> perms = DistinctPerms.GetPerms(s);
            HashSet<string> set = new HashSet<string>(perms);
            Assert.AreEqual(set.Count, perms.Count);
            Assert.AreEqual(set.Count, 30);
        }
    }
}

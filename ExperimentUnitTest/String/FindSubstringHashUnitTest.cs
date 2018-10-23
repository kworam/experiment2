using Experiment.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.String
{
    [TestClass]
    public class FindSubstringHashUnitTest
    {
        [TestCategory("FindSubstringHash"), TestMethod]
        public void AlphaCountFiveFoundAtStart()
        {
            string s = "abcde";
            string ss = "ab";
            Assert.AreEqual(FindSubstringHash.FindSubStringHash(s, ss, 5), 0);
        }

        [TestCategory("FindSubstringHash"), TestMethod]
        public void AlphaCountFiveFoundAtEnd()
        {
            string s = "abcde";
            string ss = "de";
            Assert.AreEqual(FindSubstringHash.FindSubStringHash(s, ss, 5), 3);
        }

        [TestCategory("FindSubstringHash"), TestMethod]
        public void AlphaCountFiveFoundInterior()
        {
            string s = "abcde";
            string ss = "cd";
            Assert.AreEqual(FindSubstringHash.FindSubStringHash(s, ss, 5), 2);
        }

        [TestCategory("FindSubstringHash"), TestMethod]
        public void AlphaCountFiveNotFound()
        {
            string s = "abcde";
            string ss = "cb";
            Assert.AreEqual(FindSubstringHash.FindSubStringHash(s, ss, 5), -1);
        }
    }
}

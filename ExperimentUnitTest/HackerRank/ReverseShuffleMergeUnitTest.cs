using System;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class ReverseShuffleMergeUnitTest
    {
        [TestCategory("ReverseShuffleMerge"), TestMethod]
        public void SampleInput0()
        {
            string s = "eggegg";
            Console.WriteLine(ReverseShuffleMerge.reverseShuffleMerge(s));
        }

        [TestCategory("ReverseShuffleMerge"), TestMethod]
        public void SampleInput1()
        {
            string s = "abcdefgabcdefg";
            Console.WriteLine(ReverseShuffleMerge.reverseShuffleMerge(s));
        }
    }
}

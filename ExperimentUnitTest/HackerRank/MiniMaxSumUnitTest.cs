using System;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class MiniMaxSumUnitTest
    {
        [TestCategory("MiniMaxSum"), TestMethod]
        public void TestCase7()
        {
            string a = "123";
            string b = "456";
            int digita = a[a.Length - 1] - '0';
            int digitb = b[b.Length - 1] - '0';
            
            int[] arr = new int[] { 942381765, 627450398, 954173620, 583762094, 236817490 };
            
            MiniMaxSum.miniMaxSum(arr);
        }
    }
}

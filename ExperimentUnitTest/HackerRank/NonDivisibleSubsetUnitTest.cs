using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class NonDivisibleSubsetUnitTest
    {
        [TestCategory("NonDivisibleSubset"), TestMethod]
        public void SampleTestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\NonDivisibleSubset_SampleTestCase0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("NonDivisibleSubset"), TestMethod]
        public void SampleTestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\NonDivisibleSubset_SampleTestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("NonDivisibleSubset"), TestMethod]
        public void TestCase6()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\NonDivisibleSubset_TestCase6.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("NonDivisibleSubset"), TestMethod]
        public void TestCase7()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\NonDivisibleSubset_TestCase7.txt"))
            {
                DoTest(sr);
            }
        }

        private void DoTest(StreamReader sr)
        {
            string[] nk = sr.ReadLine().Split(' ');

            int n = Convert.ToInt32(nk[0]);

            int k = Convert.ToInt32(nk[1]);

            int[] S = Array.ConvertAll(sr.ReadLine().Split(' '), STemp => Convert.ToInt32(STemp))
            ;
            int result = NonDivisibleSubset.nonDivisibleSubset(k, S);

            Console.WriteLine(result);
        }
    }
}

using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class MaximumXorUnitTest
    {
        [TestCategory("MaximumXor"), TestMethod]
        public void TestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MaximumXor_Testcase0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("MaximumXor"), TestMethod]
        public void TestCase2()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MaximumXor_Testcase2.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int n = Convert.ToInt32(sr.ReadLine());

            int[] arr = Array.ConvertAll(sr.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
            ;
            int m = Convert.ToInt32(sr.ReadLine());

            int[] queries = new int[m];

            for (int i = 0; i < m; i++)
            {
                int queriesItem = Convert.ToInt32(sr.ReadLine());
                queries[i] = queriesItem;
            }

            int[] result = MaximumXor.maxXor(arr, queries);

            Console.WriteLine(string.Join("\n", result));
        }
    }
}

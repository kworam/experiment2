using System;
using System.IO;
using Experiment.HackerRank;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class QueensAttack2UnitTest
    {
        
        [TestCategory("QueensAttack2"), TestMethod]
        public void SampleTestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\QueensAttack2_SampleTestCase0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("QueensAttack2"), TestMethod]
        public void SampleTestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\QueensAttack2_SampleTestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("QueensAttack2"), TestMethod]
        public void TestCase6()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\QueensAttack2_TestCase6.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            string[] nk = sr.ReadLine().Split(' ');

            int n = Convert.ToInt32(nk[0]);

            int k = Convert.ToInt32(nk[1]);

            string[] r_qC_q = sr.ReadLine().Split(' ');

            int r_q = Convert.ToInt32(r_qC_q[0]);

            int c_q = Convert.ToInt32(r_qC_q[1]);

            int[][] obstacles = new int[k][];

            for (int i = 0; i < k; i++)
            {
                obstacles[i] = Array.ConvertAll(sr.ReadLine().Split(' '), obstaclesTemp => Convert.ToInt32(obstaclesTemp));
            }

            int result = QueensAttack2.queensAttack(n, k, r_q, c_q, obstacles);

            Console.WriteLine(result);
        }
    }
}

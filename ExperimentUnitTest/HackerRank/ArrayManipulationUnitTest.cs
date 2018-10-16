using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class ArrayManipulationUnitTest
    {
        [TestCategory("ArrayManipulation"), TestMethod]
        public void SampleTestCase()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\ArrayManipulation_SampleTestCase.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("ArrayManipulation"), TestMethod]
        public void TestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\ArrayManipulation_Testcase1.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("ArrayManipulation"), TestMethod]
        public void TestCase1b()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\ArrayManipulation_Testcase1b.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            string[] nm = sr.ReadLine().Split(' ');

            int n = Convert.ToInt32(nm[0]);

            int m = Convert.ToInt32(nm[1]);

            int[][] queries = new int[m][];

            for (int i = 0; i < m; i++)
            {
                queries[i] = Array.ConvertAll(sr.ReadLine().Split(' '), queriesTemp => Convert.ToInt32(queriesTemp));
            }

            long result = ArrayManipulation.arrayManipulation(n, queries);

            Console.WriteLine(result);
        }
    }
}

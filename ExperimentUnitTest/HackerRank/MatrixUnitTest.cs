using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class MatrixUnitTest
    {
        [TestCategory("Matrix"), TestMethod]
        public void Sample0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\Matrix_TestCase_Sample0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("Matrix"), TestMethod]
        public void TestCase2()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\Matrix_TestCase2.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            string[] nk = sr.ReadLine().Split(' ');

            int n = Convert.ToInt32(nk[0]);

            int k = Convert.ToInt32(nk[1]);

            int[][] roads = new int[n - 1][];

            for (int i = 0; i < n - 1; i++)
            {
                roads[i] = Array.ConvertAll(sr.ReadLine().Split(' '), roadsTemp => Convert.ToInt32(roadsTemp));
            }

            int[] machines = new int[k];

            for (int i = 0; i < k; i++)
            {
                int machinesItem = Convert.ToInt32(sr.ReadLine());
                machines[i] = machinesItem;
            }

            //int result = Matrix.minTime(roads, machines);
            int result = Matrix2.minTime(roads, machines);
            //int result = Matrix3.minTime(roads, machines);

            Console.WriteLine(result);
        }
    }
}

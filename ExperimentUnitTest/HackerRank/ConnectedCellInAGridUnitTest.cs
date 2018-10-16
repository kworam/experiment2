using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class ConnectedCellInAGridUnitTest
    {
        [TestMethod]
        public void TestCase2()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\ConnectedCellInGrid_TestCase2.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int n = Convert.ToInt32(sr.ReadLine());

            int m = Convert.ToInt32(sr.ReadLine());

            int[][] grid = new int[n][];

            for (int i = 0; i < n; i++)
            {
                grid[i] = Array.ConvertAll(sr.ReadLine().Split(' '), gridTemp => Convert.ToInt32(gridTemp));
            }

            int res = ConnectedCellInAGrid.maxRegion(grid);

            Console.WriteLine(res);
        }
    }
}

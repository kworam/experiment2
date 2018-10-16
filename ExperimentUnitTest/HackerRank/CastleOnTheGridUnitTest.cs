using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class CastleOnTheGridUnitTest
    {
        [TestCategory("CastleOnTheGrid"), TestMethod]
        public void Sample0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\CastleOnTheGrid_Sample0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("CastleOnTheGrid"), TestMethod]
        public void Sample1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\CastleOnTheGrid_Sample1.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("CastleOnTheGrid"), TestMethod]
        public void TestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\CastleOnTheGrid_TestCase0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("CastleOnTheGrid"), TestMethod]
        public void TestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\CastleOnTheGrid_TestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int n = Convert.ToInt32(sr.ReadLine());

            string[] grid = new string[n];

            for (int i = 0; i < n; i++)
            {
                string gridItem = sr.ReadLine();
                grid[i] = gridItem;
            }

            string[] startXStartY = sr.ReadLine().Split(' ');

            int startX = Convert.ToInt32(startXStartY[0]);

            int startY = Convert.ToInt32(startXStartY[1]);

            int goalX = Convert.ToInt32(startXStartY[2]);

            int goalY = Convert.ToInt32(startXStartY[3]);

            int result = CastleOnTheGrid.minimumMoves(grid, startX, startY, goalX, goalY);

            Console.WriteLine(result);
        }
    }
}

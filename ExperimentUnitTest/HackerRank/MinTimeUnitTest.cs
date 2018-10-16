using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class MinTimeUnitTest
    {
        [TestCategory("MinTime"), TestMethod]
        public void TestCase6()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MinTime_TestCase6.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("MinTime"), TestMethod]
        public void TestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MinTime_TestCase0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("MinTime"), TestMethod]
        public void TestCase11()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MinTime_TestCase11.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("MinTime"), TestMethod]
        public void TestCase5()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MinTime_TestCase5.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("MinTime"), TestMethod]
        public void TestCase2()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MinTime_TestCase2.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("MinTime"), TestMethod]
        public void TestCase2a()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MinTime_TestCase2a.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            string[] nGoal = sr.ReadLine().Split(' ');

            int n = Convert.ToInt32(nGoal[0]);

            long goal = Convert.ToInt64(nGoal[1]);

            long[] machines = Array.ConvertAll(sr.ReadLine().Split(' '), machinesTemp => Convert.ToInt64(machinesTemp))
            ;
            long ans = MinTime.minTime(machines, goal);

            Console.WriteLine(ans);
        }
    }
}

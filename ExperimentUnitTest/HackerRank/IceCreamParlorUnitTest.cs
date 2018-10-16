using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class IceCreamParlorUnitTest
	{
		[TestCategory("IceCreamParlor"), TestMethod]
        [Timeout(10000)]
        public void TestCaseTwo()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\IceCreamParlor_TestCase2.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int t = Convert.ToInt32(sr.ReadLine());

            for (int tItr = 0; tItr < t; tItr++)
            {
                int money = Convert.ToInt32(sr.ReadLine());
                int n = Convert.ToInt32(sr.ReadLine());
                string line = sr.ReadLine().Trim();
                string[] lines = line.Split(' ');
                int[] cost = Array.ConvertAll(lines, costTemp => Convert.ToInt32(costTemp));

                //IceCreamParlor.whatFlavors(cost, money);
                IceCreamParlor.whatFlavors(cost, money);
            }
        }

        [TestCategory("IceCreamParlor"), TestMethod]
		public void TestCaseZero()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\IceCreamParlor_TestCase0.txt"))
			{
                DoTest(sr);
			}
		}
	}
}

using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class HackerRankUnitTest
	{
		[TestCategory("HackerRank_IceCreamParlor"), TestMethod]
		public void TestCaseZero()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\IceCreamParlor_TesCase0.txt"))
			{
				int t = Convert.ToInt32(sr.ReadLine());

				for (int tItr = 0; tItr < t; tItr++)
				{
					int money = Convert.ToInt32(sr.ReadLine());
					int n = Convert.ToInt32(sr.ReadLine());
					string line = sr.ReadLine();
					string lineTrimmed = line.Trim();
					string[] lines = lineTrimmed.Split(' ');
					int[] cost = Array.ConvertAll(lines, costTemp => Convert.ToInt32(costTemp));

					//IceCreamParlor.whatFlavors(cost, money);
					IceCreamParlor.whatFlavors2(cost, money);
				}
			}
		}

		[TestCategory("HackerRank_IceCreamParlor"), TestMethod]
		public void TestCaseTwo()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\IceCreamParlor_TesCase2.txt"))
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
					IceCreamParlor.whatFlavors2(cost, money);
				}
			}
		}

		[TestCategory("HackerRank_IceCreamParlor"), TestMethod]
		public void TestCaseZeroB()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\IceCreamParlor_TesCase0b.txt"))
			{
				int t = Convert.ToInt32(sr.ReadLine());

				for (int tItr = 0; tItr < t; tItr++)
				{
					int money = Convert.ToInt32(sr.ReadLine());
					int n = Convert.ToInt32(sr.ReadLine());
					string line = sr.ReadLine();
					string lineTrimmed = line.Trim();
					string[] lines = lineTrimmed.Split(' ');
					int[] cost = Array.ConvertAll(lines, costTemp => Convert.ToInt32(costTemp));

					//IceCreamParlor.whatFlavors(cost, money);
					IceCreamParlor.whatFlavors2(cost, money);
				}
			}
		}
	}
}

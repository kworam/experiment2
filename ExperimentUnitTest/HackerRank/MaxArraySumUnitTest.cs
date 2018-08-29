using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class MaxArraySumUnitTest
	{
		[TestCategory("MaxArraySum"), TestMethod]
		public void TestCase0()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\MaxArraySum_TestCase0.txt"))
			{
				string nStr = sr.ReadLine();
				int n = Convert.ToInt32(nStr);

				string line = sr.ReadLine();
				string lineTrimmed = line.Trim();
				string[] lines = lineTrimmed.Split(' ');

				int[] arr = Array.ConvertAll(lines, arrTemp => Convert.ToInt32(arrTemp));
				Assert.AreEqual(n, lines.Length);
				int res = MaxArraySum.maxSubsetSum(arr);
				Console.WriteLine(res);
			}
		}
	}
}

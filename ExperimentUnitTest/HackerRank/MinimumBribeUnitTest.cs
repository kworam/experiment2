using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class MinimumBribeUnitTest
	{
		[TestCategory("MinimumBribe"), TestMethod]
		public void Sample()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\MinimumBribe_SampleInput.txt"))
			{
				int t = Convert.ToInt32(sr.ReadLine());

				for (int qItr = 0; qItr < t; qItr++)
				{
					int n = Convert.ToInt32(sr.ReadLine());

					int[] q = Array.ConvertAll(sr.ReadLine().Split(' '), qTemp => Convert.ToInt32(qTemp));

					//MinimumBribeProblem.minimumBribes(q);
					MinimumBribeProblem.minimumBribes2(q);
				}
			}
		}

		[TestCategory("MinimumBribe"), TestMethod]
		public void TestCase2()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\MinimumBribe_TestCase2.txt"))
			{
				int t = Convert.ToInt32(sr.ReadLine());

				for (int qItr = 0; qItr < t; qItr++)
				{
					int n = Convert.ToInt32(sr.ReadLine());

					int[] q = Array.ConvertAll(sr.ReadLine().Split(' '), qTemp => Convert.ToInt32(qTemp));

					//MinimumBribeProblem.minimumBribes(q);
					MinimumBribeProblem.minimumBribes2(q);
				}
			}
		}
	}
}

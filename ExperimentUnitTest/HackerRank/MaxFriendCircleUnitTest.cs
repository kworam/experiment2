using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class MaxFriendCircleUnitTest
	{
		[TestCategory("MaxFriendCircle"), TestMethod]
		public void TestCase0()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\MaxFriendCircle_TestCase0.txt"))
			{
				DoTest(sr);
			}
		}

		[TestCategory("MaxFriendCircle"), TestMethod]
		//[Timeout(1000)]
		public void TestCase2()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\MaxFriendCircle_TestCase2.txt"))
			{
				DoTest(sr);
			}
		}

		private static void DoTest(StreamReader sr)
		{
			int q = Convert.ToInt32(sr.ReadLine());

			int[][] queries = new int[q][];

			for (int i = 0; i < q; i++)
			{
				queries[i] = Array.ConvertAll(sr.ReadLine().Split(' '), queriesTemp => Convert.ToInt32(queriesTemp));
			}

			int[] ans = MaxFriendCircle.maxCircle(queries);
			Console.WriteLine(string.Join("\n", ans));
		}
	}
}

using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class PrimalityUnitTest
	{
		[TestCategory("Primality"), TestMethod]
		public void TestCase7()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\Primality_TestCase7.txt"))
			{
				int p = Convert.ToInt32(sr.ReadLine());

				for (int tItr = 0; tItr < p; tItr++)
				{
					int n = Convert.ToInt32(sr.ReadLine());
					Console.WriteLine(Primality.primality(n));
				}
			}
		}
	}
}

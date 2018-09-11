using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class SherlockAndAnagramsUnitTest
	{
		[TestCategory("SherlockAndAnagrams"), TestMethod]
		public void TestCase0()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\SherlockAndAnagrams_TestCase0.txt"))
			{
				int q = Convert.ToInt32(sr.ReadLine());

				for (int qItr = 0; qItr < q; qItr++)
				{
					string s = sr.ReadLine();

					int result = SherlockAndAnagramsProblem.SherlockAndAnagrams(s);

					Console.WriteLine(result);
				}
			}
		}
	}
}

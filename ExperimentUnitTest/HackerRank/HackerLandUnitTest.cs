using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class HackerLandUnitTest
	{
		[TestCategory("HackerLand"), TestMethod]
		public void TestCase3()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\HackerLand_TestCase3.txt"))
			{
				int q = Convert.ToInt32(sr.ReadLine());

				for (int qItr = 0; qItr < q; qItr++)
				{
					//					10
					//96295 12709 99058 75153
					string[] nmC_libC_road = sr.ReadLine().Split(' ');

					int n = Convert.ToInt32(nmC_libC_road[0]);

					int m = Convert.ToInt32(nmC_libC_road[1]);

					int c_lib = Convert.ToInt32(nmC_libC_road[2]);

					int c_road = Convert.ToInt32(nmC_libC_road[3]);

					int[][] cities = new int[m][];

					for (int i = 0; i < m; i++)
					{
						cities[i] = Array.ConvertAll(sr.ReadLine().Split(' '), citiesTemp => Convert.ToInt32(citiesTemp));
					}

					long result = HackerLand.roadsAndLibraries(n, c_lib, c_road, cities);

					Console.WriteLine(result);
				}
			}
		}

		[TestCategory("HackerLand"), TestMethod]
		public void TestCase4()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\HackerLand_TestCase4.txt"))
			{
				int q = Convert.ToInt32(sr.ReadLine());

				for (int qItr = 0; qItr < q; qItr++)
				{
					string[] nmC_libC_road = sr.ReadLine().Split(' ');

					int n = Convert.ToInt32(nmC_libC_road[0]);

					int m = Convert.ToInt32(nmC_libC_road[1]);

					int c_lib = Convert.ToInt32(nmC_libC_road[2]);

					int c_road = Convert.ToInt32(nmC_libC_road[3]);

					int[][] cities = new int[m][];

					for (int i = 0; i < m; i++)
					{
						cities[i] = Array.ConvertAll(sr.ReadLine().Split(' '), citiesTemp => Convert.ToInt32(citiesTemp));
					}

					long result = HackerLand.roadsAndLibraries(n, c_lib, c_road, cities);

					Console.WriteLine(result);
				}
			}
		}
	}
}

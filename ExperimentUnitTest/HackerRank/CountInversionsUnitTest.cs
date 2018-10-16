using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class CountInversionsUnitTest
    {
        [TestCategory("CountInversions"), TestMethod]
        public void TestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\CountInversions_TestCase0.txt"))
            {
                int t = Convert.ToInt32(sr.ReadLine());

                for (int tItr = 0; tItr < t; tItr++)
                {
                    int n = Convert.ToInt32(sr.ReadLine());

                    int[] arr = Array.ConvertAll(sr.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
                    ;
                    long result = CountInversions.countInversions(arr);

                    Console.WriteLine(result);
                }

            }
        }
    }
}

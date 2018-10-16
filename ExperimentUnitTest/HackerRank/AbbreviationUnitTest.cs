using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class AbbreviationUnitTest
	{
		[TestCategory("Abbreviation"), TestMethod]
		public void TestCase12()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\Abbreviation_TestCase12.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int q = Convert.ToInt32(sr.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string a = sr.ReadLine();

                string b = sr.ReadLine();

                string result = AbbreviationProblem.Abbreviation(a, b);

                Console.WriteLine(result);
            }
        }
    }
}
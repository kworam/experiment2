using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class StringConstructionUnitTest
    {
        [TestCategory("StringConstruction"), TestMethod]
        public void TestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\StringConstruction_TestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int q = Convert.ToInt32(sr.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string s = sr.ReadLine();

                int result = StringConstruction.stringConstruction(s);

                Console.WriteLine(result);
            }
        }
    }
}

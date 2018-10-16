using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class CommonChildUnitTest
    {
        [TestCategory("CommonChild"), TestMethod]
        public void TestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\CommonChild_TestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("CommonChild"), TestMethod]
        public void Sample0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\CommonChild_Sample0.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            string s1 = sr.ReadLine();

            string s2 = sr.ReadLine();

            int result = CommonChild.commonChild(s1, s2);

            Console.WriteLine(result);
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(CommonChild.lcsString(s1, s2));
        }
    }
}

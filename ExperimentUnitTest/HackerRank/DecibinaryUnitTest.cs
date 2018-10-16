using System;
using System.Collections.Generic;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class DecibinaryUnitTest
    {
        [TestCategory("Decibinary"), TestMethod]
        public void DiscussionTest()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\Decibinary_DiscussionTest.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("Decibinary"), TestMethod]
        public void TestCase3()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\Decibinary_TestCase3.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int q = Convert.ToInt32(sr.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                long x = Convert.ToInt64(sr.ReadLine());

                long result = Decibinary.GetSpecifiedDeciBinaryNumber(x);

                Console.WriteLine(result);
            }
        }

        [TestCategory("Decibinary"), TestMethod]
        public void GetSpecificDeciBinaryNumber()
        {
            int groupSize = 10;
            for (int x=1; x<=groupSize * 10; x++)
            {
                if (x % 10 == 0)
                {
                    Console.WriteLine();
                }
                Console.WriteLine(Decibinary.GetSpecifiedDeciBinaryNumber(x));
            }
        }

        [TestCategory("Decibinary"), TestMethod]
        public void TestGenerator()
        {
            //GenerateForN(0);
            //GenerateForN(1);
            //GenerateForN(2);
            //GenerateForN(3);
            GenerateForN(4);
        }

        private static void GenerateForN(int n)
        {
            List<long> result = Decibinary.generateAllDbReps(n);
            Console.WriteLine(string.Format("decibinary representations of {0}", n));
            foreach (int dbrep in result)
            {
                Console.WriteLine(dbrep);
            }
            Console.WriteLine();
        }
    }
}

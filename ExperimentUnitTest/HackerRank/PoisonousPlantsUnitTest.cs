using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class PoisonousPlantsUnitTest
    {
        public void TestMethod1()
        {
        }
        
        [TestCategory("PoisonousPlants"), TestMethod]
        public void SampleInput1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\PoisonousPlants_SampleInput1.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("PoisonousPlants"), TestMethod]
        public void TestCase2()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\PoisonousPlants_TestCase2.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int n = Convert.ToInt32(sr.ReadLine());

            int[] p = Array.ConvertAll(sr.ReadLine().Split(' '), pTemp => Convert.ToInt32(pTemp))
            ;
            int result = PoisonousPlants.poisonousPlants(p);

            Console.WriteLine(result);
        }
    }
}

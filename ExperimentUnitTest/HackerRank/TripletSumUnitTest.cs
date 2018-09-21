using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class TripletSumUnitTest
    {
        [TestCategory("TripletSum"), TestMethod]
        public void TestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\TripletSum_TestCase0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("TripletSum"), TestMethod]
        public void TestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\TripletSum_TestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("TripletSum"), TestMethod]
        public void TestCase2()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\TripletSum_TestCase2.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            string[] lenaLenbLenc = sr.ReadLine().Split(' ');

            int lena = Convert.ToInt32(lenaLenbLenc[0]);

            int lenb = Convert.ToInt32(lenaLenbLenc[1]);

            int lenc = Convert.ToInt32(lenaLenbLenc[2]);

            int[] arra = Array.ConvertAll(sr.ReadLine().Split(' '), arraTemp => Convert.ToInt32(arraTemp))
            ;

            int[] arrb = Array.ConvertAll(sr.ReadLine().Split(' '), arrbTemp => Convert.ToInt32(arrbTemp))
            ;

            int[] arrc = Array.ConvertAll(sr.ReadLine().Split(' '), arrcTemp => Convert.ToInt32(arrcTemp))
            ;
            //long ans = TripletSum.triplets(arra, arrb, arrc);
            long ans = TripletSum.triplets(arra, arrb, arrc);

            Console.WriteLine(ans);
        }
    }
}

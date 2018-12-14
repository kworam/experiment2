using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class MagicSquareUnitTest
    {
        [TestCategory("MagicSquare"), TestMethod]
        public void SampleTestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\MagicSquare_SampleTestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int[][] s = new int[3][];

            for (int i = 0; i < 3; i++)
            {
                s[i] = Array.ConvertAll(sr.ReadLine().Split(' '), sTemp => Convert.ToInt32(sTemp));
            }

            int result = MagicSquare.formingMagicSquare(s);

            Console.WriteLine(result);
        }
    }
}

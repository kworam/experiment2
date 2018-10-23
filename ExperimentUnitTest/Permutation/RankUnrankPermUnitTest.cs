using System;
using Experiment.Permutation;
using Experiment.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Permutation
{
    [TestClass]
    public class RankUnrankPermUnitTest
    {
        [TestCategory("RankUnrankPerms"), TestMethod]
        public void UnrankSingletonString()
        {
            string s = "x";
            int n = s.Length;
            long r = KevinMath.LongRandom(0, KevinMath.Factorial(n) - 1, new Random());
            int[] pi = RankUnrank.InitIndexArray(n);
            RankUnrank.Unrank(n, r, pi);

            string ps = GetPermString(s, pi);
            Console.WriteLine(string.Format("r:{0} s:{1}  p:{2}", r, s, ps));
        }

        [TestCategory("RankUnrankPerms"), TestMethod]
        public void UnrankStringLenTwo()
        {
            string s = "xy";
            int n = s.Length;
            Console.WriteLine(string.Format("s:{0}", s));
            for (int r=0; r<n; r++)
            {
                int[] pi = RankUnrank.InitIndexArray(n);
                RankUnrank.Unrank(n, r, pi);

                string ps = GetPermString(s, pi);
                Console.WriteLine(string.Format("r:{0} p:{1}", r, ps));

                int[] inv = RankUnrank.GetInverseArray(pi);
                long rank = RankUnrank.Rank(n, pi, inv);
                Assert.AreEqual(r, rank);
            }
        }

        [TestCategory("RankUnrankPerms"), TestMethod]
        public void UnrankStringLenThree()
        {
            string s = "xyz";
            int n = s.Length;
            Console.WriteLine(string.Format("s:{0}", s));
            for (int r = 0; r < KevinMath.Factorial(n); r++)
            {
                int[] pi = RankUnrank.InitIndexArray(n);
                RankUnrank.Unrank(n, r, pi);

                string ps = GetPermString(s, pi);
                Console.WriteLine(string.Format("r:{0} p:{1}", r, ps));

                //int[] inv = RankUnrank.GetInverseArray(pi);
                //long rank = RankUnrank.Rank(n, pi, inv);
                //Assert.AreEqual(r, rank);
            }
        }

        private string GetPermString(string s, int[] pi)
        {
            char[] c = new char[pi.Length];
            for(int i=0; i<pi.Length; i++)
            {
                c[i] = s[pi[i]];
            }
            return new string(c);
        }
    }
}

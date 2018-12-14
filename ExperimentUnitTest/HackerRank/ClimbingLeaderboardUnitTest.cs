using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class ClimbingLeaderboardUnitTest
    {
        [TestCategory("ClimbingLeaderboard"),TestMethod]
        public void SampleTestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\ClimbingLeaderboard_SampleTest0.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            int scoresCount = Convert.ToInt32(sr.ReadLine());

            int[] scores = Array.ConvertAll(sr.ReadLine().Split(' '), scoresTemp => Convert.ToInt32(scoresTemp))
            ;
            int aliceCount = Convert.ToInt32(sr.ReadLine());

            int[] alice = Array.ConvertAll(sr.ReadLine().Split(' '), aliceTemp => Convert.ToInt32(aliceTemp))
            ;
            int[] result = ClimbingLeaderboard.climbingLeaderboard(scores, alice);

            Console.WriteLine(string.Join("\n", result));
        }
    }
}

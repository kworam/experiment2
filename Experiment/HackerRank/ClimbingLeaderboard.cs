using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class ClimbingLeaderboard
    {
        // Complete the climbingLeaderboard function below.
        public static int[] climbingLeaderboard(int[] scores, int[] alice)
        {
            List<int> index = GetIndex(scores, alice[0]);
            return GetRanks(index, alice);
        }
        static List<int> GetIndex(int[] scores, int minScore)
        {
            List<int> index = new List<int>();
            int ps = scores[0];
            index.Add(ps);
            for (int i = 1; i < scores.Length; i++)
            {
                int cs = scores[i];
                if (cs != ps)
                {
                    index.Add(cs);
                    ps = cs;

                    if (ps < minScore)
                    {
                        break;
                    }
                }
            }
            return index;
        }

        static int[] GetRanks(List<int> index, int[] alice)
        {
            // alice is sorted ascending
            int[] result = new int[alice.Length];
            //int hi = result.Length - 1;
            for (int i = 0; i < alice.Length; i++)
            {
                int cs = alice[i];
                int closestIndex = FindClosest(index, cs, 0, index.Count - 1, -1);
                if (closestIndex == index.Count)
                {
                    result[i] = index.Count + 1;
                }
                else if (closestIndex < 0)
                {
                    result[i] = 1;
                }
                else if (index[closestIndex] == cs)
                {
                    result[i] = closestIndex + 1;
                }
                else if (index[closestIndex] > cs)
                {
                    result[i] = closestIndex + 2;
                }
                else // index[closestIndex] < cs
                {
                    result[i] = closestIndex + 1;
                }
            }

            return result;
        }

        static int FindClosest(List<int> index, int score, int lo, int hi, int lastIndex)
        {
            // index is sorted descending
            if (lo == index.Count) return lo;
            if (hi < 0) return hi;
            if (lo > hi) return lastIndex;

            int mid = (lo + hi) / 2;
            if (score == index[mid])
            {
                return mid;
            }
            else if (score > index[mid])
            {
                return FindClosest(index, score, lo, mid - 1, mid);
            }
            else  // score < index[mid]
            {
                return FindClosest(index, score, mid + 1, hi, mid);
            }
        }
    }
}

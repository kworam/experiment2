using System;
using System.Linq;

namespace Experiment.HackerRank
{
    public class MiniMaxSum
    {
        public static void miniMaxSum(int[] arr)
        {
            long totalSum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                totalSum += arr[i];
            }

            long minSum = long.MaxValue;
            long maxSum = long.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                long thisSum = totalSum - arr[i];
                minSum = Math.Min(minSum, thisSum);
                maxSum = Math.Max(maxSum, thisSum);
            }
            Console.WriteLine(string.Format("{0} {1}", minSum, maxSum));
        }
    }
}

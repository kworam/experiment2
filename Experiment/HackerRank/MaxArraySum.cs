using System;
using System.Collections.Generic;


namespace Experiment.HackerRank
{
	public class MaxArraySum
	{
		// Complete the maxSubsetSum function below.
		public static int maxSubsetSum(int[] arr)
		{
			if (arr.Length == 1)
			{
				return arr[0];
			}
			int maxEvenSum = arr[0];
			int maxOddSum = Math.Max(maxEvenSum, arr[1]);

			for (int i = 2; i < arr.Length; i++)
			{
				if (i % 2 == 0)
				{
					maxEvenSum = Math.Max(Math.Max(maxEvenSum, maxOddSum), Math.Max(arr[i], arr[i] + maxEvenSum));
				}
				else
				{
					maxOddSum = Math.Max(Math.Max(maxEvenSum, maxOddSum), Math.Max(arr[i], arr[i] + maxOddSum));
				}
			}

			return Math.Max(maxEvenSum, maxOddSum);
		}

		//public static int maxSubsetSum(int[] arr)
		//{
		//	return InternalMaxSubsetSum(arr, 0, new Dictionary<int, int>());
		//}

		//private static int InternalMaxSubsetSum(int[] arr, int startIndex, Dictionary<int, int> cache)
		//{
		//	if (cache.ContainsKey(startIndex))
		//	{
		//		return cache[startIndex];
		//	}

		//	if (startIndex >= arr.Length)
		//	{
		//		return 0;
		//	}

		//	int maxSubsetSum = int.MinValue;
		//	for (int i = startIndex; i < arr.Length; i++)
		//	{
		//		maxSubsetSum = Math.Max(maxSubsetSum, arr[startIndex] + InternalMaxSubsetSum(arr, i + 2, cache));
		//	}
		//	for (int i = startIndex + 1; i < arr.Length; i++)
		//	{
		//		maxSubsetSum = Math.Max(maxSubsetSum, arr[startIndex + 1] + InternalMaxSubsetSum(arr, i + 2, cache));
		//	}

		//	cache[startIndex] = maxSubsetSum;
		//	return maxSubsetSum;
		//}

	}
}

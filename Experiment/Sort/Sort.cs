using System;

namespace Experiment.Sort
{
	public class Sort
	{
		public static void SortToPeaksAndValleys(int[] a)
		{
			if (a == null || a.Length == 1)
			{
				return;
			}

			Array.Sort(a);
			PairwiseSwap(a);
		}

		public static void SortToPeaksAndValleysOptimal(int[] a)
		{
			if (a == null || a.Length < 3)
			{
				return;
			}

			for (int i = 1; i < a.Length; i += 2)
			{
				int max = GetMax(a, i - 1, i, i + 1);
				if (a[i - 1] == max)
				{
					ArrayUtility.Swap(a, i - 1, i);
				} 
				else if (i < a.Length-1 && a[i + 1] == max)
				{
					ArrayUtility.Swap(a, i + 1, i);
				}
			}
		}

		private static int GetMax(int[] a, int left, int middle, int right)
		{
			int leftValue = left < 0 ? int.MinValue : a[left];
			int middleValue = a[middle];
			int rightValue = right > a.Length - 1 ? int.MinValue : a[right];
			return Math.Max(Math.Max(leftValue, middleValue), rightValue);
		}

		private static void PairwiseSwap(int[] a)
		{
			for (int i = 0; i < a.Length/2; i++)
			{
				int tmp = a[i*2];
				a[i*2] = a[i*2 + 1];
				a[i*2 + 1] = tmp;
			}
		}
	}
}

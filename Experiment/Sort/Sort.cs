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

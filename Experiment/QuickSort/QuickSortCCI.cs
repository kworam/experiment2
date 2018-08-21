using System;

namespace Experiment
{
	public class QuickSortCCI
	{
		//public static void Sort(int[] a)
		//{
		//	if (a == null) return;

		//	SortInternal(a, 0, a.Length - 1);
		//}

		//private static void SortInternal(int[] a, int start, int end)
		//{
		//	if (end - start <= 0)
		//	{
		//		return;
		//	}

		//	int pi = Partition(a, start, end, null);
		//	SortInternal(a, start, pi-1);
		//	SortInternal(a, pi+1, end);
		//}

		public static int Partition(int[] a, int start, int end, QuickSortStatistics stats)
		{
			int pv = a[end];

			while (start < end)
			{
				while (a[start] <= pv && start < end) start++;
				while (a[end] > pv && end > start) end--;

				if (start < end)
				{
					ArrayUtility.Swap(a, start, end);
				}
			}

			return start;
		}
	}
}

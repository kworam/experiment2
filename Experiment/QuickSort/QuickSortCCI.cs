using System;

namespace Experiment
{
	public class QuickSortCCI
	{
		//public static int Partition(int[] a, int start, int end, QuickSortStatistics stats)
		//{
		//	int pv = a[end];

		//	while (start < end)
		//	{
		//		while (a[start] <= pv && start < end) start++;
		//		while (a[end] > pv && end > start) end--;

		//		if (start < end)
		//		{
		//			ArrayUtility.Swap(a, start, end);
		//		}
		//	}

		//	return start;
		//}

        public static int Partition(int[] a, int start, int end, QuickSortStatistics stats)
        {
            int pv = a[end];

            int left = start - 1;
            int right = end - 1;
            while (left < right)
            {
                if (a[left+1] < pv)
                {
                    left++;
                }
                else
                {
                    ArrayUtility.Swap(a, left+1, right);
                    right--;
                }
            }
            left++;
            ArrayUtility.Swap(a, left, end);

            return left;
        }
    }
}

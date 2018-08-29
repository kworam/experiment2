using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.HackerRank
{
	public class IceCreamParlor
	{
		// Complete the whatFlavors function below.
		//public static void whatFlavors(int[] cost, int money)
		//{
		//	int[] sortedCost = cost.Clone() as int[];
		//	Array.Sort(sortedCost);

		//	for (int i = 1; i <= (money / 2); i++)
		//	{
		//		int aIndex = BinSearch(sortedCost, i);
		//		if (aIndex >= 0)
		//		{
		//			int bIndex = BinSearch(sortedCost, money - i);
		//			if (bIndex >= 0)
		//			{
		//				int aIndexOriginal = FindCost(cost, i, 0);
		//				int bStartIndex = i == money - i ? aIndexOriginal + 1 : 0;
		//				int bIndexOriginal = FindCost(cost, money - i, bStartIndex);

		//				aIndexOriginal++;
		//				bIndexOriginal++;
		//				Console.WriteLine(string.Format("{0} {1}",
		//					Math.Min(aIndexOriginal, bIndexOriginal),
		//					Math.Max(aIndexOriginal, bIndexOriginal)));
		//				break;
		//			}
		//		}
		//	}
		//}

		public static void whatFlavors(int[] cost, int money)
		{
			int[] sortedCost = cost.Clone() as int[];
			Array.Sort(sortedCost);

			for (int firstCostSortedIndex = 0; firstCostSortedIndex < sortedCost.Length; firstCostSortedIndex++)
			{
				int firstCost = sortedCost[firstCostSortedIndex];
				if (firstCost >= money)
				{
					continue;
				}

				int secondCost = money - firstCost;
				if (secondCost == firstCost && sortedCost[firstCostSortedIndex+1] != firstCost)
				{
					continue;
				}

				int secondCostSortedIndex = BinSearch(sortedCost, secondCost);
				if (secondCostSortedIndex < 0)
				{
					continue;
				}

				int firstCostIndex = FindCost(cost, firstCost, 0);
				int bStartIndex = firstCost == secondCost ? firstCostIndex + 1 : 0;
				int secondCostIndex = FindCost(cost, secondCost, bStartIndex);

				firstCostIndex++;
				secondCostIndex++;
				Console.WriteLine(string.Format("{0} {1}",
					Math.Min(firstCostIndex, secondCostIndex),
					Math.Max(firstCostIndex, secondCostIndex)));
				break;
			}
		}

		static int FindCost(int[] a, int val, int startIndex)
		{
			for (int i = startIndex; i < a.Length; i++)
			{
				if (a[i] == val) return i;
			}

			return -1;
		}

		static int BinSearch(int[] a, int val)
		{
			return InternalBinSearch(a, val, 0, a.Length - 1);
		}

		static int InternalBinSearch(int[] a, int val, int start, int end)
		{
			if (start > end)
			{
				return -1;
			}

			int mid = (end + start) / 2;
			if (a[mid] < val)
			{
				return InternalBinSearch(a, val, mid + 1, end);
			}
			else if (a[mid] > val)
			{
				return InternalBinSearch(a, val, start, mid - 1);
			}
			else
			{
				return mid;
			}
		}
	}
}


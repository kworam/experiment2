using System.Xml.Schema;

namespace Experiment.Search
{
	public class SearchInSortedRotatedArray
	{
		public static int Search(int[] ra, int val)
		{
			// assumption: ra is an array sorted in ascending order that has been rotated by an unknown amount
			if (ra == null || ra.Length == 0) return -1;
			int minIndex = FindMinIndex(ra, 0, ra.Length - 1);
			return InternalSearch(ra, val, 0, ra.Length - 1, minIndex);
		}

		private static int InternalSearch(int[] ra, int val, int start, int end, int offset)
		{
			while (start <= end)
			{
				int mid = (start + end) / 2;
				int adjustedMid = (mid + offset) % ra.Length;
				if (ra[adjustedMid] < val) start = mid + 1;
				else if (ra[adjustedMid] > val) end = mid - 1;
				else return adjustedMid;
			}

			return -1;
		}


		private static int FindMinIndex(int[] ra, int start, int end)
		{
			if (end == start || end == start + 1) return end;

			int mid = (start + end) / 2;
			if (ra[start] > ra[mid]) return FindMinIndex(ra, start, mid);
			if (ra[mid] > ra[end]) return FindMinIndex(ra, mid, end);
			return start;
		}
	}
}

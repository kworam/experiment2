namespace Experiment
{
	public class QuickSortKevinPartition
	{
		public static int PartitionArray(int[] array, int beginIndex, int endIndex, QuickSortStatistics stats)
		{
			// assumptions: there are at least two elements in the input subarray

			// invariant:
			// all elements with index >= b are >= partition value (PV)
			// all elements with index <= a are < PV

			int partitionValueIndex = endIndex;
			int partitionValue = array[partitionValueIndex];

			int a = beginIndex - 1;
			int b = endIndex - 1;
			while (b > a + 1)
			{
				if (array[b] < partitionValue)
				{
					a++;
					ArrayUtility.Swap(array, a, b);
					stats.numSwaps++;
				}
				else
				{
					b--;
				}
			}

			if (array[b] < partitionValue)
			{
				b++;
			}

			int tmp = array[b];
			array[b] = partitionValue;
			array[partitionValueIndex] = tmp;
			stats.numSwaps++;

			return b;
		}

	}
}

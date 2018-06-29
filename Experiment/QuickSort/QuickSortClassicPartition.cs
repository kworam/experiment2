namespace Experiment
{
	public class QuickSortClassicPartition
	{
		public static int PartitionArray(int[] array, int beginIndex, int endIndex, QuickSortStatistics stats)
		{
			// assumptions: there are at least two elements in the input subarray

			// invariant:
			// all elements with index >= b are >= partition value (PV)
			// all elements with index <= a are < PV

			int partitionValue = array[endIndex];

			int endLesserPartitionIndex = beginIndex - 1;
			int currentIndex = beginIndex;
			while (currentIndex < endIndex)
			{
				if (array[currentIndex] < partitionValue)
				{
					endLesserPartitionIndex++;
					ArrayUtility.Swap(array, currentIndex, endLesserPartitionIndex);
					stats.numSwaps++;
				}
				currentIndex++;
			}

			int partitionIndex = endLesserPartitionIndex + 1;
			ArrayUtility.Swap(array, partitionIndex, endIndex);
			stats.numSwaps++;

			return partitionIndex;
		}

	}
}

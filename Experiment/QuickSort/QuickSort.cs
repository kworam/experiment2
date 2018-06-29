namespace Experiment
{
    public class QuickSort
    {
		public static QuickSortStatistics stats;

		public delegate int PartitionArrayFunc(
			int[] array, 
			int beginIndex, 
			int endIndex, 
			QuickSortStatistics stats
			);

		public static void Sort(int[] array, PartitionArrayFunc partitionFunc = null)
        {
			partitionFunc = partitionFunc ?? QuickSortClassicPartition.PartitionArray;

			QuickSort.stats = new QuickSortStatistics();

            if (array == null || array.Length <= 1)
            {
                // null, empty, and singleton arrays are already sorted
                return;
            }

            QuickSortInternal(array, 0, array.Length - 1, partitionFunc);
        }

        private static void QuickSortInternal(int[] array, int beginIndex, int endIndex, PartitionArrayFunc partitionFunc)
        {
			QuickSort.stats.numRecursions++;

            if (((endIndex - beginIndex) + 1) < 2)
            {
                // empty and singleton arrays are sorted
                return;
            }

			// the array has two or more elements, 
			// partition the array and quicksort the left and right partitions
			//int partitionIndex = PartitionArray(array, beginIndex, endIndex);
			int partitionIndex = partitionFunc(array, beginIndex, endIndex, stats);
			QuickSortInternal(array, beginIndex, partitionIndex - 1, partitionFunc);
            QuickSortInternal(array, partitionIndex + 1, endIndex, partitionFunc);
        }
    }
}

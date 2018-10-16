namespace Experiment.HackerRank
{
    public class CountInversions
    {
        // Complete the countInversions function below.
        public static long countInversions(int[] arr)
        {
            Counter counter = new Counter();
            int[] copy = new int[arr.Length];
            MergeSortCountSwaps(arr, 0, arr.Length - 1, copy, counter);
            return counter.total;
        }

        private class Counter
        {
            public long total = 0;

            public override string ToString()
            {
                return total.ToString();
            }
        }

        private static void MergeSortCountSwaps(
            int[] arr, int start, int end, int[] copy, Counter counter)
        {
            if (end - start < 1)
            {
                return;
            }

            int mid = (end + start) / 2;
            MergeSortCountSwaps(arr, start, mid, copy, counter);
            MergeSortCountSwaps(arr, mid + 1, end, copy, counter);
            Merge(arr, start, mid, end, copy, counter);
        }

        private static void Merge(
            int[] arr, int start, int mid, int end, int[] copy, Counter counter)
        {
            CopyArray(copy, arr, start, end);

            int idxTarget = start;
            int idxLeft = start;
            int idxRight = mid+1;
            while (idxLeft <= mid && idxRight <= end)
            {
                if (copy[idxLeft] > copy[idxRight])
                {
                    arr[idxTarget++] = copy[idxRight++];
                    //counter.total += (idxRight - idxLeft);
                    //counter.total++;
                    counter.total += (mid - idxLeft + 1);
                }
                else
                {
                    arr[idxTarget++] = copy[idxLeft++];
                }
            }

            // the left copy or the right copy are exhausted

            // if there are remaining elements in the right copy, 
            // they are already in sorted order in arr.

            // copy remaining elements in the left copy to the end of the target
            while (idxLeft <= mid)
            {
                arr[idxTarget++] = copy[idxLeft++];
            }
        }
        private static void CopyArray(int[] target, int[] source, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                target[i] = source[i];
            }
        }
    }
}

namespace Experiment
{
	public class HeapSort
	{
		public static void Sort(int[] a)
		{
		    if (a == null || a.Length <= 1)
		    {
                // null, empty, and singleton arrays are already sorted
		        return;
		    }

			MinHeap h = HeapFactory.CreateMinHeap(a);
			for (int i = 0; i < a.Length; i++)
			{
				a[i] = h.PopMin();
			}
		}
	}
}

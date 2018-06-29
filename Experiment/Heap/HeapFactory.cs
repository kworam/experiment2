namespace Experiment
{
	public static class HeapFactory
	{
		public static MinHeap CreateMinHeap(int[] a)
		{
			return new KevinMinHeap(a);
		}
	}
}

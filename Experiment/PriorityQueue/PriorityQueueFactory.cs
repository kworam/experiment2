namespace Experiment
{
	public static class PriorityQueueFactory
	{
		public static PriorityQueue CreatePriorityQueue()
		{
			return new KevinPriorityQueue();
		}
	}
}

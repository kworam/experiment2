namespace Experiment
{
	public interface PriorityQueueNode
	{
		int Priority { get; set;  }

		HasUniqueKey Data { get; }
	}
}

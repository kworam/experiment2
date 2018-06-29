namespace Experiment
{
	public interface PriorityQueue
	{
		int Count();

		void Enqueue(PriorityQueueNode node);

		PriorityQueueNode Dequeue();

		void ChangePriority(string nodeUniqueKey, int priority);

		PriorityQueueNode Peek();

		PriorityQueueNode Peek(string nodeUniqueKey);

		bool Contains(string nodeUniqueKey);
	}
}

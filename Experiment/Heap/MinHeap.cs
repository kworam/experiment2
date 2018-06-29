namespace Experiment
{
	public interface MinHeap
	{
		int GetCount();

		int PopMin();

		int PeekMin();

		void Add(int value);
	}
}

using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;

namespace Experiment
{
	public class KevinPriorityQueue : PriorityQueue
	{
	    private class PriorityQueueNodeWrapper : PriorityQueueNode
	    {
            private readonly PriorityQueueNode node;
            
            public PriorityQueueNodeWrapper(PriorityQueueNode node)
	        {
	            this.node = node;
	            this.Sequence = KevinPriorityQueue.GetNextSequenceNumber();
	        }

	        public long Sequence { get; private set; }

            public int Priority
	        {
                get { return this.node.Priority; }
                set { this.node.Priority = value; }	            
	        }

	        public HasUniqueKey Data
	        {
                get { return this.node.Data; }	            
	        }

        }

	    private static long sequenceNumber;

		private readonly Dictionary<string, int> nodeUniqueKeyToHeapIndexMap = new Dictionary<string, int>();
        private readonly List<PriorityQueueNodeWrapper> minHeap = new List<PriorityQueueNodeWrapper>();
		private int heapCount;

		public int Count()
		{
			return heapCount;
		}

		public void Enqueue(PriorityQueueNode node)
		{
            int lastIndex = Count();
            PriorityQueueNodeWrapper wrapper = new PriorityQueueNodeWrapper(node);
			AttachNodeAtIndex(wrapper, lastIndex);
			SiftUp(lastIndex);
		}

		public PriorityQueueNode Dequeue()
		{
			if (minHeap.Count == 0)
			{
				throw new PriorityQueueEmptyException();
			}

			int topIndex = 0;
			PriorityQueueNodeWrapper min = DetachNodeAtIndex(topIndex);

            int lastIndex = Count();
			PriorityQueueNodeWrapper last = DetachNodeAtIndex(lastIndex);

			AttachNodeAtIndex(last, topIndex);

			SiftDown(0);

		    return min;
		}

		public void ChangePriority(string nodeUniqueKey, int priority)
		{
			int heapIndex = GetPriorityQueueNodeIndex(nodeUniqueKey);
			PriorityQueueNode node = minHeap[heapIndex]; 

			if (priority < node.Priority)
			{
				node.Priority = priority;
				SiftUp(heapIndex);
			}
			else if (priority > node.Priority)
			{
				node.Priority = priority;
				SiftDown(heapIndex);
			}
		}

        public PriorityQueueNode Peek()
        {
            if (minHeap.Count == 0)
            {
                throw new PriorityQueueEmptyException();
            }

            return this.minHeap[0];
        }

        public PriorityQueueNode Peek(string nodeUniqueKey)
		{
			int heapIndex = GetPriorityQueueNodeIndex(nodeUniqueKey);
			return minHeap[heapIndex];
		}

		public bool Contains(string nodeUniqueKey)
		{
			return this.nodeUniqueKeyToHeapIndexMap.ContainsKey(nodeUniqueKey);
		}

		private int GetPriorityQueueNodeIndex(string nodeUniqueKey)
		{
			if (!Contains(nodeUniqueKey))
			{
				throw new PriorityQueueNodeNotFoundException(
					string.Format("node with key '{0}' not found in queue", nodeUniqueKey));
			}

			return this.nodeUniqueKeyToHeapIndexMap[nodeUniqueKey];
		}

		public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < heapCount; i++)
            {
                PriorityQueueNodeWrapper wrapper = minHeap[i];
                sb.AppendLine(string.Format("HeapIndex: {0}  UniqueKey: {1}  Priority: {2}  Sequence: {3}", 
                    i, wrapper.Data.UniqueKey, wrapper.Priority, wrapper.Sequence));
            }
            return sb.ToString();
        }


	    [MethodImpl(MethodImplOptions.Synchronized)]
	    private static long GetNextSequenceNumber()
        {
            return KevinPriorityQueue.sequenceNumber++;
        }

        private void AttachNodeAtIndex(PriorityQueueNodeWrapper wrapper, int index)
		{
			GrowHeapByOne();

            this.minHeap[index] = wrapper;
			this.nodeUniqueKeyToHeapIndexMap[wrapper.Data.UniqueKey] = index;
		}

	    private void GrowHeapByOne()
	    {
	        this.heapCount++;
	        if (this.heapCount > this.minHeap.Count)
	        {
	            this.minHeap.Add(null);
	        }
	    }

	    private PriorityQueueNodeWrapper DetachNodeAtIndex(int index)
		{
            PriorityQueueNodeWrapper wrapper = minHeap[index];
			this.nodeUniqueKeyToHeapIndexMap.Remove(wrapper.Data.UniqueKey);
            ShrinkHeapByOne();

			return wrapper;
		}

        private void ShrinkHeapByOne()
        {
            this.heapCount--;
        }
        
        private void SiftUp(int index)
		{
			int parentIndex = GetParentIndex(index);
            if (ComparePriorities(minHeap[index], minHeap[parentIndex]) < 0)
			{
				SwapAndUpdateMap(index, parentIndex);
                SiftUp(parentIndex);
			}
		}

	    private int ComparePriorities(PriorityQueueNodeWrapper node1, PriorityQueueNodeWrapper node2)
	    {
	        int result = node1.Priority.CompareTo(node2.Priority);
	        if (result == 0)
	        {
	            result = node1.Sequence.CompareTo(node2.Sequence);
	        }

	        return result;
	    }


		private void SiftDown(int index)
		{
			PriorityQueueNodeWrapper min = minHeap[index];
			int minIndex = index;

			int leftChildIndex = GetLeftChildIndex(index);
            if (leftChildIndex < Count() && ComparePriorities(minHeap[leftChildIndex], min) < 0)
			{
				min = minHeap[leftChildIndex];
				minIndex = leftChildIndex;
			}

			int rightChildIndex = GetRightChildIndex(index);
            if (rightChildIndex < Count() && ComparePriorities(minHeap[rightChildIndex], min) < 0)
			{
				minIndex = rightChildIndex;
			}

			if (minIndex != index)
			{
				SwapAndUpdateMap(index, minIndex);
				SiftDown(minIndex);
			}
		}

		private int GetParentIndex(int childIndex)
		{
			return (childIndex - 1) / 2;
		}

		private int GetLeftChildIndex(int parentIndex)
		{
			return (parentIndex * 2) + 1;
		}

		private int GetRightChildIndex(int parentIndex)
		{
			return (parentIndex * 2) + 2;
		}

		private void SwapAndUpdateMap(int index1, int index2)
		{
			PriorityQueueNode node1 = this.minHeap[index1];
			PriorityQueueNode node2 = this.minHeap[index2];

			MapSwap(node1, node2);

			ArrayUtility.Swap(this.minHeap, index1, index2);
		}

		private void MapSwap(PriorityQueueNode node1, PriorityQueueNode node2)
		{
			int tmp = this.nodeUniqueKeyToHeapIndexMap[node1.Data.UniqueKey];

			this.nodeUniqueKeyToHeapIndexMap[node1.Data.UniqueKey] =
				this.nodeUniqueKeyToHeapIndexMap[node2.Data.UniqueKey];

			this.nodeUniqueKeyToHeapIndexMap[node2.Data.UniqueKey] = tmp;
		}
	}
}

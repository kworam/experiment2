using System.Linq;
using System.Collections.Generic;

namespace Experiment
{
	public class KevinMinHeap : MinHeap
	{
		private readonly List<int> binaryHeap;
		private int size;

		public KevinMinHeap(int[] a)
		{
			this.binaryHeap = this.Heapify(a);
			this.size = binaryHeap.Count();
		}

		public int PeekMin()
		{
			if (binaryHeap.Count() == 0)
			{
				throw new HeapEmptyException();
			}
			return binaryHeap[0];
		}

		public int PopMin()
		{
			if (binaryHeap.Count() == 0)
			{
				throw new HeapEmptyException();
			}

			int min = binaryHeap[0];

			binaryHeap[0] = binaryHeap[size - 1];
			size = size - 1;
			SiftDown(binaryHeap, 0);

			return min;
		}

		public int GetCount()
		{
			return this.size;
		}

		public void Add(int value)
		{
			if (this.size == this.binaryHeap.Count())
			{
				this.binaryHeap.Add(0);
			}

			this.size++;
			this.binaryHeap[size - 1] = value;

			SiftUp(binaryHeap, size - 1);
		}

	    public override string ToString()
	    {
	        return string.Join(" ", binaryHeap.Take(this.size));
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

		private List<int> Heapify(int[] a)
		{
			if (a == null || a.Length == 0)
			{
				return new List<int>();
			}

			List<int> heap = new List<int>(a);
			heap[0] = a[0];
			for (int i = 1; i < a.Length; i++)
			{
				heap[i] = a[i];
				SiftUp(heap, i);
			}

			return heap;
		}

		private void SiftDown(List<int> heap, int index)
		{
			int min = heap[index];
			int minIndex = index;

			int leftChildIndex = GetLeftChildIndex(index);
			if (leftChildIndex < this.size && heap[leftChildIndex] < min)
			{
				min = heap[leftChildIndex];
				minIndex = leftChildIndex;
			}

			int rightChildIndex = GetRightChildIndex(index);
			if (rightChildIndex < this.size && heap[rightChildIndex] < min)
			{
				min = heap[rightChildIndex];
				minIndex = rightChildIndex;
			}

			if (minIndex != index)
			{
				ArrayUtility.Swap(heap, index, minIndex);
				SiftDown(heap, minIndex);
			}
		}

		private void SiftUp(List<int> heap, int index)
		{
			int parentIndex = GetParentIndex(index);
			if (heap[index] < heap[parentIndex])
			{
				ArrayUtility.Swap(heap, index, parentIndex);
				SiftUp(heap, parentIndex);
			}
		}
	}
}

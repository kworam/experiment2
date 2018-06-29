using System.Collections.Generic;
using System.Linq;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest
{
	[TestClass]
	public class PriorityQueueUnitTest
	{
		[TestCategory("PriorityQueue"), TestMethod()]
		public void EmptyPriorityQueueCountZero()
		{
			PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
			Assert.AreEqual(pq.Count(), 0);
		}

		[TestCategory("PriorityQueue"), TestMethod()]
		[ExpectedException(typeof(PriorityQueueEmptyException))]
		public void EmptyPriorityQueueDequeueException()
		{
			PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
			pq.Dequeue();
		}

		[TestCategory("PriorityQueue"), TestMethod()]
		[ExpectedException(typeof(PriorityQueueEmptyException))]
		public void EmptyPriorityQueuePeekException()
		{
			PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
			pq.Peek();
		}

		[TestCategory("PriorityQueue"), TestMethod()]
		[ExpectedException(typeof(PriorityQueueNodeNotFoundException))]
		public void EmptyPriorityQueueChangePriorityException()
		{
			PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
			pq.ChangePriority(0.ToString(), 0);
		}

		[TestCategory("PriorityQueue"), TestMethod()]
		public void PriorityQueueFillAndEmpty()
		{

			List<PriorityQueueNode> nodesToEnqueue = new List<PriorityQueueNode>()
			{
				new Node(new NodeData(uniqueKey: 0.ToString(), name: "A"), priority: 3),
				new Node(new NodeData(uniqueKey: 1.ToString(), name: "B"), priority: 2),
				new Node(new NodeData(uniqueKey: 2.ToString(), name: "C"), priority: 1),
			};

			PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
			foreach (Node node in nodesToEnqueue)
			{
				pq.Enqueue(node);
			}
			int expectedCount = pq.Count();
			Assert.AreEqual(expectedCount, nodesToEnqueue.Count);

			int expectedPriority = 1;
			while (pq.Count() > 0)
			{
				PriorityQueueNode peek = pq.Peek();
				PriorityQueueNode n = pq.Dequeue();

				Assert.AreEqual(peek.Data.UniqueKey, n.Data.UniqueKey);

				Assert.AreEqual(n.Priority, expectedPriority);
				expectedPriority++;

				expectedCount--;
				Assert.AreEqual(pq.Count(), expectedCount);
			}
		}


		[TestCategory("PriorityQueue"), TestMethod()]
		public void PriorityQueueChangePriority()
		{

			List<PriorityQueueNode> nodesToEnqueue = new List<PriorityQueueNode>()
			{
				new Node(new NodeData(uniqueKey: 0.ToString(), name: "A"), priority: 3),
				new Node(new NodeData(uniqueKey: 1.ToString(), name: "B"), priority: 2),
				new Node(new NodeData(uniqueKey: 2.ToString(), name: "C"), priority: 1),
			};

			PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
			foreach (Node node in nodesToEnqueue)
			{
				pq.Enqueue(node);
			}
			int expectedCount = pq.Count();
			Assert.AreEqual(expectedCount, nodesToEnqueue.Count);

			pq.ChangePriority(nodeUniqueKey: 1.ToString(), priority: 0);
			Assert.AreEqual(expectedCount, nodesToEnqueue.Count);

			int[] expectedPriorities =                    { 0, 1, 3 };
			string[] expectedUniqueKeys = new List<int>() { 1, 2, 0 }.
				Select(uk => uk.ToString()).ToArray();
			int i = 0;
			while (pq.Count() > 0)
			{
				PriorityQueueNode n = pq.Dequeue();
				Assert.AreEqual(n.Priority, expectedPriorities[i]);
				Assert.AreEqual(n.Data.UniqueKey, expectedUniqueKeys[i]);

				expectedCount--;
				Assert.AreEqual(pq.Count(), expectedCount);

				i++;
			}
		}

        [TestCategory("PriorityQueue"), TestMethod()]
        public void PriorityQueueVerifyFIFOForSamePriority()
        {

            List<PriorityQueueNode> nodesToEnqueue = new List<PriorityQueueNode>()
			{
				new Node(new NodeData(uniqueKey: 0.ToString(), name: "11"), priority: 1),
				new Node(new NodeData(uniqueKey: 1.ToString(), name: "21"), priority: 2),
				new Node(new NodeData(uniqueKey: 2.ToString(), name: "12"), priority: 1),
				new Node(new NodeData(uniqueKey: 3.ToString(), name: "22"), priority: 2),
				new Node(new NodeData(uniqueKey: 4.ToString(), name: "13"), priority: 1),
			};

            PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
            foreach (Node node in nodesToEnqueue)
            {
                pq.Enqueue(node);
            }
            int expectedCount = pq.Count();
            Assert.AreEqual(expectedCount, nodesToEnqueue.Count);

            int[] expectedPriorities =			          { 1, 1, 1, 2, 2 };
            string[] expectedUniqueKeys = new List<int>() { 0, 2, 4, 1, 3 }.
				Select(uk => uk.ToString()).ToArray();
            int i = 0;
            while (pq.Count() > 0)
            {
                PriorityQueueNode n = pq.Dequeue();
                Assert.AreEqual(n.Priority, expectedPriorities[i]);
                Assert.AreEqual(n.Data.UniqueKey, expectedUniqueKeys[i]);

                expectedCount--;
                Assert.AreEqual(pq.Count(), expectedCount);

                i++;
            }
        }

        [TestCategory("PriorityQueue"), TestMethod()]
        public void PriorityQueueVerifyFIFOChangePriority()
        {

            List<PriorityQueueNode> nodesToEnqueue = new List<PriorityQueueNode>()
			{
				new Node(new NodeData(uniqueKey: 0.ToString(), name: "11"), priority: 1),
				new Node(new NodeData(uniqueKey: 1.ToString(), name: "21"), priority: 2),
				new Node(new NodeData(uniqueKey: 2.ToString(), name: "12"), priority: 1),
				new Node(new NodeData(uniqueKey: 3.ToString(), name: "22"), priority: 2),
				new Node(new NodeData(uniqueKey: 4.ToString(), name: "13"), priority: 1),
			};

            PriorityQueue pq = PriorityQueueFactory.CreatePriorityQueue();
            foreach (Node node in nodesToEnqueue)
            {
                pq.Enqueue(node);
            }
            int expectedCount = pq.Count();
            Assert.AreEqual(expectedCount, nodesToEnqueue.Count);

            pq.ChangePriority(nodeUniqueKey: 0.ToString(), priority: 3);
            Assert.AreEqual(expectedCount, nodesToEnqueue.Count);

            int[] expectedPriorities =                    { 1, 1, 2, 2, 3 };
			string[] expectedUniqueKeys = new List<int>() { 2, 4, 1, 3, 0 }.
				Select(uk => uk.ToString()).ToArray();
			int i = 0;
            while (pq.Count() > 0)
            {
                PriorityQueueNode n = pq.Dequeue();
                Assert.AreEqual(n.Priority, expectedPriorities[i]);
                Assert.AreEqual(n.Data.UniqueKey, expectedUniqueKeys[i]);

                expectedCount--;
                Assert.AreEqual(pq.Count(), expectedCount);

                i++;
            }
        }

        
        private class Node : PriorityQueueNode
		{
			public int Priority { get; set; }

            public HasUniqueKey Data { get; private set; }

			public Node(NodeData data, int priority)
			{
				this.Data = data;
				this.Priority = priority;
			}
		}

		private class NodeData : HasUniqueKey
		{
			public string UniqueKey { get; set; }

			public string Name { get; set; }

			public NodeData(string uniqueKey, string name)
			{
				this.UniqueKey = uniqueKey;
				this.Name = name;
			}
		}

		//[TestMethod]
		//[ExpectedException(typeof(HeapEmptyException))]
		//public void PriorityQueuePeekEmpty()
		//{
		//	PriorityQueue PriorityQueue = PriorityQueueFactory.CreatePriorityQueue(null);
		//	PriorityQueue.PeekMin();
		//}

		//[TestMethod]
		//[ExpectedException(typeof(HeapEmptyException))]
		//public void PriorityQueuePopEmpty()
		//{
		//	PriorityQueue PriorityQueue = PriorityQueueFactory.CreatePriorityQueue(null);
		//	PriorityQueue.PopMin();
		//}

		//[TestMethod]
		//public void PriorityQueueSingleton()
		//{
		//	int value = 99;
		//	PriorityQueue PriorityQueue = PriorityQueueFactory.CreatePriorityQueue(new int[] { value });
		//	Assert.AreEqual(PriorityQueue.GetCount(), 1);
		//	Assert.AreEqual(PriorityQueue.PeekMin(), value);
		//}

		//[TestMethod]
		//public void PriorityQueueLargeRandom()
		//{
		//	KevinPriorityQueue PriorityQueue = null;
		//	int[] inputArray = null;
		//	int[] expectedArray = null;
		//	List<int> poppedValues = new List<int>();
		//	try
		//	{
		//		int arrayLength = 1000;
		//		inputArray = new int[arrayLength];

		//		Random randomGenerator = new Random();
		//		for (int i = 0; i < arrayLength; i++)
		//		{
		//			inputArray[i] = randomGenerator.Next();
		//		}

		//		expectedArray = inputArray.Clone() as int[];
		//		// default sort puts min at the front of the sorted array
		//		Array.Sort(expectedArray);

		//		PriorityQueue = (KevinPriorityQueue)PriorityQueueFactory.CreatePriorityQueue(inputArray);
		//		int expectedCount = inputArray.Length;
		//		Assert.AreEqual(PriorityQueue.GetCount(), expectedCount);

		//		Console.WriteLine("binHeap:");
		//		Console.WriteLine(String.Join(" ", PriorityQueue.ToString()));


		//		for (int i = 0; i < inputArray.Length; i++)
		//		{
		//			int actual = PriorityQueue.PopMin();
		//			poppedValues.Add(actual);

		//			int expected = expectedArray[i];
		//			Assert.AreEqual(actual, expected);
		//			expectedCount--;
		//			Assert.AreEqual(PriorityQueue.GetCount(), expectedCount);
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		Console.WriteLine("inputArray:");
		//		Console.WriteLine(String.Join(" ", inputArray));
		//		Console.WriteLine("expectedArray:");
		//		Console.WriteLine(String.Join(" ", expectedArray));
		//		Console.WriteLine("popped values:");
		//		Console.WriteLine(string.Join(" ", poppedValues));
		//		Console.WriteLine("PriorityQueue:");
		//		Console.WriteLine(PriorityQueue.ToString());

		//		throw;
		//	}
		//}

		//[TestMethod]
		//public void PriorityQueueLargeRandomByAdding()
		//{
		//	KevinPriorityQueue PriorityQueue = null;
		//	int[] expectedArray = null;
		//	List<int> poppedValues = new List<int>();
		//	try
		//	{
		//		int arrayLength = 1000;

		//		expectedArray = new int[arrayLength];

		//		PriorityQueue = (KevinPriorityQueue)PriorityQueueFactory.CreatePriorityQueue(new int[0]);
		//		Random randomGenerator = new Random();
		//		for (int i = 0; i < arrayLength; i++)
		//		{
		//			int val = randomGenerator.Next();
		//			PriorityQueue.Add(val);
		//			expectedArray[i] = val;
		//		}

		//		// default sort puts min at the front of the sorted array
		//		Array.Sort(expectedArray);

		//		int expectedCount = expectedArray.Length;
		//		Assert.AreEqual(PriorityQueue.GetCount(), expectedCount);

		//		Console.WriteLine("binHeap:");
		//		Console.WriteLine(String.Join(" ", PriorityQueue.ToString()));


		//		for (int i = 0; i < expectedArray.Length; i++)
		//		{
		//			int actual = PriorityQueue.PopMin();
		//			poppedValues.Add(actual);

		//			int expected = expectedArray[i];
		//			Assert.AreEqual(actual, expected);
		//			expectedCount--;
		//			Assert.AreEqual(PriorityQueue.GetCount(), expectedCount);
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		Console.WriteLine("expectedArray:");
		//		Console.WriteLine(String.Join(" ", expectedArray));
		//		Console.WriteLine("popped values:");
		//		Console.WriteLine(string.Join(" ", poppedValues));
		//		Console.WriteLine("PriorityQueue:");
		//		Console.WriteLine(PriorityQueue.ToString());

		//		throw;
		//	}
		//}
	}
}

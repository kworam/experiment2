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
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest
{
	[TestClass]
	public class MinHeapUnitTest
	{
		[TestCategory("MinHeap"), TestMethod]
		public void MinHeapCreateFromNull()
		{
			MinHeap minHeap = HeapFactory.CreateMinHeap(null);
			Assert.AreEqual(minHeap.GetCount(), 0);
		}

		[TestCategory("MinHeap"), TestMethod]
		public void MinHeapCreateFromEmpty()
		{
			MinHeap minHeap = HeapFactory.CreateMinHeap(new int[0]);
			Assert.AreEqual(minHeap.GetCount(), 0);
		}

		[TestCategory("MinHeap"), TestMethod]
		[ExpectedException(typeof(HeapEmptyException))]
		public void MinHeapPeekEmpty()
		{
			MinHeap minHeap = HeapFactory.CreateMinHeap(null);
			minHeap.PeekMin();
		}

		[TestCategory("MinHeap"), TestMethod]
		[ExpectedException(typeof(HeapEmptyException))]
		public void MinHeapPopEmpty()
		{
			MinHeap minHeap = HeapFactory.CreateMinHeap(null);
			minHeap.PopMin();
		}

		[TestCategory("MinHeap"), TestMethod]
		public void MinHeapSingleton()
		{
			int value = 99;
			MinHeap minHeap = HeapFactory.CreateMinHeap(new int[] { value });
			Assert.AreEqual(minHeap.GetCount(), 1);
			Assert.AreEqual(minHeap.PeekMin(), value);
		}

		[TestCategory("MinHeap"), TestMethod]
		public void MinHeapLargeRandom()
		{
		    KevinMinHeap minHeap = null;
		    int[] inputArray = null;
            int[] expectedArray = null;
		    List<int> poppedValues = new List<int>();
            try
		    {
				inputArray = ArrayUtility.GenerateRandomIntArray(1000);

                expectedArray = inputArray.Clone() as int[];
                // default sort puts min at the front of the sorted array
                Array.Sort(expectedArray);

                minHeap = (KevinMinHeap)HeapFactory.CreateMinHeap(inputArray);
                int expectedCount = inputArray.Length;
                Assert.AreEqual(minHeap.GetCount(), expectedCount);

                Console.WriteLine("binHeap:");
                Console.WriteLine(string.Join(" ", minHeap.ToString()));


                for (int i = 0; i < inputArray.Length; i++)
                {
                    int actual = minHeap.PopMin();
                    poppedValues.Add(actual);

                    int expected = expectedArray[i];
                    Assert.AreEqual(actual, expected);
                    expectedCount--;
                    Assert.AreEqual(minHeap.GetCount(), expectedCount);
                }
            }
		    catch (Exception)
		    {
                Console.WriteLine("inputArray:");
                Console.WriteLine(string.Join(" ", inputArray));
                Console.WriteLine("expectedArray:");
                Console.WriteLine(string.Join(" ", expectedArray));
                Console.WriteLine("popped values:");
		        Console.WriteLine(string.Join(" ", poppedValues));
                Console.WriteLine("minHeap:");
                Console.WriteLine(minHeap.ToString());

                throw;
		    }
        }

		[TestCategory("MinHeap"), TestMethod]
		public void MinHeapLargeRandomByAdding()
		{
			KevinMinHeap minHeap = null;
			int[] expectedArray = null;
			List<int> poppedValues = new List<int>();
			try
			{
				int arrayLength = 1000;

				expectedArray = new int[arrayLength];

				minHeap = (KevinMinHeap)HeapFactory.CreateMinHeap(new int[0]);
				Random randomGenerator = new Random();
				for (int i = 0; i < arrayLength; i++)
				{
					int val = randomGenerator.Next();
					minHeap.Add(val);
					expectedArray[i] = val;
				}

				// default sort puts min at the front of the sorted array
				Array.Sort(expectedArray);

				int expectedCount = expectedArray.Length;
				Assert.AreEqual(minHeap.GetCount(), expectedCount);

				Console.WriteLine("binHeap:");
				Console.WriteLine(string.Join(" ", minHeap.ToString()));


				for (int i = 0; i < expectedArray.Length; i++)
				{
					int actual = minHeap.PopMin();
					poppedValues.Add(actual);

					int expected = expectedArray[i];
					Assert.AreEqual(actual, expected);
					expectedCount--;
					Assert.AreEqual(minHeap.GetCount(), expectedCount);
				}
			}
			catch (Exception)
			{
				Console.WriteLine("expectedArray:");
				Console.WriteLine(string.Join(" ", expectedArray));
				Console.WriteLine("popped values:");
				Console.WriteLine(string.Join(" ", poppedValues));
				Console.WriteLine("minHeap:");
				Console.WriteLine(minHeap.ToString());

				throw;
			}
		}
	}
}

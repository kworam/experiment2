using System;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest
{
    [TestClass]
    public class QuickSortUnitTest
    {
        [TestCategory("QuickSort"), TestMethod]
        public void QuickSortNullArray()
        {
            int[] a = null;
            QuickSort.Sort(a, QuickSortCCI.Partition);
            Assert.IsNull(a);
        }

        [TestCategory("QuickSort"), TestMethod]
        public void QuickSortEmptyArray()
        {
            int[] a = new int[0];
            int[] expectedOutput = a.Clone() as int[];
            QuickSort.Sort(a, QuickSortCCI.Partition);
            Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
        }

        [TestCategory("QuickSort"), TestMethod]
        public void QuickSortSingletonArray()
        {
            int[] a = new int[] { 88 };
            int[] expectedOutput = a.Clone() as int[];
            QuickSort.Sort(a, QuickSortCCI.Partition);
            Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
        }


		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortAllSameOddLength()
		{
			int[] a = new int[] { 1, 1, 1 };
			int[] expectedOutput = a.Clone() as int[];
			QuickSort.Sort(a, QuickSortCCI.Partition);
			Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
		}

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortAllSameEvenLength()
		{
			int[] a = new int[] { 1, 1, 1, 1 };
			int[] expectedOutput = a.Clone() as int[];
			QuickSort.Sort(a, QuickSortCCI.Partition);
			Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
		}

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortA()
		{
			int[] a = new int[] { 1, 3, 1 };
			int[] expectedOutput = a.Clone() as int[];
			Array.Sort(expectedOutput);
			QuickSort.Sort(a, QuickSortCCI.Partition);
			Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
		}

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortSortedOddLength()
		{
			int[] a = new int[] { 1, 2, 3 };
			int[] expectedOutput = a.Clone() as int[];
			Array.Sort(expectedOutput);
			QuickSort.Sort(a, QuickSortCCI.Partition);
			Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
		}

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortSortedEvenLength()
		{
			int[] a = new int[] { 1, 2, 3, 4 };
			int[] expectedOutput = a.Clone() as int[];
			Array.Sort(expectedOutput);
			QuickSort.Sort(a, QuickSortCCI.Partition);
			Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
		}

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortReverseSortedOddLength()
		{
			int[] a = new int[] { 3, 2, 1 };
			int[] expectedOutput = a.Clone() as int[];
			Array.Sort(expectedOutput);
			QuickSort.Sort(a, QuickSortCCI.Partition);
			Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
		}

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortReverseSortedEvenLength()
		{
			int[] a = new int[] { 4, 3, 2, 1 };
			int[] expectedOutput = a.Clone() as int[];
			Array.Sort(expectedOutput);
			QuickSort.Sort(a, QuickSortCCI.Partition);
			Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(a, expectedOutput));
		}

		[TestCategory("QuickSort"), TestMethod]
        public void QuickSortLargeRandomArray()
        {
	        int[] inputArray = ArrayUtility.GenerateRandomIntArray(arrayLength: 1000, maxValue: 20);

			int[] expectedArray = inputArray.Clone() as int[];
            // default sort puts min at the front of the sorted array
            Array.Sort(expectedArray);

            QuickSort.Sort(inputArray, QuickSortCCI.Partition);
            Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(inputArray, expectedArray));
        }

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortComparePartitionArrayFuncs()
		{
			int[] inputArrayClassic = ArrayUtility.GenerateRandomIntArray(arrayLength: 1000, maxValue: 20);

			int[] inputArrayKevin = inputArrayClassic.Clone() as int[];

			QuickSort.Sort(inputArrayClassic, QuickSortClassicPartition.PartitionArray);
			Console.WriteLine("Classic partition stats");
			Console.WriteLine(QuickSort.stats);

			QuickSort.Sort(inputArrayKevin, QuickSortKevinPartition.PartitionArray);
			Console.WriteLine("Kevin partition stats");
			Console.WriteLine(QuickSort.stats);

			QuickSort.Sort(inputArrayKevin, QuickSortCCI.Partition);
			//Console.WriteLine("QuickSortCCI partition stats");
			//Console.WriteLine(QuickSort.stats);
		}
	}
}

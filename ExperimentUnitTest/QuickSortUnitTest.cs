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
            QuickSort.Sort(a);
            Assert.IsNull(a);
        }

        [TestCategory("QuickSort"), TestMethod]
        public void QuickSortEmptyArray()
        {
            int[] a = new int[0];
            int[] expectedOutput = a.Clone() as int[];
            QuickSort.Sort(a);
            Assert.IsTrue(ArrayUtility.AreArraysEqual(a, expectedOutput));
        }

        [TestCategory("QuickSort"), TestMethod]
        public void QuickSortSingletonArray()
        {
            int[] a = new int[] { 88 };
            int[] expectedOutput = a.Clone() as int[];
            QuickSort.Sort(a);
            Assert.IsTrue(ArrayUtility.AreArraysEqual(a, expectedOutput));
        }


        [TestCategory("QuickSort"), TestMethod]
        public void QuickSortLargeRandomArray()
        {
			int arrayLength = 1000;
			int[] inputArray = new int[arrayLength];

			Random randomGenerator = new Random();
			for (int i = 0; i < arrayLength; i++)
			{
				inputArray[i] = randomGenerator.Next(maxValue: 20);
			}

			int[] expectedArray = inputArray.Clone() as int[];
            // default sort puts min at the front of the sorted array
            Array.Sort(expectedArray);

            QuickSort.Sort(inputArray);
            Assert.IsTrue(ArrayUtility.AreArraysEqual(inputArray, expectedArray));
        }

		[TestCategory("QuickSort"), TestMethod]
		public void QuickSortComparePartitionArrayFuncs()
		{
			int arrayLength = 10000;
			int[] inputArrayClassic = new int[arrayLength];

			Random randomGenerator = new Random();
			for (int i = 0; i < arrayLength; i++)
			{
				inputArrayClassic[i] = randomGenerator.Next(maxValue: 20);
			}

			int[] inputArrayKevin = inputArrayClassic.Clone() as int[];

			QuickSort.Sort(inputArrayClassic, QuickSortClassicPartition.PartitionArray);
			Console.WriteLine("Classic partition stats");
			Console.WriteLine(QuickSort.stats);

			QuickSort.Sort(inputArrayKevin, QuickSortKevinPartition.PartitionArray);
			Console.WriteLine("Kevin partition stats");
			Console.WriteLine(QuickSort.stats);
		}
	}
}

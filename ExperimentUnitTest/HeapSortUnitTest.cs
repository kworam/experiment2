using System;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest
{
    [TestClass]
    public class HeapSortUnitTest
    {
        [TestCategory("HeapSort"), TestMethod]
        public void HeapSortNullArray()
        {
            int[] a = null;
            HeapSort.Sort(a);
            Assert.IsNull(a);
        }

        [TestCategory("HeapSort"), TestMethod]
        public void HeapSortEmptyArray()
        {
            int[] a = new int[0];
            int[] expectedOutput = a.Clone() as int[];
            HeapSort.Sort(a);
            Assert.IsTrue(ArrayUtility.AreArraysEqual(a, expectedOutput));
        }

        [TestCategory("HeapSort"), TestMethod]
        public void HeapSortSingletonArray()
        {
            int[] a = new int[] { 88 };
            int[] expectedOutput = a.Clone() as int[];
            HeapSort.Sort(a);
            Assert.IsTrue(ArrayUtility.AreArraysEqual(a, expectedOutput));
        }


        [TestCategory("HeapSort"), TestMethod]
        public void HeapSortLargeRandomArray()
        {
            int arrayLength = 1000;
            int[] inputArray = new int[arrayLength];

            Random randomGenerator = new Random();
            for (int i = 0; i < arrayLength; i++)
            {
                inputArray[i] = randomGenerator.Next();
            }

            int[] expectedArray = inputArray.Clone() as int[];
            // default sort puts min at the front of the sorted array
            Array.Sort(expectedArray);

            HeapSort.Sort(inputArray);
            Assert.IsTrue(ArrayUtility.AreArraysEqual(inputArray, expectedArray));
        }
    }
}

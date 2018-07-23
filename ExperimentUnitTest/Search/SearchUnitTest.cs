using System.Linq;
using Experiment.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Search
{
	[TestClass]
	public class SearchUnitTest
	{
		[TestCategory("Search"), TestMethod]
		public void NullArray()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(null, 1), -1);
		}

		[TestCategory("Search"), TestMethod]
		public void EmptyArray()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(
				GenerateRotatedSortedArray(length: 0, rotation: 0), val: 1), -1);
		}

		[TestCategory("Search"), TestMethod]
		public void SingletonArrayNotFound()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 1, rotation: 1), val: 99), -1);
		}


		[TestCategory("Search"), TestMethod]
		public void SingletonArrayFound()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 1, rotation: 2), val: 0), 0);
		}

		[TestCategory("Search"), TestMethod]
		public void TwoItemArrayNotFound()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 2, rotation: 1), val: 5), -1);
		}

		[TestCategory("Search"), TestMethod]
		public void TwoItemArrayFound()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 2, rotation: 1), val: 0), 1);
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 2, rotation: 1), val: 1), 0);
		}

		[TestCategory("Search"), TestMethod]
		public void LargeArrayNotFound()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 100, rotation: 13), val: 107), -1);
		}

		[TestCategory("Search"), TestMethod]
		public void LargeArrayFound()
		{
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 100, rotation: 13), val: 99), (99 + 13) % 100);
			Assert.AreEqual(SearchInSortedRotatedArray.Search(GenerateRotatedSortedArray(length: 100, rotation: 13), val: 55), (55 + 13) % 100);
		}

		private int[] GenerateRotatedSortedArray(int length, int rotation)
		{
			int[] sorted = Enumerable.Range(0, length).ToArray();
			RotateArray(sorted, rotation);
			return sorted;
		}

		private static void ReverseArray(int[] a, int start, int end)
		{
			if (a == null)
			{
				return;
			}

			while (start < end)
			{
				int tmp = a[start];
				a[start] = a[end];
				a[end] = tmp;
				start++;
				end--;
			}
		}

		private static void RotateArray(int[] a, int rotation)
		{
			if (a == null || a.Length < 2 || (rotation % a.Length) == 0)
			{
				return;
			}

			ReverseArray(a, 0, a.Length - 1);
			ReverseArray(a, 0, rotation - 1);
			ReverseArray(a, rotation, a.Length - 1);
		}
	}
}

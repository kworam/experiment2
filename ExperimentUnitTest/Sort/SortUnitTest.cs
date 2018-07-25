using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Sort
{
	[TestClass]
	public class SortUnitTest
	{
		[TestCategory("Sort"), TestMethod]
		public void NullArray()
		{
			int[] a = null;
			Experiment.Sort.Sort.SortToPeaksAndValleys(a);
			Assert.IsNull(a);
		}

		[TestCategory("Sort"), TestMethod]
		public void SingletonArray()
		{
			int[] a = ArrayUtility.GenerateRandomIntArray(1);
			Experiment.Sort.Sort.SortToPeaksAndValleys(a);
			Assert.IsTrue(IsAllPeaksAndValleys(a));
		}

		[TestCategory("Sort"), TestMethod]
		public void LengthTwo()
		{
			int[] a = ArrayUtility.GenerateRandomIntArray(2);
			Experiment.Sort.Sort.SortToPeaksAndValleys(a);			
		}

		[TestCategory("Sort"), TestMethod]
		public void LengthThree()
		{
			int[] a = ArrayUtility.GenerateRandomIntArray(3);
			Experiment.Sort.Sort.SortToPeaksAndValleys(a);
		}

		[TestCategory("Sort"), TestMethod]
		public void LargeEven()
		{
			int[] a = ArrayUtility.GenerateRandomIntArray(100);
			Experiment.Sort.Sort.SortToPeaksAndValleys(a);
		}

		[TestCategory("Sort"), TestMethod]
		public void LargeOdd()
		{
			int[] a = ArrayUtility.GenerateRandomIntArray(101);
			Experiment.Sort.Sort.SortToPeaksAndValleys(a);
		}

		private static bool IsAllPeaksAndValleys(int[] a)
		{
			if (a == null || a.Length < 3) return true;

			bool nextShouldBeSmallerThanPrev = a[0] < a[1];
			int nextIndex = 2;
			while (nextIndex < a.Length)
			{
				if (nextShouldBeSmallerThanPrev)
				{
					if (a[nextIndex] > a[nextIndex - 1]) return false;
				}
				else
				{
					if (a[nextIndex] < a[nextIndex - 1]) return false;
				}
				nextIndex++;
				nextShouldBeSmallerThanPrev = !nextShouldBeSmallerThanPrev;
			}

			return true;
		}
	}
}

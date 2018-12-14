using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiment
{
    public static class ArrayUtility
    {
        public static bool AreIntegerEnumerablesEqual(IEnumerable<int> a, IEnumerable<int> b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null || b == null)
            {
                return false;
            }

            // both arrays are non-empty
            if (a.Count() != b.Count())
            {
                return false;
            }

            IEnumerator<int> aEnum = a.GetEnumerator();
            IEnumerator<int> bEnum = b.GetEnumerator();
            for (int i = 0; i < a.Count(); i++)
            {
                aEnum.MoveNext();
                bEnum.MoveNext();

                if (aEnum.Current != bEnum.Current)
                {
                    return false;
                }
            }

            return true;
        }

		public static void Swap(int[] array, int index1, int index2)
		{
			int tmp = array[index1];
			array[index1] = array[index2];
			array[index2] = tmp;
		}

		public static void Swap<T>(List<T> array, int index1, int index2)
		{
			T tmp = array[index1];
			array[index1] = array[index2];
			array[index2] = tmp;
		}

	    public static int[] GenerateRandomIntArray(int arrayLength, int? maxValue = null)
	    {
		    int[] result = new int[arrayLength];

		    Random randomGenerator = new Random();
		    for (int i = 0; i < arrayLength; i++)
		    {
			    result[i] = maxValue.HasValue
				    ? randomGenerator.Next(maxValue.Value)
				    : randomGenerator.Next();
		    }

			return result;
	    }

        public static int[] GetDistinct(int[] array)
        {
            HashSet<int> set = new HashSet<int>(array);
            return set.ToArray();
        }

        public static int[] GetSortedDistinct(int[] array)
        {
            int[] distinct = GetDistinct(array);
            Array.Sort(distinct);
            return distinct;
        }
    }
}

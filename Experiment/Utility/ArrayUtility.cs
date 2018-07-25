using System;
using System.Collections.Generic;

namespace Experiment
{
    public static class ArrayUtility
    {
        public static bool AreArraysEqual(int[] a, int[] b)
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
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
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
    }
}

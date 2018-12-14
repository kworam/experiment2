using System.Collections.Generic;
using System.Linq;

namespace Experiment.AlgorithmDesignManual
{
    public class LongestIncreasingSequence
    {
        //public static List<int> Find(int[] a)
        //{
        //    if (a == null || a.Length == 0)
        //    {
        //        return new List<int>();
        //    }

        //    List<int>[] chains = new List<int>[a.Length];
        //    for (int i = 0; i < a.Length; i++)
        //    {
        //        chains[i] = new List<int>() { a[i] };
        //    }

        //    List<int> max = chains[0];
        //    for (int i = 1; i < a.Length; i++)
        //    {
        //        for (int j = 0; j < i; j++)
        //        {
        //            List<int> chain = chains[j];
        //            if (a[i] > chain.Last())
        //            {
        //                chain.Add(a[i]);
        //                if (chain.Count > max.Count)
        //                {
        //                    max = chain;
        //                }
        //            }
        //        }
        //    }

        //    return max;
        //}

        public static List<int> Find(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return new List<int>();
            }

            int[] l = new int[arr.Length];
            int[] prev = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                l[i] = 1;
                prev[i] = -1;
            }

            int maxIndex = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (arr[i] > arr[j] && (l[j] + 1) > l[i])
                    {
                        l[i] = l[j] + 1;
                        prev[i] = j;

                        if (l[i] > l[maxIndex])
                        {
                            maxIndex = i;
                        }
                    }
                }
            }

            return BuildList(arr, prev, maxIndex);
        }


        private static List<int> BuildList(int[] arr, int[] prev, int maxIndex)
        {
            List<int> result = new List<int>();
            result.Add(arr[maxIndex]);
            while (prev[maxIndex] >= 0)
            {
                maxIndex = prev[maxIndex];
                result.Add(arr[maxIndex]);
            }
            result.Reverse();
            return result;
        }
    }
}

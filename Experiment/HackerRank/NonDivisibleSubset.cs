using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class NonDivisibleSubset
    {
        // Complete the nonDivisibleSubset function below.
        public static int nonDivisibleSubset(int k, int[] S)
        {
            Dictionary<int, int> modCounts = GetModCounts(k, S);
            return GetMaxSize(modCounts, k);
        }

        private static int GetMaxSize(Dictionary<int, int> modCounts, int k)
        {
            if (GetModCount(modCounts, 0) > 1)
            {
                modCounts[0] = 1;
            }

            if (k % 2 == 0 && GetModCount(modCounts, k / 2) > 1)
            {
                modCounts[k / 2] = 1;
            }

            return GetMaxSize(modCounts, k, 0, new HashSet<int>());
        }

        private static int GetMaxSize(Dictionary<int, int> modCounts, int k, int size, HashSet<int> inset)
        {
            if (modCounts.Count == 0)
            {
                return size;
            }

            int maxSize = size;
            foreach (int key in modCounts.Keys)
            {
                Dictionary<int, int> cloneCounts = new Dictionary<int, int>(modCounts);
                HashSet<int> cloneInSet = new HashSet<int>(inset);
                int newSize = size;
                if (IsCompatible(key, cloneInSet, k))
                {
                    newSize += cloneCounts[key];
                    cloneCounts.Remove(k - key);
                    cloneInSet.Add(key);
                }
                cloneCounts.Remove(key);
                maxSize = Math.Max(maxSize, GetMaxSize(cloneCounts, k, newSize, cloneInSet));
            }

            return maxSize;
        }

        private static bool IsCompatible(int key, HashSet<int> inset, int k)
        {
            foreach (int inValue in inset)
            {
                if ((key + inValue) % k == 0) return false;
            }
            return true;
        }


        private static int GetModCount(Dictionary<int, int> modCounts, int i)
        {
            if (!modCounts.ContainsKey(i)) return 0;
            return modCounts[i];
        }

        private static Dictionary<int, int> GetModCounts(int k, int[] s)
        {
            Dictionary<int, int> modCounts = new Dictionary<int, int>();
            for (int i = 0; i < s.Length; i++)
            {
                int key = k == 1 ? s[i] : s[i] % k;
                if (!modCounts.ContainsKey(key))
                {
                    modCounts[key] = 0;
                }
                modCounts[key]++;
            }
            return modCounts;
        }

        //return Math.Max(GetBottomHalfSize(k, modCounts), GetTopHalfSize(k, modCounts));
        //}
        //else
        //{
        //}
        //for (int i = 0; i < k; i++)
        //{
        //    int iCount = modCounts.ContainsKey(i) ? modCounts[i] : 0;

        //    if (i == 0 && iCount > 1)
        //    {
        //        continue;
        //    }

        //    if (k % 2 == 0 && i == k / 2 && iCount > 1)
        //    {
        //        continue;
        //    }

        //    for (int j = i; j < k; j++)
        //    {
        //        if ((i + j) % k != 0)
        //        {
        //            int jCount = modCounts.ContainsKey(j) ? modCounts[j] : 0;

        //            if (i == j)
        //            {
        //                maxSize = Math.Max(maxSize, iCount);
        //            }
        //            else
        //            {
        //                maxSize = Math.Max(maxSize, iCount + jCount);
        //            }
        //        }
        //    }
        //}
        //return maxSize;
    //}

    //private static int GetBottomHalfSize(int k, Dictionary<int, int> modCounts)
    //{
    //    int size = 0;
    //    if (GetModCount(modCounts, 0) == 1)
    //    {
    //        size++;
    //    }
    //    if (k % 2 == 0)
    //    {
    //        if (GetModCount(modCounts, k/2) == 1)
    //        {
    //            size++;
    //        }

    //        for (int i = 1; i < k/2; i++)
    //        {
    //            size += GetModCount(modCounts, i);
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 1; i <= k/2; i++)
    //        {
    //            size += GetModCount(modCounts, i);
    //        }
    //    }
    //    return size;
    //}

    //private static int GetTopHalfSize(int k, Dictionary<int, int> modCounts)
    //{
    //    int size = 0;
    //    if (GetModCount(modCounts, 0) == 1)
    //    {
    //        size++;
    //    }
    //    if (k % 2 == 0)
    //    {
    //        if (GetModCount(modCounts, k/2) == 1)
    //        {
    //            size++;
    //        }
    //        for (int i = k/2+1; i<k; i++)
    //        {
    //            size += GetModCount(modCounts, i);
    //        }
    //    }
    //    else  // k%2 == 1  (ex: 7, k/2 = 3)
    //    {
    //        for (int i = k/2+1; i < k; i++)
    //        {
    //            size += GetModCount(modCounts, i);
    //        }
    //    }

    //    return size;
    //}


}
}

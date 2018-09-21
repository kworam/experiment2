using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class TripletSum
    {
        // Complete the triplets function below.
        public static long triplets(int[] a, int[] b, int[] c)
        {
            int[] sda = SortAndReturnDistinct(a);
            int[] sdb = SortAndReturnDistinct(b);
            int[] sdc = SortAndReturnDistinct(c);

            int ai = 0;
            int bi = 0;
            int ci = 0;

            long dt = 0;
            while (bi < sdb.Length)
            {
                int bval = sdb[bi];

                int nai = findValOrNextSmallest(bval, sda, ai, sda.Length - 1);

                int nci = findValOrNextSmallest(bval, sdc, ci, sdc.Length - 1);

                if (nai >= 0 && nci >= 0)
                {
                    dt += (long)(nai + 1) * (long)(nci + 1);
                    ai = nai;
                    ci = nci;
                }

                bi++;
            }

            return dt;
        }

        private static int[] SortAndReturnDistinct(int[] a)
        {
            Array.Sort(a);
            int[] da = GetDistinct(a);
            return da;
        }

        private static int[] GetDistinct(int[] sorted)
        {
            List<int> result = new List<int>();
            int prev = -1;
            for (int i=0; i<sorted.Length; i++)
            {
                int curr = sorted[i];
                if (curr != prev)
                {
                    result.Add(curr);
                }
                prev = curr;
            }
            return result.ToArray();
        }

        private static int findValOrNextSmallest(
            int val, int[] sorted, int start, int end)
        {
            int idx = findValOrClosest(val, sorted, start, end);
            if (idx < 0 || sorted[idx] == val) return idx;
            while (idx >= 0 && sorted[idx] > val) idx--;
            return idx < 0 ? -1 : idx;
        }

        private static int findValOrClosest(int val, int[] sorted, int start, int end)
        {
            if (start > end)
            {
                return -1;
            }

            int mid = (start + end) / 2;
            if (sorted[mid] == val)
            {
                return mid;
            }
            else
            {
                if (sorted[mid] < val)
                {
                    if (mid == end)
                    {
                        return mid;
                    }
                    return findValOrClosest(val, sorted, mid + 1, end);
                }
                else
                {
                    if (mid == start)
                    {
                        return mid;
                    }
                    return findValOrClosest(val, sorted, start, mid - 1);
                }
            }
        }

        //public static long triplets(int[] a, int[] b, int[] c)
        //{
        //    int[] da = SortAndReturnDistinct(a);
        //    int[] db = SortAndReturnDistinct(b);
        //    int[] dc = SortAndReturnDistinct(c);

        //    Dictionary<int, int> bCache = new Dictionary<int, int>();
        //    Dictionary<int, int> cCache = new Dictionary<int, int>();

        //    long numTriplets = 0;
        //    int bLastIndex = 0;
        //    int cLastIndex = 0;
        //    for (int i = 0; i < da.Length; i++)
        //    {
        //        int av = da[i];

        //        int bi = findValOrNextLargest(av, db, bLastIndex, db.Length - 1, bCache);
        //        if (bi < 0)
        //        {
        //            break;
        //        }
        //        bLastIndex = bi;

        //        cLastIndex = 0;
        //        for (int j = bi; j < db.Length; j++)
        //        {
        //            int bv = db[j];

        //            int ci = findValOrNextSmallest(bv, dc, cLastIndex, dc.Length - 1, cCache);
        //            if (ci < 0)
        //            {
        //                continue;
        //            }
        //            cLastIndex = ci;

        //            int numbc = ci + 1;
        //            numTriplets += numbc;
        //        }
        //    }

        //    return numTriplets;
        //}

        //private static int findValOrNextLargest(
        //    int val, int[] sorted, int start, int end, Dictionary<int, int> cache)
        //{
        //    if (cache.ContainsKey(val))
        //    {
        //        return cache[val];
        //    }

        //    int idx = findValOrClosest(val, sorted, start, end);
        //    if (idx < 0 || sorted[idx] == val) return idx;
        //    while (idx < sorted.Length && sorted[idx] < val) idx++;
        //    cache[val] = idx == sorted.Length ? -1 : idx;
        //    return cache[val];
        //}

        //private static int findValOrNextSmallest(
        //    int val, int[] sorted, int start, int end, Dictionary<int, int> cache)
        //{
        //    if (cache.ContainsKey(val))
        //    {
        //        return cache[val];
        //    }

        //    int idx = findValOrClosest(val, sorted, start, end);
        //    if (idx < 0 || sorted[idx] == val) return idx;
        //    while (idx >= 0 && sorted[idx] > val) idx--;
        //    cache[val] = idx < 0 ? -1 : idx;
        //    return cache[val];
        //}

    }
}

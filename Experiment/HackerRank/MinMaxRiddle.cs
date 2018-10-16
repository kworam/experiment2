using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class MinMaxRiddle
    {
        static long[] riddle(long[] arr)
        {
            int[] minWindowLengths = GetMinWindowLengths(arr);

            Dictionary<long, int> maxLenPerValue = new Dictionary<long, int>();
            for (int i = 0; i < arr.Length; i++)
            {
                long thisVal = arr[i];
                if (!maxLenPerValue.ContainsKey(thisVal))
                {
                    maxLenPerValue[thisVal] = minWindowLengths[i];
                }
                else
                {
                    maxLenPerValue[thisVal] =
                        Math.Max(maxLenPerValue[thisVal], minWindowLengths[i]);
                }
            }

            long[] maxLens = new long[arr.Length];
            foreach (int val in maxLenPerValue.Keys)
            {
                int windowLen = maxLenPerValue[val];
                maxLens[windowLen - 1] = Math.Max(val, maxLens[windowLen - 1]);
            }

            int idx = maxLens.Length - 1;
            long last = maxLens[idx];
            for (idx--; idx >= 0; idx--)
            {
                //if (maxLens[idx] == 0)
                if (maxLens[idx] < last)
                {
                    maxLens[idx] = last;
                }
                else
                {
                    last = maxLens[idx];
                }
            }

            return maxLens;
        }

        public static int[] GetMinWindowLengths(long[] arr)
        {
            int[] minWindowLengths = new int[arr.Length];
            int[] leftLengths = CalculateLeftMinLength(arr);
            int[] rightLengths = CalculateRightMinLength(arr);
            for (int i=0; i<arr.Length; i++)
            {
                minWindowLengths[i] = leftLengths[i] + rightLengths[i] - 1;
            }
            return minWindowLengths;
        }

        public static int[]  CalculateLeftMinLength(long[] arr)
        {
            Stack<int> minIndexes = new Stack<int>();
            int[] minLengths = new int[arr.Length];

            for (int idx=0; idx<arr.Length; idx++)
            {
                while (minIndexes.Count > 0 && arr[idx] <= arr[minIndexes.Peek()])
                {
                    minIndexes.Pop();
                }

                if (minIndexes.Count == 0)
                {
                    minLengths[idx] = idx + 1;
                }
                else
                {
                    minLengths[idx] = idx - minIndexes.Peek();
                }

                minIndexes.Push(idx);
            }

            return minLengths;
        }

        public static int[] CalculateRightMinLength(long[] arr)
        {
            Stack<int> minIndexes = new Stack<int>();
            int[] minLengths = new int[arr.Length];

            for (int idx = arr.Length-1; idx >= 0; idx--)
            {
                while (minIndexes.Count > 0 && arr[idx] <= arr[minIndexes.Peek()])
                {
                    minIndexes.Pop();
                }

                if (minIndexes.Count == 0)
                {
                    minLengths[idx] = arr.Length - idx;
                }
                else
                {
                    minLengths[idx] = Math.Abs(idx - minIndexes.Peek());
                }

                minIndexes.Push(idx);
            }

            return minLengths;
        }
    }
}

using System;
using System.Collections.Generic;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class MinMaxRiddleUnitTest
    {
        [TestCategory("MinMaxRiddle"), TestMethod]
        public void TestLeftMinLengths()
        {
            long[] arr = new long[] { 2, 3, 11, 1, 8, 3, 4, 5 };
            int[] leftLengths = MinMaxRiddle.CalculateLeftMinLength(arr);
            Console.WriteLine(string.Join(",", arr));
            Console.WriteLine(string.Join(",", leftLengths));
        }

        [TestCategory("MinMaxRiddle"), TestMethod]
        public void TestRightMinLengths()
        {
            long[] arr = new long[] { 2, 3, 11, 1, 8, 3, 4, 5 };
            int[] rightLengths = MinMaxRiddle.CalculateRightMinLength(arr);
            Console.WriteLine(string.Join(",", arr));
            Console.WriteLine(string.Join(",", rightLengths));
        }

        [TestCategory("MinMaxRiddle"), TestMethod]
        public void TestGetMinWindowLengths()
        {
            long[] arr = new long[] { 2, 3, 11, 1, 8, 3, 4, 5 };
            //int[] arr = new int[] { 11, 2, 3, 14, 5, 2, 11, 12 };
            int[] minWindowLengths = MinMaxRiddle.GetMinWindowLengths(arr);
            Console.WriteLine("arr");
            Console.WriteLine(string.Join(",", arr));
            Console.WriteLine("minWindowLengths");
            Console.WriteLine(string.Join(",", minWindowLengths));
            Console.WriteLine();

            Dictionary<long, int> maxLenPerValue = new Dictionary<long, int>();
            for (int i=0; i<arr.Length; i++)
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

            Console.WriteLine("maxLenPerValue");
            foreach (int key in maxLenPerValue.Keys)
            {
                Console.WriteLine(string.Format("{0}:{1}", key, maxLenPerValue[key]));
            }

            int[] maxLens = new int[arr.Length];
            foreach (int val in maxLenPerValue.Keys)
            {
                int windowLen = maxLenPerValue[val];
                maxLens[windowLen-1] = Math.Max(val, maxLens[windowLen-1]);
            }
            Console.WriteLine("maxLens");
            Console.WriteLine(string.Join(",", maxLens));

            int idx = maxLens.Length - 1;
            int last = maxLens[idx];
            for (idx--; idx >= 0; idx--)
            {
                if (maxLens[idx] < last)
                {
                    maxLens[idx] = last;
                }
                else
                {
                    last = maxLens[idx];
                }
            }
            Console.WriteLine("maxLens");
            Console.WriteLine(string.Join(",", maxLens));
        }
    }
}

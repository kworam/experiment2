using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.HackerRank
{
    // a range is defined as a(j) a(j+1) ... a(j+m) a(j+m+1)   
    // where a(j+1) >= a (j+2) < ... a(j+m)
    // and a(j+m+1) > a(j+1)
    // and a(j) > a(j+m+1)

    // ex:  9 7 5 5 8
    // j = 0
    // m = 3 
    // j+m+1 = 4

    // the value at start index 'si' (j+m+1) will 'eat' the 'm' values to the left of it over 'm' days, 
    // and stop when it reaches the value at end index 'ei' (j)

    // If there is a sub-range 'sr' that starts within parent range 'pr':

    // 1. 'sr' cannot extend beyond 'pr' because the startValue(sr) < startValue(pr), so endValue(pr) > startValue(sr)   
    // 2. days(r) = m
    // 3. days(pr) = days(pr) - days(sr)

    // iterate through arr from right to left, tracking all ranges
    // determine days(range) for all ranges, 
    // return max(days(range))


    public class PoisonousPlants
    {
        private class Range
        {
            public int endIndex;
            public int startIndex;
            public int subtractDays;

            public int days()
            {
                return (endIndex - startIndex) - 1 - subtractDays;
            }

            public override string ToString()
            {
                return string.Format("ei:{0}  si:{1}  sd:{2}", endIndex, startIndex, subtractDays);
            }
        }


        public static int poisonousPlants(int[] arr)
        {
            if (arr == null || arr.Length == 1)
            {
                return 0;
            }

            int maxDays = 0;

            Stack<Range> ranges = new Stack<Range>();
            int i = arr.Length - 1;
            int prevValue = arr[i];
            i--;
            while (i >= 0)
            {
                int currValue = arr[i];
                if (currValue < prevValue)
                {
                    Range r = new Range();
                    r.endIndex = i + 1;
                    ranges.Push(r);
                }
                else  // currValue >= prevValue;
                {                    
                    maxDays = UpdateRanges(i, ranges, arr, maxDays);
                }

                prevValue = currValue;
                i--;
            }

            return EndRanges(ranges, arr, maxDays);
        }

        private static int UpdateRanges(int currIndex, Stack<Range> ranges, int[] arr, int maxDays)
        {
            int currValue = arr[currIndex];
            while (ranges.Count > 0)
            {
                Range r = ranges.Peek();
                if (currValue >= arr[r.endIndex])
                {
                    ranges.Pop();
                    r.startIndex = currIndex;
                    if (ranges.Count == 0)
                    {
                        maxDays = Math.Max(maxDays, r.days());
                        break;
                    }
                    else
                    {
                        ranges.Peek().subtractDays += r.days();
                    }
                }
                else
                {
                    break;
                }
            }

            return maxDays;
        }

        private static int EndRanges(Stack<Range> ranges, int[] arr, int maxDays)
        {
            while (ranges.Count > 0)
            {
                Range r = ranges.Pop();
                r.startIndex = -1;
                if (ranges.Count == 0)
                {
                    maxDays = Math.Max(maxDays, r.days());
                    break;
                }
                else
                {
                    ranges.Peek().subtractDays += r.days();
                }
            }

            return maxDays;
        }
    }
}

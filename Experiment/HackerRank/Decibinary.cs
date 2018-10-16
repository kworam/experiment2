using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class Decibinary
    {
        public static long GetSpecifiedDeciBinaryNumber(long x)
        {
            long idx = x - 1;
            int n = 0;
            long count = 0;
            while (true)
            {
                List<long> repsForN = generateAllDbReps(n);
                if (idx >= count && idx < count + repsForN.Count)
                {
                    return repsForN[(int)(idx - count)];
                }
                count += repsForN.Count;
                n++;
            }
        }

        public static List<long> generateAllDbReps(int n)
        {
            List<long> result = new List<long>();
            if (n == 0)
            {
                result.Add(0);
                return result;
            }

            int place = (int)Math.Floor(Math.Log(n, 2));
            long dbNum = 0;
            internalGenerateAllDbReps(n, place, dbNum, result);
            return result;
        }

        private static void internalGenerateAllDbReps(long n, int place, long dbNum, List<long> result)
        {
            if (n == 0)
            {
                result.Add(dbNum);
                return;
            }

            if (place < 0)
            {
                return;
            }

            long pp2 = (long)Math.Pow(2, place);
            long maxDigit = n / pp2;
            if (maxDigit > 9)
            {
                return;
            }

            long pp10 = (long)Math.Pow(10, place);
            for (int digit = 0; digit <= maxDigit; digit++)
            {
                internalGenerateAllDbReps(n - (digit * pp2), place-1, dbNum + (digit * pp10), result);
            }
        }
    }
}

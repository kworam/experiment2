using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.Utility
{
    public class KevinMath
    {
        private static Dictionary<int, long> factCache = new Dictionary<int, long>();
        public static long Factorial(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }

            if (factCache.ContainsKey(n))
            {
                return factCache[n];
            }

            factCache[n] = n * Factorial(n-1);
            return factCache[n];
        }

        public static long LongRandom(long min, long max, Random rand)
        {
            if (min > max)
            {
                long temp = min;
                min = max;
                max = temp;
            }

            if (min == max)
            {
                return min;
            }

            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}

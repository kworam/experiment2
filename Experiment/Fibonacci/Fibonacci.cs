using System.Collections.Generic;

namespace Experiment.Fibonacci
{
    public static class Fibonacci
    {
        private static Dictionary<uint, ulong> cache = new Dictionary<uint, ulong>();

        public static ulong Get(uint n)
        {
            if (Fibonacci.cache.ContainsKey(n))
            {
                return cache[n];
            }

            ulong result;
            if (n == 0)
            {
                result = 0;
            }
            else if (n == 1 || n == 2)
            {
                result = 1;
            }
            else
            {
                result = Get(n - 1) + Get(n - 2);
            }

            return cache[n] = result;
        }
    }
}

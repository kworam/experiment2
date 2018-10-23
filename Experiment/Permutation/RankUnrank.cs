using Experiment.Utility;

namespace Experiment.Permutation
{
    public class RankUnrank
    {
        public static void Unrank(int n, long r, int[] pi)
        {
            if (n == 1)
            {
                return;
            }
            int s = (int)(r / KevinMath.Factorial(n - 1));
            ArrayUtility.Swap(pi, n-1, s);
            Unrank(n-1, r % KevinMath.Factorial(n-1), pi);
        }

        public static long Rank(int n, int[] pi, int[] inv)
        {
            if (n == 1)
            {
                return 0;
            }

            int s = pi[n - 1];
            ArrayUtility.Swap(pi, n - 1, inv[n - 1]);
            ArrayUtility.Swap(inv, s, inv[n - 1]);
            return s * KevinMath.Factorial(n-1) + Rank(n - 1, pi, inv);
        }

        public static int[] InitIndexArray(int n)
        {
            int[] pi = new int[n];
            for (int i = 0; i < pi.Length; i++)
            {
                pi[i] = i;
            }
            return pi;
        }

        public static int[] GetInverseArray(int[] pi)
        {
            int[] inv = new int[pi.Length];
            for (int i = 0; i < pi.Length; i++)
            {
                inv[pi[i]] = i;
            }
            return inv;
        }
    }
}

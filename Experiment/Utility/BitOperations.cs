using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.Utility
{
    public class BitOperations
    {
        public static bool GetBit(int n, int bitpos)
        {
            int mask = 1 << bitpos;
            return (n & mask) != 0;
        }

        public static void ClearBit(ref int n, int bitpos)
        {
            int mask = ~(1 << bitpos);
            n &= mask;
        }

        public static void SetBit(ref int n, int bitpos)
        {
            int mask = 1 << bitpos;
            n |= mask;
        }

        public static void FlipBit(ref int n, int bitpos)
        {
            int mask = 1 << bitpos;
            n ^= mask;
        }
    }
}

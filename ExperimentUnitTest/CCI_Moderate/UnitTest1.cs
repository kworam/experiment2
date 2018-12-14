using System;
using Experiment.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.CCI_Moderate
{
    [TestClass]
    public class OperationsUnitTest
    {
        [TestCategory("Operations"), TestMethod]
        public void Overflow()
        {
            // How can we negate an integer using only addition?
            int b = 1;
            int y = int.MaxValue + b + 1;  // when b is positive, y overflows to int.MinValue + b
            // how to convert int.MinValue + b to 0-b?  
            if (y < 0)
            {
                //
            }
        }

        [TestCategory("Operations"), TestMethod]
        public void TwosComplement()
        {
            for (int i=0; i<100; i++)
            {
                int c = GetTwosComplement(i);
                Assert.IsTrue(SumTo2To32(i, c));
            }
            int cmin = GetTwosComplement(int.MinValue);
            Assert.IsTrue(SumTo2To32(cmin, int.MinValue));
            int cmax = GetTwosComplement(int.MaxValue);
            Assert.IsTrue(SumTo2To32(cmax, int.MaxValue));
        }

        private static bool SumTo2To32(int x, int y)
        {
            return x + y == 0;
        }

        private static int GetTwosComplement(int x)
        {
            bool carry = false;
            int y = 0;
            for (int i=0; i<32; i++)
            {
                if (BitOperations.GetBit(x, i))
                {
                    // current bit of x is set.
                    // we need the current bits plus the carry (if any) to sum to 0
                    if (carry)
                    {
                        BitOperations.ClearBit(ref y, i);
                    }
                    else
                    {
                        BitOperations.SetBit(ref y, i);
                        carry = true;
                    }
                }
                else
                {
                    // current bit of x is clear.
                    // we need the current bits plus the carry (if any) to sum to 0
                    if (carry)
                    {
                        BitOperations.SetBit(ref y, i);
                    }
                    else
                    {
                        BitOperations.ClearBit(ref y, i);
                    }
                }
            }

            return y;
        }
    }
}

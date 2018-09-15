using System;
using System.Collections.Generic;
using Experiment.Fibonacci;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Finbonacci
{
    [TestClass]
    public class FibonacciUnitTest
    {
        [TestCategory("Fibonacci"), TestMethod]
        public void GetPositionZero()
        {
            Assert.AreEqual(Fibonacci.Get(0), (ulong)0);
        }

        [TestCategory("Fibonacci"), TestMethod]
        public void GetPositionOne()
        {
            Assert.AreEqual(Fibonacci.Get(1), (ulong)1);
        }

        [TestCategory("Fibonacci"), TestMethod]
        public void GetPositionTwo()
        {
            Assert.AreEqual(Fibonacci.Get(2), (ulong)1);
        }

        [TestCategory("Fibonacci"), TestMethod]
        public void GetPositionThree()
        {
            Assert.AreEqual(Fibonacci.Get(3), (ulong)2);
        }

        [TestCategory("Fibonacci"), TestMethod]
        public void GetSeries()
        {
            List<ulong> values = new List<ulong>();
            for (uint i = 0; i < 10; i++)
            {
                values.Add(Fibonacci.Get(i));
            }
            Console.WriteLine(String.Join(" ", values));
        }

        //http://www.javascripter.net/math/calculators/fibonaccinumberscalculator.htm
        //n=45
        //1134903170
        [TestCategory("Fibonacci"), TestMethod]
        public void GetPositionFortyFive()
        {
            Assert.AreEqual(Fibonacci.Get(45), (ulong)1134903170);
        }

        //n = 55
        //139583862445
        [TestCategory("Fibonacci"), TestMethod]
        public void GetPositionFiftyFive()
        {
            Assert.AreEqual(Fibonacci.Get(55), (ulong)139583862445);
        }

    }
}

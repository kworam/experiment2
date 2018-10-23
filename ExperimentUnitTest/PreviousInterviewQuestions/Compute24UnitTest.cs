using System.Collections.Generic;
using Experiment.PreviousInterviewQuestions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.PreviousInterviewQuestions
{
    [TestClass]
    public class Compute24UnitTest
    {
        [TestCategory("Compute24"),TestMethod]
        public void Impossible()
        {
            List<int> nums = new List<int>() { 1, 1, 1, 1 };
            Assert.IsFalse(Compute24.CanCompute24(nums));
        }

        [TestCategory("Compute24"), TestMethod]
        public void Possible()
        {
            List<int> nums = new List<int>() { 2, 2, 3, 2 };
            Assert.IsTrue(Compute24.CanCompute24(nums));
        }
    }
}

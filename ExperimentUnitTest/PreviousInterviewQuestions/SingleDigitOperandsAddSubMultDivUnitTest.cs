using System;
using Experiment.PreviousInterviewQuestions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.PreviousInterviewQuestions
{
    [TestClass]
    public class SingleDigitOperandsAddSubMultDivUnitTest
    {
        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void NullExpression()
        {
            Assert.AreEqual(0, SingleDigitOperandsAddSubMultDiv.Evaluate(null));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void EmptyExpression()
        {
            Assert.AreEqual(0, SingleDigitOperandsAddSubMultDiv.Evaluate(string.Empty));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void SimpleAdd()
        {
            Assert.AreEqual(7, SingleDigitOperandsAddSubMultDiv.Evaluate("3+4"));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void SimpleSubtract()
        {
            Assert.AreEqual(-1, SingleDigitOperandsAddSubMultDiv.Evaluate("3-4"));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void SimpleMultiply()
        {
            Assert.AreEqual(12, SingleDigitOperandsAddSubMultDiv.Evaluate("3*4"));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void SimpleDivide()
        {
            Assert.AreEqual(1, SingleDigitOperandsAddSubMultDiv.Evaluate("3/3"));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void ComplexMultiplyAdd()
        {
            Assert.AreEqual(44, SingleDigitOperandsAddSubMultDiv.Evaluate("4*3*2+5*4"));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void ComplexDivideSubtract()
        {
            Assert.AreEqual(-2, SingleDigitOperandsAddSubMultDiv.Evaluate("8/4/2-9/3"));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void ComplexAddMutiply()
        {
            Assert.AreEqual(23, SingleDigitOperandsAddSubMultDiv.Evaluate("4+3+2*5+6"));
        }

        [TestCategory("SingleDigitOperandsAddSubMultDiv"), TestMethod]
        public void ComplexSubtractDivide()
        {
            Assert.AreEqual(8, SingleDigitOperandsAddSubMultDiv.Evaluate("8-4-9/3+7"));
        }
    }
}

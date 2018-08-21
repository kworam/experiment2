using Experiment.CCI_Hard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.CCI_Hard
{
	[TestClass]
	public class CCI_Hard_UnitTest
	{
		[TestCategory("CCI_Hard"), TestMethod]
		public void AddWithoutPlusXorAndZero()
		{
			int a = 0;
			int b = 0;
			Assert.AreEqual(AddWithoutArithmeticOperators.AddWithoutPlusXorAnd(a, b), a + b);
		}

		[TestCategory("CCI_Hard"), TestMethod]
		public void AddWithoutPlusXorAndBothPos()
		{
			int a = 3;
			int b = 5;
			Assert.AreEqual(AddWithoutArithmeticOperators.AddWithoutPlusXorAnd(a, b), a+b);
		}

		[TestCategory("CCI_Hard"), TestMethod]
		public void AddWithoutPlusXorAndBothNegative()
		{
			int a = -56;
			int b = -15;
			Assert.AreEqual(AddWithoutArithmeticOperators.AddWithoutPlusXorAnd(a, b), a + b);
		}

		[TestCategory("CCI_Hard"), TestMethod]
		public void AddWithoutPlusXorAndPosNeg()
		{
			int a = 67;
			int b = -35;
			Assert.AreEqual(AddWithoutArithmeticOperators.AddWithoutPlusXorAnd(a, b), a + b);
		}
	}
}

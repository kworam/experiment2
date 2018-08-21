using Experiment.CCI_Moderate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.CCI_Moderate
{
	[TestClass]
	public class CCI_Moderate_UnitTest
	{
		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapArithmeticEqual()
		{
			int a = 3;
			int b = 3;
			NumberSwap.NumberSwapArithmetic(ref a, ref b);
			Assert.AreEqual(a, 3);
			Assert.AreEqual(b, 3);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapArithmeticLesserGreater()
		{
			int a = 3;
			int b = 8;
			NumberSwap.NumberSwapArithmetic(ref a, ref b);
			Assert.AreEqual(a, 8);
			Assert.AreEqual(b, 3);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapArithmeticGreaterLesser()
		{
			int a = 9;
			int b = 2;
			NumberSwap.NumberSwapArithmetic(ref a, ref b);
			Assert.AreEqual(a, 2);
			Assert.AreEqual(b, 9);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapArithmeticLesserGreaterNegative()
		{
			int a = -10;
			int b = 8;
			NumberSwap.NumberSwapArithmetic(ref a, ref b);
			Assert.AreEqual(a, 8);
			Assert.AreEqual(b, -10);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapArithmeticGreaterLesserNegative()
		{
			int a = -9;
			int b = -11;
			NumberSwap.NumberSwapArithmetic(ref a, ref b);
			Assert.AreEqual(a, -11);
			Assert.AreEqual(b, -9);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapXorEqual()
		{
			int a = 3;
			int b = 3;
			NumberSwap.NumberSwapXor(ref a, ref b);
			Assert.AreEqual(a, 3);
			Assert.AreEqual(b, 3);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapXorLesserGreater()
		{
			int a = 3;
			int b = 8;
			NumberSwap.NumberSwapXor(ref a, ref b);
			Assert.AreEqual(a, 8);
			Assert.AreEqual(b, 3);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapXorGreaterLesser()
		{
			int a = 9;
			int b = 2;
			NumberSwap.NumberSwapXor(ref a, ref b);
			Assert.AreEqual(a, 2);
			Assert.AreEqual(b, 9);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapXorLesserGreaterNegative()
		{
			int a = -10;
			int b = 8;
			NumberSwap.NumberSwapXor(ref a, ref b);
			Assert.AreEqual(a, 8);
			Assert.AreEqual(b, -10);
		}

		[TestCategory("CCI_Moderate"), TestMethod]
		public void NumberSwapXorGreaterLesserNegative()
		{
			int a = -9;
			int b = -11;
			NumberSwap.NumberSwapXor(ref a, ref b);
			Assert.AreEqual(a, -11);
			Assert.AreEqual(b, -9);
		}
	}
}

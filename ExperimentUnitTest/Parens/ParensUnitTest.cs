using System.Collections.Generic;
using Experiment.Parens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Parens
{
	[TestClass]
	public class ParensUnitTest
	{
		[TestCategory("Parens"), TestMethod]
		public void Negative()
		{
			List<string> parens = Experiment.Parens.Parens.GetParens(-1);
			Assert.IsNull(parens);
			List<string> parensBook = Experiment.Parens.Parens.GetParensBook(-1);
			Assert.IsNull(parensBook);
			List<string> parensBookKevin = Experiment.Parens.Parens.GetParensBookKevin(-1);
			Assert.IsNull(parensBookKevin);
		}

		[TestCategory("Parens"), TestMethod]
		public void Zero()
		{
			List<string> parens = Experiment.Parens.Parens.GetParens(0);
			Assert.AreEqual(parens.Count, 1);
			Assert.AreEqual(parens[0], string.Empty);

			List<string> parensBook = Experiment.Parens.Parens.GetParensBook(0);
			Assert.AreEqual(parensBook.Count, 1);
			Assert.AreEqual(parensBook[0], string.Empty);

			List<string> parensBookKevin = Experiment.Parens.Parens.GetParensBookKevin(0);
			Assert.AreEqual(parensBookKevin.Count, 1);
			Assert.AreEqual(parensBookKevin[0], string.Empty);
		}

		[TestCategory("Parens"), TestMethod]
		public void One()
		{
			List<string> parens = Experiment.Parens.Parens.GetParens(1);
			Assert.AreEqual(parens.Count, 1);

			//List<string> parensBook = Experiment.Parens.Parens.GetParensBook(1);
			//Assert.AreEqual(parensBook.Count, 1);

			List<string> parensBookKevin = Experiment.Parens.Parens.GetParensBookKevin(1);
			Assert.AreEqual(parensBookKevin.Count, 1);
		}

		[TestCategory("Parens"), TestMethod]
		public void Two()
		{
			List<string> parens = Experiment.Parens.Parens.GetParens(2);
			Assert.AreEqual(parens.Count, 2);

			//List<string> parensBook = Experiment.Parens.Parens.GetParensBook(2);
			//Assert.AreEqual(parensBook.Count, 2);

			List<string> parensBookKevin = Experiment.Parens.Parens.GetParensBookKevin(2);
			Assert.AreEqual(parensBookKevin.Count, 2);
		}

		[TestCategory("Parens"), TestMethod]
		public void Three()
		{
			List<string> parens = Experiment.Parens.Parens.GetParens(3);
			Assert.AreEqual(parens.Count, 5);

			List<string> parensBookKevin = Experiment.Parens.Parens.GetParensBookKevin(3);
			Assert.AreEqual(parensBookKevin.Count, 5);
		}

		[TestCategory("Parens"), TestMethod]
		public void Four()
		{
			List<string> parens = Experiment.Parens.Parens.GetParens(4);
			//Assert.AreEqual(parens.Count, 14);

			List<string> parensBookKevin = Experiment.Parens.Parens.GetParensBookKevin(4);
			//Assert.AreEqual(parensBookKevin.Count, 14);
		}

		[TestCategory("Parens"), TestMethod]
		public void Five()
		{
			List<string> parens = Experiment.Parens.Parens.GetParens(5);
			//Assert.AreEqual(parens.Count, 5);

			List<string> parensBookKevin = Experiment.Parens.Parens.GetParensBookKevin(5);
			//Assert.AreEqual(parensBookKevin.Count, 42);
		}
	}
}

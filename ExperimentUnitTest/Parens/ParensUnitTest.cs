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
			//List<string> parens = Experiment.Parens.Parens.GetParens(-1);
			//Assert.IsNull(parens);
			Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(-1);
			Assert.IsNull(parens);
			Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(-1);
			Assert.IsNull(parens2);
		}

		[TestCategory("Parens"), TestMethod]
		public void Zero()
		{
			//Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GetParens(0);
			//Assert.AreEqual(parens.Count, 1);
			//Assert.AreEqual(parens[0], string.Empty);

			Experiment.Parens.Parens.ParensResult parensBook = Experiment.Parens.Parens.GenerateParens(0);
			Assert.AreEqual(parensBook.list.Count, 1);
			Assert.AreEqual(parensBook.list[0], string.Empty);

			Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(0);
			Assert.AreEqual(parens2.list.Count, 1);
			Assert.AreEqual(parens2.list[0], string.Empty);
		}

		[TestCategory("Parens"), TestMethod]
		public void One()
		{
			//List<string> parens = Experiment.Parens.Parens.GetParens(1);
			//Assert.AreEqual(parens.Count, 1);

			Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(1);
			Assert.AreEqual(parens.list.Count, 1);

			Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(1);
			Assert.AreEqual(parens2.list.Count, 1);
		}

		[TestCategory("Parens"), TestMethod]
		public void Two()
		{
			//List<string> parens = Experiment.Parens.Parens.GetParens(2);
			//Assert.AreEqual(parens.Count, 2);

			Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(2);
			Assert.AreEqual(parens.list.Count, 2);

			Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(2);
			Assert.AreEqual(parens2.list.Count, 2);
		}

		[TestCategory("Parens"), TestMethod]
		public void Three()
		{
			Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(3);
			Assert.AreEqual(parens.list.Count, 5);

			Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(3);
			Assert.AreEqual(parens2.list.Count, 5);
		}

		[TestCategory("Parens"), TestMethod]
		public void Four()
		{
			Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(4);
			Assert.AreEqual(parens.list.Count, 14);

			Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(4);
			Assert.AreEqual(parens2.list.Count, 14);
		}

		[TestCategory("Parens"), TestMethod]
		public void Five()
		{
			Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(5);
			Assert.AreEqual(parens.list.Count, 42);

			Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(5);
			Assert.AreEqual(parens2.list.Count, 42);
		}

        [TestCategory("Parens"), TestMethod]
        public void Eight()
        {
            Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(8);
            //Assert.AreEqual(parens.list.Count, 42);

            Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(8);
            //Assert.AreEqual(parens2.list.Count, 42);
        }

        [TestCategory("Parens"), TestMethod]
        public void Ten()
        {
            Experiment.Parens.Parens.ParensResult parens = Experiment.Parens.Parens.GenerateParens(10);
            //Assert.AreEqual(parens.list.Count, 42);

            Experiment.Parens.Parens.ParensResult parens2 = Experiment.Parens.Parens.GenerateParens2Count(10);
            //Assert.AreEqual(parens2.list.Count, 42);
        }
    }
}

using System.Collections.Generic;
using Experiment.Permutation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Permutation
{
	[TestClass]
	public class PermutationUnitTest
	{
		[TestCategory("Permutation"), TestMethod]
		public void NullString()
		{
			Assert.IsNull(Experiment.Permutation.Permutation.GetPerms(null));
		}

		[TestCategory("Permutation"), TestMethod]
		public void EmptyString()
		{
			List<string> perms = Experiment.Permutation.Permutation.GetPerms(string.Empty);
			Assert.AreEqual(1, perms.Count);
			Assert.AreEqual(string.Empty, perms[0]);
		}

		[TestCategory("Permutation"), TestMethod]
		public void SingletonString()
		{
			string singleton = "a";
			List<string> perms = Experiment.Permutation.Permutation.GetPerms(singleton);
			Assert.AreEqual(1, perms.Count);
			Assert.AreEqual(singleton, perms[0]);
		}
	}
}

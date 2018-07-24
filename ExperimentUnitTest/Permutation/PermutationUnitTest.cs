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

		[TestCategory("Permutation"), TestMethod]
		public void StringLengthTwo()
		{
			string s = "ab";
			List<string> perms = Experiment.Permutation.Permutation.GetPerms(s);
			Assert.AreEqual(2, perms.Count);
			//Assert.AreEqual(singleton, perms[0]);
		}

		[TestCategory("Permutation"), TestMethod]
		public void StringLengthThree()
		{
			string s = "abc";
			List<string> perms = Experiment.Permutation.Permutation.GetPerms(s);
			Assert.AreEqual(6, perms.Count);
			//Assert.AreEqual(singleton, perms[0]);
		}

		[TestCategory("Permutation"), TestMethod]
		public void aab()
		{
			string s = "aab";
			List<string> perms = Experiment.Permutation.Permutation.GetPerms(s);
			Assert.AreEqual(6, perms.Count);
			List<string> permsNoDups = Experiment.Permutation.Permutation.GetPermsWithDups(s);
			//Assert.AreEqual(singleton, perms[0]);
			List<string> permsNoDupsBruteForce = Experiment.Permutation.Permutation.GetPermsWithDupsBruteForce(s);
			List<string> permsWithDups2 = Experiment.Permutation.Permutation.GetPermsWithDups2(s);
		}

		[TestCategory("Permutation"), TestMethod]
		public void aaab()
		{
			string s = "aaab";
			List<string> perms = Experiment.Permutation.Permutation.GetPerms(s);
			Assert.AreEqual(24, perms.Count);
			List<string> permsWithDups2 = Experiment.Permutation.Permutation.GetPermsWithDups2(s);
		}
	}
}

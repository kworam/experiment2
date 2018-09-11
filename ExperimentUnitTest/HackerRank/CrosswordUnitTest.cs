using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
	[TestClass]
	public class CrosswordUnitTest
	{
		[TestCategory("Crossword"), TestMethod]
		public void TestCase0()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\Crossword_TestCase0.txt"))
			{
				string[] crossword = new string[10];

				for (int i = 0; i < 10; i++)
				{
					string crosswordItem = sr.ReadLine();
					crossword[i] = crosswordItem;
				}

				string words = sr.ReadLine();

				string[] result = Crossword.crosswordPuzzle(crossword, words);

				Console.WriteLine(string.Join("\n", result));
			}
		}

		[TestCategory("Crossword"), TestMethod]
		public void TestCase2()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\Crossword_TestCase2.txt"))
			{
				string[] crossword = new string[10];

				for (int i = 0; i < 10; i++)
				{
					string crosswordItem = sr.ReadLine();
					crossword[i] = crosswordItem;
				}

				string words = sr.ReadLine();

				string[] result = Crossword.crosswordPuzzle(crossword, words);

				Console.WriteLine(string.Join("\n", result));
			}
		}

		[TestCategory("Crossword"), TestMethod]
		public void TestCase5()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\Crossword_TestCase5.txt"))
			{
				string[] crossword = new string[10];

				for (int i = 0; i < 10; i++)
				{
					string crosswordItem = sr.ReadLine();
					crossword[i] = crosswordItem;
				}

				string words = sr.ReadLine();

				string[] result = Crossword.crosswordPuzzle(crossword, words);

				Console.WriteLine(string.Join("\n", result));
			}
		}

		[TestCategory("Crossword"), TestMethod]
		public void TestCase6()
		{
			using (StreamReader sr = new StreamReader(@"HackerRank\Crossword_TestCase6.txt"))
			{
				string[] crossword = new string[10];

				for (int i = 0; i < 10; i++)
				{
					string crosswordItem = sr.ReadLine();
					crossword[i] = crosswordItem;
				}

				string words = sr.ReadLine();

				string[] result = Crossword.crosswordPuzzle(crossword, words);

				Console.WriteLine(string.Join("\n", result));
			}
		}
	}
}

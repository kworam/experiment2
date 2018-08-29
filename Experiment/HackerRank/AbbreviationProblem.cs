using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
	class AbbreviationProblem
	{
		// Complete the abbreviation function below.
		public static string Abbreviation(string a, string b)
		{
			Dictionary<string, string> cache = new Dictionary<string, string>();
			return InternalAbbreviation(a, 0, b, 0, cache);
		}

		private static string GetCacheKey(int aIndex, int bIndex)
		{
			return string.Format("{0}-{1}", aIndex, bIndex);
		}

		private static string InternalAbbreviation(
			string a, int aIndex, string b, int bIndex, Dictionary<string, string> cache)
		{
			string cacheKey = GetCacheKey(aIndex, bIndex);
			if (cache.ContainsKey(cacheKey))
			{
				return cache[cacheKey];
			}

			if (a.Length - aIndex < b.Length - bIndex)
			{
				cache[cacheKey] = "NO";
				return cache[cacheKey];
			}

			if (bIndex == b.Length)
			{
				while (aIndex < a.Length && Char.IsLower(a[aIndex]))
				{
					aIndex++;
				}

				cache[cacheKey] = aIndex == a.Length ? "YES" : "NO";
				return cache[cacheKey];
			}

			string answer = null;
			if (Char.ToUpper(a[aIndex]) == Char.ToUpper(b[bIndex]))
			{
				// same letter, case-insensitive
				if (Char.IsLower(a[aIndex]))
				{
					// same letter but lower, try dropping it
					answer = InternalAbbreviation(a, aIndex + 1, b, bIndex, cache);
				}

				if (answer != "YES")
				{
					// same letter, dropping it didn't work
					answer = InternalAbbreviation(a, aIndex + 1, b, bIndex + 1, cache);
				}
			}
			else
			{
				// different letter
				answer = Char.IsLower(a[aIndex])
					// try dropping it if it's lower
					? InternalAbbreviation(a, aIndex + 1, b, bIndex, cache)
					// else fail
					: "NO";
			}

			cache[cacheKey] = answer;
			return cache[cacheKey];
		}
	}
}

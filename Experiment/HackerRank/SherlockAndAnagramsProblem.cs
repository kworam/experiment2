using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
	class SherlockAndAnagramsProblem
	{
		public static int SherlockAndAnagrams(string s)
		{
			int na = 0;
			Dictionary<char, int> ss1cc = new Dictionary<char, int>();
			Dictionary<char, int> ss2cc = new Dictionary<char, int>();
			for (int ssLen = 1; ssLen < s.Length; ssLen++)
			{
				for (int s1 = 0; s1 < s.Length - ssLen; s1++)
				{
					InitOrUpdateCharCount(ss1cc, s, 0, s1, ssLen);
					for (int s2 = s1 + 1; s2 < s.Length - ssLen + 1; s2++)
					{
						InitOrUpdateCharCount(ss2cc, s, s1 + 1, s2, ssLen);
						if (IsAnagram(ss1cc, ss2cc))
						{
							na++;
						}
					}
				}
			}
			return na;
		}

		private static bool IsAnagram(Dictionary<char, int> cc1, Dictionary<char, int> cc2)
		{
			if (cc1.Keys.Count != cc2.Keys.Count)
			{
				return false;
			}

			foreach (char c1 in cc1.Keys)
			{
				if (!cc2.ContainsKey(c1) || cc2[c1] != cc1[c1])
				{
					return false;
				}
			}

			return true;
		}

		private static void InitOrUpdateCharCount(
			Dictionary<char, int> cc, string s, int startIndex, int currIndex, int ssLen)
		{
			if (currIndex == startIndex)
			{
				InitCharCount(cc, s, currIndex, ssLen);
			}
			else
			{
				RemoveCharFromCount(cc, s[currIndex - 1]);
				AddCharToCount(cc, s[currIndex + ssLen - 1]);
			}
		}

		private static void InitCharCount(Dictionary<char, int> cc, string s, int start, int ssLen)
		{
			cc.Clear();
			for (int i = start; i < start + ssLen; i++)
			{
				AddCharToCount(cc, s[i]);
			}
		}

		private static void AddCharToCount(Dictionary<char, int> cc, char c)
		{
			if (!cc.ContainsKey(c))
			{
				cc[c] = 0;
			}
			cc[c]++;
		}

		private static void RemoveCharFromCount(Dictionary<char, int> cc, char c)
		{
			if (!cc.ContainsKey(c))
			{
				throw new Exception(string.Format("char {0} not found in count!", c));
			}
			cc[c]--;
			if (cc[c] == 0)
			{
				cc.Remove(c);
			}
		}
	}
}

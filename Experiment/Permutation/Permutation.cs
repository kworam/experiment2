using System.Text;
using System.Collections.Generic;

namespace Experiment.Permutation
{
	public static class Permutation
	{
		public static List<string> GetPerms(string s)
		{
			if (s == null) return null;

			return InternalGetPerms(s);
		}

		public static List<string> GetPermsWithDupsBruteForce(string s)
		{
			if (s == null) return null;

			return new List<string>(InternalGetPermsBruteForce(s));
		}

		public static List<string> GetPermsWithDups(string s)
		{
			if (s == null) return null;

			Dictionary<char, int> counts = GetCounts(s);
			string uniqueSubstring = GetUniqueSubstring(counts);
			List<string> runs = GetRuns(counts);
			List<string> perms = GetPerms(uniqueSubstring);
			List<string> result = new List<string>();
			foreach (string run in runs)
			{
				foreach (string perm in perms)
				{
					result.AddRange(Distribute(run, perm));
				}
				perms = result;
			}

			return perms;
		}

		public static List<string> GetPermsWithDups2(string s)
		{
			Dictionary<char, int> counts = GetCounts(s);
			string prefix = string.Empty;
			List<string> results = new List<string>();
			InternalGetPermsWithDups2(prefix, counts, results, s.Length);
			return results;
		}

		private static void InternalGetPermsWithDups2(
			string prefix,
			Dictionary<char, int> counts,
			List<string> results,
			int length)
		{
			if (prefix.Length == length)
			{
				results.Add(prefix);
				return;
			}

			foreach (char c in counts.Keys)
			{
				InternalGetPermsWithDups2(prefix + c, Decrement(c, counts), results, length);
			}
		}

		private static Dictionary<char, int> Decrement(char c, Dictionary<char, int> counts)
		{
			Dictionary<char, int> result = new Dictionary<char, int>(counts);
			result[c]--;
			if (result[c] == 0) result.Remove(c);
			return result;
		}

		private static List<string> Distribute(string run, string perm)
		{
			List<string> result = new List<string>();
			string prefix = string.Empty;
			InternalDistribute(prefix, run, perm, result);
			return result;
		}

		private static void InternalDistribute(
			string prefix, string run, string perm, List<string> result)
		{
			if (run == string.Empty) result.Add(prefix + perm);
			if (perm == string.Empty) result.Add(prefix + run);
			for (int i = 0; i < run.Length; i++)
			{
				string runPrefix = run.Substring(0, i+1);
				string runSuffix = run.Substring(i+1);
				for (int j = 0; j <= perm.Length; j++)
				{
					string permPrefix = perm.Substring(0, j);
					string permSuffix = perm.Substring(j);
					InternalDistribute(prefix + permPrefix + runPrefix,
						runSuffix,
						permSuffix,
						result);
				}
			}
		}

		private static Dictionary<char, int> GetCounts(string s)
		{
			Dictionary<char, int> counts = new Dictionary<char, int>();
			foreach (char c in s)
			{
				if (!counts.ContainsKey(c))
				{
					counts[c] = 0;
				}
				counts[c]++;
			}
			return counts;
		}

		private static string GetUniqueSubstring(Dictionary<char, int> counts)
		{
			StringBuilder sb = new StringBuilder(); 
			foreach (char c in counts.Keys)
			{
				if (counts[c] == 1)
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		private static List<string> GetRuns(Dictionary<char, int> counts)
		{
			List<string> runs = new List<string>();
			foreach (char c in counts.Keys)
			{
				if (counts[c] > 1)
				{
					runs.Add(GetRun(c, counts[c]));
				}
			}
			return runs;
		}

		private static string GetRun(char c, int cnt)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < cnt; i++)
			{
				sb.Append(c);
			}
			return sb.ToString();
		}

		private static List<string> InternalGetPerms(string s)
		{
			if (s == string.Empty)
			{
				return new List<string>() { s };
			}
			
			// s is not empty
			string first = s[0].ToString();
			List<string> perms = new List<string>();
			foreach (string perm in InternalGetPerms(s.Substring(1)))
			{
				for (int i = 0; i <= perm.Length; i++)
				{
					perms.Add(perm.Insert(i, first));
				}				
			}

			return perms;
		}

		private static HashSet<string> InternalGetPermsBruteForce(string s)
		{
			if (s == string.Empty)
			{
				return new HashSet<string>() { s };
			}

			// s is not empty
			string first = s[0].ToString();
			HashSet<string> perms = new HashSet<string>();
			foreach (string perm in InternalGetPerms(s.Substring(1)))
			{
				for (int i = 0; i <= perm.Length; i++)
				{
					perms.Add(perm.Insert(i, first));
				}
			}

			return perms;
		}
	}
}

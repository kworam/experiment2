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
	}
}

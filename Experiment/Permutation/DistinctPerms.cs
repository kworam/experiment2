using System.Linq;
using System.Collections.Generic;

namespace Experiment.Permutation
{
    public class DistinctPerms
    {
        public static List<string> GetPerms(string s)
        {
            if (s == null)
            {
                return null;
            }

            if (s == string.Empty)
            {
                return new List<string>() { string.Empty };
            }

            Dictionary<char, int> charCounts = GetCharCounts(s);
            char[] arr = new char[s.Length];
            List<string> result = new List<string>();
            GetDistinctPerms(new char[s.Length], 0, charCounts, result);
            return result;
        }

        private static Dictionary<char, int> GetCharCounts(string s)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>();
            for (int i=0; i<s.Length; i++)
            {
                char c = s[i];
                if (!counts.ContainsKey(c))
                {
                    counts[c] = 0;
                }
                counts[c]++;
            }
            return counts;
        }

        private static void GetDistinctPerms(char[] arr, int index, Dictionary<char, int> charCounts, List<string> result)
        {
            if (charCounts.Count == 1)
            {
                char c = charCounts.Keys.First();
                for (int i=index; i<arr.Length; i++)
                {
                    arr[i] = c;
                }
                result.Add(new string(arr));
                return;
            }

            List<char> chars = charCounts.Keys.ToList();
            foreach (char c in chars)
            {
                if (index > 0 && c == arr[index-1])
                {
                    continue;
                }

                int count = charCounts[c];
                for (int i=0; i<count; i++)
                {
                    arr[index+i] = c;
                    charCounts[c]--;
                    if (charCounts[c] == 0)
                    {
                        charCounts.Remove(c);
                    }
                    GetDistinctPerms(arr, index + i + 1, charCounts, result);
                }
                charCounts[c] = count;
            }
        }
    }
}

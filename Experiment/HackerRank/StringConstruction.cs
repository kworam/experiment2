using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class StringConstruction
    {
        public static int stringConstruction(string s)
        {
            Dictionary<char, List<int>> charIndexes = new Dictionary<char, List<int>>();
            int i = 0;
            int cost = 0;
            while (i < s.Length)
            {
                char c = s[i];
                if (charIndexes.ContainsKey(c))
                {
                    // check for maximum substring match
                    int length = maxSubString(s, charIndexes, i);
                    while (length > 0)
                    {
                        i += length;
                        length = maxSubString(s, charIndexes, i);
                    }
                }
                else
                {
                    // first time we encountered this letter
                    charIndexes[c] = new List<int>();
                    charIndexes[c].Add(i);
                    cost++;
                    i++;
                }
            }
            return cost;
        }

        static int maxSubString(string s, Dictionary<char, List<int>> charIndexes, int i)
        {
            if (i >= s.Length)
            {
                return 0;
            }

            int maxLength = 0;
            char c = s[i];
            if (!charIndexes.ContainsKey(c))
            {
                return 0;
            }

            foreach (int start in charIndexes[c])
            {
                maxLength = Math.Max(maxLength, GetMaxMatch(s, start, i));
            }
            return maxLength;
        }

        static int GetMaxMatch(string s, int s1, int s2)
        {
            int len = 0;
            int origs2 = s2;
            while (s2 < s.Length && s1 < origs2 && s[s1] == s[s2])
            {
                len++;
                s1++;
                s2++;
            }
            return len;
        }
    }
}

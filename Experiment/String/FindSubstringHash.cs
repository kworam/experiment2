using System;

namespace Experiment.String
{
    public class FindSubstringHash
    {
        public static int FindSubStringHash(string s, string ss, int alphaCount)
        {
            if (ss.Length > s.Length)
            {
                return -1;
            }

            int ssHash = GetHash(ss, 0, ss.Length, alphaCount);
            int hash = GetHash(s, 0, ss.Length, alphaCount);
            if (hash == ssHash && check(ss, s, 0))
            {
                return 0;
            }

            for (int i = ss.Length; i < s.Length; i++)
            {
                int start = i - ss.Length + 1;
                hash = UpdateHash(hash, s, start, ss.Length, alphaCount);
                if (hash == ssHash && check(ss, s, start))
                {
                    return start;
                }
            }

            return -1;
        }

        private static int GetHash(string s, int start, int length, int alphaCount)
        {
            int hash = 0;
            for (int i = start, pow = length - 1; i < start + length; i++, pow--)
            {
                hash += (int) Math.Pow(alphaCount, pow) * (s[i] - 'a');
            }
            return hash;
        }

        private static int UpdateHash(int oldHash, string s, int newStart, int length, int alphaCount)
        {
            int oldStart = newStart - 1;
            int newEnd = oldStart + length;
            int newHash = oldHash;
            newHash -= (int) Math.Pow(alphaCount, length - 1) * (s[oldStart] - 'a');
            newHash *= alphaCount;
            newHash += (s[newEnd] - 'a');
            return newHash;
        }

        private static bool check(string toMatch, string s, int bStart)
        {
            for (int i = 0; i < toMatch.Length; i++)
            {
                if (toMatch[i] != s[bStart + i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

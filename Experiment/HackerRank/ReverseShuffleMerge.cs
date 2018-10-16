using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experiment.HackerRank
{
    public class ReverseShuffleMerge
    {
        public static string reverseShuffleMerge(string s)
        {
            return reverse(FindMaxMergePair(s));
        }

        private static string FindMaxMergePair(string s)
        {
            CharInfo info = new CharInfo(s);
            char[] result = new char[s.Length / 2];
            return FindMaxMergePair(info, 0, result, null);
        }

        private static string FindMaxMergePair(CharInfo ci, int idx, char[] result, StringChar lastSuccessfulChar)
        {
            if (idx == result.Length)
            {
                return new string(result);
            }

            string s = null;
            StringChar lastFailedChar = null;
            while (s == null)
            {
                StringChar sc = ci.RemoveMaxPossibleChar(lastSuccessfulChar, lastFailedChar);
                if (sc == null)
                {
                    return null;
                }

                result[idx] = sc.c;
                s = FindMaxMergePair(ci, idx + 1, result, sc);
                if (s == null)
                {
                    lastFailedChar = sc;
                    ci.AddChar(sc);
                }
            }

            return s;
        }


        private static string reverse(string s)
        {
            char[] a = s.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }

        class StringChar
        {
            public readonly char c;
            public readonly int idx;
            public bool available = true;

            public StringChar(char c, int idx)
            {
                this.c = c;
                this.idx = idx;
            }

            public override string ToString()
            {
                return string.Format("{0}:{1}:{2}", this.c, this.idx, this.available);
            }
        }

        class CharInfo
        {
            private readonly Dictionary<char, List<StringChar>> remainingChars = new Dictionary<char, List<StringChar>>();
            private Dictionary<char, int> remainingCountsByChar;
            private int remainingCount;
            private string s;

            public CharInfo(string s)
            {
                this.s = s;
                for (int i = 0; i < s.Length; i++)
                {
                    this.AddChar(new StringChar(s[i], i));
                }
                InitRemainingCounts(s);
            }

            private void InitRemainingCounts(string s)
            {
                remainingCountsByChar = new Dictionary<char, int>();
                foreach (char c in remainingChars.Keys)
                {
                    remainingCountsByChar[c] = remainingChars[c].Count / 2;
                }
                remainingCount = s.Length / 2;
            }

            //public bool CharAvailable(StringChar lastChar)
            //{
            //    return lastChar == null ? true : remainingChars.Count > 0;
            //}

            public StringChar RemoveMaxPossibleChar(StringChar lastSuccessfulChar, StringChar lastFailedChar)
            {
                int lastIdx = lastSuccessfulChar != null ? lastSuccessfulChar.idx : -1;
                for (char c = 'z'; c >= 'a'; c--)
                {
                    if (!remainingCountsByChar.ContainsKey(c) 
                        || remainingCountsByChar[c] == 0 || 
                        (lastFailedChar != null && c >= lastFailedChar.c))
                    {
                        // avoid chars that are not part of the word, or are complete, or failed before
                        continue;
                    }
                    
                    if (remainingChars.ContainsKey(c))
                    {
                        List<StringChar> indexes = remainingChars[c];
                        for (int i = 0; i < indexes.Count; i++)
                        {
                            StringChar sc = indexes[i];
                            if (sc.available && sc.idx > lastIdx && sc.idx + remainingCount < s.Length - 1)
                            {
                                sc.available = false;
                                remainingCountsByChar[sc.c]--;
                                remainingCount--;
                                return sc;
                            }
                        }
                    }
                }

                return null;
            }

            public void AddChar(StringChar sc)
            {
                if (!remainingChars.ContainsKey(sc.c))
                {
                    remainingChars[sc.c] = new List<StringChar>();
                }

                StringChar existing = FindChar(sc, remainingChars[sc.c]);
                if (existing == null)
                {
                    remainingChars[sc.c].Add(sc);
                }
                else
                {
                    existing.available = true;
                }

                if (remainingCountsByChar != null)
                {
                    remainingCountsByChar[sc.c]++;
                    remainingCount++;
                }
            }

            private StringChar FindChar(StringChar sc, List<StringChar> lst)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].idx == sc.idx)
                    {
                        return lst[i];
                    }
                }
                return null;
            }
        }

        //private class CharInfo
        //{
        //    public Dictionary<char, int> counts;
        //    public char[] sorted;
        //}

        //public static string reverseShuffleMerge(string s)
        //{
        //    CharInfo charInfo = getCharInfo(s);

        //    int permNumber = 0;
        //    while (true)
        //    {
        //        string a = getPerm(charInfo, permNumber++);
        //        if (a == null)
        //        {
        //            return null;
        //        }

        //        string r = reverse(a);
        //        if (mergesTo(r, a, s))
        //        {
        //            return a;
        //        }
        //    }
        //}

        //// Complete the reverseShuffleMerge function below.
        //public static string reverseShuffleMerge2(string s)
        //{
        //    CharInfo charInfo = getCharInfo(s);
        //    int permNumber = 0;
        //    while (true)
        //    {
        //        string a = getPerm(charInfo, permNumber++);
        //        if (a == null)
        //        {
        //            return null;
        //        }

        //        string r = reverse(a);
        //        if (mergesTo(r, a, s))
        //        {
        //            return a;
        //        }
        //    }
        //}

        //private static CharInfo getCharInfo(string s)
        //{
        //    Dictionary<char, int> counts = new Dictionary<char, int>();
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        if (!counts.ContainsKey(s[i]))
        //        {
        //            counts[s[i]] = 0;
        //        }
        //        counts[s[i]]++;
        //    }

        //    char[] sorted = counts.Keys.ToArray();
        //    Array.Sort(sorted);
        //    return new CharInfo()
        //    {
        //        counts = counts,
        //        sorted = sorted
        //    };
        //}


        //private static string getPerm(CharInfo charInfo, int permNumber)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < charInfo.sorted.Length; i++)
        //    {
        //        char c = charInfo.sorted[i];
        //        for (int j = 0; j < charInfo.counts[c]; j += 2)
        //        {
        //            sb.Append(c);
        //        }
        //    }
        //    return sb.ToString();
        //}

        //private static string reverse(string s)
        //{
        //    char[] a = s.ToCharArray();
        //    Array.Reverse(a);
        //    return new string(a);
        //}

        //private static bool mergesTo(string s1, string s2, string target)
        //{
        //    return mergesTo(s1, 0, s2, 0, target);
        //}

        //private static bool mergesTo(string s1, int x, string s2, int y, string target)
        //{
        //    if (x == s1.Length && y == s2.Length)
        //    {
        //        return true;
        //    }

        //    bool success = false;
        //    if (x < s1.Length && s1[x] == target[x + y])
        //    {
        //        success = mergesTo(s1, x + 1, s2, y, target);
        //    }
        //    if (!success)
        //    {
        //        if (y < s2.Length && s2[y] == target[x + y])
        //        {
        //            success = mergesTo(s1, x, s2, y + 1, target);
        //        }
        //    }

        //    return success;
        //}
    }
}

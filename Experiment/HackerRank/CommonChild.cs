using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class CommonChild
    {
        public static int commonChild(string s1, string s2)
        {
            //return commonChild(s1, 0, s2, 0, 0);
            return lcs(s1, s2);
        }

        private static int lcs(string s1, string s2)
        {
            int[,] cache = FillCache(s1, s2);
            return cache[s1.Length, s2.Length];
        }

        private static int[,] FillCache(string s1, string s2)
        {
            int x = s1.Length;
            int y = s2.Length;
            int[,] cache = new int[x + 1, y + 1];
            for (int i = 0; i <= x; i++)
            {
                for (int j = 0; j <= y; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        cache[i, j] = 0;
                    }
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        cache[i, j] = 1 + cache[i - 1, j - 1];
                    }
                    else
                    {
                        cache[i, j] = Math.Max(cache[i - 1, j], cache[i, j - 1]);
                    }
                }
            }
            return cache;
        }

        public static string lcsString(string s1, string s2)
        {
            int[,] cache = FillCache(s1, s2);
            return backtrack(s1, s2, cache);
        }

        private static string backtrack(string s1, string s2, int[,] cache)
        {
            return backtrack(s1, s2, cache, s1.Length, s2.Length);
        }

        private static string backtrack(string s1, string s2, int[,] cache, int x, int y)
        {
            if (x == 0 || y == 0)
            {
                return string.Empty;
            }
            else
            {
                if (s1[x-1] == s2[y-1])
                {
                    return backtrack(s1, s2, cache, x - 1, y - 1) + s1[x - 1];
                }
                else if (cache[x-1,y] > cache[x,y-1])
                {
                    return backtrack(s1, s2, cache, x - 1, y);
                }
                else
                {
                    return backtrack(s1, s2, cache, x, y - 1);
                }
            }
        }

        // From current x and y position
        // Find letter 'c' in s1 and s2 where s1.IndexOf(c) + s2.IndexOf(c) is minimal.
        // Add 1 to childLength and repeat from new x and y position.

        private static int commonChild(string s1, int x, string s2, int y, int childLength)
        {
            HashSet<char> s1Chars = new HashSet<char>();
            HashSet<char> s2Chars = new HashSet<char>();
            Dictionary<char, int> s1CharsOffsets = new Dictionary<char, int>();
            Dictionary<char, int> s2CharsOffsets = new Dictionary<char, int>();
            int idx = 0;
            int xi = x;
            int yi = y;
            while (xi < s1.Length || yi < s2.Length)
            {
                if (xi < s1.Length)
                {
                    char s1char = s1[xi];
                    if (!s1CharsOffsets.ContainsKey(s1char))
                    {
                        s1CharsOffsets[s1char] = idx;
                        s1Chars.Add(s1char);
                    }
                }

                if (yi < s2.Length)
                {
                    char s2char = s2[yi];
                    if (!s2CharsOffsets.ContainsKey(s2char))
                    {
                        s2CharsOffsets[s2char] = idx;
                        s2Chars.Add(s2char);
                    }
                }

                if (s1Chars.Overlaps(s2Chars))
                {
                    s1Chars.IntersectWith(s2Chars);
                    int minDistance = int.MaxValue;
                    int s1Offset = 0;
                    int s2Offset = 0;
                    foreach (char c in s1Chars)
                    {
                        int distance = s1CharsOffsets[c] + s2CharsOffsets[c];
                        if (distance < minDistance)
                        {
                            s1Offset = s1CharsOffsets[c];
                            s2Offset = s2CharsOffsets[c];
                        }
                    }

                    return commonChild(s1, x + s1Offset + 1, s2, y + s2Offset + 1, childLength + 1);
                }
                idx++;
                xi++;
                yi++;
            }

            return childLength;
        }

        // TOO SLOW
        //private static int commonChild(string s1, int x, string s2, int y, int childLength)
        //{
        //    if (x == s1.Length || y == s2.Length)
        //    {
        //        return childLength;
        //    }

        //    int max = int.MinValue;

        //    if (s1[x] != s2[y])
        //    {
        //        // try deleting the first char of s1
        //        max = Math.Max(max, commonChild(s1, x+1, s2, y, childLength));

        //        // try deleting the first char of s2
        //        max = Math.Max(max, commonChild(s1, x, s2, y+1, childLength));

        //        // try deleting the first char of s1 and s2
        //        max = Math.Max(max, commonChild(s1, x + 1, s2, y + 1, childLength));
        //    }
        //    else
        //    {
        //        // add length of matching run and recurse on remainder
        //        int matchLen = 0;
        //        while (x < s1.Length && y < s2.Length && s1[x++] == s2[y++])
        //        {
        //            matchLen++;
        //        }
        //        max = Math.Max(max, commonChild(s1, x, s2, y, childLength + matchLen));
        //    }

        //    return max;
        //}

        //private static int commonChild(string s1, int x, string s2, int y, int childLength)
        //{
        //    if (x == s1.Length || y == s2.Length)
        //    {
        //        return childLength;
        //    }

        //    int max = int.MinValue;

        //    if (s1[x] != s2[y])
        //    {
        //        // find matching run by iterating x
        //        int xMatchIdx = x;
        //        int xMatchLen = 0;
        //        int yIdx = y;
        //        while (xMatchIdx < s1.Length && s1[xMatchIdx] != s2[yIdx])
        //        {
        //            xMatchIdx++;
        //        }
        //        while (xMatchIdx < s1.Length && yIdx < s2.Length && s1[xMatchIdx++] == s2[yIdx++])
        //        {
        //            xMatchLen++;
        //        }
        //        max = Math.Max(max, commonChild(s1, xMatchIdx, s2, yIdx, childLength + xMatchLen));

        //        // find matching run by iterating y
        //        int yMatchIdx = y;
        //        int yMatchLen = 0;
        //        int xIdx = x;
        //        while (yMatchIdx < s2.Length && s2[yMatchIdx] != s1[xIdx])
        //        {
        //            yMatchIdx++;
        //        }
        //        while (yMatchIdx < s2.Length && xIdx < s1.Length && s2[yMatchIdx++] == s1[xIdx++])
        //        {
        //            yMatchLen++;
        //        }
        //        max = Math.Max(max, commonChild(s1, xIdx, s2, yMatchIdx, childLength + yMatchLen));

        //        // try tail of both
        //        max = Math.Max(max, commonChild(s1, x + 1, s2, y + 1, childLength));
        //    }
        //    else
        //    {
        //        // measure matching run
        //        int matchLen = 0;
        //        while (x < s1.Length && y < s2.Length && s1[x++] == s2[y++])
        //        {
        //            matchLen++;
        //        }
        //        max = Math.Max(max, commonChild(s1, x, s2, y, childLength + matchLen));
        //    }

        //    return max;
        //}
    }
}

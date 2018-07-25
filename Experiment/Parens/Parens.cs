using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Experiment.Parens
{
	public class Parens
	{
		public static List<string> GetParens(int n)
		{
			if (n < 0)
			{
				return null;
			}

			if (n == 0)
			{
				return new List<string>() { "" };
			}

			List<string> result = new List<string>();
			foreach (string s in GetParens(n - 1))
			{
				result.Add("(" + s + ")");
				if (s.Length > 0)
				{
					result.Add("()" + s);
					if (MaxDepth(s) > 1)
					{
						result.Add(s + "()");
					}
				}
			}

			return result;
		}

		private static int MaxDepth(string s)
		{
			int maxDepth = 0;
			int currentDepth = 0;
			foreach (char c in s)
			{
				if (c == '(')
				{
					currentDepth++;
				}
				else
				{
					 currentDepth--;
				}
				maxDepth = Math.Max(maxDepth, currentDepth);
			}

			return maxDepth;
		}

		private class RecursionCount
		{
			public long count;
		}

		public class ParensResult
		{
			public long recursionCount;
			public List<string> list;
		}


		public static ParensResult GenerateParens(int count)
		{
			if (count < 0)
			{
				return null;
			}

			RecursionCount rc = new RecursionCount();
			char[] str = new char[count*2];
			List<string> list = new List<string>();
			AddParen(list, count, count, str, 0, rc);
			return new ParensResult()
			{
				list = list,
				recursionCount = rc.count
			};
		}

		private static void AddParen(List<string> list, 
			int leftRem, int rightRem, char[] str, int index, RecursionCount rc)
		{
			rc.count++;

			if (leftRem < 0 || rightRem < leftRem)
			{
				// invalid state
				return;
			}

			if (leftRem == 0 && rightRem == 0)
			{
				list.Add(new string(str));
				return;
			}

			str[index] = '(';
			AddParen(list, leftRem - 1, rightRem, str, index + 1, rc);

			str[index] = ')';
			AddParen(list, leftRem, rightRem - 1, str, index + 1, rc);
		}


		public static ParensResult GenerateParens2Count(int count)
		{
			if (count < 0)
			{
				return null;
			}

			RecursionCount rc = new RecursionCount();
			char[] str = new char[count * 2];
			List<string> list = new List<string>();
			AddParen2Count(list, count, 0, str, 0, rc);
			return new ParensResult()
			{
				list = list,
				recursionCount = rc.count
			};
		}

		private static void AddParen2Count(List<string> list, 
			int leftRemaining, int rightAvailable, char[] str, int index, RecursionCount rc)
		{
			rc.count++;

			if (leftRemaining == 0 && rightAvailable == 0)
			{
				list.Add(new string(str));
				return;
			}

			if (leftRemaining > 0)
			{
				str[index] = '(';
				AddParen2Count(list, leftRemaining - 1, rightAvailable + 1, str, index + 1, rc);
			}

			if (rightAvailable > 0)
			{
				str[index] = ')';
				AddParen2Count(list, leftRemaining, rightAvailable - 1, str, index + 1, rc);
			}
		}

		public static List<string> GenerateParens2(int count)
		{
			if (count < 0)
			{
				return null;
			}

			char[] str = new char[count * 2];
			List<string> list = new List<string>();
			AddParen2(list, count, 0, str, 0);
			return list;
		}

		private static void AddParen2(List<string> list,
			int leftRemaining, int rightAvailable, char[] str, int index)
		{
			if (leftRemaining == 0 && rightAvailable == 0)
			{
				list.Add(new string(str));
				return;
			}

			if (leftRemaining > 0)
			{
				str[index] = '(';
				AddParen2(list, leftRemaining - 1, rightAvailable + 1, str, index + 1);
			}

			if (rightAvailable > 0)
			{
				str[index] = ')';
				AddParen2(list, leftRemaining, rightAvailable - 1, str, index + 1);
			}
		}

	}
}

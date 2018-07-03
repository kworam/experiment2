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

		public static List<string> GetParensBook(int n)
		{
			if (n < 0)
			{
				return null;				
			}

			List<string> result = new List<string>();
			char[] parens = new char[n*2];
			int index = 0;
			GetParensBookInternal(index, parens, n, n, result);
			return result;
		}

		private static void GetParensBookInternal(
			int index, char[] parens, int leftRemaining, int rightRemaining, List<string> result)
		{
			if (leftRemaining < 0 || rightRemaining < 0)
			{
				// error
				return;
			}

			if (leftRemaining == 0 && rightRemaining == 0)
			{
				result.Add(new string(parens));
				return;
			}

			parens[index] = '(';
			GetParensBookInternal(index + 1, parens, leftRemaining - 1, rightRemaining, result);

			parens[index] = ')';
			GetParensBookInternal(index + 1, parens, leftRemaining, rightRemaining - 1, result);
		}


		public static List<string> GetParensBookKevin(int n)
		{
			if (n < 0)
			{
				return null;
			}

			List<string> result = new List<string>();
			char[] parens = new char[n * 2];
			int index = 0;
			int leftRemaining = n;
			int rightAvailable = 0;
			GetParensBookInternalKevin(index, parens, leftRemaining, rightAvailable, result);
			return result;
		}

		private static void GetParensBookInternalKevin(
			int index, char[] parens, int leftRemaining, int rightAvailable, List<string> result)
		{
			if (leftRemaining == 0 && rightAvailable == 0)
			{
				result.Add(new string(parens));
				return;
			}

			if (leftRemaining > 0)
			{
				parens[index] = '(';
				GetParensBookInternalKevin(index + 1, parens, leftRemaining - 1, rightAvailable + 1, result);
			}

			if (rightAvailable > 0)
			{
				parens[index] = ')';
				GetParensBookInternalKevin(index + 1, parens, leftRemaining, rightAvailable - 1, result);
			}
		}
	}
}

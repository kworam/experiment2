using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Experiment.HackerRank
{
	class MinimumBribeProblem
	{
		public static void minimumBribes2(int[] q)
		{
			if (TooChaotic(q))
			{
				Console.WriteLine("Too chaotic");
				return;
			}

			//int numSwaps = GetMinSwapsBubbleSort(q);
			int numSwaps = GetMinSwapsCount(q);

			//Console.WriteLine(string.Join(",", q));

			Console.WriteLine(numSwaps);
		}

		private static int GetMinSwapsCount(int[] q)
		{
			int numSwaps = 0;
			for (int i = 0; i < q.Length; i++)
			{
				int val = q[i];
				numSwaps += val - 1 - i;
			}
			return numSwaps;
		}


		private static int GetMinSwapsBubbleSort(int[] q)
		{
			int numSwaps = 0;
			for (int i = 0; i < q.Length; i++)
			{
				for (int j = i + 1; j < q.Length; j++)
				{
					if (q[i] > q[j])
					{
						Swap(q, i, j);
						numSwaps++;
					}
				}
			}
			return numSwaps;
		}

		private static bool TooChaotic(int[] q)
		{
			for (int i = 0; i < q.Length; i++)
			{
				if (q[i] - 1 - i > 2)
				{
					return true;
				}
			}
			return false;
		}

		public static void minimumBribes(int[] q)
		{
			State state = new State(q.Length);
			tryAllBribePerms(q, state);
			Console.WriteLine(state.minBribes == int.MaxValue ? "Too chaotic" : state.minBribes.ToString());
		}

		private class State
		{
			public readonly Dictionary<int, int> swapsRemaining = new Dictionary<int, int>();
			public int minBribes = int.MaxValue;
			public int currentBribes;
			public State(int length)
			{
				for (int i = 1; i <= length; i++)
				{
					swapsRemaining[i] = 2;
				}
			}
		}

		static void tryAllBribePerms(int[] q, State state)
		{
			int lastSwapIndex = -1;
			while (true)
			{
				int swapIndex = GetSwapIndex(q, lastSwapIndex + 1);
				if (swapIndex < 0)
				{
					if (lastSwapIndex < 0)
					{
						// q is completely sorted
						state.minBribes = Math.Min(state.minBribes, state.currentBribes);
					}

					return;
				}

				if (DoSwap(q, swapIndex, state))
				{
					tryAllBribePerms(q, state);
					UndoSwap(q, swapIndex, state);
					lastSwapIndex = swapIndex;
				}
				else
				{
					// we cannot do the swap because it violates constraints
					return;
				}
			}
		}

		static int GetSwapIndex(int[] q, int start)
		{
			for (int i = start; i < q.Length - 1; i++)
			{
				if (q[i] > q[i + 1])
				{
					return i;
				}
			}

			return -1;
		}

		static bool DoSwap(int[] q, int i, State state)
		{
			int val = q[i];
			if (state.swapsRemaining[val] == 0)
			{
				return false;
			}

			state.swapsRemaining[val]--;
			state.currentBribes++;
			Swap(q, i, i + 1);
			return true;
		}

		static void UndoSwap(int[] q, int i, State state)
		{
			int val = q[i+1];
			state.swapsRemaining[val]++;
			state.currentBribes--;
			Swap(q, i, i + 1);
		}

		static void Swap(int[] q, int x, int y)
		{
			int temp = q[x];
			q[x] = q[y];
			q[y] = temp;
		}
	}
}

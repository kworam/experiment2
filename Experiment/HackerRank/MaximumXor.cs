using System.Collections.Generic;
using Experiment.Utility;

namespace Experiment.HackerRank
{
    public class MaximumXor
    {
        public static int[] maxXor(int[] arr, int[] queries)
        {
            TrieNode index = BuildTrie(arr);

            int[] result = new int[queries.Length];
            for (int i = 0; i < queries.Length; i++)
            {
                result[i] = getMaxXor(index, queries[i]);
            }
            return result;
        }

        private static int getMaxXor(TrieNode index, int q)
        {
            return getBestValue(index, q, 31, 0) ^ q;
        }

        private static int getBestValue(TrieNode index, int q, int bitpos, int bestValue)
        {
            if (bitpos < 0)
            {
                return bestValue;
            }

            TrieNode next;
            bool qbit = BitOperations.GetBit(q, bitpos);
            if (qbit)
            {
                if (index.children.ContainsKey(false))
                {
                    BitOperations.ClearBit(ref bestValue, bitpos);
                    next = index.children[false];
                }
                else
                {
                    BitOperations.SetBit(ref bestValue, bitpos);
                    next = index.children[true];
                }
            }
            else // qbit == false
            {
                if (index.children.ContainsKey(true))
                {
                    BitOperations.SetBit(ref bestValue, bitpos);
                    next = index.children[true];
                }
                else
                {
                    BitOperations.ClearBit(ref bestValue, bitpos);
                    next = index.children[false];
                }
            }

            return getBestValue(next, q, bitpos - 1, bestValue);
        }

        private class TrieNode
        {
            public Dictionary<bool, TrieNode> children = new Dictionary<bool, TrieNode>();

            public void Insert(int n)
            {
                Insert(n, 31);
            }

            private void Insert(int n, int bitpos)
            {
                if (bitpos < 0)
                {
                    return;
                }

                bool bit = BitOperations.GetBit(n, bitpos);
                if (!children.ContainsKey(bit))
                {
                    children[bit] = new TrieNode();
                }
                children[bit].Insert(n, bitpos - 1);
            }
        }

        private static TrieNode BuildTrie(int[] arr)
        {
            TrieNode root = new TrieNode();
            for (int i = 0; i < arr.Length; i++)
            {
                root.Insert(arr[i]);
            }
            return root;
        }
    }
}

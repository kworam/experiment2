using System;
using System.Collections.Generic;

namespace ExperimentUnitTest.HackerRank
{
	internal class MaxFriendCircle
	{
		public static int[] maxCircle(int[][] queries)
		{
			int maxFriendCircle = int.MinValue;
			int[] maxes = new int[queries.Length];
			Dictionary<int, UnionFindNode> people = new Dictionary<int, UnionFindNode>();
			int j = 0;
			for (int i = 0; i < queries.Length; i++)
			{
				UnionFindNode p1 = AddPerson(queries[i][0], people);
				UnionFindNode p2 = AddPerson(queries[i][1], people);
				UnionFindNode p1Root = p1.GetRoot();
				UnionFindNode p2Root = p2.GetRoot();
				if (p1Root.id != p2Root.id)
				{
					UnionFindNode newRoot = Union(p1Root, p2Root);
					maxFriendCircle = Math.Max(maxFriendCircle, newRoot.size);
				}

				maxes[i] = maxFriendCircle;
			}

			return maxes;
		}

		private class UnionFindNode
		{
			public int id;
			public int size;
			public UnionFindNode parent;

			public UnionFindNode(int id)
			{
				this.id = id;
				this.size = 1;
				this.parent = this;
			}

			public UnionFindNode GetRoot()
			{
				UnionFindNode root = this;
				while (root.parent != root)
				{
					root = root.parent;
				}
				return root;
			}

			public override string ToString()
			{
				return string.Format("{0}_{1}", this.id,this.parent.id);
			}
		}

		private static UnionFindNode AddPerson(int id, Dictionary<int, UnionFindNode> people)
		{
			if (!people.ContainsKey(id))
			{
				people[id] = new UnionFindNode(id);
			}
			return people[id];
		}

		private static UnionFindNode Union(UnionFindNode n1, UnionFindNode n2)
		{
			UnionFindNode smaller = n1.size < n2.size ? n1 : n2;
			UnionFindNode larger = n1 == smaller ? n2 : n1;
			smaller.parent = larger;
			larger.size += smaller.size;
			return larger;
		}
	}
}

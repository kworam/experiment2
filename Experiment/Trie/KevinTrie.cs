using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiment
{
    public class KevinTrie : Trie
    {
        private class Node : TrieNode
        {
            private Dictionary<char, Node> children;

            public Dictionary<char, Node> Children
            {
                get
                {
                    if (children == null)
                    {
                        children = new Dictionary<char, Node>();
                    }

                    return children;
                }
            }

            public int Value { get; set; }

            public Node GetOrCreateChildNodeForKey(char c)
            {
                if (!Children.ContainsKey(c))
                {
                    Children[c] = new Node();
                }

                return GetChildNodeForKey(c);
            }

            public Node GetChildNodeForKey(char c)
            {
                return Children[c];
            }

            public bool IsLeaf
            {
                get { return children == null; }
            }
        }

        private int numKeys = 0;

        private readonly Node rootNode = new Node();

        public void Insert(string key, int value)
        {
            InternalInsert(rootNode, key.ToCharArray(), keyIndex: 0, value: value);
            numKeys++;
        }

        public int Get(string key)
        {
            return InternalGet(rootNode, key.ToCharArray(), keyIndex: 0);
        }

        public int Count()
        {
            return numKeys;
        }

        public void Traverse(TraversalType traversalType, TrieVisitor visitor)
        {
            if (traversalType == TraversalType.InOrder)
            {
                throw new NotImplementedException("InOrder traversal not implemented yet.");
            }
            
            InternalTraverse(rootNode, traversalType, visitor);
        }

        private void InternalTraverse(Node node, TraversalType traversalType, TrieVisitor visitor)
        {
            if (traversalType == TraversalType.PreOrder && node.IsLeaf)
            {
                visitor.Visit(node);
            }

            char[] sortedChildrenKeys = node.Children.Keys.ToArray();
            Array.Sort(sortedChildrenKeys);
            foreach (char c in sortedChildrenKeys)
            {
                Node childNode = node.Children[c];
                InternalTraverse(childNode, traversalType, visitor);
            }

            if (traversalType == TraversalType.PostOrder && node.IsLeaf)
            {
                visitor.Visit(node);
            }
        }

        private int InternalGet(Node node, char[] keyArray, int keyIndex)
        {
            if (keyIndex == keyArray.Length)
            {
                return node.Value;
            }
            else
            {
                Node childNode = node.GetChildNodeForKey(keyArray[keyIndex]);
                return InternalGet(childNode, keyArray, keyIndex + 1);
            }
        }

        private void InternalInsert(Node node, char[] keyArray, int keyIndex, int value)
        {
            if (keyIndex == keyArray.Length)
            {
                node.Value = value;
            }
            else
            {
                Node childNode = node.GetOrCreateChildNodeForKey(keyArray[keyIndex]);
                InternalInsert(childNode, keyArray, keyIndex + 1, value);
            }
        }
    }
}

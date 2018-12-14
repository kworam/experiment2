using System;
using System.Collections.Generic;

namespace Experiment.CartesianTree
{
    public class Tree
    {
        private class Node
        {
            public Node left;
            public Node right;
            public Node parent;
            public int value;

            public Node(int value, Node parent)
            {
                this.value = value;
                this.parent = parent;
            }

            public Node Insert(int value)
            {
                if (this.value < value)
                {
                    // this node is the parent of the new node
                    Node newNode = new Node(value, this);
                    newNode.left = this.right;
                    if (this.right != null)
                    {
                        this.right.parent = newNode;
                    }
                    this.right = newNode;
                    return newNode;
                }
                else
                {
                    if (this.parent == null)
                    {
                        // we have reached the root of the tree
                        Node newNode = new Node(value, null);
                        newNode.left = this;
                        this.parent = newNode;
                        return newNode;
                    }
                    else
                    {
                        return this.parent.Insert(value);
                    }
                }
            }

            public override string ToString()
            {
                //return string.Format("l:{0} r:{1} p:{2} v:{3}",
                //    left == null ? "NULL" : left.ToString(),
                //    right == null ? "NULL" : right.ToString(),
                //    parent == null ? "NULL" : parent.ToString(),
                //    value);
                return string.Format("v:{0}", value);
            }
        }

        private Node root;
        private Node last;
        private List<Node> nodes = new List<Node>();

        public void Insert(int value)
        {
            if (last == null)
            {
                last = new Node(value, null);
            }
            else
            {
                last = last.Insert(value);
            }

            if (last.parent == null)
            {
                root = last;
            }

            nodes.Add(last);
        }

        public int Count()
        {
            return nodes.Count;
        }

        public int GetMinimumForRange(int x, int y)
        {
            if (x < 0 || x >= Count())
            {
                throw new ArgumentOutOfRangeException("x is out of range");
            }
            if (y < 0 || y >= Count())
            {
                throw new ArgumentOutOfRangeException("y is out of range");
            }
            if (x > y)
            {
                int temp = x;
                y = x;
                x = temp;
            }

            Node lca = GetLowestCommonAncestor(nodes[x], nodes[y]);
            return lca.value;
        }

        public List<int> Traverse()
        {
            List<int> result = new List<int>();
            Traverse(root, result);
            return result;
        }

        private void Traverse(Node node, List<int> result)
        {
            if (node == null) return;

            Traverse(node.left, result);
            result.Add(node.value);
            Traverse(node.right, result);
        }

        private Node GetLowestCommonAncestor(Node x, Node y)
        {
            Node xStart = x;
            Node yStart = y;
            while (x != y)
            {
                x = x.parent; if (x == null) x = yStart;
                y = y.parent; if (y == null) y = xStart;
            }
            return x;
        }

    }
}

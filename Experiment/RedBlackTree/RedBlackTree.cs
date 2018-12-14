using System;
namespace Experiment.RedBlackTree
{
    enum Color
    {
        Red,
        Black
    }

    public class Tree
    {
        private class Node
        {
            public Color color;
            public Node parent;
            public Node left;
            public Node right;
            public int value;

            public Node(int value, Node parent)
            {
                this.parent = parent;
                this.value = value;
            }

            public void Insert(int value)
            {
                if (value < this.value)
                {
                    if (this.left == null)
                    {
                        this.left = new Node(value, this);
                    }
                    else
                    {
                        this.left.Insert(value);
                    }
                }
                else
                {
                    if (this.right == null)
                    {
                        this.right = new Node(value, this);
                    }
                    else
                    {
                        this.right.Insert(value);
                    }
                }
            }

            public Node Find(int value)
            {
                if (value == this.value)
                {
                    return this;
                }
                else if (value < this.value)
                {
                    if (this.left == null)
                    {
                        return null;
                    }
                    return this.left.Find(value);
                }
                else // value > this.value
                {
                    if (this.right == null)
                    {
                        return null;
                    }
                    return this.right.Find(value);
                }
            }

            public override string ToString()
            {
                return string.Format("v:{0}", value);
            }
        }

        private Node root;

        public void Insert(int value)
        {
            if (root == null)
            {
                root = new Node(value, null);
            }
            else
            {
                root.Insert(value);
            }
        }

        public void RotateLeft(int value)
        {
            Node n = Find(value);
            if (n == null)
            {
                throw new Exception(string.Format("value {0} not found", value));
            }

            RotateLeft(n);
        }

        private void RotateLeft(Node n)
        {
            Node rc = n.right;
            if (rc == null)
            {
                throw new Exception(string.Format("Node {0} has no right child, so cannot rotate left", n));
            }

            // fix n and rc parent pointers
            Node p = n.parent;
            rc.parent = p;
            n.parent = rc;

            // exchange subtrees between n and rc
            // NOTE: values in rc.left are all smaller than rc and greater than n
            // so rc.left can become new right child of n
            Node rcLeft = rc.left;
            rc.left = n;
            n.right = rcLeft;
            if (rcLeft != null)
            {
                rcLeft.parent = n;
            }

            // fix parent's child pointers
            if (p == null)
            {
                root = rc;
            }
            else if (n == p.left)
            {
                p.left = rc;
            }
            else if (n == p.right)
            {
                p.right = rc;
            }
        }

        private Node Find(int value)
        {
            if (root == null)
            {
                return null;
            }

            return root.Find(value);
        }
    }
}

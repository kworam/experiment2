using System;

namespace Experiment.HackerRank
{
    public class FraudAlertNotification
    {
        // Complete the activityNotifications function below.
        public static int activityNotifications(int[] expenditure, int d)
        {
            int numNotifications = 0;
            BST bst = new BST();
            for (int i = 0; i < d; i++)
            {
                int cex = expenditure[i];
                bst.Insert(cex);
            }
            for (int i = d; i < expenditure.Length; i++)
            {
                int cex = expenditure[i];
                if (cex >= 2 * bst.GetMedian())
                {
                    numNotifications++;
                }
                bst.DeleteMin();
                bst.Insert(cex);
            }
            return numNotifications;
        }

        private class Node
        {
            public Node parent;
            public Node left;
            public Node right;
            public int val;

            public Node(int val, Node parent)
            {
                this.val = val;
                this.parent = parent;
            }

            public void Insert(int val, Node parent)
            {
                if (this.val > val)
                {
                    if (this.left == null)
                    {
                        this.left = new Node(val, this);
                    }
                    else
                    {
                        this.left.Insert(val, this);
                    }
                }
                else
                {
                    if (this.right == null)
                    {
                        this.right = new Node(val, this);
                    }
                    else
                    {
                        this.right.Insert(val, this);
                    }
                }
            }

            public Node GetPredecessor()
            {
                // get largest node smaller than this node
                if (this.left != null)
                {
                    return GetMaxNode(this.left);
                }
                else if (this.parent.right == this)
                {
                    return this.parent;
                }
                else
                {
                    throw new Exception(
                    "GetPredecessor returning null:  val=" + this.val + " this.parent=" + this.parent);                 
                    //return null;
                }
            }

            private Node GetMaxNode(Node n)
            {
                while (n.right != null)
                {
                    n = n.right;
                }
                return n;
            }

            public Node GetSuccessor()
            {
                // get smallest node larger than this node
                if (this.right != null)
                {
                    return GetMinNode(this.right);
                }
                else if (this.parent.left == this)
                {
                    return this.parent;
                }
                else
                {
                    throw new Exception(
                    "GetSuccessor returning null:  val=" + this.val + " this.parent=" + this.parent);              
                    //return null;
                }
            }

            private Node GetMinNode(Node n)
            {
                while (n.left != null)
                {
                    n = n.left;
                }
                return n;
            }
        }

        private class BST
        {
            private Node leftMedian;
            private Node rightMedian;
            private Node root;
            private int numNodes = 0;

            public double GetMedian()
            {
                if (leftMedian == null)
                {
                    return -1;
                }
                return (leftMedian.val + rightMedian.val) / 2.0;
            }

            public void Insert(int val)
            {
                if (root == null)
                {
                    root = new Node(val, null);
                }
                else
                {
                    root.Insert(val, root);
                }
                UpdateAfterInsert(val);
            }

            public void DeleteMin()
            {
                if (root == null)
                {
                    return;
                }

                Node current = root;
                while (current.left != null)
                {
                    current = current.left;
                }

                if (current == root)
                {
                    root = root.right;
                    root.parent = null;
                }
                else
                {
                    current.parent.left = null;
                }
                UpdateAfterDeleteMin();
            }

            private void UpdateAfterInsert(int val)
            {
                numNodes++;
                if (numNodes == 1)
                {
                    leftMedian = rightMedian = root;
                }
                else if (leftMedian == rightMedian)
                {
                    if (val < leftMedian.val)
                    {
                        // 1 3 5,  insert 2
                        leftMedian = rightMedian.GetPredecessor();
                    }
                    else
                    {
                        // 1 3 5, insert 4
                        rightMedian = leftMedian.GetSuccessor();
                    }
                }
                else  // leftMedian != rightMedian
                {
                    //if (leftMedian == null) throw new Exception("leftMedian is null, numNodes=" + numNodes);
                    //if (rightMedian == null) throw new Exception("rightMedian is null, numNodes=" + numNodes);
                    if (val < leftMedian.val)
                    {
                        // 1 3 5 7, inset 2
                        rightMedian = leftMedian;
                    }
                    else if (val > rightMedian.val)
                    {
                        // 1 3 5 7, insert 6
                        leftMedian = rightMedian;
                    }
                    else
                    {
                        // 1 3 5 7, insert 4
                        leftMedian = rightMedian = leftMedian.GetSuccessor();
                    }
                }
            }

            private void UpdateAfterDeleteMin()
            {
                numNodes--;
                if (numNodes == 0)
                {
                    leftMedian = rightMedian = null;
                }
                else if (leftMedian == rightMedian)
                {
                    // 1 3 5,  delete 1
                    rightMedian = leftMedian.GetSuccessor();
                }
                else  // leftMedian != rightMedian
                {
                    // 1 3 5 7, delete 1
                    leftMedian = rightMedian;
                }
            }
        }
    }
}

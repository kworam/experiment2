using System;
using System.Collections.Generic;
using System.Text;

namespace Experiment.HackerRank
{
    public class ArrayManipulation
    {
        public static long arrayManipulation(int n, int[][] queries)
        {
            long[] da = new long[n + 1];
            for (int i=0; i<queries.Length; i++)
            {
                int[] query = queries[i];
                int a = query[0] ;
                int b = query[1];
                int k = query[2];

                da[a] += k;
                if (b+1 < da.Length)
                {
                    da[b + 1] -= k;
                }
            }

            long maxValue = 0;
            long accumulator = 0;
            for (int i=1; i<da.Length; i++)
            {
                accumulator += da[i];
                maxValue = Math.Max(maxValue, accumulator);
            }

            return maxValue;
        }

        public static long arrayManipulation2(int n, int[][] queries)
        {
            Ranges ranges = new Ranges(n);
            for (int i = 0; i < queries.Length; i++)
            {
                int[] query = queries[i];
                int a = query[0]-1;
                int b = query[1]-1;
                int k = query[2];

                ranges.DoOperation(a, b, k);
            }
            return ranges.maxValue;
        }

        private class Range
        {
            public Range(int start, int end, long value)
            {
                this.start = start;
                this.end = end;
                this.value = value;
            }

            public int start;
            public int end;
            public long value;
            public override string ToString()
            {
                return string.Format("Range: start={0} end={1} value={2}", this.start, this.end, this.value);
            }
        }

        private class Ranges
        {
            public BST rangesByStart;
            public int n;
            public long maxValue;
            private int numRanges;

            public Ranges(int n)
            {
                this.n = n;
                rangesByStart = new BST(0, n - 1, 0, null, this);
                this.maxValue = 0;
                this.numRanges = 1;
            }

            public void DoOperation(int a, int b, int k)
            {
                HashSet<Range> affectedRanges = GetAffectedRanges(a, b);
                foreach (Range ar in affectedRanges)
                {
                    // the range a, b overlaps this range (ar)
                    // cases:
                    // 1. a,b completely covers ar
                    // 2. a,b is contained within ar
                    // 3. a,b covers a left subset of this range
                    // 4. a,b covers a right subset of this range
                    if (a <= ar.start && b >= ar.end)
                    {
                        // 1. a,b completely covers ar
                        AddRange(ar.start, ar.end, ar.value + k);
                    }
                    else if (a > ar.start && b < ar.end)
                    {
                        // 2. a,b is contained within ar
                        AddRange(ar.start, a - 1, ar.value);
                        AddRange(a, b, ar.value + k);
                        AddRange(b+1, ar.end, ar.value);
                    }
                    else if (b < ar.end)
                    {
                        // 3. a,b covers a left subset of this range
                        AddRange(ar.start, b, ar.value + k);
                        AddRange(b+1, ar.end, ar.value);
                    }
                    else  // a > ar.start
                    {
                        // 4. a,b covers a right subset of this range
                        AddRange(ar.start, a-1, ar.value);
                        AddRange(a, ar.end, ar.value + k);
                    }
                    DeleteRange(ar);
                }
            }

            public void DeleteRange(Range r)
            {
                if (rangesByStart.range.start == r.start)
                {
                    if (rangesByStart.left == null)
                    {
                        rangesByStart = rangesByStart.right;
                        rangesByStart.parent = null;
                    }
                    else if (rangesByStart.right == null)
                    {
                        rangesByStart = rangesByStart.left;
                        rangesByStart.parent = null;
                    }
                    else
                    {
                        BST successor = BST.GetSuccessor(rangesByStart);
                        rangesByStart.range = successor.range;
                        successor.Delete();
                    }
                }
                else
                {
                    rangesByStart.Delete(r);
                }
                numRanges--;
                // don't have to update this.maxValue here 
            }

            void AddRange(int start, int end, long value)
            {
                if (end < start)
                {
                    return;
                }

                Range r = new Range(start, end, value);
                rangesByStart.Insert(r);
                this.maxValue = Math.Max(r.value, this.maxValue);
                this.numRanges++;
            }

            HashSet<Range> GetAffectedRanges(int a, int b)
            {
                return GetOverlapping(a, b);
            }
            private HashSet<Range> GetOverlapping(int start, int end)
            {
                BST startBST = rangesByStart.FindClosestToStart(start);
                HashSet<Range> result = new HashSet<Range>();

                result.UnionWith(startBST.GetOverlappingToLeft(start, end));
                result.UnionWith(startBST.GetOverlappingToRight(start, end));
                return result;
            }

            public override string ToString()
            {
                return GetString();
            }
            public string GetString()
            {
                ToArrayStringVisitor visitor = new ToArrayStringVisitor();
                BST.TraverseInOrder(rangesByStart, visitor);
                return string.Format("Ranges: maxValue={0} numRanges={1}  vector={2}", this.maxValue, this.numRanges, visitor.ToString());
            }

            private class ToArrayStringVisitor: I_BST_Visitor
            {
                StringBuilder sb = new StringBuilder();
                public void Visit(BST node)
                {
                    for (int i=node.range.start; i<=node.range.end; i++)
                    {
                        sb.Append(node.range.value);
                        sb.Append(" ");
                    }
                }

                public override string ToString()
                {
                    return sb.ToString();
                }
            }
        }

        private interface I_BST_Visitor
        {
            void Visit(BST node);
        }

        private class BST
        {
            public Range range;
            public BST left;
            public BST right;
            public BST parent;
            private Ranges ranges;

            public BST(int start, int end, int value, BST parent, Ranges ranges)
            {
                this.range = new Range(start, end, value);
                this.parent = parent;
                this.ranges = ranges;
            }

            public BST(Range r, BST parent, Ranges ranges)
            {
                this.range = r;
                this.parent = parent;
                this.ranges = ranges;
            }

            public void Insert(Range r)
            {
                if (r.start < range.start)
                {
                    if (left == null)
                    {
                        left = new BST(r, this, ranges);
                    }
                    else
                    {
                        left.Insert(r);
                    }
                }
                else
                {
                    if (right == null)
                    {
                        right = new BST(r, this, ranges);
                    }
                    else
                    {
                        right.Insert(r);
                    }
                }
            }

            public void Delete(Range r)
            {
                if (r.start < range.start)
                {
                    if (left == null)
                    {
                        return;        
                    }
                    left.Delete(r);
                }
                else if (r.start > range.start)
                {
                    if (right == null)
                    {
                        return;
                    }
                    right.Delete(r);
                }
                else  // range.start == r.start
                {
                    this.Delete();
                }
            }

            public void Delete()
            {
                if (parent == null)
                {
                    ranges.DeleteRange(range);
                }
                else
                {
                    if (left == null && right == null)
                    {
                        // deleting a leaf
                        parent.ReplaceChild(this, null);
                    }
                    else if (left == null)
                    {
                        parent.ReplaceChild(this, right);
                    }
                    else if (right == null)
                    {
                        parent.ReplaceChild(this, left);
                    }
                    else  // left and right are not null
                    {
                        BST successor = GetSuccessor(this);
                        this.range = successor.range;
                        successor.Delete();
                    }
                }
            }

            public BST FindClosestToStart(int start)
            {
                if (range.start == start)
                {
                    return this;
                }
                else if (range.start > start)
                {
                    if (left == null)
                    {
                        return this;
                    }
                    return left.FindClosestToStart(start);
                }
                else // range.start < start
                {
                    if (right == null)
                    {
                        return this;
                    }
                    return right.FindClosestToStart(start);
                }
            }

            private void ReplaceChild(BST existing, BST replacement)
            {
                if (existing == left)
                {
                    left = replacement;
                    if (left != null)
                    {
                        left.parent = this;
                    }
                }
                else // existing == right
                {
                    right = replacement;
                    if (right != null)
                    {
                        right.parent = this;
                    }
                }
            }

            public static BST GetSuccessor(BST node)
            {
                if (node == null)
                {
                    return null;
                }

                if (node.right != null)
                {
                    return GetMinNode(node.right);
                }

                return GetFirstGreaterAncestor(node);
            }

            public static BST GetPredecessor(BST node)
            {
                if (node == null)
                {
                    return null;
                }

                if (node.left != null)
                {
                    return GetMaxNode(node.left);
                }

                return GetFirstLesserAncestor(node);
            }

            public HashSet<Range> GetOverlappingToLeft(int start, int end)
            {
                HashSet<Range> result = new HashSet<Range>();
                BST current = this;
                while (current != null && current.range.end >= start)
                {
                    result.Add(current.range);
                    current = BST.GetPredecessor(current);
                }
                return result;
            }

            public HashSet<Range> GetOverlappingToRight(int start, int end)
            {
                HashSet<Range> result = new HashSet<Range>();
                BST current = this;
                while (current != null && current.range.start <= end)
                {
                    result.Add(current.range);
                    current = BST.GetSuccessor(current);
                }
                return result;
            }

            private static BST GetMinNode(BST node)
            {
                BST minNode = node;
                while (minNode.left != null)
                {
                    minNode = minNode.left;
                }
                return minNode;
            }

            private static BST GetMaxNode(BST node)
            {
                BST maxNode = node;
                while (maxNode.right != null)
                {
                    maxNode = maxNode.right;
                }
                return maxNode;
            }

            private static BST GetFirstGreaterAncestor(BST node)
            {
                while (node.parent != null && node.parent.right == node)
                {
                    node = node.parent;
                }
                return node.parent;
            }

            private static BST GetFirstLesserAncestor(BST node)
            {
                while (node.parent != null && node.parent.left == node)
                {
                    node = node.parent;
                }
                return node.parent;
            }

            public static void TraverseInOrder(BST node, I_BST_Visitor visitor)
            {
                if (node == null)
                {
                    return;
                }

                TraverseInOrder(node.left, visitor);
                visitor.Visit(node);
                TraverseInOrder(node.right, visitor);
            }

            public override string ToString()
            {
                return string.Format("BST: range={0}", this.range);
            }
        }
    }
}

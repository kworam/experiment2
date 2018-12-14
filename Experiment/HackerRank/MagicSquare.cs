using System;
using System.Collections.Generic;
using Experiment.Matrix;

namespace Experiment.HackerRank
{
    public class MagicSquare
    {
        public static int formingMagicSquare(int[][] s)
        {
            return magicSquareMinCost(s, 0);
        }

        private class Point
        {
            public Point(int row, int col)
            {
                this.row = row;
                this.col = col;
            }

            public int row;
            public int col;
        }

        enum Reflection
        {
            Rows,
            Cols,
            MainDiagonal,
            OffDiagonal
        }

        static int magicSquareMinCost(int[][] s, int currentCost)
        {
            Dictionary<int, List<Point>> points = getPoints(s);
            if (points.Count == 9 && isMagic(s))
            {
                return currentCost;
            }

            int minCost = int.MaxValue;
            for (int i = 1; i <= 9; i++)
            {
                if (points.ContainsKey(i))
                {
                    continue;
                }

                // i is a number not contained in the matrix
                // try swapping it in for each of the duplicate values
                foreach (int v in points.Keys)
                {
                    List<Point> locations = points[v];
                    if (locations.Count == 1)
                    {
                        continue;
                    }

                    foreach (Point p in locations)
                    {
                        int moveCost = Math.Abs(i - v);
                        s[p.row][p.col] = i;
                        minCost = Math.Min(minCost, magicSquareMinCost(s, currentCost + moveCost));
                        s[p.row][p.col] = v;
                    }
                }
            }
            return minCost;
        }

        static bool isMagic(int[][] s)
        {
            if (isTransformMagic(s)) return true;

            int[][] r = (int[][])clone(s);
            if (isTransformMagic(rotate90(r))) return true;
            if (isTransformMagic(rotate90(r))) return true;
            if (isTransformMagic(rotate90(r))) return true;

            if (isTransformMagic(reflect(s, Reflection.Rows))) return true;
            if (isTransformMagic(reflect(s, Reflection.Cols))) return true;
            if (isTransformMagic(reflect(s, Reflection.MainDiagonal))) return true;
            if (isTransformMagic(reflect(s, Reflection.OffDiagonal))) return true;

            return false;
        }

        static int[][] clone(int[][] s)
        {
            int[][] r = new int[s.Length][];
            for (int row=0; row<s.Length; row++)
            {
                r[row] = new int[s[row].Length];
                for (int col=0; col<s[row].Length; col++)
                {
                    r[row][col] = s[row][col];
                }
            }
            return r;
        }

        static int[][] rotate90(int[][] s)
        {
            MatrixRotation.Rotate(s, s.Length-1);
            return s;
        }

        static int[][] reflect(int[][] s, Reflection reflection)
        {
            int[][] r = clone(s);
            int tmp;
            switch (reflection)
            {
                case Reflection.Cols:
                    for (int row=0; row<s.Length; row++)
                    {
                        tmp = r[row][2];
                        r[row][2] = r[row][0];
                        r[row][0] = tmp;
                    }
                    break;
                case Reflection.Rows:
                    for (int col = 0; col < s.Length; col++)
                    {
                        tmp = r[2][col];
                        r[2][col] = r[0][col];
                        r[0][col] = tmp;
                    }
                    break;
                case Reflection.OffDiagonal:
                    tmp = r[0][0];
                    r[0][0] = r[2][2];
                    r[2][2] = tmp;

                    tmp = r[0][1];
                    r[0][1] = r[1][2];
                    r[1][2] = tmp;

                    tmp = r[1][0];
                    r[1][0] = r[2][1];
                    r[2][1] = tmp;
                    break;
                case Reflection.MainDiagonal:
                    tmp = r[2][0];
                    r[2][0] = r[0][2];
                    r[0][2] = tmp;

                    tmp = r[1][0];
                    r[1][0] = r[0][1];
                    r[0][1] = tmp;

                    tmp = r[2][1];
                    r[2][1] = r[1][2];
                    r[1][2] = tmp;
                    break;
            }

            return r;
        }

        static bool isTransformMagic(int[][] s)
        {
            int r = 0;
            int magicValue = s[r][0] + s[r][1] + s[r][2];

            for (r = 1; r < s.Length; r++)
            {
                if (s[r][0] + s[r][1] + s[r][2] != magicValue) return false;
            }
            for (int c = 0; c < s.Length; c++)
            {
                if (s[0][c] + s[1][c] + s[2][c] != magicValue) return false;
            }
            if (s[0][0] + s[1][1] + s[2][2] != magicValue) return false;
            if (s[0][2] + s[1][1] + s[2][0] != magicValue) return false;

            return true;
        }

        static Dictionary<int, List<Point>> getPoints(int[][] s)
        {
            Dictionary<int, List<Point>> points = new Dictionary<int, List<Point>>();
            for (int r = 0; r < s.Length; r++)
            {
                for (int c = 0; c < s[0].Length; c++)
                {
                    int val = s[r][c];
                    if (!points.ContainsKey(val))
                    {
                        points[val] = new List<Point>();
                    }
                    points[val].Add(new Point(r, c));
                }
            }

            return points;
        }
    }
}

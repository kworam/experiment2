using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class QueensAttack2
    {
        class Cell
        {
            public int row;
            public int col;

            public override string ToString()
            {
                return string.Format("r:{0},c={1}", row, col);
            }
        }

        enum Axis
        {
            Row,
            Col
        }

        enum Direction
        {
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }

        // Complete the queensAttack function below.
        public static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles)
        {
            Cell[] obstaclesSortedByCol = GetObstacleCells(obstacles);
            Array.Sort(obstaclesSortedByCol, (c1, c2) => c1.col.CompareTo(c2.col));
            Cell[] obstaclesSortedByRow = GetObstacleCells(obstacles);
            Array.Sort(obstaclesSortedByRow, (c1, c2) => c1.row.CompareTo(c2.row));

            Dictionary<Direction, int> results = new Dictionary<Direction, int>();
            int rq0 = r_q - 1;
            int cq0 = c_q - 1;
            GetAttackCellsByCol(obstaclesSortedByCol, results, rq0, cq0, n);
            GetAttackCellsByRow(obstaclesSortedByRow, results, rq0, cq0, n);
            return GetTotalCells(results, n, rq0, cq0);
        }

        private static Cell[] GetObstacleCells(int[][] obstacles)
        {
            Cell[] result = new Cell[obstacles.Length];
            for (int i = 0; i < obstacles.Length; i++)
            {
                result[i] = new Cell() { row = obstacles[i][0] - 1, col = obstacles[i][1] - 1 };
            }
            return result;
        }

        private static void GetAttackCellsByCol(
            Cell[] obstaclesSortedByCol, Dictionary<Direction, int> results, int rq0, int cq0, int n)
        {
            int closestObstacleIndex = BinSearch(obstaclesSortedByCol, cq0, Axis.Col);
            if (closestObstacleIndex < 0)
            {
                return;
            }

            int index = GetFirstIndexLessThan(obstaclesSortedByCol, Axis.Col, cq0, closestObstacleIndex);
            while (index >= 0 &&
                !(results.ContainsKey(Direction.W) && results.ContainsKey(Direction.NW)))
            {
                Cell obstacle = obstaclesSortedByCol[index];
                index--;
                int dist = Math.Abs(cq0 - obstacle.col);
                if (obstacle.row == rq0 && !results.ContainsKey(Direction.W))
                {
                    results[Direction.W] = dist - 1;
                }
                if (obstacle.row == rq0 + dist && !results.ContainsKey(Direction.NW))
                {
                    results[Direction.NW] = dist - 1;
                }
            }

            index = GetFirstIndexGreaterThan(obstaclesSortedByCol, Axis.Col, cq0, closestObstacleIndex);
            while (index < obstaclesSortedByCol.Length &&
                !(results.ContainsKey(Direction.E) && results.ContainsKey(Direction.SE)))
            {
                Cell obstacle = obstaclesSortedByCol[index];
                index++;
                int dist = Math.Abs(cq0 - obstacle.col);
                if (obstacle.row == rq0 && !results.ContainsKey(Direction.E))
                {
                    results[Direction.E] = dist - 1;
                }
                if (obstacle.row == rq0 - dist && !results.ContainsKey(Direction.SE))
                {
                    results[Direction.SE] = dist - 1;
                }
            }
        }

        private static void GetAttackCellsByRow(
            Cell[] obstaclesSortedByRow, Dictionary<Direction, int> results, int rq0, int cq0, int n)
        {
            int closestObstacleIndex = BinSearch(obstaclesSortedByRow, rq0, Axis.Row);
            if (closestObstacleIndex < 0)
            {
                return;
            }

            int index = GetFirstIndexGreaterThan(obstaclesSortedByRow, Axis.Row, rq0, closestObstacleIndex);
            while (index < obstaclesSortedByRow.Length &&
                !(results.ContainsKey(Direction.N) && results.ContainsKey(Direction.NE)))
            {
                Cell obstacle = obstaclesSortedByRow[index];
                index++;
                int dist = Math.Abs(rq0 - obstacle.row);
                if (obstacle.col == cq0 && !results.ContainsKey(Direction.N))
                {
                    results[Direction.N] = dist - 1;
                }
                if (obstacle.col == cq0 + dist && !results.ContainsKey(Direction.NE))
                {
                    results[Direction.NE] = dist - 1;
                }
            }

            index = GetFirstIndexLessThan(obstaclesSortedByRow, Axis.Row, rq0, closestObstacleIndex);
            while (index >= 0 &&
                !(results.ContainsKey(Direction.S) && results.ContainsKey(Direction.SW)))
            {
                Cell obstacle = obstaclesSortedByRow[index];
                index--;
                int dist = Math.Abs(rq0 - obstacle.row);
                if (obstacle.col == cq0 && !results.ContainsKey(Direction.S))
                {
                    results[Direction.S] = dist - 1;
                }
                if (obstacle.col == cq0 - dist && !results.ContainsKey(Direction.SW))
                {
                    results[Direction.SW] = dist - 1;
                }
            }
        }

        private static int GetFirstIndexGreaterThan(
            Cell[] cellsSorted,
            Axis axis,
            int target, 
            int index)
        {
            while (index < cellsSorted.Length)
            {
                int testVal = axis == Axis.Row ? cellsSorted[index].row : cellsSorted[index].col;
                if (testVal > target)
                {
                    break;
                }
                index++;
            }

            return index;
        }

        private static int GetFirstIndexLessThan(
            Cell[] cellsSorted,
            Axis axis,
            int target,
            int index)
        {
            while (index >= 0)
            {
                int testVal = axis == Axis.Row ? cellsSorted[index].row : cellsSorted[index].col;
                if (testVal < target)
                {
                    break;
                }
                index--;
            }

            return index;
        }

        private static int BinSearch(Cell[] cellsSorted, int val, Axis axis)
        {
            return BinSearch(cellsSorted, val, axis, 0, cellsSorted.Length - 1, -1);
        }

        private static int BinSearch(Cell[] cellsSorted, int val, Axis axis, int lo, int hi, int last)
        {
            if (lo > hi)
            {
                return last;
            }

            int mid = (lo + hi) / 2;
            Cell midCell = cellsSorted[mid];
            int midVal = axis == Axis.Row ? midCell.row : midCell.col;
            if (midVal == val)
            {
                return mid;
            }
            else if (midVal < val)
            {
                return BinSearch(cellsSorted, val, axis, mid+1, hi, mid);
            }
            else // midVal > val
            {
                return BinSearch(cellsSorted, val, axis, lo, mid-1, mid);
            }
        }

        private static int GetTotalCells(Dictionary<Direction, int> results, int n, int rq0, int cq0)
        {
            int total = 0;
            foreach (Direction d in (Direction[])Enum.GetValues(typeof(Direction)))
            {
                if (results.ContainsKey(d))
                {
                    total += results[d];
                }
                else
                {
                    switch (d)
                    {
                        case Direction.W:
                            total += cq0;
                            break;
                        case Direction.NW:
                            total += Math.Min(cq0, (n - 1) - rq0);
                            break;
                        case Direction.N:
                            total += (n - 1) - rq0;
                            break;
                        case Direction.NE:
                            total += Math.Min((n - 1) - cq0, (n - 1) - rq0);
                            break;
                        case Direction.E:
                            total += (n - 1) - cq0;
                            break;
                        case Direction.SE:
                            total += Math.Min((n - 1) - cq0, rq0);
                            break;
                        case Direction.S:
                            total += rq0;
                            break;
                        case Direction.SW:
                            total += Math.Min(cq0, rq0);
                            break;
                    }
                }
            }

            return total;
        }
    }
}

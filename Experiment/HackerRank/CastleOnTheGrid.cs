using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class CastleOnTheGrid
    {
        //enum Direction
        //{
        //    up,
        //    down,
        //    left,
        //    right
        //}



        //private class Node
        //{
        //    public int x;
        //    public int y;
        //    private string[] grid;
        //    public Dictionary<Direction, Node> adjacentNodes;

        //    public Node(string[] grid, int x, int y)
        //    {
        //        this.grid = grid;
        //        this.x = x;
        //        this.y = y;
        //    }

        //    public string GetKey()
        //    {
        //        return string.Format("{0}-{1}", x, y);
        //    }

        //    public Dictionary<Direction, Node> getAdjacentNodes()
        //    {
        //        if (adjacentNodes == null)
        //        {
        //            initializeAdjacentNodes();
        //        }
        //        return adjacentNodes;
        //    }

        //    private void initializeAdjacentNodes()
        //    {
        //        this.adjacentNodes = new Dictionary<Direction, Node>();
        //        if (this.x > 0 && grid[x - 1][y] != 'X')
        //        {
        //            adjacentNodes[Direction.up] = new Node(grid, x - 1, y);
        //        }
        //        if (this.x < grid.Length - 1 && grid[x + 1][y] != 'X')
        //        {
        //            adjacentNodes[Direction.down] = new Node(grid, x + 1, y);
        //        }
        //        if (this.y > 0 && grid[x][y - 1] != 'X')
        //        {
        //            adjacentNodes[Direction.left] = new Node(grid, x, y - 1);
        //        }
        //        if (this.y < grid.Length - 1 && grid[x][y + 1] != 'X')
        //        {
        //            adjacentNodes[Direction.right] = new Node(grid, x, y + 1);
        //        }
        //    }

        //    public override string ToString()
        //    {
        //        return GetKey();
        //    }
        //}

        private static int minTurns2(string[] grid, int startX, int startY, int goalX, int goalY)
        {
            int[][] score = new int[grid.Length][];
            int[][] visited = new int[grid.Length][];
            for (int i=0; i<grid.Length; i++)
            {
                score[i] = new int[grid.Length];
                visited[i] = new int[grid.Length];
                for (int j=0; j<grid[i].Length; j++)
                {
                    score[i][j] = grid[i][j] == 'X' ? -1 : int.MaxValue;
                }
            }

            Queue<Position> originsToTry = new Queue<Position>();

            Position start = new Position(startX, startY);
            originsToTry.Enqueue(start);

            score[startX][startY] = 0;
            while (originsToTry.Count > 0)
            {
                Position currentOrigin = originsToTry.Dequeue();
                if (visited[currentOrigin.x][currentOrigin.y] == 0)
                {
                    visited[currentOrigin.x][currentOrigin.y] = 1;
                    AddMoves(score, currentOrigin, originsToTry);
                }
            }
            return score[goalX][goalY];
        }

        private static void AddMoves(int[][] score, Position currentOrigin, Queue<Position> originsToTry)
        {
            int x = currentOrigin.x;
            int y = currentOrigin.y;
            int originScore = score[x][y];
            for (int i=x-1; i >= 0 && score[i][y] != -1; i--)
            {
                originsToTry.Enqueue(new Position(i, y));
                if (score[i][y] > originScore + 1) score[i][y] = originScore + 1;
            }
            for (int i = x + 1; i < score.Length && score[i][y] != -1; i++)
            {
                originsToTry.Enqueue(new Position(i, y));
                if (score[i][y] > originScore + 1) score[i][y] = originScore + 1;
            }
            for (int i = y - 1; i >= 0 && score[x][i] != -1; i--)
            {
                originsToTry.Enqueue(new Position(x, i));
                if (score[x][i] > originScore + 1) score[x][i] = originScore + 1;
            }
            for (int i = y + 1; i < score.Length && score[x][i] != -1; i++)
            {
                originsToTry.Enqueue(new Position(x, i));
                if (score[x][i] > originScore + 1) score[x][i] = originScore + 1;
            }
        }

        //private static int minTurns(string[] grid, int startX, int startY, int goalX, int goalY)
        //{
        //    return minTurns(grid, startX, startY, goalX, goalY, 0, (Direction)(-1), new Dictionary<string, int>());
        //}

        //private static string getPositionKey(int currX, int currY)
        //{
        //    return string.Format("{0}_{1}", currX, currY);
        //}

        //private static int minTurns(
        //    string[] grid, int currX, int currY, int goalX, int goalY, int turnsSoFar, Direction previousDirection, Dictionary<string, int> cache)
        //{
        //    if (outOfBounds(grid, currX, currY) || grid[currX][currY] == 'X')
        //    {
        //        return int.MaxValue;
        //    }

        //    string key = getPositionKey(currX, currY);
        //    if (cache.ContainsKey(key))
        //    {
        //        return cache[key];
        //    }

        //    if (currX == goalX && currY == goalY)
        //    {
        //        cache[key] = turnsSoFar;
        //        return turnsSoFar;
        //    }

        //    int mt = int.MaxValue;
        //    foreach (Direction d in (Direction[])Enum.GetValues(typeof(Direction)))
        //    {
        //        if (d == opposite(previousDirection))
        //        {
        //            continue;
        //        }

        //        mt = Math.Min(mt,
        //            minTurns(
        //                grid, 
        //                applyX(currX, d), applyY(currY, d), 
        //                goalX, goalY, 
        //                d == previousDirection ? turnsSoFar : turnsSoFar + 1, 
        //                d,
        //                cache));
        //    }

        //    cache[key] = mt;
        //    return mt;
        //}

        //private static int applyX(int currX, Direction d)
        //{
        //    if (d == Direction.left) return currX - 1;
        //    if (d == Direction.right) return currX + 1;
        //    return currX;
        //}

        //private static int applyY(int currY, Direction d)
        //{
        //    if (d == Direction.up) return currY - 1;
        //    if (d == Direction.down) return currY + 1;
        //    return currY;
        //}

        //private static Direction opposite(Direction d)
        //{
        //    switch(d)
        //    {
        //        case Direction.up: return Direction.down;
        //        case Direction.down: return Direction.up;
        //        case Direction.left: return Direction.right;
        //        case Direction.right: return Direction.left;
        //        default: return (Direction)(-2);
        //    }            
        //}

        //private static bool outOfBounds(string[] grid, int currX, int currY)
        //{
        //    return
        //        (currX < 0 || currX >= grid.Length)
        //        ||
        //        (currY < 0 || currY >= grid.Length);
        //}

        public static int minimumMoves(string[] grid, int startX, int startY, int goalX, int goalY)
        {
            return minTurns2(grid, startX, startY, goalX, goalY);

            //return minTurns(grid, startX, startY, goalX, goalY);

            //Node start = new Node(grid, startX, startY);
            //Node goal = new Node(grid, goalX, goalY);

            //List<DiscoveredNode> path = GetShortestPath(start, goal);
            //PrintGrid(grid, path);
            //return CountPathSegments(path);
        }

        //private class DiscoveredNode
        //{
        //    public DiscoveredNode(Node node, Node parentNode, Direction directionFromParent)
        //    {
        //        this.node = node;
        //        this.key = node.GetKey();
        //        if (parentNode != null)
        //        {
        //            this.parentKey = parentNode.GetKey();
        //            this.directionFromParent = directionFromParent;
        //        }
        //    }

        //    public string key;
        //    public string parentKey;
        //    public Direction directionFromParent;
        //    public Node node;

        //    public override string ToString()
        //    {
        //        return string.Format("{0} - {1} - {2}", this.key, this.directionFromParent, this.parentKey);
        //    }
        //}

        //private static List<DiscoveredNode> GetShortestPath(Node start, Node end)
        //{
        //    string endNodeKey = end.GetKey();

        //    Dictionary<string, DiscoveredNode> discoveredNodes =
        //        new Dictionary<string, DiscoveredNode>();
        //    discoveredNodes[start.GetKey()] = new DiscoveredNode(start, null, Direction.up);

        //    Queue<Node> nodesToExplore = new Queue<Node>();
        //    nodesToExplore.Enqueue(start);
        //    while (nodesToExplore.Count > 0)
        //    {
        //        Node currentNode = nodesToExplore.Dequeue();
        //        string currentNodeKey = currentNode.GetKey();
        //        if (currentNodeKey == endNodeKey)
        //        {
        //            return BuildPath(currentNode, start, discoveredNodes);
        //        }

        //        Dictionary<Direction, Node> adjacentNodes = currentNode.getAdjacentNodes();
        //        foreach (Direction d in adjacentNodes.Keys)
        //        {
        //            Node adj = adjacentNodes[d];
        //            string adjKey = adj.GetKey();
        //            if (!discoveredNodes.ContainsKey(adjKey))
        //            {
        //                discoveredNodes[adjKey] = new DiscoveredNode(adj, currentNode, d);
        //                nodesToExplore.Enqueue(adj);
        //            }
        //        }
        //    }

        //    return new List<DiscoveredNode>();
        //}

        //private static List<DiscoveredNode> BuildPath(
        //    Node endNode, Node startNode, Dictionary<string, DiscoveredNode> discoveredNodes)
        //{
        //    List<DiscoveredNode> path = new List<DiscoveredNode>();
        //    DiscoveredNode currNode = discoveredNodes[endNode.GetKey()];
        //    while (currNode.key != startNode.GetKey())
        //    {
        //        path.Add(currNode);
        //        currNode = discoveredNodes[currNode.parentKey];
        //    }
        //    path.Add(currNode);
        //    return path;
        //}

        //private static int CountPathSegments(List<DiscoveredNode> path)
        //{
        //    int count = 1;
        //    Direction prevDirection = path[0].directionFromParent;
        //    for (int i = 1; i < path.Count; i++)
        //    {
        //        DiscoveredNode currNode = path[i];
        //        if (currNode.parentKey != null && currNode.directionFromParent != prevDirection)
        //        {
        //            count++;
        //            prevDirection = currNode.directionFromParent;
        //        }
        //    }
        //    return count;
        //}

        //private static void PrintGrid(string[] grid, List<DiscoveredNode> path)
        //{
        //    char[][] arr = new char[grid.Length][];
        //    for (int i=0; i<arr.Length; i++)
        //    {
        //        arr[i] = grid[i].ToCharArray();
        //    }
        //    for (int i=0; i<path.Count; i++)
        //    {
        //        DiscoveredNode dn = path[i];
        //        char symbol = '-';
        //        if (i == 0)
        //        {
        //            symbol = 'S';
        //        }
        //        else if (i == path.Count - 1)
        //        {
        //            symbol = 'E';
        //        }
        //        arr[dn.node.x][dn.node.y] = symbol;
        //    }

        //    for (int i=0; i<arr.Length; i++)
        //    {
        //        Console.WriteLine(new string(arr[i]));
        //    }
        //}


        //// Complete the minimumMoves function below.
        //public static int minimumMoves2(string[] grid, int startX, int startY, int goalX, int goalY)
        //{
        //    Stack<Direction> moves = new Stack<Direction>();
        //    return mm(grid, startX, startY, goalX, goalY, moves);
        //}

        //private static int mm(
        //    string[] grid, int currX, int currY, int goalX, int goalY, Stack<Direction> moves)
        //{
        //    if (currX == goalX && currY == goalY)
        //    {
        //        return moves.Count;
        //    }

        //    int minMoves = int.MaxValue;
        //    foreach (Direction d in (Direction[])Enum.GetValues(typeof(Direction)))
        //    {
        //        // don't move back in the direction we came from
        //        if (moves.Count > 0 && moves.Peek() == opposite(d))
        //        {
        //            continue;
        //        }

        //        Position p = makeMove(grid, currX, currY, d);
        //        if (p.x != currX || p.y != currY)
        //        {
        //            // it is possible to move in direction d from (currX, currY);
        //            moves.Push(d);
        //            minMoves = Math.Min(minMoves, mm(grid, p.x, p.y, goalX, goalY, moves));
        //            moves.Pop();
        //        }
        //    }

        //    return minMoves;
        //}

        //private static Direction opposite(Direction d)
        //{
        //    if (d == Direction.left) return Direction.right;
        //    else if (d == Direction.right) return Direction.left;
        //    else if (d == Direction.up) return Direction.down;
        //    else return Direction.up;
        //}

        //private static Position makeMove(string[] grid, int currX, int currY, Direction d)
        //{
        //    if (d == Direction.up)
        //    {
        //        while (currY >= 0 && grid[currX][currY] != 'X') currY--;
        //        if (currY < 0) currY++;
        //    }
        //    else if (d == Direction.down)
        //    {
        //        while (currY < grid.Length && grid[currX][currY] != 'X') currY++;
        //        if (currY == grid.Length) currY--;
        //    }
        //    else if (d == Direction.left)
        //    {
        //        while (currX >= 0 && grid[currX][currY] != 'X') currX--;
        //        if (currX < 0) currX++;
        //    }
        //    else // if (d == Direction.right)
        //    {
        //        while (currX < grid.Length && grid[currX][currY] != 'X') currX++;
        //        if (currX == grid.Length) currX--;
        //    }

        //    return new Position(currX, currY);
        //}
    }
}

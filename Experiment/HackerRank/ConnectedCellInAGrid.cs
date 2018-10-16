using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.HackerRank
{
    public class ConnectedCellInAGrid
    {
        // Complete the maxRegion function below.
        public static int maxRegion(int[][] grid)
        {
            Graph g = new Graph(grid);
            List<HashSet<Node>> connectedComponents = getConnectedComponents(g);
            return maxComponentSize(connectedComponents);
        }

       private class Node
       { 
            int[][] grid;
	        Graph g;
            int row;
            int col;
            List<Node> adjacentNodes = null;

            public Node(int[][] grid, int row, int col, Graph g)
            {
                this.grid = grid;
                this.row = row;
                this.col = col;
                this.g = g;
            }

            public IEnumerable<Node> getAdjacentNodes()
            {
                if (adjacentNodes == null)
                {
                    adjacentNodes = BuildAdjacentNodes();
                }
                return adjacentNodes;
            }

            private List<Node> BuildAdjacentNodes()
	        {
		        List<Node> adjacentNodes = new List<Node>();
		
		        if (row > 0)
		        {
                    if (grid[row - 1][col] == 1)
                    {
                        adjacentNodes.Add(g.GetNode(row - 1, col));
                    }
                    if (col > 0 && grid[row - 1][col - 1] == 1)
			        {
				        adjacentNodes.Add(g.GetNode(row-1, col-1));
			        }
			        if (col<grid[row - 1].Length-1 && grid[row - 1][col + 1] == 1)
			        {
				        adjacentNodes.Add(g.GetNode(row-1, col+1));
			        }
		        }
		
		        if (row < grid.Length-1)
		        {
                    if (grid[row + 1][col] == 1)
                    {
                        adjacentNodes.Add(g.GetNode(row + 1, col));
                    }
                    if (col > 0 && grid[row + 1][col - 1] == 1)
			        {
				        adjacentNodes.Add(g.GetNode(row+1, col-1));
			        }
			        if (col<grid[row + 1].Length-1 && grid[row + 1][col + 1] == 1)
			        {
				        adjacentNodes.Add(g.GetNode(row+1, col+1));
			        }
		        }
		
		        if (col > 0 && grid[row][col - 1] == 1)
		        {
			        adjacentNodes.Add(g.GetNode(row, col-1));
		        }
		
		        if (col < grid[row].Length-1 && grid[row][col + 1] == 1)
		        {
			        adjacentNodes.Add(g.GetNode(row, col+1));
		        }

		        return adjacentNodes;
	        }
	
	        public string GetKey()
            {
                return MakeKey(this.row, this.col);
            }

            public override string ToString()
            {
                return GetKey();
            }

            public static string MakeKey(int row, int col)
            {
                return string.Format("{0},{1}", row, col);
            }
        }

        class Graph
        {
            Dictionary<string, Node> nodesByKey = new Dictionary<string, Node>();

            public Graph(int[][] grid)
            {
                for (int row = 0; row < grid.Length; row++)
                {
                    for (int col = 0; col < grid[row].Length; col++)
                    {
                        if (grid[row][col] == 1)
                        {
                            Node n = new Node(grid, row, col, this);
                            nodesByKey[n.GetKey()] = n;
                        }
                    }
                }
            }

            public Node GetNode(int row, int col)
            {
                string key = Node.MakeKey(row, col);
                if (nodesByKey.ContainsKey(key))
                {
                    return nodesByKey[key];
                }
                return null;
            }


            public HashSet<Node> GetAllNodes()
            {
                HashSet<Node> result = new HashSet<Node>();
                foreach(Node n in nodesByKey.Values)
                {
                    result.Add(n);
                }
                return result;
            }
        }

        private static List<HashSet<Node>> getConnectedComponents(Graph g)
        {
            List<HashSet<Node>> result = new List<HashSet<Node>>();

            HashSet<Node> remainingNodes = g.GetAllNodes();
            while (remainingNodes.Count > 0)
            {
                Node start = remainingNodes.First();
                HashSet<Node> component = BFS(g, start);
                remainingNodes.ExceptWith(component);
                result.Add(component);
            }
            return result;
        }

        private static HashSet<Node> BFS(Graph g, Node start)
        {
            HashSet<Node> visited = new HashSet<Node>();
            Queue<Node> toExplore = new Queue<Node>();
            visited.Add(start);
            toExplore.Enqueue(start);
            while (toExplore.Count > 0)
            {
                Node current = toExplore.Dequeue();
                foreach (Node adj in current.getAdjacentNodes())
                {
                    if (!visited.Contains(adj))
                    {
                        visited.Add(adj);
                        toExplore.Enqueue(adj);
                    }
                }
            }
            return visited;
        }

        private static int maxComponentSize(List<HashSet<Node>> components)
        {
            int maxSize = int.MinValue;
            foreach (HashSet<Node> component in components)
            {
                maxSize = Math.Max(maxSize, component.Count);
            }
            return maxSize;
        }
    }
}

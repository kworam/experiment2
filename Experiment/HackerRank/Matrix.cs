using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiment.HackerRank
{
    public class Matrix
    {
        private class UnionFindNode
        {
            public int id;
            public int parentId;
            public int size;
            public bool containsMachine;
            public UnionFindNode machineNode;
            public int weight;

            public UnionFindNode(int id, bool containsMachine)
            {
                this.id = id;
                this.parentId = id;
                this.size = 1;
                this.containsMachine = containsMachine;
                this.weight = 0;
            }

            public override string ToString()
            {
                return string.Format("id:{0}  pid:{1}  m:{2}  sz:{3}", this.id, this.parentId, this.containsMachine, this.size);
            }
        }

        public static int minTime(int[][] roads, int[] machines)
        {
            BuildRoadMap(roads);
            HashSet<int> machineSet = getMachineSet(machines);
            Array.Sort(roads, (x, y) => { return x[2].CompareTo(y[2]); });
            int minTime = ProcessRoads(roads, machineSet);
            return minTime;
        }

        private static void BuildRoadMap(int[][] roads)
        {
            for (int i=0; i<roads.Length; i++)
            {
                int[] road = roads[i];
                roadToWeightMap[GetRoadKey(road[0], road[1])] = road[2];
                roadToWeightMap[GetRoadKey(road[1], road[0])] = road[2];
            }
        }

        private static HashSet<int> getMachineSet(int[] machines)
        {
            HashSet<int> set = new HashSet<int>();
            for(int i=0; i<machines.Length; i++)
            {
                set.Add(machines[i]);
            }
            return set;
        }

        private static string GetRoadKey(int source, int target)
        {
            return string.Format("{0}_{1}", source, target);
        }

        private static Dictionary<int, UnionFindNode> allNodes = new Dictionary<int, UnionFindNode>();
        private static Dictionary<int, UnionFindNode> roots = new Dictionary<int, UnionFindNode>();
        private static Dictionary<string, int> roadToWeightMap = new Dictionary<string, int>();
        private static int ProcessRoads(int[][] sortedRoads, HashSet<int> machineSet)
        {
            int minTime = 0;
            Init(sortedRoads.Length+1, machineSet);
            for (int i=0; i<sortedRoads.Length; i++)
            {
                int[] road = sortedRoads[i];

                UnionFindNode source = allNodes[road[0]];
                UnionFindNode target = allNodes[road[1]];
                UnionFindNode sourceRoot = Find(source);
                UnionFindNode targetRoot = Find(target);
                if (sourceRoot.id != targetRoot.id)
                {
                    minTime += Union(sourceRoot, targetRoot, road);
                }
            }
            return minTime;
        }

        private static void Init(int n, HashSet<int> machineSet)
        {
            for (int i=0; i<n; i++)
            {
                GetOrCreateNode(i, machineSet.Contains(i));
            }
        }

        private static UnionFindNode GetOrCreateNode(int road, bool isMachine)
        {
            if (!allNodes.ContainsKey(road))
            {
                allNodes[road] = roots[road] = new UnionFindNode(road, isMachine);
                if (isMachine)
                {
                    allNodes[road].machineNode = allNodes[road];
                }
            }
            return allNodes[road];
        }

        private static UnionFindNode Find(UnionFindNode node)
        {
            while (node.id != node.parentId)
            {
                UnionFindNode parent = allNodes[node.parentId];
                if (node.containsMachine)
                {
                    parent.containsMachine = true; ;
                    parent.machineNode = node.machineNode;
                }
                node = parent;
            }
            return node;
        }

        private static int Union(UnionFindNode a, UnionFindNode b, int[] road)
        {
            if (a.containsMachine && b.containsMachine)
            {
                // the components we unioned both contain machines
                // find the path between them
                int[] minRoad = FindMinRoadOnPathBetweenMachines(a.machineNode, b.machineNode, road);
                //RemoveRoad(minRoad);
                return minRoad[2];
            }

            if (a.size > b.size)
            {
                b.parentId = a.id;
                a.size += b.size;
                if (!a.containsMachine && b.containsMachine)
                {
                    a.containsMachine = true;
                    a.machineNode = b.machineNode;
                }
                roots.Remove(b.id);
            }
            else
            {
                a.parentId = b.id;
                b.size += a.size;
                if (!b.containsMachine && a.containsMachine)
                {
                    b.containsMachine = true;
                    b.machineNode = a.machineNode;
                }
                roots.Remove(a.id);
            }

            return 0;
        }

        private static int[] GetMinRoadOnPath(List<UnionFindNode> path)
        {
            int minTime = int.MaxValue;
            int minSource = 0;
            int minTarget = 0;
            for (int i = 1; i < path.Count; i++)
            {
                int source = path[i - 1].id;
                int target = path[i].id;
                string roadKey = GetRoadKey(source, target);
                int weight = roadToWeightMap[roadKey];
                if (weight < minTime)
                {
                    minTime = weight;
                    minSource = source;
                    minTarget = target;
                }
                minTime = Math.Min(minTime, weight);
            }
            return new int[] { minSource, minTarget, minTime };
        }

        private static int[] FindMinRoadOnPathBetweenMachines(UnionFindNode a, UnionFindNode b, int[] road)
        {
            //if (a.id == b.id)
            //{
            //    return new List<UnionFindNode>();
            //}
            int aOrigParentId = a.parentId;
            int bOrigParentId = b.parentId;
            if (a.size > b.size)
            {
                b.parentId = a.id;
            }
            else
            {
                a.parentId = b.id;
            }


            int ad = DistanceToRoot(a);
            int bd = DistanceToRoot(b);
            List<UnionFindNode> fromA = new List<UnionFindNode>() { a };
            List<UnionFindNode> fromB = new List<UnionFindNode>() { b };
            if (ad > bd)
            {
                a = Advance(fromA, a, ad - bd);
            }
            else
            {
                b = Advance(fromB, b, bd - ad);
            }
            while (a.id != b.id)
            {
                a = Advance(fromA, a, 1);
                b = Advance(fromB, b, 1);
            }

            List<UnionFindNode> path = Join(fromA, fromB);

            int[] minRoad = null;
            if (path.Count == 0)
            {
                minRoad = road;
            }
            else
            {
                minRoad = GetMinRoadOnPath(path);
            }

            a.parentId = aOrigParentId;
            b.parentId = bOrigParentId;

            return minRoad;
        }

        private static int DistanceToRoot(UnionFindNode node)
        {
            int distance = 0;
            while (node.id != node.parentId)
            {
                distance++;
                node = allNodes[node.parentId];
            }
            return distance;
        }

        private static UnionFindNode Advance(List<UnionFindNode> path, UnionFindNode node, int count)
        {
            while (count > 0)
            {
                count--;
                node = allNodes[node.parentId];
                path.Add(node);
            }
            return node;
        }

        private static List<UnionFindNode> Join(List<UnionFindNode> path1, List<UnionFindNode> path2)
        {
            List<UnionFindNode> longer = path1.Count > path2.Count ? path1 : path2;
            List<UnionFindNode> shorter = path1 == longer ? path2 : path1;

            longer.Reverse();
            longer.RemoveAt(0);
            return shorter.Concat(longer).ToList();
        }

        public static int minTime2(int[][] roads, int[] machines)
        {
            GraphNode node = BuildGraph(roads, machines);
            return MakeBestCut(node, machines.Length);
        }

        private static int MakeBestCut(GraphNode root, int nMachines)
        {
            if (nMachines == 1)
            {
                return 0;
            }

            BestEdgeInfo bestEdgeInfo = Graph.GetBestEdge(root, nMachines);
            GraphEdge bestEdge = bestEdgeInfo.edge;
            Graph.RemoveEdge(bestEdge);
            return bestEdge.weight + 
                MakeBestCut(bestEdge.GetSource(), bestEdgeInfo.sourceMachineCount) + 
                MakeBestCut(bestEdge.GetTarget(), nMachines - bestEdgeInfo.sourceMachineCount);
        }

        private static GraphNode BuildGraph(int[][] roads, int[] machines)
        {
            for (int i = 0; i < roads.Length; i++)
            {
                int[] edge = roads[i];
                Graph.AddEdge(edge[0], edge[1], edge[2]);
            }
            for (int i = 0; i < machines.Length; i++)
            {
                Graph.SetMachine(machines[i]);
            }
            return Graph.FirstNode();
        }

        private class BestEdgeInfo
        {
            public GraphEdge edge;
            public int sourceMachineCount;
        }

        private class Graph
        {
            private static Dictionary<int, GraphNode> allNodes = new Dictionary<int, GraphNode>();

            public static GraphNode GetNode(int id)
            {
                return allNodes[id];
            }

            public static void AddEdge(int source, int target, int weight)
            {
                AddNode(source);
                AddNode(target);
                GetNode(source).AddEdge(new GraphEdge(source, target, weight));
                GetNode(target).AddEdge(new GraphEdge(target, source, weight));
            }

            public static void RemoveEdge(GraphEdge edge)
            {
                GraphNode source = Graph.GetNode(edge.source);
                source.RemoveEdge(edge.target);
                GraphNode target = Graph.GetNode(edge.target);
                target.RemoveEdge(edge.source);
            }

            public static BestEdgeInfo GetBestEdge(GraphNode root, int nMachines)
            {
                // Find the lowest cost edge that splits 
                // the tree T at root with N machines
                // into two trees T1 and T2 with N1 and N2 machines
                // where the difference between N1 and N2 is minimal.

                int minDistFromMid = int.MaxValue;
                GraphEdge bestEdge = null;
                int bestSourceMachines = 0;
                GraphEdge[] edges = Graph.GetEdgesSortedByWeight(root);
                foreach (GraphEdge edge in edges)
                {
                    Graph.RemoveEdge(edge);
                    int sourceMachines = Graph.GetMachineCount(edge.GetSource());
                    int targetMachines = nMachines - sourceMachines;
                    int distFromMid = Math.Abs(targetMachines - sourceMachines);
                    if (distFromMid < minDistFromMid)
                    {
                        minDistFromMid = distFromMid;
                        bestEdge = edge;
                        bestSourceMachines = sourceMachines;
                    }
                    Graph.AddEdge(edge.source, edge.target, edge.weight);
                }

                return new BestEdgeInfo()
                {
                    edge = bestEdge,
                    sourceMachineCount = bestSourceMachines
                };
            }

            public static void SetMachine(int id)
            {
                allNodes[id].SetMachine();
            }

            public static GraphNode FirstNode()
            {
                return allNodes.Values.First();
            }

            public static int GetMachineCount(GraphNode root)
            {
                int machineCount = 0;
                Queue<int> toExplore = new Queue<int>();
                HashSet<int> visited = new HashSet<int>();
                visited.Add(root.id);
                toExplore.Enqueue(root.id);
                while (toExplore.Count > 0)
                {
                    GraphNode current = Graph.GetNode(toExplore.Dequeue());
                    if (current.IsMachine())
                    {
                        machineCount++;
                    }
                    foreach (GraphEdge edge in current.GetEdges())
                    {
                        GraphNode adj = edge.GetTarget();
                        if (!visited.Contains(adj.id))
                        {
                            visited.Add(adj.id);
                            toExplore.Enqueue(adj.id);
                        }
                    }
                }
                return machineCount;
            }

            public static GraphEdge[] GetEdgesSortedByWeight(GraphNode root)
            {
                Queue<int> toExplore = new Queue<int>();
                HashSet<int> visited = new HashSet<int>();
                List<GraphEdge> edgesFollowed = new List<GraphEdge>();
                visited.Add(root.id);
                toExplore.Enqueue(root.id);
                while (toExplore.Count > 0)
                {
                    GraphNode current = Graph.GetNode(toExplore.Dequeue());
                    foreach (GraphEdge edge in current.GetEdges())
                    {
                        edgesFollowed.Add(edge);
                        GraphNode adj = edge.GetTarget();
                        if (!visited.Contains(adj.id))
                        {
                            visited.Add(adj.id);
                            toExplore.Enqueue(adj.id);
                        }
                    }
                }

                GraphEdge[] arr = edgesFollowed.ToArray();
                Array.Sort(arr, (x, y) => x.weight.CompareTo(y.weight) );
                return arr;
            }

            private static void AddNode(int id)
            {
                if (!allNodes.ContainsKey(id))
                {
                    allNodes[id] = new GraphNode(id);
                }
            }

        }

        private class GraphNode
        {
            public readonly int id;
            private readonly List<GraphEdge> adjacent = new List<GraphEdge>();
            private bool hasMachine;

            public GraphNode(int id)
            {
                this.id = id;
            }
            public void AddEdge(GraphEdge edge)
            {
                this.adjacent.Add(edge);
            }

            public void RemoveEdge(int target)
            {
                for (int i=0; i<adjacent.Count; i++)
                {
                    if (adjacent[i].target == target)
                    {
                        adjacent.RemoveAt(i);
                        return;
                    }
                }
            }

            public IEnumerable<GraphEdge> GetEdges()
            {
                return adjacent;
            }

            public void SetMachine()
            {
                this.hasMachine = true;
            }

            public bool IsMachine()
            {
                return hasMachine;
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", this.id, this.hasMachine);
            }
        }

        private class GraphEdge
        {
            public readonly int source;
            public readonly int target;
            public readonly int weight;

            public GraphEdge(int source, int target, int weight)
            {
                this.source = source;
                this.target = target;
                this.weight = weight;
            }

            public GraphNode GetSource()
            {
                return Graph.GetNode(source);
            }

            public GraphNode GetTarget()
            {
                return Graph.GetNode(target);
            }

            public override string ToString()
            {
                return string.Format("s:{0} t:{1} w:{2}", this.source, this.target, this.weight);
            }
        }
    }
}

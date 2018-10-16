using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class Matrix3
    {
        public static int minTime(int[][] roads, int[] machines)
        {
            Graph g = BuildGraph(roads, machines);
            // sort roads by weight
            Array.Sort(roads, (x, y) => { return x[2].CompareTo(y[2]);  });
            return EliminateMachinesOnPath(g, roads, machines);
        }

        private static int EliminateMachinesOnPath(Graph g, int[][] sorted, int[] machines)
        {
            HashSet<int> machineSet = new HashSet<int>(machines);

            int minTime = 0;
            for (int i=0; i<sorted.Length; i++)
            {
                int[] road = sorted[i];
                minTime += GetMinTimeOnPath(g, road, machineSet);
                if (machineSet.Count == 0)
                {
                    break;
                }
            }

            return minTime;
        }
        
        private class MachineInfo
        {
            public int id;
            public GraphEdge minEdge;

            public override string ToString()
            {
                return string.Format("id:{0}  minEdge:{1}", this.id, this.minEdge);
            }
        }

        private static int GetMinTimeOnPath(Graph g, int[] road, HashSet<int> remainingMachines)
        {
            MachineInfo m0 = FindMachineFrom(g, road[0], road[1]);
            MachineInfo m1 = FindMachineFrom(g, road[1], road[0]);

            int minTime = 0;
            if (m0.id >= 0 && m1.id >= 0)
            {
                GraphEdge minEdge = m0.minEdge.weight < m1.minEdge.weight ? m0.minEdge : m1.minEdge;
                g.DeleteEdge(minEdge);

                minTime = minEdge.weight;
                remainingMachines.Remove(m0.id);
                remainingMachines.Remove(m1.id);
            }

            return minTime;
        }

        private static MachineInfo FindMachineFrom(Graph g, int start, int exclude)
        {
            GraphEdge minEdge = null;
            int machineId = -1;

            Queue<int> toExplore = new Queue<int>();
            toExplore.Enqueue(start);

            Dictionary<int, int> discovered = new Dictionary<int, int>();
            discovered[start] = exclude;
            discovered[exclude] = start;
            while (toExplore.Count > 0)
            {
                GraphNode current = g.GetNode(toExplore.Dequeue());
                GraphEdge edge = g.GetEdge(current.id, discovered[current.id]);
                if (minEdge == null)
                {
                    minEdge = edge;
                }
                else
                {
                    minEdge = edge.weight < minEdge.weight ? edge : minEdge;
                }

                if (current.isMachine)
                {
                    machineId = current.id;
                    break;
                }

                foreach (int adjId in current.adjacentByTarget.Keys)
                {
                    if (!discovered.ContainsKey(adjId))
                    {
                        discovered[adjId] = current.id;
                        toExplore.Enqueue(adjId);
                    }
                }
            }

            return new MachineInfo()
            {
                minEdge = minEdge,
                id = machineId
            };
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

            public override string ToString()
            {
                return string.Format("s:{0} t:{1} w:{2}", this.source, this.target, this.weight);
            }
        }

        private class GraphNode
        {
            public readonly int id;
            public readonly bool isMachine;
            public readonly Dictionary<int, GraphEdge> adjacentByTarget = new Dictionary<int, GraphEdge>();

            public GraphNode(int id, bool isMachine)
            {
                this.id = id;
                this.isMachine = isMachine;
            }

            public void AddEdge(int target, int weight)
            {
                adjacentByTarget[target] = new GraphEdge(this.id, target, weight);
            }

            public GraphEdge GetEdge(int target)
            {
                return adjacentByTarget[target];
            }

            public void DeleteEdge(int target)
            {
                adjacentByTarget.Remove(target);
            }

            public override string ToString()
            {
                return string.Format("id:{0} isMachine:{1}", this.id, this.isMachine);
            }
        }

        private class Graph
        {
            private Dictionary<int, GraphNode> nodes = new Dictionary<int, GraphNode>();

            public GraphNode AddNode(int id, bool isMachine)
            {
                if (GetNode(id) == null)
                {
                    nodes[id] = new GraphNode(id, isMachine);
                }
                return GetNode(id);
            }

            public GraphNode GetNode(int id)
            {
                if (nodes.ContainsKey(id))
                {
                    return nodes[id];
                }
                return null;
            }

            public GraphEdge GetEdge(int source, int target)
            {
                GraphNode sourceNode = GetNode(source);
                return sourceNode.GetEdge(target);
            }

            public void DeleteEdge(GraphEdge edge)
            {
                GraphNode source = GetNode(edge.source);
                source.DeleteEdge(edge.target);
                GraphNode target = GetNode(edge.target);
                target.DeleteEdge(edge.source);
            }
        }

        private class BiDirectionalBFS
        {
            public readonly Dictionary<int, int> discovered = new Dictionary<int, int>();
            public BiDirectionalBFS other;
            public bool done;
            public int common = -1;

            private readonly Graph g;
            private readonly Queue<int> toExplore = new Queue<int>();
            private int start;

            public BiDirectionalBFS(Graph g, int start)
            {
                this.g = g;
                this.start = start;
                toExplore.Enqueue(start);
                discovered[start] = -1;
            }

            public void ExploreNextLevel()
            {
                int countInThisLevel = toExplore.Count;
                if (countInThisLevel == 0)
                {
                    done = true;
                    return;
                }

                for (int i = 0; i < countInThisLevel; i++)
                {
                    GraphNode current = g.GetNode(toExplore.Dequeue());
                    foreach (int adjId in current.adjacentByTarget.Keys)
                    {
                        GraphNode adj = g.GetNode(adjId);
                        if (!discovered.ContainsKey(adj.id))
                        {
                            discovered[adj.id] = current.id;
                            toExplore.Enqueue(adj.id);
                            if (other.discovered.ContainsKey(adj.id))
                            {
                                done = true;
                                common = other.common = adj.id;
                                return;
                            }
                        }
                    }
                }

                if (toExplore.Count == 0)
                {
                    done = true;
                }
            }

            public GraphEdge GetMinEdgeOnPathToCommon()
            {
                if (common < 0)
                {
                    return null;
                }

                int minTime = int.MaxValue;
                GraphEdge minEdge = null;
                int current = common;
                while (current != start)
                {
                    int parent = discovered[current];
                    GraphEdge edge = g.GetEdge(parent, current);
                    if (edge.weight < minTime)
                    {
                        minTime = Math.Min(minTime, edge.weight);
                        minEdge = edge;
                    }
                    current = parent;
                }

                return minEdge;
            }
        }

        private static Graph BuildGraph(int[][] roads, int[] machines)
        {
            Graph g = new Graph();
            HashSet<int> machineSet = new HashSet<int>(machines);
            foreach (int[] road in roads)
            {
                GraphNode source = g.AddNode(road[0], machineSet.Contains(road[0]));
                GraphNode target = g.AddNode(road[1], machineSet.Contains(road[1]));
                int weight = road[2];
                source.AddEdge(target.id, weight);
                target.AddEdge(source.id, weight);
            }
            return g;
        }

        private static int TestPathBetweenEachMachine(Graph g, int[] machines)
        {
            //HashSet<int> visited = new HashSet<int>();
            int minTime = 0;
            for (int i = 0; i < machines.Length - 1; i++)
            {
                for (int j = i + 1; j < machines.Length; j++)
                {
                    //int source = machines[i];
                    //int target = machines[j];
                    //if (visited.Contains(source) && visited.Contains(target))
                    //{
                    //    continue;
                    //}

                    //visited.Add(machines[i]);
                    //visited.Add(machines[j]);

                    GraphEdge minEdge = FindMinEdgeOnPath(g, machines[i], machines[j]);
                    if (minEdge != null)
                    {
                        minTime += minEdge.weight;
                        g.DeleteEdge(minEdge);
                    }
                }
            }
            return minTime;
        }

        private static GraphEdge FindMinEdgeOnPath(Graph g, int machine1, int machine2)
        {
            // the fastest way to build the path between the machines is bidirectional BFS
            BiDirectionalBFS bfs1 = new BiDirectionalBFS(g, machine1);
            BiDirectionalBFS bfs2 = new BiDirectionalBFS(g, machine2);
            bfs1.other = bfs2;
            bfs2.other = bfs1;
            BiDirectionalBFS current = bfs1;
            while (!bfs1.done && !bfs2.done)
            {
                current.ExploreNextLevel();
                current = current.other;
            }

            GraphEdge minEdge1 = bfs1.GetMinEdgeOnPathToCommon();
            GraphEdge minEdge2 = bfs2.GetMinEdgeOnPathToCommon();
            if (minEdge1 == null) return minEdge2;
            if (minEdge2 == null) return minEdge1;
            return minEdge1.weight < minEdge2.weight ? minEdge1 : minEdge2;
        }
    }
}

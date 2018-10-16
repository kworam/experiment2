using System.Collections.Generic;

namespace Experiment
{
	public class GraphPrimMinimumSpanningTree
	{
		private readonly Graph weightedGraph;

        public GraphPrimMinimumSpanningTree(Graph weightedGraph)
		{
			this.weightedGraph = weightedGraph;
		}

		public Graph GetTree()
		{
            // initialize the graph that will hold the MST
			Graph mst = new KevinGraph();

            // initialize the priority queue that will hold the vertices outside the MST 
			PriorityQueue remainingVertices = new KevinPriorityQueue();
			foreach (GraphVertex v in weightedGraph.GetAllVertices())
			{
				remainingVertices.Enqueue(new MSTPriorityQueueNode(data: v, priority: int.MaxValue));
			}

            Dictionary<string, GraphEdge> lowestCostEdgeForVertex = new Dictionary<string, GraphEdge>();
			while (remainingVertices.Count() > 0)
			{
                // Get the vertex with the lowest code to add to the MST
                // The first vertex is chosen arbitrarily because all vertices start with max cost.
				PriorityQueueNode currentNode = remainingVertices.Dequeue();
				GraphVertex currentVertex = currentNode.Data as GraphVertex;

                // Add the vertex and its lowest cost edge (if any) to the MST
                mst.AddVertex(currentVertex.UniqueKey);
			    if (lowestCostEdgeForVertex.ContainsKey(currentVertex.UniqueKey))
			    {
			        GraphEdge lowestCostEdge = lowestCostEdgeForVertex[currentVertex.UniqueKey];
                    // TO-DO: why?
                    mst.AddEdge(lowestCostEdge.SourceVertexUniqueKey, lowestCostEdge.TargetVertexUniqueKey, lowestCostEdge.Weight);
                    mst.AddEdge(lowestCostEdge.TargetVertexUniqueKey, lowestCostEdge.SourceVertexUniqueKey, lowestCostEdge.Weight);
			    }
                
                // update the minimum cost for each adjacent vertex, and the associated edge
				foreach (GraphEdge edge in currentVertex.GetIncidentEdges())
				{
					GraphVertex edgeTarget = weightedGraph.GetVertexByUniqueKey(edge.TargetVertexUniqueKey);

					if (!remainingVertices.Contains(edgeTarget.UniqueKey))
					{
						continue;
					}

					int newCost = edge.Weight;
					PriorityQueueNode targetNode = remainingVertices.Peek(edgeTarget.UniqueKey);
					if (newCost < targetNode.Priority)
					{
						remainingVertices.ChangePriority(targetNode.Data.UniqueKey, newCost);
                        lowestCostEdgeForVertex[edgeTarget.UniqueKey] = edge;
					}
				}
			}

		    return mst;
		}

		private class MSTPriorityQueueNode : PriorityQueueNode
		{
            public MSTPriorityQueueNode(HasUniqueKey data, int priority)
			{
				this.Data = data;
				this.Priority = priority;
			}

			public int Priority { get; set; }

			public HasUniqueKey Data { get; private set; }
		}
	}
}

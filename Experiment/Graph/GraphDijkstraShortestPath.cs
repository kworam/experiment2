using System.Collections.Generic;

namespace Experiment
{
	public class GraphDijkstraShortestPath
	{
		private readonly Graph weightedGraph;

		public GraphDijkstraShortestPath(Graph weightedGraph)
		{
			this.weightedGraph = weightedGraph;
		}

		public List<GraphVertex> FindShortestPath(
			string startVertexUniqueKey, 
			string targetVertexUniqueKey)
		{
			GraphVertex startVertex = weightedGraph.GetVertexByUniqueKey(startVertexUniqueKey);
			GraphVertex targetVertex = weightedGraph.GetVertexByUniqueKey(targetVertexUniqueKey);

            // initialize the 'prev' data structure that will hold the path
			Dictionary<string, GraphVertex> prev = new Dictionary<string, GraphVertex>();

            // initialize the priority queue
			PriorityQueue remainingVertices = new KevinPriorityQueue();
			foreach (GraphVertex v in weightedGraph.GetAllVertices())
			{
				int totalPathWeightThroughCurrentVertex = v.UniqueKey == startVertex.UniqueKey ?
					0 :
					int.MaxValue;
				remainingVertices.Enqueue(
					new ShortestPathPriorityQueueNode(data: v, priority: totalPathWeightThroughCurrentVertex));
			}

			while (remainingVertices.Count() > 0)
			{
				PriorityQueueNode currentNode = remainingVertices.Dequeue();
				GraphVertex currentVertex = currentNode.Data as GraphVertex;

				foreach (GraphEdge edge in currentVertex.GetIncidentEdges())
				{
					GraphVertex edgeTarget = weightedGraph.GetVertexByUniqueKey(edge.TargetVertexUniqueKey);

					if (!remainingVertices.Contains(edgeTarget.UniqueKey))
					{
						continue;
					}

					int newDist = currentNode.Priority + edge.Weight;
					PriorityQueueNode targetNode = remainingVertices.Peek(edgeTarget.UniqueKey);
					if (newDist < targetNode.Priority)
					{
						remainingVertices.ChangePriority(targetNode.Data.UniqueKey, newDist);
						prev[edgeTarget.UniqueKey] = currentVertex;
					}
				}

				if (currentVertex.UniqueKey == targetVertex.UniqueKey)
				{
					break;
				}
			}

			return BuildPath(prev, startVertexUniqueKey, targetVertexUniqueKey);
		}

		private List<GraphVertex> BuildPath(
			Dictionary<string, GraphVertex> prev,
			string startVertexUniqueKey,
			string targetVertexUniqueKey)
		{
			List<GraphVertex> path = new List<GraphVertex>();

			string currentKey = targetVertexUniqueKey;
			while (currentKey != startVertexUniqueKey)
			{
				GraphVertex currentVertex = this.weightedGraph.GetVertexByUniqueKey(currentKey);
				path.Add(currentVertex);
				currentKey = prev[currentVertex.UniqueKey].UniqueKey;
			}
			path.Add(this.weightedGraph.GetVertexByUniqueKey(currentKey));
			path.Reverse();

			return path;
		}

		private class ShortestPathPriorityQueueNode : PriorityQueueNode
		{
			public ShortestPathPriorityQueueNode(HasUniqueKey data, int priority)
			{
				this.Data = data;
				this.Priority = priority;
			}

			public int Priority { get; set; }

			public HasUniqueKey Data { get; private set; }
		}
	}
}

using System.Collections.Generic;
using System.Linq;

namespace Experiment
{
	public class GraphTopologicalSort
	{
		public static IEnumerable<GraphTopologicalSortNode> Sort(Graph g)
		{
			// while there are nodes in inDegreeZero
			//  dequeue node 'n', append it to the topSort list
			//  for each node 'v' adjacent to 'n'
			//    delete the edge n->v from the graph 
			//    if 'v' now has inDegree of zero, append it to the inDegreeZero list

			// if there are edges left in clone graph, throw exception (graph not DAG)

			List<GraphTopologicalSortNode> topSort = new List<GraphTopologicalSortNode>();

			Graph clone = g.Clone() as Graph;

			Queue<GraphTopologicalSortNode> inDegreeZeroVertices =
				new Queue<GraphTopologicalSortNode>(
					clone.GetAllVertices().Where(v => clone.GetInDegreeForVertex(v.UniqueKey) == 0).
					Select(v =>
					new GraphTopologicalSortNode()
					{
						Vertex = v,
						Level = 0
					}));
			while (inDegreeZeroVertices.Count > 0)
			{
				GraphTopologicalSortNode current = inDegreeZeroVertices.Dequeue();
				topSort.Add(current);

				List<GraphEdge> edgesCopy = current.Vertex.GetIncidentEdges().Select(e => e).ToList();

				foreach (GraphEdge edge in edgesCopy)
				{
					clone.RemoveEdge(edge.SourceVertexUniqueKey, edge.TargetVertexUniqueKey);

					if (clone.GetInDegreeForVertex(edge.TargetVertexUniqueKey) == 0)
					{
						inDegreeZeroVertices.Enqueue(
							new GraphTopologicalSortNode
							{
								Vertex = g.GetVertexByUniqueKey(edge.TargetVertexUniqueKey),
								Level = current.Level + 1
							});
					}
				}
			}

			if (clone.NumEdges > 0)
			{
				throw new CyclicGraphException(string.Format("You cannot topologically sort a cyclic graph."));
			}

			return topSort;
		}
	}
}

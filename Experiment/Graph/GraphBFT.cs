using System.Collections.Generic;

namespace Experiment
{
	public class GraphBFT
	{
		private readonly Graph g;
		public GraphBFT(Graph g)
		{
			this.g = g;
		}

		public void TraverseGraph(string startingVertexUniqueKey, GraphVisitor visitor)
		{
			Dictionary<string, bool> visited = new Dictionary<string, bool>();

			Queue<GraphVertex> q = new Queue<GraphVertex>();

			GraphVertex start = g.GetVertexByUniqueKey(startingVertexUniqueKey);
			q.Enqueue(start);
			while (q.Count > 0)
			{
				GraphVertex current = q.Dequeue();

				visitor.Visit(current);
                visited[current.UniqueKey] = true;

				foreach (GraphVertex v in current.GetAdjacentVertices())
				{
					if (!visited.ContainsKey(v.UniqueKey) && !q.Contains(v))
					{
						q.Enqueue(v);
					}
				}
			}
		}
	}
}

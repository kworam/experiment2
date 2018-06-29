using System.Collections.Generic;

namespace Experiment
{
    public class GraphDFT
    {
        private readonly Graph g;

        public GraphDFT(Graph g)
        {
            this.g = g;
        }

        public void TraverseGraph(string startingVertexUniqueKey, GraphVisitor visitor)
        {
            GraphVertex start = g.GetVertexByUniqueKey(startingVertexUniqueKey);
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            InternalTraverseGraph(start, visitor, visited);
        }

        public void InternalTraverseGraph(GraphVertex v, GraphVisitor visitor, Dictionary<string, bool> visited)
        {
            visitor.Visit(v);
            visited[v.UniqueKey] = true;

            foreach (GraphVertex adj in v.GetAdjacentVertices())
            {
                if (!visited.ContainsKey(adj.UniqueKey))
                {
                    InternalTraverseGraph(adj, visitor, visited);
                }
            }
        }
    }
}

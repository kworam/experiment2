using System.Collections.Generic;

namespace Experiment
{
	public interface GraphVertex : HasUniqueKey
	{
		void AddEdge(string targetVertexUniqueKey);

        void AddEdge(string targetVertexUniqueKey, int weight);

        void RemoveEdge(string targetVertexUniqueKey);
        
        IEnumerable<GraphVertex> GetAdjacentVertices();

		IEnumerable<GraphEdge> GetIncidentEdges();
	}
}

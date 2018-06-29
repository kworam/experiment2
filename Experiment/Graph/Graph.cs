using System;
using System.Collections.Generic;

namespace Experiment
{
	public interface Graph : ICloneable
	{
        void AddVertex(string key);

	    void AddEdge(string sourceUniqueKey, string targetUniqueKey, int weight);

        void RemoveEdge(string sourceUniqueKey, string targetUniqueKey);

        int NumVertices { get; }

        int NumEdges { get;  }

	    bool ContainsVertex(string key);

		GraphVertex GetVertexByUniqueKey(string key);

		IEnumerable<GraphVertex> GetAllVertices();

	    uint GetInDegreeForVertex(string vertexUniqueKey);

        uint GetOutDegreeForVertex(string vertexUniqueKey);
    }
}

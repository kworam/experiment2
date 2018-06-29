using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Experiment
{
	public class KevinGraphVertex : GraphVertex
	{
	    private readonly Graph g;
		private readonly List<GraphEdge> incidentEdges = new List<GraphEdge>();

		public KevinGraphVertex(Graph g, string uniqueName)
		{
            this.g = g;
            this.UniqueKey = uniqueName;
		}

		public string UniqueKey { get; private set; }

        public uint InDegree { get; set; }

        public uint OutDegree { get; set; }

        public void AddEdge(string targetVertexUniqueKey)
		{
            this.AddEdge(targetVertexUniqueKey, weight: 0);
		}

        public void AddEdge(string targetVertexUniqueKey, int weight)
		{
            InternalCheckAndAddEdge(targetVertexUniqueKey, weight);
		}

	    public void RemoveEdge(string targetVertexUniqueKey)
	    {
            GraphEdge edge = GetEdgeToVertex(targetVertexUniqueKey);
	        InternalRemoveEdge(edge);
	    }

        public void InternalCheckAndAddEdge(string targetVertexUniqueKey, int weight)
		{
            this.incidentEdges.ForEach(incidentEdge =>
		    {
                if (targetVertexUniqueKey == incidentEdge.TargetVertexUniqueKey)
		        {
		            throw new ArgumentException(
		                string.Format("Vertex {0} already has an edge to vertex {1}.",
		                    this.UniqueKey,
                            targetVertexUniqueKey));
		        }
		    });

            InternalAddEdge(new KevinGraphEdge(this.g, this.UniqueKey, targetVertexUniqueKey, weight));
		}

		public IEnumerable<GraphVertex> GetAdjacentVertices()
		{
			return incidentEdges.Select(edge => g.GetVertexByUniqueKey(edge.TargetVertexUniqueKey));
		}

		public IEnumerable<GraphEdge> GetIncidentEdges()
		{
			return incidentEdges;
		}

		public override string ToString()
		{
			return this.UniqueKey;
		}

		private void InternalAddEdge(GraphEdge edge)
		{
			incidentEdges.Add(edge);
		}

        private void InternalRemoveEdge(GraphEdge edge)
        {
            incidentEdges.Remove(edge);
        }

	    private GraphEdge GetEdgeToVertex(string targetVertexUniqueKey)
	    {
	        foreach (GraphEdge edge in incidentEdges)
	        {
                if (edge.TargetVertexUniqueKey == targetVertexUniqueKey)
	            {
	                return edge;
	            }
	        }

            throw new GraphEdgeNotFoundException(
                string.Format("Vertex {0} has no edge to vertex {1}", 
                    this.UniqueKey,
                    targetVertexUniqueKey));
	    }
	}
}

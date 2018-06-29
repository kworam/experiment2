namespace Experiment
{
	public class KevinGraphEdge : GraphEdge
	{
        private readonly Graph g;

        public KevinGraphEdge(Graph g, string sourceVertexUniqueKey, string targetVertexUniqueKey, int weight)
        {
            this.g = g;
            this.SourceVertexUniqueKey = sourceVertexUniqueKey;
            this.TargetVertexUniqueKey = targetVertexUniqueKey;
			this.Weight = weight;
		}

		public int Weight { get; private set; }

        public string SourceVertexUniqueKey { get; private set; }

        public string TargetVertexUniqueKey { get; private set; }

		public override string ToString()
		{
			return this.TargetVertexUniqueKey;
		}
	}
}

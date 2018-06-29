namespace Experiment
{
	public interface GraphEdge
	{
		int Weight { get; }

		string SourceVertexUniqueKey { get; }

		string TargetVertexUniqueKey { get; }
	}
}

using System;

namespace Experiment
{
	public class GraphUniqueVertexNameViolationException : Exception
	{
		public GraphUniqueVertexNameViolationException(string msg) : base(msg)
		{
		}
	}
}

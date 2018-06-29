using System;

namespace Experiment
{
	public class GraphEdgeNotFoundException : Exception
	{
        public GraphEdgeNotFoundException(string msg) : base(msg)
		{
		}
	}
}


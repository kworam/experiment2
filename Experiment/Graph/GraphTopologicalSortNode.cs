namespace Experiment
{
	public class GraphTopologicalSortNode
	{
		public GraphVertex Vertex { get; set;  }

        public int Level { get; set;  }

		public override string ToString()
		{
			return string.Format("{0}:{1}", Vertex, Level);
		}
	}
}

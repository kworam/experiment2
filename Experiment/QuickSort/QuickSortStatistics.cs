using System.Text;

namespace Experiment
{
	public class QuickSortStatistics
	{
		public long numRecursions = 0;
		public long numSwaps = 0;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format("NumRecursions: {0}", numRecursions));
			sb.AppendLine(string.Format("NumSwaps: {0}", numSwaps));
			return sb.ToString();
		}
	}
}
using System;

namespace Experiment
{
	public class PriorityQueueNodeNotFoundException : Exception
	{
		public PriorityQueueNodeNotFoundException(string message) : base(message)
		{
		}
	}
}

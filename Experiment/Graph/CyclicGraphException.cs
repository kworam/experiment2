using System;

namespace Experiment
{
    public class CyclicGraphException : Exception
    {
        public CyclicGraphException(string msg)
            : base(msg)
        {
        }
    }
}


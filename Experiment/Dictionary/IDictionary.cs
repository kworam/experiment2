using System;
using System.Collections.Generic;

namespace Experiment.Dictionary
{
    public interface IDictionary<K, V> where K : IEquatable<K>
    {
        void Put(K key, V value);

        V Get(K key);

        bool Remove(K key);

        int Count { get;  }

        IEnumerable<K> Keys { get;  }
    }
}

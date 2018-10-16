using System;
using System.Collections.Generic;

namespace Experiment.Dictionary
{
    public class KevinDictionary<K, V> : IDictionary<K, V> where K : IEquatable<K>
    {
        private class Node
        {
            public K key;
            public V value;
            public Node next;
            public Node prev;
            public int bucket;

            public Node(K key, V value, int bucket)
            {
                this.key = key;
                this.value = value;
                this.bucket = bucket;
            }

            public override string ToString()
            {
                //return string.Format("k:{0} v:{1} b:{2} prev:{3} next:{4}",
                //    this.key,
                //    this.value,
                //    this.bucket,
                //    this.prev == null ? "NULL" : this.prev.ToString(),
                //    this.next == null ? "NULL" : this.next.ToString());
                return string.Format("k:{0} v:{1} b:{2}",
                    this.key,
                    this.value,
                    this.bucket);
            }
        }

        private int capacity;
        private Node[] arr;
        private Node head;
        private Node tail;


        private int Hash(K key)
        {
            return key.GetHashCode();
        }

        private int GetArrIndex(K key)
        {
            return Hash(key) % arr.Length;
        }

        public KevinDictionary(int capacity)
        {
            this.capacity = capacity;
            arr = new Node[capacity * 2];
        }

        public void Put(K key, V value)
        {
            int index = GetArrIndex(key);
            Node n = Find(index, key);
            if (n == null || !n.key.Equals(key))
            {
                // key does not exist in dictionary, insert it
                Node newNode = new Node(key, value, index);
                if (n == null)
                {
                    // new Bucket
                    arr[index] = newNode;
                    if (tail == null)
                    {
                        head = tail = newNode;
                    }
                    else
                    {
                        tail.next = newNode;
                        newNode.prev = tail;
                        tail = newNode;
                    }
                }
                else
                {
                    // append to existing Bucket
                    Node prev = n;
                    Node next = prev.next;

                    prev.next = newNode;
                    newNode.prev = prev;
                    newNode.next = next;
                    if (newNode.next == null)
                    {
                        tail = newNode;
                    }
                }
            }
            else
            {
                // key already exists in dictionary, just update it
                n.value = value;
            }
        }

        // the head of each bucket list points to the tail of the previous list
        // with head.prevList
        // the tail of each bucket list points to the head of the next list
        // with head.nextList
        public bool Remove(K key)
        {
            int index = GetArrIndex(key);
            Node toRemove = Find(index, key);
            if (toRemove == null || !toRemove.key.Equals(key))
            {
                return false;
            }

            // cases:
            // 1. remove head of complete list
            // 2. remove tail of complete list
            // 3. remove head of bucket list
            // 4. remove tail of bucket list
            // 5. remove interior of bucket list
            if (toRemove.prev == null && toRemove.next  == null)
            {
                // toRemove is last item in dictionary
                head = tail = arr[index] = null;
            }
            else if (toRemove.prev == null)
            {
                // toRemove is head of complete list of 2 or more items.
                head = toRemove.next;
                head.prev = null;
                if (toRemove == arr[index])
                {
                    // toRemove is head of bucket list
                    arr[index] = head.bucket == toRemove.bucket ? head : null;
                }
            }
            else if (toRemove.next == null)
            {
                // toRemove is tail of complete list of 2 or more items.
                tail = toRemove.prev;
                tail.next = null;
                if (toRemove == arr[index])
                {
                    // toRemove is last item in bucket list
                    arr[index] = null;
                }
            }
            else
            {
                // toRemove is interior node in complete list
                toRemove.prev.next = toRemove.next;
                toRemove.next.prev = toRemove.prev;
                if (toRemove == arr[index])
                {
                    // to remove is last item in bucket list
                    arr[index] = null;
                }
            }

            return true;
        }

        private Node Find(int index, K key)
        {
            Node n = arr[index];
            while (n != null && n.next != null && n.next.bucket == n.bucket)
            {
                if (n.key.Equals(key))
                {
                    return n;
                }

                n = n.next;
            }

            return n;
        }

        public V Get(K key)
        {
            int index = GetArrIndex(key);
            Node n = Find(index, key);
            if (n == null || !n.key.Equals(key))
            {
                throw new DictionaryKeyNotFoundException();
            }
            return n.value;
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<K> Keys
        {
            get
            {
                List<K> keys = new List<K>();
                Node n = head;
                while (n != null)
                {
                    keys.Add(n.key);
                    n = n.next;
                }
                return keys;
            }
        }

        public IEnumerable<V> Values
        {
            get
            {
                List<V> values = new List<V>();
                Node n = head;
                while (n != null)
                {
                    values.Add(n.value);
                    n = n.next;
                }
                return values;
            }
        }
    }
}

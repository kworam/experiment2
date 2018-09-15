using System;
using System.Collections.Generic;
using System.Text;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest
{
    [TestClass]
    public class TrieUnitTest
    {
        [TestCategory("Trie"), TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetFromEmptyTrie()
        {
            Trie trie = TrieFactory.Create();
            string key = "bogus";
            int value = trie.Get(key);
        }

        [TestCategory("Trie"), TestMethod]
        public void InsertIntoTrie()
        {
            Trie trie = TrieFactory.Create();
            Tuple<string, int> tuple = new Tuple<string, int>("bogus", 99);
            trie.Insert(tuple.Item1, tuple.Item2);
            Assert.AreEqual(trie.Count(), 1);
            Assert.AreEqual(trie.Get(tuple.Item1), tuple.Item2);
        }

        [TestCategory("Trie"), TestMethod]
        public void TraverseTriePreOrder()
        {
            Trie trie = TrieFactory.Create();
            List<Tuple<string, int>> tuples = new List<Tuple<string, int>>()
            {
                new Tuple<string, int>("bogus", 99),
                new Tuple<string, int>("aack", 88),
                new Tuple<string, int>("zebra", 77),
                new Tuple<string, int>("kangaroo", 66),
                new Tuple<string, int>("donkey", 55),
                new Tuple<string, int>("dog", 44)
            };

            foreach (Tuple<string, int> tuple in tuples)
            {
                trie.Insert(tuple.Item1, tuple.Item2);
            }
            Assert.AreEqual(trie.Count(), tuples.Count);

            TestVisitor testVisitor = new TestVisitor();
            trie.Traverse(TraversalType.PreOrder, testVisitor);
            Console.WriteLine(testVisitor.ToString());
        }

        private class TestVisitor : TrieVisitor
        {
            private List<int> values = new List<int>();

            public void Visit(TrieNode node)
            {
                values.Add(node.Value);
            }

            public override string ToString()
            {
                return String.Join(" ", values);
            }
        }
    }
}

namespace Experiment
{
    public interface Trie
    {
        void Insert(string key, int value);

        int Get(string key);

        int Count();

        void Traverse(TraversalType traversalType, TrieVisitor visitor);
    }
}

namespace Experiment
{
    public static class TrieFactory
    {
        public static Trie Create()
        {
            return new KevinTrie();
        }
    }
}

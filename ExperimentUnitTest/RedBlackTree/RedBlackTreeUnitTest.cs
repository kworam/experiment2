using Experiment.RedBlackTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.RedBlackTree
{
    [TestClass]
    public class RedBlackTreeUnitTest
    {
        [TestCategory("RedBlackTree"),TestMethod]
        public void TestLeftRotation1()
        {
            Tree rbt = new Tree();
            rbt.Insert(1);
            rbt.Insert(2);
            rbt.Insert(3);
            rbt.Insert(4);
            rbt.RotateLeft(1);
        }

        [TestCategory("RedBlackTree"), TestMethod]
        public void TestLeftRotation2()
        {
            Tree rbt = new Tree();
            rbt.Insert(1);
            rbt.Insert(3);
            rbt.Insert(2);
            rbt.Insert(4);
            rbt.RotateLeft(1);
        }

    }
}

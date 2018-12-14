using System.Collections.Generic;
using System.Linq;
using Experiment;
using Experiment.Dictionary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.Dictionary
{
    [TestClass]
    public class DictionaryUnitTest
    {
        [TestCategory("Dictionary"), TestMethod]
        [ExpectedException(typeof(DictionaryKeyNotFoundException))]
        public void GetMissing()
        {
            int key = 0;
            KevinDictionary<int, string> kd = new KevinDictionary<int, string>(100);
            kd.Get(key);
        }

        [TestCategory("Dictionary"), TestMethod]
        public void GetExisting()
        {
            int key = 99;
            string value = "3231";
            KevinDictionary<int, string> kd = new KevinDictionary<int, string>(100);
            kd.Put(key, value);
            Assert.AreEqual(kd.Get(key), value);
        }

        [TestCategory("Dictionary"), TestMethod]
        public void GetKeys()
        {
            int[] random = ArrayUtility.GenerateRandomIntArray(5, 1000);
            int[] sortedDistinct = ArrayUtility.GetSortedDistinct(random);

            KevinDictionary<int, string> kd = new KevinDictionary<int, string>(100);
            SortedList<int, int> originalKeys = new SortedList<int, int>();
            for (int i=0; i< sortedDistinct.Length; i++)
            {
                kd.Put(sortedDistinct[i], sortedDistinct[i].ToString());
                originalKeys.Add(sortedDistinct[i], sortedDistinct[i]);
            }

            int[] keysFromDictionary = kd.Keys.ToArray();
            int[] sdk = ArrayUtility.GetSortedDistinct(keysFromDictionary);
            Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(originalKeys.Values.ToArray(), sdk)); 

            for (int i=0; i<sortedDistinct.Length; i++)
            {
                kd.Remove(sortedDistinct[i]);
                originalKeys.Remove(sortedDistinct[i]);

                keysFromDictionary = kd.Keys.ToArray();
                sdk = ArrayUtility.GetSortedDistinct(keysFromDictionary);

                Assert.IsTrue(ArrayUtility.AreIntegerEnumerablesEqual(originalKeys.Values.ToArray(), sdk));
            }
        }
    }
}

using System;
using System.IO;
using Experiment.HackerRank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest.HackerRank
{
    [TestClass]
    public class FraudAlertNotificationUnitTest
    {
        [TestCategory("FraudAlertNotification"), TestMethod]
        public void TestCase0()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\FraudAlertNotification_TestCase0.txt"))
            {
                DoTest(sr);
            }
        }

        [TestCategory("FraudAlertNotification"), TestMethod]
        public void TestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\FraudAlertNotification_TestCase1.txt"))
            {
                DoTest(sr);
            }
        }

        private static void DoTest(StreamReader sr)
        {
            string[] nd = sr.ReadLine().Split(' ');

            int n = Convert.ToInt32(nd[0]);

            int d = Convert.ToInt32(nd[1]);

            int[] expenditure = Array.ConvertAll(sr.ReadLine().Split(' '), expenditureTemp => Convert.ToInt32(expenditureTemp))
            ;
            //int result = FraudAlertNotification.activityNotifications(expenditure, d);
            int result = FraudAlertNotification.activityNotifications2(expenditure, d);

            Console.WriteLine(result);
        }
    }
}

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
        public void TestCase1()
        {
            using (StreamReader sr = new StreamReader(@"HackerRank\FraudAlertNotification_TestCase1.txt"))
            {
                string[] nd = sr.ReadLine().Split(' ');

                int n = Convert.ToInt32(nd[0]);

                int d = Convert.ToInt32(nd[1]);

                int[] expenditure = Array.ConvertAll(sr.ReadLine().Split(' '), expenditureTemp => Convert.ToInt32(expenditureTemp))
                ;
                int result = FraudAlertNotification.activityNotifications(expenditure, d);

                Console.WriteLine(result);
            }
        }
    }
}

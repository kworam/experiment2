using System;
using System.Collections.Generic;
using System.Text;

namespace Experiment.HackerRank
{
    public class FraudAlertNotification
    {
        public static int activityNotifications2(int[] expenditure, int d)
        {
            Queue<int> expenditureWindow = new Queue<int>();
            int numNotifications = 0;
            int[] counts = new int[201];
            for (int i=0; i<d; i++)
            {
                int ex = expenditure[i];
                insert(counts, ex, expenditureWindow);
            }
            for (int i=d; i<expenditure.Length; i++)
            {
                int ex = expenditure[i];
                double median = getMedian(counts, d);
                if (ex >= median * 2.0)
                {
                    numNotifications++;
                }
                insert(counts, ex, expenditureWindow);
                deleteOldest(counts, expenditureWindow);
            }

            return numNotifications;
        }

        private static double getMedian(int[] counts, int d)
        {
            int i = 0;
            if (d % 2 == 1)
            {
                int idx = (d / 2) + 1;
                while (counts[i] < idx) i++;
                return (double)i;
            }
            else
            {
                int idx = d / 2;
                while (counts[i] < idx) i++;
                int leftMedian = i;
                int rightMedian = idx < counts[i] ? i : getNextVal(counts, i);
                return ((double)leftMedian + (double)rightMedian) / 2.0;
            }
        }

        private static int getNextVal(int[] counts, int start)
        {
            int startCount = counts[start];
            int i = start;
            while (++i < counts.Length)
            {
                if (counts[i] > startCount)
                {
                    return i;
                }
            }
            throw new Exception("next val not found");
        }

        private static void insert(int[] counts, int ex, Queue<int> expenditureWindow)
        {
            for (int i=ex; i<counts.Length; i++)
            {
                counts[i]++;
            }
            expenditureWindow.Enqueue(ex);
        }

        private static void deleteOldest(int[] counts, Queue<int> expenditureWindow)
        {
            int deleteVal = expenditureWindow.Dequeue();
            for (int i=deleteVal; i<counts.Length; i++)
            {
                counts[i]--;
            }
        }
    }
}

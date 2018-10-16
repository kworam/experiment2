using System;
using System.Collections.Generic;

namespace Experiment.HackerRank
{
    public class MinTime
    {
        private class MachineInfo
        {
            public readonly Dictionary<long, long> dtfCountMap = new Dictionary<long, long>();
            public long minDTF;
            public long maxDTF;
        }

        public static long minTime(long[] machines, long goal)
        {
            MachineInfo mi = getMachineInfo(machines);
            int n = machines.Length;
            long itemsPerMachine = (long)Math.Ceiling(goal / (double)n);
            long minDTF = itemsPerMachine * mi.minDTF;
            long maxDTF = itemsPerMachine * mi.maxDTF;
            return binSearch(mi, goal, minDTF, maxDTF);
        }

        static MachineInfo getMachineInfo(long[] machines)
        {
            MachineInfo mi = new MachineInfo();
            mi.maxDTF = long.MinValue;
            mi.minDTF = long.MaxValue;
            for (long i=0; i<machines.Length; i++)
            {
                long dtf = machines[i];
                mi.maxDTF = Math.Max(mi.maxDTF, dtf);
                mi.minDTF = Math.Min(mi.minDTF, dtf);
                if (!mi.dtfCountMap.ContainsKey(dtf))
                {
                    mi.dtfCountMap[dtf] = 0;
                }
                mi.dtfCountMap[dtf]++;
            }
            return mi;
        }

        static long binSearch(MachineInfo mi, long goal, long loDTF, long hiDTF)
        {
            if (loDTF > hiDTF)
            {
                return 0;
            }

            long midDTF = (loDTF + hiDTF) / 2;
            long production = getProductionInt(mi, midDTF);
            if (production == goal || midDTF == loDTF || midDTF == hiDTF)
            {
                return linearSearch(mi, production, goal, midDTF);
            }
            else if (production > goal)
            {
                return binSearch(mi, goal, loDTF, midDTF - 1);
            }
            else
            {
                return binSearch(mi, goal, midDTF + 1, hiDTF);
            }
        }

        static long linearSearch(MachineInfo mi, long lastProduction, long goal, long lastDay)
        {
            if (lastProduction < goal)
            {
                lastDay++;
                while (getProductionInt(mi, lastDay) < goal)
                {
                    lastDay++;
                }
            }
            else
            {
                lastDay--;
                while (getProductionInt(mi, lastDay) >= goal)
                {
                    lastDay--;
                }
                lastDay++;
            }

            return lastDay;
        }

        static long getProductionInt(MachineInfo mi, long days)
        {
            long production = 0;
            foreach (long dtf in mi.dtfCountMap.Keys)
            {
                long numProduced = days / dtf;
                production += (numProduced * mi.dtfCountMap[dtf]);
            }
            return production;
        }

        //static long binSearch(long[] machines, long goal, long loDTF, long hiDTF)
        //{
        //    if (loDTF > hiDTF)
        //    {
        //        return 0;
        //    }

        //    long midDTF = (loDTF + hiDTF) / 2;
        //    double production = getProductionFloat(machines, midDTF);
        //    if (production == goal)
        //    {
        //        return midDTF;
        //    }
        //    else
        //    {
        //        if (midDTF == loDTF)
        //        {
        //            // we have gotten as close as we can with fractional production
        //            return linearSearch(machines, goal, production, midDTF);
        //        }
        //        else
        //        {
        //            if (production > goal)
        //            {
        //                return binSearch(machines, goal, loDTF, midDTF - 1);
        //            }
        //            else
        //            {
        //                return binSearch(machines, goal, midDTF + 1, hiDTF);
        //            }
        //        }
        //    }
        //}

        //static double getProductionFloat(long[] machines, long days)
        //{
        //    double production = 0;
        //    for (int i = 0; i < machines.Length; i++)
        //    {
        //        production += (days / (double)machines[i]);
        //    }
        //    return production;
        //}

        //static long linearSearch(long[] machines, long goal, double lastProduction, long lastDay)
        //{
        //    long days = lastDay;
        //    long production = getProductionInt(machines, days);
        //    if (production == goal)
        //    {
        //        return days;
        //    }
        //    else if (production < goal)
        //    {
        //        days++;
        //        while (getProductionInt(machines, days) < goal)
        //        {
        //            days++;
        //        }
        //    }
        //    else
        //    {
        //        days--;
        //        while (production >= goal)
        //        {
        //            days--;
        //        }
        //        days++;
        //    }

        //    return days;
        //}

        //static long getProductionInt(long[] machines, long days)
        //{
        //    long production = 0;
        //    for (int i = 0; i < machines.Length; i++)
        //    {
        //        production += (days / machines[i]);
        //    }
        //    return production;
        //}
    }
}

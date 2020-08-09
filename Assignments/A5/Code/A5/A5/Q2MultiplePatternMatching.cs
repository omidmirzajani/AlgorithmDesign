﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q2MultiplePatternMatching : Processor
    {
        public Q2MultiplePatternMatching(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(45, 50);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
            #region Implement Trie
            long tt = 0;
            for(int i=0;i<n;i++)
            {
                patterns[i] += "$";
                tt += patterns[i].Length;
            }
            Graph g = new Graph(tt);
            long last = 1;
            for (int i = 0; i < patterns[0].Length; i++)
            {
                g.addEdge(i, i + 1, patterns[0][i]);
                last++;
            }
            for (int i = 1; i < n; i++)
            {
                long check = 0;
                int j = 0;
                long t = myfunc(g.adj[check], patterns[i][j]);
                while (t != -1)
                {
                    check = t;
                    j++;
                    if (j >= patterns[i].Length)
                        t = -1;
                    else
                        t = myfunc(g.adj[check], patterns[i][j]);
                }
                for (long k = j; k < patterns[i].Length; k++)
                {
                    g.addEdge(check, last++, patterns[i][(int)k]);
                    check = last - 1;
                }
            }
            #endregion 
            List<long> result = new List<long>();
            for (int i2 = 0; i2 < text.Length; i2++)
            {
                long lastChecked = 0;
                long j2 = i2;
                long t2 = myfunc(g.adj[lastChecked], text[(int)j2]);
                while (t2 != -1)
                {
                    lastChecked = t2;
                    j2++;
                    if (j2 >= text.Length)
                        t2 = -1;
                    else
                        t2 = myfunc(g.adj[lastChecked], text[(int)j2]);

                    if (myfunc(g.adj[lastChecked], '$') != -1 && !result.Contains(i2))
                        result.Add(i2);

                }
            }
            if (result.Count == 0)
                result.Add(-1);
            return result.ToArray();
        }
        private long myfunc(List<Tuple<long, char>> list, char v)
        {
            foreach (var vv in list)
                if (vv.Item2 == v)
                    return vv.Item1;
            return -1;
        }
    }
}

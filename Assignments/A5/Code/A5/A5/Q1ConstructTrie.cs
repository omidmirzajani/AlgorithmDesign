using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public string[] Solve(long n, string[] patterns)
        {
            long tt = patterns.Select(d => d.Length).Max();
            Graph g = new Graph(n * tt);
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
                    t = myfunc(g.adj[check], patterns[i][j]);
                }
                for (long k = j; k < patterns[i].Length; k++)
                {
                    g.addEdge(check, last++, patterns[i][(int)k]);
                    check = last - 1;
                }
            }
            return g.Q1ConstructTrie.ToArray();
        }
         private long myfunc(List<Tuple<long, char>> list, char v)
        {
            foreach(var vv in list)
                if (vv.Item2 == v)
                    return vv.Item1;
            return -1;
        }
    }
}

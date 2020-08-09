using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            MyGraph demo = new MyGraph(nodeCount);
            foreach (var t in edges)
            {
                if(t[0]!=t[1])
                    demo.addEdge(t[0] - 1, t[1] - 1, t[2]);
            }
            long sum = 0;
            while (true)
            {
                long[] path = demo.ShortestPath(0, nodeCount - 1);
                if (path.Length == 0)
                {
                    return sum;
                }
                else
                {
                    long min = int.MaxValue;
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        min = Math.Min(min, demo.res[path[i], path[i + 1]]);
                    }
                    sum += min;
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        demo.res[path[i], path[i + 1]] -= min;
                        demo.res[path[i + 1], path[i]] += min;
                    }
                }
            }
        }
    }
}

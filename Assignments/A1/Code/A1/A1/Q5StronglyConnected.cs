using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            Graph g = new Graph(nodeCount);
            for (int i = 0; i < edges.Length; i++)
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1);
            long res = g.SCC(edges);
            return res;
        }
        
    }
}

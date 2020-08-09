using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long[] dist;

        public long Solve(long nodeCount, long[][] edges)
        {
            Graph g = new Graph(nodeCount, edges);
            for (long i = 0; i < edges.Length; i++)
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1, edges[i][2]);

            return g.Cycle();
        }
    }
}

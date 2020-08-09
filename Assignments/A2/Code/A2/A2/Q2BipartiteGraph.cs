using System;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long NodeCount, long[][] edges)
        {
            Graph g = new Graph(NodeCount, edges);
            for (long i = 0; i < edges.Length; i++)
            {
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1);
                g.addEdge(edges[i][1] - 1, edges[i][0] - 1);

            }
            return g.TwoPart();
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            Graph g = new Graph(nodeCount);
            
            for (int i=0;i<edges.Length;i++)
            {
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1);
                g.addEdge(edges[i][1] - 1, edges[i][0] - 1);
            }
            List<long> dfs = g.DFS(StartNode-1);
            if (dfs.Contains(EndNode-1))
                return 1;
            return 0;
        }
       
        private long[] ToDisjoint(long nodeCount, long[][] edges)
        {
            long[] res = new long[nodeCount+1];
            for(int i=0;i<nodeCount+1;i++)
            {
                res[i] = i;
            }
            for(int i=0;i<edges.Length;i++)
            {
                long first = edges[i][0];
                long last = edges[i][1];
                long min = Math.Min(res[first],res[last]);
                long max= res[first]+ res[last]-min;
                for (int j = 0; j <= nodeCount; j++)
                    if (res[j] == max)
                        res[j] = min;
            }
            return res;
        }
    }
}

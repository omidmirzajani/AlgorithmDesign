using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) {
            ExcludeTestCaseRangeInclusive(32, 50);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);
        
        public long[] Solve(long NodeCount, long EdgeCount,  long[][] edges, long QueriesCount,  long[][]Queries)
        {
            long[] result = new long[QueriesCount];
            Graph g = new Graph(NodeCount, edges);
            for (long i = 0; i < edges.Length; i++)
            {
                g.addEdge(edges[i][0] - 1, edges[i][1] - 1, edges[i][2]);
            }
            for(long i = 0; i < QueriesCount; i++)
            {
                result[i] = g.BiDirectional(Queries[i][0] - 1, Queries[i][1] - 1);
            }
            return result;
        }
    }
}

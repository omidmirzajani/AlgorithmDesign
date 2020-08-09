using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        public Q2Airlines(string testDataName) : base(testDataName) {
          
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);

        public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            long nodeCount = flightCount + crewCount + 2;
            MyGraph graph = new MyGraph(nodeCount);
            for(int i = 0; i < flightCount; i++)
            {
                graph.addEdge(0, i + 1, 1);
            }
            for (int i = (int)flightCount + 1; i < nodeCount - 1; i++) 
            {
                graph.addEdge(i, nodeCount-1 , 1);
            }
            for(int i = 0; i < flightCount; i++)
            {
                for(int j = 0; j <crewCount; j++)
                {
                    if (info[i][j] == 1)
                        graph.addEdge(i + 1, j + flightCount + 1, 1);
                }
            }

            long[] result = new long[flightCount];
            result = result.Select(d => (long)-1).ToArray();
            while (true)
            {
                long[] path = graph.ShortestPath(0, nodeCount - 1);
                if (path.Length == 0)
                {
                    for(int i = 1; i < flightCount + 1; i++)
                    {
                        for(int j = (int)flightCount; j < nodeCount - 1; j++)
                        {
                            if (graph.adj[i].Contains(j) && graph.res[i, j] == 0)
                                result[i - 1] = j - flightCount;
                        }
                    }
                    return result;
                }
                else
                {
                    long start = path[path.Length - 3] - 1;
                    long end = path[path.Length - 2] - flightCount;
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        graph.res[path[i], path[i + 1]]--;
                        graph.res[path[i + 1], path[i]]++;
                    }
                }
            }
        }
    }
}

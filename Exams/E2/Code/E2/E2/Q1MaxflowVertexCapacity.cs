using System;
using TestCommon;

namespace E2
{
    public class Q1MaxflowVertexCapacity : Processor
    {
        public Q1MaxflowVertexCapacity(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][],long[] , long, long, long>)Solve);

        public virtual long Solve(long nodeCount, 
            long edgeCount, long[][] edges, long[] nodeWeight, 
            long startNode , long endNode)
        {
            MyGraph demo = new MyGraph(nodeCount);
            foreach (var item in edges)
            {
                var value = Math.Min(nodeWeight[item[0] - 1], nodeWeight[item[1] - 1]);
                if (value < item[2])
                    demo.addEdge(item[0] - 1, item[1] - 1, value);
                else
                    demo.addEdge(item[0] - 1, item[1] - 1, item[2]);
            }
            long sum = 0;
            while (true)
            {
                long[] path = demo.ShortestPath(startNode-1, endNode - 1);
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
                        min = Math.Min(min, nodeWeight[path[i]]);
                        min = Math.Min(min, nodeWeight[path[i + 1]]);
                    }
                    bool flag = false;
                    for (int i = 0; i < path.Length; i++) 
                    {
                        if (nodeWeight[path[i]] <= 0)
                            flag = true;
                    }
                    if (!flag)
                    {
                        sum += min;
                        for (int i = 0; i < path.Length - 1; i++)
                        {
                            demo.res[path[i], path[i + 1]] -= min;
                            demo.res[path[i + 1], path[i]] += min;
                        }
                        foreach (var item in path)
                        {
                            nodeWeight[item] -= min;
                        }
                        foreach (var item in nodeWeight)
                        {
                            if (item < 0)
                                return sum-min;
                        }
                    }
                    else
                        return sum;

                }
            }
        }

    }
}

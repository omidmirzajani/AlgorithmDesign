using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adjListArray=new List<long>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adjListArray[i] = new List<long>();

            for (int i = 0; i < edges.Length; i++)
            {
                adjListArray[edges[i][0] - 1].Add(edges[i][1] - 1);
                adjListArray[edges[i][1] - 1].Add(edges[i][0] - 1);
            }
            return NumOfConnected(nodeCount,adjListArray);

        }
        public void dfs(int v, bool[] visited, List<long>[] adjListArray)
        {
            visited[v] = true;

            foreach (int x in adjListArray[v])
                if (!visited[x])
                    dfs(x, visited,adjListArray);

        }
        public long NumOfConnected(long nodeCount, List<long>[] adjListArray)
        {
            long t = 0;
            bool[] visited = new bool[nodeCount];
            for (int v = 0; v < nodeCount; ++v)
                if (!visited[v])
                {
                    dfs(v, visited,adjListArray);
                    t++;
                }
            return t;
        }
    }
}

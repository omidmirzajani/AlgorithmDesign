using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
   
        public class Q3Acyclic : Processor
        {
        public Q3Acyclic(string testDataName) : base(testDataName)
        {
            
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adj = new List<long>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adj[i] = new List<long>();

            for (int i = 0; i < edges.Length; i++)
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);

            return isAcyclic(nodeCount, adj);

        }
        public long isAcyclic(long nodeCount, List<long>[] adj)
        {
            bool[] visited = new bool[nodeCount];
            bool[] recuersive = new bool[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                if (!visited[i])
                    if (dfs(i, visited, recuersive, adj))
                        return 1;
            return 0;

        }
        public bool dfs(long v,bool[] visited,bool[] recuersive, List<long>[] adj)
        {
            visited[v] = true;
            recuersive[v] = true;
            foreach(long ad in adj[v])
            {
                if (!visited[ad])
                {
                    if (dfs(ad, visited, recuersive, adj))
                        return true;
                }
                else if (recuersive[ad])
                    return true;
                
            }
            recuersive[v] = false;
            return false;
        }

    }
}
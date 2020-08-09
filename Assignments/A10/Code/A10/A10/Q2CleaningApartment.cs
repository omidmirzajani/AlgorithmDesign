using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public String[] Solve(int V, int E, long[,] matrix)
        {
            List<long>[] adj = new List<long>[V + 1];
            for (int i = 1; i <= V; i++) { adj[i] = new List<long>(); }
            for(int i = 0; i < E; i++)
            {
                if(!adj[matrix[i,0]].Contains(matrix[i,1]))
                    adj[matrix[i, 0]].Add(matrix[i, 1]);
                if (!adj[matrix[i, 1]].Contains(matrix[i, 0]))
                    adj[matrix[i, 1]].Add(matrix[i, 0]);
            }
            List<string> result = new List<string>();
            long[] a = new long[V];
            for (int i = 1; i <= V; i++) 
            {
                for(int j = 0; j < V; j++)
                    a[j] = V * (i - 1) + j+1;
                for(int j=0;j<V;j++)
                    for(int k = j+1; k < V; k++)
                        result.Add($"{-a[j]} {-a[k]}");
                result.Add(string.Join(' ', a));
            }
            for(int i = 1; i <= V; i++)
            {
                for (int j = 0; j < V; j++)
                    a[j] = V * (j) + i;
                for (int j = 0; j < V; j++)
                    for (int k = j + 1; k < V; k++)
                        result.Add($"{-a[j]} {-a[k]}");
                result.Add(string.Join(' ', a));
            }
            for(int i = 1; i <= V; i++)
            {
                List<string> mine = new List<string>();
                for (int j = 0; j < V - 1; j++)
                {
                    mine.Clear();
                    mine.Add((-((i - 1) * V + j + 1)).ToString());
                    for(int k = 0; k < adj[i].Count; k++)
                        mine.Add(((adj[i][k] - 1) * V + (j + 2)).ToString());
                    result.Add(string.Join(' ', mine));
                }
            }
            List<string> res = new List<string> { $"{V * V} {result.Count}" };
            res.AddRange(result);
            return res.ToArray();
            
        }

    }
}

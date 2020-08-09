using System;
using System.Collections.Generic;

namespace A5
{
    public class Graph
    {
        public long V;
        public List<Tuple<long, char>>[] adj;
        public List<string> Q1ConstructTrie;
        public Graph(long v)
        {
            V = v;
            adj = new List<Tuple<long, char>>[v];
            Q1ConstructTrie = new List<string>();
            for (int i = 0; i < v; ++i)
                adj[i] = new List<Tuple<long, char>>();
        }
        public void addEdge(long start, long end, char weight)
        {
            adj[start].Add(new Tuple<long, char>(end, weight));
            Q1ConstructTrie.Add($"{start}->{end}:{weight}");
        }

    }
}

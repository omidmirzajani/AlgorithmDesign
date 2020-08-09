using System;
using System.Collections.Generic;

namespace A8
{
    public class MyGraph
    {
        public long V;
        public List<long>[] adj;
        public long[,] res;

        public MyGraph(long v)
        {
            V = v;
            adj = new List<long>[V];
            for(int i = 0; i < V; i++)
            {
                adj[i] = new List<long>();
            }
            res = new long[V, V];
        }

        public void addEdge(long i,long j,long w)
        {
            adj[i].Add(j);
            adj[j].Add(i);
            res[i, j] += w;
        }
        public long[] ShortestPath(long start, long end)
        {
            long[] parent = new long[V];
            long[] vis = new long[V];
            vis[start] = 1;
            Queue<long> q = new Queue<long>();
            q.Enqueue(start);
            long last = 1;
            for (int i = 0; i < V; i++)
            {
                Queue<long> qPrime = new Queue<long>();
                while (q.Count != 0)
                {
                    long node = q.Dequeue();
                    foreach (long t in adj[node])
                    {
                        if (vis[t] == 0 && res[node, t] != 0)
                        {
                            parent[t] = node;
                            qPrime.Enqueue(t);
                            vis[t] = last + 1;
                        }
                    }
                }
                last++;
                q = qPrime;
            }
            List<long> result = new List<long>();
            if (vis[end] != 0)
            {
                result.Add(end);
                while (end != start)
                {
                    end = parent[end];
                    result.Add(end);
                }
            }
            result.Reverse();
            return result.ToArray();
        }
        public long[] findPathDFS(long current, long goal, List<long> visited)
        {
            if (current == goal)
                return new long[] { current };


            //if current in graph:
            foreach(var node in adj[current])
            {
                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    long[] path = findPathDFS(node, goal, visited);
                    if (path.Length > 0)
                    {
                        List<long> t = new List<long>() { current };
                        t.AddRange(path);
                        return t.ToArray();
                    }
                }
            }
            return new long[0];
            //for neighbor in graph[current]:
            //        if neighbor not in visited:
            //visited.add(neighbor)
            //            path = __findPathDFS(graph, neighbor, goal, visited)


            //            if path is not None:
            //                path.insert(0, current)
            //                return path
        }
    }
}

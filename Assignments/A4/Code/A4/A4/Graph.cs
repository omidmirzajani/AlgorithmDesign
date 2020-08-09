using System;
using System.Collections.Generic;
using Priority_Queue;
using GeoCoordinatePortable;

namespace A4
{
    public class Graph
    {
        public long V;
        public List<Tuple<long, double>>[] adj;
        public double[][] edges;
        public double[][] points;
        SimplePriorityQueue<long, double> H ;
        public Graph(long v, double[][] ed, double[][] point)
        {
            V = v;
            edges = ed;
            points = point;
            adj = new List<Tuple<long, double>>[v];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<Tuple<long, double>>();
            H = new SimplePriorityQueue<long, double>();
        }
        public void addEdge(long start, long end, double weight)
        {
            adj[start].Add(new Tuple<long, double>(end, weight));
        }

        public double Dijkstra(long startNode, long endNode)
        {
            double[] dist = new double[V];
            for (int i = 0; i < V; i++)
                dist[i] = long.MaxValue;
            dist[startNode] = 0;
            for (int i = 0; i < V; i++)
                if (!H.Contains(i))
                    if (i != startNode)
                        H.Enqueue(i, dist[i]);
                    else
                        H.Enqueue(i, Distance(points[i], points[endNode]));
            while (H.Count > 0)
            {
                var u = H.Dequeue();
                if (dist[u] != long.MaxValue)
                    foreach (var edge in adj[u])
                    {
                        if (dist[edge.Item1] > dist[u] + edge.Item2)
                        {
                            if (H.Contains(edge.Item1))
                            {
                                dist[edge.Item1] = dist[u] + edge.Item2;
                                H.UpdatePriority(edge.Item1, dist[edge.Item1] + Distance(points[u], points[endNode]));
                            }
                        }
                    }
            }
            if (dist[endNode] != long.MaxValue)
                return dist[endNode];
            return -1;
        }

        private double minimum(List<double> toWeight)
        {
            double res = double.MaxValue;
            foreach (long e in toWeight)
                if (e < res)
                    res = e;
            return res;
        }

        private double Distance(double[] v1, double[] v2)
        {
            if (v1[0] == (int)v1[0])
            {
                double x = (v1[0] - v2[0]) * (v1[0] - v2[0]);
                double y = (v1[1] - v2[1]) * (v1[1] - v2[1]);
                double r = x + y;
                return Math.Pow(r, 0.5);
            }
            GeoCoordinate t1 = new GeoCoordinate(v1[1], v1[0]);
            GeoCoordinate t2 = new GeoCoordinate(v2[1], v2[0]);
            return t1.GetDistanceTo(t2);
        }
    }
}
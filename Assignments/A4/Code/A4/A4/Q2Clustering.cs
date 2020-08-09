using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using static A4.Q1BuildingRoads;

namespace A4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);

        public double Solve(long pointCount, long[][] points, long clusterCount)
        {
            Disjointset s = new Disjointset(pointCount);
            for (int o = 0; o < pointCount; o++)
                s.MakeSet(o);
            long len = pointCount * (pointCount - 1);
            len = Convert.ToInt64(len / 2);
            edge[] edges = new edge[len];
            
            int t = 0;
            List<double> mylist = new List<double>();
            for (int j = 0; j < pointCount; j++)
            {
                for (int k = j + 1; k < pointCount; k++)
                {
                    edges[t] = new edge();
                    
                    edges[t].j = j;
                    edges[t].k = k;
                    edges[t].dist = (Dist(points, j, k));

                    t++;
                }
            }
            edges = edges.OrderBy(d => d.dist).ToArray();

            List<double> weight = new List<double>() ;
            int i = 0;
            while (i != len)
            {
                var min = edges[i];
                if (s.FindSet(Convert.ToInt32(min.j)) != s.FindSet(Convert.ToInt32(min.k)))
                {
                    weight.Add(min.dist);
                    s.Union(Convert.ToInt32(min.j), Convert.ToInt32(min.k));
                }
                i++;
            }
            return (double)Math.Round(weight[(int)(pointCount - clusterCount)] * 1000000d) / 1000000d;
        }
        private double Dist(long[][] points, int j, int k)
        {
            long d1 = points[j][0] - points[k][0];
            long d2 = points[j][1] - points[k][1];
            return Math.Pow((d1 * d1 + d2 * d2), 0.5);
        }
    }
    public class edge
    {
        public long j;
        public long k;
        public double dist;

    }
}

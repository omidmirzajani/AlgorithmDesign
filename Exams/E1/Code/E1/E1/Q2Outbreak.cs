using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace E1
{
    public class Q2Outbreak : Processor
    {
        public Q2Outbreak(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string>)Solve);

        public static Tuple<int, int, int[,], int[,]> ProcessQ2(string[] data)
        {
            var temp = data[0].Split();
            int N = int.Parse(temp[0]);
            int M = int.Parse(temp[1]);
            int[,] carriers = new int[N, 2];
            int[,] safe = new int[M, 2];
            for (int i = 0; i < N; i++)
            {
                carriers[i, 0] = int.Parse(data[i + 1].Split()[0]);
                carriers[i, 1] = int.Parse(data[i + 1].Split()[1]);
            }

            for (int i = 0; i < M; i++)
            {
                safe[i, 0] = int.Parse(data[i + N + 1].Split()[0]);
                safe[i, 1] = int.Parse(data[i + N + 1].Split()[1]);
            }
            return Tuple.Create(N, M, carriers, safe);
        }
        public string Solve(string[] input)
        {
            var data = ProcessQ2(input);
            return Solve(data.Item1,data.Item2,data.Item3,data.Item4).ToString();
        }
        public double Solve(int M, int N, int[,] safe, int[,] carrier)
        {
            double max = -1;
            for(int i = 0; i < M; i++)
            {
                double min = long.MaxValue;
                for(int j = 0; j < N; j++)
                {
                    double d = Distance(safe[i, 0], safe[i, 1], carrier[j, 0], carrier[j, 1]);
                    min = Math.Min(min, d);
                }
                max = Math.Max(max, min);
            }
            max = (int)(max * 1000000);
            max = max / 1000000.0;
            return max;
        }

        private double Distance(int v1, int v2, int v3, int v4)
        {
            var v = (v3 - v1) * (v3 - v1);
            var v333 = (v4 - v2) * (v4 - v2);
            return Math.Sqrt(v + v333);
        }
    }
}

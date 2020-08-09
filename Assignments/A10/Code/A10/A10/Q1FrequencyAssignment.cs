using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {

        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            string[] res = new string[4 * V + 3 * E+1];
            res[0] = $"{3 * V} {4 * V + 3 * E}";
            int index = 1;
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < V; i++)
            {
                var a = i * 3 + 1;
                var b = i * 3 + 2;
                var c = i * 3 + 3;
                res[index++] = ($"{a} {b} {c}");
                res[index++] = ($"{-a} {-b}");
                res[index++] = ($"{-a} {-c}");
                res[index++] = ($"{-c} {-b}");
            }
            for (int i = 0; i < E; i++)
            {
                res[index++] = ($"{-(matrix[i, 0] * 3 - 2)} {-(matrix[i, 1] * 3 - 2)}");
                res[index++] = ($"{-(matrix[i, 0] * 3 - 1)} {-(matrix[i, 1] * 3 - 1)}");
                res[index++] = ($"{-(matrix[i, 0] * 3)} {-(matrix[i, 1] * 3)}");
            }
            return res;
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }
}

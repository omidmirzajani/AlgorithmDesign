using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A10
{
    public class Q3AdBudgetAllocation : Processor
    {
        public Q3AdBudgetAllocation(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < eqCount; ++i)
            {
                List<int> AllVari = new List<int>();
                for (int j = 0; j < A[i].Length; ++j)
                    if (A[i][j] != 0)
                        AllVari.Add(j);
                List<long> t = new List<long>();
                for (int j = 0; j < AllVari.Count; ++j)
                {
                    if (A[i][AllVari[j]] > b[i])
                    {
                        t.Clear();
                        for (int k = 0; k < AllVari.Count; ++k)
                            if (k == j)
                                t.Add(-AllVari[k] - 1);
                            else
                                t.Add(AllVari[k] + 1);
                        res.Add(string.Join(' ', t));
                    }
                    for (int l = j + 1; l < AllVari.Count; ++l)
                    {
                        if (A[i][AllVari[j]] + A[i][AllVari[l]] > b[i])
                        {
                            t.Clear();
                            for (int k = 0; k < AllVari.Count; ++k)
                                if (k == j || l == k)
                                    t.Add(-AllVari[k] - 1);
                                else
                                    t.Add(AllVari[k] + 1);
                            res.Add(string.Join(' ', t));
                        }
                    }
                }
                if (AllVari.Count == 3 && AllVari.Sum(x => A[i][x]) > b[i])
                {
                    t.Clear();
                    AllVari.ForEach(d => t.Add(-d - 1));
                    res.Add(string.Join(' ', t));
                }
                if (b[i] < 0)
                {
                    t.Clear();
                    AllVari.ForEach(d => t.Add(d + 1));
                    res.Add(string.Join(' ',t));
                }
            }
            List<string> result = new List<string>();
            result.Add($"{res.Count} {varCount}");
            result.AddRange(res);
            return result.ToArray();
        }
    }
}

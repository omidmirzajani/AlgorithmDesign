using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;
using System.Linq;

namespace E2
{
    public class Q2BoardGame : Processor
    {
        public Q2BoardGame(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, long[,], string[]>)Solve);

        public string[] Solve(int BoardSize, long[,] Board)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < BoardSize; i++)
            {
                StringBuilder s = new StringBuilder();
                for (int j = 0; j < BoardSize; j++)
                {
                    s.Append($"{Red(i,j,BoardSize)} ");
                    s.Append($"{Blue(i,j,BoardSize)} ");
                }
                s.Append("0");
                res.Add(s.ToString());
            }
            for (int i = 0; i < BoardSize; i++)
            {
                StringBuilder s = new StringBuilder();
                for (int j = 0; j < BoardSize; j++)
                {
                    s.Append($"{Red(j, i, BoardSize)} ");
                    s.Append($"{Blue(j, i, BoardSize)} ");
                }
                s.Append("0");
                res.Add(s.ToString());
            }
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    var value = i * BoardSize + j+1;
                    if (Board[i, j] != 1)
                    {
                        res.Add($"{value} {value + BoardSize * BoardSize * (Board[i, j] - 1)} 0");
                        res.Add($"-{value + BoardSize * BoardSize * (4 - (Board[i, j]))} 0");
                    }
                    else
                    {
                        res.Add($"{value} 0");
                        res.Add($"-{Blue(i, j, BoardSize)} 0");
                        res.Add($"-{Red(i, j, BoardSize)} 0");
                    }
                }
            }
            for(int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    for (int k = j+1; k < BoardSize; k++)
                    {
                        res.Add($"-{Blue(j, i, BoardSize)} -{Red(k, i, BoardSize)} 0");
                        res.Add($"-{Red(j, i, BoardSize)} -{Blue(k, i, BoardSize)} 0");
                    }
                }
            }
            var x = res.Count;
            res.Insert(0, $"{3 * BoardSize * BoardSize} {x}");
            return res.ToArray();
            // write your code here
            throw new NotImplementedException();            
        }

        private int Blue(int i, int j,int size)
        {
            return (i * size + j) + size * size+1;
        }
        private int Red(int i, int j, int size)
        {
            return (i * size + j) + 2 * size * size+1;
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}

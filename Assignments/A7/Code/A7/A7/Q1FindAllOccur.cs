using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        public static long[] Solve(string text, string pattern)
        {
            long [] res = FindAllOccurrences(text, pattern).ToArray();
            if (res.Length == 0)
                return new long[1] { -1 };
            return res;
            // write your code here
            throw new NotImplementedException();
        }

        private static List<long> FindAllOccurrences(string T, string P)
        {
            string S = P + "$" + T;
            long[] s = ComputePrefixFunction(S);
            List<long> res = new List<long>();
            for(int i = P.Length + 1; i < S.Length; i++)
            {
                if (s[i] == P.Length)
                    res.Add(i - 2 * P.Length);
            }
            return res;
        }

        private static long[] ComputePrefixFunction(string P)
        {
            long[] s = new long[P.Length];
            s[0] = 0;
            long border = 0;
            for(int i = 1; i < P.Length; i++)
            {
                while (border > 0 && P[i] != P[(int)border])
                    border = s[border - 1];
                if (P[i] == P[(int)border])
                    border++;
                else
                    border = 0;
                s[i] = border;
            }
            return s;
            throw new NotImplementedException();
        }
    }
}

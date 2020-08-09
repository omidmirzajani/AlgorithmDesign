using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public static long[] Solve(string text)
        {
            long[] res= BuildSuffixArray(text);
            return res;
        }
        public static long[] SortCharacters(string S)
        {
            long[] order = new long[S.Length];
            char[] sigma = new char[4] { 'A', 'C', 'G', 'T' };
            long[] count = new long[sigma.Length];
            for(int i=0;i<S.Length;i++)
            {
                int c = 4;
                if (S[i] == 'A')
                    c = 0;
                if (S[i] == 'C')
                    c = 1;
                if (S[i] == 'G')
                    c = 2;
                if (S[i] == 'T')
                    c = 3;
                if (c != 4)
                {
                    count[c]++;
                }
            }
            for(int j = 1; j < sigma.Length;j++)
            {
                count[j] += count[j - 1];
            }
            for(int i = S.Length - 1; i >= 0; i--)
            {
                int c = 4;
                if (S[i] == 'A')
                    c = 0;
                if (S[i] == 'C')
                    c = 1;
                if (S[i] == 'G')
                    c = 2;
                if (S[i] == 'T')
                    c = 3;
                if (c != 4)
                {
                    count[c]--;
                    order[count[c]+1] = i;
                }
                else
                    order[0] = i;
            }
            return order;
        }
        public static long[] ComputeCharClasses(string S, long[] order)
        {
            long[] myclass = new long[S.Length];
            myclass[order[0]] = 0;
            for(int i=1; i < S.Length; i++)
                if (S[(int)order[i]] != S[(int)order[i - 1]])
                    myclass[order[i]] = myclass[order[i - 1]] + 1;
                else
                    myclass[order[i]] = myclass[order[i - 1]];
            return myclass;
        }
        public static long[] SortDoubled(string S, long L, long[] order, long[] myclass)
        {
            long[] count = new long[S.Length];
            long[] newOrder = new long[S.Length];
            for(int i = 0; i < S.Length; i++)
            {
                count[myclass[i]]++;
            }
            for(int j = 1; j < S.Length; j++)
            {
                count[j] += count[j - 1];
            }
            for(int i = S.Length - 1; i >= 0; i--)
            {
                long start = order[i] - L + S.Length;
                start = start % S.Length;
                var cl = myclass[start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }
        public static long[] UpdateClasses(long[] newOrder, long[] myclass, long L)
        {
            long n = newOrder.Length;
            long[] newclass = new long[n];
            newclass[newOrder[0]] = 0;
            for(int i = 1; i < n; i++)
            {
                var cur = newOrder[i];
                var prev = newOrder[i - 1];
                var mid = cur + L;
                var midprev = (prev + L) % n;
                if ((myclass[cur] != myclass[prev]) || (myclass[mid] != myclass[midprev]))
                    newclass[cur] = newclass[prev] + 1;
                else
                    newclass[cur] = newclass[prev];
            }
            return newclass;
        }
        public static long[] BuildSuffixArray(string S)
        {
            var order = SortCharacters(S);
            var myclass = ComputeCharClasses(S, order);
            long L = 1;
            while (L < S.Length)
            {
                order = SortDoubled(S, L, order, myclass);
                myclass = UpdateClasses(order, myclass, L);
                L = 2 * L;
            }
            return order;   
        }
    }
}

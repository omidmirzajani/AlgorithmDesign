using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace E1
{
    public struct myTuple
    {
        public char s;
        public int i;

    }
    public class Q4Vaccine : Processor
    {
        public Q4Vaccine(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, string>)Solve);

        public string Solve(string dna, string pattern)
        {
            return "Not Solved";
            string bwt = BWT(dna+"$");
            long[] mismatch = new long[dna.Length+1];

            List<myTuple> first = new List<myTuple>();

            myTuple myTuple;

            for (int i = 0; i < bwt.Length; i++)
            {
                myTuple.i = i;
                myTuple.s = bwt[i];
                first.Add(myTuple);
            }
            List<myTuple> lastt = first;
            first = first.OrderBy(d => d.s).ToList();

            long[] firstaraay = new long[first.Count];
            for(int i = 0; i < firstaraay.Length; i++)
            {
                firstaraay[first[i].i] = i;
            }
            
            int last = pattern.Length-1;
            List<long> toSearch = new List<long>();
            for (int i = 0; i < mismatch.Length; i++)
                toSearch.Add(i);
            while (last >= 1)
            {
                foreach(int i in toSearch)
                {
                    if (mismatch[i] < 2)
                    {
                        if (first[i].s != pattern[last])
                            mismatch[i]++;
                    }
                }
                foreach (int i in toSearch)
                {
                    if (mismatch[i] < 2)
                    {
                        if (bwt[i] != pattern[last - 1])
                        {
                            mismatch[i]++;
                        }
                    }
                }
                toSearch.Clear();
                for(int i = 0; i < mismatch.Length; i++)
                {
                    if (mismatch[i] < 2)
                        toSearch.Add(firstaraay[i]);
                }
                last--;
            }
            long[] suff = BuildSuffixArray(dna);
            //List<long> result = new List<long>();
            //for(int i = 0; i <= dna.Length - pattern.Length; i++)
            //{
            //    if (mismatch(dna, i, pattern))
            //    {
            //        result.Add(i);
            //    }
            //}
            //if (result.Count == 0)
            //    return "No Match!";
            //return string.Join(" ", result.ToArray());

            throw new NotImplementedException();
        }

        private bool mismatch(string dna, int i, string pattern)
        {
            int k = 0;
            for(int j = i; j < i + pattern.Length; j++)
            {
                if (dna[j] != pattern[j - i])
                    k++;
                if (k == 2)
                    return false;
            }
            return true;
        }

        public string BWT(string text)
        {
            string[] matris = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                text = Rotate(text);
                matris[i] = text;
            }
            matris = matris.OrderBy(d => d).ToArray();
            string result = "";
            foreach (var t in matris)
            {
                result += t[t.Length - 1];
            }
            return result;
        }
        public string Rotate(string s)
        {
            return s[s.Length - 1] + s.Substring(0, s.Length - 1);
        }
        
        public static long[] SortCharacters(string S)
        {
            long[] order = new long[S.Length];
            char[] sigma = new char[4] { 'A', 'C', 'G', 'T' };
            long[] count = new long[sigma.Length];
            for (int i = 0; i < S.Length; i++)
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
            for (int j = 1; j < sigma.Length; j++)
            {
                count[j] += count[j - 1];
            }
            for (int i = S.Length - 1; i >= 0; i--)
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
                    order[count[c] + 1] = i;
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
            for (int i = 1; i < S.Length; i++)
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
            for (int i = 0; i < S.Length; i++)
            {
                count[myclass[i]]++;
            }
            for (int j = 1; j < S.Length; j++)
            {
                count[j] += count[j - 1];
            }
            for (int i = S.Length - 1; i >= 0; i--)
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
            for (int i = 1; i < n; i++)
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

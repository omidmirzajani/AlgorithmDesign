using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{

    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName)
        : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, String[] patterns)
        {
            List<myTuple> first = new List<myTuple>();

            myTuple myTuple;

            long a = 0;
            long c = 0;
            long g = 0;
            long t = 0;

            for (int i = 0; i < text.Length; i++)
            {
                myTuple.i = i;
                myTuple.s = text[i];
                if (myTuple.s == 'A')
                    a++;
                if (myTuple.s == 'C')
                    c++;
                if (myTuple.s == 'G')
                    g++;
                if (myTuple.s == 'T')
                    t++;
                first.Add(myTuple);
            }
            long aStart = 1;
            long cStart = 1 + a;
            long gStart = 1 + a + c;
            long tStart = 1 + a + c + g;

            Dictionary<char, int[]> count = new Dictionary<char, int[]>() { {'A', new int[text.Length + 1]},
                { 'C', new int[text.Length + 1] },
                { 'G', new int[text.Length + 1] },
                { 'T', new int[text.Length + 1] },
                { '$', new int[text.Length + 1] }
            };
            Count(text, count);
            long[] res = new long[n];
            for(int iter = 0; iter < n; iter++)
            {
                string pattern = patterns[iter];
                res[iter] = BetterBW(aStart, cStart, gStart, tStart, text, pattern, count);
                
            }
            return res;
        }

        private long BetterBW(long aStart, long cStart, long gStart, long tStart, string text, string pattern, Dictionary<char, int[]> count)
        {
            int j = pattern.Length - 1;
            int top = 0;
            int bottom = text.Length - 1;
            while (top <= bottom && j >= 0)
            {
                int firs;
                if (pattern[j] == 'A')
                    firs = (int)aStart;
                else if (pattern[j] == 'C')
                    firs = (int)cStart;
                else if (pattern[j] == 'G')
                    firs = (int)gStart;
                else
                    firs = (int)tStart;

                top = firs + count[pattern[j]][top];
                bottom = firs + count[pattern[j]][bottom + 1] - 1;
                j--;
            }
            if (top > bottom)
                return 0;
            else
                return (bottom - top + 1);
        }

        void Count(string bwt, Dictionary<char, int[]> count)
        {
            char[] characters = new char[5] { '$', 'A', 'C', 'G', 'T' };
            for (int i = 0; i < bwt.Length; ++i)
                foreach (var c in characters)
                    if (bwt[i] == c)
                        count[c][i + 1] = count[c][i] + 1;
                    else
                        count[c][i + 1] = count[c][i];
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using TestCommon;

//namespace A6
//{
//    public class Q3MatchingAgainCompressedString : Processor
//    {
//        public Q3MatchingAgainCompressedString(string testDataName) 
//        : base(testDataName) {
//            ExcludeTestCaseRangeInclusive(1, 8);
//        }

//        public override string Process(string inStr) =>
//        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

//        /// <summary>
//        /// Implement BetterBWMatching algorithm
//        /// </summary>
//        /// <param name="text"> A string BWT(Text) </param>
//        /// <param name="n"> Number of patterns </param>
//        /// <param name="patterns"> Collection of n strings Patterns </param>
//        /// <returns> A list of integers, where the i-th integer corresponds
//        /// to the number of substring matches of the i-th member of Patterns
//        /// in Text. </returns>
//        public long[] Solve(string text, long n, String[] patterns)
//        {
//            List<myTuple> first = new List<myTuple>();

//            myTuple myTuple;

//            long a = 0;
//            long c = 0;
//            long g = 0;
//            long t = 0;

//            for (int i = 0; i < text.Length; i++)
//            {
//                myTuple.i = i;
//                myTuple.s = text[i];
//                if (myTuple.s == 'A')
//                    a++;
//                if (myTuple.s == 'C')
//                    c++;
//                if (myTuple.s == 'G')
//                    g++;
//                if (myTuple.s == 'T')
//                    t++;
//                first.Add(myTuple);
//            }
//            long aStart = 1;
//            long cStart = 1 + a;
//            long gStart = 1 + a + c;
//            long tStart = 1 + a + c + g;

//            long[] res = new long[n];
//            for (int i = 0; i < n; i++)
//            {
//                string pattern = patterns[i];

//                bool flag = true;
//                int j = pattern.Length - 1;
//                long tedad = 0;

//                while(j>=1 && flag)
//                {
//                    flag = false;
//                    if (pattern[j] == 'A')
//                    {
//                        for(long k = aStart; k < cStart; k++)
//                        {
//                            if (first[(int)k].s == pattern[j - 1])
//                            {
//                                if (j == 1)
//                                    tedad++;
//                                flag = true;
//                            }
//                        }
//                        if (flag)
//                            j--;
//                    }
//                    else if (pattern[j] == 'C')
//                    {
//                        for (long k = cStart; k < gStart; k++)
//                        {
//                            if (first[(int)k].s == pattern[j - 1])
//                            {
//                                if (j == 1)
//                                    tedad++;
//                                flag = true;
//                            }
//                        }
//                        if (flag)
//                            j--;
//                    }
//                    else if (pattern[j] == 'G')
//                    {
//                        for (long k = gStart; k < tStart; k++)
//                        {
//                            if (first[(int)k].s == pattern[j - 1])
//                            {
//                                if (j == 1)
//                                    tedad++;
//                                flag = true;
//                            }
//                        }
//                        if (flag)
//                            j--;
//                    }
//                    else if (pattern[j] == 'T')
//                    {
//                        for (long k = tStart; k < text.Length; k++)
//                        {
//                            if (first[(int)k].s == pattern[j - 1])
//                            {
//                                if (j == 1)
//                                    tedad++;
//                                flag = true;
//                            }
//                        }
//                        if (flag)
//                            j--;
//                    }
//                }
//                res[i] = tedad;
//                if (pattern.Length == 1)
//                {
//                    if (pattern == "A")
//                        res[i] = a;
//                    if (pattern == "C")
//                        res[i] = c;
//                    if (pattern == "G")
//                        res[i] = g;
//                    if (pattern == "T")
//                        res[i] = t;
//                }
//            }

//            return res;

//            first = first.OrderBy(d => d.s).ToList();
//            throw new NotImplementedException();
//        }
//    }
//}

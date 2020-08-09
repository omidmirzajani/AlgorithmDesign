using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{
    public struct myTupleString
    {
        public string s;
        public long i;

    }
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public long[] Solve(string text)
        {
            myTupleString[] matris = new myTupleString[text.Length];

            myTupleString myTupleString;

            for (int i = 0; i < text.Length; i++)
            {
                text = Rotate(text);
                myTupleString.i = i;
                myTupleString.s = text;
                matris[i] = myTupleString;
            }
            matris = matris.OrderBy(d => d.s).ToArray();

            long[] res = new long[text.Length];
            for(int i = 0; i < res.Length; i++)
            {
                res[i] = text.Length - matris[i].i - 1;
            }
            return res;
        }
        public string Rotate(string s)
        {
            return s[s.Length - 1] + s.Substring(0, s.Length - 1);
        }
    }
}

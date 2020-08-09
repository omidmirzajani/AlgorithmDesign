using System;
using TestCommon;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace A6
{
    public struct myTuple
    {
        public char s;
        public long i;

    }
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName)
        : base(testDataName) { }
        

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string bwt)
        {
            List<myTuple> first = new List<myTuple>();

            myTuple myTuple;

            for (int i = 0; i < bwt.Length; i++)
            {
                myTuple.i = i;
                myTuple.s = bwt[i];
                first.Add(myTuple);
            }

            first = first.OrderBy(d => d.s).ToList();
            StringBuilder ss = new StringBuilder();
            long ind = 0;
            long tt = 0;
            long n = bwt.Length;
            while (tt != n)
            {
                ind = first[(int)ind].i;
                ss.Append(first[(int)ind].s);
                tt++;
            }
            char[] chararray = ss.ToString().ToCharArray();
            return new string(chararray);
        }
    }
}


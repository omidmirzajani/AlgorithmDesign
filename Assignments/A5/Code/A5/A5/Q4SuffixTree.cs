using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{

    public class Q4SuffixTree : Processor
    {
        public Q4SuffixTree(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String[]>)Solve);

        public string[] Solve(string text)
        {
            List<string> s= new SuffixTree(text).Q3();
            return s.ToArray();
        }
    }
}

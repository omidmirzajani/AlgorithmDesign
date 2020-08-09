using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace E1
{
    public class Q1GeneticMutation : Processor
    {
        public Q1GeneticMutation(string testDataName) : base(testDataName) { ExcludeTestCases(1, 2); }
        public override string Process(string inStr)
            => TestTools.Process(inStr, (Func<string, string, string>)Solve);


        static int no_of_chars = 256;

        public string Solve(string firstDNA, string secondDNA)
        {
            if (firstDNA.Length != secondDNA.Length)
                return "-1";
            for(int i = 0; i < firstDNA.Length+1; i++)
            {
                if (firstDNA == secondDNA)
                    return "1";
                firstDNA = Shift(firstDNA);
            }
            return "-1";
        }

        private string Shift(string s)
        {
            return s[s.Length - 1] + s.Substring(0, s.Length - 1);
        }
    }
}

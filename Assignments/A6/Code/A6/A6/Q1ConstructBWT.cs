using System;
using TestCommon;
using System.Linq;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string text)
        {
            string[] matris = new string[text.Length];
            for(int i=0;i<text.Length;i++)
            {
                text = Rotate(text);
                matris[i] = text;
            }
            matris = matris.OrderBy(d => d).ToArray();
            string result = "";
            foreach(var t in matris)
            {
                result += t[t.Length-1];
            }
            return result;
        }
        public string Rotate(string s)
		{
            return s[s.Length-1] + s.Substring(0, s.Length - 1);
		}
    }
}

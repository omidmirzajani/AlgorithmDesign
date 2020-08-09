using System;
using TestCommon;

namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {

        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(19,33);
            ExcludeTestCaseRangeInclusive(35, 37);
            ExcludeTestCaseRangeInclusive(39, 42);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

        public string Solve(int c, int v, double[,] matrix1)
        {
            return Q2OptimalDiet.Solve(c, v, matrix1);
        }
    }
}

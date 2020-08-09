using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    class Q5
    {
        public static int shortestSuperSequence(String X,
                                        String Y)
        {
            int m = X.Length;
            int n = Y.Length;

            // find lcs 
            int l = lcs(X, Y, m, n);

            // Result is sum of input string 
            // lengths - length of lcs 
            return (m + n - l);
        }

        // Returns length of LCS for 
        // X[0..m - 1], Y[0..n - 1] 
        public static int lcs(String X, String Y,
                            int m, int n)
        {
            int[,] L = new int[m + 1, n + 1];
            int i, j;

            // Following steps build L[m + 1][n + 1] 
            // in bottom up fashion.Note that 
            // L[i][j] contains length of LCS of 
            // X[0..i - 1] and Y[0..j - 1] 
            for (i = 0; i <= m; i++)
            {
                for (j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                        L[i, j] = 0;

                    else if (X[i - 1] == Y[j - 1])
                        L[i, j] = L[i - 1, j - 1] + 1;

                    else
                        L[i, j] = Math.Max(L[i - 1, j],
                                           L[i, j - 1]);
                }
            }

            // L[m][n] contains length of LCS 
            // for X[0..n - 1] and Y[0..m - 1] 
            return L[m, n];
        }
    }
    public class Q5ShortestNonSharedSubstring : Processor
    {
        public Q5ShortestNonSharedSubstring(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, String>)Solve);

        private string Solve(string text1, string text2)
        {
            //int n = text2.Length;
            //int x = text2.Length * (text2.Length+1);
            //x = (int)(x / 2);
            //string[] patterns = new string[x];
            //for(int i = 1; i <= text2.Length; i++)
            //{
            //    for (int j = 0; j <= text2.Length - i; j++) 
            //    {

            //    }
            //}
            //#region Implement Trie
            //long tt = 0;
            //for (int i = 0; i < n; i++)
            //{
            //    patterns[i] += "$";
            //    tt += patterns[i].Length;
            //}
            //Graph g = new Graph(tt);
            //long last = 1;
            //for (int i = 0; i < patterns[0].Length; i++)
            //{
            //    g.addEdge(i, i + 1, patterns[0][i]);
            //    last++;
            //}
            //for (int i = 1; i < n; i++)
            //{
            //    long check = 0;
            //    int j = 0;
            //    long t = myfunc(g.adj[check], patterns[i][j]);
            //    while (t != -1)
            //    {
            //        check = t;
            //        j++;
            //        if (j >= patterns[i].Length)
            //            t = -1;
            //        else
            //            t = myfunc(g.adj[check], patterns[i][j]);
            //    }
            //    for (long k = j; k < patterns[i].Length; k++)
            //    {
            //        g.addEdge(check, last++, patterns[i][(int)k]);
            //        check = last - 1;
            //    }
            //}
            //#endregion 
            throw new NotImplementedException();
        }
    }
}

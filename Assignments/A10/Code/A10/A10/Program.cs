using System;
using System.Collections.Generic;
using System.Text;

namespace A10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public static IEnumerable<string> Combine(int m, int n)
        {
            for (int i = (int)Math.Pow(2, n) - 1; i < Math.Pow(2, n) * Math.Pow(2, m - n); i++)
            {
                string binary = Convert.ToString(i, 2);

                if (HasN(binary, n))
                    yield return Zero(m - binary.Length) + binary;
            }
        }

        private static string Zero(int v)
        {
            StringBuilder t = new StringBuilder();
            for (int i = 0; i < v; i++)
                t.Append("0");
            return t.ToString();
        }

        private static bool HasN(string binary, int n)
        {
            int s = 0;
            foreach (char c in binary)
            {
                if (c == '1')
                    s++;
                if (s > n)
                    return false;
            }
            if (s == n)
                return true;
            else
                return false;
        }
    }
}

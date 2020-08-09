using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TestCommon;
using System.Drawing;
using System.Linq;

namespace E1
{
    public class Q3SeamCarving1 : Processor // Calculate Energy
    {
        public Q3SeamCarving1(string testDataName) : base(testDataName) { }

        public long len = 0;
        public long vv = 0;
        public override string Process(string inStr)
        {
            // Parse input file
            string[] lines = inStr.Split(new char[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries);
            long v = lines[0].Split('|').Length;
            len = lines.Length;
            vv = v;
            Color[,] data = new Color[lines.Length, v];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] t = lines[i].Split('|');
                for(int j = 0; j < v; j++)
                {
                    string[] s = t[j].Split(',');
                    Color redColor = Color.FromArgb(Convert.ToInt32(s[0]),
                        Convert.ToInt32(s[1]), Convert.ToInt32(s[2]));
                    data[i, j] = redColor;
                }
            }
            var solved = Solve(data);
            // convert solved into output string
            string res = "";
            for(int i = 0; i < lines.Length; i++)
            {

                for(int j = 0; j < v; j++)
                {
                    res += solved[i, j];
                    if (j == v - 1)
                        res += "\n";
                    else
                        res += ',';
                }
            }
            return res ;
        }
            

        public double[,] Solve(Color[,] data)
        {
            double[,] result = new double[len, vv];
            for(int i = 0; i < vv; i++)
            {
                result[0, i] = 1000;
                result[len - 1, i] = 1000;
            }
            for(int i = 0; i < len; i++)
            {
                result[i, 0] = 1000;
                result[i, vv - 1] = 1000;
            }
            for(int i = 0; i < len; i++)
            {
                for(int j = 0; j < vv; j++)
                {
                    if (result[i, j] == 0)
                    {
                        result[i, j] = Energy(data, i, j);
                        result[i,j] = (int)(result[i,j] * 1000);
                        result[i, j] = result[i, j] / 1000.0 + 0.001;
                    }
                }
            }
            return result;
        }

        public double Energy(Color[,] result, int i, int j)
        {
            return Math.Sqrt(Deltax(result, i, j) * Deltax(result, i, j) + Deltay(result, i, j) * Deltay(result, i, j));
        }

        private double Deltax(Color[,] result, int i, int j)
        {
            return Math.Sqrt(Rx(result, i, j) * Rx(result, i, j) +
                Gx(result, i, j) * Gx(result, i, j) +
                Bx(result, i, j) * Bx(result, i, j));
        }

        private double Bx(Color[,] result, int i, int j)
        {
            return result[i + 1, j].B - result[i - 1, j].B;
        }

        private int Gx(Color[,] result, int i, int j)
        {
            return result[i + 1, j].G - result[i - 1, j].G;
        }

        private int Rx(Color[,] result, int i, int j)
        {
            return result[i + 1, j].R - result[i - 1, j].R;
        }

        private double Deltay(Color[,] result, int i, int j)
        {
            return Math.Sqrt(Ry(result, i, j) * Ry(result, i, j) +
                Gy(result, i, j) * Gy(result, i, j) +
                By(result, i, j) * By(result, i, j));
        }

        private double By(Color[,] result, int i, int j)
        {
            return result[i, j + 1].B - result[i, j - 1].B;
        }

        private int Gy(Color[,] result, int i, int j)
        {
            return result[i, j + 1].G - result[i, j - 1].G;
        }

        private int Ry(Color[,] result, int i, int j)
        {
            return result[i, j + 1].R - result[i, j - 1].R;
        }


    }

    public class Q3SeamCarving2 : Processor // Find Seam
    {
        public long len = 0;
        public long vv = 0;
        public Q3SeamCarving2(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            // Parse input file
            string[] lines = inStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            double[,] data = new double[lines.Length, (int)(lines[0].Split(',').Length)];
            for(int i = 0; i < lines.Length; i++)
            {
                string[] t = lines[i].Split(',');
                for (int j = 0; j < lines[0].Split(',').Length; j++) 
                {
                    data[i, j] = Convert.ToDouble(t[j]);
                }
            }
            len = lines.Length;
            vv = (lines[0].Split(',').Length);
            var solved = Solve(data);
            // convert solved into output string
            string res = "";
            for(int i = 0; i < lines.Length; i++)
            {
                res += solved[i];
                if (i != lines.Length - 1)
                    res += ',';
            }
            res += "\n";
            for (int i = 0; i < (lines[0].Split(',').Length); i++)
            {
                res += solved[i + lines.Length];
                if (i != (lines[0].Split(',').Length) - 1)
                    res += ',';
            }
            res += "\n";
            return res;
        }


        public int[] Solve(double[,] data)
        {
            List<int> res = new List<int>();
            res.Add((int)vv - 2);
            int j = (int)vv - 2;
            for(int i = 0; i < len-1;i++)
            {

                if(data[i+1,j]<data[i+1,j+1] && data[i + 1, j] < data[i + 1, j - 1])
                {
                    j = j;
                }
                if (data[i + 1, j - 1] < data[i + 1, j + 1] && data[i + 1, j - 1] < data[i + 1, j]) 
                {
                    j = j-1;
                }
                if (data[i + 1, j+1] < data[i + 1, j ] && data[i + 1, j+1] < data[i + 1, j - 1])
                {
                    j = j+1;
                }

                res.Add((j));
            }

            res.Add(2);
            int ii = 2;
            for (int k = 0; k < vv - 1; k++)
            {

                if (data[ii, k + 1] < data[ii + 1, k + 1] && data[ii, k] < data[ii - 1, k + 1])
                {
                    ii = ii;
                }
                if (data[ii + 1, k + 1] < data[ii, k + 1] && data[ii + 1, k + 1] < data[ii - 1, k + 1])
                {
                    ii = ii + 1;
                }
                if (data[ii - 1, k + 1] < data[ii + 1, k + 1] && data[ii - 1, k + 1] < data[ii, k + 1])
                {
                    ii = ii - 1;
                }

                res.Add((ii));
            }
            return res.ToArray();
        }
    }

    public class Q3SeamCarving3 : Processor // Remove Seam
    {
        public Q3SeamCarving3(string testDataName) : base(testDataName) { }

        long len = 0;
        long vv = 0;
        long num = 0;
        List<long> which;
        long ver = 0;
        long hor = 0;
        public override string Process(string inStr)
        {
            // Parse input file
            string[] lines = inStr.Split(new char[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries);
            long n = Convert.ToInt64(lines[0]);
            double[,] data = new double[n, (int)(lines[1].Split(',').Length)];
            len = n;
            vv = lines[1].Split(',').Length;
            for (int i = 1; i < n; i++)
            {
                string[] t = lines[i].Split(',');
                for (int j = 0; j < lines[1].Split(',').Length; j++)
                {
                    data[i-1, j] = Convert.ToDouble(t[j]);
                }
            }



            if (lines[2 + n][0] == 'h')
            {
                hor = 1;
                ver = 0;
            }
            else
            {
                ver = 1;
                hor = 0;
            }
            which = lines[2 + n].Substring(2).Split(',').Select(d => Convert.ToInt64(d)).ToList();

            var solved = Solve(data);
            // convert solved into output string
            string res = "";
            if (ver == 1 )
            {
                vv--;
            }
            else if(hor==1)
            {
                len--;
            }
            for (int i = 0; i < len; i++)
            {
                for(int j = 0; j < vv; j++)
                {
                    if (solved[i, j] != 0)
                    {
                        res += solved[i, j];
                        if (!solved[i, j].ToString().Contains('.'))
                        {
                            res += ".00";
                        }
                        if (solved[i, j].ToString().IndexOf('.') == solved[i, j].ToString().Length - 2)
                            res += '0';

                        if (j != vv - 1)
                            res += ",";
                        else
                            res += "\n";
                    }
                    else
                    {
                        if (j == vv - 1)
                            res += "1000.00";
                        else
                            res += "1000.00,";
                    }
                }
            }
            return res;
        }


        public double[,] Solve(double[,] data)
        {
            double[,] res;
            if (ver == 1)
            {
                res = new double[len, vv - 1];
                for(int i = 0; i < len; i++)
                {
                    long k = 0;
                    for(int j = 0; j < vv; j++)
                    {
                        if (j != which[i])
                        {
                            res[i, k] = data[i, j] * 100;
                            res[i, k] = res[i, k] / 100.0;
                            k++;
                        }
                    }
                }
            }
            else
            {
                res = new double[len-1, vv];
                for(int j = 0; j < vv; j++)
                {
                    long k = 0;
                    for(int i = 0; i < len; i++)
                    {
                        if (i != which[j])
                        {
                            res[k, j] = data[i, j] * 100;
                            res[k, j] = res[k, j] / 100.0;
                            k++;
                        }
                    }
                }
            }
            return res;
            
            throw new NotImplementedException();
        }
    }
}

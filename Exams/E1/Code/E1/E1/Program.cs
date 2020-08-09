using System;
using System.Drawing;
using System.Collections.Generic;

namespace E1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            long a = 0;
            string[] t = new string[2] { "12 H", @"C:\git\AD98992\E1\E1.Tests\TestData\E1.TD3.4\In_1.jpg" };
            var v = Solve(t);
        }

        public static string[] Solve(string[] data)
        {
            int dimReduction = int.Parse(data[0].Split()[0]);
            char direction = char.Parse(data[0].Split()[1]);
            string imagePath = data[1];
            var img = Utilities.LoadImage(imagePath);
            var bmp = Utilities.ConvertImageToColorArray(img);
            var res = Solve(bmp, dimReduction, direction,img);
            Utilities.SavePhoto(res, imagePath, "../../../../asd", direction);
            return Utilities.ConvertColorArrayToRGBMatrix(res);
        }

        private static List<List<Color>> BuildList(Color[,] input, char dim)
        {
            throw new NotImplementedException();
        }
        public static double[,] Q1Solve(Color[,] data, int len, int vv)
        {
            Q3SeamCarving1 q31 = new Q3SeamCarving1("");
            double[,] result = new double[len, vv];
            for (int i = 0; i < vv; i++)
            {
                result[0, i] = 1000;
                result[len - 1, i] = 1000;
            }
            for (int i = 0; i < len; i++)
            {
                result[i, 0] = 1000;
                result[i, vv - 1] = 1000;
            }
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < vv; j++)
                {
                    if (result[i, j] == 0)
                    {
                        result[i, j] = q31.Energy(data, i, j);
                        result[i, j] = (int)(result[i, j] * 1000);
                        result[i, j] = result[i, j] / 1000.0 + 0.001;
                    }
                }
            }
            return result;
        }
        public static int[] Q2Solve(double[,] data,long vv,long len)
        {
            List<int> res = new List<int>();
            res.Add((int)vv - 2);
            int j = (int)vv - 2;
            for (int i = 0; i < len - 1; i++)
            {

                if (data[i + 1, j] < data[i + 1, j + 1] && data[i + 1, j] < data[i + 1, j - 1])
                {
                    j = j;
                }
                if (data[i + 1, j - 1] < data[i + 1, j + 1] && data[i + 1, j - 1] < data[i + 1, j])
                {
                    j = j - 1;
                }
                if (data[i + 1, j + 1] < data[i + 1, j] && data[i + 1, j + 1] < data[i + 1, j - 1])
                {
                    j = j + 1;
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
        public static Color[,] Solve(Color[,] input, int reduction, char direction,Image img)
        {
            Q3SeamCarving1 q31 = new Q3SeamCarving1("");
            double[,] Energy = Q1Solve(input,img.Height,img.Width);

            for (int k = 0; k < reduction; k++)
            {
                Q3SeamCarving2 q32 = new Q3SeamCarving2("");
                int[] seams = Q2Solve(Energy,img.Height,img.Width);
                #region Remove
                Color[,] res;

                long vv = img.Height;
                long len = img.Width;

                if (direction == 'H')
                {
                    res = new Color[len, vv - 1];
                    for (int i = 0; i < len; i++)
                    {
                        long kk = 0;
                        for (int j = 0; j < vv; j++)
                        {
                            if (j != seams[i])
                            {
                                res[i, kk] = input[i, j];
                                kk++;
                            }
                        }
                    }
                }
                else
                {
                    res = new Color[len - 1, vv];
                    for (int j = 0; j < vv; j++)
                    {
                        long kk = 0;
                        for (int i = 0; i < len; i++)
                        {
                            if (i != seams[j])
                            {
                                res[kk, j] = input[i, j] ;
                                kk++;
                            }
                        }
                    }
                }
                #endregion
            }

            return input;
        }



        // sequence of indices for horizontal seam
        public static int[] findHorizontalSeam(List<List<double>> energy)
        {
            throw new NotImplementedException();
        }


        // sequence of indices for vertical seam
        public static int[] findVerticalSeam(List<List<double>> energy)
        {
            throw new NotImplementedException();
        }

        // energy of pixel at column x and row y
        public static List<List<double>> ComputeEnergy(List<List<Color>> bmp)
        {
            throw new NotImplementedException();
        }

        public static int ArgMin(int t, int minIdx, List<List<double>> energy)
        {
            throw new NotImplementedException();
        }

        // remove horizontal seam from current picture
        public static void removeHorizontalSeam(int[] seam, ref List<List<Color>> bmp, ref List<List<double>> energy)
        {
            throw new NotImplementedException();
        }

        // remove vertical seam from current picture
        public static void removeVerticalSeam(int[] seam, ref List<List<Color>> bmp, ref List<List<double>> energy)
        {
            throw new NotImplementedException();
        }
    }
}

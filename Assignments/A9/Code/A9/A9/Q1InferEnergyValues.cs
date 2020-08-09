using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            for(int i = 0; i < MATRIX_SIZE; i++)
            {
                if (matrix[i, i] == 0)
                {
                    int idx = 0;
                    for(int k = i; k < MATRIX_SIZE; k++)
                    {
                        if(matrix[k,i]!=0)
                        {
                            idx = k;
                            break;
                        }
                    }
                    Swap(matrix, idx, i,MATRIX_SIZE);
                }
                for(int j = i + 1; j < MATRIX_SIZE; j++)
                {
                    if (matrix[i, i] == 0)
                    {
                        return new double[MATRIX_SIZE + 1];
                    }
                    double zarib = matrix[j, i] / matrix[i, i];
                    for (int k = 0; k <= MATRIX_SIZE; k++)
                        matrix[j, k] -= zarib * matrix[i, k];
                }
            }

            double[] result = new double[MATRIX_SIZE];
            for (long i = MATRIX_SIZE - 1; i >= 0; i--) 
            {
                double mul = Multiply(matrix, i, result);
                if (matrix[i, i] == 0)
                {
                    return new double[MATRIX_SIZE + 1];
                }
                result[i]=(matrix[i, MATRIX_SIZE] - mul) / matrix[i, i];
            }
            Relax(result);
            return result;
        }

        private void Swap(double[,] matrix, int j, int v,long MATRIX_SIZE)
        {
            for(int i = 0; i < MATRIX_SIZE+1; i++)
            {
                (matrix[j, i], matrix[v, i]) = (matrix[v, i], matrix[j, i]);
            }
        }

        private double Multiply(double[,] matrix, long i, double[] result)
        {
            double d = 0;
            for(int j = 0;j < result.Length; j++)
            {
                d += matrix[i, j] * result[j];
            }
            return d;
        }
        public void Relax(double[] vs)
        {
            for(int i = 0; i < vs.Length; i++)
            {
                double rest = Math.Abs(vs[i] - (int)vs[i]);
                if (rest < 0.25)
                    vs[i] = (int)(vs[i]);
                else if (rest > 0.75)
                    if (vs[i] >= 0)
                        vs[i] = (int)(vs[i]) + 1;
                    else
                        vs[i] = (int)(vs[i]) - 1;
                else
                    if (vs[i] >= 0)
                        vs[i] = (int)(vs[i]) + 0.5;
                    else
                        vs[i] = (int)(vs[i]) - 0.5;
            }
        }
    }
}

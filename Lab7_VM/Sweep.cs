using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_VM
{
    public static class Sweep
    {
        public static double[] SweepMatrix(double[,] matrixA)
        {
            var hight = matrixA.GetLength(0);
            var length = matrixA.GetLength(1);

            if (hight + 1 != length) throw new FormatException();

            int N1 = hight - 1;

            var y = 0.0;
            var A = new double[hight];
            var B = new double[hight];

            var matrixRes = new double[hight];

            y = matrixA[0, 0]; // верно
            A[0] = -matrixA[0, 1] / matrixA[0, 0]; // верно
            B[0] = matrixA[0, length - 1] / matrixA[0, 0]; // верно

            for (int i = 1; i < N1; i++)
            {
                y = matrixA[i, i] + matrixA[i, i - 1] * A[i - 1];
                A[i] = -matrixA[i, i + 1] / y;
                B[i] = (matrixA[i, length - 1] - matrixA[i, i - 1] * B[i - 1]) / y;
            }

            y = matrixA[N1, N1] + matrixA[N1, N1 - 1] * A[N1 - 1];
            B[N1] = (matrixA[N1, length - 1] - matrixA[N1, N1 - 1] * B[N1 - 1]) / y;

            matrixRes[N1] = B[N1];

            for (int j = N1 - 1; j != -1; j--)
            {
                matrixRes[j] = A[j] * matrixRes[j + 1] + B[j];
            }

            return matrixRes;
        }
    }
}
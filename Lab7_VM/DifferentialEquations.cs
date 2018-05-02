using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2_VM;

namespace Lab7_VM
{
    static class DifferentialEquations
    {
        public delegate double InputFunction(double x, double y);

        public delegate double BoundaryValueFunction(double x);

        static double PureFunc(double x) => -0.5 * Math.Cos(x) + 0.5 * Math.Sin(x) + 10.5 * Math.Exp(-x);

        public static IEnumerable<(double x, double y)> GetPureSolution(double x0,double y0, double h, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return (x0, y0);
                y0 += h * PureFunc(x0);
                x0 += h;
            }
        }

        /// <summary>
        /// Возвращает значения численного решения дифференциального уравнения, полученные методом Эйлера
        /// </summary>
        public static IEnumerable<(double x, double y)> GetPureEuler(InputFunction f, double x0, double y0, double h, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return (x0, y0);
                y0 += h * f(x0, y0);
                x0 += h;
            }
        }

        /// <summary>
        /// Возвращает значения численного решения дифференциального уравнения, полученные модифицированным методом Эйлера
        /// </summary>
        public static IEnumerable<(double x, double y)> GetModifiedEuler(InputFunction f, double x0, double y0, double h, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return (x0, y0);
                y0 += h * f(x0 + h / 2, y0 + h / 2 * f(x0, y0));
                x0 += h;
            }
        }

        /// <summary>
        /// Решает краевую задачу с заданными параметрами
        /// </summary>
        /// <param name="input">Структура, представляющая входные параметры</param>
        /// <param name="count">Количество значений</param>
        public static double[] GetBoundaryValue(BoundaryInput input, int count)
        {
            var boundaryMatrix = GetBoundaryMatrix(input, count);
            var outputArray = Sweep.SweepMatrix(boundaryMatrix);
            return outputArray;
        }

        private static double[,] GetBoundaryMatrix(BoundaryInput input, int count)
        {
            double h = (input.b - input.a) / count;
            int N = count - 1;

            double Alpha(int i) => i == N ? -input.d2 : 1 - (input.p(i * h) * h) / 2.0;
            double Beta(int i) => i == 0 ? h * input.c1 - input.c2 : i == N ? h * input.d1 + input.d2 : input.q(i * h) * h * h - 2;
            double Gamma(int i) => i == 0 ? input.c2 : 1 + input.p(i * h) * h / 2.0;
            double Phi(int i) => i == 0 ? h * input.c : i == N ? h * input.d : input.f(i * h) * h * h;

            var outputMatrix = new double[count, count + 1];

            for (int i = 0; i < count; i++)
            {
                if (i != 0) outputMatrix[i, i - 1] = Alpha(i);
                outputMatrix[i, i] = Beta(i);
                if (i != N) outputMatrix[i, i + 1] = Gamma(i);
                outputMatrix[i, count] = Phi(i);
            }

            return outputMatrix;
        }

        public struct BoundaryInput
        {
            public BoundaryValueFunction
                p, q, f;
            public double
                a, b,
                c, c1, c2,
                d, d1, d2;

            public BoundaryInput(
                BoundaryValueFunction p,
                BoundaryValueFunction q,
                BoundaryValueFunction f,
                double a,
                double b,
                double c, double c1, double c2,
                double d, double d1, double d2)
            {
                this.p = p;
                this.q = q;
                this.f = f;
                this.a = a;
                this.b = b;
                this.c = c; this.c1 = c1; this.c2 = c2;
                this.d = d; this.d1 = d1; this.d2 = d2;
            }
        }
    }
}
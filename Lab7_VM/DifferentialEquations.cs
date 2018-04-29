using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7_VM
{
    static class DifferentialEquations
    {
        public delegate double InputFunction(double x, double y);

        public static IEnumerable<(double x, double y)> GetPureEuler(InputFunction f, double x0, double y0, double h, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return (x0, y0);
                y0 += h * f(x0, y0);
                x0 += h;
            }
        }

        public static IEnumerable<(double x, double y)> GetModifiedEuler(InputFunction f, double x0, double y0, double h, int count)
        {
            for(var i = 0; i < count; i++)
            {
                yield return (x0, y0);
                y0 += h * f(x0 + h / 2, y0 + h / 2 * f(x0, y0));
                x0 += h / 2;
            }
        }
    }
}

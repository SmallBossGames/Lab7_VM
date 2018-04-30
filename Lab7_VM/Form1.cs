using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_VM
{
    public partial class Form1 : Form
    {
        double Fuc(double x, double y) => Math.Sin(x) - y;

        double PFunc(double x) => (1.0 + 2.0 * x) / (x * x - 1.0);

        double QFunc(double x) => 12.0 / Math.Sqrt(1 - x * x);

        public Form1()
        {
            foreach (var (x, y) in DifferentialEquations.GetModifiedEuler(Fuc, 0, 1000, 1.0/12.0, 61))
            {
                Console.WriteLine(x.ToString() + ' ' + y.ToString());
            }
            InitializeComponent();
        }


    }
}

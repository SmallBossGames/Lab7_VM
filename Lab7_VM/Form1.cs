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
        double Fuc(double x, double y) => 0.1 * y;

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

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
            DifferentialEquations.BoundaryInput input =
                new DifferentialEquations.BoundaryInput(
                    PFunc, QFunc, x => 0,
                    0, 0.8,
                    0, 1, 1,
                    0.1, 1, 1);

            var value = DifferentialEquations.GetBoundaryValue(input, 4);

            foreach (var (x, y) in DifferentialEquations.GetModifiedEuler(Fuc, 0, 1000, 1.0 / 12.0, 61))
            {
                Console.WriteLine(x.ToString() + ' ' + y.ToString());
            }
            InitializeComponent();
        }

        private void EulerButton_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();

            foreach (var (x, y) in DifferentialEquations.GetPureEuler(Fuc, Convert.ToDouble(XTextBox.Text), Convert.ToDouble(YTextBox.Text), Convert.ToDouble(HTextBox.Text), Convert.ToInt32(CountTextBox.Text)))
            {
                chart1.Series[0].Points.AddXY(x, y);
            }
        }

        private void ModifiedEulerButton_Click(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();

            foreach (var (x, y) in DifferentialEquations.GetModifiedEuler(Fuc, Convert.ToDouble(XTextBox.Text), Convert.ToDouble(YTextBox.Text), Convert.ToDouble(HTextBox.Text), Convert.ToInt32(CountTextBox.Text)))
            {
                chart1.Series[1].Points.AddXY(x, y);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comport
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double f = -2.12;
            double c = 0;
            if (Math.Abs(f) < 3)
            {
                f = f * -1;
                double a = 2.5;
                double b = 2.6;
                 c = Math.Abs(a - b);
                //Math.Abs(sdf);
            }
            textBox1.Text= c.ToString();
        }
    }
}

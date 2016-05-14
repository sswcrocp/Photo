using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Photo
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 from2 = new Form2();

            from2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 from3 = new Form3();
            from3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 from4 = new Form4();
            from4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 from1 = new Form1();
            from1.Show();
        }
    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Photo
{
    public partial class Form3 : Form
    {
        private Image img = null;
        private int scale = 1;
        public Form3()
        {
            InitializeComponent();
        }
  

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (img != null)
            {
                int left = (int)(e.X * (1.0 * img.Width / pictureBox1.Width) * scale);
                int top = (int)(e.Y * (1.0 * img.Height / pictureBox1.Height) * scale);

                left -= panel2.Width / 2;
                top -= panel2.Height / 2;

                pictureBox2.Left = -left;
                pictureBox2.Top = -top;

                pictureBox1.Refresh();
            }
            panel2.Top = e.Y - panel2.Height / 2;
            panel2.Left = e.X - panel2.Width / 2;
        }


        private void pictureBox1_DoubleClick(object sender, System.EventArgs e)
        {
            if (img == null)
            {
                return;
            }
            scale = 1;
            panel2.Width = 80;
            panel2.Height = 80;
            pictureBox2.Width = img.Width * scale;
            pictureBox2.Height = img.Height * scale;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "图片文件|*.jpg;*.bmp;*.gif;*.jpeg;*.png";
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            pictureBox1.Width = pictureBox1.Height = 500;
            try
            {
                img = Image.FromFile(openFileDialog1.FileName);
                if (img.Width < img.Height)
                {
                    pictureBox1.Width = (int)((1.0 * img.Width / img.Height) * pictureBox1.Height);
                }
                else
                {
                    pictureBox1.Height = (int)((1.0 * img.Height / img.Width) * pictureBox1.Width);
                }
                pictureBox1.Image = img;
                pictureBox2.Width = img.Width * scale;
                pictureBox2.Height = img.Height * scale;
                pictureBox2.Image = img;
            }
            catch (OutOfMemoryException ee)
            {
                MessageBox.Show("文件类型不对" + ee);
            }
            catch (Exception ee)
            {
                MessageBox.Show("不知名错误" + ee);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (img == null || scale > 10)
            {
                return;
            }
            scale++;
            panel2.Width += 10;
            panel2.Height += 10;
            pictureBox2.Width = img.Width * scale;
            pictureBox2.Height = img.Height * scale;
        }
    }
}

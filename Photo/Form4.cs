using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Photo
{
    public partial class Form4 : Form
    {
        private string[] fileNames;
        Thread thDraw;
        delegate void myDrawRectangel();
        myDrawRectangel mydraw;
        private Point ptBegin = new Point();
        private bool blIsDrawRectangle = false;
        public Form4()
        {
            InitializeComponent();
        }

        private void listViewImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewImage.SelectedItems.Count > 0)
            {
                pictureBox1.Image = Image.FromFile(fileNames[this.listViewImage.SelectedItems[0].Index]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "请选择文件";
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "所有文件(*.*)|*.*";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileNames = this.openFileDialog1.FileNames;
                int i = 0;
                foreach (string fileName in fileNames)
                {
                    imageList1.Images.Add(Image.FromFile(fileName));
                    listViewImage.Items.Add(fileName.Substring(fileName.LastIndexOf("\\") + 1), i++);
                }
            }

           
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            mydraw = new myDrawRectangel(ShowDrawRectangle);
            thDraw = new Thread(Run);
            thDraw.Start();
        }
        private void Run()
        {
            while (true)
            {
                if (pictureBox1.Image != null)
                {
                    this.BeginInvoke(mydraw);
                }
                Thread.Sleep(50);
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thDraw != null)
            {
                thDraw.Abort();
            }

        }

        private void ShowDrawRectangle()
        {
            int imageWidth = 476;
            imageWidth = pictureBox1.Image.Size.Width;
            int imageHeight = 674;
            imageHeight = pictureBox1.Image.Height;
            Rectangle rec = new Rectangle(ptBegin.X * pictureBox1.Image.Size.Width / imageWidth, ptBegin.Y * pictureBox1.Image.Size.Height / imageHeight,
                                           50 * pictureBox1.Image.Size.Width / imageWidth, 50 * pictureBox1.Image.Size.Height / imageHeight);
            Graphics g = pictureBox2.CreateGraphics();
            g.DrawImage(pictureBox1.Image, pictureBox2.ClientRectangle, rec, GraphicsUnit.Pixel);
            g.Flush();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            blIsDrawRectangle = false;
            pictureBox1.Refresh();

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            blIsDrawRectangle = true;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X - 25 <= 0)
            {
                ptBegin.X = 0;
            }
            else if (pictureBox1.Size.Width - e.X <= 25)
            {
                ptBegin.X = pictureBox1.Size.Width - 50;
            }
            else
            {
                ptBegin.X = e.X - 25;
            }

            if (e.Y - 25 <= 0)
            {
                ptBegin.Y = 0;
            }
            else if (pictureBox1.Size.Height - e.Y <= 25)
            {
                ptBegin.Y = pictureBox1.Size.Height - 50;
            }
            else
            {
                ptBegin.Y = e.Y - 25;
            }
            pictureBox1.Refresh();

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //画矩形 初始值为false否则 一开始就会显示矩形框
            if (blIsDrawRectangle)
            {
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), ptBegin.X, ptBegin.Y, 50, 50);
            }
        }

    }
}

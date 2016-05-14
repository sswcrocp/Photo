using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Photo
{
    public partial class Form1 : Form
    {

        private string[] fileNames;
        Bitmap maps;
        int a = 90, b = 90;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }
        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的图片</returns>
        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH, int Mode)
        {
            try
            {
                Bitmap map = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(map);
                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return map;
            }
            catch
            {
                return null;
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.pictureBox1.MouseWheel += new MouseEventHandler(PictureBox1_MouseWheel);
                maps = new Bitmap(fileNames[this.listView1.SelectedItems[0].Index]);
                maps = KiResizeImage(maps, 90, 90, 20);
                pictureBox1.Image = maps;
            }
        }
      

        private void pictureBox1_MousDown(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.Text = "放大";
                a = a+20; b = b+20;
                if (a < 90 || b < 90)
                {
                    a = 90; b = 90;
                }
                if (this.listView1.SelectedItems.Count > 0)
                {
                    maps = new Bitmap(fileNames[this.listView1.SelectedItems[0].Index]);
                    maps = KiResizeImage(maps, a, b, 20);
                    pictureBox1.Image = maps;
                }
            }
            else
            {
                this.Text = "缩小";
                a = a-20; b = b-20;
                if (a < 90 || b < 90)
                {
                    return;
                }
                if (this.listView1.SelectedItems.Count > 0)
                {
                    maps = new Bitmap(fileNames[this.listView1.SelectedItems[0].Index]);
                    maps = KiResizeImage(maps, a, b, 20);
                    pictureBox1.Image = maps;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            a = a - 20; b = b - 20;
            if (a < 90 || b < 90)
            {
                return;
            }
            if (this.listView1.SelectedItems.Count > 0)
            {
                maps = new Bitmap(fileNames[this.listView1.SelectedItems[0].Index]);
                maps = KiResizeImage(maps, a, b, 20);
                pictureBox1.Image = maps;
            }
        }


        private void button5_Click(object sender, EventArgs e)
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
                    listView1.Items.Add(fileName.Substring(fileName.LastIndexOf("\\") + 1), i++);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a=a+20; b=b+20;
            if (a < 90 || b < 90)
            {
                a = 90; b = 90;
            }
            if (this.listView1.SelectedItems.Count > 0)
            {
                maps = new Bitmap(fileNames[this.listView1.SelectedItems[0].Index]);
                maps = KiResizeImage(maps, a, b, 20);
                pictureBox1.Image = maps;
            }
        }
    }
}

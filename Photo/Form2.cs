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
    public partial class Form2 : Form
    {
        public static int i = 0;
        private Point m_ptStart = new Point(0, 0);
        private Point m_ptEnd = new Point(0, 0);
        private bool m_bMouseDown = false;
        private float xRate, yRate, realX1, realY1, realX2, realY2;
        int pLeft = 0;
        int pTop = 0;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            xRate = (float)pictureBox1.Image.Width / pictureBox1.Width;
            yRate = (float)pictureBox1.Image.Height / pictureBox1.Height;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲

        }

     
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.HasChildren)
            {
                for (int i = 0; i < pictureBox1.Controls.Count; i++)
                {
                    pictureBox1.Controls.RemoveAt(0);
                }
            }
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            m_ptEnd = new Point(e.X, e.Y);
            this.pictureBox1.Refresh();
            realX1 = e.X * xRate;
            realY1 = e.Y * yRate;
            if (!m_bMouseDown)
            {
                m_ptStart = new Point(e.X, e.Y);
                m_ptEnd = new Point(e.X, e.Y);
            }
            m_bMouseDown = !m_bMouseDown;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (m_ptEnd.X - m_ptStart.X < 0 || m_ptEnd.Y - m_ptStart.Y < 0)
            {
                return;
            }
            if (m_ptEnd.X - m_ptStart.X >= 100)
            {
                m_ptEnd.X = m_ptStart.X + 100;
            }
            if (m_ptEnd.Y - m_ptStart.Y >= 100)
            {
                m_ptEnd.Y = m_ptStart.Y + 100;
            }
            e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, m_ptStart.X, m_ptStart.Y, m_ptEnd.X - m_ptStart.X, m_ptEnd.Y - m_ptStart.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int eX = 0, eY = 0;
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (e.X - m_ptStart.X >= 100)
            {
                if (e.X >= pictureBox1.Width - 1)
                {
                    if (pictureBox1.Width - m_ptStart.X - 1 > 100)
                    {
                        eX = m_ptStart.X + 100;
                    }
                    else
                    {
                        eX = pictureBox1.Width - 1;
                    }
                }
                else
                {
                    eX = m_ptStart.X + 100;
                }
            }
            else
            {
                if (e.X >= pictureBox1.Width - 1)
                {
                    eX = pictureBox1.Width - 1;
                }
                else
                {
                    eX = e.X;
                }
            }
            if (e.Y - m_ptStart.Y >= 100)
            {
                if (e.Y >= pictureBox1.Height - 1)
                {
                    if (pictureBox1.Height - m_ptStart.Y - 1 > 100)
                    {
                        eX = m_ptStart.Y + 100;
                    }
                    else
                    {
                        eY = pictureBox1.Height - 1;
                    }
                }
                else
                {
                    eY = m_ptStart.Y + 100;
                }
            }
            else
            {
                if (e.Y >= pictureBox1.Height - 1)
                {
                    eY = pictureBox1.Height - 1;
                }
                else
                {
                    eY = e.Y;
                }
            }
            if (m_ptStart.X >= 0 && m_ptEnd.X >= 0
                 && m_ptStart.Y >= 0 && m_ptEnd.Y >= 0
                 && m_ptStart.X <= 254 && m_ptEnd.X <= 254
                 && m_ptStart.Y <= 163 && m_ptEnd.Y <= 163)
            {
                m_ptEnd = new Point(eX, eY);
                m_bMouseDown = !m_bMouseDown;
                this.pictureBox1.Refresh();
            }
            else
            {
                m_ptEnd = new Point(eX, eY);
                m_ptEnd = m_ptStart;
                m_bMouseDown = !m_bMouseDown;
                this.pictureBox1.Refresh();
            }
            realX2 = eX * xRate;
            realY2 = eY * yRate;
            Crop((Bitmap)pictureBox1.Image);
            Panel p = new Panel();
            p.Name = "panel1";
            p.Location = new Point((int)(realX1 / xRate), (int)(realY1 / yRate));
            p.Size = new Size((int)(realX2 / xRate - realX1 / xRate), (int)(realY2 / yRate - realY1 / yRate));
            //p.BackColor = Color.Transparent;
            p.BackColor = Color.FromArgb(100, 135, 206, 250);//Azure 240 255 255
            p.BorderStyle = BorderStyle.FixedSingle;
            p.MouseDown += (s1, e1) =>
            {
                pLeft = e1.X;
                pTop = e1.Y;
            };
            p.MouseMove += (s2, e2) =>
            {
                GC.Collect();
                if (e2.Button.ToString().Equals("Left"))
                {
                    if (p.Location.X + e2.X - pLeft <= 1)
                    {
                        p.Left = 1;
                    }
                    else if (p.Location.X + e2.X - pLeft >= pictureBox1.Width - p.Width)
                    {
                        p.Left = pictureBox1.Width - p.Width - 1;
                    }
                    else
                    {
                        p.Left = p.Location.X + e2.X - pLeft;
                    }
                    if (p.Location.Y + e2.Y - pTop <= 1)
                    {
                        p.Top = 1;
                    }
                    else if (p.Location.Y + e2.Y - pTop >= pictureBox1.Height - p.Height)
                    {
                        p.Top = pictureBox1.Height - p.Height - 1;
                    }
                    else
                    {
                        p.Top = p.Location.Y + e2.Y - pTop;
                    }
                }
                Crop((Bitmap)pictureBox1.Image, Convert.ToInt32(p.Location.X * xRate), Convert.ToInt32(p.Location.Y * yRate), Convert.ToInt32(p.Width), Convert.ToInt32(p.Height));
            };
            pictureBox1.Controls.Add(p);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            m_ptEnd = new Point(e.X, e.Y);
            this.pictureBox1.Refresh();
        }
        private void Crop(Bitmap bitmap)
        {
            if ((int)(realX2 - realX1) > 0 && (int)(realY2 - realY1) > 0)
            {
                GC.Collect();
                //GC.WaitForPendingFinalizers();
                Rectangle rec = new Rectangle((int)realX1, (int)realY1, (int)(realX2 - realX1), (int)(realY2 - realY1));
                pictureBox2.Image = bitmap.Clone(rec, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
        }
        private void Crop(Bitmap bitmap, int X, int Y, int width, int height)
        {
            if (width > 0 && height > 0)
            {
                Rectangle rec = new Rectangle(X, Y, width, height);
                try
                {
                    GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    pictureBox2.Image = bitmap.Clone(rec, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    //bitmap.Dispose();
                }
                catch (Exception ex)
                {
                    i++;
                }
                finally
                {
                }
            }
        }
    }
}
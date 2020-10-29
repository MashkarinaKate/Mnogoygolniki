using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace многоугольники_минизадача
{
    public partial class Form1 : Form
    {
        Вершина ob;
        Point p;
        int delx;
        int dely;
        bool dr;
        bool drag;
        int num;
        public Form1()
        {
            InitializeComponent();
            кругToolStripMenuItem.CheckOnClick = true;
            квадратToolStripMenuItem.CheckOnClick = true;
            треугольникToolStripMenuItem.CheckOnClick = true;
            кругToolStripMenuItem.Checked = true;
            dr = false;
            drag = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            if (dr)
            {
                ob.P = p;
                ob.Draw(e.Graphics);
            }
        }

        private void КругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            квадратToolStripMenuItem.Checked = false;
            треугольникToolStripMenuItem.Checked = false;
            num = 0;
        }

        private void КвадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            кругToolStripMenuItem.Checked = false;
            треугольникToolStripMenuItem.Checked = false;
            num = 1;
        }

        private void ТреугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            квадратToolStripMenuItem.Checked = false;
            кругToolStripMenuItem.Checked = false;
            num = 2;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (dr && ob.Check(e.X, e.Y))
            {
                if(e.Button == MouseButtons.Left)
                {
                    delx = e.X - ob.P.X;
                    dely = e.Y - ob.P.Y;
                    drag = true;
                }
                if(e.Button == MouseButtons.Right)
                {
                    dr = false;
                    ob = null;
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    switch (num)
                    {
                        case 0: ob = new Круг(p); break;
                        case 1: ob = new Квадрат(p); break;
                        case 2: ob = new Треугольник(p); break;
                        default: ob = new Круг(p); break;
                    }
                    dr = true;
                    p.X = e.X;
                    p.Y = e.Y;
                }
                dr = true;
            }
            this.Refresh();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                p.X = e.X - delx;
                p.Y = e.Y - dely;
                this.Refresh();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                drag = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }
    }
}

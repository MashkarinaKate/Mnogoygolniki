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
        Point p;
        int delx;
        int dely;
        bool dr;
        bool drag;
        int num;
        int i;

        List<Вершина> вершины;
        public Form1()
        {
            InitializeComponent();
            кругToolStripMenuItem.CheckOnClick = true;
            квадратToolStripMenuItem.CheckOnClick = true;
            треугольникToolStripMenuItem.CheckOnClick = true;
            кругToolStripMenuItem.Checked = true;
            вершины = new List<Вершина>();
            dr = false;
            drag = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (dr)
            {
                foreach(Вершина i in вершины)
                {
                    i.Draw(e.Graphics);
                }
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
            if (вершины.Count == 0) dr = false;
            i = -1;
            while (i < вершины.Count - 1)
            {
                i++;
                if (вершины[i].Check(e.X, e.Y))
                    break;
            }
            if (dr && вершины[i].Check(e.X, e.Y))
            {
                   if (e.Button == MouseButtons.Left)
                   {
                       delx = e.X - вершины[i].P.X;
                       dely = e.Y - вершины[i].P.Y;
                       drag = true;
                       вершины[i].P = new Point(e.X - delx, e.Y - dely);
                }
                   if (e.Button == MouseButtons.Right)
                   {
                       вершины[i] = null;
                       вершины.RemoveAt(i);
                   }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    switch (num)
                    {
                        case 0: вершины.Add(new Круг(p)); break;
                        case 1: вершины.Add(new Квадрат(p)); break;
                        case 2: вершины.Add(new Треугольник(p)); break;
                        default: вершины.Add(new Круг(p)); break;
                    }
                    i++;
                    вершины[i].P = new Point(e.X, e.Y);
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
                вершины[i].P = new Point(e.X, e.Y);
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

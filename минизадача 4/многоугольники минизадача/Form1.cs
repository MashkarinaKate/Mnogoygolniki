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
        bool dr;
        int num;

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
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (dr)
            {
                foreach(Вершина t in вершины)
                {
                    t.Draw(e.Graphics);
                }
            }
            if (вершины.Count > 2)
            {
                for (int i = 0; i < вершины.Count - 1; i++)
                {
                    for (int j = i + 1; j < вершины.Count; j++)
                    {
                        for (int k = 0; k < вершины.Count; k++)
                        {
                            if(k != i && k != j)
                            вершины[k].OneSide(вершины[i].X, вершины[i].Y, вершины[j].X, вершины[j].Y);
                        }
                        int s = 0;
                        foreach (var g in вершины)
                        {
                            s = s + g.Flag;
                        }
                        if (s == вершины.Count - 2 || s == 0)
                        {
                            e.Graphics.DrawLine(new Pen(Color.DarkSalmon), вершины[i].X, вершины[i].Y, вершины[j].X, вершины[j].Y);
                        }
                        foreach (var g in вершины)
                        {
                            g.Flag = 0;
                        }
                    }
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
            int size;
            if (вершины.Count == 0) size = 1;
            else size = вершины.Count;
            bool anychecked = false;
            for(int i = 0; i < вершины.Count; i++)
            {
                if (вершины[i].Check(e.X, e.Y))
                    anychecked = true;
            }
            if (dr && anychecked)
            {
                for (int i = 0; i < size; ++i)
                {
                    if (вершины[i].Check(e.X, e.Y))
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            вершины[i].Delx = e.X - вершины[i].X;
                            вершины[i].Dely = e.Y - вершины[i].Y;
                            вершины[i].Drag = true;
                        }
                        if (e.Button == MouseButtons.Right)
                        {
                            вершины[i] = null;
                            вершины.RemoveAt(i);
                            size--;
                        }
                    }
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    switch (num)
                    {
                        case 0: вершины.Add(new Круг(e.X, e.Y)); break;
                        case 1: вершины.Add(new Квадрат(e.X, e.Y)); break;
                        case 2: вершины.Add(new Треугольник(e.X, e.Y)); break;
                    }
                    dr = true;
                }
            }
            this.Refresh();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < вершины.Count; i++)
            {
                if (вершины[i].Drag && вершины[i].Check(e.X, e.Y))
                {
                    вершины[i].X = e.X - вершины[i].Delx;
                    вершины[i].Y = e.Y - вершины[i].Dely;
                    this.Refresh();

                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (var aPart in вершины)
            {
                if (aPart.Drag)
                {
                    aPart.Drag = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }
    }
}

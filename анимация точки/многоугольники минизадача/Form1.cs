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
        bool dr; //рисование вершины
        int num; // 1 - круг, 2 - квадрат, 3 - треугольник
        bool dance; // дерганье вершин
        Form2 f2 = new Form2();

        List<Вершина> вершины; // список с вершинами
        public Form1()
        {
            InitializeComponent();
            кругToolStripMenuItem.CheckOnClick = true;
            квадратToolStripMenuItem.CheckOnClick = true;
            треугольникToolStripMenuItem.CheckOnClick = true;
            кругToolStripMenuItem.Checked = true;
            вершины = new List<Вершина>();
            dr = false;
            dance = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (dr)
            {
                foreach (Вершина t in вершины)
                {
                    t.Draw(e.Graphics);
                    t.ISFig = false; // является ли вершина частью оболочки
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
                            if (k != i && k != j)
                            вершины[k].OneSide(вершины[i].X, вершины[i].Y, вершины[j].X, вершины[j].Y);
                        } // флаг == 0, если вершина лежит по одну сторону, флаг == 1, если по другую
                        int s = 0; // флаг == -1, если точка лежит на прямой
                        foreach (var g in вершины)
                        {
                            s = s + g.Flag; // сумма флагов
                        }
                        if (s == вершины.Count - 2 || s == 0) // если все точки, кроме 
                        {
                            e.Graphics.DrawLine(new Pen(Color.DarkSalmon), вершины[i].X, вершины[i].Y, вершины[j].X, вершины[j].Y);
                            вершины[i].ISFig = true;
                            вершины[j].ISFig = true;
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
                if (вершины.Count > 2 && Вершина.MoveOb(вершины, e.X, e.Y))
                {
                    for(int f = 0; f < вершины.Count; f++)
                    {
                        вершины[f].Delx = e.X - вершины[f].X;
                        вершины[f].Dely = e.Y - вершины[f].Y;
                        вершины[f].Drag = true;
                    }
                }
                else
                {
                        switch (num)
                        {
                            case 0: вершины.Add(new Круг(e.X, e.Y)); break;
                            case 1: вершины.Add(new Квадрат(e.X, e.Y)); break;
                            case 2: вершины.Add(new Треугольник(e.X, e.Y)); break;
                        }
                }
                dr = true;

            }
            this.Refresh();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < вершины.Count; i++)
            {
                if (вершины[i].Drag)
                {
                    вершины[i].X = e.X - вершины[i].Delx;
                    вершины[i].Y = e.Y - вершины[i].Dely;
                    this.Refresh();

                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            for(int i = 0; i < вершины.Count; i++)
            {
                if (вершины[i].Drag)
                {
                    вершины[i].Drag = false;
                    вершины[i].OldX = вершины[i].X;
                    вершины[i].OldY = вершины[i].Y;
                }
            }
            if (вершины.Count > 2)
            {
                for(int i = 0; i < вершины.Count; i++)
                {
                    if(!вершины[i].ISFig)
                    вершины.Remove(вершины[i]);
                }
            }
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void ЦветВершиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Вершина.Col = colorDialog1.Color;
            Refresh();
        }

        private void PlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dance = true;
            timer1.Start();
            foreach (Вершина t in вершины)
            {
                t.OldX = t.X;
                t.OldY = t.Y;
            }
            Refresh();
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dance = false;
            timer1.Stop();
            Refresh();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (dance && dr)
            {
                foreach (Вершина t in вершины)
                {
                    t.Dancing();
                }
            }
            Refresh();
        }

        private void ИзменитьTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f2.ShowDialog();
            timer1.Interval = f2.r;
        }
    }
}

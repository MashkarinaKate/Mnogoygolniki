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
    abstract class Вершина
    {
        protected static int r;
        protected int x;
        protected int y;
        protected int oldx;
        protected int oldy;
        protected int delx;
        protected int dely;
        protected bool drag;
        protected int flag;
        protected bool IsFig;
        protected static Color col;
        protected static Random rand = new Random();
        public static Color Col {get {return col; } set {col = value;}}
        public Вершина(int x, int y)
        {
            this.x = x;
            this.y = y;
            flag = 0;
            IsFig = false;
        }

        static Вершина()
        {
            r = 50;
            col = Color.Black;
        }
        public abstract void Draw(Graphics graf);
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int OldX { get { return oldx; } set { oldx = value; } }
        public int OldY { get { return oldy; } set { oldy = value; } }
        public int Delx { get { return delx; } set { delx = value; } }
        public int Dely { get { return dely; } set { dely = value; } }
        public int R { get { return r; } set { if (value > 0) { r = value; } } }
        public bool Drag { get { return drag; } set { drag = value;} }
        public bool ISFig { get { return IsFig; } set { IsFig = value; } }
        public void OneSide(int xi, int yi, int xj, int yj)
        {
            float d = (x - xi) * (yj - yi) - (y - yi) * (xj - xi);
            if(d > 0) flag = 1;
            if(d < 0) flag = 0;
            if (d == 0) flag = -1;
        }
        public int Flag { get { return flag; } set { flag = value; } }
        public abstract bool Check(int x, int y);
        public static bool MoveOb(List<Вершина> вершины, int ex, int ey)
        {
            Вершина mouse = new Круг(ex, ey);
            вершины.Add(mouse);
            List<Вершина> newвершины = new List<Вершина>();
            for (int i = 0; i < вершины.Count - 1; i++)
            {
                for (int j = i + 1; j < вершины.Count; j++)
                {
                    for (int k = 0; k < вершины.Count; k++)
                    {
                        if (k != i && k != j)
                            вершины[k].OneSide(вершины[i].X, вершины[i].Y, вершины[j].X, вершины[j].Y);
                    }
                    int s = 0;
                    foreach (var g in вершины)
                    {
                        s = s + g.Flag; 
                    }
                    if (s == вершины.Count - 2 || s == 0)
                    { 
                        newвершины.Add(вершины[i]);
                        newвершины.Add(вершины[j]);
                    }
                    foreach (var g in вершины)
                    {
                        g.Flag = 0;
                    }
                }
            }
            if (!newвершины.Contains(mouse))
            {
                вершины.Remove(mouse);
                return true;
            }
            вершины.Remove(mouse);
            return false;
        }
        public void Dancing()
        { 
            //int x1, y1;
            if((Math.Abs(x - oldx) < 5) && (Math.Abs(x - oldx) >= 0))
            {
                this.x -= rand.Next(0, 5);
            }
            else
            {
                this.x += rand.Next(0, 5);
            }
            if ((Math.Abs(y - oldy) <= 5) && (Math.Abs(y - oldy) >= 0))
            {
                this.y -= rand.Next(0, 5);
            }
            else
            {
                this.y += rand.Next(0, 5);
            }
            /*if (Math.Abs(x - oldx) < 5)
            {
                this.x += rand.Next(-5, 5);
            }
            else
            {
                x1 = rand.Next(-5, 5);
                while (Math.Abs((this.x + x1) - oldx) >= 5)
                {
                    x1 = rand.Next(-5, 5);
                }
                this.x += x1;
            }
            if (Math.Abs(y - oldy) < 5)
            {
                this.y += rand.Next(-5, 5);
            }
            else
            {
                y1 = rand.Next(-5, 5);
                while (Math.Abs((this.y + y1) - oldy) >= 5)
                {
                    y1 = rand.Next(-5, 5);
                }
                this.y += y1;
            }*/
        }
    }
    class Круг : Вершина
    {
        public Круг(int x, int y): base(x, y) { }
        public override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(col), x - r/2, y - r/2, r, r);
        }
        public override bool Check(int x, int y)
        {
            return (Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2) <= Math.Pow(r / 2, 2));
        }
    }
    class Квадрат : Вершина
    {
        public Квадрат(int x, int y) : base(x, y) { }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(col), x - r/2, y - r/2, r, r);
        }
        public override bool Check(int x, int y)
        {
            return (Math.Abs(this.x - x) <= r / 2 && Math.Abs(this.y - y) <= r / 2);
        }
    }
    class Треугольник : Вершина
    {
        public Треугольник(int x, int y) : base(x, y) { }
        public override void Draw(Graphics g)
        {
            Point[] s = new Point[3];
            s[0] = new Point(x, y - r / 2);
            s[1] = new Point(x - r/2, y + r / 2);
            s[2] = new Point(x + r/2, y + r / 2);
            g.FillPolygon(new SolidBrush(col), s);
        }
        public override bool Check(int x, int y)
        {
            int i = (this.x - x) * (this.y + r / 2 - (this.y - r / 2)) - (- r / 2 * (this.y - r / 2 - y));
            int j = 0 - (this.x + r/2 - (this.x - r/2))*(this.y + r/2 - y);
            int l = (this.x + r / 2 - x)*(this.y - r/2 - (this.y + r/2)) - (this.x - (this.x + r/2))*(this.y + r/2 - y);
            return ((i >= 0 && j >= 0 && l >= 0) || (i <= 0 && j <= 0 && l <= 0)); 
        }
    }
}

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
        protected Point p;
        protected static Color col;
        public Вершина(Point p)
        {
            this.p.X = p.X;
            this.p.Y = p.Y;
        }

        static Вершина()
        {
            r = 25;
            col = Color.Black;
        }
        public abstract void Draw(Graphics graf);
        public abstract Point P { get; set; }
        public abstract int R { get; set; }
        public abstract bool Check(int x, int y);
    }
    class Круг : Вершина
    {
        public Круг(Point p): base(p) { }
        public override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(col), p.X - r/2, p.Y - r/2, r, r);
        }
        public override bool Check(int x, int y)
        {
            if (Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2) <= Math.Pow(r, 2))
                return true;
            else return false;
        }
        public override Point P
        {
            get { return p; }
            set
            {
                p = value;
            }
        }
        public override int R
        {
            get { return r; }
            set
            {
                if (value > 0)
                {
                    r = value;
                }
            }
        }
    }
    class Квадрат : Вершина
    {
        public Квадрат(Point p) : base(p) { }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(col), p.X - r/2, p.Y - r/2, r, r);
        }
        public override bool Check(int x, int y)
        {
            if (Math.Abs(p.X - x) <= r && Math.Abs(p.Y - y) <= r) return true;
            else return false;
        }
        public override Point P
        {
            get { return p; }
            set
            {
                p = value;
            }
        }
        public override int R
        {
            get { return r; }
            set
            {
                if (value > 0)
                {
                    r = value;
                }
            }
        }
    }
    class Треугольник : Вершина
    {
        public Треугольник(Point p) : base(p) { }
        public override void Draw(Graphics g)
        {
            Point[] s = new Point[3];
            s[0] = new Point(p.X, p.Y - r / 2);
            s[1] = new Point(p.X - r/2, p.Y + r / 2);
            s[2] = new Point(p.X + r/2, p.Y + r / 2);
            g.FillPolygon(new SolidBrush(col), s);
        }
        public override bool Check(int x, int y)
        {
            int i = (p.X - x) * (p.Y + r / 2 - (p.Y - r / 2)) - (- r / 2 * (p.Y - r / 2 - y));
            int j = 0 - (p.X + r/2 - (p.X - r/2))*(p.Y + r/2 - y);
            int l = (p.X + r / 2 - x)*(p.Y - r/2 - (p.Y + r/2)) - (p.X - (p.X + r/2))*(p.Y + r/2 - y);
            if ((i >= 0 && j >= 0 && l >= 0) || (i <= 0 && j <= 0 && l <= 0))
                return true;
            else return false;
        }
        public override Point P
        {
            get { return p; }
            set
            {
                p = value;
            }
        }
        public override int R
        {
            get { return r; }
            set
            {
                if (value > 0)
                {
                    r = value;
                }
            }
        }
    }
}

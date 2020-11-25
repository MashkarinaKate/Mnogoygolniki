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
        protected int delx;
        protected int dely;
        protected static Color col;
        public Вершина(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static Вершина()
        {
            r = 25;
            col = Color.Black;
        }
        public abstract void Draw(Graphics graf);
        public abstract int R { get; set; }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Delx { get; set; }
        public abstract int Dely { get; set; }
        public abstract bool Check(int x, int y);
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
            if (Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2) <= Math.Pow(r, 2))
                return true;
            else return false;
        }
        public override int X
        {
            get { return x; }
            set { x = value; }
        }
        public override int Y
        {
            get { return y; }
            set { y = value; }
        }
        public override int Delx
        {
            get { return delx; }
            set { delx = value; }
        }
        public override int Dely
        {
            get { return dely; }
            set { dely = value; }
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
        public Квадрат(int x, int y) : base(x, y) { }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(col), x - r/2, y - r/2, r, r);
        }
        public override bool Check(int x, int y)
        {
            if (Math.Abs(this.x - x) <= r && Math.Abs(this.y - y) <= r) return true;
            else return false;
        }
        public override int X
        {
            get { return x; }
            set { x = value; }
        }
        public override int Y
        {
            get { return y; }
            set { y = value; }
        }
        public override int Delx
        {
            get { return delx; }
            set { delx = value; }
        }
        public override int Dely
        {
            get { return dely; }
            set { dely = value; }
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
            if ((i >= 0 && j >= 0 && l >= 0) || (i <= 0 && j <= 0 && l <= 0))
                return true;
            else return false;
        }
        public override int X
        {
            get { return x; }
            set { x = value; }
        }
        public override int Y
        {
            get { return y; }
            set { y = value; }
        }
        public override int Delx
        {
            get { return delx; }
            set { delx = value; }
        }
        public override int Dely
        {
            get { return dely; }
            set { dely = value; }
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

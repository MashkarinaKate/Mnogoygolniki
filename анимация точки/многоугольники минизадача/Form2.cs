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
    public partial class Form2 : Form
    {
        public int r { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            r = trackBar1.Value;
            if (trackBar1.Value == 0) r = 40;
        }
    }
}

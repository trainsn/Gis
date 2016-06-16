using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapControlApplication2
{
    public partial class RasterInput : Form
    {
        public RasterInput()
        {
            InitializeComponent();
        }

        private void plus_Click(object sender, EventArgs e)
        {
            RasterUtil rastUtil = new RasterUtil();
            rastUtil.RasterCalculatePlus(path1.Text, filename1.Text, filename1.Text, "plus.tif");
        }

        private void minus_Click(object sender, EventArgs e)
        {
            RasterUtil rastUtil = new RasterUtil();
            rastUtil.RasterCalculateMinus(path1.Text, filename1.Text, filename1.Text, "minus.tif");
        }

        private void multiply_Click(object sender, EventArgs e)
        {
            RasterUtil rastUtil = new RasterUtil();
            rastUtil.RasterCalculateMultiply(path1.Text, filename1.Text, filename1.Text, "multiply.tif");
        }

        private void RasterInput_Load(object sender, EventArgs e)
        {

        }
    }
}
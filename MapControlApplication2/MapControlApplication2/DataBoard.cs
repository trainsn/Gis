using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapControlApplication2
{
    public partial class DataBoard : Form
    {
        public DataBoard (string sDataName, DataTable dataTable)
        {
            //初始化窗体及控件
            InitializeComponent();

            //设置文本框中的文本和数据网络视图的数据源
            tbDataName.Text = sDataName;
            dataGridView1.DataSource = dataTable; 
        }

        private void DataBoard_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
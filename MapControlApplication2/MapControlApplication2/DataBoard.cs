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
            //��ʼ�����弰�ؼ�
            InitializeComponent();

            //�����ı����е��ı�������������ͼ������Դ
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
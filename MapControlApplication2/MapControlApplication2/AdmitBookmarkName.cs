using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapControlApplication2
{
    public partial class AdmitBookmarkName : Form
    {
        //���ڱ������������
        public MainForm m_frmMain;
        
        //���ڴ������������
        public AdmitBookmarkName(MainForm frm)
        {
            InitializeComponent();
            if (frm != null)
            {
                m_frmMain = frm;
            }  
        }

        //��ȷ�ϡ���ť�ġ�������¼���Ӧ���������ڴ�����ǩ
        private void btnAdmit_Click(object sender, EventArgs e)
        {   
            if (m_frmMain != null || tbBookmarkName.Text == "")
            {
                m_frmMain.CreateBookmark(tbBookmarkName.Text);
            }
            this.Close();
        }
    }
}
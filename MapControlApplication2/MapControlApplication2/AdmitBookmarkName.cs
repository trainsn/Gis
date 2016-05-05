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
        //用于保存主窗体对象
        public MainForm m_frmMain;
        
        //用于传入主窗体对象
        public AdmitBookmarkName(MainForm frm)
        {
            InitializeComponent();
            if (frm != null)
            {
                m_frmMain = frm;
            }  
        }

        //“确认”按钮的“点击”事件响应函数，用于创建书签
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
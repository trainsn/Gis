using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Output;

namespace MapControlApplication2
{
    public partial class DataBoard : Form
    {
        IMap m_map;
        string temp;
        
        //dataTable2用于条件表格的显示
        public DataBoard(string sDataName, DataTable dataTable1,DataTable dataTable2, IMap map)
        {
            //初始化窗体及控件
            InitializeComponent();

            //设置文本框中的文本和数据网络视图的数据源
            tbDataName.Text = sDataName;
            temp = sDataName;
            dataGridView1.DataSource = dataTable1;
            dataGridView2.DataSource = dataTable2;
            m_map=map;
        }

        private void DataBoard_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string oid=dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ObjectID"].Value.ToString();
            //MessageBox.Show(oid);

            MapAnalysis mapAnalysis = new MapAnalysis();
            IGeometry geometry = mapAnalysis.SearchByOid(tbDataName.Text, m_map, oid);            

            IActiveView pActiveView=(IActiveView)m_map;
            IEnvelope pEnvelope;
            if (geometry is IPoint)
            {
                IEnvelope currentEnv = pActiveView.Extent;
                IPoint point = (IPoint)geometry;
                currentEnv.CenterAt(point);
                pActiveView.Extent = currentEnv;
                m_map.MapScale = 5000000;   // to set the scale to 1:5000000                
            }
            else 
            {
                pEnvelope = geometry.Envelope;
                pEnvelope.Expand(1.2, 1.2, true);

                pActiveView.Extent = pEnvelope;
            }
            pActiveView.Refresh();
        }       

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void selected_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Visible == false)
            {
                dataGridView2.Visible = true;
                dataGridView1.Visible = false;
                tbDataName.Text = tbDataName.Text + "(selected)";
            }
            else
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
                tbDataName.Text = temp;
            }
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string oid = dataGridView2.Rows[dataGridView1.CurrentRow.Index].Cells["ObjectID"].Value.ToString();
            //MessageBox.Show(oid);

            MapAnalysis mapAnalysis = new MapAnalysis();
            IGeometry geometry = mapAnalysis.SearchByOid(tbDataName.Text, m_map, oid);


            IActiveView pActiveView = (IActiveView)m_map;
            IEnvelope pEnvelope;
            if (geometry is IPoint)
            {
                IEnvelope currentEnv = pActiveView.Extent;
                IPoint point = (IPoint)geometry;
                currentEnv.CenterAt(point);
                pActiveView.Extent = currentEnv;
                m_map.MapScale = 5000000;   // to set the scale to 1:100                
            }
            else
            {
                pEnvelope = geometry.Envelope;
                pEnvelope.Expand(1.2, 1.2, true);

                pActiveView.Extent = pEnvelope;
                //m_map.MapScale = 5000000;   // to set the scale to 1:100
            }
            pActiveView.Refresh();
        }

        private void miAnalysis_Click(object sender, EventArgs e)
        {
            DataOperator dataOperator = new DataOperator(m_map);
            DataBoard frmABN = new DataBoard("Statistics", dataOperator.StatisticContinents(), null, m_map);

            frmABN.Show();
        }
    }
}
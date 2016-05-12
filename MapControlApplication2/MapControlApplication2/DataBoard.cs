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
        
        public DataBoard(string sDataName, DataTable dataTable, IMap map)
        {
            //初始化窗体及控件
            InitializeComponent();

            //设置文本框中的文本和数据网络视图的数据源
            tbDataName.Text = sDataName;
            dataGridView1.DataSource = dataTable; 
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
            IGeometry geometry=mapAnalysis.SearchByOid("World Cities", m_map, oid);


            IActiveView pActiveView=(IActiveView)m_map;
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
            }
            pActiveView.Refresh();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
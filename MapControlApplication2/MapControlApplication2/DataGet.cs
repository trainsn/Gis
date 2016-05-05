using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

namespace MapControlApplication2
{
    public partial class DataGet : Form
    {
        public IMap m_map;
        
        
        public DataGet(IMap map)
        {
            InitializeComponent();
            m_map = map;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataOperator dataOperator = new DataOperator(m_map);
            DataBoard frmABN = new DataBoard("暴力膜蛤不可取", dataOperator.GetContinentsNames(textBox1.Text));

            frmABN.Show();
        }

        private void DataGet_Load(object sender, EventArgs e)
        {

        }

    }
}
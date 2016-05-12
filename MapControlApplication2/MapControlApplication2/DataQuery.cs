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
    public partial class DataQuery : Form
    {
        //保存当前地图对象
        //用于传入当前地图对象
        public IMap m_map;

        public DataQuery(IMap map)
        {
            InitializeComponent();
            m_map = map;


        }

        private void DataQuery_Load(object sender, EventArgs e)
        {
            //向框中添加图层名称
            cbLayer.Items.Clear();
            ILayer iLayer = null;
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                iLayer = m_map.get_Layer(i);
                string lyrName = iLayer.Name;
                //IFeatureLayer feaLayer = iLayer as IFeatureLayer;
                //IFeatureClass feaClass =  feaLayer.FeatureClass;
                cbLayer.Items.Add(lyrName);
            }
        }
    }
}
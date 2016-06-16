using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.AnalysisTools;

namespace MapControlApplication2
{
    class Extent
    {
        public void ExecuteGP()
        {
            //�����ʼ��һ�����������
            ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            gp.OverwriteOutput = true;

            //����һ��������������ִ�е�����
            IGeoProcessorResult results;

            //����һ����������������
            ESRI.ArcGIS.AnalysisTools.Buffer bufferTool = new ESRI.ArcGIS.AnalysisTools.Buffer();

            //���û���������
            bufferTool.in_features = @"D:\data\ushigh.shp";
            bufferTool.out_feature_class = @"D:\data\result.shp";
            bufferTool.buffer_distance_or_field = 1;

            //ִ�л���������
            gp.Execute(bufferTool, null);
        }
      
    }
}

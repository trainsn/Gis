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
            //定义初始化一个地理处理对象
            ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            gp.OverwriteOutput = true;

            //定义一个地理处理结果对象并执行地理处理
            IGeoProcessorResult results;

            //定义一个缓冲区分析工具
            ESRI.ArcGIS.AnalysisTools.Buffer bufferTool = new ESRI.ArcGIS.AnalysisTools.Buffer();

            //设置缓冲区参数
            bufferTool.in_features = @"D:\data\ushigh.shp";
            bufferTool.out_feature_class = @"D:\data\result.shp";
            bufferTool.buffer_distance_or_field = 1;

            //执行缓冲区分析
            gp.Execute(bufferTool, null);
        }
      
    }
}

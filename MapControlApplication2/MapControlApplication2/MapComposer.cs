using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;



namespace MapControlApplication2
{
    class MapComposer
    {
        public static String GetRendererTypeByLayer(ILayer layer)
        {
            if (layer == null)
            {
                return "获取图层失败";
            }
            IFeatureLayer featurelayer = layer as IFeatureLayer;
            IGeoFeatureLayer geofeaturelayer = layer as IGeoFeatureLayer;
            IFeatureRenderer featurerenderer = geofeaturelayer.Renderer;
            if (featurerenderer is ISimpleRenderer) 
            {
                return "SimpleRenderer";
            }
            else if(featurerenderer is IUniqueValueRenderer)
            {
                return "UniqueValueRenderer";
            }
            else if(featurerenderer is IDotDensityRenderer){
                return "DotDensityRender";
            }
            else if(featurerenderer is IChartRenderer){
                return "ChartRenderer";
            }
            else if(featurerenderer is IProportionalSymbolRenderer){
                return "ChartRenderer";
            }
            else if (featurerenderer is IRepresentationRenderer)
            {
                return "RepresentationRenderer";
            }
            else if (featurerenderer is IClassBreaksRenderer)
            {
                return "ClassBreaksRenderer";
            }
            else if (featurerenderer is IBivariateRenderer)
            {
                return "BivariateRenderer";
            }
            return "未知或渲染器获取失败";
        }

         public static ISymbol GetSymbolFromLayer(ILayer layer,string whereClause)
        {
            if (layer == null)
            {
                return null;
            }
        
            //利用IFeatureLayer接口访问指定图层，获取到图层中的第一个要素，判断
            //是否成功。若失败，函数返回空
            IQueryFilter queryFilter = null;
            if (whereClause != null)
            {
                  queryFilter.WhereClause = whereClause;
            }
            
            else
            {
                queryFilter = null;
            }
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureCursor featureCursor = featureLayer.Search(queryFilter, false);
            IFeature feature = featureCursor.NextFeature();
            if (feature == null)
            {
                return null;
            }
            //利用IGeoFeatureLayer访问指定图层，获取到图层中的第一个要素，判断
            //是否成功，若失败，函数返回空
            IGeoFeatureLayer geoFeatureLayer = featureLayer as IGeoFeatureLayer;
            IFeatureRenderer featureRenderer = geoFeatureLayer.Renderer;
            if (featureRenderer == null)
            {
                return null;
            }
            ISymbol symbol = featureRenderer.get_SymbolByFeature(feature);
            return symbol;
        }

        public static bool RenderSimply(ILayer layer, IColor color)
        {
            if (layer == null || color == null)
            {
                return false;
            }
            //调用GetSymbolFromLayer成员函数，获取指定图层的符号，并判断是否成功
            ISymbol symbol = GetSymbolFromLayer(layer,null);
            if (symbol == null)
            {
                return false;
            }
            //获取指定图层的要素类，并判断是否成功
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            if (featureClass == null)
            {
                return false;
            }
            //获取指定图层要素类的几何形状信息，并进行匹配。根据不同的形状，设置不同
            //类型符号的颜色，若几何形状不属于Poiny,MultiPoint,PolyLine,Polygon
            //函数返回false
            esriGeometryType geoType = featureClass.ShapeType;
            switch (geoType)
            {
                case esriGeometryType.esriGeometryPoint:
                    {
                        IMarkerSymbol markerSymbol = symbol as IMarkerSymbol;
                        markerSymbol.Color = color;
                        break;
                    }
                case esriGeometryType.esriGeometryMultipoint:
                    {
                        IMarkerSymbol markerSymbol = symbol as IMarkerSymbol;
                        markerSymbol.Color = color;
                        break;
                    }
                case esriGeometryType.esriGeometryPolyline:
                    {
                        IMarkerSymbol markerSymbol = symbol as IMarkerSymbol;
                        markerSymbol.Color = color;
                        break;
                    }
                case esriGeometryType.esriGeometryPolygon:
                    {
                        IMarkerSymbol markerSymbol = symbol as IMarkerSymbol;
                        markerSymbol.Color = color;
                        break;
                    }
                default:
                    return false;
            }
            //新建简单渲染对象，设置其符号
            ISimpleRenderer simpleRenderer = new SimpleRendererClass();
            simpleRenderer.Symbol = symbol;
            IFeatureRenderer featureRenderer = simpleRenderer as IFeatureRenderer;
            if (featureRenderer == null)
            {
                return false;
            }
            IGeoFeatureLayer geoFeatureLayer = featureLayer as IGeoFeatureLayer;
            geoFeatureLayer.Renderer = featureRenderer;
            return true;
        }
    }
}

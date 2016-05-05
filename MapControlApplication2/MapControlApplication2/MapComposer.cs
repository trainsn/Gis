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
                return "��ȡͼ��ʧ��";
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
            return "δ֪����Ⱦ����ȡʧ��";
        }

         public static ISymbol GetSymbolFromLayer(ILayer layer,string whereClause)
        {
            if (layer == null)
            {
                return null;
            }
        
            //����IFeatureLayer�ӿڷ���ָ��ͼ�㣬��ȡ��ͼ���еĵ�һ��Ҫ�أ��ж�
            //�Ƿ�ɹ�����ʧ�ܣ��������ؿ�
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
            //����IGeoFeatureLayer����ָ��ͼ�㣬��ȡ��ͼ���еĵ�һ��Ҫ�أ��ж�
            //�Ƿ�ɹ�����ʧ�ܣ��������ؿ�
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
            //����GetSymbolFromLayer��Ա��������ȡָ��ͼ��ķ��ţ����ж��Ƿ�ɹ�
            ISymbol symbol = GetSymbolFromLayer(layer,null);
            if (symbol == null)
            {
                return false;
            }
            //��ȡָ��ͼ���Ҫ���࣬���ж��Ƿ�ɹ�
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            if (featureClass == null)
            {
                return false;
            }
            //��ȡָ��ͼ��Ҫ����ļ�����״��Ϣ��������ƥ�䡣���ݲ�ͬ����״�����ò�ͬ
            //���ͷ��ŵ���ɫ����������״������Poiny,MultiPoint,PolyLine,Polygon
            //��������false
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
            //�½�����Ⱦ�������������
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

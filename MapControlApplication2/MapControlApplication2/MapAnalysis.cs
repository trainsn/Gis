using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;

namespace MapControlApplication2
{
    class MapAnalysis
    {
        public DataTable QueryIntersect(string srcLayerName,string tgtLayerName,IMap iMap,esriSpatialRelationEnum spatialRel/*string whereClausePolygon,string whereClausePoint*/)
        {
            DataOperator dataOperator = new DataOperator(iMap);

            //���岢����ͼ�����ƻ�ȡͼ�����
            IFeatureLayer iSrcLayer = (IFeatureLayer)dataOperator.GetLayerByName(srcLayerName);
            IFeatureLayer iTgtLayer = (IFeatureLayer)dataOperator.GetLayerByName(tgtLayerName);

            //ͨ����ѯ���˻�ȡContinents�������޵ļ���
            IGeometry geom;
            IFeature feature;
            IFeatureCursor featCursor;
            IFeatureClass srcFectClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "CONTINENT='Asia'";//���ò�ѯ����
            featCursor = iTgtLayer.FeatureClass.Search(queryFilter, false);
            feature = featCursor.NextFeature();
            geom = feature.Shape;//��ȡ����ͼ�μ���

            //������ѡ��ļ��ζԳ���ͼ�����������ռ����
            srcFectClass = iSrcLayer.FeatureClass;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = geom;
            spatialFilter.WhereClause = "POP_RANK=5";//�˿ڵȼ�����5�ĳ���
            spatialFilter.SpatialRel = (esriSpatialRelEnum)spatialRel;

            //����Ҫ��ѡ�������Ҫ������ͼ�����ʵ����
            IFeatureSelection featSelect = (IFeatureSelection)iSrcLayer;
            //�Կռ��������Ҫ�ؽ���ѡ�񣬲�������ѡ��
            featSelect.SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            return dataOperator.GetContinentsNamesSelect("World Cities", spatialFilter);
            //return true;
        }

        public DataTable QueryIntersect(string srcLayerName, string tgtLayerName, IMap iMap, esriSpatialRelationEnum spatialRel,string continent,string pop)
        {
            DataOperator dataOperator = new DataOperator(iMap);

            //���岢����ͼ�����ƻ�ȡͼ�����
            IFeatureLayer iSrcLayer = (IFeatureLayer)dataOperator.GetLayerByName(srcLayerName);
            IFeatureLayer iTgtLayer = (IFeatureLayer)dataOperator.GetLayerByName(tgtLayerName);

            //ͨ����ѯ���˻�ȡContinents�������޵ļ���
            IGeometry geom;
            IFeature feature;
            IFeatureCursor featCursor;
            IFeatureClass srcFectClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "CONTINENT='"+continent+"'";//���ò�ѯ����
            featCursor = iTgtLayer.FeatureClass.Search(queryFilter, false);
            feature = featCursor.NextFeature();
            geom = feature.Shape;//��ȡ����ͼ�μ���

            //������ѡ��ļ��ζԳ���ͼ�����������ռ����
            srcFectClass = iSrcLayer.FeatureClass;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = geom;
            spatialFilter.WhereClause = "POP_RANK="+pop;//�˿ڵȼ�����pop�ĳ���
            spatialFilter.SpatialRel = (esriSpatialRelEnum)spatialRel;

            //����Ҫ��ѡ�������Ҫ������ͼ�����ʵ����
            IFeatureSelection featSelect = (IFeatureSelection)iSrcLayer;
            //�Կռ��������Ҫ�ؽ���ѡ�񣬲�������ѡ��
            featSelect.SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            return dataOperator.GetContinentsNamesSelect("World Cities", spatialFilter);
            //return true;
        }

//         public void CreateGraphicBuffersAroundSelectedFeatures(ESRI.ArcGIS.Carto.IActiveView activeView, System.Double distance)
//         {
//             //parameter check
//             if (activeView == null || distance < 0)
//             {
//                 return;
//             }
//             ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;
//             // Clear any previous buffers from the screen
//             ESRI.ArcGIS.Carto.IGraphicsContainer graphicsContainer = (ESRI.ArcGIS.Carto.IGraphicsContainer)map; // Explicit Cast
//             graphicsContainer.DeleteAllElements();
// 
//             // Verify there is a feature(s) selected
//             if (map.SelectionCount == 0)
//             {
//                 return;
//             }
// 
//             // Reset to the first selected feature
//             ESRI.ArcGIS.Geodatabase.IEnumFeature enumFeature = (ESRI.ArcGIS.Geodatabase.IEnumFeature)map.FeatureSelection; // Explicit Cast
//             enumFeature.Reset();
//             ESRI.ArcGIS.Geodatabase.IFeature feature = enumFeature.Next();
// 
//             // Buffer all the selected features by the buffer distance and create a new polygon element from each result
//             ESRI.ArcGIS.Geometry.ITopologicalOperator topologicalOperator;
//             ESRI.ArcGIS.Carto.IElement element;
//             while (!(feature == null))
//             {
//                 topologicalOperator = (ESRI.ArcGIS.Geometry.ITopologicalOperator)feature.Shape; // Explicit Cast
//                 element = new ESRI.ArcGIS.Carto.PolygonElementClass();
//                 element.Geometry = topologicalOperator.Buffer(distance);
//                 graphicsContainer.AddElement(element, 0);
//                 feature = enumFeature.Next();
//             }
// 
//             activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGraphics, null, null);
//         }
        public DataTable Buffer(string layerName,string sWhere,double iSize,IMap map)
        {
            //���ݹ���������ȡ��������ΪŦԼ�ĳ���Ҫ�صļ���
            IFeatureClass featClass;
            IFeature feature;
            IGeometry iGeom;

            DataOperator dataOperator = new DataOperator(map);
            IFeatureLayer featLayer = (IFeatureLayer)dataOperator.GetLayerByName(layerName);

            featClass = featLayer.FeatureClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = sWhere;
            IFeatureCursor featCursor;
            featCursor = (IFeatureCursor)featClass.Search(queryFilter, false);
            int count = featClass.FeatureCount(queryFilter);

            feature = featCursor.NextFeature();
            iGeom = feature.Shape;

            //���ÿռ�Ļ�������Ϊ�ռ��ѯ�ļ��η�Χ
            ITopologicalOperator ipTO = (ITopologicalOperator)iGeom;
            IGeometry iGeomBuffer = ipTO.Buffer(iSize);

            //���ݻ��������ζԳ���ͼ����пռ����
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = iGeomBuffer;
            spatialFilter.WhereClause = "POP_RANK<=4";//�˿ڵȼ����ڵ���4�ĳ���            
            spatialFilter.SpatialRel = (esriSpatialRelEnum)esriSpatialRelationEnum.esriSpatialRelationIntersection;
            
            //����Ҫ��ѡ�����������Ҫ��ͼ�����ʵ����
            IFeatureSelection featSelect = (IFeatureSelection)featLayer;
            //�Կռ��������Ҫ�ؽ���ѡ�񣬲�������ѡ��
            featSelect.SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            return dataOperator.GetContinentsNamesSelect("World Cities", spatialFilter);
            //return true;

        }

        public IGeometry SearchByOid(string LayerName, IMap iMap,string oid/*string whereClausePolygon,string whereClausePoint*/)
        {
            IFeatureClass featClass;
            IFeature feature;
            IGeometry iGeom;

            DataOperator dataOperator = new DataOperator(iMap);
            IFeatureLayer featLayer = (IFeatureLayer)dataOperator.GetLayerByName(LayerName);

            featClass = featLayer.FeatureClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "ObjectID="+oid;
            IFeatureCursor featCursor;
            featCursor = (IFeatureCursor)featClass.Search(queryFilter, false);
            int count = featClass.FeatureCount(queryFilter);

            feature = featCursor.NextFeature();
            iGeom = feature.Shape;

            //����Ҫ��ѡ�����������Ҫ��ͼ�����ʵ����
            IFeatureSelection featSelect = (IFeatureSelection)featLayer;
            //�Կռ��������Ҫ�ؽ���ѡ�񣬲�������ѡ��
            featSelect.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            return iGeom;
        }

        public string Statistic(string layerName,string fieldName,IMap iMap)
        {
            //����ͼ����ָ������
            DataOperator dataOperator = new DataOperator(iMap);
            IFeatureLayer featLayer=(IFeatureLayer)dataOperator.GetLayerByName(layerName);
          
            //
            IFeatureClass featClass = featLayer.FeatureClass;
            IFeatureCursor featCursor = featClass.Search(null, false);
            IDataStatistics dataStatistics = new DataStatisticsClass();
            dataStatistics.Cursor = (ICursor)featCursor;
            dataStatistics.Field = fieldName;

            IStatisticsResults statResult = dataStatistics.Statistics;

            double dMax = statResult.Maximum;
            double dMin = statResult.Minimum;
            double dMean = statResult.Mean;

            string sResult;
            sResult = "������Ϊ" + dMax.ToString() + "��С���Ϊ" + dMin.ToString() + "ƽ�����Ϊ" + dMean.ToString();

            return sResult;           
        }
    }
}

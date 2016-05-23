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

            //定义并根据图层名称获取图层对象
            IFeatureLayer iSrcLayer = (IFeatureLayer)dataOperator.GetLayerByName(srcLayerName);
            IFeatureLayer iTgtLayer = (IFeatureLayer)dataOperator.GetLayerByName(tgtLayerName);

            //通过查询过滤获取Continents层中亚洲的几何
            IGeometry geom;
            IFeature feature;
            IFeatureCursor featCursor;
            IFeatureClass srcFectClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "CONTINENT='Asia'";//设置查询条件
            featCursor = iTgtLayer.FeatureClass.Search(queryFilter, false);
            feature = featCursor.NextFeature();
            geom = feature.Shape;//获取亚洲图形几何

            //根据所选择的几何对城市图层进行属性与空间过滤
            srcFectClass = iSrcLayer.FeatureClass;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = geom;
            spatialFilter.WhereClause = "POP_RANK=5";//人口等级等于5的城市
            spatialFilter.SpatialRel = (esriSpatialRelEnum)spatialRel;

            //定义要素选择对象，以要素搜索图层进行实例化
            IFeatureSelection featSelect = (IFeatureSelection)iSrcLayer;
            //以空间过滤器对要素进行选择，并建立新选择集
            featSelect.SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            return dataOperator.GetContinentsNamesSelect("World Cities", spatialFilter);
            //return true;
        }

        public DataTable QueryIntersect(string srcLayerName, string tgtLayerName, IMap iMap, esriSpatialRelationEnum spatialRel,string continent,string pop)
        {
            DataOperator dataOperator = new DataOperator(iMap);

            //定义并根据图层名称获取图层对象
            IFeatureLayer iSrcLayer = (IFeatureLayer)dataOperator.GetLayerByName(srcLayerName);
            IFeatureLayer iTgtLayer = (IFeatureLayer)dataOperator.GetLayerByName(tgtLayerName);

            //通过查询过滤获取Continents层中亚洲的几何
            IGeometry geom;
            IFeature feature;
            IFeatureCursor featCursor;
            IFeatureClass srcFectClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "CONTINENT='"+continent+"'";//设置查询条件
            featCursor = iTgtLayer.FeatureClass.Search(queryFilter, false);
            feature = featCursor.NextFeature();
            geom = feature.Shape;//获取亚洲图形几何

            //根据所选择的几何对城市图层进行属性与空间过滤
            srcFectClass = iSrcLayer.FeatureClass;
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = geom;
            spatialFilter.WhereClause = "POP_RANK="+pop;//人口等级等于pop的城市
            spatialFilter.SpatialRel = (esriSpatialRelEnum)spatialRel;

            //定义要素选择对象，以要素搜索图层进行实例化
            IFeatureSelection featSelect = (IFeatureSelection)iSrcLayer;
            //以空间过滤器对要素进行选择，并建立新选择集
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
            //根据过滤条件获取城市名称为纽约的城市要素的几何
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

            //设置空间的缓冲区作为空间查询的几何范围
            ITopologicalOperator ipTO = (ITopologicalOperator)iGeom;
            IGeometry iGeomBuffer = ipTO.Buffer(iSize);

            //根据缓冲区几何对城市图层进行空间过滤
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = iGeomBuffer;
            spatialFilter.WhereClause = "POP_RANK<=4";//人口等级大于等于4的城市            
            spatialFilter.SpatialRel = (esriSpatialRelEnum)esriSpatialRelationEnum.esriSpatialRelationIntersection;
            
            //定义要素选择对象，以搜索要素图层进行实例化
            IFeatureSelection featSelect = (IFeatureSelection)featLayer;
            //以空间过滤器对要素进行选择，并建立新选择集
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

            //定义要素选择对象，以搜索要素图层进行实例化
            IFeatureSelection featSelect = (IFeatureSelection)featLayer;
            //以空间过滤器对要素进行选择，并建立新选择集
            featSelect.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            return iGeom;
        }

        public string Statistic(string layerName,string fieldName,IMap iMap)
        {
            //根据图层获得指定对象
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
            sResult = "最大面积为" + dMax.ToString() + "最小面积为" + dMin.ToString() + "平均面积为" + dMean.ToString();

            return sResult;           
        }
    }
}

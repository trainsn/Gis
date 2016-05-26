using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;


namespace MapControlApplication2
{
    class RasterUtil
    {
        IRasterWorkspaceEx OpenRasterWorkspaceFromFileGDB(string filePath)
        {
            IWorkspaceFactory wsFactory=new RasterWorkspaceFactoryClass();
            IRasterWorkspaceEx ws = (IRasterWorkspaceEx)wsFactory.OpenFromFile(filePath, 0);
            return ws;
        }

        public bool CreateRaster(string filePath, string rasterName)
        {
            //打开工作空间
            IRasterWorkspaceEx rasterWorkspaceEx;
            rasterWorkspaceEx = /*(IRasterWorkspaceEx)*/OpenRasterWorkspaceFromFileGDB(filePath);

            //设置存储参数
            IRasterStorageDef storageDef = new RasterStorageDefClass();
            storageDef.CompressionType = esriRasterCompressionType.esriRasterCompressionJPEG;

            //设置栅格列属性
            IRasterDef rasterDef = new RasterDefClass();

            //定义空间参考
            ISpatialReferenceFactory2 srFactory = new SpatialReferenceEnvironmentClass();
            int gcsType = (int)esriSRGeoCSType.esriSRGeoCS_WGS1984;
            IGeographicCoordinateSystem geoCoordSystem = srFactory.CreateGeographicCoordinateSystem(gcsType);
            ISpatialReference spatialRef = (ISpatialReference)geoCoordSystem;
            rasterDef.SpatialReference = (ISpatialReference)geoCoordSystem;
            
            //创建栅格数据集
            IRasterDataset rasterDataset;
            rasterDataset = rasterWorkspaceEx.CreateRasterDataset(rasterName, 3, rstPixelType.PT_FLOAT, storageDef, null, rasterDef, null);
            return true;
        }

        //影像镶嵌，讲catalogName中所有栅格影像镶嵌成单个影像，并以outputName为文件名保存至outputFolder
        public void Mosaic(string GDBName,string catalogName,string outputFolder,string outputName)
        {
            //打开个人数据库
            IWorkspaceFactory workspaceGDBFactory = new AccessWorkspaceFactoryClass();
            IWorkspace GDBworkspace = workspaceGDBFactory.OpenFromFile(GDBName, 0);

            //打开要被镶嵌的影像所在的栅格目录
            IRasterWorkspaceEx rasterWorkspaceEx = (IRasterWorkspaceEx)GDBworkspace;
            IRasterCatalog rasterCatalog;
            rasterCatalog = rasterWorkspaceEx.OpenRasterCatalog(catalogName);

            //定义一个影像镶嵌对象
            IMosaicRaster mosaicRaster = new MosaicRasterClass();
            //镶嵌栅格目录中的所有影像输出到一个栅格数据集
            mosaicRaster.RasterCatalog = rasterCatalog;

            //设置镶嵌选项
            mosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_MATCH;
            mosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_LAST;

            //打开输出栅格数据集所在的工作空间
            IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();
            IWorkspace workspace = workspaceFactory.OpenFromFile(outputFolder, 0);

            //保存目标栅格数据集
            ISaveAs saveas = (ISaveAs)mosaicRaster;
            saveas.SaveAs(outputName, workspace, "TIFF");

        }
    }
}

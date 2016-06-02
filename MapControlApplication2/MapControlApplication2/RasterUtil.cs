using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.GeoAnalyst;


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
            //�򿪹����ռ�
            IRasterWorkspaceEx rasterWorkspaceEx;
            rasterWorkspaceEx = /*(IRasterWorkspaceEx)*/OpenRasterWorkspaceFromFileGDB(filePath);

            //���ô洢����
            IRasterStorageDef storageDef = new RasterStorageDefClass();
            storageDef.CompressionType = esriRasterCompressionType.esriRasterCompressionJPEG;

            //����դ��������
            IRasterDef rasterDef = new RasterDefClass();

            //����ռ�ο�
            ISpatialReferenceFactory2 srFactory = new SpatialReferenceEnvironmentClass();
            int gcsType = (int)esriSRGeoCSType.esriSRGeoCS_WGS1984;
            IGeographicCoordinateSystem geoCoordSystem = srFactory.CreateGeographicCoordinateSystem(gcsType);
            ISpatialReference spatialRef = (ISpatialReference)geoCoordSystem;
            rasterDef.SpatialReference = (ISpatialReference)geoCoordSystem;
            
            //����դ�����ݼ�
            IRasterDataset rasterDataset;
            rasterDataset = rasterWorkspaceEx.CreateRasterDataset(rasterName, 3, rstPixelType.PT_FLOAT, storageDef, null, rasterDef, null);
            return true;
        }

        //Ӱ����Ƕ����catalogName������դ��Ӱ����Ƕ�ɵ���Ӱ�񣬲���outputNameΪ�ļ���������outputFolder
        public void Mosaic(string GDBName,string catalogName,string outputFolder,string outputName)
        {
            //�򿪸������ݿ�
            IWorkspaceFactory workspaceGDBFactory = new AccessWorkspaceFactoryClass();
            IWorkspace GDBworkspace = workspaceGDBFactory.OpenFromFile(GDBName, 0);

            //��Ҫ����Ƕ��Ӱ�����ڵ�դ��Ŀ¼
            IRasterWorkspaceEx rasterWorkspaceEx = (IRasterWorkspaceEx)GDBworkspace;
            IRasterCatalog rasterCatalog;
            rasterCatalog = rasterWorkspaceEx.OpenRasterCatalog(catalogName);

            //����һ��Ӱ����Ƕ����
            IMosaicRaster mosaicRaster = new MosaicRasterClass();
            //��Ƕդ��Ŀ¼�е�����Ӱ�������һ��դ�����ݼ�
            mosaicRaster.RasterCatalog = rasterCatalog;

            //������Ƕѡ��
            mosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_MATCH;
            mosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_LAST;

            //�����դ�����ݼ����ڵĹ����ռ�
            IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();
            IWorkspace workspace = workspaceFactory.OpenFromFile(outputFolder, 0);

            //����Ŀ��դ�����ݼ�
            ISaveAs saveas = (ISaveAs)mosaicRaster;
            saveas.SaveAs(outputName, workspace, "TIFF");

        }

        public void RasterCalculateMinus(string filePath,string rasterName,string rasterName2,string outputName)
        {
            //���Ƶ�ͼ�����Ĳ���
            IMapAlgebraOp mapAlgebraOp;
            mapAlgebraOp = new RasterMapAlgebraOpClass();

            //����raster�����Ļ���
            IRasterAnalysisEnvironment rasterAnalysisEnvironment = default(IRasterAnalysisEnvironment);
            rasterAnalysisEnvironment = (IRasterAnalysisEnvironment)mapAlgebraOp;
            
            IWorkspace workspace;
            IRasterWorkspace rasterWorkspaceEx;

            //���ù����ռ�
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            workspace = workspaceFactory.OpenFromFile(filePath, 0);
            rasterWorkspaceEx = (IRasterWorkspace)workspace;
            rasterAnalysisEnvironment.OutWorkspace = workspace;

            //��դ�����ݼ�
            IRasterDataset rasterDataset = rasterWorkspaceEx.OpenRasterDataset(rasterName);
            IRasterDataset rasterDataset2=rasterWorkspaceEx.OpenRasterDataset(rasterName2);
            
            mapAlgebraOp.BindRaster(rasterDataset as IGeoDataset, "raster1");
            mapAlgebraOp.BindRaster(rasterDataset2 as IGeoDataset, "raster2");
            IRaster outRaster= (IRaster)mapAlgebraOp.Execute("[raster1] - [raster2]");

            ISaveAs2 saveAs;
            saveAs=(ISaveAs2)outRaster;
            saveAs.SaveAs(outputName, workspace, "TIFF");
        }

        public void RasterCalculatePlus(string filePath, string rasterName, string rasterName2, string outputName)
        {
            //���Ƶ�ͼ�����Ĳ���
            IMapAlgebraOp mapAlgebraOp;
            mapAlgebraOp = new RasterMapAlgebraOpClass();

            //����raster�����Ļ���
            IRasterAnalysisEnvironment rasterAnalysisEnvironment = default(IRasterAnalysisEnvironment);
            rasterAnalysisEnvironment = (IRasterAnalysisEnvironment)mapAlgebraOp;

            IWorkspace workspace;
            IRasterWorkspace rasterWorkspaceEx;

            //���ù����ռ�
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            workspace = workspaceFactory.OpenFromFile(filePath, 0);
            rasterWorkspaceEx = (IRasterWorkspace)workspace;
            rasterAnalysisEnvironment.OutWorkspace = workspace;

            //��դ�����ݼ�
            IRasterDataset rasterDataset = rasterWorkspaceEx.OpenRasterDataset(rasterName);
            IRasterDataset rasterDataset2 = rasterWorkspaceEx.OpenRasterDataset(rasterName2);

            mapAlgebraOp.BindRaster(rasterDataset as IGeoDataset, "raster1");
            mapAlgebraOp.BindRaster(rasterDataset2 as IGeoDataset, "raster2");
            IRaster outRaster = (IRaster)mapAlgebraOp.Execute("[raster1] + [raster2]");

            ISaveAs2 saveAs;
            saveAs = (ISaveAs2)outRaster;
            saveAs.SaveAs(outputName, workspace, "TIFF");
        }

        public void RasterCalculateMultiply(string filePath, string rasterName, string rasterName2, string outputName)
        {
            //���Ƶ�ͼ�����Ĳ���
            IMapAlgebraOp mapAlgebraOp;
            mapAlgebraOp = new RasterMapAlgebraOpClass();

            //����raster�����Ļ���
            IRasterAnalysisEnvironment rasterAnalysisEnvironment = default(IRasterAnalysisEnvironment);
            rasterAnalysisEnvironment = (IRasterAnalysisEnvironment)mapAlgebraOp;

            IWorkspace workspace;
            IRasterWorkspace rasterWorkspaceEx;

            //���ù����ռ�
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            workspace = workspaceFactory.OpenFromFile(filePath, 0);
            rasterWorkspaceEx = (IRasterWorkspace)workspace;
            rasterAnalysisEnvironment.OutWorkspace = workspace;

            //��դ�����ݼ�
            IRasterDataset rasterDataset = rasterWorkspaceEx.OpenRasterDataset(rasterName);
            IRasterDataset rasterDataset2 = rasterWorkspaceEx.OpenRasterDataset(rasterName2);

            mapAlgebraOp.BindRaster(rasterDataset as IGeoDataset, "raster1");
            mapAlgebraOp.BindRaster(rasterDataset2 as IGeoDataset, "raster2");
            IRaster outRaster = (IRaster)mapAlgebraOp.Execute("[raster1] * [raster2]");

            ISaveAs2 saveAs;
            saveAs = (ISaveAs2)outRaster;
            saveAs.SaveAs(outputName, workspace, "TIFF");
        }    
    }
}

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
    }
}

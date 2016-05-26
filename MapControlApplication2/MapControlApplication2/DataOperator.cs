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
    class DataOperator
    {
        //���浱ǰ��ͼ����
       //���ڴ��뵱ǰ��ͼ����
         public IMap m_map;
        
        public DataOperator(IMap map)
        {
            m_map = map;

        }

        //
        public ILayer GetLayerByName(String sLayerName)
        {
            //�ж�ͼ�������ͼ�����Ƿ�Ϊ�ա���Ϊ�գ���������Ϊ��
            if (sLayerName == "" || m_map == null)
            {
                return null;
            }

            //�Ե�ͼ��������ͼ����б�������ĳһͼ���������ָ��������ͬ���򷵻ظ�ͼ��
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                if (m_map.get_Layer(i).Name == sLayerName)
                {
                    return m_map.get_Layer(i);
                }
            }

            //����ͼ�����е�����ͼ��������ָ��ͼ������ƥ�䣬�������ؿ�
            return null;
        }

        //�ӵ�ͼ��ȡҪ�������
        public static IFeatureClass GetFeatureClass(IMap map, string lyrname)
        {
            int i;
            ILayer iLayer = null;
            if (lyrname == "" || lyrname == null)
            {
                return null;
            }
            //IMap map = MapControl.Map;
            for (i = 0; i < map.LayerCount; i++)
            {
                //��һ��map���õ�i��ͼ��
                iLayer = map.get_Layer(i);
                //��ȡ��ͼ����
                string lyrNameTemp = iLayer.Name;
                //�ж�ͼ�����Ƿ���������һ�£����һ���˳�ѭ��
                if (lyrNameTemp == lyrname)
                {
                    break;
                }

            }
            if (i >= map.LayerCount)
                return null;
            IFeatureLayer featLayer = iLayer as IFeatureLayer;
            return featLayer.FeatureClass;

        }

        //��ȡ�����޵����ƣ�����DataTable���ͷ���
        public DataTable GetContinentsNames(string inlayer)
        {
            //��ȡ��continents��ͼ�㣬����IFeatureLayer�ӿڷ��ʣ����ж��Ƿ�ɹ�����ʧ�ܣ��������ؿ�
            //ILayer layer = GetLayerByName("Continents");
            IFeatureClass featureLayer = GetFeatureClass(m_map,inlayer);
            if (featureLayer == null)
            {
                return null;
            }

            //����IFeatureLayer�ӿڵ�Search��������ȡҪ��ָ�루IFeatureCursor���ӿڶ���������֮�����ͼ���е�ȫ��Ҫ�أ�
            //���ж��Ƿ�ɹ���ȡ��һ��Ҫ�ء���ʧ�ܣ��������ؿա�
            IFeature feature;
            IFeatureCursor featureCursor = featureLayer.Search(null, false);
            feature = featureCursor.NextFeature();
            if (feature == null)
            {
                return null;

            }

            //�½�DataTable���Ͷ������ں�������
            DataTable dataTable = new DataTable();

            //�½�DataColumn���Ͷ������ڱ�����޵���ź����ơ�������Ϻ󣬼���dataTable���еļ��ϣ�Column����
            /*DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "���";
            dataColumn.DataType = System.Type.GetType("System.Int32");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "����";
            dataColumn.DataType = System.Type.GetType("System.String");
            dataTable.Columns.Add(dataColumn);*/


            IFields fields = feature.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                DataColumn dataColumn = new DataColumn();
                IField field =fields.get_Field(i);
                
                dataColumn.ColumnName = field.AliasName;
                dataColumn.DataType = System.Type.GetType("System.String");//field.Type as System.Type;
                dataTable.Columns.Add(dataColumn);
            }
            //��Ҫ������ź������ֶ��ϵ�ֵ����DataRow�еĶ�Ӧ���С��ڡ�continents��ͼ�����Ա��У������Ϣ�ڵ�0���ֶ��У�
            //������Ϣ�ڵڶ����ֶ��С�
            DataRow dataRow;
            while (feature != null)
            {
                dataRow = dataTable.NewRow();
                /*dataRow[0] = feature.get_Value(0);
                dataRow[1] = feature.get_Value(2);*/
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    dataRow[i] = feature.get_Value(i);
                }
                dataTable.Rows.Add(dataRow); 


                feature = featureCursor.NextFeature();
            }


            //
            return dataTable;
        }

        public DataTable GetContinentsNamesSelect(string inlayer,IQueryFilter queryFilter)
        {
            //��ȡ��continents��ͼ�㣬����IFeatureLayer�ӿڷ��ʣ����ж��Ƿ�ɹ�����ʧ�ܣ��������ؿ�
            //ILayer layer = GetLayerByName("Continents");
            IFeatureClass featureLayer = GetFeatureClass(m_map, inlayer);
            if (featureLayer == null)
            {
                return null;
            }

            //����IFeatureLayer�ӿڵ�Search��������ȡҪ��ָ�루IFeatureCursor���ӿڶ���������֮�����ͼ���е�ȫ��Ҫ�أ�
            //���ж��Ƿ�ɹ���ȡ��һ��Ҫ�ء���ʧ�ܣ��������ؿա�
            IFeature feature;
            IFeatureCursor featureCursor = featureLayer.Search(queryFilter, false);
            feature = featureCursor.NextFeature();
            if (feature == null)
            {
                return null;

            }

            //�½�DataTable���Ͷ������ں�������
            DataTable dataTable = new DataTable();

            //�½�DataColumn���Ͷ������ڱ�����޵���ź����ơ�������Ϻ󣬼���dataTable���еļ��ϣ�Column����
            /*DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "���";
            dataColumn.DataType = System.Type.GetType("System.Int32");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "����";
            dataColumn.DataType = System.Type.GetType("System.String");
            dataTable.Columns.Add(dataColumn);*/


            IFields fields = feature.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                DataColumn dataColumn = new DataColumn();
                IField field = fields.get_Field(i);

                dataColumn.ColumnName = field.AliasName;
                dataColumn.DataType = System.Type.GetType("System.String");//field.Type as System.Type;
                dataTable.Columns.Add(dataColumn);
            }
            //��Ҫ������ź������ֶ��ϵ�ֵ����DataRow�еĶ�Ӧ���С��ڡ�continents��ͼ�����Ա��У������Ϣ�ڵ�0���ֶ��У�
            //������Ϣ�ڵڶ����ֶ��С�
            DataRow dataRow;
            while (feature != null)
            {
                dataRow = dataTable.NewRow();
                /*dataRow[0] = feature.get_Value(0);
                dataRow[1] = feature.get_Value(2);*/
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    dataRow[i] = feature.get_Value(i);
                }
                dataTable.Rows.Add(dataRow);


                feature = featureCursor.NextFeature();
            }


            //
            return dataTable;
        }

        public IFeatureClass CreateFeatureClass(
            IWorkspace workspace, 
            //IFeatureDataset featureDataset,
            string Name,
            IFields fields,
            UID CLSID,
            UID EXTCLSID,
            //esriFeatureType FeatureType,
            //string ShapeFieldName,
            string ConfigKeyword,
            int type)
        {
            if (Name == "")
                return null;

            IFeatureClass featureClass;
            IFeatureWorkspace featureWorkspace=workspace as IFeatureWorkspace;

            //assign the class id value if not assigned
            if (CLSID==null)
            {
                CLSID=new UIDClass();
                CLSID.Value="esriGeoDatabase.Feature";
            }
            
            IObjectClassDescription objectClassDescription=new FeatureClassDescription();
            
            if (fields==null)
            {
                //create the fields using the required fields methond
                fields = objectClassDescription.RequiredFields;
                
                IFieldsEdit fieldsEdit = fields as IFieldsEdit;
               
                //setup field properties
                IFieldEdit fieldEdit = new FieldClass();

                fieldEdit.Name_2 = "Name";
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //fieldEdit.IsNullable_2 = true;
                fieldEdit.AliasName_2 = "����";
                //fieldEdit.DefaultValue_2 = "true";
                //fieldEdit.Editable_2 = true;
                //fieldEdit.Length_2 = 100;

                //add field to field collection
                fieldsEdit.AddField((IField)fieldEdit);
            }

            String strShapeField = "";

            //locate the shape field
            for (int j=0;j<fields.FieldCount;j++)
            {
                if (fields.get_Field(j).Type==esriFieldType.esriFieldTypeGeometry)
                {

                    //���������壬������ռ�ο��ͼ������ͣ�Ϊ��������״���ֶ���׼����
                    IGeometryDefEdit geoDefEdit = new GeometryDefClass();
                    ISpatialReference spatialReference = m_map.SpatialReference;
                    geoDefEdit.SpatialReference_2 = spatialReference;
                    if (type == 1)
                    {
                        geoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                    }
                    else if (type==2)
                    {
                        geoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                    }
                    else if (type == 3)
                    {
                        geoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    }
                    

                    strShapeField = fields.get_Field(j).Name;

                    IFieldEdit  fieldEdit = fields.get_Field(j) as IFieldEdit;
                    fieldEdit.GeometryDef_2 = geoDefEdit;                    
                }
            }

            //use IFieldChecker to create a validated field collection
            IFieldChecker fieldChecker =new FieldCheckerClass();
            IEnumFieldError EnumFieldError =null;
            IFields validatedFields=null;
            fieldChecker.ValidateWorkspace=workspace as IWorkspace;
            fieldChecker.Validate(fields,out EnumFieldError, out validatedFields);

            //finally create and return the feature class
            {
                featureClass=featureWorkspace.CreateFeatureClass(Name,fields,CLSID,null,esriFeatureType.esriFTSimple,strShapeField,null);
            }
            return featureClass;
        }

        public bool AddFeatureClassToMap(
            IFeatureClass featureClass, //��Ҫ����ӵ�Ҫ����
            String sLayerName)
        {
            //
            if (featureClass==null||sLayerName==null||m_map==null)
        {
                return false;
            }

            //ͨ��IFeatureLayer�ӿڴ���Ҫ��ͼ����󣬽�Ҫ������ͼ�����ʽ���в�����
            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer.FeatureClass = featureClass;
            featureLayer.Name = sLayerName;

            //��Ҫ��ͼ��ת��Ϊһ��ͼ�㣬���ж��Ƿ�ɹ�����ʧ�ܣ���������false��
            ILayer layer = featureLayer as ILayer;
            if (layer==null)
            {
                return false;
            }

            //�������õ�ͼ���������ͼ���󣬽���ͼ����ת��Ϊ���ͼ�����ж��Ƿ�ɹ�����ʧ�ܣ���������false��
            m_map.AddLayer(layer);
            IActiveView activeView = m_map as IActiveView;
            if (activeView==null)
            {
                return false;
            }

            //���ͼ���и��£�����ӵĻ��ͼ��չʾ�ڿؼ��С���������true��
            activeView.Refresh();
            return true;
        }

        public bool AddFeatureToLayer(
            String sLayerName,      //ָ��ͼ�������
            String sFeatureName,    //������ӵ�Ҫ�ص�����
           IPoint point )   //�������Ҫ�ص�������Ϣ
        {
            if (sLayerName == "" || sFeatureName == "" || point == null || m_map == null)
            {
                return false;
            }

            ILayer layer= null;
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                layer = m_map.get_Layer(i);
                if (layer.Name == sLayerName)
                {
                    break;
                }
                layer = null;
            }

            if (layer == null)
            {
                return false;
            }

            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;

            IFeature feature=featureClass.CreateFeature();
            if (feature==null)
            {
                return false;
            }

            //�༭Ҫ�أ��������ꡢ����ֵ�������á�����Ҫ�أ��ж��Ƿ�ɹ���
            feature.Shape = point;
            int index = feature.Fields.FindField("Name");
            feature.set_Value(index, sFeatureName);
            feature.Store();
            if (feature == null)
            {
                return false;
            }

            //����ͼ����ת��Ϊ���ͼ���ж��Ƿ�ɹ���
            IActiveView activeView = m_map as IActiveView;
            if (activeView == null)
            {
                return false;
            }

            //���ͼˢ�£�����ӵ�Ҫ�ؽ���չʾ�ڿؼ��С���������true��
            activeView.Refresh();
            return true;

        }

        public bool AddFeatureToLayer(
            String sLayerName,      //ָ��ͼ�������
            String sFeatureName,    //������ӵ�Ҫ�ص�����
           IPolygon polygon)   //�������Ҫ��(polygon)
        {
            if (sLayerName == "" || sFeatureName == "" || polygon == null || m_map == null)
            {
                return false;
            }

            ILayer layer = null;
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                layer = m_map.get_Layer(i);
                if (layer.Name == sLayerName)
                {
                    break;
                }
                layer = null;
            }

            if (layer == null)
            {
                return false;
            }

            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;

            IFeature feature = featureClass.CreateFeature();
            if (feature == null)
            {
                return false;
            }

            //�༭Ҫ�أ��������ꡢ����ֵ�������á�����Ҫ�أ��ж��Ƿ�ɹ���
            feature.Shape = polygon;
            int index = feature.Fields.FindField("Name");
            feature.set_Value(index, sFeatureName);
            feature.Store();
            if (feature == null)
            {
                return false;
            }

            //����ͼ����ת��Ϊ���ͼ���ж��Ƿ�ɹ���
            IActiveView activeView = m_map as IActiveView;
            if (activeView == null)
            {
                return false;
            }

            //���ͼˢ�£�����ӵ�Ҫ�ؽ���չʾ�ڿؼ��С���������true��
            activeView.Refresh();
            return true;

        }

        public bool AddFeatureToLayer(
            String sLayerName,      //ָ��ͼ�������
            String sFeatureName,    //������ӵ�Ҫ�ص�����
           IPolyline polyline)   //�������Ҫ��(polygon)
        {
            if (sLayerName == "" || sFeatureName == "" || polyline == null || m_map == null)
            {
                return false;
            }

            ILayer layer = null;
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                layer = m_map.get_Layer(i);
                if (layer.Name == sLayerName)
                {
                    break;
                }
                layer = null;
            }

            if (layer == null)
            {
                return false;
            }

            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;

            IFeature feature = featureClass.CreateFeature();
            if (feature == null)
            {
                return false;
            }

            //�༭Ҫ�أ��������ꡢ����ֵ�������á�����Ҫ�أ��ж��Ƿ�ɹ���
            feature.Shape = polyline;
            int index = feature.Fields.FindField("Name");
            feature.set_Value(index, sFeatureName);
            feature.Store();
            if (feature == null)
            {
                return false;
            }

            //����ͼ����ת��Ϊ���ͼ���ж��Ƿ�ɹ���
            IActiveView activeView = m_map as IActiveView;
            if (activeView == null)
            {
                return false;
            }

            //���ͼˢ�£�����ӵ�Ҫ�ؽ���չʾ�ڿؼ��С���������true��
            activeView.Refresh();
            return true;

        }

        public DataTable StatisticContinents()
        {
            MapAnalysis mapAnalysis = new MapAnalysis();

            String[] continents;
            continents=new String[10];
            int pos=0;
            int i = 0,j;

            //����dataTable�������Ӧ����
            DataTable dataTable=new DataTable();

            DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "Continents";
            dataColumn.DataType = System.Type.GetType("System.String");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "POP";
            dataColumn.DataType = System.Type.GetType("System.Int32");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "C0UNT";
            dataColumn.DataType = System.Type.GetType("System.Int32");
            dataTable.Columns.Add(dataColumn);

            //ͨ��GetLayerbyName�Լ�����������ȡ�ߴ�����Ϣ
            IFeatureLayer featLayer = (IFeatureLayer)GetLayerByName("Continents");

            IFeatureClass featClass;
            featClass = featLayer.FeatureClass;
            IFeatureCursor featCursor;
            featCursor = (IFeatureCursor)featClass.Search(null, false);           

            IFeature feature;
            feature = featCursor.NextFeature();
            
            //ͨ����صĽӿڻ�ȡ�ߴ��޵�����
            IFields fields = feature.Fields;
            for (pos = 0; pos < fields.FieldCount; pos++)
            {
                IField field =fields.get_Field(pos);
                
               if (field.AliasName=="CONTINENT")
               {
                   break;
               }            
            }
             
            DataRow dataRow;
            while (feature!=null)
            {
                continents[i++]=feature.get_Value(pos).ToString();                
                for (j=1;j<=7;j++)
                {
                    DataTable tempDataTable=mapAnalysis.QueryIntersect("World Cities", "Continents", m_map, esriSpatialRelationEnum.esriSpatialRelationIntersection,continents[i-1],j.ToString());    
                    dataRow=dataTable.NewRow();
                    dataRow[0]=continents[i-1];
                    dataRow[1]=j;
                    if (tempDataTable == null)
                        dataRow[2] = 0;
                    else 
                        dataRow[2]=tempDataTable.Rows.Count;
                    dataTable.Rows.Add(dataRow); 
                }
                feature = featCursor.NextFeature();
            }

            return dataTable;
        }

    }
}

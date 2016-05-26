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
        //保存当前地图对象
       //用于传入当前地图对象
         public IMap m_map;
        
        public DataOperator(IMap map)
        {
            m_map = map;

        }

        //
        public ILayer GetLayerByName(String sLayerName)
        {
            //判断图层名或地图对象是否为空。若为空，函数返回为空
            if (sLayerName == "" || m_map == null)
            {
                return null;
            }

            //对地图对象所有图层进行遍历。若某一图层的名称与指定名称相同，则返回该图层
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                if (m_map.get_Layer(i).Name == sLayerName)
                {
                    return m_map.get_Layer(i);
                }
            }

            //若地图对象中的所有图层名均与指定图层名不匹配，函数返回空
            return null;
        }

        //从地图获取要素类对象
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
                //从一个map里获得第i个图层
                iLayer = map.get_Layer(i);
                //获取其图层名
                string lyrNameTemp = iLayer.Name;
                //判断图层名是否和输入参数一致，如果一致退出循环
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

        //获取各个洲的名称，并以DataTable类型返回
        public DataTable GetContinentsNames(string inlayer)
        {
            //获取“continents”图层，利用IFeatureLayer接口访问，并判断是否成功。若失败，函数返回空
            //ILayer layer = GetLayerByName("Continents");
            IFeatureClass featureLayer = GetFeatureClass(m_map,inlayer);
            if (featureLayer == null)
            {
                return null;
            }

            //调用IFeatureLayer接口的Search方法，获取要素指针（IFeatureCursor）接口对象，用于在之后遍历图层中的全部要素，
            //并判断是否成功获取第一个要素。若失败，函数返回空。
            IFeature feature;
            IFeatureCursor featureCursor = featureLayer.Search(null, false);
            feature = featureCursor.NextFeature();
            if (feature == null)
            {
                return null;

            }

            //新建DataTable类型对象，用于函数返回
            DataTable dataTable = new DataTable();

            //新建DataColumn类型对象，用于保存各洲的序号和名称。设置完毕后，加入dataTable的列的集合（Column）中
            /*DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "序号";
            dataColumn.DataType = System.Type.GetType("System.Int32");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "名称";
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
            //将要素在序号和名称字段上的值赋给DataRow中的对应列中。在“continents”图层属性表中，序号信息在第0个字段中，
            //名称信息在第二个字段中。
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
            //获取“continents”图层，利用IFeatureLayer接口访问，并判断是否成功。若失败，函数返回空
            //ILayer layer = GetLayerByName("Continents");
            IFeatureClass featureLayer = GetFeatureClass(m_map, inlayer);
            if (featureLayer == null)
            {
                return null;
            }

            //调用IFeatureLayer接口的Search方法，获取要素指针（IFeatureCursor）接口对象，用于在之后遍历图层中的全部要素，
            //并判断是否成功获取第一个要素。若失败，函数返回空。
            IFeature feature;
            IFeatureCursor featureCursor = featureLayer.Search(queryFilter, false);
            feature = featureCursor.NextFeature();
            if (feature == null)
            {
                return null;

            }

            //新建DataTable类型对象，用于函数返回
            DataTable dataTable = new DataTable();

            //新建DataColumn类型对象，用于保存各洲的序号和名称。设置完毕后，加入dataTable的列的集合（Column）中
            /*DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "序号";
            dataColumn.DataType = System.Type.GetType("System.Int32");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "名称";
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
            //将要素在序号和名称字段上的值赋给DataRow中的对应列中。在“continents”图层属性表中，序号信息在第0个字段中，
            //名称信息在第二个字段中。
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
                fieldEdit.AliasName_2 = "名称";
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

                    //创建地理定义，设置其空间参考和几何类型，为创建“形状”字段做准备。
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
            IFeatureClass featureClass, //将要被添加的要素累
            String sLayerName)
        {
            //
            if (featureClass==null||sLayerName==null||m_map==null)
        {
                return false;
            }

            //通过IFeatureLayer接口创建要素图层对象，将要素类以图层的形式进行操作。
            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer.FeatureClass = featureClass;
            featureLayer.Name = sLayerName;

            //将要素图层转换为一般图层，并判断是否成功。若失败，函数返回false。
            ILayer layer = featureLayer as ILayer;
            if (layer==null)
            {
                return false;
            }

            //将创建好的图层添加至地图对象，将地图对象转换为活动视图，并判断是否成功。若失败，函数返回false。
            m_map.AddLayer(layer);
            IActiveView activeView = m_map as IActiveView;
            if (activeView==null)
            {
                return false;
            }

            //活动视图进行更新，新添加的活动视图被展示在控件中。函数返回true。
            activeView.Refresh();
            return true;
        }

        public bool AddFeatureToLayer(
            String sLayerName,      //指定图层的名称
            String sFeatureName,    //将被添加的要素的名称
           IPoint point )   //将被添加要素的坐标信息
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

            //编辑要素，对其坐标、属性值进行设置。保存要素，判断是否成功。
            feature.Shape = point;
            int index = feature.Fields.FindField("Name");
            feature.set_Value(index, sFeatureName);
            feature.Store();
            if (feature == null)
            {
                return false;
            }

            //将地图对象转换为活动视图，判断是否成功。
            IActiveView activeView = m_map as IActiveView;
            if (activeView == null)
            {
                return false;
            }

            //活动视图刷新，新添加的要素将被展示在控件中。函数返回true。
            activeView.Refresh();
            return true;

        }

        public bool AddFeatureToLayer(
            String sLayerName,      //指定图层的名称
            String sFeatureName,    //将被添加的要素的名称
           IPolygon polygon)   //将被添加要素(polygon)
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

            //编辑要素，对其坐标、属性值进行设置。保存要素，判断是否成功。
            feature.Shape = polygon;
            int index = feature.Fields.FindField("Name");
            feature.set_Value(index, sFeatureName);
            feature.Store();
            if (feature == null)
            {
                return false;
            }

            //将地图对象转换为活动视图，判断是否成功。
            IActiveView activeView = m_map as IActiveView;
            if (activeView == null)
            {
                return false;
            }

            //活动视图刷新，新添加的要素将被展示在控件中。函数返回true。
            activeView.Refresh();
            return true;

        }

        public bool AddFeatureToLayer(
            String sLayerName,      //指定图层的名称
            String sFeatureName,    //将被添加的要素的名称
           IPolyline polyline)   //将被添加要素(polygon)
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

            //编辑要素，对其坐标、属性值进行设置。保存要素，判断是否成功。
            feature.Shape = polyline;
            int index = feature.Fields.FindField("Name");
            feature.set_Value(index, sFeatureName);
            feature.Store();
            if (feature == null)
            {
                return false;
            }

            //将地图对象转换为活动视图，判断是否成功。
            IActiveView activeView = m_map as IActiveView;
            if (activeView == null)
            {
                return false;
            }

            //活动视图刷新，新添加的要素将被展示在控件中。函数返回true。
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

            //创建dataTable并添加相应的列
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

            //通过GetLayerbyName以及后续操作获取七大洲信息
            IFeatureLayer featLayer = (IFeatureLayer)GetLayerByName("Continents");

            IFeatureClass featClass;
            featClass = featLayer.FeatureClass;
            IFeatureCursor featCursor;
            featCursor = (IFeatureCursor)featClass.Search(null, false);           

            IFeature feature;
            feature = featCursor.NextFeature();
            
            //通过相关的接口获取七大洲的名称
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

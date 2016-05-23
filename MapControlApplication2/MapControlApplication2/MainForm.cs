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
using ESRI.ArcGIS.Output;


namespace MapControlApplication2
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion
        
        private IGeometry _polygon = null;//定义一个几何对象，作为绘制结果
        private INewPolygonFeedback _polyFeedback = null;//定义一个多边形反馈对象
        private IPoint _startPoint = null;//多边形起始节点
        private IPoint _endPoint = null;//多边形终止节点

        private bool _drawStart = false; //多边形绘制开始标记
        /*DrawPolygon drawPolygon = new DrawPolygon();*/

        private IGeometry _polyline = null;
        private INewLineFeedback _lineFeedback = null;

        private DataTable dataTable = null;//每次查询过后用于记录表格
        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;

            //添加书签
            IMapBookmarks bookmarks = axMapControl1.Map as IMapBookmarks;
            if (bookmarks != null)
            {
                IEnumSpatialBookmark enumSpatialBookmark = bookmarks.Bookmarks;               
                //对地图所包含的书签进行遍历
                enumSpatialBookmark.Reset();
                ISpatialBookmark spatialBookmark = enumSpatialBookmark.Next();
                while (spatialBookmark != null)
                {                
                   cbBookmarkList.Items.Add(spatialBookmark.Name);
                    spatialBookmark = enumSpatialBookmark.Next();
                }
            }

            //向框中添加图层名称
            cbLayer.Items.Clear();
            IMap map = axMapControl1.Map;
            ILayer iLayer = null;
            for (int i = 0; i < map.LayerCount; i++)
            {
                iLayer = map.get_Layer(i);
                string lyrName = iLayer.Name;
                //IFeatureLayer feaLayer = iLayer as IFeatureLayer;
                //IFeatureClass feaClass = feaLayer.FeatureClass;
                cbLayer.Items.Add(lyrName);
            }

            /*drawPolygon.OnCreate(); */                     
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);
                
                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }
                
                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {            
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }

            
            //向框中添加书签
            cbBookmarkList.Items.Clear();
            IMapBookmarks bookmarks = axMapControl1.Map as IMapBookmarks;
            if (bookmarks != null)
            {
                IEnumSpatialBookmark enumSpatialBookmark = bookmarks.Bookmarks;             
                //对地图所包含的书签进行遍历
                enumSpatialBookmark.Reset();
                ISpatialBookmark spatialBookmark = enumSpatialBookmark.Next();
                while (spatialBookmark != null)
                {                    
                    cbBookmarkList.Items.Add(spatialBookmark.Name);
                    spatialBookmark = enumSpatialBookmark.Next();
                }
            }

            //向框中添加图层名称
            cbLayer.Items.Clear();
            IMap map=axMapControl1.Map;
            ILayer iLayer=null;
            for (int i = 0; i < map.LayerCount; i++)
            {
                iLayer = map.get_Layer(i);
                string lyrName = iLayer.Name;
                //IFeatureLayer feaLayer = iLayer as IFeatureLayer;
                //IFeatureClass feaClass = feaLayer.FeatureClass;
                cbLayer.Items.Add(lyrName);
            }

        }


        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {            
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
            if (miDrawPolygon.Checked==true)
            {
                /*drawPolygon.OnMouseMove(e.button,e.shift,e.x,e.y);*/
                if (_startPoint != null)
                {
                    IPoint movePoint = /*(myHook.FocusMap as IActiveView)*/axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                    _polyFeedback.MoveTo(movePoint);//鼠标移动过程中实时显示反馈效果
                }
            }
            if (miAddLine.Checked==true)
            {
                if (_startPoint!=null)
                {
                    IPoint movePoint = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                    _lineFeedback.MoveTo(movePoint);
                }
            }
        }


        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            
            if (miAddFeature.Checked == true)
            {
                IPoint point = new PointClass();
                point.PutCoords(e.mapX, e.mapY);

                DataOperator dataOperator = new DataOperator(axMapControl1.Map);
                dataOperator.AddFeatureToLayer("ObservationStations","Observation Station",point);

            }

//             if (miDrawPolygon.Checked==true)
//             {
//                 _polygon = null;//每次重设多边形为空值
//                 _drawStart = true; //开始绘制标记置为true
// 
//                 _polyFeedback = new NewPolygonFeedbackClass();
//                 _polyFeedback.Display = myHook.ActiveView.ScreenDisplay;
//             }
            if (miDrawPolygon.Checked==true)
            {
                //button=1 左键
                if (e.button == 1)
                {
                    if (_startPoint == null)//如果是多边形的第一个点
                    {
                        _startPoint = /*(axMapControl1.FocusMap as IActiveView)*/axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        _polyFeedback.Start(_startPoint);
                    }
                    else
                    {
                        _endPoint = /*(axMapControl1.FocusMap as IActiveView)*/axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        _polyFeedback.AddPoint(_endPoint);
                    }
                }
            }

            if (miAddLine.Checked==true)
            {
                if (e.button==1)
                {
                    if (_startPoint==null)
                    {
                        _startPoint = /*(axMapControl1.FocusMap as IActiveView)*/axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        _lineFeedback.Start(_startPoint);
                    }
                    else 
                    {
                        _endPoint = /*(axMapControl1.FocusMap as IActiveView)*/axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                        _lineFeedback.AddPoint(_endPoint);
                    }
                }
            }
            return;
        }

        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            if (miDrawPolygon.Checked==true)
            {
                _polygon = _polyFeedback.Stop();
                _startPoint = null;
                _drawStart = false;

                DataOperator dataOperator = new DataOperator(axMapControl1.Map);
                dataOperator.AddFeatureToLayer("Polygon", "polygon", _polygon as IPolygon);
            }

            if (miAddLine.Checked==true)
            {
                _polyline = _lineFeedback.Stop();
                _startPoint = null;
                _drawStart = false;

                DataOperator dataOperator = new DataOperator(axMapControl1.Map);
                dataOperator.AddFeatureToLayer("Polyline", "polyline", _polyline as IPolyline);
            }
        }
        
        private void bookmarklist_Click(object sender, EventArgs e)
        {
        
        }

        //创建书签
        public void CreateBookmark(string sBookmarkName)
        {
            //通过IAOIBookmark接口创建一个变量，其类型为AOIBookmark，用于保存当前地图的范围
            IAOIBookmark aoiBookmark = new AOIBookmarkClass();
            if (aoiBookmark != null)
            {
                aoiBookmark.Location = axMapControl1.ActiveView.Extent;
                aoiBookmark.Name = sBookmarkName;
            }

            //通过IMapBookmarks接口访问当前地图，并向地图中加入新建书签
            IMapBookmarks bookmarks = axMapControl1.Map as IMapBookmarks;
            if (bookmarks != null)
            {
                IEnumSpatialBookmark enumSpatialBookmark = bookmarks.Bookmarks;
                //对地图所包含的书签进行遍历,如果存在相同书签
                enumSpatialBookmark.Reset();
                ISpatialBookmark spatialBookmark = enumSpatialBookmark.Next();
                while (spatialBookmark != null)
                {
                    if (sBookmarkName == spatialBookmark.Name)
                    {
                        Error error = new Error();
                        error.Show();
                        return;
                    }
                    spatialBookmark = enumSpatialBookmark.Next();
                }
                bookmarks.AddBookmark(aoiBookmark);
                
            }

            //将新建书签名加入组合框中，用于之后调用对应书签
            cbBookmarkList.Items.Add(aoiBookmark.Name);
        }

        //"创建书签"按钮的“点击”事件响应函数，用于运行“确认书签名称”按钮
        private void miCreateBookmark_Click(object sender, EventArgs e)
        {
            AdmitBookmarkName frmABN = new AdmitBookmarkName(this);
            frmABN.Show();
        }

        //组合框的“选择索引更改”事件响应函数，用于在改变组合框所选项时，地图范围变为其对应书签所保存的范围
        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            //访问地图所包含的书签，并获取书签序列
            IMapBookmarks bookmarks = axMapControl1.Map as IMapBookmarks;
            //另一个接口，用于遍历
            IEnumSpatialBookmark enumSpatialBookmark = bookmarks.Bookmarks;

            //对地图所包含的书签进行遍历，获取与组合框所选项名称相符的书签
            enumSpatialBookmark.Reset();
            ISpatialBookmark spatialBookmark = enumSpatialBookmark.Next();
            while (spatialBookmark != null)
            {
                if (cbBookmarkList.SelectedItem.ToString() == spatialBookmark.Name)
                {
                    spatialBookmark.ZoomTo((IMap)axMapControl1.ActiveView);
                    axMapControl1.ActiveView.Refresh();
                    break;
                }
                spatialBookmark = enumSpatialBookmark.Next();
            }
        }


        //从地图获取要素类对象ILayer
        public static IFeatureClass GetFeatureClass(IMap map,string lyrname)
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
            if (i>=map.LayerCount)
                return null;
            IFeatureLayer featLayer = iLayer as IFeatureLayer;
            return featLayer.FeatureClass;

        }

        //根据图层名从一个Map里获取一个图层
        public static bool AddFLayer(IMap map,IFeatureClass featureClass, string lyrname)
        {
            //如果没有给定图层名称，则获取要素类的名称作为图层名称
            if (lyrname == "" || lyrname == null)
            {
                lyrname = featureClass.AliasName;
            }
            IFeatureLayer featureLayer = new FeatureLayer();
            featureLayer.FeatureClass = featureClass;
            featureLayer.Name=lyrname;

            //将要素图层转换为一般图层，并判断是否成功。若失败，则返回false
            ILayer layer=featureLayer as ILayer;
            if (layer==null)
            {
                return false;
            }

            //将创建好的图层添加至地图对象，并根据返回的图层index判断是否成功
            //long index;
            map.AddLayer(layer);
            /*if (index==-1)//若失败，则返回false
            {
                return false;
            }*/
            return true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void miAccessData_Click(object sender, EventArgs e)
        {
            
            /*DataOperator dataOperator = new DataOperator(axMapControl1.Map);
            DataBoard frmABN = new DataBoard("各大洲洲名", dataOperator.GetContinentsNames());
            
            frmABN.Show();*/

//             DataGet dataGet = new DataGet(axMapControl1.Map);
//             dataGet.Show();
        }

        private void SelectedIndexChangedLayer(object sender, EventArgs e)
        {
            
            DataOperator dataOperator = new DataOperator(axMapControl1.Map);
            MapAnalysis mapAnalysis = new MapAnalysis();
            DataBoard frmABN = new DataBoard(cbLayer.SelectedItem.ToString(), dataOperator.GetContinentsNamesSelect(cbLayer.SelectedItem.ToString(),null), /*mapAnalysis.Buffer("World Cities", "CITY_NAME='New York'", 5, axMapControl1.Map)*/dataTable,axMapControl1.Map);

            frmABN.Show();
        }

        private void miCreateShapefile_Click(object sender, EventArgs e)
        {
            //IFeatureClass iFeatureClass = new IFeatureClass();
           // IFields iFields = iFeatureClass.Fields;
           //创建Shape文件，将其以要素类形式返回，并判断是否成功。若失败，消息框提示，函数返回空。
            DataOperator dataOperator = new DataOperator(axMapControl1.Map);
            
            IWorkspaceFactory workspaceFactory =new ShapefileWorkspaceFactoryClass();
            String sParentDirectory="d://";
            String sWorkspaceName = "ShapefileWorkSpace" ;
            String sFileName = "counties";

            if (System.IO.Directory.Exists(sParentDirectory + sWorkspaceName))
            {
                System.IO.Directory.Delete(sParentDirectory+sWorkspaceName, true);
            }
            
            IWorkspaceName WorkspaceName=workspaceFactory.Create(sParentDirectory,sWorkspaceName,null,0);
            IName name=WorkspaceName as IName;
            
            IWorkspace workspace=(IWorkspace)name.Open();

            IFeatureClass featureClass = dataOperator.CreateFeatureClass(workspace, sFileName, null, null, null, null,1);
            if (featureClass == null)
            {
                MessageBox.Show("创建Shape文件失败");
                return;
            }

            //将要素类添加到地图中，记录结果。若为true，将创建Shapefile按钮禁用，函数返回空。
            bool bRes = dataOperator.AddFeatureClassToMap(featureClass, "ObservationStations");
            if (bRes)
            {
                miCreateShapefile.Enabled = false;
                return;
            }
            else
            {
                MessageBox.Show("将新建Shape文件加入地图失败！");
                return;
            }
        }

        private void miCreatePolygon_Click(object sender, EventArgs e)
        {
            //IFeatureClass iFeatureClass = new IFeatureClass();
            // IFields iFields = iFeatureClass.Fields;
            //创建Shape文件，将其以要素类形式返回，并判断是否成功。若失败，消息框提示，函数返回空。
            DataOperator dataOperator = new DataOperator(axMapControl1.Map);

            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
            String sParentDirectory = "d://";
            String sWorkspaceName = "ShapefileWorkSpacePolygon";
            String sFileName = "test";

            if (System.IO.Directory.Exists(sParentDirectory + sWorkspaceName))
            {
                System.IO.Directory.Delete(sParentDirectory + sWorkspaceName, true);
            }

            IWorkspaceName WorkspaceName = workspaceFactory.Create(sParentDirectory, sWorkspaceName, null, 0);
            IName name = WorkspaceName as IName;

            IWorkspace workspace = (IWorkspace)name.Open();

            IFeatureClass featureClass = dataOperator.CreateFeatureClass(workspace, sFileName, null, null, null, null, 3);
            if (featureClass == null)
            {
                MessageBox.Show("创建Shape文件失败");
                return;
            }

            //将要素类添加到地图中，记录结果。若为true，将创建Shapefile按钮禁用，函数返回空。
            bool bRes = dataOperator.AddFeatureClassToMap(featureClass, "Polygon");
            if (bRes)
            {
                miCreatePolygon.Enabled = false;
                return;
            }
            else
            {
                MessageBox.Show("将新建Shape文件加入地图失败！");
                return;
            }
        }

        private void miCreateLine_Click(object sender, EventArgs e)
        {
            //IFeatureClass iFeatureClass = new IFeatureClass();
            // IFields iFields = iFeatureClass.Fields;
            //创建Shape文件，将其以要素类形式返回，并判断是否成功。若失败，消息框提示，函数返回空。
            DataOperator dataOperator = new DataOperator(axMapControl1.Map);

            IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
            String sParentDirectory = "d://";
            String sWorkspaceName = "ShapefileWorkSpacePolyline";
            String sFileName = "testline";

            if (System.IO.Directory.Exists(sParentDirectory + sWorkspaceName))
            {
                System.IO.Directory.Delete(sParentDirectory + sWorkspaceName, true);
            }

            IWorkspaceName WorkspaceName = workspaceFactory.Create(sParentDirectory, sWorkspaceName, null, 0);
            IName name = WorkspaceName as IName;

            IWorkspace workspace = (IWorkspace)name.Open();

            IFeatureClass featureClass = dataOperator.CreateFeatureClass(workspace, sFileName, null, null, null, null, 2);
            if (featureClass == null)
            {
                MessageBox.Show("创建Shape文件失败");
                return;
            }

            //将要素类添加到地图中，记录结果。若为true，将创建Shapefile按钮禁用，函数返回空。
            bool bRes = dataOperator.AddFeatureClassToMap(featureClass, "Polyline");
            if (bRes)
            {
                miCreateLine.Enabled = false;
                return;
            }
            else
            {
                MessageBox.Show("将新建Shape文件加入地图失败！");
                return;
            }
        }

        private void tsmSimpleRender_Click(object sender, EventArgs e)
        {
            //获取“World Cities”图层。
            DataOperator dataOperator = new DataOperator(axMapControl1.Map);
            ILayer layer = dataOperator.GetLayerByName("World Cities");
            //通过IRgbColor接口新建RgbColor类型对象，将其设置为红色。
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = 255;
            rgbColor.Green = 0;
            rgbColor.Blue = 0;
            //获取“World Cities”图层的符号信息，并通过IColor接口访问设置好的颜色对象。
            ISymbol symbol = MapComposer.GetSymbolFromLayer(layer, null);
            IColor color = rgbColor as IColor;
            bool bRes = MapComposer.RenderSimply(layer, color);
            if (bRes)
            {
                axTOCControl1.ActiveView.ContentsChanged();
                axMapControl1.ActiveView.Refresh();
                tsmSimpleRender.Enabled = false;
            }
            else
            {
                MessageBox.Show("简单渲染图层失败");
            }
        }

        private void miGetRendererInfo_Click_1(object sender, EventArgs e)
        {
            //获取World Cities图层
            DataOperator dataOperator = new DataOperator(axMapControl1.Map);
            ILayer layer = dataOperator.GetLayerByName("World Cities");

            //消息框提示该图层的渲染器类型信息
            MessageBox.Show(MapComposer.GetRendererTypeByLayer(layer));
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {

        }


        private void miPageLayout_Click(object sender, EventArgs e)
        {
            //点击“显示页面布局”菜单项并使其被勾选，显示页面布局空间，
            //隐藏地图空间，并使工具条空间和TOC空间与页面空间进行关联，同时
            //激活“打印”菜单项；反之则做逆向操作
            if (miPageLayout.Checked == false)
            {
                axToolbarControl1.SetBuddyControl(axPageLayoutControl1.Object);
                axTOCControl1.SetBuddyControl(axPageLayoutControl1.Object);

                axPageLayoutControl1.Show();
                axMapControl1.Hide();

                miPageLayout.Checked =true;
                miMap.Checked = false;
                miPrint.Enabled = true;
                miOutput.Enabled = true;
            }
            else
            {
                axToolbarControl1.SetBuddyControl(axMapControl1.Object);
                axTOCControl1.SetBuddyControl(axMapControl1.Object);

                axPageLayoutControl1.Hide();
                axMapControl1.Show();

                miPageLayout.Checked = false;
                miMap.Checked = true;
                miPrint.Enabled = false;
                miOutput.Enabled = false;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////
        private void miMap_Click(object sender, EventArgs e)
        {
            if (miMap.Checked == false)
            {
                axToolbarControl1.SetBuddyControl(axMapControl1.Object);
                axTOCControl1.SetBuddyControl(axMapControl1.Object);

                axMapControl1.Show();
                axPageLayoutControl1.Hide();

                miMap.Checked = true;
                miPageLayout.Checked = false;
                miPrint.Enabled = false;
                miOutput.Enabled = false;
            }
            else
            {
                axTOCControl1.SetBuddyControl(axPageLayoutControl1.Object);
                axToolbarControl1.SetBuddyControl(axPageLayoutControl1.Object);

                axPageLayoutControl1.Show();
                axMapControl1.Hide();

                miMap.Checked = false;
                miPageLayout.Checked = true;
                miPrint.Enabled = true;
                miOutput.Enabled = true;
            }

        }

        private void miPrint_Click(object sender, EventArgs e)
        {
            IPrinter printer = axPageLayoutControl1.Printer;
            if (printer == null)
            {
                MessageBox.Show("获取默认打印机失败！");
            }
            String sMsg = "是否使用默认打印机:" + printer.Name + "?";
            if (MessageBox.Show(sMsg, "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            IPaper paper = printer.Paper;
            paper.Orientation = 1;
            IPage page = axPageLayoutControl1.Page;
            page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale;
            axPageLayoutControl1.PrintPageLayout(1, 1, 0);
        }

        private void mapOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IActiveView activeView;
            if (miPageLayout.Checked==true)
            {
                activeView = axPageLayoutControl1.ActiveView;
            }
            else
            {
                activeView = axMapControl1.ActiveView;
            }

            String pathFileName = "D://Export.jpg";

            ESRI.ArcGIS.Output.IExport export = new ESRI.ArcGIS.Output.ExportJPEGClass();
            export.ExportFileName = pathFileName;

            // Microsoft Windows default DPI resolution
            export.Resolution = 96;
            ESRI.ArcGIS.Display.tagRECT exportRECT = activeView.ExportFrame;
            ESRI.ArcGIS.Geometry.IEnvelope envelope = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            envelope.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
            export.PixelBounds = envelope;
            System.Int32 hDC = export.StartExporting();
            activeView.Output(hDC, (System.Int16)export.Resolution, ref exportRECT, null, null);

            // Finish writing the export file and cleanup any intermediate files
            export.FinishExporting();
            export.Cleanup();
        }

        //axMapControl加载的数据发生重绘时，需要联动，所以在axMapControl的OnAfterScreenDraw事件中，需添加获取axMapControl控件中当前所显示的地理范围代码，并将当前显示范围传给axPageLayoutControl控件ActiveView对象的FocusMap中，同时调用拷贝方法；
        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView pAcv = axPageLayoutControl1.ActiveView.FocusMap as IActiveView;
            IDisplayTransformation displayTransformation = pAcv.ScreenDisplay.DisplayTransformation;
            displayTransformation.VisibleBounds = axMapControl1.Extent;//设置焦点地图的可视范围
            axPageLayoutControl1.ActiveView.Refresh();
            GeoMapLoad.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);

            //向框中添加图层名称
            cbLayer.Items.Clear();
            IMap map = axMapControl1.Map;
            ILayer iLayer = null;
            for (int i = 0; i < map.LayerCount; i++)
            {
                iLayer = map.get_Layer(i);
                string lyrName = iLayer.Name;
                //IFeatureLayer feaLayer = iLayer as IFeatureLayer;
                //IFeatureClass feaClass = feaLayer.FeatureClass;
                cbLayer.Items.Add(lyrName);
            }
        }

        private void miAddFeature_Click(object sender, EventArgs e)
        {
            
            if (miAddFeature.Checked == true)
            {
                miAddFeature.Checked = false;
            }
            else
            {
                miAddFeature.Checked = true;
                miDrawPolygon.Checked = false;
                miAddLine.Checked = false;
            }
        }

        private void miDrawPolygon_Click(object sender, EventArgs e)
        {
            if (miDrawPolygon.Checked == true)
            {
                miDrawPolygon.Checked = false;
            }
            else
            {
                miDrawPolygon.Checked = true;
                miAddFeature.Checked = false;
                miAddLine.Checked = false;
                /*drawPolygon.OnClick();*/
                _polygon = null;//每次重设多边形为空值
                _drawStart = true; //开始绘制标记置为true

//                 myHook = new HookHelperClass();
//                 (myHook.Hook as IMapControl3).CurrentTool = this;
                _polyFeedback = new NewPolygonFeedbackClass();
                _polyFeedback.Display = axMapControl1.ActiveView.ScreenDisplay;
            }
        }

        private void miAddLine_Click(object sender, EventArgs e)
        {
            if (miAddLine.Checked == true)
            {
                miAddLine.Checked = false;
            }
            else
            {
                miAddLine.Checked = true;
                miAddFeature.Checked = false;
                miDrawPolygon.Checked = false;

                _polyline = null;
                _drawStart = true;

                _lineFeedback = new NewLineFeedbackClass();
                _lineFeedback.Display = axMapControl1.ActiveView.ScreenDisplay;
            }
        }

        private void miSpatialFilter_Click(object sender, EventArgs e)
        {
//             DataQuery DataQuery = new DataQuery(axMapControl1.Map);
//             DataQuery.Show();
            MapAnalysis mapAnalysis = new MapAnalysis();
            dataTable=mapAnalysis.QueryIntersect("World Cities", "Continents", axMapControl1.Map, esriSpatialRelationEnum.esriSpatialRelationIntersection);
            IActiveView activeView;
            activeView = axMapControl1.ActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, 0, axMapControl1.Extent);
        }

        private void cbBookmarkList_Click(object sender, EventArgs e)
        {

        }

        private void miBuffer_Click(object sender, EventArgs e)
        {
            MapAnalysis mapAnalysis = new MapAnalysis();
            dataTable=mapAnalysis.Buffer("World Cities", "CITY_NAME='New York'", 5, axMapControl1.Map);
            IActiveView activeView;
            activeView = axMapControl1.ActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, 0, axMapControl1.Extent);
         
        }

        private void miStatistic_Click(object sender, EventArgs e)
        {
            MapAnalysis mapAnalysis = new MapAnalysis();
            string sMsg;
            sMsg=mapAnalysis.Statistic("Continents","SQMI",axMapControl1.Map);
            MessageBox.Show(sMsg);
        }  
       

       
    }
}
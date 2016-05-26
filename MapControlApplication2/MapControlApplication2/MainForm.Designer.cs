namespace MapControlApplication2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
            //Failure to do this may result in random crashes on exit due to the operating system unloading 
            //the libraries in the incorrect order. 
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.miCreate1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.cbBookmarkList = new System.Windows.Forms.ToolStripComboBox();
            this.sdata = new System.Windows.Forms.ToolStripMenuItem();
            this.miAccessData = new System.Windows.Forms.ToolStripMenuItem();
            this.cbLayer = new System.Windows.Forms.ToolStripComboBox();
            this.tbRender = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSimpleRender = new System.Windows.Forms.ToolStripMenuItem();
            this.miGetRendererInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.地图显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miPageLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.miData = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateShapefile = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreatePolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateLine = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.miDrawPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.gIS分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSpatialFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.miBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.miStatistic = new System.Windows.Forms.ToolStripMenuItem();
            this.栅格管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateRaster = new System.Windows.Forms.ToolStripMenuItem();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbookmarklist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.miCreatePerson = new System.Windows.Forms.ToolStripMenuItem();
            this.miRasterMosaic = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreate1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // miCreate1
            // 
            this.miCreate1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.miCreateBookmark,
            this.cbBookmarkList,
            this.sdata,
            this.cbLayer,
            this.tbRender,
            this.地图显示ToolStripMenuItem,
            this.miData,
            this.gIS分析ToolStripMenuItem,
            this.栅格管理ToolStripMenuItem});
            this.miCreate1.Location = new System.Drawing.Point(0, 0);
            this.miCreate1.Name = "miCreate1";
            this.miCreate1.Size = new System.Drawing.Size(859, 29);
            this.miCreate1.TabIndex = 0;
            this.miCreate1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewDoc,
            this.menuOpenDoc,
            this.menuSaveDoc,
            this.menuSaveAs,
            this.miPrint,
            this.miOutput,
            this.menuSeparator,
            this.menuExitApp});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(39, 25);
            this.menuFile.Text = "File";
            // 
            // menuNewDoc
            // 
            this.menuNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuNewDoc.Image")));
            this.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuNewDoc.Name = "menuNewDoc";
            this.menuNewDoc.Size = new System.Drawing.Size(180, 22);
            this.menuNewDoc.Text = "New Document";
            this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
            // 
            // menuOpenDoc
            // 
            this.menuOpenDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuOpenDoc.Image")));
            this.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuOpenDoc.Name = "menuOpenDoc";
            this.menuOpenDoc.Size = new System.Drawing.Size(180, 22);
            this.menuOpenDoc.Text = "Open Document...";
            this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
            // 
            // menuSaveDoc
            // 
            this.menuSaveDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveDoc.Image")));
            this.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuSaveDoc.Name = "menuSaveDoc";
            this.menuSaveDoc.Size = new System.Drawing.Size(180, 22);
            this.menuSaveDoc.Text = "SaveDocument";
            this.menuSaveDoc.Click += new System.EventHandler(this.menuSaveDoc_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(180, 22);
            this.menuSaveAs.Text = "Save As...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // miPrint
            // 
            this.miPrint.Name = "miPrint";
            this.miPrint.Size = new System.Drawing.Size(180, 22);
            this.miPrint.Text = "Print";
            this.miPrint.Click += new System.EventHandler(this.miPrint_Click);
            // 
            // miOutput
            // 
            this.miOutput.Name = "miOutput";
            this.miOutput.Size = new System.Drawing.Size(180, 22);
            this.miOutput.Text = "MapOutput";
            this.miOutput.Click += new System.EventHandler(this.mapOutputToolStripMenuItem_Click);
            // 
            // menuSeparator
            // 
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // menuExitApp
            // 
            this.menuExitApp.Name = "menuExitApp";
            this.menuExitApp.Size = new System.Drawing.Size(180, 22);
            this.menuExitApp.Text = "Exit";
            this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
            // 
            // miCreateBookmark
            // 
            this.miCreateBookmark.Name = "miCreateBookmark";
            this.miCreateBookmark.Size = new System.Drawing.Size(68, 25);
            this.miCreateBookmark.Text = "创建书签";
            this.miCreateBookmark.Click += new System.EventHandler(this.miCreateBookmark_Click);
            // 
            // cbBookmarkList
            // 
            this.cbBookmarkList.Name = "cbBookmarkList";
            this.cbBookmarkList.Size = new System.Drawing.Size(121, 25);
            this.cbBookmarkList.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.cbBookmarkList.Click += new System.EventHandler(this.cbBookmarkList_Click);
            // 
            // sdata
            // 
            this.sdata.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAccessData});
            this.sdata.Name = "sdata";
            this.sdata.Size = new System.Drawing.Size(68, 25);
            this.sdata.Text = "空间数据";
            this.sdata.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // miAccessData
            // 
            this.miAccessData.Name = "miAccessData";
            this.miAccessData.Size = new System.Drawing.Size(148, 22);
            this.miAccessData.Text = "访问图层数据";
            this.miAccessData.Click += new System.EventHandler(this.miAccessData_Click);
            // 
            // cbLayer
            // 
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Size = new System.Drawing.Size(121, 25);
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChangedLayer);
            // 
            // tbRender
            // 
            this.tbRender.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSimpleRender,
            this.miGetRendererInfo});
            this.tbRender.Name = "tbRender";
            this.tbRender.Size = new System.Drawing.Size(68, 25);
            this.tbRender.Text = "地图表现";
            // 
            // tsmSimpleRender
            // 
            this.tsmSimpleRender.Name = "tsmSimpleRender";
            this.tsmSimpleRender.Size = new System.Drawing.Size(184, 22);
            this.tsmSimpleRender.Text = "简单渲染图层";
            this.tsmSimpleRender.Click += new System.EventHandler(this.tsmSimpleRender_Click);
            // 
            // miGetRendererInfo
            // 
            this.miGetRendererInfo.Name = "miGetRendererInfo";
            this.miGetRendererInfo.Size = new System.Drawing.Size(184, 22);
            this.miGetRendererInfo.Text = "获取图层渲染器信息";
            this.miGetRendererInfo.Click += new System.EventHandler(this.miGetRendererInfo_Click_1);
            // 
            // 地图显示ToolStripMenuItem
            // 
            this.地图显示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMap,
            this.miPageLayout});
            this.地图显示ToolStripMenuItem.Name = "地图显示ToolStripMenuItem";
            this.地图显示ToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.地图显示ToolStripMenuItem.Text = "地图显示";
            // 
            // miMap
            // 
            this.miMap.Checked = true;
            this.miMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miMap.Name = "miMap";
            this.miMap.Size = new System.Drawing.Size(148, 22);
            this.miMap.Text = "显示地图";
            this.miMap.Click += new System.EventHandler(this.miMap_Click);
            // 
            // miPageLayout
            // 
            this.miPageLayout.Name = "miPageLayout";
            this.miPageLayout.Size = new System.Drawing.Size(148, 22);
            this.miPageLayout.Text = "显示页面布局";
            this.miPageLayout.Click += new System.EventHandler(this.miPageLayout_Click);
            // 
            // miData
            // 
            this.miData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCreateShapefile,
            this.miCreatePolygon,
            this.miCreateLine,
            this.miAddFeature,
            this.miDrawPolygon,
            this.miAddLine});
            this.miData.Name = "miData";
            this.miData.Size = new System.Drawing.Size(68, 25);
            this.miData.Text = "数据操作";
            // 
            // miCreateShapefile
            // 
            this.miCreateShapefile.Name = "miCreateShapefile";
            this.miCreateShapefile.Size = new System.Drawing.Size(165, 22);
            this.miCreateShapefile.Text = "创建Shapefile点";
            this.miCreateShapefile.Click += new System.EventHandler(this.miCreateShapefile_Click);
            // 
            // miCreatePolygon
            // 
            this.miCreatePolygon.Name = "miCreatePolygon";
            this.miCreatePolygon.Size = new System.Drawing.Size(165, 22);
            this.miCreatePolygon.Text = "创建Shapefile面";
            this.miCreatePolygon.Click += new System.EventHandler(this.miCreatePolygon_Click);
            // 
            // miCreateLine
            // 
            this.miCreateLine.Name = "miCreateLine";
            this.miCreateLine.Size = new System.Drawing.Size(165, 22);
            this.miCreateLine.Text = "创建Shapefile线";
            this.miCreateLine.Click += new System.EventHandler(this.miCreateLine_Click);
            // 
            // miAddFeature
            // 
            this.miAddFeature.Name = "miAddFeature";
            this.miAddFeature.Size = new System.Drawing.Size(165, 22);
            this.miAddFeature.Text = "添加要素";
            this.miAddFeature.Click += new System.EventHandler(this.miAddFeature_Click);
            // 
            // miDrawPolygon
            // 
            this.miDrawPolygon.Name = "miDrawPolygon";
            this.miDrawPolygon.Size = new System.Drawing.Size(165, 22);
            this.miDrawPolygon.Text = "添加面";
            this.miDrawPolygon.Click += new System.EventHandler(this.miDrawPolygon_Click);
            // 
            // miAddLine
            // 
            this.miAddLine.Name = "miAddLine";
            this.miAddLine.Size = new System.Drawing.Size(165, 22);
            this.miAddLine.Text = "添加线";
            this.miAddLine.Click += new System.EventHandler(this.miAddLine_Click);
            // 
            // gIS分析ToolStripMenuItem
            // 
            this.gIS分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSpatialFilter,
            this.miBuffer,
            this.miStatistic});
            this.gIS分析ToolStripMenuItem.Name = "gIS分析ToolStripMenuItem";
            this.gIS分析ToolStripMenuItem.Size = new System.Drawing.Size(64, 25);
            this.gIS分析ToolStripMenuItem.Text = "GIS分析";
            // 
            // miSpatialFilter
            // 
            this.miSpatialFilter.Name = "miSpatialFilter";
            this.miSpatialFilter.Size = new System.Drawing.Size(136, 22);
            this.miSpatialFilter.Text = "空间查询";
            this.miSpatialFilter.Click += new System.EventHandler(this.miSpatialFilter_Click);
            // 
            // miBuffer
            // 
            this.miBuffer.Name = "miBuffer";
            this.miBuffer.Size = new System.Drawing.Size(136, 22);
            this.miBuffer.Text = "缓冲区分析";
            this.miBuffer.Click += new System.EventHandler(this.miBuffer_Click);
            // 
            // miStatistic
            // 
            this.miStatistic.Name = "miStatistic";
            this.miStatistic.Size = new System.Drawing.Size(136, 22);
            this.miStatistic.Text = "要素统计";
            this.miStatistic.Click += new System.EventHandler(this.miStatistic_Click);
            // 
            // 栅格管理ToolStripMenuItem
            // 
            this.栅格管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCreateRaster,
            this.miCreatePerson,
            this.miRasterMosaic});
            this.栅格管理ToolStripMenuItem.Name = "栅格管理ToolStripMenuItem";
            this.栅格管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.栅格管理ToolStripMenuItem.Text = "栅格管理";
            // 
            // miCreateRaster
            // 
            this.miCreateRaster.Name = "miCreateRaster";
            this.miCreateRaster.Size = new System.Drawing.Size(160, 22);
            this.miCreateRaster.Text = "创建栅格数据集";
            this.miCreateRaster.Click += new System.EventHandler(this.miCreateRaster_Click);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(191, 57);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(668, 462);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnDoubleClickEventHandler(this.axMapControl1_OnDoubleClick);
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 29);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(859, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axTOCControl1.Location = new System.Drawing.Point(3, 57);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(188, 462);
            this.axTOCControl1.TabIndex = 4;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(466, 278);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 57);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 484);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarXY});
            this.statusStrip1.Location = new System.Drawing.Point(3, 519);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(856, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusBar1";
            // 
            // statusBarXY
            // 
            this.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarXY.Name = "statusBarXY";
            this.statusBarXY.Size = new System.Drawing.Size(57, 17);
            this.statusBarXY.Text = "Test 123";
            // 
            // cbookmarklist
            // 
            this.cbookmarklist.Name = "cbookmarklist";
            this.cbookmarklist.Size = new System.Drawing.Size(61, 4);
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(191, 57);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(668, 462);
            this.axPageLayoutControl1.TabIndex = 8;
            this.axPageLayoutControl1.Visible = false;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            // 
            // miCreatePerson
            // 
            this.miCreatePerson.Name = "miCreatePerson";
            this.miCreatePerson.Size = new System.Drawing.Size(160, 22);
            this.miCreatePerson.Text = "创建个人数据库";
            this.miCreatePerson.Click += new System.EventHandler(this.miCreatePerson_Click);
            // 
            // miRasterMosaic
            // 
            this.miRasterMosaic.Name = "miRasterMosaic";
            this.miRasterMosaic.Size = new System.Drawing.Size(160, 22);
            this.miRasterMosaic.Text = "影像镶嵌";
            this.miRasterMosaic.Click += new System.EventHandler(this.miRasterMosaic_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 541);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.miCreate1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.miCreate1;
            this.Name = "MainForm";
            this.Text = "ArcEngine Controls Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.miCreate1.ResumeLayout(false);
            this.miCreate1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip miCreate1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNewDoc;
        private System.Windows.Forms.ToolStripMenuItem menuOpenDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExitApp;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarXY;
        private System.Windows.Forms.ToolStripComboBox cbBookmarkList;
        private System.Windows.Forms.ContextMenuStrip cbookmarklist;
        private System.Windows.Forms.ToolStripMenuItem miCreateBookmark;
        private System.Windows.Forms.ToolStripMenuItem sdata;
        private System.Windows.Forms.ToolStripMenuItem miAccessData;
        private System.Windows.Forms.ToolStripComboBox cbLayer;
        private System.Windows.Forms.ToolStripMenuItem miData;
        private System.Windows.Forms.ToolStripMenuItem miCreateShapefile;
        private System.Windows.Forms.ToolStripMenuItem tbRender;
        private System.Windows.Forms.ToolStripMenuItem tsmSimpleRender;
        private System.Windows.Forms.ToolStripMenuItem miGetRendererInfo;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private System.Windows.Forms.ToolStripMenuItem miPrint;
        private System.Windows.Forms.ToolStripMenuItem miOutput;
        private System.Windows.Forms.ToolStripMenuItem 地图显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miMap;
        private System.Windows.Forms.ToolStripMenuItem miPageLayout;
        private System.Windows.Forms.ToolStripMenuItem miAddFeature;
        private System.Windows.Forms.ToolStripMenuItem miDrawPolygon;
        private System.Windows.Forms.ToolStripMenuItem miCreatePolygon;
        private System.Windows.Forms.ToolStripMenuItem miCreateLine;
        private System.Windows.Forms.ToolStripMenuItem miAddLine;
        private System.Windows.Forms.ToolStripMenuItem gIS分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSpatialFilter;
        private System.Windows.Forms.ToolStripMenuItem miBuffer;
        private System.Windows.Forms.ToolStripMenuItem miStatistic;
        private System.Windows.Forms.ToolStripMenuItem 栅格管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCreateRaster;
        private System.Windows.Forms.ToolStripMenuItem miCreatePerson;
        private System.Windows.Forms.ToolStripMenuItem miRasterMosaic;
    }
}


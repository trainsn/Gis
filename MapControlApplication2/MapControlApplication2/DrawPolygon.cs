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

// namespace ESRI.ArcGIS.ADF.BaseClasses
namespace MapControlApplication2
{
    class DrawPolygon : ESRI.ArcGIS.ADF.BaseClasses.BaseTool
    {
        private IGeometry _polygon = null;//定义一个几何对象，作为绘制结果
        private INewPolygonFeedback _polyFeedback = null;//定义一个多边形反馈对象
        private IPoint _startPoint = null;//多边形起始节点
        private IPoint _endPoint = null;//多边形终止节点

        private bool _drawStart = false; //多边形绘制开始标记
        //public event AfterDrawGeometry eventAfterDrawGeometry;

        protected AxMapControl myMapControl = null;
        protected IHookHelper myHook;

        //返回结果多边形
        public IGeometry Polygon
        {
            get { return _polygon; }
        }

        public override void OnCreate(object hook)
        {
            myHook.Hook = hook;
            if (myHook==null)
            {
                myHook = new HookHelperClass();
               /* myHook.Hook = hook;*/
            }

            if (_drawStart)
            {
                (myHook.Hook as IMapControl3).CurrentTool = this;
                _polyFeedback = new NewPolygonFeedbackClass();
                _polyFeedback.Display = myHook.ActiveView.ScreenDisplay;
            }
            return;
        }

        //如果写在mainform只需要这一部分
        public override void OnClick()//单机鼠标开始绘制图或添加点
        {
            _polygon = null;//每次重设多边形为空值
            _drawStart=true; //开始绘制标记置为true

            myHook = new HookHelperClass();
            (myHook.Hook as IMapControl3).CurrentTool=this;
            _polyFeedback=new NewPolygonFeedbackClass();
            _polyFeedback.Display=myHook.ActiveView.ScreenDisplay;
        }

        //x,y指屏幕上位置
        public override void OnMouseDown(int Button,int Shift,int X,int Y)
        {
            //button=1 左键
            if (Button==1)
            {
                if (_startPoint==null)//如果是多边形的第一个点
                {
                    _startPoint = (myHook.FocusMap as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    _polyFeedback.Start(_startPoint);
                }
                else 
                {
                    _endPoint = (myHook.FocusMap as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    _polyFeedback.AddPoint(_endPoint);
                }
            }
        }

        public override void OnMouseMove(int Button,int Shift,int X,int Y)
        {
            if (_startPoint!=null)
            {
                IPoint movePoint = (myHook.FocusMap as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                _polyFeedback.MoveTo(movePoint);//鼠标移动过程中实时显示反馈效果
            }
        }

        public override void Refresh(int hDC)
        {
            base.Refresh(hDC);
            if (_polyFeedback!=null)
            {
                (_polyFeedback as IDisplayFeedback).Refresh(hDC);//实时显示反馈效果
            }
        }

        public override void OnDblClick()//双击鼠标结束绘图
        {
            _polygon = _polyFeedback.Stop();
            _startPoint = null;
            _drawStart = false;
        }
    }
}

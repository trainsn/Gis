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
        private IGeometry _polygon = null;//����һ�����ζ�����Ϊ���ƽ��
        private INewPolygonFeedback _polyFeedback = null;//����һ������η�������
        private IPoint _startPoint = null;//�������ʼ�ڵ�
        private IPoint _endPoint = null;//�������ֹ�ڵ�

        private bool _drawStart = false; //����λ��ƿ�ʼ���
        //public event AfterDrawGeometry eventAfterDrawGeometry;

        protected AxMapControl myMapControl = null;
        protected IHookHelper myHook;

        //���ؽ�������
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

        //���д��mainformֻ��Ҫ��һ����
        public override void OnClick()//������꿪ʼ����ͼ����ӵ�
        {
            _polygon = null;//ÿ����������Ϊ��ֵ
            _drawStart=true; //��ʼ���Ʊ����Ϊtrue

            myHook = new HookHelperClass();
            (myHook.Hook as IMapControl3).CurrentTool=this;
            _polyFeedback=new NewPolygonFeedbackClass();
            _polyFeedback.Display=myHook.ActiveView.ScreenDisplay;
        }

        //x,yָ��Ļ��λ��
        public override void OnMouseDown(int Button,int Shift,int X,int Y)
        {
            //button=1 ���
            if (Button==1)
            {
                if (_startPoint==null)//����Ƕ���εĵ�һ����
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
                _polyFeedback.MoveTo(movePoint);//����ƶ�������ʵʱ��ʾ����Ч��
            }
        }

        public override void Refresh(int hDC)
        {
            base.Refresh(hDC);
            if (_polyFeedback!=null)
            {
                (_polyFeedback as IDisplayFeedback).Refresh(hDC);//ʵʱ��ʾ����Ч��
            }
        }

        public override void OnDblClick()//˫����������ͼ
        {
            _polygon = _polyFeedback.Stop();
            _startPoint = null;
            _drawStart = false;
        }
    }
}

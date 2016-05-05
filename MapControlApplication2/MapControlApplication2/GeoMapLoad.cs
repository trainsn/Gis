using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;

namespace MapControlApplication2
{
    class GeoMapLoad
    {
        public static void CopyAndOverwriteMap(AxMapControl axMapControl, AxPageLayoutControl axPageLayoutControl)
        {
            IObjectCopy objectCopy = new ObjectCopyClass();
            object toCopyMap = axMapControl.Map;
            object copiedMap = objectCopy.Copy(toCopyMap);
            object overwriteMap = axPageLayoutControl.ActiveView.FocusMap;
            objectCopy.Overwrite(toCopyMap, ref overwriteMap);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;

namespace Yu3zx.Moonlit.Modols
{
    public class MapMarker
    {
        GMapMarker imgmarker;
        private string _devid = "";

        /// <summary>
        /// 地图Mark
        /// </summary>
        public GMapMarker BindMarker
        {
            set { imgmarker = value; }
            get { return imgmarker; }
        }
        /// <summary>
        /// 标识ID
        /// </summary>
        public string DevID
        {
            set { _devid = value; }
            get { return _devid; }
        }
    }
}

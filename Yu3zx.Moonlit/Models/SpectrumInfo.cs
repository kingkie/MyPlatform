using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steema.TeeChart.Styles;

namespace Yu3zx.Moonlit.Modols
{
    public class SpectrumInfo
    {
        private string _devid = "";
        private Line _line;

        public SpectrumInfo()
        {
            _line = new Line();
        }
        /// <summary>
        /// 频谱数据ID
        /// </summary>
        public string DevId
        {
            set { _devid = value; }
            get { return _devid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Line DevLine
        {
            set { _line = value; }
            get { return _line; }
        }
    }
}

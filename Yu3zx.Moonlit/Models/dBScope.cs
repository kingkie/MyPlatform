using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Modols
{
    public class dBScope
    {
        private float _maxvalue;
        private float _minvalue;
        private string _spectrumid;
        private string _devid;

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DevId
        {
            set { _devid = value; }
            get { return _devid; }
        }

        /// <summary>
        /// 量程ID，以频率作为ID
        /// </summary>
        public string ScopeId
        {
            set { _spectrumid = value; }
            get { return _spectrumid; }
        }
        /// <summary>
        /// 量程最大值
        /// </summary>
        public float MaxScope
        {
            set { _maxvalue = value; }
            get { return _maxvalue; }
        }
        /// <summary>
        /// 量程最小值
        /// </summary>
        public float MinScope
        {
            set { _minvalue = value; }
            get { return _minvalue; }
        }
    }
}

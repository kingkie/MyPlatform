using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Modols
{
    public class SiteInfo
    {
        /// <summary>
        /// 场强范围
        /// </summary>
        public List<dBScope> FieldScopes;

        public SiteInfo()
        {
            FieldScopes = new List<dBScope>();
        }

        public string DevId
        {
            set;
            get;
        }
        public string DevAddr
        {
            set;
            get;
        }
        public string DevMac
        {
            set;
            get;
        }
        public string GPS
        {
            set;
            get;
        }
        public string IPAddrAndPort
        {
            set;
            get;
        }
    }
}

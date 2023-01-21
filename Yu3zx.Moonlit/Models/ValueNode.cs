using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yu3zx.Moonlit.Modols
{
    public class ValueNode
    {
        private string _nodeid;
        private float _nodevalue;

        /// <summary>
        /// 绑定的结点ID，一般为频率ID
        /// </summary>
        public string NodeId
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }
        /// <summary>
        /// 绑定的结点值
        /// </summary>
        [JsonIgnore]
        public float NodeValue
        {
            set { _nodevalue = value; }
            get { return _nodevalue; }
        }
    }
}

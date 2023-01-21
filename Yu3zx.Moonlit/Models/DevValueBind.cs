using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Modols
{
    /// <summary>
    /// 配置绑定收到的数据
    /// </summary>
    public class DevValueBind
    {
        /// <summary>
        /// 绑定的ValueNode
        /// </summary>
        public List<ValueNode> ValueNodes;

        public DevValueBind()
        {
            ValueNodes = new List<ValueNode>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inBytes"></param>
        public void SetNodeValues(byte[] inBytes)
        {

        }

        /// <summary>
        /// 设置结点值
        /// </summary>
        /// <param name="inFloats"></param>
        public void SetNodeValues(float[] inFloats)
        {
            int cnt = Math.Min(inFloats.Length,ValueNodes.Count);
            for (int i = 0; i < cnt; i++)
            {
                ValueNodes[i].NodeValue = inFloats[i];
            }
        }

        public void SetNodeValues(string nodeid, float nodevalue)
        {
            ValueNode setNode = FindNode(nodeid);
            if (setNode != null)
            {
                setNode.NodeValue = nodevalue;
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public ValueNode FindNode(string nodeid)
        {
            if (string.IsNullOrEmpty(nodeid))
            {
                return null;
            }
            return ValueNodes.Find(x => x.NodeId == nodeid);
        }

    }
}

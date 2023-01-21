using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    /// <summary>
    /// 面料标签,外部套管标签
    /// </summary>
    public class FabricLabel
    {
        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string QualityName
        {
            get;
            set;
        }
        /// <summary>
        /// 色号
        /// </summary>
        public string ColorNum
        {
            get;
            set;
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs
        {
            get;
            set;
        }
        /// <summary>
        /// 数量
        /// </summary>
        public float ProduceNum
        {
            get;
            set;
        }
        /// <summary>
        /// 卷号--自动编号
        /// </summary>
        public int ReelNum
        {
            get;
            set;
        }
    }
}

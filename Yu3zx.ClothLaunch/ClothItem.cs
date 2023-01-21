using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.ClothLaunch
{
    [Serializable]
    /// <summary>
    /// 线上产品
    /// </summary>
    public class ClothItem
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public int LineNum
        {
            get;
            set;
        }
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
        /// 长度
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
        } = 0;
    }
}

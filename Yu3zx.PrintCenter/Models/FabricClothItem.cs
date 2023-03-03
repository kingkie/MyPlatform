using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.PrintCenter
{
    /// <summary>
    /// 布料，线上产品
    /// </summary>
    public class FabricClothItem
    {
        /// <summary>
        /// 生产线号
        /// </summary>
        public string LineNum
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
        /// 品名,质量标志
        /// </summary>
        public string QualityName
        {
            get;
            set;
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string QualityString
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
        /// 布料宽度
        /// </summary>
        public int FabricWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 一卷直径
        /// </summary>
        public int RollDiam
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

        /// <summary>
        /// 随机数
        /// </summary>
        public string RndString
        {
            get;
            set;
        }
    }
}

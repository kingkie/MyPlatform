using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.ClothLaunch
{
    public class OnLaunchItem
    {
        public long Id { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// 质量标志
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
        /// 是否是批次最后一个
        /// </summary>
        public bool BLast
        {
            get;
            set;
        }
    }
}

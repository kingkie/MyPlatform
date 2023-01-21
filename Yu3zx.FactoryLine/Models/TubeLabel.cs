using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.FactoryLine.Models
{
    /// <summary>
    /// 纸管标签
    /// </summary>
    public class TubeLabel
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
        /// 色号
        /// </summary>
        public string ColorNum
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
        /// 卷号
        /// </summary>
        public int ReelNum
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    /// <summary>
    /// 纸管标签
    /// </summary>
    public class PaperTubeLabel
    {
        /// <summary>
        /// 批号 BN
        /// </summary>
        public string BatchNo
        {
            get;
            set;
        }

        /// <summary>
        /// 数量,长度 PN
        /// </summary>
        public float ProduceNum
        {
            get;
            set;
        }
        /// <summary>
        /// 卷号 RN
        /// </summary>
        public int ReelNum
        {
            get;
            set;
        }
    }
}

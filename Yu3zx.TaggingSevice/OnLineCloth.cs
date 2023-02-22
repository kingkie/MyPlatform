using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    /// <summary>
    /// 当前产线上面料信息
    /// </summary>
    public class OnLineCloth
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string LineNum
        {
            get;
            set;
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// A品数量
        /// </summary>
        public int AClassSum
        {
            get;
            set;
        }
        /// <summary>
        /// 上线的面料-A品
        /// </summary>
        public List<FabricClothItem> ClothItems = new List<FabricClothItem>();

        /// <summary>
        /// 其它品质的
        /// </summary>
        public List<FabricClothItem> OtherQualityItem = new List<FabricClothItem>();
    }
}

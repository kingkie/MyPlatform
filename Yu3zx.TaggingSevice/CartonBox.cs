using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class CartonBox
    {
        public string BatchNo
        {
            get;
            set;
        }

        
        /// <summary>
        /// 上线一箱列表
        /// </summary>
        public List<FabricClothItem> OnLaunchItems = new List<FabricClothItem>();
    }
}

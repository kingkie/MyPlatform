using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class CartonBox
    {
        private int indexLauch = 0;
        public string BatchNo
        {
            get;
            set;
        }

        public int LaunchIndex
        {
            get
            {
                return indexLauch;
            }
            set
            {
                indexLauch = value;
            }
        }
        /// <summary>
        /// 箱号
        /// </summary>
        public string BoxNum
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

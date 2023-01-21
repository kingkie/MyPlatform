using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class ProductManager
    {
        private static ProductManager instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static ProductManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new ProductManager();
                }
            }
            return instance;
        }
        /// <summary>
        /// 多少个A品装箱
        /// </summary>
        public int QualityNum
        {
            get;
            set;
        }

        /// <summary>
        /// 上线产品
        /// </summary>
        public Dictionary<string, OnLineCloth> DictOnLineCloths = new Dictionary<string, OnLineCloth>();

    }
}

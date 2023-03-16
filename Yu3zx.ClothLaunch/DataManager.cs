using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.ClothLaunch
{
    public class DataManager
    {
        #region 单例
        private static DataManager instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static DataManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new DataManager();
                }
            }
            return instance;
        }

        #endregion End

        public Queue<FabricClothItem> NeedSend = new Queue<FabricClothItem>();
    }
}

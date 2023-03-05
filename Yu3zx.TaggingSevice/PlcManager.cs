using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class PlcManager
    {
        #region PlcManager管理

        private static PlcManager instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static PlcManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new PlcManager();
                }
            }
            return instance;
        }

        #endregion End

        /// <summary>
        /// PLC服务IP
        /// </summary>
        public string PlcServerIp
        {
            get;
            set;
        }
        /// <summary>
        /// PLC服务端口
        /// </summary>
        public int Port
        {
            get;
            set;
        }
    }
}

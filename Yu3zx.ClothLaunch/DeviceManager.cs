using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.ClothLaunch
{
    public class DeviceManager
    {
        #region 单例
        private static DeviceManager instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static DeviceManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new DeviceManager();
                }
            }
            return instance;
        }

        #endregion End

        public TcpClient ClothClient
        {
            get;
            set;
        }


    }
}

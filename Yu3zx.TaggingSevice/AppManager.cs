using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Xml;

namespace Yu3zx.TaggingSevice
{
    public class AppManager
    {
        #region 单例
        private static AppManager instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static AppManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new AppManager();
                }
            }
            return instance;
        }

        #endregion End

        /// <summary>
        /// 服务IP
        /// </summary>
        public string ServerIp
        {
            get;
            set;
        }
        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port
        {
            get;
            set;
        }
        /// <summary>
        /// 自动服务
        /// </summary>
        public bool AutoServer
        {
            get;
            set;
        }
        /// <summary>
        /// 装箱数量
        /// </summary>
        public int PackingNum
        {
            get;
            set;
        }



        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (File.Exists("Config/ItemConfig.xml"))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("Config/ItemConfig.xml");

                    XmlNode vNode = xmlDoc.SelectSingleNode("Configuration/Server"); //
                    ServerIp = vNode.Attributes["ip"].Value.Trim();
                    Port = int.Parse(vNode.Attributes["port"].Value.Trim());
                    string strAuto = vNode.Attributes["auto"].Value.Trim();
                    if(strAuto == "1")
                    {
                        AutoServer = true;
                    }
                    else
                    {
                        AutoServer = false;
                    }

                    XmlNode configNode = xmlDoc.SelectSingleNode("Configuration/LineNum"); //
                    PackingNum = int.Parse(configNode.Attributes["fullnum"].Value.Trim()); //达到装箱数量
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("文件不存在!");
            }
        }

    }
}

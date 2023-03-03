using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public List<PrintCfg> LPrinter = new List<PrintCfg>();

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

                    //PrintConfig 30*25
                    XmlNode printNode = xmlDoc.SelectSingleNode("Configuration/PrintConfig"); //
                    foreach (XmlNode nodeSub in printNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if (!PrintHelper.TempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.TempleteFieldsList.Add(sKey, sMatch);
                        }
                    }
                    //FabricConfig 布卷标签
                    XmlNode fabricNode = xmlDoc.SelectSingleNode("Configuration/FabricConfig"); //
                    foreach (XmlNode nodeSub in fabricNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if (!PrintHelper.FabricTempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.FabricTempleteFieldsList.Add(sKey, sMatch);
                        }
                    }
                    //CartonConfig 箱外标签
                    XmlNode cartonNode = xmlDoc.SelectSingleNode("Configuration/FabricConfig"); //
                    foreach (XmlNode nodeSub in cartonNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if (!PrintHelper.FabricTempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.FabricTempleteFieldsList.Add(sKey, sMatch);
                        }
                    }

                    //PrinterConfig
                    XmlNode pNode = xmlDoc.SelectSingleNode("Configuration/PrinterConfig"); //
                    foreach (XmlNode nSub in pNode.ChildNodes)
                    {
                        PrintCfg cfg = new PrintCfg();
                        cfg.LineNum = nSub.Attributes["linenum"].Value.Trim();
                        cfg.PrinterName = nSub.Attributes["pname"].Value.Trim();
                        cfg.LabelName = nSub.Attributes["lblname"].Value.Trim();

                        cfg.LabelBName = nSub.Attributes["lblowname"].Value.Trim();

                        //---------------
                        cfg.CartonLabel = nSub.Attributes["boxlbl"].Value.Trim();
                        cfg.CartonPrinter = nSub.Attributes["boxprt"].Value.Trim();
                        //---------------
                        cfg.PrintCopies = int.Parse(nSub.Attributes["copies"].Value.Trim());
                        LPrinter.Add(cfg);
                    }
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

        public PrintCfg GetPrintCfg(string lnum)
        {
            var item = LPrinter.Find(x => x.LineNum == lnum);
            if(item == null)
            {

            }
            return item;
        }
    }
}

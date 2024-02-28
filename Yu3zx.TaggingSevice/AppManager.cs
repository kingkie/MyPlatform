﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Xml;

using Yu3zx.DapperExtend;

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

        public string PlcIp
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
                    PlcIp = vNode.Attributes["plcip"].Value.Trim();
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
                    XmlNode cartonNode = xmlDoc.SelectSingleNode("Configuration/CartonConfig"); //
                    foreach (XmlNode nodeSub in cartonNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if (!PrintHelper.CartonTempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.CartonTempleteFieldsList.Add(sKey, sMatch);
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

        public int GetBoxNoAndUpdate(string batchno)
        {
            if (string.IsNullOrEmpty(batchno))
            {
                Logs.Log.Instance.LogWrite("没有batchno！");
                return 0;
            }
            int iSnRtn = 0;
            try
            {
                PoductSerial config = GetPoductSerial("Box" + batchno);
                if (config != null)
                {
                    iSnRtn = int.Parse(config.KeyValue);
                    Logs.Log.Instance.LogWrite(string.Format("箱号:{0}；{1}", batchno, iSnRtn));
                    using (var db = new DapperContext("MySqlDbConnection"))
                    {
                        try
                        {
                            var rtnB = db.Update("update productserial set KeyValue=@KeyValue where KeyName=@KeyName", new { KeyValue = iSnRtn + 1, KeyName = "Box" + batchno });
                            db.Dispose();
                            if (rtnB)
                            {
                                Console.WriteLine("更新成功！");
                            }
                            else
                            {
                                Console.WriteLine("更新失败！");
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    iSnRtn = 1;
                    PoductSerial confignew = new PoductSerial();
                    confignew.KeyName = "Box" + batchno;
                    confignew.KeyValue = (iSnRtn + 1).ToString();
                    SetPoductSerialSave(confignew);
                }
            }
            catch
            { }

            return iSnRtn;
        }

        public PoductSerial GetPoductSerial(string strKey)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var lConfigs = db.Select<PoductSerial>(u => u.KeyName == strKey);
                    if (lConfigs != null && lConfigs.Count > 0)
                    {
                        Console.WriteLine("查询PoductSerial成功");
                        return lConfigs[0];
                    }
                    else
                    {
                        Console.WriteLine("查询PoductSerial失败");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public void SetPoductSerialSave(PoductSerial config)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var rtnBool = db.Insert(config);
                    if (rtnBool)
                    {
                        Console.WriteLine("添加成功");
                    }
                    else
                    {
                        Console.WriteLine("添加失败");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public Dictionary<string, string> GetEntityPropertyToDict<T>(T tEntity)
        {
            Dictionary<string, string> dictClass = new Dictionary<string, string>();
            Type ty = tEntity.GetType();//获取对象类型
            PropertyInfo[] infos = ty.GetProperties();
            foreach (PropertyInfo item in infos)
            {
                try
                {
                    string pName = item.Name;//获取属性名称
                    string pValue = string.Empty;
                    var itVal = item.GetValue(tEntity, null);
                    if (itVal != null)
                    {
                        pValue = itVal.ToString();
                    }
                    dictClass.Add(pName, pValue);
                }
                catch
                { }
            }
            return dictClass;
        }
    }
}

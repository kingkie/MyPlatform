using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Xml;
using Yu3zx.DapperExtend;

namespace Yu3zx.ClothLaunch
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
        /// 当前生产线
        /// </summary>
        public string LineNum
        {
            get;
            set;
        }
        /// <summary>
        /// 序列号
        /// </summary>
        public int SerialNum
        {
            get;
            set;
        }
        #region 打印相关属性
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName
        {
            get;
            set;
        }
        /// <summary>
        /// 对应打印机名称
        /// </summary>
        public string PrinterName
        {
            get;
            set;
        }

        public int PrintCopies
        {
            get;
            set;
        }
        #endregion End
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
                    LineNum = configNode.Attributes["value"].Value.Trim();

                    //PrintConfig
                    XmlNode printNode = xmlDoc.SelectSingleNode("Configuration/PrintConfig"); //
                    PrinterName = printNode.Attributes["pname"].Value.Trim();
                    LabelName = printNode.Attributes["lblname"].Value.Trim();
                    PrintCopies = int.Parse(printNode.Attributes["copies"].Value.Trim());

                    foreach (XmlNode nodeSub in printNode.ChildNodes)
                    {
                        string sKey = nodeSub.Attributes["dataname"].Value.Trim();
                        string sMatch = nodeSub.Attributes["matchname"].Value.Trim();
                        if(!PrintHelper.TempleteFieldsList.ContainsKey(sKey))
                        {
                            PrintHelper.TempleteFieldsList.Add(sKey, sMatch);
                        }
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

        public int GetSerialNoAndUpdate(string batchno)
        {
            if(string.IsNullOrEmpty(batchno))
            {
                return 0;
            }
            int iSnRtn = 0;
            PoductSerial config = GetPoductSerial(batchno);
            if (config != null)
            {
                iSnRtn = int.Parse(config.KeyValue);
            }
            else
            {
                iSnRtn = 1;
                PoductSerial confignew = new PoductSerial();
                confignew.KeyName = batchno;
                confignew.KeyValue = iSnRtn.ToString();
                SetPoductSerialSave(confignew);
            }
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var rtnB = db.Update("update productserial set KeyValue=@KeyValue where KeyName=@KeyName", new { KeyValue = iSnRtn + 1, KeyName = batchno });
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
            return iSnRtn;
        }

        public bool SetBeginSerialNo(string batchno, int val)
        {
            if (string.IsNullOrEmpty(batchno))
            {
                return false;
            }
            PoductSerial config = GetPoductSerial(batchno);
            if (config != null)
            {
                using (var db = new DapperContext("MySqlDbConnection"))
                {
                    try
                    {
                        var rtnB = db.Update("update productserial set KeyValue=@KeyValue where KeyName=@KeyName", new { KeyValue = val, KeyName = batchno });
                        if (rtnB)
                        {
                            Console.WriteLine("更新成功！");
                            return true;
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
                return false;
            }
            else
            {
                PoductSerial confignew = new PoductSerial();
                confignew.KeyName = batchno;
                confignew.KeyValue = val.ToString();
                SetPoductSerialSave(confignew);
                return true;
            }
            return false;
        }

        public SetConfig GetSetConfig(string strKey)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var lConfigs = db.Select<SetConfig>(u => u.KeyName == strKey);
                    if (lConfigs != null && lConfigs.Count > 0)
                    {
                        Console.WriteLine("获取SetConfig成功");
                        return lConfigs[0];
                    }
                    else
                    {
                        Console.WriteLine("获取SetConfig失败");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
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

        public void SetConfigSave(SetConfig config)
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
    }
}

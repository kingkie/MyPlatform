using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class PrintHelper
    {
        #region 简单单例
        private static PrintHelper instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static PrintHelper CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new PrintHelper();
                }
            }
            return instance;
        }

        #endregion End

        private static BarTender.Application btApp = new BarTender.Application();
        private static BarTender.Format btFormat = new BarTender.Format();

        /// <summary>
        /// 报表打印模板字段集
        /// </summary>
        public static Dictionary<string,string> TempleteFieldsList = new Dictionary<string, string>();

        public void Init()
        {
            if(btApp == null)
            {
                btApp = new BarTender.Application();
            }
        }

        public static Dictionary<string, string> GetEntityPropertyToDict<T>(T tEntity)
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

        /// <summary>
        /// 标签打印
        /// </summary>
        /// <param name="filePath">模板路径</param>
        /// <param name="printerName">打印机名</param>
        /// <param name="dictData">数据字典</param>
        /// <param name="CopiesOfLabel">打印份数</param>
        public void BarPrintInit(string filePath,string printerName, Dictionary<string,string> dictData,int CopiesOfLabel = 1)
        {
            try
            {
                if (btApp == null)
                {
                    btApp = new BarTender.Application();
                }

                btFormat = btApp.Formats.Open(filePath, false, "");
                //if(TempleteFieldsList.Count > 0)
                //{

                //}
                //else
                //{

                //}
                foreach(string skey in TempleteFieldsList.Keys)
                {
                    try
                    {
                        //向bartender模板传递变量,SN为条形码数据的一个共享名称
                        if(dictData.ContainsKey(skey))
                        {
                            string sMatch = TempleteFieldsList[skey];
                            string strVal = dictData[skey];
                            if (string.IsNullOrEmpty(strVal))
                            {
                                strVal = "";
                            }
                            btFormat.SetNamedSubStringValue(sMatch, strVal);
                        }
                    }
                    catch
                    {
                        //处理可能没有对应打印字段的问题
                    }
                }

                //foreach(string key in dictData.Keys)
                //{
                //    try
                //    {
                //        //向bartender模板传递变量,SN为条形码数据的一个共享名称
                //        string strVal = dictData[key];
                //        if(string.IsNullOrEmpty(strVal))
                //        {
                //            strVal = "";
                //        }
                //        btFormat.SetNamedSubStringValue(key, strVal);
                //    }
                //    catch
                //    {
                //        //处理可能没有对应打印字段的问题
                //    }
                //}

                //选择打印机
                btFormat.Printer = printerName;
                //设置打印份数
                btFormat.IdenticalCopiesOfLabel = CopiesOfLabel;
                //设置打印时是否跳出打印属性
                btFormat.PrintOut(false, false);
                //退出时是否保存标签
                //btFormat.Close(BarTender.BtSaveOptions.btSaveChanges);
                btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);
            }
            catch (Exception ex)
            {
                Console.WriteLine("打印错误：" + ex.Message);
                return;
            }
        }

        public void UnInit()
        {
            //btApp.Quit(BarTender.BtSaveOptions.btSaveChanges);
            btApp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
        }
    }
}

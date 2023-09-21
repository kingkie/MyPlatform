using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
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
        /// <summary>
        /// 套装也是
        /// </summary>
        public static Dictionary<string, string> FabricTempleteFieldsList = new Dictionary<string, string>();
        /// <summary>
        /// 箱外标识打印模板字段
        /// </summary>
        public static Dictionary<string, string> CartonTempleteFieldsList = new Dictionary<string, string>();

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

        public void BarPrintInit(string filePath, string printerName, Dictionary<string, string> dictData, Dictionary<string, string> dictTemplate, int CopiesOfLabel = 1)
        {
            try
            {
                if (btApp == null)
                {
                    btApp = new BarTender.Application();
                }

                btFormat = btApp.Formats.Open(filePath, false, "");
                foreach (string skey in dictTemplate.Keys)
                {
                    try
                    {
                        //向bartender模板传递变量,SN为条形码数据的一个共享名称
                        if (dictData.ContainsKey(skey))
                        {
                            string sMatch = dictTemplate[skey];
                            string strVal = dictData[skey];
                            if (string.IsNullOrEmpty(strVal))
                            {
                                strVal = "";
                            }
                            btFormat.SetNamedSubStringValue(sMatch, strVal);
                        }
                    }
                    catch(Exception ex)
                    {
                        //处理可能没有对应打印字段的问题
                        Console.WriteLine(ex);
                    }
                }

                if(!string.IsNullOrEmpty(printerName))
                {
                    //选择打印机
                    btFormat.Printer = printerName;
                }
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
            try
            {
                btFormat?.Close();
                btApp = null;
            }
            catch
            { }
        }

        private void PrintStateView()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer WHERE Default = True");
            string printerName = string.Empty;
            foreach (ManagementObject printer in searcher.Get())
            {
                Console.WriteLine($"---------设备-----------");
                var val1 = printer["WorkOffline"];
                var val2 = printer["Default"];

                var val3 = printer["Status"];
                var val4 = printer["PrinterStatus"];
                var val5 = printer["PrinterState"];
                var val6 = printer["ExtendedPrinterStatus"];
                var val7 = printer["ExtendedDetectedErrorState"];

                var val8 = printer["DetectedErrorState"];
                var val9 = printer["Caption"];
                var val10 = printer["DriverName"];
                var val11 = printer["Queued"];
                var val12 = printer["DeviceID"];
                string strState = $"WorkOffline {val1};\r\nDefault {val2};\r\nStatus {val3};\r\nPrinterStatus {val4};\r\nPrinterState {val5};\r\nExtendedPrinterStatus {val6};\r\nExtendedDetectedErrorState {val7};\r\nDetectedErrorState {val8};\r\nCaption {val9};\r\nDriverName {val10};\r\nQueued {val11};\r\nDeviceID {val12}";
                //txtPrefor.Text = txtCurrent.Text;
                //txtCurrent.Text = strState;
                Console.WriteLine(strState);
                continue;
                foreach (var item in printer.Properties)
                {
                    string typename = item.GetType().Name;
                    //TypeName:PropertyData
                    if (!typename.ToLower().Contains("string") && !typename.ToLower().Contains("propertydata"))
                    {
                        Console.WriteLine($"TypeName:{typename}");
                    }

                    if (typename.ToLower().Contains("array"))
                    {
                        var strValues = item.Value as Array;
                        foreach (var prostr in strValues)
                        {
                            Console.WriteLine($"{prostr.ToString()} =:= {prostr.ToString()}");
                        }
                    }
                    else
                    {

                        if (item.Value != null && item.Value is Array)
                        {
                            var strValues = item.Value as Array;
                            int index = 1;
                            foreach (var prostr in strValues)
                            {
                                Console.WriteLine($"{index}.{item.Name} =-:-= {prostr.ToString()}");
                                index++;
                            }
                        }
                        else
                        {
                            string strVal = item.Value?.ToString();
                            if (!string.IsNullOrEmpty(strVal))
                            {
                                Console.WriteLine($"{item.Name}: {strVal}");
                            }
                        }
                    }
                }
            }
        }

        private string GetStatus(string printerName, string attrStr)
        {
            ManagementObjectSearcher searcher = null;
            if (string.IsNullOrEmpty(printerName))
            {
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            }
            else
            {
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer WHERE DeviceID = " + printerName);
            }
            string getData = string.Empty;
            foreach (ManagementObject printer in searcher.Get())
            {
                switch (attrStr)
                {
                    case "WorkOffline":
                        getData = printer["WorkOffline"].ToString();//False 是否离线
                        break;
                    case "Default":
                        getData = printer["Default"].ToString();//False  是否为默认打印机
                        break;
                    case "PrinterStatus":
                        getData = printer["PrinterStatus"].ToString();//3 其他 = 1,未知 = 2,空闲 = 3,打印 = 4,预热 = 5,停止打印 = 6,脱机 = 7,
                        break;
                    case "PrinterState":
                        getData = printer["PrinterState"].ToString();//0
                        break;
                    case "ExtendedPrinterStatus":
                        getData = printer["ExtendedPrinterStatus"].ToString();//2
                        break;
                    case "ExtendedDetectedErrorState":
                        getData = printer["ExtendedDetectedErrorState"].ToString();// 0
                        break;
                    case "Queued":
                        getData = printer["Queued"].ToString();//False 是否有打印队列
                        break;
                }
                return getData;
            }
            return getData;
        }

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool OpenPrinter(string pPrintName, out IntPtr hPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool GetPrinter(IntPtr hPrinter, int dwLevel, IntPtr pPrinter, int cbBuf, out int pcNeeded);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct PRINTER_INFO_2
        {
            public string pServerName;
            public string pPrinterName;
            public string pshareName;
            public string pPortName;
            public string pDriverName;
            public string pComment;
            public string pLocation;
            public IntPtr pDevMode;
            public string pSepFile;
            public string pPrintProcessor;
            public string pDatatype;
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public uint Attributes;
            public uint Priority;
            public uint DefaultPriority;
            public uint StartTime;
            public uint UntilTime;
            public uint Status;
            public uint cJobs;
            public uint AverageppM;
        }

        private int GetPrinterSatusInt(string printname)
        {
            int intRet = 0;
            IntPtr hPrinter;
            if (OpenPrinter(printname, out hPrinter, IntPtr.Zero))
            {
                int cbNeeded = 0;
                bool bolRet = GetPrinter(hPrinter, 2, IntPtr.Zero, 0, out cbNeeded);
                if (cbNeeded > 0)
                {
                    IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
                    bolRet = GetPrinter(hPrinter, 2, pAddr, cbNeeded, out cbNeeded);
                    if (bolRet)
                    {
                        PRINTER_INFO_2 Info2 = new PRINTER_INFO_2();
                        Info2 = (PRINTER_INFO_2)Marshal.PtrToStructure(pAddr, typeof(PRINTER_INFO_2));
                        intRet = System.Convert.ToInt32(Info2.Status);
                    }
                    Marshal.FreeHGlobal(pAddr);
                }
                ClosePrinter(hPrinter);
            }
            return intRet;
        }

        private string GetPrinterStatus(string printName)
        {
            int intValue = GetPrinterSatusInt(printName);
            string strRet = string.Empty;
            switch (intValue)
            {
                case 0:
                    strRet = "准备就绪(Ready)";
                    break;
                case 0x00000200:
                    strRet = "忙(Busy)";
                    break;
                case 0x00400000:
                    strRet = "被打开(Printer Door Open)";
                    break;
                case 0x00000002:
                    strRet = "错误(Printer Error)";
                    break;
                case 0x00008000:
                    strRet = "初始化(Initializing)";
                    break;
                case 0x00000100:
                    strRet = "正在输入输出(I/O Active)";
                    break;
                case 0x00000020:
                    strRet = "手工送纸(Manual Feed)";
                    break;
                case 0x00040000:
                    strRet = "无墨粉(No Toner)";
                    break;
                case 0x00001000:
                    strRet = "不可用(Not Available)";
                    break;
                case 0x00000080:
                    strRet = "脱机(Off Line)";
                    break;
                case 0x00200000:
                    strRet = "内存溢出(Out of Memory)";
                    break;
                case 0x00000800:
                    strRet = "输出口已满(Out Bin Full)";
                    break;

                case 0x00080000:
                    strRet = "当前页无法打印(Page Punt)";
                    break;
                case 0x00000008:
                    strRet = "塞纸(Paper Jam)";
                    break;

                case 0x00000010:
                    strRet = "打印纸用完(Page Out)";
                    break;
                case 0x00000040:
                    strRet = "纸张问题(Paper Problem)";
                    break;
                case 0x00000001:
                    strRet = "暂停(Paused)";
                    break;
                case 0x00000004:
                    strRet = "正在删除(Pending Deletion)";
                    break;
                case 0x00000400:
                    strRet = "正在打印(Printing)";
                    break;
                case 0x00004000:
                    strRet = "正在处理(Processing)";
                    break;
                case 0x00020000:
                    strRet = "墨粉不足(Toner Low)";
                    break;
                case 0x00100000:
                    strRet = "需要用户千预(User Intervention)";
                    break;
                case 0x20000000:
                    strRet = "等待(Waiting)";
                    break;
                case 0x00010000:
                    strRet = "热机中(Warming Up)";
                    break;
                default:
                    strRet = "未状态(Unknown status)";
                    break;
            }

            return strRet;
        }
    }
}

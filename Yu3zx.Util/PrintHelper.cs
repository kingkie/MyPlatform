using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Yu3zx.Util
{
    public class PrintHelper
    {
        /// <summary>
        /// 设置默认打印机API
        /// </summary>
        /// <param name="name">打印机名</param>
        /// <returns>设置结果</returns>
        [DllImport("winspool.drv")]
        private static extern bool SetDefaultPrinter(string name); //调用win api将指定名称的打印机设置为默认打印机

        /// <summary>
        /// 获取本机所有打印机
        /// </summary>
        /// <returns>打印机列表</returns>
        public static List<string> GetLocalPrinters()
        {
            List<string> fPrinters = new List<string>();
            //fPrinters.Add(DefaultPrinter()); //默认打印机始终出现在列表的第一项
            foreach (string fPrinterName in PrinterSettings.InstalledPrinters)
            {
                fPrinters.Add(fPrinterName);
            }
            return fPrinters;
        }

        /// <summary>
        /// 设置默认打印机
        /// </summary>
        /// <param name="prtName">打印机名</param>
        /// <returns>设置结果</returns>
        public static bool SetDefaultPrint(string prtName)
        {
            try
            {
                if (string.IsNullOrEmpty(prtName))
                {
                    return false;
                }
                if (GetLocalPrinters().Contains(prtName))
                {
                    return SetDefaultPrinter(prtName);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("设置默认打印机异常", ex);
                return false;
            }
        }


    }
}

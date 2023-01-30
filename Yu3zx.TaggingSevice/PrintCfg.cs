using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class PrintCfg
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string LineNum
        {
            get;
            set;
        }
        /// <summary>
        /// 打印机名
        /// </summary>
        public string PrinterName
        {
            get;
            set;
        }
        /// <summary>
        /// 打印标签模板文件名
        /// </summary>
        public string LabelName
        {
            get;
            set;
        }
        /// <summary>
        /// 打印份数
        /// </summary>
        public int PrintCopies
        {
            get;
            set;
        }
    }
}

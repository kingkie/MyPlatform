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
        /// 次品打印标签模板文件名
        /// </summary>
        public string LabelBName
        {
            get;
            set;
        }
        /// <summary>
        /// 箱外标签模板
        /// </summary>
        public string CartonLabel
        {
            get;
            set;
        }
        /// <summary>
        /// 箱外标签打印机名
        /// </summary>
        public string CartonPrinter
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

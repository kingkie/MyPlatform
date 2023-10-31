using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.PrintCenter
{
    /// <summary>
    /// 打印装箱单子
    /// </summary>
    public class CartonBoxLabel
    {
        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// 箱号
        /// </summary>
        public string BoxNum
        {
            get;
            set;
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string QualityString
        {
            get;
            set;
        }
        /// <summary>
        /// 色号
        /// </summary>
        public string ColorNum
        {
            get;
            set;
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs
        {
            get;
            set;
        }

        #region 6卷长度及总长度
        /// <summary>
        /// 卷一
        /// </summary>
        public decimal RollNum1
        {
            get;
            set;
        }
        /// <summary>
        /// 卷二
        /// </summary>
        public decimal RollNum2
        {
            get;
            set;
        }
        /// <summary>
        /// 卷三
        /// </summary>
        public decimal RollNum3
        {
            get;
            set;
        }
        /// <summary>
        /// 卷四
        /// </summary>
        public decimal RollNum4
        {
            get;
            set;
        }
        /// <summary>
        /// 卷五
        /// </summary>
        public decimal RollNum5
        {
            get;
            set;
        }
        /// <summary>
        /// 卷六
        /// </summary>
        public decimal RollNum6
        {
            get;
            set;
        }
        /// <summary>
        /// 合计
        /// </summary>
        public decimal RollSum
        {
            get
            {
                return (RollNum1 + RollNum2 + RollNum3 + RollNum4 + RollNum5 + RollNum6);
            }
        }

        #endregion

        public int ReelNum1
        {
            get;
            set;
        }

        public int ReelNum2
        {
            get;
            set;
        }

        public int ReelNum3
        {
            get;
            set;
        }

        public int ReelNum4
        {
            get;
            set;
        }

        public int ReelNum5
        {
            get;
            set;
        }

        public int ReelNum6
        {
            get;
            set;
        }

    }
}

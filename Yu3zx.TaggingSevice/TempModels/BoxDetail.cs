using System;
using System.Collections.Generic;
using System.Text;

namespace Yu3zx.TaggingSevice
{
    public class BoxDetail: IComparable
    {
        /// <summary>
        /// 等级名称
        /// </summary>
        public string GradeName
        {
            get;
            set;
        }

        public string BoxNum
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

        //public int CompareTo(object obj)
        //{
        //    return this.GradeName.CompareTo((obj as BoxDetail).GradeName);
        //}

        public int CompareTo(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return 1; // 非空检查，如果obj为null则返回1（正数），表示当前实例大于比较对象
                }

                BoxDetail otherbox = obj as BoxDetail;
                if (otherbox != null)
                {
                    return this.GradeName.CompareTo(otherbox.GradeName);
                }
                else
                {
                    return 0;
                    //throw new ArgumentException("Object is not a Person"); // 如果比较对象不是Person类型则抛出异常
                }
            }
            catch
            {

            }
            return 0;
        }
    }
}

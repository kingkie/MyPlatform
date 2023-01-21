using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    public class PageModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 开始行
        /// </summary>
        public int RowBegin
        {
            get { return ((PageIndex - 1) * PageSize) + 1; }
        }

        /// <summary>
        /// 结束行
        /// </summary>
        public int RowEnd
        {
            get { return (PageIndex * PageSize); }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int DataRows { get; set; }
    }
}

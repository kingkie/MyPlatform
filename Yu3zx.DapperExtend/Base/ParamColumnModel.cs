using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// 生成SQL时参数里面的列名和对应值名称
    /// </summary>
    public class ParamColumnModel
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 对应类属性名
        /// </summary>
        public string FieldName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// 列字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnAttribute : BaseAttribute
    {
        /// <summary>
        /// 自增长
        /// </summary>
        public bool AutoIncrement { get; set; }
        public ColumnAttribute()
        {
            AutoIncrement = false;
        }
        /// <summary>
        /// 是否是自增长
        /// </summary>
        /// <param name="autoIncrement"></param>
        public ColumnAttribute(bool autoIncrement)
        {
            AutoIncrement = autoIncrement;
        }

        /// <summary>
        /// 是否是自增长
        /// </summary>
        /// <param name="autoIncrement"></param>
        public ColumnAttribute(string name = null)
        {
            Name = name;
        }

        /// <summary>
        /// 别名，对应数据里面的名字
        /// </summary>
        public string Name { get; set; }
    }
}

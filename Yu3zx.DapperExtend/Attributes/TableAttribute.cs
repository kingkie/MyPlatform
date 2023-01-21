using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// 数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class TableAttribute : BaseAttribute
    {
        /// <summary>
        /// 表的名称;对应数据里面的名字
        /// </summary>
        public string Name { get; set; }

        public TableAttribute(string name)
        {
            this.Name = name;
        }
    }
}

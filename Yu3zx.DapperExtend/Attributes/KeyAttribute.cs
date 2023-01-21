using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class KeyAttribute : BaseAttribute
    {
        /// <summary>
        /// 是否为自动主键
        /// </summary>
        public bool CheckAutoId { get; set; }
       
        public KeyAttribute()
        {
            this.CheckAutoId = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkAutoId">是否为自动主键</param>
        public KeyAttribute(bool checkAutoId)
        {
            this.CheckAutoId = checkAutoId;
        }
    }
}

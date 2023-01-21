using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.DapperExtend;

namespace Yu3zx.PackingMonitor.Models
{
    public class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            //AddTime = DateTime.Now;
        }

        /// <summary>
        /// 主键编号
        /// </summary>
        [Key(true)]
        public virtual long Id { get; set; }
    }
}

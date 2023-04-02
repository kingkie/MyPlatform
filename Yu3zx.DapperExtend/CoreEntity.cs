using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// 核心基类
    /// </summary>
    public abstract class CoreEntity : IEntity
    {
        protected CoreEntity()
        {
            AddTime = DateTime.Now;
        }

        /// <summary>
        /// 主键编号
        /// </summary>
        public virtual long Id { get; set; }

        private DateTime? _addTime;

        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("addtime")]
        public DateTime AddTime
        {
            get
            {
                return _addTime.HasValue ? _addTime.Value : DateTime.Now;
            }
            set { _addTime = value; }
        }
    }
}

using System;
using Yu3zx.DapperExtend;

namespace MyPlatForms
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            Valid = 1;
            AddTime = DateTime.Now;
            ModifiedTime = DateTime.Now;
        }

        /// <summary>
        /// 主键编号
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [Column("AddBy")]
        public virtual long? AddBy { get; set; }

        private DateTime? _addTime;

        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("AddTime")]
        public DateTime AddTime
        {
            get
            {
                return _addTime.HasValue ? _addTime.Value : DateTime.Now;
            }
            set { _addTime = value; }
        }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Column("LastModifiedBy")]
        public long? ModifiedBy { get; set; }

        private DateTime? _modifiedTime = null;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Column("LastModifiedTime")]
        public DateTime ModifiedTime
        {
            get
            {
                return _modifiedTime.HasValue ? _modifiedTime.Value : DateTime.Now;
            }
            set { _modifiedTime = value; }
        }

        private int _valid = -1;

        /// <summary>
        /// 是否有效（1有效，0失效）
        /// </summary>
        [Column("Valid")]
        public int Valid
        {
            get
            {
                return _valid < 0 ? 1 : _valid;
            }
            set
            {
                _valid = value;
            }
        }
    }
}

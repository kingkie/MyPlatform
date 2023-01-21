using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu3zx.DapperExtend;

namespace Yu3zx.ClothLaunch
{
    [Table("setconfigs")]
    public class SetConfig : BaseEntity
    {
        [Key]
        [Column("Id")]
        public override long Id { get; set; }

        /// <summary>
        /// 键名称
        /// </summary>
        [Column("KeyName")]
        public string KeyName
        {
            get;
            set;
        }
        /// <summary>
        /// 键值
        /// </summary>
        [Column("KeyValue")]
        public string KeyValue
        {
            get;
            set;
        }
    }
}

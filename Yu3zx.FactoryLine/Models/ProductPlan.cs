using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu3zx.DapperExtend;
using Yu3zx.FactoryLine.DataModels;

namespace Yu3zx.FactoryLine.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("productplan")]
    public class ProductPlan : BaseEntity
    {
        [Key]
        [Column("id")]
        public override long Id { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        [Column("BatchNo")]
        public string BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// 色号
        /// </summary>
        [Column("ColorNum")]
        public string ColorNum
        {
            get;
            set;
        }
        /// <summary>
        /// 规格
        /// </summary>
        [Column("Specs")]
        public string Specs
        {
            get;
            set;
        }
        /// <summary>
        /// t生产总量
        /// </summary>
        [Column("ProduceNum")]
        public float ProduceNum
        {
            get;
            set;
        }
        /// <summary>
        /// 生产线号
        /// </summary>
        [Column("LineNum")]
        public int LineNum
        {
            get;
            set;
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        [Column("IsFinish")]
        public int IsFinish
        {
            get;
            set;
        } = 0;

        private DateTime? pTime;

        /// <summary>
        /// 生产时间
        /// </summary>
        [Column("ProduceTime")]
        public DateTime? ProduceTime
        {
            get
            {
                return pTime.HasValue ? pTime.Value : DateTime.Now;
            }
            set { pTime = value; }
        }
    }
}

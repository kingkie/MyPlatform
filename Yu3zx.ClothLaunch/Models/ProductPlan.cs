using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu3zx.ClothLaunch;
using Yu3zx.DapperExtend;


namespace Yu3zx.ClothLaunch
{
    /// <summary>
    /// 计划
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
        /// 品名
        /// </summary>
        [Column("QualityString")]
        public string QualityString
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
        public string LineNum
        {
            get;
            set;
        }
        /// <summary>
        /// 布料宽度
        /// </summary>
        [Column("FabricWidth")]
        public int FabricWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 一卷直径
        /// </summary>
        [Column("RollDiam")]
        public int RollDiam
        {
            get;
            set;
        }

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

        /// <summary>
        /// 是否完成
        /// </summary>
        [Column("IsFinish")]
        public int IsFinish
        {
            get;
            set;
        }
    }
}

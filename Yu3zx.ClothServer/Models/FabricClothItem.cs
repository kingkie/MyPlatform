using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu3zx.DapperExtend;

namespace Yu3zx.ClothServer.Models
{
    /// <summary>
    /// 布料
    /// </summary>
    [Table("fabric_cloths")]
    public class FabricClothItem : BaseEntity
    {
        [Key]
        [Column("id")]
        public override long Id { get; set; }

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
        /// 批号
        /// </summary>
        [Column("BatchNo")]
        public string BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// 质量标志
        /// </summary>
        [Column("QualityName")]
        public string QualityName
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
        /// 长度
        /// </summary>
        [Column("ProduceNum")]
        public float ProduceNum
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

        /// <summary>
        /// 卷号--自动编号
        /// </summary>
        [Column("ReelNum")]
        public int ReelNum
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
        /// <summary>
        /// 随机数
        /// </summary>
        [Column("RndString")]
        public string RndString
        {
            get;
            set;
        }
    }
}

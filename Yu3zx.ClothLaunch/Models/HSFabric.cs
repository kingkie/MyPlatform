using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yu3zx.DapperExtend;

namespace Yu3zx.ClothLaunch.Models
{
    /// <summary>
    /// 环思生产单子
    /// </summary>
    [Table("qmInspectHdr")]
    public class HSFabric: BaseEntityOne
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [Column("sCardNo")]
        public string SCardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 批号
        /// </summary>
        [Column("sMaterialLot")]
        public string SMaterialLot
        {
            get;
            set;
        }
        /// <summary>
        /// 布号
        /// </summary>
        [Column("sFabricNo")]
        public string SFabricNo
        {
            get;
            set;
        }
        /// <summary>
        /// 品名
        /// </summary>
        [Column(" sMaterialName")]
        public string SMaterialName
        {
            get;
            set;
        }
        /// <summary>
        /// 规格
        /// </summary>
        [Column("sYarnInfo")]
        public string SYarnInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 卷号
        /// </summary>
        [Column("iManualOrderNo")]
        public int IManualOrderNo
        {
            get;
            set;
        }
        /// <summary>
        /// 长度
        /// </summary>
        [Column("nLength")]
        public decimal NLength
        {
            get;
            set;
        }
        /// <summary>
        /// 幅宽
        /// </summary>
        [Column("sProductWidthOrder")]
        public string SProductWidthOrder
        {
            get;
            set;
        }
        /// <summary>
        /// 色号
        /// </summary>
        [Column("sColorNo")]
        public string SColorNo
        {
            get;
            set;
        }
        /// <summary>
        /// 色名
        /// </summary>
        [Column("sColorName")]
        public string SColorName
        {
            get;
            set;
        }
        /// <summary>
        /// 线号
        /// </summary>
        [Column("sEquipmentNo")]
        public string SEquipmentNo
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("sFabricWidth")]
        public string SFabricWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 产品等级
        /// </summary>
        [Column("sGrade")]
        public string SGrade
        {
            get;
            set;
        }
        /// <summary>
        /// 疵点信息
        /// </summary>
        [Column("sRemark")]
        public string SRemark
        {
            get;
            set;
        }
        /// <summary>
        /// 是否打包
        /// </summary>
        [Column("bEnd")]
        public bool Bend
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("tInspectTime")]
        public DateTime TInspectTime
        {
            get;
            set;
        }
    }
}

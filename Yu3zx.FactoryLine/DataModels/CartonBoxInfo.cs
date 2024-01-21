﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu3zx.DapperExtend;

namespace Yu3zx.FactoryLine.DataModels
{
    [Table("cartonboxinfo")]
    public class CartonBoxInfo : BaseEntity
    {
        [Key]
        [Column("Id")]
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
        /// 箱号
        /// </summary>
        [Column("BoxNum")]
        public string BoxNum
        {
            get;
            set;
        }
        /// <summary>
        /// 总长度
        /// </summary>
        [Column("SumNum")]
        public string SumNum
        {
            get;
            set;
        }
        /// <summary>
        /// 随机数
        /// </summary>
        [Column("RndStrings")]
        public string RndStrings
        {
            get;
            set;
        }

        /// <summary>
        /// 卷号
        /// </summary>
        [Column("ReelNums")]
        public string ReelNums
        {
            get;
            set;
        }

        [Column("ProdLens")]
        public string ProdLens
        {
            get;
            set;
        }
    }

    public class CartonBox
    {
        private int indexLauch = 0;
        public string BatchNo
        {
            get;
            set;
        }

        public int LaunchIndex
        {
            get
            {
                return indexLauch;
            }
            set
            {
                indexLauch = value;
            }
        }
        /// <summary>
        /// 箱号
        /// </summary>
        public string BoxNum
        {
            get;
            set;
        }

        /// <summary>
        /// 上线一箱列表
        /// </summary>
        public List<FabricClothItem> OnLaunchItems = new List<FabricClothItem>();
    }
}

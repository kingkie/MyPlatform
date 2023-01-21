using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.DapperExtend;

namespace Yu3zx.PackingMonitor.Models
{
    /// <summary>
    /// 监控流水
    /// </summary>
    [Table("monitorrecordflow")]
    public class MonitorRecord : BaseEntity
    {
        /// <summary>
        /// 二维码内容
        /// </summary>
        [JsonProperty("qrcontent")]
        [Column("qrcontent")]
        public string QrContent
        {
            get;
            set;
        }

        /// <summary>
        /// 二维码图片
        /// </summary>
        [JsonProperty("qrimgfile")]
        [Column("qrimgfile")]
        public string QrImgFile
        {
            get;
            set;
        }

        /// <summary>
        /// 对应监控文件
        /// </summary>
        [JsonProperty("monitorimgfiles")]
        [Column("monitorimgfiles")]
        public string MonitorImgFiles
        {
            get;
            set;
        }

        /// <summary>
        /// 监控时间
        /// </summary>
        [Column("monitortime")]
        public DateTime MonitorTime
        {
            get;
            set;
        }
    }
}

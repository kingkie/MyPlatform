using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class PlcCmd
    {
        /// <summary>
        /// 
        /// </summary>
        public int CmdCode
        {
            get;
            set;
        }

        public int MachineId
        {
            get;
            set;
        }
        /// <summary>
        /// 数据段
        /// </summary>
        public List<byte> DataSegment = new List<byte>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Devices.Interfaces
{
    /// <summary>
    /// 总线接口
    /// </summary>
    public interface IBus : IConnector
    {
        void Init();

        bool Write(byte[] buf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf">读取缓冲区</param>
        /// <param name="count">读取个数</param>
        /// <param name="bytesread">实际读取个数</param>
        /// <returns></returns>
        bool Read(byte[] buf, int count, ref int bytesread);
    }
}

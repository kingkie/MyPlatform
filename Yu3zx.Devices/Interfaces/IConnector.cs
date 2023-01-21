using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Devices.Interfaces
{
    /// <summary>
    /// 连接接口
    /// </summary>
    public interface IConnector
    {
        bool Connected { get; }

        bool Open();

        bool Close();
    }
}

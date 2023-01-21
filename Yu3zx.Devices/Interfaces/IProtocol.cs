using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Devices.Interfaces
{
    /// <summary>
    /// 通信协议接口
    /// </summary>
    public interface IProtocol
    {
        void Parser();

        void Receiver();

        void Sender();
    }
}

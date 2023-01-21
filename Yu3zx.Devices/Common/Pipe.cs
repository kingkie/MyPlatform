using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Devices.Interfaces;
using Newtonsoft.Json;

namespace Yu3zx.Devices.Common
{
    /// <summary>
    /// 增加Pipe是为了解决设备组网问题
    /// </summary>
    public class Pipe
    {
        private string _pipeid = "pipe" + DateTime.Now.ToString("MMddHHmmfff");
        private string _pipename = "通道";
        private IBus _CommBus;

        /// <summary>
        /// 设备ID，可组网，需要使用相同的BUS
        /// </summary>
        public List<string> DevicesId = new List<string>();

        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        /// <summary>
        /// 通信接口
        /// </summary>
        public IBus BusConnector
        {
            get { return _CommBus; }
            set { _CommBus = value; }
        }
        /// <summary>
        /// 通道ID
        /// </summary>
        public string PipeId
        {
            get { return _pipeid; }
            set { _pipeid = value; }
        }

        public string PipeName
        {
            get { return _pipename; }
            set { _pipename = value; }
        }

        public void AddDevice(string devid)
        {
            if (!DevicesId.Contains(devid))
            {
                DevicesId.Add(devid);
            }
        }

        public void ReMoveDevice(string devid)
        {
            DevicesId.Remove(devid);
        }

        [JsonIgnore]
        /// <summary>
        /// 设备通信连接状态
        /// </summary>
        public bool Connected
        {
            get
            {
                if (_CommBus != null)
                {
                    return false;
                }
                else
                {
                    return _CommBus.Connected;
                }
            }
        }
        /// <summary>
        /// 打开通道
        /// </summary>
        /// <returns></returns>
        public bool PipeOpen()
        {
            return _CommBus.Open();
        }
        /// <summary>
        /// 关闭通道
        /// </summary>
        /// <returns></returns>
        public bool PipeClose()
        {
            return _CommBus.Close();
        }

        public void PipeRead(byte[] buf, int count, ref int bytesread)
        {
            _CommBus.Read(buf, count, ref bytesread);
        }

        public void PipeWrite(byte[] buff)
        {
            _CommBus.Write(buff);
        }

    }
}

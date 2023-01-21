using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Yu3zx.Devices.Interfaces;
using Newtonsoft.Json;
using Yu3zx.Json;
using Yu3zx.Devices.Common;

namespace Yu3zx.Devices
{
    /// <summary>
    /// 设备基础模块
    /// </summary>
    public class Device : IComparer
    {
        #region 私有变量
        private string _deviceid;
        private string _devicename;
        private string _deviceaddr = "01";
        private string _friendlyname;
        private string _devdescription;
        private string _pipeId;
        private int _inChlCount;
        private int _outChlCount;

        private IProtocol _Protocol;
        private IProcessor _Processor;//数据处理器


        public List<Channel> InChanels = new List<Channel>();

        public List<Channel> OutChanels = new List<Channel>();
        #endregion End

        #region 设备属性
        /// <summary>
        /// 设备编号-唯一性
        /// </summary>
        public string DeviceId
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            set { _devicename = value; }
            get { return _devicename; }
        }

        /// <summary>
        /// 设备通信地址
        /// </summary>
        public string DeviceAddr
        {
            set { _deviceaddr = value; }
            get { return _deviceaddr; }
        }

        /// <summary>
        /// 设备种类名称
        /// </summary>
        public string FriendlyName
        {
            set { _friendlyname = value; }
            get { return _friendlyname; }
        }
        /// <summary>
        /// 设备描述
        /// </summary>
        public string DevDescription
        {
            set { _devdescription = value; }
            get { return _devdescription; }
        }
        /// <summary>
        /// 通道ID
        /// </summary>
        public string PipeId
        {
            get { return _pipeId; }
            set { _pipeId = value; }
        }

        /// <summary>
        /// 输入端数量
        /// </summary>
        public int InChannelCount
        {
            get { return _inChlCount; }
            set { _inChlCount = value; }
        }
        /// <summary>
        /// 输出端数量
        /// </summary>
        public int OutChannelCount
        {
            get { return _outChlCount; }
            set { _outChlCount = value; }
        }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        /// <summary>
        /// 通信协议-如果没有默认是无帧头帧尾简单协议
        /// </summary>
        public IProtocol BusProtocol
        {
            get { return _Protocol; }
            set { _Protocol = value; }
        }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        /// <summary>
        /// 数据处理器-如果没有不做处理
        /// </summary>
        public IProcessor DataProcessor
        {
            get { return _Processor; }
            set { _Processor = value; }
        }

        #endregion End

        #region 通信控制

        public virtual bool DevOpen()
        {
            return PipesManager.GetInstance().OpenPipe(this.PipeId);
        }

        public virtual bool DevClose()
        {
            return PipesManager.GetInstance().ClosePipe(this.PipeId);
        }

        public virtual void DevRead(byte[] buf, int count, ref int bytesread)
        {
            Pipe pipe = PipesManager.GetInstance().GetPipeById(_pipeId);
            if (pipe != null)
            {
                pipe.PipeRead(buf, count,ref bytesread);
            }
        }

        public virtual void DevWrite(byte[] buffers)
        {
            Pipe pipe = PipesManager.GetInstance().GetPipeById(_pipeId);
            if (pipe != null)
            {
                pipe.PipeWrite(buffers);
            }
        }

        #endregion End

        #region 设备控制

        /// <summary>
        /// 设备初始化
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// 发送指令到设备
        /// </summary>
        /// <param name="send"></param>
        public virtual bool SendCmd(byte[] sendbytes)
        {
            return true;
        }
        /// <summary>
        /// 获取设备数据
        /// </summary>
        public virtual void GetDeviceData()
        {

        }

        #endregion End

        /// <summary>
        /// 重建通道
        /// </summary>
        public void ReBuildChannel()
        {
            this.InChanels.Clear();
            this.OutChanels.Clear();

            for (int i = 0; i < InChannelCount; i++)
            {
                Channel chl = new Channel();
                chl.ChannelId = "InChanel_" + (i+1).ToString();
                chl.ChannelName = "输入通道" + (i + 1).ToString();
                this.InChanels.Add(chl);
            }

            for (int i = 0; i < OutChannelCount; i++)
            {
                Channel chl = new Channel();
                chl.ChannelId = "OutChanel_" + (i + 1).ToString();
                chl.ChannelName = "输出通道" + (i + 1).ToString();
                this.OutChanels.Add(chl);
            }
        }

        #region 实现IComparer接口，按设备ID排序
        public int Compare(object x, object y)
        {
            if ((x is Device) && (y is Device))
            {
                Device a = (Device)x;
                Device b = (Device)y;

                return a._deviceid.CompareTo(b._deviceid);
            }
            return 0;
        }
        #endregion End

    }
}

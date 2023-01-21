using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Devices.CommonDevices
{
    public class GatherDevice : Device
    {
        ModbusIpMaster modbusmaster;
        public GatherDevice()
        {
            if (string.IsNullOrEmpty(this.FriendlyName))
            {
                this.FriendlyName = "采集数据PLC(TCP模式)";
            }
            if (string.IsNullOrEmpty(this.DevDescription))
            {
                this.DevDescription = "采集数据PLC-用于Modbus TCP模式";
            }
            if (string.IsNullOrEmpty(this.DeviceName))
            {
                this.DeviceName = "GatherDevice";
            }
            if (string.IsNullOrEmpty(this.DeviceId))
            {
                this.DeviceId = "Dev_" + DateTime.Now.ToString("ddHHmmfff");
            }

        }

        #region 重写
        public override void Init()
        {
            //this.FriendlyName = "";
        }

        public override bool DevClose()
        {
            return base.DevClose();
        }

        public override bool DevOpen()
        {
            return base.DevOpen();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf">读取缓冲区</param>
        /// <param name="count">读取个数</param>
        /// <param name="bytesread">返回实际读取个数</param>
        public override void DevRead(byte[] buf, int count, ref int bytesread)
        {
            base.DevRead(buf, count, ref bytesread);
        }

        public override void DevWrite(byte[] bRites)
        {
            base.DevWrite(bRites);
        }

        public override void GetDeviceData()
        {
            base.GetDeviceData();
        }

        public override bool SendCmd(byte[] sendbytes)
        {
            return base.SendCmd(sendbytes);
        }

        #endregion End
    }
}

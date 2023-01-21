using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Devices.Common;

namespace Yu3zx.Devices.CommonDevices
{
    public class SiemensPLC:Device
    {

        public SiemensPLC()
        {
            if (string.IsNullOrEmpty(this.FriendlyName))
            {
                this.FriendlyName = "西门子S7200PLC";
            }
            if (string.IsNullOrEmpty(this.DevDescription))
            {
                this.DevDescription = "西门子7200PLC，小型可编程序控制器";
            }
            if (string.IsNullOrEmpty(this.DeviceName))
            {
                this.DeviceName = "西门子S7200PLC";
            }
            if (string.IsNullOrEmpty(this.DeviceId))
            {
                this.DeviceId = "s7200lplc_" + DateTime.Now.ToString("ddHHmmfff");
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

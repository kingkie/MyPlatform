using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Yu3zx.Logs;
using Yu3zx.Util;

namespace Yu3zx.Acquisition
{
    /// <summary>
    /// 生产线专用采集PLC
    /// </summary>
    public class AcqPlc : IComparable
    {
        private string deviceid = string.Empty;
        private string devicename = string.Empty;
        private string ipAddr = string.Empty;
        private int siteorder = 0;

        private int ipPort = 0;
        private byte macAddr = 0x01;
        private TcpClient tcpClient;
        private Thread thRead;

        public event DataReceivedHandler DataReceived;

        public event DataReceivedHandler DataRead;

        public event DataDealHandler DataDeal;
        /// <summary>
        /// 数据接收缓存区
        /// </summary>
        private byte[] buffer = new byte[2048];

        private bool HaveInit = false;

        private string memoryData = string.Empty; //临时读取的数据

        private bool CanRead = false; //默认不能读取

        private bool IsDelete = false; //是否增加还是删除,false为增加，true为删除

        private bool IsComplete = false; // 是否完成

        /// <summary>
        /// 构造函数
        /// </summary>
        public AcqPlc()
        {
            DataRead += DealData;
        }

        #region 设备属性
        /// <summary>
        /// 设备编号-唯一性
        /// </summary>
        public string DeviceId
        {
            set { deviceid = value; }
            get { return deviceid; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            set { devicename = value; }
            get { return devicename; }
        }
        /// <summary>
        /// PLC站位号
        /// </summary>
        public byte Addr
        {
            get { return macAddr; }
            set { macAddr = value; }
        }

        /// <summary>
        /// 工位序号
        /// </summary>
        public int SiteOrder
        {
            set { siteorder = value; }
            get { return siteorder; }
        }

        /// <summary>
        ///服务端IP地址
        /// </summary>
        public string IPAddr
        {
            get { return ipAddr; }
            set { ipAddr = value; }
        }
        /// <summary>
        /// 服务端端口
        /// </summary>
        public int IpPort
        {
            get { return ipPort; }
            set { ipPort = value; }
        }

        [JsonIgnore]
        public int ReadInterval
        {
            get;
            set;
        } = 400;

        private ushort coilsAddr = 3122;
        /// <summary>
        /// 线圈地址
        /// </summary>
        public ushort CoilsAddr
        {
            get
            {
                return coilsAddr;
            }
            set
            {
                coilsAddr = value;
            }
        }
        private ushort regsAddr = 912;
        /// <summary>
        /// 寄存器地址
        /// </summary>
        public ushort RegsAddr
        {
            get
            {
                return regsAddr;
            }
            set
            {
                regsAddr = value;
            }
        }

        [JsonIgnore]
        public bool Connected
        {
            get;
            set;
        }

        #endregion End

        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <returns></returns>
        public Result Init()
        {
            HaveInit = false;
            if(string.IsNullOrEmpty( IPAddr))
            {
                return new Result(false, "服务端IP地址未配置");
            }
            IPAddress ipSer = null;
            if(!IPAddress.TryParse(IPAddr,out ipSer))
            {
                return new Result(false, "服务端IP地址配置错误:" + IPAddr);
            }
            if(IpPort == 0)
            {
                return new Result(false, "服务端IP端口未配置");
            }

            //if(tcpClient == null)
            //{
            //    tcpClient = new TcpClient();
            //} //0102
            HaveInit = true;
            return new Result(true);
        }
        /// <summary>
        /// 卸载设备
        /// </summary>
        /// <returns></returns>
        public Result UnInit()
        {
            try
            {
                if(thRead != null)
                {
                    thRead.Abort();
                    thRead = null;
                }
                DisConnect();
            }
            catch(Exception ex)
            {
                Log.Instance.LogWrite(ex.Message, MsgLevel.Err);
            }
            return new Result(true);
        }
        /// <summary>
        /// 检测连接--拔网络无用
        /// </summary>
        /// <returns></returns>
        public bool CheckLink()
        {
            if (tcpClient == null || tcpClient.Client == null)
            {
                return false;
            }
            bool blockingState = tcpClient.Client.Blocking;
            try
            {
                byte[] tmp = new byte[1];
                tcpClient.Client.Blocking = false;
                tcpClient.Client.Send(tmp, 0, 0);
                Connected = true; //若Send错误会跳去执行catch体，而不会执行其try体里其之后的代码  
            }
            catch (SocketException e)
            {
                //10035 == WSAEWOULDBLOCK  
                if (e.NativeErrorCode.Equals(10035))
                {
                    //Still Connected, but the Send would block;
                    Connected = true;
                }
                //else if (e.NativeErrorCode.Equals(10056))
                //{
                //    try
                //    {
                //        tcpClient.Close();
                //    }
                //    catch
                //    {
                //    }
                //    Connected = false;
                //}
                else
                {
                    //Disconnected: error code {0}!", e.NativeErrorCode
                    Connected = false;
                }
            }
            catch(Exception ex)
            {
                Log.Instance.LogWrite("检测异常:" + ex.Message, MsgLevel.Err);
                Connected = false;
            }
            finally
            {
                tcpClient.Client.Blocking = blockingState;
            }
            return Connected;
        }
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public Result OpenDevice()
        {
            if(!HaveInit)
            {
                var rInit = Init();
                if(!rInit.IsSuccess)
                {
                    return new Result(false, rInit.Message);
                }
            }
            DisConnect();
            tcpClient = new TcpClient();
            try
            {
                int tryLink = 3;
                while (tryLink > 0)
                {
                    IAsyncResult asyncResult = tcpClient.BeginConnect(IPAddr, IpPort, null, null);
                    asyncResult.AsyncWaitHandle.WaitOne(3000, true); //wait for 3 sec
                    if (!asyncResult.IsCompleted)
                    {
                        try
                        {
                            tcpClient.Close(); //Cannot connect to server.
                        }
                        catch
                        {
                        }
                    }
                    if (tcpClient.Connected)
                    {
                        this.Connected = true;
                        break;
                    }
                    else
                    {
                        tryLink--;
                    }
                }
                if (!tcpClient.Connected)
                {
                    this.Connected = false;
                    return new Result(false, "客户端未连接到服务端！");
                }
            }
            catch(Exception ex)
            {
                return ReOpen();
            }
            try
            {
                this.Connected = tcpClient.Connected;
                if(this.Connected)
                {
                    tcpClient.GetStream().BeginRead(buffer, 0, buffer.Length, new AsyncCallback(TcpCallBack), tcpClient);
                    if(thRead != null)
                    {
                        thRead.Abort();
                        thRead = null;
                    }
                    thRead = new Thread(TimerRead);
                    thRead.IsBackground = true;
                    thRead.Name = "read data";
                    thRead.Start();
                }
                return new Result(this.Connected);
            }
            catch(Exception ex)
            {
                DisConnect();
                return new Result(false,  DeviceId + "打开异常:" + ex.Message);
            }
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public Result CloseDevice()
        {
            try
            {
                DisConnect();
                return new Result(true);
            }
            catch(Exception ex)
            {
                return new Result(false,"设备关闭异常:" + ex.Message);
            }
        }

        public Result ReOpen()
        {
            DisConnect();
            tcpClient = new TcpClient();
            int tryLink = 3;
            while (tryLink > 0)
            {
                IAsyncResult asyncResult = tcpClient.BeginConnect(IPAddr, IpPort, null, null);
                asyncResult.AsyncWaitHandle.WaitOne(3000, true); //wait for 3 sec
                if (!asyncResult.IsCompleted)
                {
                    try
                    {
                        tcpClient.Close(); //Cannot connect to server.
                    }
                    catch
                    {
                    }
                }
                if (tcpClient.Connected)
                {
                    this.Connected = true;
                    break;
                }
                else
                {
                    tryLink--;
                }
            }
            if (!tcpClient.Connected)
            {
                this.Connected = false;
                return new Result(false, "客户端未连接到服务端！");
            }
            else
            {
                tcpClient.GetStream().BeginRead(buffer, 0, buffer.Length, new AsyncCallback(TcpCallBack), tcpClient);
                this.Connected = true;
                return new Result(true);
            }
            //try
            //{
            //    this.Connected = tcpClient.Connected;
            //    if (this.Connected)
            //    {
            //        tcpClient.GetStream().BeginRead(buffer, 0, buffer.Length, new AsyncCallback(TcpCallBack), tcpClient);
            //        if (thRead != null)
            //        {
            //            thRead.Abort();
            //            thRead = null;
            //        }
            //        thRead = new Thread(TimerRead);
            //        thRead.IsBackground = true;
            //        thRead.Name = "readdata";
            //        thRead.Start();
            //    }
            //    return new Result(this.Connected);
            //}
            //catch (Exception ex)
            //{
            //    DisConnect();
            //    return new Result(false, "重新打开异常:" + ex.Message);
            //}
        }

        /// <summary>
        /// 获取条码数据
        /// </summary>
        /// <returns></returns>
        public bool ReadQrCode()
        {
            try
            {
                byte[] bRegs = BitConverter.GetBytes(RegsAddr);
                byte[] bCmd = new byte[] {0x00, 0x03, 0x00, 0x00, 0x00, 0x06, macAddr, 0x03, bRegs[1], bRegs[0], 0x00, 0x22};
                bool bSend = SendData(bCmd);
                return bSend;
            }
            catch (SocketException se)
            {
                return false;
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("ReadQrCode异常:" + ex.Message, MsgLevel.Err);
                return false;
            }
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <returns></returns>
        public bool SetCoilStatus(ushort addr, bool bStatus)
        {
            try
            {
                byte[] bCoils = BitConverter.GetBytes(addr);
                byte bCoil = 0x00;
                if(bStatus)
                {
                    bCoil = 0xFF;
                }
                else
                {
                    bCoil = 0x00;
                }
                byte[] bCmd = new byte[] { 0x00, 0x05, 0x00, 0x00, 0x00, 0x06, macAddr, 0x05, bCoils[1], bCoils[0], bCoil, 0x00 };
                bool bSend = SendData(bCmd);
                return bSend;
            }
            catch (SocketException se)
            {
                ReOpen();
                return false;
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("SetCoilStatus异常:" + ex.Message, MsgLevel.Err);
                return false;
            }
        }

        private void TcpCallBack(IAsyncResult iAr)
        {
            TcpClient client = (TcpClient)iAr.AsyncState;
            if(client.Connected)
            {
                NetworkStream ns = client.GetStream();
                try
                {
                    byte[] recdata = new byte[ns.EndRead(iAr)];
                    Array.Copy(buffer, recdata, recdata.Length);
                    if (recdata.Length > 0)
                    {
                        if (DataReceived != null)
                        {
                            DataReceived.BeginInvoke(DeviceId, recdata, null, null);//异步输出数据
                        }

                        if (DataRead != null)
                        {
                            if (recdata.Length > 6)
                            {
                                if(recdata[0] == 0x00 && recdata[1] == 0x01)//序号00 01作为开关量指令
                                {
                                    if(recdata.Length > 8)
                                    {
                                        CanRead = (recdata[9] & 0x01) >= 0x01? true : false;
                                        IsDelete = (recdata[9] & 0x02) >= 0x01 ? true : false;
                                        IsComplete = (recdata[9] & 0x04) >= 0x01 ? true : false;

                                        //if(DeviceId == "dev2")
                                        //{
                                        //    string str = BytesToStr(recdata);
                                        //    Log.Instance.LogWrite("CanRead And IsDelete:" + str,MsgLevel.Comm);
                                        //    Log.Instance.LogWrite("CanRead:" + CanRead.ToString() + "IsDelete:" + IsDelete.ToString(),MsgLevel.Comm);
                                        //}
                                    }
                                    else
                                    {
                                        CanRead = false;
                                        IsDelete = false;
                                        IsComplete = false;
                                    }
                                }
                                else if(recdata[0] == 0x00 && recdata[1] == 0x05) //写线圈返回
                                {

                                }
                                else if(recdata[0] == 0x00 && recdata[1] == 0x03) //读寄存返回
                                {
                                    byte[] byteBack = new byte[recdata.Length - 6];
                                    Array.Copy(buffer, 6, byteBack, 0, byteBack.Length);
                                    DataRead.BeginInvoke(DeviceId, byteBack, null, null);
                                }
                            }
                        }
                        ns.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(TcpCallBack), client);
                    }
                    else
                    {
                        //if(client.Connected)
                        //{
                        //    if(client.Client != null)
                        //    {

                        //    }
                        //}
                        DisConnect();
                        return;
                    }
                }
                catch
                {
                    DisConnect();
                    return;
                }
            }
        }

        public bool SendData(byte[] data)
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected)
                {
                    NetworkStream ns = tcpClient.GetStream();
                    ns.Write(data, 0, data.Length);
                    this.Connected = true;
                    return true;
                }
                else
                {
                    var rtnSendOpen = ReOpen();
                    if(rtnSendOpen.IsSuccess)
                    {
                        this.Connected = true;
                        return true;
                    }
                    this.Connected = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.LogWrite("发送数据异常：" + ex.Message, MsgLevel.Debug);
                var rtnSendOpen = ReOpen();
                if (rtnSendOpen.IsSuccess)
                {
                    this.Connected = true;
                    return true;
                }
                this.Connected = false;
                return false;
            }
        }

        private void DealData(object sender, byte[] data)
        {
            memoryData = string.Empty;
            if (data != null && data.Length > 0)
            {
                if(data.Length > 3)
                {
                    byte[] bModData = new byte[data.Length - 3];
                    Array.Copy(data, 3, bModData, 0, bModData.Length);
                    for(int i = 0; i < bModData.Length / 2; i++)
                    {
                        byte tmp = bModData[i * 2];
                        bModData[i * 2] = bModData[i * 2 + 1];
                        bModData[i * 2 + 1] = tmp;
                    }
                    memoryData = System.Text.ASCIIEncoding.ASCII.GetString(bModData).Trim('\0').Trim();
                    if(DataDeal != null)
                    {
                        //IsDelete = false;
                        IsComplete = false;
                        if(IsComplete)
                        {
                            IsComplete = false;
                            DataDeal.BeginInvoke(DeviceId, "Complete", memoryData, null, null);
                        }
                        else
                        {
                            if(IsDelete)
                            {
                                IsDelete = false;
                                DataDeal.BeginInvoke(DeviceId, "Del", memoryData, null, null);
                            }
                            else
                            {
                                if(memoryData.Length > 0)
                                {
                                    DataDeal.BeginInvoke(DeviceId, "Add", memoryData, null, null);
                                }
                            }
                        }
                        Log.Instance.LogWrite(this.DeviceId + "当前状态:IsDelete-" + IsDelete + "; IsComplete-" + IsComplete + ",数据:" + memoryData,MsgLevel.Debug);
                    }
                }
                else
                {
                }
                //Log.Instance.LogWrite("接收到" + sender.ToString() + "的数据:" + BytesToStr(data), MsgLevel.Debug);
            }
        }


        /// <summary>
        /// 读取数据线程
        /// </summary>
        private void TimerRead()
        {
            while(true)
            {
                try
                {
                    byte[] bCoils = BitConverter.GetBytes(CoilsAddr);
                    //byte[] bCmdFlag = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, macAddr, 0x01, bCoils[1], bCoils[0], 0x00, 0x04 };
                    byte[] bCmdFlag = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, macAddr, 0x01, bCoils[1], bCoils[0], 0x00, 0x04 };
                    string str = BytesToStr(bCmdFlag);
                    bool bSendFlag = SendData(bCmdFlag);
                    if(bSendFlag)
                    {
                        Thread.Sleep(100);
                        if(CanRead || IsDelete)
                        {
                            ReadQrCode();
                            if (ReadInterval > 200)
                            {
                                Thread.Sleep(ReadInterval);
                            }
                            else
                            {
                                Thread.Sleep(250); //400
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Log.Instance.LogWrite(DeviceId + "读取数据异常:" + ex.Message,MsgLevel.Err);
                }
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            try
            {
                if (tcpClient != null && tcpClient.Client != null) // && tcpClient.Connected
                {
                    tcpClient.Client.Disconnect(true);
                    tcpClient.Close();
                    tcpClient = null;
                }
                Connected = false;
            }
            catch (Exception ex)
            {
                Connected = false;
            }
        }

        private string BytesToStr(byte[] bInput)
        {
            if (bInput == null)
            {
                return string.Empty;
            }
            List<byte> lTmp = new List<byte>();
            lTmp.AddRange(bInput);
            return string.Join(" ", lTmp.ConvertAll(b => b.ToString("X2")));
        }

        public int CompareTo(object obj)
        {
            return this.SiteOrder.CompareTo((obj as AcqPlc).SiteOrder);
        }
    }

    /// <summary>
    /// 数据接收事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="data"></param>
    public delegate void DataReceivedHandler(object sender, byte[] data);

    /// <summary>
    /// 发送数据事件
    /// </summary>
    /// <param name="data"></param>
    public delegate bool DataSendHandler(byte[] data);
    /// <summary>
    /// 数据处理过程
    /// </summary>
    /// <param name="strData"></param>
    /// <returns></returns>
    public delegate void DataDealHandler(object sender,string cmdType, string strData);

}

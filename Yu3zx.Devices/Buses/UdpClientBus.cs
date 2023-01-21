using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Devices.Interfaces;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace Yu3zx.Devices.Buses
{
    public class UdpClientBus : IBus
    {
        private string _busid = "udpc" + DateTime.Now.ToString("MMddHHmmfff");
        private string _busname = "UdpClient总线";
        private string _hostName;
        private int _hostPort;
        private int _localPort = -1;
        private UdpClient udpClient;
        private IPEndPoint remoteEP;

        protected bool connected;

        /// <summary>
        /// 设备ID
        /// </summary>
        public string BusId
        {
            set { _busid = value; }
            get { return _busid; }
        }
        /// <summary>
        /// 总线名称
        /// </summary>
        public string BusName
        {
            set { _busname = value; }
            get { return _busname; }
        }

        /// <summary>
        /// 种类名称
        /// </summary>
        public string FriendlyName
        {
            get
            {
                return "Udp客户端连接";
            }
        }

        /// <summary>
        /// 服务端IP
        /// </summary>
        public string ServerIP
        {
            get { return _hostName; }
            set { _hostName = value; }
        }
        /// <summary>
        /// 服务端端口
        /// </summary>
        public int ServerPort
        {
            get { return _hostPort; }
            set { _hostPort = value; }
        }
        /// <summary>
        /// 接收端口
        /// </summary>
        public int LocalPort
        {
            get { return _localPort; }
            set { _localPort = value; }
        }
        
        #region BUS接口实现
        public void Init()
        {

        }

        public bool Write(byte[] buf)
        {
            try
            {
                udpClient.Send(buf, buf.Length, remoteEP);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Read(byte[] buf, int count, ref int bytesread)
        {
            try
            {
                if (!Connected)
                    return false;
                byte[] bytes = udpClient.Receive(ref remoteEP);
                if (bytes.Length > buf.Length)
                {
                    Buffer.BlockCopy(bytes, 0, buf, 0, buf.Length);
                    bytesread = buf.Length;
                }
                else
                {
                    Buffer.BlockCopy(bytes, 0, buf, 0, bytes.Length);
                    bytesread = bytes.Length;
                }
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        [JsonIgnore]
        public bool Connected
        {
            get
            {
                if (udpClient != null)
                {
                    return udpClient.Client.Connected && connected;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Open()
        {
            try
            {
                remoteEP = new IPEndPoint(IPAddress.Parse(_hostName), _hostPort);
                if (_localPort == -1)
                    udpClient = new UdpClient();
                else
                    udpClient = new UdpClient(_localPort);

                connected = true;
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                if (udpClient != null)
                {
                    udpClient.Close();
                    udpClient = null;
                }
                connected = false;
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion End
    }
}

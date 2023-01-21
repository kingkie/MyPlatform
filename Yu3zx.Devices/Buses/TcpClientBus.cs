using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Devices.Interfaces;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Yu3zx.Devices.Buses
{
    /// <summary>
    /// 服务端断开连接时无法判断，
    /// </summary>
    public class TcpClientBus:IBus
    {
        private string _busid = "tcpcbus" + DateTime.Now.ToString("MMddHHmmfff");
        private string _busname = "TcpClient总线";
        private string _hostName;
        private int _hostPort;
        private TcpClient clientSocket;
        private NetworkStream netStream;
        private int readErrCount = 0;

        public TcpClientBus()
        {

        }
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
                return "Tcp连接";
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

        #region BUS接口实现
        public void Init()
        {
 
        }

        public bool Write(byte[] buf)
        {
            try
            {
                if (netStream != null && clientSocket.Client.Connected)
                {
                    netStream.Write(buf, 0, buf.Length);
                    netStream.Flush();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// KeepAlive的使用，如果5秒后无法连通，自动置断开连接
        /// </summary>
        public void LinkAble()
        {
            if (clientSocket != null)
            {
                uint dummy = 0;
                byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
                clientSocket.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
            }
        }

        public bool Read(byte[] buf, int count, ref int bytesread)
        {
            try
            {
                if (netStream == null && clientSocket.Client.Connected)
                {
                    return false;
                }
                bytesread = netStream.Read(buf, 0, buf.Length);
                if (bytesread <= 0)
                {
                    if (++readErrCount > 10)
                    {
                        throw new IOException();
                    }
                }
                else
                {
                    readErrCount = 0;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        [JsonIgnore]
        public bool Connected
        {
            get 
            {
                if (clientSocket != null)
                {
                    return clientSocket.Connected;
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
                clientSocket = new TcpClient(_hostName, _hostPort);
                netStream = clientSocket.GetStream();
                LinkAble();
            }
            catch (SocketException)
            {
                return false;
            }
            return true;
        }

        public bool Close()
        {
            try
            {
                if (netStream != null)
                {
                    netStream.Close();
                    netStream = null;
                }
                if (clientSocket != null)
                {
                    clientSocket.Client.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    clientSocket = null;
                }
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

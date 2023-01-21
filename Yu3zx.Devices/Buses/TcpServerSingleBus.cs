using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Devices.Interfaces;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace Yu3zx.Devices.Buses
{
    public class TcpServerSingleBus:IBus
    {
        private string _busid = "tcpssbus" + DateTime.Now.ToString("MMddHHmmfff");
        private string _busname = "Tcp单服务端";
        private string _hostName;
        private int _hostPort;
        private TcpListener listener;
        private NetworkStream netStream;
        private TcpClient clientSocket;
        private int readErrCount = 0;
        private AutoResetEvent acceptEvent;

        private bool connected = false;

        public TcpServerSingleBus()
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
                return "Tcp服务端(单客户端)";
            }
        }

        /// <summary>
        /// 服务IP(一个本地IP)
        /// </summary>
        public string ServerIP
        {
            get { return _hostName; }
            set { _hostName = value; }
        }
        /// <summary>
        /// 服务端口(对外监听端口)
        /// </summary>
        public int ServerPort
        {
            get { return _hostPort; }
            set { _hostPort = value; }
        }

        private void StartListening()
        {
            connected = true;
            IPAddress hostip;
            if (!IPAddress.TryParse(_hostName, out hostip))
            {
                connected = false;
                return;
            }
            listener = new TcpListener(hostip, _hostPort);
            bool listening = false;
            while (connected)
            {
                try
                {
                    if (!listening)
                    {
                        listener.Start();
                        listening = true;
                    }
                    clientSocket = listener.AcceptTcpClient();
                    if (this.netStream != null)
                    {
                        this.netStream.Close();
                        this.netStream = null;
                    }
                    this.netStream = clientSocket.GetStream();

                    acceptEvent.Set();
                }
                catch
                {

                }
            }
        }

        #region BUS接口实现
        public void Init()
        {

        }

        public bool Write(byte[] buf)
        {
            if (netStream != null)
            {
                this.netStream.Write(buf, 0, buf.Length);
                this.netStream.Flush();
                return true;
            }
            return false;
        }

        public bool Read(byte[] buf, int count, ref int bytesread)
        {
            if (this.netStream != null)
            {
                try
                {
                    bytesread = this.netStream.Read(buf, 0, buf.Length);
                    if (bytesread == 0)
                    {
                        Thread.Sleep(500);//客户端断开后会一直读，避免耗费CPU,增加等待时间
                    }
                    return true;
                }
                catch (System.IO.IOException ioEx)
                {
                    Thread.Sleep(500);
                }
            }
            this.acceptEvent.WaitOne();//意外断线会导致超时阻止主线程
            return false;
        }
        [JsonIgnore]
        public bool Connected
        {
            get 
            {
                if (clientSocket != null)
                    return clientSocket.Connected && connected;
                else
                    return false;
            }
        }

        public bool Open()
        {
            acceptEvent = new AutoResetEvent(false);
            (new Thread(new ThreadStart(StartListening))).Start();
            connected = true;
            return true;
        }
        /// <summary>
        /// 关闭所有连接
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            this.acceptEvent.Set();

            connected = false;

            if (listener != null)
            {
                listener.Stop();
            }
            if (netStream != null)
            {
                netStream.Close();
                netStream = null;
            }
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
            return true;
        }
        #endregion End
    }
}

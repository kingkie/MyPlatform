using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Devices.Interfaces;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Yu3zx.Devices.Buses
{
    public class TcpServerBus:IBus
    {
        private string _busid = "tcpsbus" + DateTime.Now.ToString("MMddHHmmfff");
        private string _busname = "Tcp服务端";
        private string _hostName;
        private int _hostPort;
        private TcpListener listener;
        private NetworkStream netStream;
        private int readErrCount = 0;
        private List<NetTcpClient> tcpClientList = new List<NetTcpClient>();
        private bool connected = false;

        public TcpServerBus()
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
                return "Tcp服务端";
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
                    TcpClient clientSocket = listener.AcceptTcpClient();
                    NetTcpClient ntC = new NetTcpClient(clientSocket);
                    ntC.KeyId = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString();//目前以IP作为唯一Key
                    tcpClientList.Add(ntC);
                }
                catch
                {

                }
            }
        }

        public NetTcpClient GetNetTcpClient(string _key)
        {
            //根据包找到对应的客户端
            NetTcpClient tcpClient = tcpClientList.Find(x => x.KeyId == _key);
            if (tcpClient != null)
            {
                return tcpClient;
            }
            else
            {
                return null;
            }
        }

        #region BUS接口实现
        public void Init()
        {

        }

        public bool Write(byte[] buf)
        {
            //根据包找到对应的客户端
            string _key = "";//从buf上获取地址
            NetTcpClient tcpClient = GetNetTcpClient(_key);
            if (tcpClient != null)
            {
                tcpClient.Write(buf);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Read(byte[] buf, int count, ref int bytesread)
        {
            //从公共变量列表上读取
            return true;
        }
        [JsonIgnore]
        public bool Connected
        {
            get { return connected; }
        }

        public bool Open()
        {
            (new Thread(new ThreadStart(StartListening))).Start();
            return true;
        }
        /// <summary>
        /// 关闭所有连接
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            connected = false;
            if (this.listener != null)
            {
                this.listener.Stop();
            }
            foreach (NetTcpClient client in this.tcpClientList)
            {
                client.Close();
            }
            return true;
        }
        #endregion End
    }

    /// <summary>
    /// 专用于登记与服务端连接的TCPClient
    /// </summary>
    public class NetTcpClient
    {
        private string _key;
        private TcpClient clientSocket;
        private NetworkStream netStream;
        private bool connected;
        private const int BUFFERSIZE = 10240;
        private byte[] dataBuffer = new byte[BUFFERSIZE];
        private byte[] tempBuffer = new byte[BUFFERSIZE];
        private int receivedPos = 0;
        //private AutoResetEvent readEvent;


        public TcpClient NetClient
        {
            get { return clientSocket; }
        }

        public event EventHandler DisConnected;

        /// <summary>
        /// 客户端唯一标识键
        /// </summary>
        public string KeyId
        {
            get { return this._key; }
            set { this._key = value; }
        }

        public NetTcpClient(TcpClient _clientsocket)
        {
            this.clientSocket = _clientsocket;
            this.netStream = clientSocket.GetStream();
        }

        /// <summary>
        /// 读数据线程
        /// </summary>
        private void ReadThread()
        {
            while (connected)
            {
                int readBytes = 0;
                try
                {
                    readBytes = netStream.Read(tempBuffer, 0, BUFFERSIZE);
                    if (readBytes == 0)
                    {
                        throw new IOException();
                    }
                }
                catch (IOException)
                {
                    this.Close();
                    if (this.DisConnected != null)
                    {
                        this.DisConnected(this, null);
                    }
                }
                //解析数据从中获取客户端唯一key
                byte[] data = new byte[readBytes];
                Buffer.BlockCopy(tempBuffer, 0, data, 0, readBytes);

                //将收到数据存入缓冲区
                if (receivedPos + readBytes >= BUFFERSIZE)
                {
                    receivedPos = 0;
                }
                lock (this.dataBuffer)
                {
                    Buffer.BlockCopy(tempBuffer, 0, dataBuffer, receivedPos, readBytes);
                }
                receivedPos += readBytes;
                //触发读事件
                //readWaitNode.TcpClient = this;
                //readWaitNode.Set();

            }
        }

        /// <summary>
        /// 从该客户端读取数据
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="count"></param>
        /// <param name="bytesread"></param>
        public bool Read(byte[] buf, int count, ref int bytesread)
        {
            bytesread = Math.Min(count, receivedPos);
            lock (this.dataBuffer)
            {
                Buffer.BlockCopy(dataBuffer, 0, buf, 0, bytesread);
                receivedPos -= bytesread;
                Buffer.BlockCopy(dataBuffer, bytesread, dataBuffer, 0, receivedPos);
            }
            return true;
        }
        /// <summary>
        /// 向客户端写数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool Write(byte[] buffer)
        {
            netStream.Write(buffer, 0, buffer.Length);
            netStream.Flush();
            return true;
        }
        /// <summary>
        /// 关闭客户端
        /// </summary>
        public void Close()
        {
            this.connected = false;
            if (this.netStream != null)
            {
                this.netStream.Close();
            }
            if (this.clientSocket != null)
            {
                this.clientSocket.Close();
            }
        }

    }
}

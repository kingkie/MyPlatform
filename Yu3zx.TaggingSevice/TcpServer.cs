using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class TcpServer
    {
        private IPAddress serverIp;
        private int serverPort;
        TcpListener listener;

        Dictionary<string, TCPClient> DictClient = new Dictionary<string, TCPClient>();

        Thread thMonitor = null;

        private bool connect = false;

        public event OnClothOnLine OnClothDataHandle;

        public TcpServer(IPAddress _ip, int _port)
        {
            serverPort = _port;
            serverIp = _ip;
            //tcpClientList = new List<TCPClient>(32);//先设置个默认值可以提高性能
        }
        /// <summary>
        /// 是否处于服务中
        /// </summary>
        public bool IsServer
        {
            get { return connect; }
            set { connect = value; }
        }
        /// <summary>
        /// 服务IP地址
        /// </summary>
        public IPAddress ServerIP
        {
            set { serverIp = value; }
            get { return serverIp; }
        }
        /// <summary>
        /// 服务端口号
        /// </summary>
        public int ServerPort
        {
            set { serverPort = value; }
            get { return serverPort; }
        }

        public void StartServer()
        {
            if(thMonitor != null)
            {
                thMonitor.Abort();
                thMonitor = null;
            }
            try
            {
                foreach (var ntc in DictClient.Values)
                {
                    ntc.Client.Close();
                }
                listener?.Stop();
            }
            catch
            { }
            thMonitor = new Thread(StartListening);
            thMonitor.Name = "SpectrumData";
            thMonitor.IsBackground = true;
            thMonitor.Start();
            IsServer = true;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopServer()
        {
            try
            {
                foreach (var ntc in DictClient.Values)//tcpClientList
                {
                    try
                    {
                        ntc.Client.Close();
                    }
                    catch
                    { }
                }

                if (thMonitor != null)
                {
                    thMonitor.Abort();
                    thMonitor = null;
                }
                listener.Stop();
            }
            catch (Exception exStop)
            {
                Console.WriteLine(exStop.Message);
            }
            IsServer = false;
        }

        /// <summary>
        /// 监听处理-接收设备
        /// </summary>
        private void StartListening()
        {
            listener = new TcpListener(serverIp, serverPort);
            listener.Start(); //开始侦听:调用该实例上Start()方法开启对指定端口的侦听
            connect = true;
            while (connect)
            {
                try
                {
                    TcpClient remoteClient = listener.AcceptTcpClient();//获取客户端发来的一个连接，同步方法

                    string tcpKey = remoteClient.Client.RemoteEndPoint.ToString();
                    if (DictClient.ContainsKey(tcpKey))
                    {
                        if(!DictClient[tcpKey].Client.Connected)
                        {
                            DictClient[tcpKey].Client = remoteClient;
                            //DictClient[tcpKey].OnClosedHandle += LinkTcpClient_OnClosedHandle;
                            DictClient[tcpKey].BeginReceive();
                        }
                    }
                    else
                    {
                        var linkTcpClient = new TCPClient(remoteClient);
                        linkTcpClient.ClientKey = remoteClient.Client.RemoteEndPoint.ToString();
                        linkTcpClient.OnClosedHandle += LinkTcpClient_OnClosedHandle;
                        linkTcpClient.OnClothBuildHandle += LinkTcpClient_OnClothBuildHandle;
                        DictClient.Add(tcpKey, linkTcpClient);
                        // tcpClientList.Add(linkTcpClient);
                        linkTcpClient.BeginReceive();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void LinkTcpClient_OnClothBuildHandle(FabricClothItem item)
        {
            OnClothDataHandle?.Invoke(item);
        }

        private void LinkTcpClient_OnClosedHandle(string keyid)
        {
            lock(DictClient)
            {
                if (DictClient.ContainsKey(keyid))
                {
                    try
                    {
                        DictClient[keyid]?.Stop();
                    }
                    catch(Exception ex)
                    {
                    }
                    DictClient.Remove(keyid);
                }
            }
        }
    }

    public delegate void OnClosedAction(string keyid);

    public delegate void OnClothOnLine(FabricClothItem item);
}

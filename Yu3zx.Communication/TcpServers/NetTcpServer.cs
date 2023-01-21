using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Yu3zx.Communication
{
    public class NetTcpServer:IDisposable
    {
        const int BufferSize = 8192;//缓存大小
        private IPAddress _ServerIp;
        private int _ServerPort;

        TcpListener listener;
        List<NetTcpClient> tcpClientList;//将监听到的信息保存在该集合中
        private ReadWaitNode readWaitNode = new ReadWaitNode(false);
        NetTcpClient linkTcpClient = null;

        Thread thMonitor = null;
        private bool connect = false;
        /// <summary>
        /// 目前是否服务中
        /// </summary>
        public bool IsServer
        {
            get { return connect;}
        }
        /// <summary>
        /// 服务IP地址
        /// </summary>
        public IPAddress ServerIP
        {
            set { _ServerIp = value; }
            get { return _ServerIp; }
        }
        /// <summary>
        /// 服务端口号
        /// </summary>
        public int ServerPort
        {
            set { _ServerPort = value; }
            get { return _ServerPort; }
        }

        public NetTcpServer(IPAddress _ip,int _port)
        {
            _ServerPort = _port;
            _ServerIp = _ip;
            tcpClientList = new List<NetTcpClient>(256);//先设置个默认值可以提高性能
        }

        public void StartServer()
        {
            //listener = new TcpListener(_ServerIp, _ServerPort); 
            //listener.Start(); //开始侦听:调用该实例上Start()方法开启对指定端口的侦听
            //while (true)
            //{
            //    try
            //    {
            //        TcpClient remoteClient = listener.AcceptTcpClient();//获取客户端发来的一个连接，同步方法
            //        linkTcpClient = new NetTcpClient(remoteClient, readWaitNode);
            //        tcpClientList.Add(linkTcpClient);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            thMonitor = new Thread(StartListening);
            thMonitor.Name = "SpectrumData";
            thMonitor.IsBackground = true;
            thMonitor.Start();
        }
        /// <summary>
        /// 监听处理
        /// </summary>
        private void StartListening()
        {
            listener = new TcpListener(_ServerIp, _ServerPort);
            listener.Start(); //开始侦听:调用该实例上Start()方法开启对指定端口的侦听
            connect = true;
            while (connect)
            {
                try
                {
                    TcpClient remoteClient = listener.AcceptTcpClient();//获取客户端发来的一个连接，同步方法
                    linkTcpClient = new NetTcpClient(remoteClient);
                    tcpClientList.Add(linkTcpClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopServer()
        {
            try
            {
                foreach (NetTcpClient ntc in tcpClientList)
                {
                    ntc.ClientSocket.Close();
                }
                //if (thMonitor != null)
                //{
                //    thMonitor.Abort();
                //    thMonitor = null;
                //}
                listener.Stop();
            }
            catch(Exception exStop)
            {
                Console.WriteLine(exStop.Message);
            }
            connect = false;
        }

        public void  Dispose()
        {
            StopServer();
        }

        #region List操作
        /// <summary>
        /// 查找客户端
        /// </summary>
        /// <param name="_clientkey"></param>
        /// <returns></returns>
        public NetTcpClient FindClient(string _clientkey)
        {
            foreach (NetTcpClient client in tcpClientList)
            {
                if (client.ClientKey != null && client.ClientKey.Equals(_clientkey))
                {
                    return client;
                }
            }
            return null;
        }
        /// <summary>
        /// 移除指定的连接
        /// </summary>
        /// <param name="_clientkey"></param>
        /// <returns></returns>
        public bool RemoveClient(string _clientkey)
        {
            foreach (NetTcpClient client in tcpClientList)
            {
                if (client.ClientKey != null && client.ClientKey.Equals(_clientkey))
                {
                    try
                    {
                        tcpClientList.Remove(client);
                        client.Close();
                    }
                    catch
                    {
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 移除断开的连接
        /// </summary>
        public void RemoveUnLink()
        {
            foreach (NetTcpClient client in tcpClientList)
            {
                if (!client.ClientSocket.Connected)
                {
                    tcpClientList.Remove(client);
                    continue;
                }
            }
        }

        #endregion End
    }
}

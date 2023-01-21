using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class TCPClient
    {
        private TcpClient tcpClient = null;
        Thread thData = null;

        public event OnClothOnLine OnClothBuildHandle;
        public TCPClient(TcpClient client)
        {
            tcpClient = client;
        }

        public event OnClosedAction OnClosedHandle;

        public TcpClient Client
        {
            get
            {
                return tcpClient;
            }
            set
            {
                tcpClient = value;
            }
        }

        public string ClientKey
        {
            get;
            set;
        }
        /// <summary>
        /// 生产线号
        /// </summary>
        public string LineNum
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已经连接
        /// </summary>
        public bool Connected
        {
            get
            {
                try
                {
                    if (tcpClient != null)
                    {
                        return tcpClient.Connected;
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
        }
        /// <summary>
        /// 开始接收数据
        /// </summary>
        public void BeginReceive()
        {
            if(thData != null)
            {
                thData.Abort();
                thData = null;
            }
            thData = new Thread(DataDeal);
            thData.IsBackground = true;
            thData.Name = "th" + ClientKey;
            thData.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            try
            {
                if (thData != null)
                {
                    thData.Abort();
                    thData = null;
                }
                Client?.Close();
            }
            catch
            { }
        }

        private void DataDeal()
        {
            while(true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    if(Client.Client.Connected)
                    {
                        int length = Client.Client.Receive(buffer);
                        if (length > 0)
                        {
                            string res = Encoding.UTF8.GetString(buffer, 0, length);

                            Console.WriteLine(Client.Client.RemoteEndPoint.ToString() + " 接收数据：" + res);
                            try
                            {
                                var item = Json.JSONUtil.DeserializeJSON<FabricClothItem>(res);// JsonConvert.DeserializeObject<BaseResponse<ReportResult>>(response);
                                if(item != null)
                                {
                                    OnClothBuildHandle?.Invoke(item);
                                }
                            }
                            catch
                            { }
                        }
                        else
                        {
                            Thread.Sleep(50);
                        }
                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(100);//等待100ms
                }
                catch
                { }
            }
        }
    }
}

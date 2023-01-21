using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Yu3zx.Util;

namespace Yu3zx.Communication
{
    /// <summary>
    /// 与服务器相连的一个客户端
    /// 创建人：kingkie
    /// 创建时间：2018-11-18
    /// </summary>
    public class NetTcpClient
    {
        private bool connected;
        private TcpClient clientSocket;
        private NetworkStream netStream;
        private const int BUFFERSIZE = 1024;
        private byte[] dataBuffer = new byte[BUFFERSIZE];
        private byte[] tempBuffer = new byte[BUFFERSIZE];
        private int receivedPos = 0;
        private string clientkey;
        public event EventHandler DisConnected;
        const int SpectrumLen = 2048;//频谱数据长度

        private List<byte> dataListBuffer;

        /// <summary>
        /// 获取TcpClient
        /// </summary>
        public TcpClient ClientSocket
        {
            get { return clientSocket; }
        }
        /// <summary>
        /// 客户端唯一标识键,IP地址和端口号
        /// </summary>
        public string ClientKey
        {
            get { return this.clientkey; }
            //set { this.clientkey = value; }
        }
        /// <summary>
        /// 获取网络数据流
        /// </summary>
        /// <returns></returns>
        public NetworkStream GetNetStream
        {
            get { return netStream; }
        }

        /// <summary>
        /// TcpClient封装-以IP区分不同客户端
        /// </summary>
        /// <param name="clientsocket"></param>
        /// <param name="readwait"></param>
        public NetTcpClient(TcpClient clientsocket)
        {
            connected = true;
            this.clientSocket = clientsocket;
            this.netStream = clientsocket.GetStream();
            IPEndPoint ipclient = (IPEndPoint)clientSocket.Client.RemoteEndPoint;
            clientkey = ipclient.Address.ToString();// +ipclient.Port.ToString();//标识客户端
            dataListBuffer = new List<byte>(4096);
            //开启读数据线程
            Thread readThread = new Thread(ReadThread);
            readThread.Priority = ThreadPriority.AboveNormal;
            readThread.Start();
        }

        private int currentpos = 0;//当前位置
        private int cntAnylas = 0;
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
                //采集数据并解析 currentpos采集到的位置
                if (currentpos + readBytes >= BUFFERSIZE)
                {
                    Buffer.BlockCopy(tempBuffer, 0, dataBuffer, currentpos, BUFFERSIZE - currentpos);
                    Buffer.BlockCopy(tempBuffer, BUFFERSIZE - currentpos, dataBuffer, 0, currentpos + readBytes - BUFFERSIZE);
                    currentpos = currentpos + readBytes - BUFFERSIZE;
                }
                else
                {
                    Buffer.BlockCopy(tempBuffer, 0, dataBuffer, currentpos, readBytes);
                    currentpos = currentpos + readBytes;
                }
                //==========================================
                byte[] data = new byte[readBytes];
                Buffer.BlockCopy(tempBuffer, 0, data, 0, readBytes);
                dataListBuffer.AddRange(data);
                if (dataListBuffer.Count > 8)
                {
                    int rmvcnt = 0;
                    for (int i = 0; i < dataListBuffer.Count - 8;i++ )
                    {
                        if (dataListBuffer[i] == 0x23)//判断是不是开头
                        {
                            int dlen = dataListBuffer[i + 4];
                            if (dataListBuffer.Count >= dlen + 8)
                            {
                                if (dataListBuffer[i + dlen + 7] == 0x0D)
                                {
                                    byte[] crcbytes = new byte[dlen + 5];
                                    dataListBuffer.CopyTo(i, crcbytes, 0, dlen + 5);
                                    //int crcCalc = CRCHelper.GetTable_CRC(crcbytes, true);
                                    //int crcGet = (dataListBuffer[i + dlen + 5] + dataListBuffer[i + dlen + 6] * 256);
                                    if (CRCHelper.GetTable_CRC(crcbytes,true) == (dataListBuffer[i + dlen + 5] + dataListBuffer[i + dlen + 6] * 256))
                                    {
                                        //Spectrum _data = new Spectrum();
                                        //if (clientkey == "")
                                        //{
                                        //    clientkey = dataListBuffer[i + 1].ToString("X2") + dataListBuffer[i + 2].ToString("X2");
                                        //}
                                        //_data.MsgId = clientkey;
                                        //_data.SpectrumData = dataListBuffer.GetRange(i, dlen + 5).ToArray();
                                        //MainUtilities.GetInstance().SpectrumList.Enqueue(_data);//把数据添加到公共队列里
                                        //i = i + dlen + 8;
                                        //rmvcnt = i;//要移除的长度
                                    }
                                }
                                else
                                {
                                    rmvcnt = i;
                                }
                            }
                            else
                            {
                                rmvcnt = i;
                                break;
                            }
                        }
                        else
                        {
                            rmvcnt = i;
                        }
                    }
                    dataListBuffer.RemoveRange(0, rmvcnt);
                }
                //if (dataListBuffer.Count >= SpectrumLen)
                //{
                //    Spectrum _data = new Spectrum();
                //    _data.MsgId = clientkey;
                //    _data.SpectrumData = dataListBuffer.GetRange(0, SpectrumLen).ToArray();
                //    dataListBuffer.RemoveRange(0,SpectrumLen);
                //    MainUtilities.GetInstance().SpectrumList.Enqueue(_data);//把数据添加到公共队列里
                //}

                //将收到数据存入缓冲区
                //if (receivedPos + readBytes >= BUFFERSIZE)
                //{
                //    receivedPos = 0;
                //}
                //lock (this.dataBuffer)
                //{
                //    Buffer.BlockCopy(tempBuffer, 0, dataBuffer, receivedPos, readBytes);
                //}
                //receivedPos += readBytes;
                //触发读事件==2018-12-16
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
            if (this.clientSocket != null)
            {
                this.clientSocket.Close();
            }
        }

    }

    /// <summary>
    /// 等待读事件的类：主要封装了NetTcpClient到里面，用来获取触发读事件的客户端
    /// 创建人：kingkie
    /// 创建时间：2018-11-18
    /// </summary>
    public class ReadWaitNode
    {
        private AutoResetEvent readEvent;
        private NetTcpClient tcpClient = null;

        public NetTcpClient TcpClient
        {
            get { return this.tcpClient; }
            set { this.tcpClient = value; }
        }
        public ReadWaitNode(bool initialState)
        {
            readEvent = new AutoResetEvent(initialState);
        }
        public bool WaitOne()
        {
            return readEvent.WaitOne();
        }
        public bool Reset()
        {
            return readEvent.Reset();
        }
        public bool Set()
        {
            return readEvent.Set();
        }
    }
}

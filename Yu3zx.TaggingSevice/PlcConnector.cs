using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using S7.Net;

namespace Yu3zx.TaggingSevice
{
    public class PlcConnector
    {
        private TcpClient tcpClient = null;
        Thread thData = null;

        private Socket listenSocket;

        public PlcConnector()
        {

        }

        public PlcConnector(TcpClient client)
        {
            tcpClient = client;
        }

        public event OnClosedAction OnClosedHandle;
        /// <summary>
        /// 
        /// </summary>
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
        /// 服务端口号
        /// </summary>
        public int Port
        {
            get;
            set;
        }
        /// <summary>
        /// 服务IP地址
        /// </summary>
        public string ServerIp
        {
            get;
            set;
        }
        /// <summary>
        ///机架号 Rack 默认为 0
        /// </summary>
        public int Rack
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 槽号
        /// </summary>
        public int Slot
        {
            get;
            set;
        } = 1;
        /// <summary>
        /// PLC是否连接
        /// </summary>
        public bool S7Connected
        {
            get
            {
                if(S7Plc != null)
                {
                    return S7Plc.IsConnected;
                }
                else
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
            if (thData != null)
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
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    if (Client.Client != null && Client.Client.Connected)
                    {
                        int length = Client.Client.Receive(buffer);
                        if (length > 0)
                        {
                            try
                            {
                                byte[] revBytes = new byte[length];
                                Array.Copy(buffer, 0, revBytes, 0, length);
                                string strRec = string.Join(" ", revBytes.Select(x => x.ToString("X2")));
                                Console.WriteLine("接收到数据：" + strRec);
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

        public void SendCmd(byte[] bytes)
        {
            if(bytes != null)
            {

            }
        }

        //----------PLC--------------
        public Plc S7Plc
        {
            get;
            set;
        }

        public void S7Connet()
        {
            //CpuType cputype = (CpuType)(Enum.Parse(typeof(CpuType), cboType.Text, true));
            CpuType cputype = CpuType.S71200;
            if(S7Plc == null)
            {
                S7Plc = new Plc(cputype, ServerIp, Convert.ToInt16(Rack), Convert.ToInt16(Slot));
            }
            else
            {
                if(S7Plc.Rack != Rack || S7Plc.Slot != Slot)
                {
                    S7Plc = new Plc(cputype, ServerIp, Convert.ToInt16(Rack), Convert.ToInt16(Slot));
                }
            }

            try
            {
                S7Plc.Open();
                //if (S7Plc.IsConnected)
                //{
                //    S7Connected = true;
                //}
                //S7Connected = S7Plc.IsConnected;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void WriteDataBlock(int dbInt,int startInt, byte[] bytes)
        {
            try
            {
                if(S7Plc == null)
                {
                    S7Connet();
                    return;
                }
                if(S7Plc.IsConnected)
                {
                    //S7Plc.Write("DB1.DBX0.0", 1);
                    //第一个参数DB数据类型；DB号；参数起始地址；PLC内该变量的类型；需要读取的个数
                    //bool result = (bool)S7Plc.Read(DataType.DataBlock, 10, 0, VarType.Bit, 1);
                    //第一个参数DB的数据类型，可以是DB，定时器，计数器，Merker内存，输入输出；DB号；起始地址；写入的值
                    //S7Plc.Write(DataType.DataBlock, 10, 0, true);
                    S7Plc.WriteBytes(DataType.DataBlock, dbInt, startInt, bytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public byte[] ReadDataBlock(int dbInt,int startInt,int cnt)
        {
            try
            {
                if (S7Plc == null)
                {
                    S7Connet();
                }
                if (S7Plc.IsConnected)
                {
                    byte[] revData = S7Plc.ReadBytes(DataType.DataBlock, dbInt, startInt, cnt);
                    return revData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void WriteFlag(int dbInt, int startInt, bool bFlag)
        {
            try
            {
                if (S7Plc == null)
                {
                    S7Connet();
                    return;
                }
                if (S7Plc.IsConnected)
                {
                    //S7Plc.Write("DB1.DBX0.0", 1);
                    //第一个参数DB数据类型；DB号；参数起始地址；PLC内该变量的类型；需要读取的个数
                    //bool result = (bool)S7Plc.Read(DataType.DataBlock, 10, 0, VarType.Bit, 1);
                    //第一个参数DB的数据类型，可以是DB，定时器，计数器，Merker内存，输入输出；DB号；起始地址；写入的值
                    //S7Plc.Write(DataType.DataBlock, 10, 0, true);
                    S7Plc.Write(DataType.DataBlock, dbInt, startInt, bFlag);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool ReadFlag(int dbInt, int startInt)
        {
            try
            {
                if (S7Plc == null)
                {
                    S7Connet();
                }
                if (S7Plc.IsConnected)
                {
                    var rtnBool = (bool)S7Plc.Read(DataType.DataBlock, dbInt, startInt, VarType.Bit, 1);
                    return rtnBool;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}

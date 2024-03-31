using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.Util
{
    public class NetUtil
    {
        public static string[] getLocalIP()
        {
            //初始化本地IP下拉列表
            IPAddress[] ipArr = Dns.GetHostAddresses(Dns.GetHostName());
            List<string> lIPList = new List<string>();
            for (int i = 0; i < ipArr.Length; i++)
            {
                if (ipArr[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    lIPList.Add(ipArr[i].ToString());
                }
            }
            return lIPList.ToArray();
        }

        #region 判断网络
        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;
        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(
        ref int dwFlag,
        int dwReserved
        );

        /// <summary>
        /// 判断网络的连接状态
        /// </summary>
        /// <returns>
        /// 网络状态(1-->未联网;2-->采用调治解调器上网;3-->采用网卡上网)
        ///</returns>
        public static int GetNetConStatus(string strNetAddress)
        {
            int iNetStatus = 0;
            int dwFlag = new int();
            if (!InternetGetConnectedState(ref dwFlag, 0))
            {
                //没有能连上互联网
                iNetStatus = 1;
            }
            else if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
            {
                //采用调治解调器上网,需要进一步判断能否登录具体网站
                if (PingNetAddress(strNetAddress))
                {
                    //可以ping通给定的网址,网络OK
                    iNetStatus = 2;
                }
                else
                {
                    //不可以ping通给定的网址,网络不OK
                    iNetStatus = 4;
                }
            }
            else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
            {
                //采用网卡上网,需要进一步判断能否登录具体网站
                if (PingNetAddress(strNetAddress))
                {
                    //可以ping通给定的网址,网络OK
                    iNetStatus = 3;
                }
                else
                {
                    //不可以ping通给定的网址,网络不OK
                    iNetStatus = 5;
                }
            }
            Console.WriteLine("网络状态：" + iNetStatus);
            return iNetStatus;
        }

        /// <summary>
        /// ping 具体的网址看能否ping通
        /// </summary>
        /// <param name="strNetAdd"></param>
        /// <returns></returns>
        public static bool PingNetAddress(string strNetAdd)
        {
            bool Flage = false;
            Ping ping = new Ping();
            try
            {
                PingReply pr = ping.Send(strNetAdd, 5000);
                if (pr.Status == IPStatus.TimedOut)
                {
                    Flage = false;
                }
                if (pr.Status == IPStatus.Success)
                {
                    Flage = true;
                }
                else
                {
                    Flage = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Flage = false;
            }
            Console.WriteLine("Ping网络状态：" + Flage);
            return Flage;
        }

        //用于转换ip地址
        [DllImport("ws2_32.dll")]
        public static extern int inet_addr(string cp);

        //用于发送APR包（根据APR协议！）
        [DllImport("IPHLPAPI.dll")]
        public static extern int SendARP(int DestIP, int ScrIP, ref long pMacAddr, ref int PhyAddrLen);
        /// <summary>
        /// 根据IP地址发送ARP获取MAC地址
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="destMac"></param>
        /// <returns></returns>
        public static int ExtractMac(string IP, ref string destMac)
        {
            StringBuilder MacRouteBuilder = new StringBuilder();
            int flag = 0;
            try
            {
                int ldest = inet_addr(IP); //将IP地址从 点数格式转换成无符号长整型
                long macinfo = new long();
                int len = 6;
                //SendARP函数发送一个地址解析协议(ARP)请求获得指定的目的地IPv4地址相对应的物理地址
                int ret = SendARP(ldest, 0, ref macinfo, ref len);
                if (ret == 0)
                {
                    string TmpMac = Convert.ToString(macinfo, 16).PadLeft(12, '0');//转换成16进制
                                                                                   //
                    for (int i = 10; i >= 0; i = i - 2)//反过来读取，原因可以查看接口函数sendApr！
                    {
                        MacRouteBuilder.Append(TmpMac.Substring(i, 2).ToUpper());

                        if (i >= 2)
                        {
                            MacRouteBuilder.Append("-");
                        }
                    }
                    flag = 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            if (flag == 1)
            {
                destMac = MacRouteBuilder.ToString();
                return 0;
            }
            return -1;
        }


        #endregion End
    }
}

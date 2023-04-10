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

        #endregion End
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Yu3zx.Util
{
    public class DateTimeHelper
    {
        public static string GetWeek(DateTime dateTime)
        {
            string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = Day[Convert.ToInt32(dateTime.DayOfWeek.ToString("d"))].ToString();
            return week;
        }

        public static DateTime GetDateTime(string strDateTime, string strPattern)
        {
            //DateTime dt = DateTime.ParseExact(strDateTime, strPattern, System.Globalization.CultureInfo.CurrentCulture);
            DateTime dt = DateTime.ParseExact(strDateTime, strPattern, System.Globalization.CultureInfo.InvariantCulture);
            return dt;
        }

        /// <summary>
        /// 获取时间10位戳
        /// </summary>
        /// <returns>时间长整形数据</returns>
        public static long GetTimeStampTen()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
        /// <summary>
        /// 生成时间流水号
        /// </summary>
        /// <returns>时间流水号</returns>
        public static int BuildTimeSerialNo()
        {
            TimeSpan timeSpan = DateTime.Now - DateTime.Parse("2018-01-01");
            return (int)timeSpan.TotalSeconds;
        }
       /// <summary>
       /// 判断凌晨房、人工校验时间是否在时间段内
       /// </summary>
       /// <param name="sdt"></param>
       /// <param name="edt"></param>
       /// <returns></returns>
        public static bool IsMorningRoomDt(DateTime sdt, DateTime edt)
        {
            //判断当前时间是否在工作时间段内 
            //DateTime sdt;
            //DateTime.TryParse(MorningRoomSdt, out sdt);
            //DateTime edt;
            //DateTime.TryParse(MorningRoomEdt, out edt);
            if (sdt > edt && DateTime.Now > edt)
                edt = edt.AddDays(1);
            if (sdt > edt && DateTime.Now < edt)
                sdt = sdt.AddDays(-1);
            if (DateTime.Now > sdt && DateTime.Now < edt)
                return true;
            return false;
        }

        //设置系统时间的API函数
        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME time);
        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }
        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="dt">需要设置的时间</param>
        /// <returns>返回系统时间设置状态，true为成功，false为失败</returns>
        public static bool SetDate()
        {
            string getNetDateTimes = DateTimeHelper.GetNetDateTimes();
            DateTime dt = DateTime.Now;
            Console.WriteLine($"正在同步服务器时间，本地时间为:{dt},服务器时间为：{getNetDateTimes}");
            DateTime.TryParse(getNetDateTimes, out dt);
            SYSTEMTIME st;
            st.year = (short)dt.Year;
            st.month = (short)dt.Month;
            st.dayOfWeek = (short)dt.DayOfWeek;
            st.day = (short)dt.Day;
            st.hour = (short)dt.Hour;
            st.minute = (short)dt.Minute;
            st.second = (short)dt.Second;
            st.milliseconds = (short)dt.Millisecond;
            bool rt = SetLocalTime(ref st);
            //Tool.In.AppStartDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return rt;
        }
        /// <summary>
        /// 获取网络日期时间
        /// </summary>
        /// <returns>时间字符串</returns>
        public static string GetNetDateTimes()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy(); //代码就可以顺利和https服务器建立SSL通道

                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse(); //基础连接已经关闭: 无法与远程服务器建立信任关系
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                {
                    if (h == "Date")
                    {
                        datetime = headerCollection[h];
                    }
                }
                return datetime;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取网络日期时间错误:" + ex.Message);
                return datetime;
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (headerCollection != null)
                {
                    headerCollection.Clear();
                }
            }
        }
        /// <summary>
        /// 同步网络时间
        /// </summary>
        /// <returns>是否同步成功</returns>
        public static bool SynchNetDateTimes()
        {
            string[] synchTimeHosts = { "0.cn.pool.ntp.org", "edu.ntp.org.cn", "2.cn.pool.ntp.org", "cn.pool.ntp.org", "ntp.ntsc.ac.cn", "1.cn.pool.ntp.org", "3.cn.pool.ntp.org" };
            IPHostEntry iphostinfo;
            IPAddress ip;
            IPEndPoint ipe;
            DateTime startDT = DateTime.Now;
            int port = 13;
            string sEx = string.Empty;
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //创建Socket

            c.ReceiveTimeout = 8 * 1000; //设置超时时间
                                          // 遍历时间服务器列表
            foreach (string strHost in synchTimeHosts)
            {
                try
                {
                    iphostinfo = Dns.GetHostEntry(strHost);
                    ip = iphostinfo.AddressList[0];
                    ipe = new IPEndPoint(ip, port);

                    c.Connect(ipe); //连接到服务器
                    if (c.Connected)
                    {
                        Console.WriteLine("当前服务地址:" + strHost);
                        break; //如果连接到服务器就跳出
                    }
                }
                catch (Exception ex)
                {
                    sEx = ex.Message;
                }
            }
            try
            {
                //SOCKET同步接受数据
                byte[] recvBuffer = new byte[1024];
                int nBytes, nTotalBytes = 0;
                StringBuilder sb = new StringBuilder();
                System.Text.Encoding myE = Encoding.UTF8;

                while ((nBytes = c.Receive(recvBuffer, 0, 1024, SocketFlags.None)) > 0)
                {
                    nTotalBytes += nBytes;
                    sb.Append(myE.GetString(recvBuffer, 0, nBytes));
                }

                //关闭连接
                c.Close();
                Console.WriteLine("获取的时间:" + sb.ToString());
                string[] o = sb.ToString().Split(' '); // 打断字符串

                TimeSpan k = new TimeSpan();
                k = (TimeSpan)(DateTime.Now - startDT); //得到开始到现在所消耗的时间
                Console.WriteLine("获取的时间1:" + (o[1] + " " + o[2]));
                DateTime setDT = Convert.ToDateTime(o[1] + " " + o[2]).Subtract(-k); // 减去中途消耗的时间
                                                                                     //处置北京时间 +8时
                string newStr = sb.ToString().Replace("CEST", string.Empty).Trim();
                //setDT = setDT.AddHours(8);
                DateTime.TryParse(newStr, out setDT);
                setDT = setDT.AddHours(6);
                Console.WriteLine("处理的时间:" + setDT.ToString("yyyy-MM-dd HH:mm:ss"));
                //转换System.DateTime到SystemTime
                SYSTEMTIME st;
                st.year = (short)setDT.Year;
                st.month = (short)setDT.Month;
                st.dayOfWeek = (short)setDT.DayOfWeek;
                st.day = (short)setDT.Day;
                st.hour = (short)setDT.Hour;
                st.minute = (short)setDT.Minute;
                st.second = (short)setDT.Second;
                st.milliseconds = (short)setDT.Millisecond;
                return SetLocalTime(ref st);
            }
            catch(Exception ex)
            {
                Console.WriteLine("同步时间异常:" + ex.Message);
                return false;
            }
        }

        public class TrustAllCertificatePolicy : ICertificatePolicy
        {
            public TrustAllCertificatePolicy()
            {
            }

            public bool CheckValidationResult(ServicePoint sp, System.Security.Cryptography.X509Certificates.X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }
    }
}

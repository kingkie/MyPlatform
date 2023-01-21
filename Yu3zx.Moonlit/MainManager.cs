using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using Yu3zx.DataBases;
using System.Text;
using Yu3zx.Acquisition;

namespace Yu3zx.Moonlit
{
    /// <summary>
    /// 控制程序整个运行，集成常用变量，常用方法等单例
    /// </summary>
    public class MainManager
    {
        #region 单例定义
        private static object syncObj = new object();
        private static MainManager instance = null;
        public static MainManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = new MainManager();
                }
            }
            return instance;
        }

        #endregion End

        #region 常用变量

        public MysqlFactory DbFactory;

        public Thread thCollect = null; //收集

        private mainFrm mainFrm;//主窗体

        #endregion End

        /// <summary>
        /// 主程序初始化参数
        /// </summary>
        public void InitMain()
        {
            MySQLDbHelper.connectionString = ProgramManager.GetInstance().DbConnString;
            MainManager.GetInstance().DbFactory = new MysqlFactory(ProgramManager.GetInstance().DbConnString);
            if (!MainManager.GetInstance().DbFactory.ConnectionTest())
            {
                MessageBox.Show("数据库链接错误！");
                Environment.Exit(0);
            }
        }

        public bool InitDevices(out string strMsg)
        {
            try
            {
                strMsg = string.Empty;
                StringBuilder sbMsg = new StringBuilder();
                foreach (AcqPlc acqplc in MyPlcManager.GetInstance().Devices)
                {
                    var rtnOpen = acqplc.OpenDevice();
                    if (!rtnOpen.IsSuccess)
                    {
                        sbMsg.Append("设备" + acqplc.DeviceId).Append("打开连接错误:" + rtnOpen.Message + ";");
                    }
                }
                strMsg = sbMsg.ToString();
                return true;
            }
            catch(Exception ex)
            {
                strMsg = "初始化设备异常:" + ex.Message;
                Logs.Log.Instance.LogWrite(strMsg, Logs.MsgLevel.Err);
                return false;
            }
        }

        /// <summary>
        /// 初始化主窗体变量
        /// </summary>
        /// <param name="tmpmainfrm">设置主窗体</param>
        public void SetMainForm(mainFrm tmpmainfrm)
        {
            mainFrm = tmpmainfrm;
        }

        private string GroupToString(float[] f)
        {
            string rtnStr = string.Join(",", f);
            return rtnStr;
        }

        /// <summary>
        /// 获取本机IP列表
        /// </summary>
        /// <returns></returns>
        public string[] getLocalIP()
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

        /// <summary>
        /// 16进制字符串转换成byte数组
        /// </summary>
        /// <param name="_HexStr"></param>
        /// <returns></returns>
        public byte[] HexStrToBytes(string _HexStr)
        {
            string[] hexGroup = _HexStr.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries);
            byte[] rtnBytes = new byte[hexGroup.Length];
            for (int i = 0; i < hexGroup.Length; i++)
            {
                rtnBytes[i] = Convert.ToByte(hexGroup[i], 16);
            }
            return rtnBytes;
        }

    }
}

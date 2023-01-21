using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Json;

namespace Yu3zx.PackingMonitor
{
    /// <summary>
    /// 主程序常用配置类单例
    /// </summary>
    public class ProgramManager
    {
        #region 单例定义
        private static object syncObj = new object();
        private static ProgramManager instance = null;
        public static ProgramManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = Read();

                    if (instance == null)
                    {
                        instance = new ProgramManager();
                    }
                }
            }
            return instance;
        }

        private string _DefaultFilePathString = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            lock (syncObj)
            {
                try
                {
                    //string filepathstring = Path.Combine(_DefaultFilePathString, "Config", "\\ProgramConfig.json");
                    string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ProgramManager.json";
                    //为了断电等原因导致xml文件保存出错，文件损坏，采用先写副本再替换的方式。
                    using (TextWriter textWriter = File.CreateText(filepathstring))
                    {
                        string jsonsavestr = JSONUtil.SerializeJSON(this);
                        textWriter.Write(jsonsavestr);
                        textWriter.Flush();
                    }
                    if (File.Exists(filepathstring))
                    {
                        FileInfo fi = new FileInfo(filepathstring);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        private static ProgramManager Read()
        {
            string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ProgramManager.json";
            if (filepathstring == "")
                return null;
            if (!File.Exists(filepathstring))
            {
                using (TextWriter textWriter = File.CreateText(filepathstring))
                {
                    textWriter.Write("{}");
                    textWriter.Flush();
                }
            }

            using (StreamReader sr = new StreamReader(filepathstring))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.TypeNameHandling = TypeNameHandling.Objects;//这一行就是设置Json.NET能够序列化接口或继承类的关键，将TypeNameHandling设置为All
                    //构建Json.net的读取流
                    JsonReader reader = new JsonTextReader(sr);
                    //对读取出的Json.net的reader流进行反序列化，并装载到模型中
                    ProgramManager instancetmp = serializer.Deserialize<ProgramManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new ProgramManager();
                }
            }
        }
        #endregion End

        #region 变量定义
        public int QrCamera
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 二维码摄像头分辨率
        /// </summary>
        public int QrCameraRate
        {
            get;
            set;
        } = 0;

        public int MonitorCamera
        {
            get;
            set;
        } = 1;

        public int MonitorCameraRate
        {
            get;
            set;
        } = 0;

        #endregion End

        #region 配置保存
        /// <summary>
        /// 串口号
        /// </summary>
        public string PortName
        {
            set;
            get;
        }

        public int BaudRate
        {
            set;
            get;
        }

        public int DataBit
        {
            set;
            get;
        }

        public string Parity
        {
            set;
            get;
        }

        public int StopBit
        {
            set;
            get;
        }

        public bool DTR
        {
            set;
            get;
        }

        public bool RTS
        {
            set;
            get;
        }

        /// <summary>
        /// 图片文件夹
        /// </summary>
        public string ImgsFolder
        {
            get;
            set;
        }
        #endregion End
    }
}

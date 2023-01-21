using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Windows.Forms;
using Yu3zx.Json;

namespace Yu3zx.Moonlit
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
                    string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ProgramConfig.json";
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
            string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ProgramConfig.json";
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

        #region 变量定义区
        private string _DbConnString = "";//数据库连接字符串
        private string _AccessMode = "";
        private bool autoRun = true;

        #endregion End

        #region 配置保存

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnString
        {
            set
            {
                _DbConnString = value;
            }
            get { return _DbConnString; }
        }

        /// <summary>
        /// 自动启动服务
        /// </summary>
        public bool AutoRun
        {
            set { autoRun = value; }
            get { return autoRun; }
        }

        #endregion End

        #region 打印配置
        public int MarginTop
        {
            get;
            set;
        } = 10;

        public int MarginBottom
        {
            get;
            set;
        } = 10;

        public int MarginLeft
        {
            get;
            set;
        } = 10;

        public int MarginRight
        {
            get;
            set;
        } = 10;

        public int QrHeight
        {
            get;
            set;
        } = 103;

        public int QrWidth
        {
            get;
            set;
        } = 85;

        public float QrFontSize
        {
            get;
            set;
        } = 18;

        #endregion End
    }
}

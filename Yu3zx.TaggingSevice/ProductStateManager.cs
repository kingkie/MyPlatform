using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Yu3zx.Json;

namespace Yu3zx.TaggingSevice
{
    /// <summary>
    /// 当前系统运行状态
    /// </summary>
    public class ProductStateManager : IDisposable
    {
        #region  单例
        private static object syncObj = new object();
        private static ProductStateManager instance = null;
        public static ProductStateManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = new ProductStateManager();
                }
            }
            return instance;
        }

        ProductStateManager()
        {

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
                    //string filepathstring = Path.Combine(_DefaultFilePathString, "Config", "\\Devices.json");
                    string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ProductStateManager.json";
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

        private static ProductStateManager Read()
        {
            string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ProductStateManager.json";
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
                    ProductStateManager instancetmp = serializer.Deserialize<ProductStateManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new ProductStateManager();
                }
            }
        }

        public void Dispose()
        {
            lock (syncObj)
            {
                instance = null;
            }
        }

        #endregion End

        /// <summary>
        /// 生产序号
        /// </summary>
        public int ProductSerialNo
        {
            get;
            set;
        }


        /// <summary>
        /// 上线批次的数量
        /// </summary>
        public Dictionary<string, OnLineCloth> DictOnLine = new Dictionary<string, OnLineCloth>();
    }
}

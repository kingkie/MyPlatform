using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Yu3zx.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yu3zx.Moonlit.Modols;

namespace Yu3zx.Moonlit
{
    public class ValueNodesManager
    {
        #region 单例定义
        private static object syncObj = new object();
        private static ValueNodesManager instance = null;
        public static ValueNodesManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = Read();

                    if (instance == null)
                    {
                        instance = new ValueNodesManager();
                    }
                }
            }
            return instance;
        }

        public ValueNodesManager()
        {
            DevBindValues = new DevValueBind();
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
                    string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ValueNodesConfig.json";
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

        private static ValueNodesManager Read()
        {
            string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\ValueNodesConfig.json";
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
                    ValueNodesManager instancetmp = serializer.Deserialize<ValueNodesManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new ValueNodesManager();
                }
            }
        }
        #endregion End

        public DevValueBind DevBindValues;
    }
}

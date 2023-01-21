using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using Yu3zx.Json;

namespace Yu3zx.Acquisition
{
    /// <summary>
    /// 设备管理类
    /// </summary>
    public class MyPlcManager : IDisposable
    {
        #region  初始化单例
        private static object syncObj = new object();
        private static MyPlcManager instance = null;
        public static MyPlcManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = Read();
                    if(instance == null)
                    {
                        instance = new MyPlcManager();
                    }
                }
            }
            return instance;
        }

        MyPlcManager()
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
                    //string filepathstring = Path.Combine(_DefaultFilePathString, "Config", "\\MyPlcDevices.json");
                    string filepathstring = Path.Combine(_DefaultFilePathString, "Config") + "\\MyPlcDevices.json";
                    //string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\MyPlcDevices.json";
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

        private static MyPlcManager Read()
        {

            string filepathstring = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config") + "\\MyPlcDevices.json"; //Application.StartupPath
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
                    MyPlcManager instancetmp = serializer.Deserialize<MyPlcManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new MyPlcManager();
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

        public List<AcqPlc> Devices = new List<AcqPlc>();

        /// <summary>
        /// 通过设备ID查找设备
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public AcqPlc GetDeviceById(string deviceId)
        {
            return Devices.Find(x => x.DeviceId == deviceId);
        }
        /// <summary>
        /// 增加设备
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        public bool AddDevice(AcqPlc dev)
        {
            if (dev != null && dev.DeviceId != "")
            {
            }
            else
            {
                //MessageBox.Show("增加的设备及其ID不能为空！");
                return false;
            }
            AcqPlc devm = GetDeviceById(dev.DeviceId);
            if (devm != null)
            {
                //MessageBox.Show("已存在此通道");
                return false;
            }
            else
            {
                Devices.Add(dev);
                return true;
            }
        }
        /// <summary>
        /// 移除设备
        /// </summary>
        /// <param name="devid"></param>
        public void RemoveDevice(string devid)
        {
            AcqPlc dev = GetDeviceById(devid);
            if (dev != null)
            {
                Devices.Remove(dev);
            }
        }

    }
}

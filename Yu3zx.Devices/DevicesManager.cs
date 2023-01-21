using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Yu3zx.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Windows.Forms;

namespace Yu3zx.Devices
{
    public class DevicesManager : IDisposable
    {
        #region  单例
        private static object syncObj = new object();
        private static DevicesManager instance = null;
        public static DevicesManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = new DevicesManager();
                }
            }
            return instance;
        }

        DevicesManager()
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
                    string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\Devices.json";
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

        private static DevicesManager Read()
        {
            string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\Devices.json";
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
                    DevicesManager instancetmp = serializer.Deserialize<DevicesManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new DevicesManager();
                }
            }
        }

        #endregion End

        public List<Device> Devices = new List<Device>();

        /// <summary>
        /// 通过设备ID查找设备
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public Device GetDeviceById(string deviceId)
        {
            return Devices.Find(x =>x.DeviceId == deviceId);
        }

        public void AddDevice(Device dev)
        {
            if (dev != null && dev.DeviceId != "")
            {
            }
            else
            {
                MessageBox.Show("增加的设备及其ID不能为空！");
                return;
            }
            Device devm = GetDeviceById(dev.DeviceId);
            if (devm != null)
            {
                MessageBox.Show("已存在此通道");
            }
            else
            {
                Devices.Add(dev);
            }
        }

        public void RemovePipe(string devid)
        {
            Device dev = GetDeviceById(devid);
            if (dev != null)
            {
                Devices.Remove(dev);
            }
        }

        public void Dispose()
        {
            lock (syncObj)
            {
                instance = null;
            }
        }
    }
}

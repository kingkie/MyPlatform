using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Yu3zx.Json;

namespace Yu3zx.Enroll
{
    public class SoftRegisterManager
    {
        private static object syncObj = new object();
        private static SoftRegisterManager instance = null;
        public static SoftRegisterManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = Read();

                    if (instance == null)
                    {
                        instance = new SoftRegisterManager();
                    }
                }
            }
            return instance;
        }


        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc =
                 new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk =
                 new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns></returns>
        public string GetCpuSerialNumber()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }
        /// <summary>
        /// 计算注册码
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public string CalmRegString(string sn)
        {
            sn = new string(sn.ToCharArray().Reverse().ToArray());
            byte[] result = Encoding.Default.GetBytes(sn);//
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] boutput = md5.ComputeHash(result);
            string strout = BitConverter.ToString(boutput).Replace("-", "");  //
            return strout;
        }

        /// <summary>
        /// 检验注册码
        /// </summary>
        /// <returns></returns>
        public bool CheckCode()
        {
            try
            {
                //有注册码，判断是否正确
                if(_regstring.Length > 0)
                {
                    string strDiskSn = GetDiskVolumeSerialNumber();
                    string strgetcode = CalmRegString(strDiskSn);
                    if(strgetcode == _regstring)//正确
                    {
                        return true;
                    }
                }
                //没有注册码或者注册码错误
                if(_probation == true)
                {

                    if(_regtime != DateTime.MinValue && _regtime.AddDays(30) > DateTime.Now)
                    {
                        _errmsg = "软件试用天数还剩" + (30 - (DateTime.Now -  _regtime).Days).ToString() + "天！";
                        return true;
                    }
                    else
                    {
                        if(_regtime == DateTime.MinValue)
                        {
                            _regtime = DateTime.Now;
                            _errmsg = "试用天数还剩30天！";
                            this.Save();
                            return true;
                        }
                        else
                        {
                            _errmsg = "试用已到期！";
                            _probation = false;
                            this.Save();
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private DateTime _regtime = DateTime.MinValue;
        private string _regstring = "";
        //private string _macstring = "";
        private bool _probation = true;//默认试用状态
        private string _errmsg = "";

        #region 属性
        [JsonConverter(typeof(ChinaDateTime))]
        public DateTime RegisterTime
        {
            set {_regtime = value; }
            get {return _regtime; }
        }
        /// <summary>
        /// 注册字符串
        /// </summary>
        public string RegisterString
        {
            set { _regstring = value; }
            get {return _regstring; }
        }
        /// <summary>
        /// 是否试用
        /// </summary>
        public bool Probation
        {
            set { _probation = value; }
            get { return _probation; }
        }

        [JsonIgnore]
        public string ErrMsg
        {
            get { return _errmsg; }
        }
        #endregion End

        private static SoftRegisterManager Read()
        {
            string filepathstring = @AppDomain.CurrentDomain.BaseDirectory + "Yu3zx.InfoReg.dll";
            if (filepathstring == "")
                return null;
            if(!File.Exists(filepathstring))
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
                    SoftRegisterManager instancetmp = serializer.Deserialize<SoftRegisterManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new SoftRegisterManager();
                }
            }
        }

        public bool Save()
        {
            lock (syncObj)
            {
                try
                {
                    string filepathstring = @AppDomain.CurrentDomain.BaseDirectory + "Yu3zx.InfoReg.dll";
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
    }

    /// <summary>
    /// 时间序列化处理类
    /// </summary>
    public class ChinaDateTime : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }

}

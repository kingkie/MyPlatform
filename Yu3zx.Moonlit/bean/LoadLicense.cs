using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.bean
{
   public class LoadLicense
    {
        private static LicenseModel _licenseModel = null;
        public static LicenseModel GetLicenseModel
        {
            get { return _licenseModel; }
        }
        public static Boolean isLicense() {//是否授权的判断
            //1、读取授权文件 并再标题显示，当授权到期后，无法登录系统
            // 6e4899486db15300fcea720bc79da15e 试用版
            // 6fb35b0c3b4099471b34d1f7e865c29e 正式版

            string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\license.json";
            if (filepathstring == "")
                return false;
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
                    _licenseModel = serializer.Deserialize<LicenseModel>(reader);
                    String LicenseStr = _licenseModel.License;
                    string deLicStr= Base64Encode.DecodeBase64("utf-8",LicenseStr);
                    string licstr=deLicStr.Substring(0, 32);
                    string timestr1 = deLicStr.Substring(32, 8);
                    if ("6e4899486db15300fcea720bc79da15e".Equals(licstr)) { //试用版
                        string[] format = { "yyyyMMdd" };
                        DateTime tempDate;
                        DateTime nowDate=DateTime.Now;
                        DateTime.TryParseExact(timestr1,
                                                   format,
                                                   System.Globalization.CultureInfo.InvariantCulture,
                                                   System.Globalization.DateTimeStyles.None,
                                                   out tempDate);

                        _licenseModel.Time = timestr1;
                        if (DateTime.Compare(tempDate, nowDate) > 0)
                        { //有效
                            _licenseModel.Valid = "1";
                            _licenseModel.Type = "0";//试用版
                            return true;
                        }
                      
                    }
                    else if ("6fb35b0c3b4099471b34d1f7e865c29e".Equals(licstr)){ //正式版
                        _licenseModel.Valid = "1";
                        _licenseModel.Type = "1";//正式版
                        return true;
                    }
                }
                catch
                {
                    string aa = "1";
                }
            }
            _licenseModel.Valid = "0";//无效
            return false;
        }
    }
    
}

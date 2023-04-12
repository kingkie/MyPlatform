using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Json
{
    /// <summary>
    /// JSON序列化与对象实例转换类扩展。
    /// </summary>
    public static class JsonConvertExtention
    {
        /// <summary>
        /// 将对象序列化为JSON格式.
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string Serialize(this object obj, bool format = false)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.Formatting = format ? Formatting.Indented : Formatting.None;
            setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(obj, setting);
        }

        /// <summary>
        /// 将JSON字符串反序列化为对象实体。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T Deserialize<T>(this string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("JSON转换异常：{0}，\r\n{1}", ex.Message, json));
                return default(T);
            }
        }

        /// <summary>
        /// 将JSON字符串反序列化为匿名对象实体.
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>匿名对象实体</returns>
        public static JObject Deserialize(this string json) => JsonConvert.DeserializeObject(json) as JObject;
    }
}

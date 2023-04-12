using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yu3zx.Json
{
    public class JSONUtil
    {
        #region 序列化，反序列化 - Json String  

        public static string SerializeJSON<T>(T obj)
        {
            //var settings = new JsonSerializerSettings();
            //settings.TypeNameHandling = TypeNameHandling.Objects;//增加接口序列化，可以直接序列化接口和抽象类 2019-01-23
            //return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeJSON<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        #endregion

        #region 序列化，反序列化 - Byte[]  

        public static byte[] SerializeBytes<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        public static T DeSerializeBytes<T>(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            T o = (T)formatter.Deserialize(stream);
            return o;
        }

        #endregion

        #region 序列化，反序列化 - MemoryStream  

        public static MemoryStream SerializeStream<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream;
        }

        public static T DeSerializeStream<T>(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            T o = (T)formatter.Deserialize(stream);
            return o;
        }

        public static T DeSerializeStreamFile<T>(string memFile)
        {
            MemoryStream stream = ReadFileToMemoryStream(memFile);
            return DeSerializeStream<T>(stream);
        }

        public static void SerializeStreamFile<T>(T obj, string memFile)
        {
            MemoryStream stream = SerializeStream<T>(obj);
            WriteMemoryStreamToFile(stream, memFile);
        }

        private static void WriteMemoryStreamToFile(MemoryStream stream, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.WriteTo(fs);
            }
        }

        private static MemoryStream ReadFileToMemoryStream(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                MemoryStream ms = new MemoryStream(bytes);
                return ms;
            }
        }

        #endregion

        /// <summary>
        /// 获取未知对象的属性值
        /// </summary>
        /// <param name="resultStr"></param>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public static string GetValue(string resultStr,string tagname)
        {
            Newtonsoft.Json.Linq.JObject resultObject = Newtonsoft.Json.Linq.JObject.Parse(resultStr);
            if(resultObject == null)
            {
                return "";
            }
            if (((IDictionary<string, JToken>)resultObject).ContainsKey(tagname))
            {
                return resultObject.GetValue(tagname).ToString();
            }
            else
            {
                return "";
            }

            //if (resultObject.ContainsKey(tagname)) //可能存在未有ContainsKey属性现象
            //    return resultObject.GetValue(tagname).ToString();
            //else
            //    return "";
        }

        public static JObject Readjson(string jsonfile)
        {
            JObject jObj = null;
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    jObj = (JObject)JToken.ReadFrom(reader);
                }
            }
            return jObj;
        }
    }

    /// <summary>
    /// JSON日期序列化处理类
    /// --使用方法如下
    /// [JsonConverter(typeof(ChinaDateTimeConverter))]
    /// public DateTime Birthday { get; set; }
    /// </summary>
    public class ChinaDateTimeConverter : DateTimeConverterBase
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

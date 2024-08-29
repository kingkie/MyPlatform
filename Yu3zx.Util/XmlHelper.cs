using System.Xml;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Web;
using System.Globalization;
using System.Text;
using System;


namespace Yu3zx.Util
{
    public class XmlHelper
    {
        /// <summary>
        /// 反序列化XML为类实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlObj"></param>
        /// <returns></returns>
        public static T DeserializeXML<T>(string xmlObj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T)); 
            //object convertedObject = null;//
            using (StringReader reader = new StringReader(xmlObj))
            {
                //convertedObject = serializer.Deserialize(reader);//
                //reader.Close();//
                return (T)serializer.Deserialize(reader);
                //return convertedObject;//
            }
        }

        /// <summary>
        /// 序列化类实例为XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeXML<T>(T obj)
        {
            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(obj.GetType()).Serialize((TextWriter)writer, obj);
                return writer.ToString();
            }
        }

        /// </summary>
        /// 替换低位无法打印字符集
        /// <param name="tmp">处理的字符串</param>
        /// <returns></returns>
        public static string ReplaceLowOrderASCIICharacters(string tmp)
        {
            StringBuilder info = new StringBuilder();
            foreach (char cc in tmp)
            {
                int ss = (int)cc;
                if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                    info.AppendFormat("&#x{0:X};", ss);
                else info.Append(cc);
            }
            return info.ToString();
        }
        /// </summary>
        /// 处理低位字符集
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetLowOrderASCIICharacters(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            int pos, startIndex = 0, len = input.Length;
            if (len <= 4) return input;
            StringBuilder result = new StringBuilder();
            while ((pos = input.IndexOf("&#x", startIndex)) >= 0)
            {
                bool needReplace = false;
                string rOldV = string.Empty, rNewV = string.Empty;
                int le = (len - pos < 6) ? len - pos : 6;
                int p = input.IndexOf(";", pos, le);
                if (p >= 0)
                {
                    rOldV = input.Substring(pos, p - pos + 1);
                    // 计算 对应的低位字符
                    short ss;
                    if (short.TryParse(rOldV.Substring(3, p - pos - 3), NumberStyles.AllowHexSpecifier, null, out ss))
                    {
                        if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                        {
                            needReplace = true;
                            rNewV = Convert.ToChar(ss).ToString();
                        }
                    }
                    pos = p + 1;
                }
                else pos += le;
                string part = input.Substring(startIndex, pos - startIndex);
                if (needReplace) result.Append(part.Replace(rOldV, rNewV));
                else result.Append(part);
                startIndex = pos;
            }
            result.Append(input.Substring(startIndex));
            return result.ToString();
        }

        //====-=-=-=-=--=-=-=-=-=止-=-=-==-=-=-=-=-=---

        #region DataTableToXml
        /// <summary>
        /// 将DataTable对象转换成XML字符串
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <returns>XML字符串</returns>
        public static string DataTableToXml(DataTable dt, string sName)
        {
            if (dt != null)
            {
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, System.Text.Encoding.Unicode);
                    //获取ds中的数据
                    dt.TableName = sName;
                    dt.WriteXml(XmlWt, XmlWriteMode.WriteSchema);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    System.Text.UnicodeEncoding ucode = new System.Text.UnicodeEncoding();
                    string returnValue = ucode.GetString(temp).Trim();
                    return returnValue;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region Xml To DataSet
        public static DataSet XmlToDataSet(string xmlString)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlString);
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmldoc.InnerXml);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                reader.Close();
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                reader.Close();
                throw ex;
            }
        }
        #endregion
    }
}

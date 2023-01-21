using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Yu3zx.Json
{
    public class VarJsonConfig
    {
        private static readonly string filename = ConstJsonConfig.ConfigPath + "Var.json";

        private static VarJsonConfig _instance = null;
        private static readonly object _synObject = new object();
        private static JObject jOb = null;
        public VarJsonConfig()
        {
            LoadVarConfig();
        }

        /// <summary>
        ///单例
        /// </summary>
        public static VarJsonConfig Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (_synObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new VarJsonConfig();
                        }
                    }
                }
                return _instance;
            }
        }

        private void LoadVarConfig()
        {
            TextWriter textWriter = null;
            try
            {
                if (!File.Exists(filename))
                {
                    textWriter = File.CreateText(filename);
                    string json = "{}";
                    textWriter.Write(json);
                    textWriter.Close();
                    textWriter = null;
                }
                jOb = JSONUtil.Readjson(filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("加载文件：" + filename + "异常，" + ex.Message);
            }
            finally
            {
                if (textWriter != null)
                    textWriter.Close();
            }
        }

        public string GetValue(string tagname)
        {
            if (jOb == null)
                jOb = new JObject();
            if (jOb.ContainsKey(tagname))
                return jOb.GetValue(tagname).ToString();
            else
                return "";
        }

        public  void SetValue(string tagname,object tagvalue)
        {
            if (jOb == null)
                jOb = new JObject();
            {
                if (jOb.ContainsKey(tagname))
                    jOb[tagname] = tagvalue.ToString();
                else
                {
                    jOb.Add(new JProperty(tagname, tagvalue));
                }
            }
            Save();
        }

        public void Save()
        {
            try
            {
                //using (TextWriter textWriter = File.CreateText(filename))
                //{
                //    string json = jOb.ToString();
                //    textWriter.Write(json);
                //    textWriter.Close();
                //}
                using (TextWriter textWriter = new StreamWriter(filename,false))
                {
                    string json = jOb.ToString();
                    textWriter.Write(json);
                    textWriter.Close();
                }
                //string json = JSONUtil.SerializeJSON(jOb);
            }
            catch (Exception ex)
            {
                Console.WriteLine("保存文件：" + filename + "异常，" + ex.Message);
            }
        }
    }
}

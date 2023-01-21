using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using Yu3zx.Json;
using System.Windows.Forms;

namespace Yu3zx.Devices.Common
{
    public class PipesManager : IDisposable
    {
        #region  单例
        private static object syncObj = new object();
        private static PipesManager instance = null;
        public static PipesManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = new PipesManager();
                }
            }
            return instance;
        }

        PipesManager()
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
                    string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\Pipes.json";
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

        private static PipesManager Read()
        {
            string filepathstring = Path.Combine(Application.StartupPath, "Config") + "\\Pipes.json";
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
                    PipesManager instancetmp = serializer.Deserialize<PipesManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new PipesManager();
                }
            }
        }

        #endregion End

        public List<Pipe> Pipes = new List<Pipe>();

        public void AddPipe(Pipe _pipe)
        {
            if (_pipe != null && _pipe.PipeId != "")
            {
            }
            else
            {
                MessageBox.Show("增加的通道及其ID不能为空！");
                return;
            }
            Pipe pipe = GetPipeById(_pipe.PipeId);
            if (pipe != null)
            {
                MessageBox.Show("已存在此通道");
            }
            else
            {
                Pipes.Add(_pipe);
            }
        }

        public void RemovePipe(string pipeid)
        {
            Pipe pipe = GetPipeById(pipeid);
            if (pipe != null)
            {
                Pipes.Remove(pipe);
            }
        }

        /// <summary>
        /// 通过通道ID查找通道
        /// </summary>
        /// <param name="pipeid"></param>
        /// <returns></returns>
        public Pipe GetPipeById(string pipeid)
        {
            return Pipes.Find(x => x.PipeId == pipeid);
        }


        /// <summary>
        /// 打开所有通道
        /// </summary>
        public void OpenAllPipes()
        {
            foreach (Pipe pipe in Pipes)
            {
                try
                {
                    pipe.PipeOpen();
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 关闭所有通道
        /// </summary>
        public void CloseAllPipes()
        {
            foreach (Pipe pipe in Pipes)
            {
                try
                {
                    pipe.PipeClose();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 打开某个通道
        /// </summary>
        /// <param name="_pipeid"></param>
        /// <returns></returns>
        public bool OpenPipe(string _pipeid)
        {
            Pipe pipe = GetPipeById(_pipeid);
            if (pipe != null)
            {
                return  pipe.PipeOpen();
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭某个通道
        /// </summary>
        /// <param name="_pipeid"></param>
        /// <returns></returns>
        public bool ClosePipe(string _pipeid)
        {
            Pipe pipe = GetPipeById(_pipeid);
            if (pipe != null)
            {
                return pipe.PipeClose();
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            foreach (Pipe pipe in Pipes)
            {
                try
                {
                    pipe.PipeClose();
                }
                catch
                {
                }
            }
            lock (syncObj)
            {
                instance = null;
            }
        }
    }
}

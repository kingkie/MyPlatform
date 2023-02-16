using LuaInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yu3zx.LuaScript
{
    public delegate void LuaRunErrorHandle(string errmsg);
    public class LuaScriptHelper
    {
        #region 单例
        private static LuaScriptHelper instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static LuaScriptHelper CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new LuaScriptHelper();
                }
            }
            return instance;
        }

        #endregion End

        public event LuaRunErrorHandle LuaRunError;//报错的回调

        /// <summary>
        /// LUA全局执行对象
        /// </summary>
        private Lua LuaHelper = new Lua();

        private CancellationTokenSource tokenSource = null;
        /// <summary>
        /// 初始化运行环境
        /// </summary>
        public void LuaInit()
        {
            try
            {
                string strDirectory = SysApis.GetDirectoryString();
                //string s = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName; //模块地址
                string sss = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                if (!Directory.Exists(SysApis.GetDirectoryString() + "\\CoreScript"))
                {
                    Directory.CreateDirectory(SysApis.GetDirectoryString() + "\\CoreScript");
                }
                CreateFile("Yu3zx.LuaScript.DefaultScripts.CoreScript.head.lua", strDirectory + "/CoreScript/head.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.CoreScript.JSON.lua", strDirectory + "/CoreScript/JSON.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.CoreScript.log.lua", strDirectory + "/CoreScript/log.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.CoreScript.once.lua", strDirectory + "/CoreScript/once.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.CoreScript.strings.lua", strDirectory + "/CoreScript/strings.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.CoreScript.sys.lua", strDirectory + "/CoreScript/sys.lua", false);

                CreateFile("Yu3zx.LuaScript.DefaultScripts.CoreScript.example.lua", strDirectory + "/CoreScript/example.lua", false);

                if (!Directory.Exists(SysApis.GetDirectoryString() + "\\logs"))
                {
                    Directory.CreateDirectory(SysApis.GetDirectoryString() + "\\logs");
                }

                if (!Directory.Exists(SysApis.GetDirectoryString() + "\\UsersScripts"))
                {
                    Directory.CreateDirectory(SysApis.GetDirectoryString() + "\\UsersScripts");
                }

                CreateFile("Yu3zx.LuaScript.DefaultScripts.UsersScripts.example.lua", strDirectory + "/UsersScripts/example.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.UsersScripts.AT控制TCP连接-快发模式.lua", strDirectory + "/UsersScripts/AT控制TCP连接-快发模式.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.UsersScripts.AT控制TCP连接-慢发模式.lua", strDirectory + "/UsersScripts/AT控制TCP连接-慢发模式.lua", false);
                CreateFile("Yu3zx.LuaScript.DefaultScripts.UsersScripts.循环发送快捷发送区数据.lua", strDirectory + "/UsersScripts/循环发送快捷发送区数据.lua", false);

                if (!Directory.Exists(strDirectory + "\\user_script_send_convert"))
                {
                    Directory.CreateDirectory(strDirectory + "\\user_script_send_convert");
                }
                CreateFile("Yu3zx.LuaScript.DefaultScripts.user_script_send_convert.16进制数据.lua", strDirectory + "/user_script_send_convert/16进制数据.lua");
                CreateFile("Yu3zx.LuaScript.DefaultScripts.user_script_send_convert.GPS NMEA.lua", strDirectory + "/user_script_send_convert/GPS NMEA.lua");
                CreateFile("Yu3zx.LuaScript.DefaultScripts.user_script_send_convert.加上换行回车.lua", strDirectory + "/user_script_send_convert/加上换行回车.lua");
                CreateFile("Yu3zx.LuaScript.DefaultScripts.user_script_send_convert.解析换行回车的转义字符.lua", strDirectory + "/user_script_send_convert/解析换行回车的转义字符.lua");
                CreateFile("Yu3zx.LuaScript.DefaultScripts.user_script_send_convert.默认.lua", strDirectory + "/user_script_send_convert/默认.lua");

                CreateFile("Yu3zx.LuaScript.DefaultScripts.LICENSE", strDirectory + "\\LICENSE", false);
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show("生成文件结构失败。\r\n错误信息：" + e.Message);
                LuaRunError?.Invoke(e.Message);
            }

            //注册静态方法
            LuaHelper.RegisterFunction("Utf8ToAsciiHex",null, typeof(SysApis).GetMethod("Utf8ToAsciiHex"));//静态方法
            LuaHelper.RegisterFunction("GetDirectoryString", null, typeof(SysApis).GetMethod("GetDirectoryString"));//静态方法
            LuaHelper.RegisterFunction("PrintLog", null, typeof(SysApis).GetMethod("PrintLog"));//打印日志
            LuaHelper.RegisterFunction("Hex2Byte", null, typeof(SysApis).GetMethod("Hex2Byte"));//静态方法
            LuaHelper.RegisterFunction("Byte2Hex", null, typeof(SysApis).GetMethod("Byte2Hex"));//静态方法
            LuaHelper.RegisterFunction("WriteLine", null, typeof(SysApis).GetMethod("WriteLine"));//静态方法

        }
        /// <summary>
        /// 执行Lua脚本
        /// </summary>
        /// <param name="strPath">脚本文件路径</param>
        public void RunLuaFile(string strPath)
        {
            try
            {
                if (File.Exists(strPath))
                {
                    LuaHelper.DoFile(strPath);
                }
            }
            catch(Exception ex)
            {
                LuaRunError?.Invoke(ex.Message);
            }
        }
        /// <summary>
        /// 重新Lua虚拟机运行脚本文件
        /// </summary>
        /// <param name="strPath">脚本文件路径</param>
        public void NewRunLuaFile(string strPath)
        {
            if (!File.Exists(strPath))
            {
                return;
            }
            try
            {
                using(Lua lua = new Lua())
                {
                    lua.DoFile(strPath);
                }
            }
            catch(Exception ex)
            {
                LuaRunError?.Invoke(ex.Message);
            }
        }
        /// <summary>
        /// 执行单条Lua语句
        /// </summary>
        /// <param name="strScript"></param>
        public object[] RunLuaScript(string strScript)
        {
            try
            {
                object[] rtnObjs = LuaHelper.DoString(strScript);
                return rtnObjs;
            }
            catch(Exception ex)
            {
                LuaRunError?.Invoke(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 新建一个新的lua虚拟机
        /// </summary>
        public void NewLua(string file)
        {
            if (tokenSource != null)
            {
                tokenSource.Dispose();
            }
            tokenSource = new CancellationTokenSource();//task取消指示

            //文件不存在
            if (!File.Exists(file))
            {
                return;
            }
            Lua lua = new Lua();
            Task.Run(() =>
            {
                try
                {
                    lua.DoString($"require '{file.Replace("/", ".").Substring(0, file.Length - 4)}'");
                }
                catch (Exception ex)
                {
                    LuaRunError?.Invoke(ex.Message);
                }
            }, tokenSource.Token);
        }

        /// <summary>
        /// 取出文件
        /// </summary>
        /// <param name="insidePath">软件内部的路径</param>
        /// <param name="outPath">需要释放到的路径</param>
        /// <param name="d">是否覆盖</param>
        private void CreateFile(string insidePath, string outPath, bool d = true)
        {
            if (!File.Exists(outPath) || d)
            {
                File.WriteAllBytes(outPath, GetFileContent(insidePath));
            }
        }

        /// <summary>
        /// 读取软件资源文件内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>内容字节数组</returns>
        private byte[] GetAssetsFileContent(string path)
        {
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            var source = System.Windows.Application.GetResourceStream(uri).Stream;
            byte[] f = new byte[source.Length];
            source.Read(f, 0, (int)source.Length);
            return f;
        }

        /// <summary>
        /// 读取软件资源文件内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>内容字节数组</returns>
        private byte[] GetFileContent(string path)
        {
            string sss = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + @"\ConfigText.txt";

            System.Reflection.Assembly appDll = System.Reflection.Assembly.GetCallingAssembly();
            System.IO.Stream stream = appDll.GetManifestResourceStream(path);
            if(stream != null)
            {
                byte[] fileByte = new byte[(int)stream.Length];
                stream.Read(fileByte, 0, (int)stream.Length);
                return fileByte;
            }
            else
            {
                return null;
            }
        }
    }
}

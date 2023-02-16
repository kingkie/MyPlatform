using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yu3zx.LuaScript
{
    /// <summary>
    /// 供Lua调用的系统API函数
    /// </summary>
    public class SysApis
    {
        /// <summary>
        /// utf8编码改为gbk的hex编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Utf8ToAsciiHex(string input)
        {
            return BitConverter.ToString(Encoding.GetEncoding("GB2312").GetBytes(input)).Replace("-", "");
        }

        /// <summary>
        /// 获取当前DLL所在位置
        /// </summary>
        /// <returns></returns>
        public static string GetDirectoryString()
        {
            System.Reflection.Assembly appInformation = System.Reflection.Assembly.GetAssembly(typeof(SysApis));
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(appInformation.Location);
            string strDirectory = fileInfo.DirectoryName;
            return strDirectory;
        }

        /// <summary>
        /// 获取程序运行目录
        /// </summary>
        /// <returns>主程序运行目录（win10商店时返回appdata路径）</returns>
        public static string GetPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Scripts\";
        }

        private static string luaLogFile = string.Empty;
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        public static void PrintLog(string log)
        {
            if (string.IsNullOrEmpty(luaLogFile))
            {
                luaLogFile = GetDirectoryString() + "\\logs\\lua_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".log";
            }

            try
            {
                File.AppendAllText(luaLogFile, DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss:ffff] ") + log + "\r\n");
            }
            catch
            {
            }
        }

        public static void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// hex转byte
        /// </summary>
        /// <param name="mHex">hex值</param>
        /// <returns>原始字符串</returns>
        public static byte[] Hex2Byte(string mHex)
        {
            if(string.IsNullOrEmpty(mHex))
            {
                return null;
            }
            mHex = Regex.Replace(mHex, "[^0-9A-Fa-f]", "");
            if (mHex.Length % 2 != 0)
            {
                mHex = mHex.Remove(mHex.Length - 1, 1);
            }

            if (mHex.Length <= 0) return new byte[0];
            byte[] vBytes = new byte[mHex.Length / 2];
            for (int i = 0; i < mHex.Length; i += 2)
            {
                if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            }
            return vBytes;
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public static Encoding GetEncoding() => Encoding.GetEncoding(65001);//

        /// <summary>
        /// hex值转字符串
        /// </summary>
        /// <param name="mHex">hex值</param>
        /// <returns>原始字符串</returns>
        public static string Hex2String(string mHex)
        {
            mHex = Regex.Replace(mHex, "[^0-9A-Fa-f]", "");
            if (mHex.Length % 2 != 0)
                mHex = mHex.Remove(mHex.Length - 1, 1);
            if (mHex.Length <= 0) return "";
            byte[] vBytes = new byte[mHex.Length / 2];
            for (int i = 0; i < mHex.Length; i += 2)
                if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            return GetEncoding().GetString(vBytes);
        }

        /// <summary>
        /// 字符串转hex值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="space">间隔符号</param>
        /// <returns>结果</returns>
        public static string String2Hex(string str, string space)
        {
            return BitConverter.ToString(GetEncoding().GetBytes(str)).Replace("-", space);
        }

        /// <summary>
        /// byte转string
        /// </summary>
        /// <param name="mHex"></param>
        /// <returns></returns>
        public static string Byte2String(byte[] vBytes)
        {
            var br = from e in vBytes
                     where e != 0
                     select e;
            return GetEncoding().GetString(br.ToArray());
        }

        public static string Byte2Hex(byte[] d, string s = "")
        {
            return BitConverter.ToString(d).Replace("-", s);
        }

    }
}

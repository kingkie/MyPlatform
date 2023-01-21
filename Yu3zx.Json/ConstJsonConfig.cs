using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.Json
{
    public class ConstJsonConfig
    {
        private static string appDir = AppDomain.CurrentDomain.BaseDirectory;

        public static string GetPath(string type)
        {
            return Path.Combine(appDir, type) + "\\";
        }

        public static string ConfigPath
        {
            get { return GetPath("Config"); }
        }

        public static string ImgPathPath
        {
            get { return GetPath("img"); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Yu3zx.Util
{
    public class RegistryHelper
    {
        #region 获取注册本键值
        /// <summary>
        /// 获取CurrentUser下的某键值
        /// </summary>
        /// <param name="keyPath">@"Software\Microsoft\Internet   Explorer\Main"</param>
        /// <param name="keyName">"Window   Title"</param>
        /// <returns></returns>
        public string GetCurrentUserKeyValue(string keyPath, string keyName)
        {
            try
            {
                RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyPath);
                return Key.GetValue(keyName).ToString(); 
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 获取LocalMachine下的某键值
        /// </summary>
        /// <param name="keyPath">@"Software\Microsoft\Internet   Explorer\Main"</param>
        /// <param name="keyName">"Window   Title"</param>
        /// <returns></returns>
        public string GetLocalMachineKeyValue(string keyPath, string keyName)
        {
            try
            {
                RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(keyPath);
                return Key.GetValue(keyName).ToString();
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 获取ClassesRoot下的某键值
        /// </summary>
        /// <param name="keyPath">@"Software\Microsoft\Internet   Explorer\Main"</param>
        /// <param name="keyName">"Window   Title"</param>
        /// <returns></returns>
        public string GetClassesRootKeyValue(string keyPath, string keyName)
        {
            try
            {
                RegistryKey Key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(keyPath);
                return Key.GetValue(keyName).ToString();
            }
            catch
            {
                return "";
            }

        }
        #endregion End

        #region 设置注册表键值

        /// <summary>
        /// 获取CurrentUser下的某键值
        /// </summary>
        /// <param name="keyPath">@"Software\Microsoft\Internet   Explorer\Main"</param>
        /// <param name="keyName">"Window   Title"</param>
        /// <param name="value">设置的值</param>
        /// <param name="vType">设置值的类型</param>
        /// <returns></returns>
        public bool SetCurrentUserKeyValue(string keyPath, string keyName,object value,RegistryValueKind vType)
        {
            try
            {
                RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyPath);
                Key.SetValue(keyName, value, vType);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 获取LocalMachine下的某键值
        /// </summary>
        /// <param name="keyPath">@"Software\Microsoft\Internet   Explorer\Main"</param>
        /// <param name="keyName">"Window   Title"</param>
        /// <param name="value">设置的值</param>
        /// <param name="vType">设置值的类型</param>
        /// <returns></returns>
        public bool SetLocalMachineKeyValue(string keyPath, string keyName,object value,RegistryValueKind vType)
        {
            try
            {
                RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(keyPath);
                Key.SetValue(keyName, value, vType);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 获取ClassesRoot下的某键值
        /// </summary>
        /// <param name="keyPath">@"Software\Microsoft\Internet   Explorer\Main"</param>
        /// <param name="keyName">"Window   Title"</param>
        /// <param name="value">设置的值</param>
        /// <param name="vType">设置值的类型</param>
        /// <returns></returns>
        public bool SetClassesRootKeyValue(string keyPath, string keyName,object value, RegistryValueKind vType)
        {
            try
            {
                RegistryKey Key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(keyPath);
                Key.SetValue(keyName, value, vType);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion End

        private static bool IsExistKey(string keyName)
        {
            try
            {
                bool _exist = false;
                RegistryKey local = Registry.LocalMachine;
                RegistryKey runs = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null)
                {
                    RegistryKey key2 = local.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName)
                {
                    if (strName.ToUpper() == keyName.ToUpper())
                    {
                        _exist = true;
                        return _exist;
                    }
                }
                return _exist;

            }
            catch
            {
                return false;
            }
        }

        ///isStart--是否开机自启动
        ///exeName--应用程序名
        ///path--应用程序路径
        private static bool SelfRunning(bool isStart, string exeName, string path)
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                if (isStart)//若开机自启动则添加键值对
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames)
                    {
                        if (keyName.ToUpper() == exeName.ToUpper())
                        {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }

            return true;
        }

        private static bool IsStart = true;

        public static bool AutoStartUp()
        {
            var exeName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            return StartAutomaticallyCreate(exeName);
        }

        public static bool AutoStartUpCancel()
        {
            var exeName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            return StartAutomaticallyDel(exeName);
        }


        public static void AutoStartupRegedit()
        {
            string appName = "ScentShow";
            string appFullName = System.IO.Path.Combine(Environment.CurrentDirectory, appName + ".exe");
            if (!IsExistKey(appName) && IsStart)
            {
                SelfRunning(IsStart, appName, appFullName);
            }
            else if (IsExistKey(appName) && !IsStart)
            {
                SelfRunning(!IsStart, appName, appFullName);
            }
        }
        public static void AutoStartupLink()
        {
            var exeName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();

            //var r = StartAutomaticallyCreate(exeName);
            //var r = StartAutomaticallyDel(exeName);
            //Console.WriteLine("AutoStartupLink = " + r);
        }

        #region 开机自启
        /// <summary>
        /// 开机自启创建
        /// </summary>
        /// <param name="exeName">程序名称</param>
        /// <returns></returns>
        public static bool StartAutomaticallyCreate(string exeName)
        {
            try
            {
                var startupPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                 exeName + ".lnk");
                var shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                var shortcut = shell.CreateShortcut(startupPath);

                //设置快捷方式的目标所在的位置(源程序完整路径) 
                shortcut.TargetPath = System.Windows.Forms.Application.ExecutablePath;
                //应用程序的工作目录 
                //当用户没有指定一个具体的目录时，快捷方式的目标应用程序将使用该属性所指定的目录来装载或保存文件。 
                shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
                //目标应用程序窗口类型(1.Normal window普通窗口,3.Maximized最大化窗口,7.Minimized最小化) 
                shortcut.WindowStyle = 1;
                //快捷方式的描述 
                shortcut.Description = exeName + "_Ink";
                //设置快捷键(如果有必要的话.) 
                //shortcut.Hotkey = "CTRL+ALT+D"; 
                shortcut.Save();
                return true;
            }
            catch (Exception) { }
            return false;
        }
        /// <summary>
        /// 开机自启删除
        /// </summary>
        /// <param name="exeName">程序名称</param>
        /// <returns></returns>
        public static bool StartAutomaticallyDel(string exeName)
        {
            try
            {
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk");
                return true;
            }
            catch (Exception) { }
            return false;
        }
        #endregion
    }
}

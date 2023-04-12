using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Yu3zx.NetBase
{
    /// <summary>
    /// FTP服务类
    /// </summary>
    public class FtpService
    {
        private static string FTPCONSTR;//FTP的服务器地址，格式为ftp://192.168.1.234:8021/。ip地址和端口换成自己的，这些建议写在配置文件中，方便修改
        private static string FTPUSERNAME;//FTP服务器的用户名
        private static string FTPPASSWORD;//FTP服务器的密码

        static FtpService()
        {
        }
        /// <summary>
        /// FTP的服务器地址，格式为ftp://192.168.1.234:8021/。ip地址和端口换成自己的，这些建议写在配置文件中，方便修改
        /// </summary>
        public string FtpAddress
        {
            get
            {
                return FTPCONSTR;
            }
            set
            {
                FTPCONSTR = value;
            }
        }
        /// <summary>
        /// FTP服务器的用户名
        /// </summary>
        public string FtpUserName
        {
            get
            {
                return FTPUSERNAME;
            }
            set
            {
                FTPUSERNAME = value;
            }
        }
        /// <summary>
        /// FTP服务器的密码
        /// </summary>
        public string FtpPwd
        {
            get
            {
                return FTPPASSWORD;
            }
            set
            {
                FTPPASSWORD = value;
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ftpPath">FTP地址</param>
        /// <param name="localFile">本地文件</param>
        /// <returns>操作结果</returns>
        public static bool FtpUpload(string ftpPath, string localFile)
        {
            FileStream fs = null;
            FtpWebRequest req = null;
            try
            {
                FileInfo fi = new FileInfo(localFile);
                if (fi.Exists)
                {
                    //检查目录是否存在，不存在创建  
                    FtpCheckDirectoryExist(ftpPath + fi.Name);
                    // FileStream fs = fi.OpenRead();
                    fs = new FileStream(localFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                    long length = fs.Length;
                    req = (FtpWebRequest)WebRequest.Create(FTPCONSTR + ftpPath + fi.Name);
                    req.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                    req.Method = WebRequestMethods.Ftp.UploadFile;
                    req.ContentLength = length;
                    req.Timeout = 10 * 1000;

                    Stream stream = req.GetRequestStream();
                    int BufferLength = 2048; //2K     
                    byte[] b = new byte[BufferLength];
                    int i;
                    while ((i = fs.Read(b, 0, BufferLength)) > 0)
                    {
                        stream.Write(b, 0, i);
                    }
                    stream.Close();
                    stream.Dispose();


                    Console.WriteLine("FTP：文件{0}，上传到{1}", localFile, ftpPath);
                }
                else
                {
                    Console.WriteLine("FTP上传失败，文件不存在，" + fi.FullName);
                }
            }
            catch (Exception e)
            {
                // ErrLog(e.Message + e.StackTrace);
                Console.WriteLine("FTP上传出现异常，" + e.ToString());
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }

                if (req != null)
                {
                    req.Abort();
                }
            }
            //req.Abort();
            return true;
        }

        /// <summary>
        /// 判断文件的目录是否存,不存则创建
        /// </summary>
        /// <param name="destFilePath">目录地址</param>
        public static void FtpCheckDirectoryExist(string destFilePath)
        {
            string fullDir = FtpParseDirectory(destFilePath);
            string[] dirs = fullDir.Split('/');
            string curDir = "/";
            for (int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i];
                //如果是以/开始的路径,第一个为空
                if (dir != null && dir.Length > 0)
                {
                    try
                    {
                        curDir += dir + "/";
                        FtpMakeDir(curDir);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FTP检查目录失败:" + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 获取父目录
        /// </summary>
        /// <param name="destFilePath">当前目录</param>
        /// <returns>目录地址</returns>
        public static string FtpParseDirectory(string destFilePath)
        {
            return destFilePath.Substring(0, destFilePath.LastIndexOf("/"));
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="localFile">目录</param>
        /// <returns>操作结果</returns>
        public static bool FtpMakeDir(string localFile)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(FTPCONSTR + localFile);
            req.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                //RH.Util.Log.Logger.Debug("FTP创建目录失败:" + ex.Message + FTPCONSTR + localFile);
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }

        /// <summary>
        /// 下载指定文件夹下的所有文件及子文件夹和文件
        /// </summary>
        /// <param name="serverPath">服务端路径</param>
        /// <param name="localPath">本地路径</param>
        /// <returns>是否下载成功</returns>
        public static bool DownLoadFtpFileAll(string serverPath, string localPath)
        {
            try
            {
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                //ServerPath = FTPCONSTR + ServerPath;

                //1.获取文件夹下所有的文件并下载下来
                List<string> FileList = GetDirctory(serverPath);
                foreach (string fileName in FileList)
                {
                    DownLoad(localPath + "/" + fileName, serverPath + "/" + fileName);
                }
                //2.获取文件夹下所有的子文件夹并遍历下载下来子文件夹中的文件
                FileList = GetDirctory(serverPath, WebRequestMethods.Ftp.ListDirectoryDetails);
                foreach (string fileName in FileList)
                {
                    DownLoadFtpFileAll(serverPath + "/" + fileName, localPath + "/" + fileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取服务器指定文件夹下指定的文件、文件夹列表
        /// </summary>
        /// <param name="serverPath">服务器文件夹路径</param>
        /// <param name="fileType">文件：WebRequestMethods.Ftp.ListDirectory 文件夹：WebRequestMethods.Ftp.ListDirectoryDetails</param>
        /// <returns>文件列表</returns>
        public static List<string> GetDirctory(string serverPath, string fileType = WebRequestMethods.Ftp.ListDirectory)
        {
            List<string> strs = new List<string>();
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPCONSTR + serverPath));
                reqFTP.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                reqFTP.Method = fileType;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default); //中文文件名

                string line = reader.ReadLine();
                while (line != null)
                {
                    switch (fileType)
                    {
                        case WebRequestMethods.Ftp.ListDirectoryDetails:
                            if (line.Contains("drw"))
                            {
                                string msg = line.Substring(line.LastIndexOf(" ")).Trim();
                                if (msg != "." && msg != "..")
                                {
                                    strs.Add(msg);
                                }
                            }
                            break;
                        case WebRequestMethods.Ftp.ListDirectory:
                            strs.Add(line);
                            break;
                        default:
                            break;
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return strs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取ftp文件目录出错：" + ex.Message);
            }
            return strs;
        }

        private static bool DownLoad(string localPath, string lerverPath)
        {
            bool result = false;
            using (FileStream fs = new FileStream(localPath, FileMode.Create)) //创建或打开本地文件
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(FTPCONSTR + lerverPath));
                request.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    fs.Position = fs.Length;
                    byte[] buffer = new byte[4096];//4K
                    int count = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        fs.Write(buffer, 0, count);
                        count = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                    }
                    response.GetResponseStream().Close();
                }
                result = true;
            }
            return result;
        }
    }
}

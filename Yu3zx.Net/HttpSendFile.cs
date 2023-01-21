using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Yu3zx.NetBase
{
    public class HttpSendFile
    {
        public static string PostTarFile(string url, string filepath)
        {
            string result = "";
            try
            {
                //HttpPostedFile myFile = file1.PostedFile;
                //将文件转换成字节形式
                Guid guidSplit = Guid.NewGuid();
                string sSplitStr = "--" + guidSplit.ToString() + "--";
                string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                byte[] splitbyte = StrToByte(sSplitStr, "UTF-8");
                FileStream fs = new FileStream(filepath, FileMode.Open);
                //获取文件大小
                string hInfo = @"--" + guidSplit.ToString() + "\r\n" + "Content-Disposition:attachment; filename=\"" + filename + "\"\r\n" +
                               "Content-Type:application/x-tar\r\n" + @"--" + guidSplit.ToString() + "\r\n";
                byte[] headinfo = StrToByte(hInfo, "UTF-8");

                long size = fs.Length + splitbyte.Length + headinfo.Length;//增加分隔符数据

                byte[] fileByte = new byte[size];  //将文件读到byte数组中

                byte[] fileBytetmp = new byte[fs.Length];
                headinfo.CopyTo(fileByte, 0);
                fs.Read(fileBytetmp, 0, fileBytetmp.Length);
                fs.Close();
                fileBytetmp.CopyTo(fileByte, headinfo.Length);

                splitbyte.CopyTo(fileByte, size - splitbyte.Length);

                string postUrl = url;
                try
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    webClient.Headers.Add("Content-Type", "multipart/form-data");//;multipart/form-databoundary=" + guidSplit.ToString()
                    webClient.Headers.Add("boundary", guidSplit.ToString());
                    webClient.Headers.Add("Charset", "UTF-8");
                    webClient.Headers.Add("Content-Length", size.ToString());
                    byte[] responseArray = webClient.UploadData(postUrl, "POST", fileByte);

                    //将返回的字节数据转成字符串（也就是uploadpic.aspx里面的页面输出内容）
                    result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);

                    //返回结果的处理
                    switch (result)
                    {
                        case "-1":
                            // Console.WriteLine("文件上传时发生异常，未提交成功。");
                            result = "文件上传时发生异常，未提交成功。";
                            break;
                        case "0":
                            break;
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = "上传异常！";
            }
            return result;
        }

        public static byte[] StrToByte(string msgstr, string CodeType)
        {
            byte[] byteArry;
            switch (CodeType)
            {
                case "Unicode":
                    byteArry = System.Text.Encoding.Unicode.GetBytes(msgstr);
                    break;
                case "UTF-8":
                    byteArry = System.Text.Encoding.UTF8.GetBytes(msgstr);
                    break;
                default:
                    byteArry = System.Text.Encoding.Default.GetBytes(msgstr);
                    break;
            }
            return byteArry;
        }
    }
}

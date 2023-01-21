using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.ClothBatchNumber
{
    public class HttpRequest
    {
        private static string baseUrl = "https://localhost:44301/api";//

        private static string API_UploadResult = "/v1/rank";
        private static string API_AddDevice = "/v1/equipments";
        private static string API_BuildNative = "/pay/wechat/native";
        private static string API_PayState = "/pay/order";

        private static string API_GetStudent = "/Student";

        /// <summary>
        /// 
        /// </summary>
        public static bool EnableRequestSimulate { get; set; } = true;

        public static string RequestToken { get; set; }

        //v1/question_bank/all

        public static string HttpGet(string url, string method)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + method);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 5000;
            httpWebRequest.Headers["Authorization"] = "Bearer " + RequestToken;

            HttpWebResponse httpWebResponse = null;

            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException wex)
            {
                Console.WriteLine("HttpPost [" + url + "] Error :" + wex.Message);
                if (wex.HResult == -2146233079)
                {
                    string ErrorNetMSG = "{\"meta\": {\"code\": 22, \"message\": \"网络连接异常\", \"data\": []}}";
                    return ErrorNetMSG;
                }
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    httpWebResponse = wex.Response as HttpWebResponse;
                }
                //连接中断
                string ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \"" + wex.Message + "\", \"data\": []}}";
                return ErrorMSG;
            }
            catch (Exception e)
            {
                Console.WriteLine("HttpPost [" + url + "] Error :" + e.Message);
                string ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \"" + e.Message + "\", \"data\": []}}";
                return ErrorMSG;
            }
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();


            httpWebResponse.Close();
            streamReader.Close();

            return responseContent;
        }
        public static string HttpPost(string url, string method, string param, bool jsontype = true)
        {
            //转换输入参数的编码类型，获取bytep[]数组 
            byte[] byteArray = Encoding.UTF8.GetBytes(param);
            //初始化新的webRequst
            //1． 创建httpWebRequest对象
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url + method));
            //2． 初始化HttpWebRequest对象
            webRequest.Method = "POST";
            if(jsontype)
            {
                webRequest.ContentType = "application/json";
            }
            else
            {
                webRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            }

            webRequest.ContentLength = byteArray.Length;
            webRequest.Timeout = 15000;
            //3． 附加要POST给服务器的数据到HttpWebRequest对象(附加POST数据的过程比较特殊，它并没有提供一个属性给用户存取，需要写入HttpWebRequest对象提供的一个stream里面。)

            try
            {
                Stream newStream = webRequest.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse res = wex.Response as HttpWebResponse;
                if(wex.HResult == -2146233079)
                {
                    string ErrorNetMSG = "{\"meta\": {\"code\": 22, \"message\": \"网络连接异常\", \"data\": []}}";
                    return ErrorNetMSG;
                }
                //连接中断
                string ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \""+ wex.Message +"\", \"data\": []}}";
                return ErrorMSG;
            }
            catch (Exception Ex)
            {
                String ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \"" + Ex.Message + "\", \"data\": []}}";
                return ErrorMSG;
            }

            //4． 读取服务器的返回信息
            HttpWebResponse response = null;//= (HttpWebResponse)webRequest.GetResponse();

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine("HttpPost ["+ url + method + "] Error :" + e.Message);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = e.Response as HttpWebResponse;
                }
            }

            StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string phpend = php.ReadToEnd();

            return phpend;
        }

        public static string HttpPUT(string url, string method, string param, bool WithToken = true)
        {
            //转换输入参数的编码类型，获取bytep[]数组 
            byte[] byteArray = Encoding.UTF8.GetBytes(param);
            //初始化新的webRequst
            //1． 创建httpWebRequest对象
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url + method));
            //2． 初始化HttpWebRequest对象
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = byteArray.Length;
            if (WithToken)
                webRequest.Headers["Authorization"] = "Bearer " + RequestToken;
            webRequest.Timeout = 5000;
            //3． 附加要POST给服务器的数据到HttpWebRequest对象(附加POST数据的过程比较特殊，它并没有提供一个属性给用户存取，需要写入HttpWebRequest对象提供的一个stream里面。)

            try
            {
                Stream newStream = webRequest.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse res = wex.Response as HttpWebResponse;
                if (wex.HResult == -2146233079)
                {
                    string ErrorNetMSG = "{\"meta\": {\"code\": 22, \"message\": \"网络连接异常\", \"data\": []}}";
                    return ErrorNetMSG;
                }
                //连接中断
                string ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \"" + wex.Message + "\", \"data\": []}}";
                return ErrorMSG;
            }
            catch (HttpRequestException hex)
            {
                if (typeof(WebException) == hex.InnerException.GetType() && (hex.InnerException as WebException).HResult == -2146233079)
                {
                    //连接中断
                    string ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \"网络连接异常\", \"data\": []}}";
                    return ErrorMSG;
                }
                else
                {
                    string ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \"接口调用异常\", \"data\": []}}";
                    return ErrorMSG;
                }
            }
            catch (Exception Ex)
            {
                String ErrorMSG = "{\"meta\": {\"code\": 22, \"message\": \"" + Ex.Message + "\", \"data\": []}}";
                return ErrorMSG;
            }

            //4． 读取服务器的返回信息
            HttpWebResponse response = null;//= (HttpWebResponse)webRequest.GetResponse();

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine("HttpPost [" + url + method + "] Error :" + e.Message);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = e.Response as HttpWebResponse;
                }
            }

            StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string phpend = php.ReadToEnd();

            return phpend;
        }


        public static BaseResponse<DevInfor> AddDevice(string macStr)
        {
            string paramsstr = string.Format(@"{{'mac':'{0}','on_status':{1}}}", macStr, 1);
            paramsstr = paramsstr.Replace('\'', '\"');
            string response = HttpPost(baseUrl, API_AddDevice, paramsstr);

            if (string.IsNullOrEmpty(response))
                return null;
            var sr = JsonConvert.DeserializeObject<BaseResponse<DevInfor>>(response);
            return sr;
        }

        public static string GetStudent(int stdid)
        {
            string apiStr = string.Format(baseUrl + "/Student/{0}",stdid);
            string response = HttpGet(apiStr, "");
            return response;
        }

        public static string GetAllStudent()
        {
            string apiStr = baseUrl + "/Student";
            string response = HttpGet(apiStr, "");
            return response;
        }

        public static string GetStudentByPost(int stdid)
        {
            //string paramStr = string.Format(@"?id={0}", stdid);
            //string paramStr = string.Format(@"{{'id':{0},'on_status':{1}}}", stdid, 1);
            string paramStr = string.Format(@"{{'StdId':{0},'StudentName':'{1}','Age':{2}}}", stdid, "吴天德", 36);

            string response = HttpPost(baseUrl, API_GetStudent, paramStr, true);
            return response;
        }

        public static string StudentByPost(int stdid)
        {
            string paramStr = string.Format(@"?param1={0}&param2=={1}", stdid,"周小");
            //string paramStr = string.Format(@"{{'id':{0},'on_status':{1}}}", stdid, 1);
            //string paramStr = string.Format(@"{{'StdId':{0},'StudentName':'{1}','Age':{2}}}", stdid, "吴天德", 36);

            string response = HttpPost(baseUrl, API_GetStudent, paramStr, false);
            return response;
        }

        public static T GetT<T>(string content)
        {
            try
            {
                var sr = JsonConvert.DeserializeObject<T>(content);
                return sr;
            }
            catch
            {

            }
            return default(T);
        }

        public static string GetTString(object obj)
        {
            try
            {
                string request = JsonConvert.SerializeObject(obj);
                return request;
            }
            catch
            {

            }
            return string.Empty;
        }
    }


    public class HttpRequestSimulate
    {


    }


    public class Meta {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ReportResult
    {
        /// <summary>
        /// 返回的成绩分享ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ShareID { get; set; }
        /// <summary>
        /// 返回的信息
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    public class DevInfor
    {
        /// <summary>
        /// 设备编号 no
        /// </summary>
        [JsonProperty(PropertyName = "no")]
        public int DevSn { get; set; }
        /// <summary>
        /// 设备ID equipment_id
        /// </summary>
        [JsonProperty(PropertyName = "equipment_id")]
        public int DevId { get; set; }
    }

    /// <summary>
    /// 请求json基础格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 描述成功与否，错误信息
        /// </summary>
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// 描述请求内容
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }

        public bool IsSuccess {
            get {
                return Meta.Code == "200";
            }
        }

        public string ErrorMSG
        {
            get
            {
                return Meta.Message;
            }
        }

        [JsonProperty(PropertyName = "code")]
        public int StateCode
        {
            get;
            set;
        }
        [JsonProperty(PropertyName = "msg")]
        public string Msg
        {
            get;
            set;
        }
    }
}

using System;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using System.Management;

namespace Yu3zx.NetBase
{
    /// <summary>
    /// NetSocket 的摘要说明。
    /// </summary>
    public class NetSocket
	{

		#region 穿过代理服务器获得Ip地址,如果有多个IP，则第一个是用户的真实IP，其余全是代理的IP，用逗号隔开
		public static string getRealIp()
		{
			string UserIP;						
			if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"]!=null)  //得到穿过代理服务器的ip地址
			{   
				
				UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
			}
			else
			{
				UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
			}
			return UserIP;
		}
		#endregion

		#region 获取指定WEB页面
		/// <summary>
		/// 获取指定WEB页面
		/// </summary>
		/// <param name="strurl">URL</param>
		/// <returns>string</returns>
		public static string GetWebUrl(string strurl)
		{
			try
			{
				WebClient MyWebClient = new  WebClient();
				MyWebClient.Credentials = CredentialCache.DefaultCredentials;
				Byte[] pageData = MyWebClient.DownloadData(strurl);
				//string pageHtml = Encoding.UTF8.GetString(pageData);
				string pageHtml=Encoding.Default.GetString(pageData);
				return pageHtml;
			} 
			catch (WebException webEx)
			{
				return "error_GetWebUrl:"+webEx.ToString();
			}
		}
		#endregion

		#region GET方法获取页面
		/// GET方法获取页面
		/// 函数名:GetUrl	
		/// 功能描述:GET方法获取页面	
		/// 处理流程:
		/// 算法描述:
		/// 作 者: 杨栋
		/// 日 期: 2006-11-19 12:00
		/// 修 改: 2007-01-29 17:00 2007-01-29 17:00
		/// 日 期:
		/// 版 本:
		#region GetUrl(String url)
		/// <summary>
		/// GET方法获取页面
		/// </summary>
		/// <param name="url">目标url</param>
		/// <returns></returns>
		public static string GetUrl(String url) 
		{
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "GET";
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				CookieContainer cookieCon = new CookieContainer();	
			
				//				req.Headers.Add("Accept-Encoding", "gzip, deflate"); 
				//				req.Headers.Add("Accept-Language", "zh-cn"); 
				//				req.Headers.Add("UA-CPU", "x86"); 
				//				req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				req.Headers.Add("User-Agent", "Mozilla/4.0"); 
				//				Accept-Language: zh-cn
				req.CookieContainer = cookieCon;
				//				req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				//				outcookieheader = req.CookieContainer.GetCookieHeader(new Uri(url));
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");

				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}
			return strResult;
		}

		#endregion

		#region GetUrl(String url,int timeout)
		/// <summary>
		/// GET方法获取页面
		/// </summary>
		/// <param name="url">目标url</param>
		/// <returns></returns>
		public static string GetUrl(String url,int timeout) 
		{
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "GET";
				req.ContentType = "application/x-www-form-urlencoded";
				req.Timeout=timeout;
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				CookieContainer cookieCon = new CookieContainer();	
			
				//				req.Headers.Add("Accept-Encoding", "gzip, deflate"); 
				//				req.Headers.Add("Accept-Language", "zh-cn"); 
				//				req.Headers.Add("UA-CPU", "x86"); 
				//				req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				req.Headers.Add("User-Agent", "Mozilla/4.0"); 
				//				Accept-Language: zh-cn
				req.CookieContainer = cookieCon;
				//				req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				//				outcookieheader = req.CookieContainer.GetCookieHeader(new Uri(url));
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}
			return strResult;
		}

		#endregion

		#region GetUrl(String url,out string  outcookieheader)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="outcookieheader">输出Cookie</param>
		/// <returns></returns>
		public static string GetUrl(String url,out string  outcookieheader) 
		{
			outcookieheader=string.Empty;
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "GET";
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				CookieContainer cookieCon = new CookieContainer();				
				req.CookieContainer = cookieCon;
				//				req.CookieContainer.SetCookies(new Uri(url),cookieheader);			
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				outcookieheader = req.CookieContainer.GetCookieHeader(new Uri(url));//获得cookie
				if (outcookieheader.Length<2)
				{
					try
					{
						outcookieheader   =   res.Headers["Set-Cookie"]; 
						outcookieheader=outcookieheader.Substring(0,outcookieheader.IndexOf(";"));
					}
					catch
					{
						outcookieheader="";
					}
				}
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}
			return strResult;
		}

		#endregion   

		#region GetUrl(String url,string  cookieheader)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="cookieheader">输入Cookie</param>
		/// <returns></returns>
		public static string GetUrl(String url,string  cookieheader) 
		{
			//			outcookieheader="";
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "GET";
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//
				//				//				string name=cookieheader.Substring(0,cookieheader.IndexOf("="));
				//				//				string key=cookieheader.Substring(cookieheader.IndexOf("=")+1,cookieheader.Length-cookieheader.IndexOf("=")-1);
				//				//				cookieCon.Add(new Uri(url),new Cookie(name,key));
				//				
				//				
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				//				}
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;

					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
				}			
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				//				outcookieheader = req.CookieContainer.GetCookieHeader(new Uri(url));
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}
			return strResult;
		}

		#endregion   

		#region GetUrl(String url,string  cookieheader,bool AutoRedirect)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="cookieheader">输入Cookie</param>
		/// <returns></returns>
		public static string GetUrl(String url,string  cookieheader,bool AutoRedirect) 
		{
			//			outcookieheader="";

			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "GET";
				req.AllowAutoRedirect=AutoRedirect;
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//
				//				//				string name=cookieheader.Substring(0,cookieheader.IndexOf("="));
				//				//				string key=cookieheader.Substring(cookieheader.IndexOf("=")+1,cookieheader.Length-cookieheader.IndexOf("=")-1);
				//				//				cookieCon.Add(new Uri(url),new Cookie(name,key));
				//				
				//				
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				//				}
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;

					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
				}
			
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				//				outcookieheader = req.CookieContainer.GetCookieHeader(new Uri(url));
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}
			return strResult;
		}

		#endregion   

		#region GetUrl(String url,string  cookieheader,out string  outcookieheader,string Header_UserAgent,string http_type)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="cookieheader">输入Cookie</param>
		/// <param name="outcookieheader">输出Cookie</param>
		/// <param name="Header_UserAgent">包头 UserAgent</param>
		/// <param name="http_type"> 请求类型 http https </param>
		/// <returns></returns>
		public static string GetUrl(String url,string  cookieheader,out string  outcookieheader,string Header_UserAgent,string http_type) 
		{
			outcookieheader="";
			if (http_type=="https")
			{
				System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy(); //https 跳过证书
			}
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "GET";
				req.AllowAutoRedirect=false;
				req.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*";
				req.Headers.Add("Accept-Encoding","gzip, deflate");
				req.Headers.Add("Accept-Language","zh-cn");
				req.Headers.Add("Cache-Control","no-cache");
				req.KeepAlive=true;
				req.ContentType="application/x-www-form-urlencoded";
				//req.Referer = "https://esales.16288.com/homepage.aspx";
				req.Headers.Add("UA-CPU","x86");
				req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
				//				CookieContainer cookieCon = new CookieContainer();
				//
				//				string[] ls_cookies = cookieheader.Split(';');
				//				if (ls_cookies.Length <= 1)
				//				{
				//					req.CookieContainer = cookieCon;
				//					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//					{
				//						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				//					}
				//				}
				//				else
				//				{
				//
				//					//////////////////////////////////
				//					
				//					string[] ls_cookie = null;
				//
				//					for(int i=0;i<ls_cookies.Length;i++)
				//					{
				//						ls_cookie = ls_cookies[i].Split('=');
				//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
				//					}				
				//					
				//					req.CookieContainer = cookieCon;
				//
				//					////////////////////////////////////
				//				}
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;
					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
				}
				Stream ReceiveStream = null;
				try
				{
					res = (HttpWebResponse)req.GetResponse();
					ReceiveStream = res.GetResponseStream();
				}
				catch(Exception exp)
				{
                    Console.WriteLine(exp.Message.ToString());
				}
				try
				{
					outcookieheader = req.CookieContainer.GetCookieHeader(new Uri(url));//获得cookie
				}
				catch(Exception exp)
				{
                    Console.WriteLine(exp.Message.ToString());
				}
				if (outcookieheader.Length<2)
				{
					try
					{
						outcookieheader   =   res.Headers["Set-Cookie"]; 
						outcookieheader=outcookieheader.Substring(0,outcookieheader.IndexOf(";"));
					}
					catch(Exception exp)
					{
						string s = exp.Message.ToString();
						outcookieheader="";
					}
				}
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");

				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}
			return strResult;
		}

		#endregion   

		#endregion

		#region POST方法获取页面
		/// POST方法获取页面
		/// 函数名:PostUrl	
		/// 功能描述:POST方法获取页面	
		/// 处理流程:
		/// 算法描述:
		/// 作 者: 杨栋
		/// 日 期: 2006-11-19 12:00
		/// 修 改: 2007-01-29 17:00
		/// 日 期:
		/// 版 本:
		#region PostUrl(String url, String paramList)
		/// <summary>
		/// POST方法获取页面
		/// </summary>
		/// <param name="url"></param>
		/// <param name="paramList">格式: a=xxx&b=xxx&c=xxx</param>
		/// <returns></returns>
		public static string PostUrlPara(String url, String paramList)
		{
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				req.KeepAlive = true;
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				CookieContainer cookieCon = new CookieContainer();
				req.CookieContainer = cookieCon;
				StringBuilder UrlEncoded = new StringBuilder();
				Char[] reserved = {'?', '=', '&'};
				byte[] SomeBytes = null;
				if (paramList != null) 
				{
					int i=0, j;
					while(i<paramList.Length)
					{
						j=paramList.IndexOfAny(reserved, i);
						if (j==-1)
						{
							//							UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length-i)));
							UrlEncoded.Append((paramList.Substring(i, paramList.Length-i)));
							break;
						}
						//						UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j-i)));
						UrlEncoded.Append((paramList.Substring(i, j-i)));
						UrlEncoded.Append(paramList.Substring(j,1));
						i = j+1;
					}
					SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
					req.ContentLength = SomeBytes.Length;
					Stream newStream = req.GetRequestStream();
					newStream.Write(SomeBytes, 0, SomeBytes.Length);
					newStream.Close();
				} 
				else 
				{
					req.ContentLength = 0;
				}
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}			
			return strResult;
		}

        /// <summary>
        /// 发送JSON对象
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="JSONData"></param>
        /// <returns></returns>
        public static string PostUrl(string Url, string JSONData)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                //request.ContentType = "text/json";
                request.ContentType = "application/json";
                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);

                //声明一个HttpWebRequest请求  
                //request.Timeout = 90000;  
                //设置连接超时时间  
                //request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.UTF8;

                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                string strResult = streamReader.ReadToEnd();
                streamReceive.Dispose();
                streamReader.Dispose();

                return strResult;
            }
            catch
            {
                return "";
            }

        }

		#endregion

		#region PostUrl(String url, String paramList,string cookieheader,string Header_Referer)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="paramList">格式: a=xxx&b=xxx&c=xxx</param>
		/// <param name="cookieheader">输入cookie</param>
		/// <param name="Header_Referer">包头 Referer</param>
		/// <returns></returns>
		public static string PostUrl(String url, String paramList,string cookieheader,string Header_Referer ) 
		{	
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				if (Header_Referer.Length>1)
				{
					req.Referer=Header_Referer;
				}
				req.KeepAlive = true;
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);
				//				}
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					string[] ls_cookie = null;
					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
				}
				StringBuilder UrlEncoded = new StringBuilder();
				Char[] reserved = {'?', '=', '&'};
				byte[] SomeBytes = null;
				if (paramList != null) 
				{
					int i=0, j;
					while(i<paramList.Length)
					{
						j=paramList.IndexOfAny(reserved, i);
						if (j==-1)
						{
							//							UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length-i)));
							UrlEncoded.Append((paramList.Substring(i, paramList.Length-i)));
							break;
						}
						//						UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j-i)));
						UrlEncoded.Append((paramList.Substring(i, j-i)));
						UrlEncoded.Append(paramList.Substring(j,1));
						i = j+1;
					}
					SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
					req.ContentLength = SomeBytes.Length;
					Stream newStream = req.GetRequestStream();
					newStream.Write(SomeBytes, 0, SomeBytes.Length);
					newStream.Close();
				} 
				else 
				{
					req.ContentLength = 0;
				}
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");

				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}			
			return strResult;
		}

		#endregion

		#region PostUrl(String url, String paramList,out string outcookieheader)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="paramList">格式: a=xxx&b=xxx&c=xxx</param>
		/// <param name="outcookieheader">输出cookie</param>
		/// <returns></returns>
		public static string PostUrl(String url, String paramList,out string outcookieheader) 
		{
			outcookieheader=string.Empty;
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				req.KeepAlive = true;
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				CookieContainer cookieCon = new CookieContainer();
				req.CookieContainer = cookieCon;				
				StringBuilder UrlEncoded = new StringBuilder();
				Char[] reserved = {'?', '=', '&'};
				byte[] SomeBytes = null;
				if (paramList != null) 
				{
					int i=0, j;
					while(i<paramList.Length)
					{
						j=paramList.IndexOfAny(reserved, i);
						if (j==-1)
						{
							//							UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length-i)));
							UrlEncoded.Append((paramList.Substring(i, paramList.Length-i)));
							break;
						}
						//						UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j-i)));
						UrlEncoded.Append((paramList.Substring(i, j-i)));
						UrlEncoded.Append(paramList.Substring(j,1));
						i = j+1;
					}
					SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
					req.ContentLength = SomeBytes.Length;
					Stream newStream = req.GetRequestStream();
					newStream.Write(SomeBytes, 0, SomeBytes.Length);
					newStream.Close();
				} 
				else 
				{
					req.ContentLength = 0;
				}
				res = (HttpWebResponse)req.GetResponse();
				outcookieheader=req.CookieContainer.GetCookieHeader(new Uri(url));
				if (outcookieheader.Length<2)
				{
					try
					{
						outcookieheader   =   res.Headers["Set-Cookie"]; 
						outcookieheader=outcookieheader.Substring(0,outcookieheader.IndexOf(";"));
					}
					catch
					{
						outcookieheader="";
					}
				}
				Stream ReceiveStream = res.GetResponseStream();
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}			
			return strResult;
		}


		#endregion

		#region PostUrl(String url, String paramList,string cookieheader,out string outcookieheader,string Header_Referer)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="paramList">格式: a=xxx&b=xxx&c=xxx</param>
		/// <param name="cookieheader">输入cookie</param>
		/// <param name="outcookieheader">输出cookie</param>
		/// <param name="Header_Referer">包头 Referer</param>
		/// <returns></returns>
		public static string PostUrl(String url, String paramList,string cookieheader,out string outcookieheader,string Header_Referer) 
		{
			outcookieheader=string.Empty;
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				if (Header_Referer.Length>1)
				{
					req.Referer=Header_Referer;
				}
				req.KeepAlive = true;
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);
				//				}
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;

					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;

					////////////////////////////////////
				}
				StringBuilder UrlEncoded = new StringBuilder();
				Char[] reserved = {'?', '=', '&'};
				byte[] SomeBytes = null;
				if (paramList != null) 
				{
					int i=0, j;
					while(i<paramList.Length)
					{
						j=paramList.IndexOfAny(reserved, i);
						if (j==-1)
						{
							//							UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length-i)));
							UrlEncoded.Append((paramList.Substring(i, paramList.Length-i)));
							break;
						}
						//						UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j-i)));
						UrlEncoded.Append((paramList.Substring(i, j-i)));
						UrlEncoded.Append(paramList.Substring(j,1));
						i = j+1;
					}
					SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
					req.ContentLength = SomeBytes.Length;
					Stream newStream = req.GetRequestStream();
					newStream.Write(SomeBytes, 0, SomeBytes.Length);
					newStream.Close();
				} 
				else 
				{
					req.ContentLength = 0;
				}
				res = (HttpWebResponse)req.GetResponse();
				outcookieheader=req.CookieContainer.GetCookieHeader(new Uri(url));
				if (outcookieheader.Length<2)
				{
					try
					{
						outcookieheader   =   res.Headers["Set-Cookie"]; 
						outcookieheader=outcookieheader.Substring(0,outcookieheader.IndexOf(";"));
					}
					catch
					{
						outcookieheader="";
					}
				}
				Stream ReceiveStream = res.GetResponseStream();
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}			
			return strResult;
		}

		#endregion

		#region PostUrl(String url, String paramList,string cookieheader,out string outcookieheader,string Header_Referer,bool AutoRedirect) 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="paramList">格式: a=xxx&b=xxx&c=xxx</param>
		/// <param name="cookieheader">输入cookie</param>
		/// <param name="outcookieheader">输出cookie</param>
		/// <param name="Header_Referer">包头 Referer</param>
		/// <param name="AutoRedirect">是否自动跳转</param>
		/// <returns></returns>
		public static string PostUrl(String url, String paramList,string cookieheader,out string outcookieheader,string Header_Referer,bool AutoRedirect) 
		{
			outcookieheader=string.Empty;

			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				req.AllowAutoRedirect=AutoRedirect;
				if (Header_Referer.Length>1)
				{
					req.Referer=Header_Referer;
				}
				req.KeepAlive = true;
				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);
				//				}
                //为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;
					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
					////////////////////////////////////
				}
				StringBuilder UrlEncoded = new StringBuilder();
				Char[] reserved = {'?', '=', '&'};
				byte[] SomeBytes = null;
				if (paramList != null) 
				{
					int i=0, j;
					while(i<paramList.Length)
					{
						j=paramList.IndexOfAny(reserved, i);
						if (j==-1)
						{
							//							UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length-i)));
							UrlEncoded.Append((paramList.Substring(i, paramList.Length-i)));
							break;
						}
						//						UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j-i)));
						UrlEncoded.Append((paramList.Substring(i, j-i)));
						UrlEncoded.Append(paramList.Substring(j,1));
						i = j+1;
					}
					SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
					req.ContentLength = SomeBytes.Length;
					Stream newStream = req.GetRequestStream();
					newStream.Write(SomeBytes, 0, SomeBytes.Length);
					newStream.Close();
				} 
				else 
				{
					req.ContentLength = 0;
				}
				res = (HttpWebResponse)req.GetResponse();
				outcookieheader=req.CookieContainer.GetCookieHeader(new Uri(url));
				if (outcookieheader.Length<2)
				{
					try
					{
						outcookieheader   =   res.Headers["Set-Cookie"]; 
						outcookieheader=outcookieheader.Substring(0,outcookieheader.IndexOf(";"));
					}
					catch
					{
						outcookieheader="";
					}
				}
				Stream ReceiveStream = res.GetResponseStream();
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}			
			return strResult;
		}

		#endregion

		#region PostUrl(String url, String paramList,string cookieheader,out string outcookieheader,string Header_Referer,bool AutoRedirect,string Header_UserAgent,string http_type) 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="paramList">格式: a=xxx&b=xxx&c=xxx</param>
		/// <param name="cookieheader">输入cookie</param>
		/// <param name="outcookieheader">输出cookie</param>
		/// <param name="Header_Referer">包头 Referer</param>
		/// <param name="AutoRedirect">是否自动跳转</param>
		/// <param name="Header_UserAgent">包头 UserAgent</param>
		/// <param name="http_type"> 请求类型 http https </param>
		/// <returns></returns>
		
		public static string PostUrl(String url, String paramList,string cookieheader,out string outcookieheader,string Header_Referer,bool AutoRedirect,string Header_UserAgent,string http_type) 
		{
			if (http_type=="https")
			{
				System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy(); //https 跳过证书
			}
			outcookieheader=string.Empty;
			HttpWebResponse res = null;
			string strResult = "";
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";				
				req.AllowAutoRedirect=AutoRedirect;
				req.Accept="image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/msword, application/vnd.ms-powerpoint, */*";
				req.Headers.Add("Accept-Encoding","gzip, deflate");
				req.Headers.Add("Accept-Language","zh-cn");
				req.Headers.Add("Cache-Control","no-cache");
				req.KeepAlive=true;
				req.ContentType="application/x-www-form-urlencoded";
				req.Headers.Add("UA-CPU","x86");
				req.Referer=Header_Referer ;
				req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;

					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
					////////////////////////////////////
				}
				StringBuilder UrlEncoded = new StringBuilder();
				Char[] reserved = {'?', '=', '&'};
				byte[] SomeBytes = null;
				if (paramList != null) 
				{
					int i=0, j;
					while(i<paramList.Length)
					{
						j=paramList.IndexOfAny(reserved, i);
						if (j==-1)
						{
							//UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length-i)));
							UrlEncoded.Append((paramList.Substring(i, paramList.Length-i)));
							break;
						}
						//UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j-i)));
						UrlEncoded.Append((paramList.Substring(i, j-i)));
						UrlEncoded.Append(paramList.Substring(j,1));
						i = j+1;
					}
					SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
					req.ContentLength = SomeBytes.Length;
					Stream newStream = null;
					try
					{
						newStream = req.GetRequestStream();
					}
					catch(Exception exp)
					{
						Console.WriteLine(exp.Message.ToString());
					}
					newStream.Write(SomeBytes, 0, SomeBytes.Length);
					newStream.Close();
				} 
				else 
				{
					req.ContentLength = 0;
				}
				//取得返回的响应
				res = (HttpWebResponse)req.GetResponse();
				outcookieheader=req.CookieContainer.GetCookieHeader(new Uri(url));
				if (outcookieheader.Length<2)
				{
					try
					{
						outcookieheader   =   res.Headers["Set-Cookie"]; 
						outcookieheader=outcookieheader.Substring(0,outcookieheader.IndexOf(";"));
					}
					catch(Exception exp)
					{
						string s = exp.Message.ToString();
						outcookieheader="";
					}
				}
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				//				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
				string encodeheader=res.ContentType;
				string encodestr=System.Text.Encoding.Default.HeaderName;
				if ((encodeheader.IndexOf("charset=")>=0)&&(encodeheader.IndexOf("charset=GBK")==-1)&&(encodeheader.IndexOf("charset=gbk")==-1))
				{
					int i=encodeheader.IndexOf("charset=");
					encodestr=encodeheader.Substring(i+8);
				}
				Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");
				StreamReader sr = new StreamReader( ReceiveStream,encode );
				Char[] read = new Char[256];
				int count = sr.Read( read, 0, 256 );
				while (count > 0) 
				{
					String str = new String(read, 0, count);
					strResult += str;
					count = sr.Read(read, 0, 256);
				}
			} 
			catch(Exception e) 
			{
				strResult = e.ToString();
			} 
			finally 
			{
				if ( res != null ) 
				{
					res.Close();
				}
			}			
			return strResult;
		}

		#endregion
		

		#endregion

		#region 获取图片

		/// GET方法获取页面
		/// 函数名:GetImage	
		/// 功能描述:GET方法获取页面	
		/// 处理流程:
		/// 算法描述:
		/// 作 者: 杨栋
		/// 日 期: 2006-11-21 09:00
		/// 修 改: 2007-01-29 17:00 2006-12-05 17:00
		/// 日 期:
		/// 版 本:
		/// 
		#region GetImage(String url,string  cookieheader)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="cookieheader">输入Cookie</param>
		/// <returns></returns>
		public static byte[] GetImage(String url,string  cookieheader) 
		{
			//			outcookieheader="";
			HttpWebResponse res = null;
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				//				req.Method = "GET";
				//				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//
				//				//				string name=cookieheader.Substring(0,cookieheader.IndexOf("="));
				//				//				string key=cookieheader.Substring(cookieheader.IndexOf("=")+1,cookieheader.Length-cookieheader.IndexOf("=")-1);
				//				//				cookieCon.Add(new Uri(url),new Cookie(name,key));
				//				
				//				
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				//				}
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;

					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
				}
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				byte[] mybytes=new  byte[4096];		
				int count=ReceiveStream.Read(mybytes,0,4096);
				byte[] image = new byte[count];
				Array.Copy(mybytes,image,count);
				if ( res != null ) 
				{
					res.Close();
				}
				return image;
			}  
			finally
			{
			}
		}

		#endregion   

		#region GetImage(String url,string  cookieheader,out string outcookieheader,string Header_Referer)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="cookieheader">输入Cookie</param>
		/// <returns></returns>
		public static byte[] GetImage(String url,string  cookieheader,out string outcookieheader,string Header_Referer) 
		{
			//			outcookieheader="";
			HttpWebResponse res = null;
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				if (Header_Referer.Length>1)
				{
					req.Referer=Header_Referer;
				}
				//				req.Method = "GET";
				//				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//
				//				//				string name=cookieheader.Substring(0,cookieheader.IndexOf("="));
				//				//				string key=cookieheader.Substring(cookieheader.IndexOf("=")+1,cookieheader.Length-cookieheader.IndexOf("=")-1);
				//				//				cookieCon.Add(new Uri(url),new Cookie(name,key));
				//				
				//				
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				//				}

				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					//////////////////////////////////
					string[] ls_cookie = null;

					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;

					////////////////////////////////////
				}
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				outcookieheader = req.CookieContainer.GetCookieHeader(new Uri(url));//获得cookie
				if (outcookieheader.Length<2)
				{
					try
					{
						outcookieheader   =   res.Headers["Set-Cookie"]; 
						outcookieheader=outcookieheader.Substring(0,outcookieheader.IndexOf(";"));
					}
					catch
					{
						outcookieheader="";
					}
				}
				byte[] mybytes=new  byte[4096];		
				int count=ReceiveStream.Read(mybytes,0,4096);
				byte[] image = new byte[count];
				Array.Copy(mybytes,image,count);
				if ( res != null ) 
				{
					res.Close();
				}
				return image;
			}  
			finally
			{
			}
		}

		#endregion   

		#region GetImage(String url,string  cookieheader,string Header_Referer)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="cookieheader">输入Cookie</param>
		/// <param name="Header_Referer">包头 Referer</param>
		/// <returns></returns>
		public static byte[] GetImage(String url,string  cookieheader,string Header_Referer) 
		{
			//			outcookieheader="";
			HttpWebResponse res = null;
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				if (Header_Referer.Length>1)
				{
					req.Referer=Header_Referer;
				}
				//				req.Method = "GET";
				//				req.ContentType = "application/x-www-form-urlencoded";
				//req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
				//				CookieContainer cookieCon = new CookieContainer();
				//
				//				//				string name=cookieheader.Substring(0,cookieheader.IndexOf("="));
				//				//				string key=cookieheader.Substring(cookieheader.IndexOf("=")+1,cookieheader.Length-cookieheader.IndexOf("=")-1);
				//				//				cookieCon.Add(new Uri(url),new Cookie(name,key));
				//				
				//				
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				//				}

				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					string[] ls_cookie = null;
					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
				}
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				byte[] mybytes=new  byte[4096];		
				int count=ReceiveStream.Read(mybytes,0,4096);
				byte[] image = new byte[count];
				Array.Copy(mybytes,image,count);
				if ( res != null ) 
				{
					res.Close();
				}
				return image;
			}  
			finally
			{
			}
		}

		#endregion   

		#region GetImage(String url,string  cookieheader,string Header_Referer,string Header_UserAgent,string http_type)  
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">目标url</param>
		/// <param name="cookieheader">输入Cookie</param>
		/// <param name="Header_Referer">包头 Referer</param>
		/// <param name="Header_UserAgent">包头 UserAgent</param>
		/// <param name="http_type"> 请求类型 http https </param>
		/// <returns></returns>
		public static byte[] GetImage(String url,string  cookieheader,string Header_Referer ,string Header_UserAgent,string http_type) 
		{
			//			outcookieheader="";

			if (http_type=="https")
			{
				System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy(); //https 跳过证书
			}
			HttpWebResponse res = null;
			try 
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				if (Header_Referer.Length>1)
				{
					req.Referer=Header_Referer;
				}
				//				req.Method = "GET";
				//				req.ContentType = "application/x-www-form-urlencoded";
				if(Header_UserAgent.Length>2)
				{
					req.UserAgent=Header_UserAgent;
				}
				//				CookieContainer cookieCon = new CookieContainer();
				//
				//				//				string name=cookieheader.Substring(0,cookieheader.IndexOf("="));
				//				//				string key=cookieheader.Substring(cookieheader.IndexOf("=")+1,cookieheader.Length-cookieheader.IndexOf("=")-1);
				//				//				cookieCon.Add(new Uri(url),new Cookie(name,key));
				//				
				//				
				//				req.CookieContainer = cookieCon;
				//				if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
				//				{
				//					req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
				//				}
				//为请求加入cookies 
				CookieContainer cookieCon = new CookieContainer();
				//				req.CookieContainer = cookieCon;
				//取得cookies 集合
				string[] ls_cookies = cookieheader.Split(';');
				if (ls_cookies.Length <= 1) //如果有一个或没有cookies 就采用下面的方法。
				{
					req.CookieContainer = cookieCon;
					if ((cookieheader.Length>0 )&(cookieheader.IndexOf("=")>0))
					{
						req.CookieContainer.SetCookies(new Uri(url),cookieheader);	
					}
				}
				else
				{
					//如果是多个cookie 就分别加入 cookies 容器。
					string[] ls_cookie = null;
					for(int i=0;i<ls_cookies.Length;i++)
					{
						ls_cookie = ls_cookies[i].Split('=');
						//						cookieCon.Add(new Uri(url),new Cookie(ls_cookie[0].ToString().Trim(),ls_cookie[1].ToString().Trim()));
						cookieCon.Add(new Uri(url), new Cookie(ls_cookie[0].ToString().Trim(),ls_cookies[i].Substring(ls_cookies[i].IndexOf("=")+1)));
					}
					req.CookieContainer = cookieCon;
				}
				res = (HttpWebResponse)req.GetResponse();
				Stream ReceiveStream = res.GetResponseStream();
				byte[] mybytes=new  byte[4096];		
				int count=ReceiveStream.Read(mybytes,0,4096);
				byte[] image = new byte[count];
				Array.Copy(mybytes,image,count);
				if ( res != null ) 
				{
					res.Close();
				}
				return image;
			}  
			finally
			{
			}
		}

		#endregion   

		#endregion
	}
}

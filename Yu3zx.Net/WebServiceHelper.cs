using System;
using System.Reflection;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace Yu3zx.NetBase
{
	/// <summary>
	/// WebServiceHelper 静态类。
	/// </summary>
	public static class WebServiceHelper
	{
		#region InvokeWebService
		/// <summary>
        /// InvokeWebService 动态调用web服务
		/// </summary>
		/// <param name="wsUrl">WebService 地址</param>
		/// <param name="methodname">方法名</param>
		/// <param name="args">参数，仅仅支持简单类型</param>		
		public static object InvokeWebService(string wsUrl, string methodname, object[] args)
		{
			return WebServiceHelper.InvokeWebService(wsUrl ,null ,methodname ,args) ;
		}

        /// <summary>
        /// InvokeWebService 动态调用web服务
        /// </summary>
		public static object InvokeWebService(string wsUrl,  string classname, string methodname, object[] args)
		{		
			try
			{
                Type wsProxyType = WebServiceHelper.GetWsProxyType(wsUrl, classname);
                object obj = Activator.CreateInstance(wsProxyType);
                MethodInfo mi = wsProxyType.GetMethod(methodname);

				return mi.Invoke(obj,args);
			}
			catch(Exception ex)
			{
                throw ex;
			}
		}

        #region GetWsClassName
        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];
        } 
        #endregion
		#endregion

        #region GetWsProxyType ,using Cache
        private static IDictionary<string, Type> WSProxyTypeDictionary = new Dictionary<string, Type>();
        /// <summary>
        /// GetWsProxyType 获取目标Web服务对应的代理类型
        /// </summary>
        /// <param name="wsUrl">目标Web服务的url</param>
        /// <param name="classname">Web服务的class名称，如果不需要指定，则传入null</param>      
        public static Type GetWsProxyType(string wsUrl, string classname)
        {
            string @namespace = "Yu3zx.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = WebServiceHelper.GetWsClassName(wsUrl);
            }
            string cacheKey = wsUrl + "@" + classname;
            if (WebServiceHelper.WSProxyTypeDictionary.ContainsKey(cacheKey))
            {
                return WebServiceHelper.WSProxyTypeDictionary[cacheKey];
            }


            //获取WSDL
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(wsUrl + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);

            //生成客户端代理类代码
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider csc = new CSharpCodeProvider();
            ICodeCompiler icc = csc.CreateCompiler();

            //设定编译参数
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            //编译代理类
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            //生成代理实例，并调用方法
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            Type[] ts = assembly.GetTypes();
            Type wsProxyType = assembly.GetType(@namespace + "." + classname, true, true);
            

            lock (WebServiceHelper.WSProxyTypeDictionary)
            {
                if (!WebServiceHelper.WSProxyTypeDictionary.ContainsKey(cacheKey))
                {
                    WebServiceHelper.WSProxyTypeDictionary.Add(cacheKey, wsProxyType);
                }
            }
            return wsProxyType;
        }
        #endregion
	}
}

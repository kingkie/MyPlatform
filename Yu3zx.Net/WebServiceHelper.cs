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
	/// WebServiceHelper ��̬�ࡣ
	/// </summary>
	public static class WebServiceHelper
	{
		#region InvokeWebService
		/// <summary>
        /// InvokeWebService ��̬����web����
		/// </summary>
		/// <param name="wsUrl">WebService ��ַ</param>
		/// <param name="methodname">������</param>
		/// <param name="args">����������֧�ּ�����</param>		
		public static object InvokeWebService(string wsUrl, string methodname, object[] args)
		{
			return WebServiceHelper.InvokeWebService(wsUrl ,null ,methodname ,args) ;
		}

        /// <summary>
        /// InvokeWebService ��̬����web����
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
        /// GetWsProxyType ��ȡĿ��Web�����Ӧ�Ĵ�������
        /// </summary>
        /// <param name="wsUrl">Ŀ��Web�����url</param>
        /// <param name="classname">Web�����class���ƣ��������Ҫָ��������null</param>      
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


            //��ȡWSDL
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(wsUrl + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);

            //���ɿͻ��˴��������
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider csc = new CSharpCodeProvider();
            ICodeCompiler icc = csc.CreateCompiler();

            //�趨�������
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            //���������
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

            //���ɴ���ʵ���������÷���
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

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Yu3zx.NetBase
{
	/// <summary>
	/// TrustAllCertificatePolicy 的摘要说明。
	/// </summary>
	public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
	{
		public TrustAllCertificatePolicy()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public bool CheckValidationResult(ServicePoint sp,System.Security.Cryptography.X509Certificates.X509Certificate cert,WebRequest req, int problem) 
		{ 
			return true; 
		} 
	}
}

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Yu3zx.NetBase
{
	/// <summary>
	/// TrustAllCertificatePolicy ��ժҪ˵����
	/// </summary>
	public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
	{
		public TrustAllCertificatePolicy()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public bool CheckValidationResult(ServicePoint sp,System.Security.Cryptography.X509Certificates.X509Certificate cert,WebRequest req, int problem) 
		{ 
			return true; 
		} 
	}
}

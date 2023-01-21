using System;
using System.Net.Sockets;

namespace Yu3zx.Addins
{	
	/// <summary>
	/// IAddin ���в�������ӿ� 
	/// </summary>
	public interface IAddin
	{		
        /// <summary>
        /// OnLoading �������ڻص��������������ϱ����á����Դ�AddinUtil��ȡ��Ӧ�ô��ݵĲ�������ʼ�����
        /// </summary>
		void OnLoading() ;

        /// <summary>
        /// BeforeTerminating �������ڻص���ж�ز��ǰ����
        /// </summary>
		void BeforeTerminating() ;

        /// <summary>
        /// Enabled ����Ƿ�����
        /// </summary>
		bool Enabled{get ;set ;}

        /// <summary>
        /// AddinKey ����ؼ��֣���ͬ�Ĳ����Key�ǲ�һ����
        /// </summary>
		int AddinKey {get ;}

        /// <summary>
        /// ServiceName ����ṩ�ķ��������	
        /// </summary>
        string AddinName { get; } 
	
        /// <summary>
        /// Description �����������Ϣ	
        /// </summary>
		string Description{get ;}      

        /// <summary>
        /// Version ����汾
        /// </summary>
		float  Version{get ;}
	}	

	public class AddinHelper
	{
		public const string AddinSign = "Addin.dll" ; //���еĲ������"Addin.dll"��β
	}
}

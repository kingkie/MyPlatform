using System;
using System.Net.Sockets;

namespace Yu3zx.Addins
{	
	/// <summary>
	/// IAddin 所有插件基本接口 
	/// </summary>
	public interface IAddin
	{		
        /// <summary>
        /// OnLoading 生命周期回调，当插件加载完毕被调用。可以从AddinUtil获取主应用传递的参数来初始化插件
        /// </summary>
		void OnLoading() ;

        /// <summary>
        /// BeforeTerminating 生命周期回调，卸载插件前调用
        /// </summary>
		void BeforeTerminating() ;

        /// <summary>
        /// Enabled 插件是否启用
        /// </summary>
		bool Enabled{get ;set ;}

        /// <summary>
        /// AddinKey 插件关键字，不同的插件其Key是不一样的
        /// </summary>
		int AddinKey {get ;}

        /// <summary>
        /// ServiceName 插件提供的服务的名字	
        /// </summary>
        string AddinName { get; } 
	
        /// <summary>
        /// Description 插件的描述信息	
        /// </summary>
		string Description{get ;}      

        /// <summary>
        /// Version 插件版本
        /// </summary>
		float  Version{get ;}
	}	

	public class AddinHelper
	{
		public const string AddinSign = "Addin.dll" ; //所有的插件都以"Addin.dll"结尾
	}
}

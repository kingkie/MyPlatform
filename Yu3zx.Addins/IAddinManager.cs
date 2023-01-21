using System;
using System.Collections.Generic ;

namespace Yu3zx.Addins
{
	/// <summary>
	/// IAddinManagement 用于加载/卸载，管理各种插件。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public interface IAddinManager
	{
        #region Property
        /// <summary>
        /// CopyToMemory 是否将插件拷贝到内存后加载
        /// </summary>
        bool CopyToMemory { get;set;}       

        /// <summary>
        /// AddinList 已加载的插件列表
        /// </summary>
        IList<IAddin> AddinList { get;} //集合中为IAddin
        
        #endregion

        #region Method
        /// <summary>
        /// LoadDefault 加载当前目录或子目录下的所有有效插件
        /// </summary>
        void LoadDefault();

        /// <summary>
        /// LoadAllAddins 加载指定目录下的所有插件
        /// </summary>      
        void LoadAllAddins(string addinFolderPath, bool searchChildFolder);

        /// <summary>
        /// LoadAddinAssembly 加载指定的插件
        /// </summary>        
        void LoadAddinAssembly(string assemblyPath);

        /// <summary>
        /// Clear 清空所有已经加载的插件
        /// </summary>
        void Clear();      

        /// <summary>
        /// DynRemoveAddin 动态移除指定的插件
        /// </summary>       
        void DynRemoveAddin(int addinKey);

        /// <summary>
        /// EnableAddin 启用指定的插件
        /// </summary>       
        void EnableAddin(int addinKey);

        /// <summary>
        /// EnableAddin 禁用指定的插件
        /// </summary> 
        void DisableAddin(int addinKey);      

        IAddin GetAddin(int addinKey);
        #endregion

        #region Event
        event CbSimple AddinsChanged; 
        #endregion
	}
}

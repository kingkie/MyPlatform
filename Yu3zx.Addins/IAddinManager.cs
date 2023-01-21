using System;
using System.Collections.Generic ;

namespace Yu3zx.Addins
{
	/// <summary>
	/// IAddinManagement ���ڼ���/ж�أ�������ֲ����
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public interface IAddinManager
	{
        #region Property
        /// <summary>
        /// CopyToMemory �Ƿ񽫲���������ڴ�����
        /// </summary>
        bool CopyToMemory { get;set;}       

        /// <summary>
        /// AddinList �Ѽ��صĲ���б�
        /// </summary>
        IList<IAddin> AddinList { get;} //������ΪIAddin
        
        #endregion

        #region Method
        /// <summary>
        /// LoadDefault ���ص�ǰĿ¼����Ŀ¼�µ�������Ч���
        /// </summary>
        void LoadDefault();

        /// <summary>
        /// LoadAllAddins ����ָ��Ŀ¼�µ����в��
        /// </summary>      
        void LoadAllAddins(string addinFolderPath, bool searchChildFolder);

        /// <summary>
        /// LoadAddinAssembly ����ָ���Ĳ��
        /// </summary>        
        void LoadAddinAssembly(string assemblyPath);

        /// <summary>
        /// Clear ��������Ѿ����صĲ��
        /// </summary>
        void Clear();      

        /// <summary>
        /// DynRemoveAddin ��̬�Ƴ�ָ���Ĳ��
        /// </summary>       
        void DynRemoveAddin(int addinKey);

        /// <summary>
        /// EnableAddin ����ָ���Ĳ��
        /// </summary>       
        void EnableAddin(int addinKey);

        /// <summary>
        /// EnableAddin ����ָ���Ĳ��
        /// </summary> 
        void DisableAddin(int addinKey);      

        IAddin GetAddin(int addinKey);
        #endregion

        #region Event
        event CbSimple AddinsChanged; 
        #endregion
	}
}

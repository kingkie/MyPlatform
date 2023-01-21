using System;
using System.Collections.Generic ;

namespace Yu3zx.Addins
{
	/// <summary>
	/// AddinUtil static class ��
	/// ��������Ӧ�ó����������ݱ�Ҫ�Ĳ�����Ϣ
	/// </summary>
	public static class AddinUtil
	{
        private static IDictionary<string, object> DicUtil = new Dictionary<string, object>();

        #region RegisterObject
        public static void RegisterObject(string name, object obj)
        {
            lock (AddinUtil.DicUtil)
            {
                if (AddinUtil.DicUtil.ContainsKey(name))
                {
                    AddinUtil.Remove(name);
                }

                AddinUtil.DicUtil.Add(name, obj);
            }
        } 
        #endregion

        #region GetObject
        public static object GetObject(string name)
        {
            lock (AddinUtil.DicUtil)
            {
                if (AddinUtil.DicUtil.ContainsKey(name))
                {
                    return AddinUtil.DicUtil[name];
                }
                return null;
            }
        } 
        #endregion

        #region Remove
        public static void Remove(string name)
        {
            lock (AddinUtil.DicUtil)
            {
                if (AddinUtil.DicUtil.ContainsKey(name))
                {
                    AddinUtil.DicUtil.Remove(name);
                }
            }
        } 
        #endregion

        #region Clear
        public static void Clear()
        {
            lock (AddinUtil.DicUtil)
            {
                AddinUtil.DicUtil.Clear();
            }
        } 
        #endregion
	}
}

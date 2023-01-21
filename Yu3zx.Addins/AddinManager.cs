using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Yu3zx.Util;

namespace Yu3zx.Addins
{
	/// <summary>
	/// NetAddinManagement 用于管理所有的插件，是IAddinManagement的默认实现。
	/// </summary>
	public class AddinManager :IAddinManager
	{
        private IDictionary<int, IAddin> dicAddins = new Dictionary<int, IAddin>();
        public event CbSimple AddinsChanged;

		public AddinManager()
		{
            this.AddinsChanged += delegate { };
		}

		#region IAddinManagement 成员
        #region LoadAllAddins
        public void LoadAllAddins(string addin_FolderPath, bool searchChildFolder)
        {
            ReflectionHelper.TypeLoadConfig config = new ReflectionHelper.TypeLoadConfig(this.copyToMem, false, AddinHelper.AddinSign);
            IList<Type> addinTypeList = ReflectionHelper.LoadDerivedType(typeof(IAddin), addin_FolderPath, searchChildFolder, config);
            foreach (Type addinType in addinTypeList)
            {
                IAddin addin = (IAddin)Activator.CreateInstance(addinType);
                this.dicAddins.Add(addin.AddinKey, addin);
                addin.OnLoading();
            }

            this.AddinsChanged();
        } 
        #endregion

        #region LoadDefault
        public void LoadDefault()
        {
            this.LoadAllAddins(AppDomain.CurrentDomain.BaseDirectory, true);
        } 
        #endregion

        #region LoadAddinAssembly

        public byte[] ReadFileReturnBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            BinaryReader br = new BinaryReader(fs);

            byte[] buff = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return buff;
        }

        public void LoadAddinAssembly(string assemblyPath)
        {
            Assembly asm = null;
            if (this.copyToMem)
            {
                byte[] addinStream = ReadFileReturnBytes(assemblyPath);
                asm = Assembly.Load(addinStream);
            }
            else
            {
                asm = Assembly.LoadFrom(assemblyPath);
            }


            IList<IAddin> newList = ReflectionHelper.LoadDerivedInstance<IAddin>(asm);
            foreach (IAddin newAddin in newList)
            {
                this.dicAddins.Add(newAddin.AddinKey ,newAddin);
                newAddin.OnLoading();
            }

            this.AddinsChanged();
        } 
        #endregion

		#region Clear ,DynRemoveAddin
		public void Clear()
		{
			foreach(IAddin addin in this.dicAddins.Values)
			{
				try	
				{
					addin.BeforeTerminating() ;
				}
				catch{}
			}

			this.dicAddins.Clear() ;
			this.AddinsChanged() ;
		}

		public void DynRemoveAddin(int addinKey)
		{
			IAddin dest = this.GetAddin(addinKey) ;
			
			if(dest != null)
			{
				dest.BeforeTerminating() ;
                this.dicAddins.Remove(addinKey);	
				this.AddinsChanged() ;
			}
		}

		private bool ContainsAddin(int addinKey)
		{
            return this.dicAddins.ContainsKey(addinKey);
		}
		#endregion

		#region AddinList       
        public IList<IAddin> AddinList
		{
			get
			{
                return CollectionConverter.CopyAllToList<IAddin>(this.dicAddins.Values);
			}
		}
		#endregion	

        #region GetAddin
        public IAddin GetAddin(int addinKey)
        {
            if (!this.dicAddins.ContainsKey(addinKey))
            {
                return null;
            }

            return this.dicAddins[addinKey];
        } 
        #endregion

		#region event ,property
        private bool copyToMem = false;
		public bool CopyToMemory
		{
			get
			{
                return this.copyToMem;
			}
			set
			{
                this.copyToMem = value;
			}
		}     

		
		
		#endregion

		#region EnableAddin ,DisableAddin
		public void EnableAddin(int addinKey)
		{
            IAddin dest = this.GetAddin(addinKey);
            if (dest != null)
            {
                dest.Enabled = true;
            }
		}

		public void DisableAddin(int addinKey)
		{
            IAddin dest = this.GetAddin(addinKey);
            if (dest != null)
            {
                dest.Enabled = false;
            }
		}
		#endregion
		#endregion
	}		
}

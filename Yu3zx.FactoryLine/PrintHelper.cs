using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu3zx.FactoryLine.Models;

namespace Yu3zx.FactoryLine
{
    public class PrintHelper
    {
        #region 单例定义
        private static object syncObj = new object();
        private static PrintHelper instance = null;
        public static PrintHelper GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = new PrintHelper();
                }
            }
            return instance;
        }

        #endregion End

        /// <summary>
        /// 面料标签纸
        /// </summary>
        public FabricLabel ClothInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public TubeLabel TubePaperInfo
        {
            get;
            set;
        }
    }
}

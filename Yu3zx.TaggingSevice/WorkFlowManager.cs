using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class WorkFlowManager
    {
        #region 单例
        private static WorkFlowManager instance = null;

        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static WorkFlowManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new WorkFlowManager();
                }
            }
            return instance;
        }

        #endregion End

        public string CurrentLine
        {
            get;
            set;
        }

        public string CurrentBatchNo
        {
            get;
            set;
        }

        /// <summary>
        /// 当前上线面料
        /// </summary>
        public List<FabricClothItem> OnLaunchItems = new List<FabricClothItem>();
    }

    public enum WorkState: int
    {
        /// <summary>
        /// 当前待机
        /// </summary>
        None = 0,
        /// <summary>
        /// 缓冲积累状态
        /// </summary>
        ClothPrepare = 1,
        /// <summary>
        /// 上线阶段
        /// </summary>
        ClothOnLine = 2,
        /// <summary>
        /// 粘贴管外标签
        /// </summary>
        PasteLabel = 3,
        /// <summary>
        /// 6个装箱
        /// </summary>
        PackSmallBag = 4,

        
    }
}

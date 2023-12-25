using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu3zx.ClothServer.Models;
using Yu3zx.DapperExtend;

namespace SKII.LargeScreen
{
    public class DataHelper
    {
        #region 配置
        public SetConfig GetSetConfig(string strKey)
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var lConfigs = db.Select<SetConfig>(u => u.KeyName == strKey);
                    if (lConfigs != null && lConfigs.Count > 0)
                    {
                        Console.WriteLine("获取SetConfig成功");
                        return lConfigs[0];
                    }
                    else
                    {
                        Console.WriteLine("获取SetConfig失败");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        #endregion End


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FabricClothItem> GetFabrics()
        {
            using (var db = new DapperContext("MySqlDbConnection"))
            {
                try
                {
                    var lFabrics = db.Select<FabricClothItem>("select", null);
                    if (lFabrics != null && lFabrics.Count > 0)
                    {
                        Console.WriteLine("获取成功");
                        return lFabrics;
                    }
                    else
                    {
                        Console.WriteLine("获取失败");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

using Yu3zx.ClothLaunch.Models;
using Yu3zx.DapperExtend;
using Yu3zx.Logs;

namespace Yu3zx.ClothLaunch
{
    public class SqlDataHelper
    {
        /// <summary>
        /// DATE
        /// </summary>
        /// <returns></returns>
        public static HSFabric GetNoPackFabric(DateTime dtDay)
        {
            using (var db = new DapperContext("DbConnection"))
            {
                try
                {
                    var lFabric = db.Select<HSFabric>("SELECT sCardNo,sMaterialLot,sFabricNo,sMaterialName,sYarnInfo,iManualOrderNo,nLength,nClothRollDiameter,sProductWidthOrder,sColorNo,sColorName,sEquipmentNo,sEquipmentName,sGrade,sRemark,bend,tInspectTime " +
                        " from qmInspectHdr where Date(tInspectTime)=@AddTime AND (bEnd is null OR bEnd = 0) ORDER BY tInspectTime ASC ", new { AddTime = dtDay.Date});// u => u.AlarmTime.Date == date.Date && u.DevId == strId
                    if(lFabric != null && lFabric.Count > 0)
                    {
                        return lFabric[0];
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.LogWrite(ex.Message);
                    Log.Instance.LogWrite(ex.StackTrace);
                    return null;
                }
                return null;
            }
        }

        public static HSFabric GetNoPackFabric(string lineNum)
        {
            using (var db = new DapperContext("DbConnection"))
            {
                try
                {
                    var lFabric = db.Select<HSFabric>("SELECT sCardNo,sMaterialLot,sFabricNo,sMaterialName,sYarnInfo,iManualOrderNo,nLength,nClothRollDiameter,sProductWidthOrder,sColorNo,sColorName,sEquipmentNo,sFabricWidth,sGrade,sRemark,bend,tInspectTime " +
                        " from qmInspectHdr where sEquipmentNo=@EquipmentNo AND (bEnd is null OR bEnd = 0) ORDER BY tInspectTime ASC ", new { EquipmentNo = lineNum });// u => u.AlarmTime.Date == date.Date && u.DevId == strId
                    db.Dispose();
                    if (lFabric != null && lFabric.Count > 0)
                    {
                        Log.Instance.LogWrite("查询到数据：" + lFabric.Count.ToString());
                        return lFabric[0];
                    }
                    Log.Instance.LogWrite("未查询到数据：");
                }
                catch (Exception ex)
                {
                    db.Dispose();
                    Log.Instance.LogWrite(ex.Message);
                    Log.Instance.LogWrite(ex.StackTrace);
                    return null;
                }
                return null;
            }
        }

        public static bool HSFabricUpdate(string sFabricNo)
        {
            using (var db = new DapperContext("DbConnection"))
            {
                try
                {
                    //var rtnUpdate = db.Update<AlarmItem>(u => new { u.AlarmTime },new AlarmItem() { DevId = id, AlarmTime = dtUpdate });
                    var rtnUpdate = db.Update("UPDATE qmInspectHdr SET bEnd = 1 WHERE sFabricNo = @sFabricNo", new { sFabricNo = sFabricNo});//
                    db.Dispose();
                    return rtnUpdate;
                }
                catch (Exception ex)
                {
                    db.Dispose();
                    return false;
                }
            }
        }

        public static bool HSFabricAllUpdate(string lineNum)
        {
            using (var db = new DapperContext("DbConnection"))
            {
                try
                {
                    //var rtnUpdate = db.Update<AlarmItem>(u => new { u.AlarmTime },new AlarmItem() { DevId = id, AlarmTime = dtUpdate });
                    var rtnUpdate = db.Update("UPDATE qmInspectHdr SET bEnd = 1 WHERE sEquipmentNo=@EquipmentNo", new { EquipmentNo = lineNum });//
                    db.Dispose();
                    return rtnUpdate;
                }
                catch (Exception ex)
                {
                    db.Dispose();
                    return false;
                }
            }
        }

        public static bool HSFabricUpdate(string lineNum,string strbatch)
        {
            using (var db = new DapperContext("DbConnection"))
            {
                try
                {
                    //var rtnUpdate = db.Update<AlarmItem>(u => new { u.AlarmTime },new AlarmItem() { DevId = id, AlarmTime = dtUpdate });
                    var rtnUpdate = db.Update("UPDATE qmInspectHdr SET bEnd = 1 WHERE sEquipmentNo=@EquipmentNo AND sCardNo=@CardNo", new { EquipmentNo = lineNum, CardNo = strbatch });//
                    db.Dispose();
                    return rtnUpdate;
                }
                catch (Exception ex)
                {
                    db.Dispose();
                    return false;
                }
            }
        }

        public static List<HSFabric> GetFabricList(string lineNum)
        {
            using (var db = new DapperContext("DbConnection"))
            {
                try
                {
                    var lFabric = db.Select<HSFabric>("SELECT sCardNo,sMaterialLot,sFabricNo,sMaterialName,sYarnInfo,iManualOrderNo,nLength,sProductWidthOrder,sColorNo,sColorName,sEquipmentNo,sFabricWidth,sGrade,sRemark,bend,tInspectTime " +
                        " from qmInspectHdr where sEquipmentNo=@EquipmentNo AND (bEnd is null OR bEnd = 0) ORDER BY tInspectTime ASC ", new { EquipmentNo = lineNum });// u => u.AlarmTime.Date == date.Date && u.DevId == strId
                    db.Dispose();
                    if (lFabric != null && lFabric.Count > 0)
                    {
                        Log.Instance.LogWrite("查询到数据：" + lFabric.Count.ToString());
                        return lFabric;
                    }
                    Log.Instance.LogWrite("未查询到数据：");
                }
                catch (Exception ex)
                {
                    db.Dispose();
                    Log.Instance.LogWrite(ex.Message);
                    Log.Instance.LogWrite(ex.StackTrace);
                    return null;
                }
                return null;
            }
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public static RoleItem GetRoleItem()
        {
            using (var db = new DapperContext("DbConnection1"))
            {
                try
                {
                    var lFabric = db.Select<RoleItem>("SELECT * FROM Sys_Role ",null);// u => u.AlarmTime.Date == date.Date && u.DevId == strId
                    db.Dispose();
                    if (lFabric != null && lFabric.Count > 0)
                    {
                        return lFabric[0];
                    }
                }
                catch (Exception ex)
                {
                    db.Dispose();
                    Log.Instance.LogWrite(ex.Message);
                    Log.Instance.LogWrite(ex.StackTrace);
                    return null;
                }
                return null;
            }
        }

    }
}

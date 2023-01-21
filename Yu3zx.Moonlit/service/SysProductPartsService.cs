using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
    public class SysProductPartsService : BaseService
    {

        public List<SysProductParts> list(string name)
        {
            string sqlstrSpec = "select name,no1,no2,no3,no4,no5,product_id,plc_id,stage,serial from sys_product_parts where name like '%" + name + "%'";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_product_info");

            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<SysProductParts> sysProductPartsList = new List<SysProductParts>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {
                    SysProductParts sysProductParts = convertToModel(dsSpectrum.Tables[0].Rows[0]);
                    sysProductPartsList.Add(sysProductParts);
                }
                return sysProductPartsList;
            }
            return null;
        }
        public List<SysProductParts> getProductParts(int productId)
        {
            string sqlstrSpec = "select id,name,no1,no2,no3,no4,no5,product_id,plc_id,stage,serial from sys_product_parts where product_id=" + productId;
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_product_info");

            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<SysProductParts> sysProductPartsList = new List<SysProductParts>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {
                    SysProductParts sysProductParts = convertToModel(dsSpectrum.Tables[0].Rows[i]);
                    sysProductPartsList.Add(sysProductParts);
                }
                return sysProductPartsList;
            }
            return null;
        }
        public bool insert(SysProductParts sysProductParts)
        {
            string sqlstr = "insert into sys_product_parts(name,no1,no2,no3,no4,no5,product_id,plc_id,stage,serial) " +
            " values ('" + sysProductParts.Name + "','" +
           sysProductParts.No1 + "','" +
           sysProductParts.No2 + "','" +
           sysProductParts.No3 + "','" +
           sysProductParts.No4 + "','" +
          sysProductParts.No5 + "','" +
          sysProductParts.productId + "','" +
          sysProductParts.plcId + "','" +
          sysProductParts.stage + "','" +
          sysProductParts.serial + "'" +
            ")";
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            //=========================
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool update(SysProductParts sysProductParts)
        {
            string sqlstr = "update sys_product_parts set name = '" +
                            sysProductParts.Name.ToString() + "',no1 = '" +
                            sysProductParts.No1.ToString() + "',no2= '" +
                            sysProductParts.No2.ToString() + "',no3 = '" +
                            sysProductParts.No3.ToString() + "',no4 = '" +
                            sysProductParts.No4.ToString() + "',no5 = '" +
                            sysProductParts.No5.ToString() + "',product_id = '" +
                            sysProductParts.productId.ToString() + "',plc_id = '" +
                            sysProductParts.plcId.ToString() + "',stage = '" +
                            sysProductParts.stage.ToString() + "',serial = '" +
                            sysProductParts.serial.ToString() +
                            "' where id =" + sysProductParts.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool delete(SysProductParts sysProductParts)
        {
            return delete(sysProductParts.ID);
        }
        public bool delete(int partsId)
        {
            string sqldel = "delete from sys_product_parts where id = " + partsId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }
        public bool deleteProduct(int productId)
        {
            string sqldel = "delete from sys_product_parts where product_id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        private SysProductParts convertToModel(DataRow dataRow)
        {
            SysProductParts sysProductParts = new SysProductParts();
            sysProductParts.Name = dataRow["name"].ToString();
            sysProductParts.No1 = dataRow["no1"].ToString();
            sysProductParts.No2 = dataRow["no2"].ToString();
            sysProductParts.No3 = dataRow["no3"].ToString();
            sysProductParts.No4 = dataRow["no4"].ToString();
            sysProductParts.No5 = dataRow["no5"].ToString();
            sysProductParts.productId = int.Parse(dataRow["product_id"].ToString());
            sysProductParts.plcId = int.Parse(dataRow["plc_Id"].ToString());
            sysProductParts.stage = dataRow["stage"].ToString();
            sysProductParts.serial = dataRow["serial"].ToString();


            sysProductParts.ID = int.Parse(dataRow["id"].ToString());
            return sysProductParts;
        }


    }


}

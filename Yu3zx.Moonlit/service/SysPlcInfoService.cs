using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
   public class SysPlcInfoService:BaseService
    {


        public List<SysPlcInfo> list(string name)
        {
            string sqlstrSpec = "select id,name,ip,port,station,del_address,total_address,parts_address,valid_address,write_address,info,create_time,alter_time,op_id,op_name from sys_plc_info where name like  '%" + name + "%'";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_product_info");
            
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<SysPlcInfo> productList= new List<SysPlcInfo>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {
                    SysPlcInfo spi = new SysPlcInfo();
                    spi.Name = dsSpectrum.Tables[0].Rows[i]["name"].ToString();
                    spi.Ip = dsSpectrum.Tables[0].Rows[i]["ip"].ToString();
                    spi.Port = dsSpectrum.Tables[0].Rows[i]["port"].ToString();
                    spi.Station = dsSpectrum.Tables[0].Rows[i]["station"].ToString();
                    spi.delAddress = dsSpectrum.Tables[0].Rows[i]["del_address"].ToString();
                    spi.totalAddress = dsSpectrum.Tables[0].Rows[i]["total_address"].ToString();
                    spi.partsAddress = dsSpectrum.Tables[0].Rows[i]["parts_address"].ToString();
                    spi.validAddress = dsSpectrum.Tables[0].Rows[i]["valid_address"].ToString();
                    spi.writeAddress = dsSpectrum.Tables[0].Rows[i]["write_address"].ToString();
                    spi.Info = dsSpectrum.Tables[0].Rows[i]["info"].ToString();
                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString()))
                    {
                        //spi.createTime = DateTime.Parse(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString());
                        DateTime tempDate;
                        DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString(), out tempDate);
                        spi.createTime = tempDate;
                    }
                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["alter_time"].ToString()))
                    {
                        //spi.alterTime = DateTime.Parse(dsSpectrum.Tables[0].Rows[i]["alter_time"].ToString());
                        DateTime tempDate;
                        DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["alter_time"].ToString(), out tempDate);
                        spi.alterTime = tempDate;
                    }
                    spi.opId = dsSpectrum.Tables[0].Rows[i]["op_id"].ToString();
                    spi.opName = dsSpectrum.Tables[0].Rows[i]["op_name"].ToString();



                    spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    productList.Add(spi);
                }
                return productList;
            }
            else
            {
                return null;
            }
        }

        public bool insert(SysPlcInfo modelObj)
        {
                string sqlstr = "insert into sys_plc_info(name,ip,port,station,del_address,total_address,parts_address,valid_address,write_address,info,create_time,alter_time,op_id,op_name) " +
                " values ('" + modelObj.Name + "','" +
                    modelObj.Ip + "','" +
                    modelObj.Port + "','" +
                    modelObj.Station + "','" +
                    modelObj.delAddress + "','" +
                    modelObj.totalAddress + "','" +
                    modelObj.partsAddress + "','" +
                    modelObj.validAddress + "','" +
                    modelObj.writeAddress + "','" +
                    modelObj.Info + "','" +
                    modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                    modelObj.alterTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                    modelObj.opId + "','" +
                    modelObj.opName + "'" +

                ")";
                int result=MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            //=========================
            if (result==-1) {//当出现异常时返回错误信息
                return false;
            }
            return true;
            }

        public bool update(SysPlcInfo modelObj)
        {
            string sqlstr = "update sys_plc_info set " + " name = '" + modelObj.Name.ToString() + "',"
            + " ip = '" + modelObj.Ip.ToString() + "',"
            + " port = '" + modelObj.Port.ToString() + "',"
            + " station = '" + modelObj.Station.ToString() + "',"
            + " del_address = '" + modelObj.delAddress.ToString() + "',"
            + " total_address = '" + modelObj.totalAddress.ToString() + "',"
            + " parts_address = '" + modelObj.partsAddress.ToString() + "',"
            + " valid_address = '" + modelObj.validAddress.ToString() + "',"
            + " write_address = '" + modelObj.writeAddress.ToString() + "',"
            + " info = '" + modelObj.Info.ToString() + "',"
            + " create_time = '" + modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "',"
            + " alter_time = '" + modelObj.alterTime.ToString("yyyy-MM-dd HH:mm:ss") + "',"
            + " op_id = '" + modelObj.opId.ToString() + "',"
            + " op_name = '" + modelObj.opName.ToString() + "'"+

                            " where id =" + modelObj.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool delete(SysPlcInfo modelObj)
        {
            return delete(modelObj.ID);
        }
        public bool delete(int productId)
        {
            string sqldel = "delete from sys_plc_info where id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }
    }


}

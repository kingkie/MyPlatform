using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
   public class SysBaseInfoService:BaseService
    {

        public SysBaseInfo getBaseInfo()
        {
            string sqlstrSpec = "select id,collect_rate,logo_path,company_name,workshop_name,line_name,sign,qrcode_length,create_time,alter_time,op_id,op_name from sys_base_info ";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_base_info");
            
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                    SysBaseInfo spi = new SysBaseInfo();
                spi.collectRate = dsSpectrum.Tables[0].Rows[0]["collect_rate"].ToString();
                spi.logoPath = dsSpectrum.Tables[0].Rows[0]["logo_path"].ToString();
                spi.companyName = dsSpectrum.Tables[0].Rows[0]["company_name"].ToString();
                spi.workshopName = dsSpectrum.Tables[0].Rows[0]["workshop_name"].ToString();
                spi.lineName = dsSpectrum.Tables[0].Rows[0]["line_name"].ToString();
                spi.Sign = dsSpectrum.Tables[0].Rows[0]["sign"].ToString();
                spi.qrcodeLength = dsSpectrum.Tables[0].Rows[0]["qrcode_length"].ToString();

                if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[0]["create_time"].ToString()))
                {
                    DateTime tempDate;
                    DateTime.TryParse(dsSpectrum.Tables[0].Rows[0]["create_time"].ToString(), out tempDate);
                    spi.createTime = tempDate;
                }
                if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[0]["alter_time"].ToString()))
                {
                    DateTime tempDate;
                    DateTime.TryParse(dsSpectrum.Tables[0].Rows[0]["alter_time"].ToString(), out tempDate);
                    spi.alterTime = tempDate;
                }
                spi.opId = dsSpectrum.Tables[0].Rows[0]["op_id"].ToString();
                spi.opName = dsSpectrum.Tables[0].Rows[0]["op_name"].ToString();

                return spi;
            }
            else
            {
                return null;
            }
        }

        public bool insert(SysBaseInfo modelObj)
        {
                string sqlstr = "insert into sys_base_info(collect_rate,logo_path,company_name,workshop_name,line_name,sign,qrcode_length,create_time,alter_time,op_id,op_name) " +
                " values ('" + modelObj.collectRate + "','" +
                    modelObj.logoPath + "','" +
                    modelObj.companyName + "','" +
                    modelObj.workshopName + "','" +
                    modelObj.lineName + "','" +
                    modelObj.Sign + "','" +
                    modelObj.qrcodeLength + "','" +
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

        public bool update(SysBaseInfo modelObj)
        {
            string sqlstr = "update sys_base_info set collect_rate= '" + modelObj.collectRate.ToString() + "'," +
                "logo_path = '" +modelObj.logoPath.ToString() + "'," +
                "company_name = '" +modelObj.companyName.ToString() + "'," +
                "workshop_name = '" +modelObj.workshopName.ToString() + "'," +
                "line_name = '" +modelObj.lineName.ToString() + "'," +
                "sign = '" +modelObj.Sign.ToString() + "'," +
                "qrcode_length = '" +modelObj.qrcodeLength.ToString() + "'," +
                "create_time = '" +modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "alter_time = '" +modelObj.alterTime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "op_id = '" +modelObj.opId.ToString() + "'," +
                "op_name = '" +modelObj.opName.ToString() + "' " +
                " where id =" + modelObj.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool delete(SysBaseInfo modelObj)
        {
            return delete(modelObj.ID);
        }
        public bool delete(int productId)
        {
            string sqldel = "delete from sys_base_info where id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }
    }


}

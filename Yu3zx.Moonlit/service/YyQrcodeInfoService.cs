using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
   public class YyQrcodeInfoService:BaseService
    {


        public List<YyQrcodeInfo> list(string name)
        {

            string sqlstrSpec = "select id";
                        sqlstrSpec+=" ,total_code";
                        sqlstrSpec+=" ,total_qr_code";
                        sqlstrSpec+=" ,print_status";
                        sqlstrSpec+=" ,create_time";
                        sqlstrSpec+=" ,print_time";
                        sqlstrSpec+=" ,type";
                        sqlstrSpec+=" ,source_ip";
                        sqlstrSpec+=" ,qualified_status";
                        sqlstrSpec+=" ,length";
                        sqlstrSpec+= " from yy_qrcode_info ";

            if (name != null && name.Trim() != "")
            {
                sqlstrSpec += " where total_code like  '%" + name + "%' ";
            }
            sqlstrSpec += " order by id desc limit 0,3000 ";

            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "yy_qrcode_info");
            
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<YyQrcodeInfo> yyQrcodeInfoList= new List<YyQrcodeInfo>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {

                    YyQrcodeInfo spi = new YyQrcodeInfo();
                                                                    
                                                                    spi.totalCode = dsSpectrum.Tables[0].Rows[i]["total_code"].ToString();
                        
                                                                    spi.totalQrCode = dsSpectrum.Tables[0].Rows[i]["total_qr_code"].ToString();
                        
                                                                    spi.printStatus = dsSpectrum.Tables[0].Rows[i]["print_status"].ToString();
                        
                                                                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString())){
                            DateTime tempDate;
                            DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString(), out tempDate);
                            spi.createTime = tempDate;
                        }
                        
                                                                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["print_time"].ToString())){
                            DateTime tempDate;
                            DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["print_time"].ToString(), out tempDate);
                            spi.printTime = tempDate;
                        }
                        
                                                                    spi.Type = dsSpectrum.Tables[0].Rows[i]["type"].ToString();
                        
                                                                    spi.sourceIp = dsSpectrum.Tables[0].Rows[i]["source_ip"].ToString();
                        
                                                                    spi.qualifiedStatus = dsSpectrum.Tables[0].Rows[i]["qualified_status"].ToString();
                        
                                                                    spi.Length = dsSpectrum.Tables[0].Rows[i]["length"].ToString();
                        
                    
                    spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    yyQrcodeInfoList.Add(spi);
                }
                return yyQrcodeInfoList;
            }
            else
            {
                return null;
            }
        }

        public bool insert(YyQrcodeInfo modelObj)
        {
                string sqlstr = "insert into yy_qrcode_info(";
                                                                                                                    sqlstr+=" total_code";
                                                                                sqlstr+=" ,total_qr_code";
                                                                                sqlstr+=" ,print_status";
                                                                                sqlstr+=" ,create_time";
                                                                                sqlstr+=" ,print_time";
                                                                                sqlstr+=" ,type";
                                                                                sqlstr+=" ,source_ip";
                                                                                sqlstr+=" ,qualified_status";
                                                                                sqlstr+=" ,length";
                                    
                sqlstr+=") values ('";
                                                                    
                                                                    sqlstr+= modelObj.totalCode + "','";
                        
                                                                    sqlstr+= modelObj.totalQrCode + "','";
                        
                                                                    sqlstr+= modelObj.printStatus + "','";
                        
                                                                    sqlstr+= modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        
                                                                    sqlstr+= modelObj.printTime.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        
                                                                    sqlstr+= modelObj.Type + "','";
                        
                                                                    sqlstr+= modelObj.sourceIp + "','";
                        
                                                                    sqlstr+= modelObj.qualifiedStatus + "','";
                        
                                                                    sqlstr+= modelObj.Length + "'";
                        
                    
                sqlstr+=")";
                int result=MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            //=========================
            if (result==-1) {//当出现异常时返回错误信息
                return false;
            }
            return true;
            }

        public bool update(YyQrcodeInfo modelObj)
        {
            string sqlstr = "update yy_qrcode_info set ";
                                            
                                            sqlstr+=" total_code='"+modelObj.totalCode .ToString() + "',";
                
                                            sqlstr+=" total_qr_code='"+modelObj.totalQrCode .ToString() + "',";
                
                                            sqlstr+=" print_status='"+modelObj.printStatus .ToString() + "',";
                
                                            sqlstr+=" create_time='"+modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                
                                            sqlstr+=" print_time='"+modelObj.printTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                
                                            sqlstr+=" type='"+modelObj.Type .ToString() + "',";
                
                                            sqlstr+=" source_ip='"+modelObj.sourceIp .ToString() + "',";
                
                                            sqlstr+=" qualified_status='"+modelObj.qualifiedStatus .ToString() + "',";
                
                                            sqlstr+=" length='"+modelObj.Length .ToString() + "'";
                
                        sqlstr+=" where id =" + modelObj.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool delete(YyQrcodeInfo modelObj)
        {
            return delete(modelObj.ID);
        }
        public bool delete(int productId)
        {
            string sqldel = "delete from yy_qrcode_info where id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }
    }


}

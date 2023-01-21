using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
   public class YyCollectInfoBakService:BaseService
    {


        public List<YyCollectInfoBak> list(string name)
        {

            string sqlstrSpec = "select id";
                        sqlstrSpec+=" ,total_code";
                        sqlstrSpec+=" ,parts_code";
                        sqlstrSpec+=" ,status";
                        sqlstrSpec+=" ,create_time";
                        sqlstrSpec+=" ,del_time";
                        sqlstrSpec+=" ,source_type";
                        sqlstrSpec+=" ,source_plc_ip";
                        sqlstrSpec+=" ,op_id";
                        sqlstrSpec+=" ,bak_time";
                        sqlstrSpec+="from yy_collect_info_bak  where name like  '%" + name + "%'";


            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "yy_collect_info_bak");
            
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<YyCollectInfoBak> yyCollectInfoBakList= new List<YyCollectInfoBak>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {

                    YyCollectInfoBak spi = new YyCollectInfoBak();
                                                                    
                                                                    spi.totalCode = dsSpectrum.Tables[0].Rows[i]["total_code"].ToString();
                        
                                                                    spi.partsCode = dsSpectrum.Tables[0].Rows[i]["parts_code"].ToString();
                        
                                                                    spi.Status = dsSpectrum.Tables[0].Rows[i]["status"].ToString();
                        
                                                                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString())){
                            DateTime tempDate;
                            DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString(), out tempDate);
                            spi.createTime = tempDate;
                        }
                        
                                                                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["del_time"].ToString())){
                            DateTime tempDate;
                            DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["del_time"].ToString(), out tempDate);
                            spi.delTime = tempDate;
                        }
                        
                                                                    spi.sourceType = dsSpectrum.Tables[0].Rows[i]["source_type"].ToString();
                        
                                                                    spi.sourcePlcIp = dsSpectrum.Tables[0].Rows[i]["source_plc_ip"].ToString();
                        
                                                                    spi.opId = dsSpectrum.Tables[0].Rows[i]["op_id"].ToString();
                        
                                                                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["bak_time"].ToString())){
                            DateTime tempDate;
                            DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["bak_time"].ToString(), out tempDate);
                            spi.bakTime = tempDate;
                        }
                        
                    
                    spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    yyCollectInfoBakList.Add(spi);
                }
                return yyCollectInfoBakList;
            }
            else
            {
                return null;
            }
        }

        public bool insert(YyCollectInfoBak modelObj)
        {
                string sqlstr = "insert into yy_collect_info_bak(";
                                                                                                                    
                        sqlstr+=" total_code";
                                                                                sqlstr+=" ,parts_code";
                                                                                sqlstr+=" ,status";
                                                                                sqlstr+=" ,create_time";
                                                                                sqlstr+=" ,del_time";
                                                                                sqlstr+=" ,source_type";
                                                                                sqlstr+=" ,source_plc_ip";
                                                                                sqlstr+=" ,op_id";
                                                                                sqlstr+=" ,bak_time";
                                    
                sqlstr+=") values ('";
                                                                    
                                                                    sqlstr+= modelObj.totalCode + "','";
                        
                                                                    sqlstr+= modelObj.partsCode + "','";
                        
                                                                    sqlstr+= modelObj.Status + "','";
                        
                                                                    sqlstr+= modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        
                                                                    sqlstr+= modelObj.delTime.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        
                                                                    sqlstr+= modelObj.sourceType + "','";
                        
                                                                    sqlstr+= modelObj.sourcePlcIp + "','";
                        
                                                                    sqlstr+= modelObj.opId + "','";
                        
                                                                    sqlstr+= modelObj.bakTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        
                    
                sqlstr+=")";
                int result=MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            //=========================
            if (result==-1) {//当出现异常时返回错误信息
                return false;
            }
            return true;
            }

        public bool update(YyCollectInfoBak modelObj)
        {
            string sqlstr = "update yy_collect_info_bak set ";
                                            
                                            sqlstr+=" total_code='"+modelObj.totalCode .ToString() + "',";
                
                                            sqlstr+=" parts_code='"+modelObj.partsCode .ToString() + "',";
                
                                            sqlstr+=" status='"+modelObj.Status .ToString() + "',";
                
                                            sqlstr+=" create_time='"+modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                
                                            sqlstr+=" del_time='"+modelObj.delTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                
                                            sqlstr+=" source_type='"+modelObj.sourceType .ToString() + "',";
                
                                            sqlstr+=" source_plc_ip='"+modelObj.sourcePlcIp .ToString() + "',";
                
                                            sqlstr+=" op_id='"+modelObj.opId .ToString() + "',";
                
                                            sqlstr+=" bak_time='"+modelObj.bakTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                
                        sqlstr+=" where id =" + modelObj.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool delete(YyCollectInfoBak modelObj)
        {
            return delete(modelObj.ID);
        }
        public bool delete(int productId)
        {
            string sqldel = "delete from yy_collect_info_bak where id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }
    }


}

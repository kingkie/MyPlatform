using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
   public class YyOpSwitchService:BaseService
    {


        public List<YyOpSwitch> list(string name)
        {

            string sqlstrSpec = "select id";
                        sqlstrSpec+=" ,action_name";
                        sqlstrSpec+=" ,action_no";
                        sqlstrSpec+=" ,create_time";
                        sqlstrSpec+=" ,op_id";
                        sqlstrSpec+=" ,op_name";
                        sqlstrSpec+=" from yy_op_switch  where name like  '%" + name + "%'";


            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "yy_op_switch");
            
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<YyOpSwitch> yyOpSwitchList= new List<YyOpSwitch>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {

                    YyOpSwitch spi = new YyOpSwitch();
                                                                    
                                                                    spi.actionName = dsSpectrum.Tables[0].Rows[i]["action_name"].ToString();
                        
                                                                    spi.actionNo = dsSpectrum.Tables[0].Rows[i]["action_no"].ToString();
                        
                                                                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString())){
                            DateTime tempDate;
                            DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString(), out tempDate);
                            spi.createTime = tempDate;
                        }
                        
                                                                    spi.opId = dsSpectrum.Tables[0].Rows[i]["op_id"].ToString();
                        
                                                                    spi.opName = dsSpectrum.Tables[0].Rows[i]["op_name"].ToString();
                        
                    
                    spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    yyOpSwitchList.Add(spi);
                }
                return yyOpSwitchList;
            }
            else
            {
                return null;
            }
        }

        public bool insert(YyOpSwitch modelObj)
        {
                string sqlstr = "insert into yy_op_switch(";
                                                                                                                    sqlstr+=" action_name";
                                                                                sqlstr+=" ,action_no";
                                                                                sqlstr+=" ,create_time";
                                                                                sqlstr+=" ,op_id";
                                                                                sqlstr+=" ,op_name";
                                    
                sqlstr+=") values ('";
                                                                    
                                                                    sqlstr+= modelObj.actionName + "','";
                        
                                                                    sqlstr+= modelObj.actionNo + "','";
                        
                                                                    sqlstr+= modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        
                                                                    sqlstr+= modelObj.opId + "','";
                        
                                                                    sqlstr+= modelObj.opName + "'";
                        
                    
                sqlstr+=")";
                int result=MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            //=========================
            if (result==-1) {//当出现异常时返回错误信息
                return false;
            }
            return true;
            }

        public bool update(YyOpSwitch modelObj)
        {
            string sqlstr = "update yy_op_switch set ";
                                            
                                            sqlstr+=" action_name='"+modelObj.actionName .ToString() + "',";
                
                                            sqlstr+=" action_no='"+modelObj.actionNo .ToString() + "',";
                
                                            sqlstr+=" create_time='"+modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                
                                            sqlstr+=" op_id='"+modelObj.opId .ToString() + "',";
                
                                            sqlstr+=" op_name='"+modelObj.opName .ToString() + "'";
                
                        sqlstr+=" where id =" + modelObj.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool delete(YyOpSwitch modelObj)
        {
            return delete(modelObj.ID);
        }
        public bool delete(int productId)
        {
            string sqldel = "delete from yy_op_switch where id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }
    }


}

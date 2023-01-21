using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
   public class YyCollectInfoService:BaseService
    {
        public List<YyCollectInfo> list(string name)
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
            sqlstrSpec += " ,product_no";
            sqlstrSpec += " ,station";
            sqlstrSpec += " from yy_collect_info  ";
            if (name!=null && name.Trim()!="") {
                sqlstrSpec += " where total_code like  '%" + name + "%' ";
            }
            sqlstrSpec += " order by id desc limit 0,3000 ";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "yy_collect_info");
            
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<YyCollectInfo> yyCollectInfoList= new List<YyCollectInfo>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {

                    YyCollectInfo spi = new YyCollectInfo();
                         
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
                    spi.ProductNo = dsSpectrum.Tables[0].Rows[i]["product_no"].ToString();
                    spi.Station = dsSpectrum.Tables[0].Rows[i]["station"].ToString();

                    spi.opId = dsSpectrum.Tables[0].Rows[i]["op_id"].ToString();
                        
                    
                    spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    yyCollectInfoList.Add(spi);
                }
                return yyCollectInfoList;
            }
            else
            {
                return null;
            }
        }

        public YyCollectInfo get(string totalcode, string partcode)
        {
            string sqlstrSpec = "select id";
            sqlstrSpec += " ,total_code";
            sqlstrSpec += " ,parts_code";
            sqlstrSpec += " ,status";
            sqlstrSpec += " ,create_time";
            sqlstrSpec += " ,del_time";
            sqlstrSpec += " ,source_type";
            sqlstrSpec += " ,source_plc_ip";
            sqlstrSpec += " ,op_id";
            sqlstrSpec += " ,product_no";
            sqlstrSpec += " ,station";
            sqlstrSpec += " from yy_collect_info  where total_code =  '" + totalcode + "' and parts_code = '" + partcode + "'";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "yy_collect_info");
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                YyCollectInfo spi = new YyCollectInfo();

                spi.totalCode = dsSpectrum.Tables[0].Rows[0]["total_code"].ToString();

                spi.partsCode = dsSpectrum.Tables[0].Rows[0]["parts_code"].ToString();

                spi.Status = dsSpectrum.Tables[0].Rows[0]["status"].ToString();

                if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[0]["create_time"].ToString()))
                {
                    DateTime tempDate;
                    DateTime.TryParse(dsSpectrum.Tables[0].Rows[0]["create_time"].ToString(), out tempDate);
                    spi.createTime = tempDate;
                }

                if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[0]["del_time"].ToString()))
                {
                    DateTime tempDate;
                    DateTime.TryParse(dsSpectrum.Tables[0].Rows[0]["del_time"].ToString(), out tempDate);
                    spi.delTime = tempDate;
                }

                spi.sourceType = dsSpectrum.Tables[0].Rows[0]["source_type"].ToString();
                spi.sourcePlcIp = dsSpectrum.Tables[0].Rows[0]["source_plc_ip"].ToString();
                spi.opId = dsSpectrum.Tables[0].Rows[0]["op_id"].ToString();
                spi.ProductNo = dsSpectrum.Tables[0].Rows[0]["product_no"].ToString();
                spi.Station = dsSpectrum.Tables[0].Rows[0]["station"].ToString();

                spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[0]["id"].ToString());

                return spi;
            }
            else
            {
                return null;
            }
        }

        public bool insert(YyCollectInfo modelObj)
        {
                string sqlstr = "insert into yy_collect_info(";
                                                                                sqlstr+=" total_code";
                                                                                sqlstr+=" ,parts_code";
                                                                                sqlstr+=" ,status";
                                                                                sqlstr+=" ,create_time";
                                                                                sqlstr+=" ,del_time";
                                                                                sqlstr+=" ,source_type";
                                                                                sqlstr+=" ,source_plc_ip";
                                                                                sqlstr+=" ,op_id";
            sqlstr += " ,product_no";
            sqlstr += " ,station";
             
            sqlstr += ") values ('";
                                                                    
                                                                    sqlstr+= modelObj.totalCode + "','";
                        
                                                                    sqlstr+= modelObj.partsCode + "','";
                        
                                                                    sqlstr+= modelObj.Status + "','";
                        
                                                                    sqlstr+= modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        
                                                                    sqlstr+= modelObj.delTime.ToString("yyyy-MM-dd HH:mm:ss") + "','";
                        
                                                                    sqlstr+= modelObj.sourceType + "','";
                        
                                                                    sqlstr+= modelObj.sourcePlcIp + "','";
                        
                                                                    sqlstr+= modelObj.opId + "','";
            sqlstr += modelObj.ProductNo + "','";
            sqlstr += modelObj.Station + "'";


            sqlstr +=")";
                int result=MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            //=========================
            if (result==-1) {//当出现异常时返回错误信息
                return false;
            }
            return true;
            }

        public bool update(YyCollectInfo modelObj)
        {
            string sqlstr = "update yy_collect_info set ";
                                            
                                            sqlstr+=" total_code='"+modelObj.totalCode .ToString() + "',";
                
                                            sqlstr+=" parts_code='"+modelObj.partsCode .ToString() + "',";
                
                                            sqlstr+=" status='"+modelObj.Status .ToString() + "',";
                
                                            sqlstr+=" create_time='"+modelObj.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                
                                            sqlstr+=" del_time='"+modelObj.delTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                
                                            sqlstr+=" source_type='"+modelObj.sourceType .ToString() + "',";
                
                                            sqlstr+=" source_plc_ip='"+modelObj.sourcePlcIp .ToString() + "',";

                                            sqlstr += " source_plc_ip='" + modelObj.sourcePlcIp.ToString() + "',";

            sqlstr += " product_no='" +ModelsUtil.toString( modelObj.ProductNo) + "',";

            sqlstr += " station='" + ModelsUtil.toString(modelObj.Station) + "',";

            sqlstr +=" op_id='"+ ModelsUtil.toString(modelObj.opId ) + "'";
                
                        sqlstr+=" where id =" + modelObj.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }
        //删除工位的上一个记录
        public bool updateLastDel(string _source_plc_ip) {
            string sqlstrSpec = "select id";
            sqlstrSpec += " ,total_code";
            sqlstrSpec += " ,parts_code";
            sqlstrSpec += " ,status";
            sqlstrSpec += " ,create_time";
            sqlstrSpec += " ,del_time";
            sqlstrSpec += " ,source_type";
            sqlstrSpec += " ,source_plc_ip";
            sqlstrSpec += " ,op_id";
            sqlstrSpec += " ,product_no";
            sqlstrSpec += " ,station";
            sqlstrSpec += " from yy_collect_info  where source_plc_ip =  '" + _source_plc_ip + "'  ";
            sqlstrSpec += " order by create_time desc limit 1";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "yy_collect_info");
            YyCollectInfo spi = null;
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                 spi = new YyCollectInfo();

                spi.totalCode = dsSpectrum.Tables[0].Rows[0]["total_code"].ToString();

                spi.partsCode = dsSpectrum.Tables[0].Rows[0]["parts_code"].ToString();

                spi.Status = dsSpectrum.Tables[0].Rows[0]["status"].ToString();

                if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[0]["create_time"].ToString()))
                {
                    DateTime tempDate;
                    DateTime.TryParse(dsSpectrum.Tables[0].Rows[0]["create_time"].ToString(), out tempDate);
                    spi.createTime = tempDate;
                }

                if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[0]["del_time"].ToString()))
                {
                    DateTime tempDate;
                    DateTime.TryParse(dsSpectrum.Tables[0].Rows[0]["del_time"].ToString(), out tempDate);
                    spi.delTime = tempDate;
                }

                spi.sourceType = dsSpectrum.Tables[0].Rows[0]["source_type"].ToString();
                spi.sourcePlcIp = dsSpectrum.Tables[0].Rows[0]["source_plc_ip"].ToString();
                spi.ProductNo = dsSpectrum.Tables[0].Rows[0]["product_no"].ToString();
                spi.Station = dsSpectrum.Tables[0].Rows[0]["station"].ToString();

                spi.opId = dsSpectrum.Tables[0].Rows[0]["op_id"].ToString();
                spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[0]["id"].ToString());

            }
            if (spi!=null && spi.Status==null || spi.Status !="0") {
                spi.Status = "0";//设置为已删除
                spi.delTime = DateTime.Now;
                this.update(spi);
                return true;
            }
            return false;
        }
        public bool delete(YyCollectInfo modelObj)
        {
            return delete(modelObj.ID);
        }
        public bool delete(int productId)
        {
            string sqldel = "delete from yy_collect_info where id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public PageBean<YyCollectInfo> Page(string name,int rowNum,int page)
        {
            PageBean < YyCollectInfo > pageObj= new PageBean<YyCollectInfo>();
            int total= count(name);
            int startNum = 0, endNum = 0;
            startNum = rowNum * page;
            endNum = rowNum * (page+1)-1;
            if (endNum> total) {
                endNum = total;
            }
            int ys= total% rowNum;

            int totalPage = 0;
            if (ys > 0)
            {
                totalPage = total / rowNum + 1;
            }
            else {
                totalPage = total / rowNum ;
            }
            pageObj.Total= total;
            pageObj.TotalPage = totalPage;
            pageObj.RowNum = rowNum;
            pageObj.Page = page;



            string sqlstrSpec = "select id";
            sqlstrSpec += " ,total_code";
            sqlstrSpec += " ,parts_code";
            sqlstrSpec += " ,status";
            sqlstrSpec += " ,create_time";
            sqlstrSpec += " ,del_time";
            sqlstrSpec += " ,source_type";
            sqlstrSpec += " ,source_plc_ip";
            sqlstrSpec += " ,op_id";
            sqlstrSpec += " ,product_no";
            sqlstrSpec += " ,station";
            sqlstrSpec += " from yy_collect_info  where total_code like  '%" + name + "%' order by id desc  limit "+ startNum + ","+ rowNum ;

            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "yy_collect_info");

            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<YyCollectInfo> yyCollectInfoList = new List<YyCollectInfo>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {

                    YyCollectInfo spi = new YyCollectInfo();

                    spi.totalCode = dsSpectrum.Tables[0].Rows[i]["total_code"].ToString();

                    spi.partsCode = dsSpectrum.Tables[0].Rows[i]["parts_code"].ToString();

                    spi.Status = dsSpectrum.Tables[0].Rows[i]["status"].ToString();

                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString()))
                    {
                        DateTime tempDate;
                        DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["create_time"].ToString(), out tempDate);
                        spi.createTime = tempDate;
                    }

                    if (!string.IsNullOrWhiteSpace(dsSpectrum.Tables[0].Rows[i]["del_time"].ToString()))
                    {
                        DateTime tempDate;
                        DateTime.TryParse(dsSpectrum.Tables[0].Rows[i]["del_time"].ToString(), out tempDate);
                        spi.delTime = tempDate;
                    }

                    spi.sourceType = dsSpectrum.Tables[0].Rows[i]["source_type"].ToString();

                    spi.sourcePlcIp = dsSpectrum.Tables[0].Rows[i]["source_plc_ip"].ToString();
                    spi.ProductNo = dsSpectrum.Tables[0].Rows[i]["product_no"].ToString();
                    spi.Station = dsSpectrum.Tables[0].Rows[i]["station"].ToString();

                    spi.opId = dsSpectrum.Tables[0].Rows[i]["op_id"].ToString();


                    spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    yyCollectInfoList.Add(spi);
                }
                pageObj.List = yyCollectInfoList;
                return pageObj;
            }
                return pageObj;
        }
        public int count(string name)
        {
            string sqlstrSpec = "select count(id) colcount from yy_collect_info  where total_code like  '%" + name + "%'";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_product_info");

            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                return (int)dsSpectrum.Tables[0].Rows[0]["colcount"];
            }
            return 0;
        }
    }


}

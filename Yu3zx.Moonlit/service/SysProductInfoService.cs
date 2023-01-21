using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.service
{
   public class SysProductInfoService:BaseService
    {


        public List<SysProductInfo> list(string productNo)
        {
            string sqlstrSpec = "select id,name,no,code,create_time,alter_time,info,print_name,code2,code3 from sys_product_info where name like  '%" + productNo + "%'";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_product_info");
            
            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                List<SysProductInfo> productList= new List<SysProductInfo>();
                for (int i = 0; i < dsSpectrum.Tables[0].Rows.Count; i++)
                {

                    SysProductInfo spi = new SysProductInfo();
                    spi.Name = dsSpectrum.Tables[0].Rows[i]["name"].ToString();
                    spi.No = dsSpectrum.Tables[0].Rows[i]["no"].ToString();
                    spi.Code = dsSpectrum.Tables[0].Rows[i]["code"].ToString();
                    spi.Code2 = dsSpectrum.Tables[0].Rows[i]["code2"].ToString();
                    spi.Code3 = dsSpectrum.Tables[0].Rows[i]["code3"].ToString();
                    spi.printName = dsSpectrum.Tables[0].Rows[i]["print_name"].ToString();
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
                    spi.ID = int.Parse(dsSpectrum.Tables[0].Rows[i]["id"].ToString());
                    spi.Info = dsSpectrum.Tables[0].Rows[i]["info"].ToString();

                    productList.Add(spi);
                }
                return productList;
            }
            else
            {
                return null;
            }
        }

        public bool insert(SysProductInfo sysProductInfo)
        {
                string sqlstr = "insert into sys_product_info(name,no,code,info,op_id,op_name,create_time,alter_time,print_name,code2,code3) " +
                " values ('" + sysProductInfo.Name + "','" +
               sysProductInfo.No + "','" +
               sysProductInfo.Code + "','" +
               sysProductInfo.Info + "','" +
               sysProductInfo.opId + "','" +
               sysProductInfo.opName + "','" +
               sysProductInfo.createTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
               sysProductInfo.alterTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
               sysProductInfo.printName + "','" +
               sysProductInfo.Code2 + "','" +
               sysProductInfo.Code3 + "'" +
                ")";

            int result=MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            //=========================
            if (result==-1) {//当出现异常时返回错误信息
                return false;
            }
            return true;
            }

        public bool update(SysProductInfo sysProductInfo)
        {
            string sqlstr = "update sys_product_info set name = '" +
                            sysProductInfo.Name.ToString() + "',no = '" +
                            sysProductInfo.No.ToString() + "',code = '" +
                            sysProductInfo.Code.ToString() + "',code2 = '" +
                            sysProductInfo.Code2.ToString()+ "',code3 = '" +
                            sysProductInfo.Code3.ToString() + "', info = '" +
                           sysProductInfo.Info.ToString() + "',print_name = '" +
                            sysProductInfo.printName.ToString() + "',alter_time = '" +
                            sysProductInfo.alterTime.ToString("yyyy-MM-dd HH:mm:ss") +
                            "' where id =" + sysProductInfo.ID + "";

            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlstr.ToString());
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public bool delete(SysProductInfo product)
        {
            return delete(product.ID);
        }
        public bool delete(int productId)
        {
            string sqldel = "delete from sys_product_info where id = " + productId;
            int result = MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqldel);
            if (result == -1)
            {//当出现异常时返回错误信息
                return false;
            }
            return true;
        }

        public SysProductInfo get(int productId)
        {
            string sqlstrSpec = "select id,name,no,code,create_time,alter_time,info,print_name,code2,code3 from sys_product_info where id=" + productId;
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_product_info");

            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                return convertToModel(dsSpectrum.Tables[0].Rows[0]);
            }
            return null;
        }
        public int maxIndex()
        {
            string sqlstrSpec = "select max(id) maxid from sys_product_info " ;
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlstrSpec, "sys_product_info");

            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                return (int)dsSpectrum.Tables[0].Rows[0]["maxid"];
            }
            return 0;
        }
        private SysProductInfo convertToModel(DataRow dataRow) {
            SysProductInfo spi = new SysProductInfo();
            spi.Name = dataRow["name"].ToString();
            spi.No = dataRow["no"].ToString();
            spi.Code = dataRow["code"].ToString();
            spi.Code2 = dataRow["code2"].ToString();
            spi.Code3 = dataRow["code3"].ToString();

            spi.printName = dataRow["print_name"].ToString();
            if (!string.IsNullOrWhiteSpace(dataRow["create_time"].ToString()))
            {
                //spi.createTime = DateTime.Parse(dsSpectrum.Tables[0].Rows[0]["create_time"].ToString());
                DateTime tempDate;
                DateTime.TryParse(dataRow["create_time"].ToString(), out tempDate);
                spi.createTime = tempDate;
            }
            if (!string.IsNullOrWhiteSpace(dataRow["alter_time"].ToString()))
            {
                //spi.alterTime = DateTime.Parse(dsSpectrum.Tables[0].Rows[0]["alter_time"].ToString());
                DateTime tempDate;
                DateTime.TryParse(dataRow["alter_time"].ToString(), out tempDate);
                spi.alterTime = tempDate;
            }
            spi.ID = int.Parse(dataRow["id"].ToString());
            spi.Info = dataRow["info"].ToString();
            return spi;
        }
        /**
         * 
         * 校验产品编号，判断所属产品 
         * 传入参数产品编码 校验参数产品识别码
         */
        public SysProductInfo getProduct(string productCode) {
            string vsql = "SELECT id,name,no,code,create_time,alter_time,info,print_name,code2,code3 FROM sys_product_info WHERE INSTR('" + productCode + "', code) > 0 and INSTR('" + productCode + "', code2) > 0 and INSTR('" + productCode + "', code3) > 0";
            DataSet dsSpectrum = MainManager.GetInstance().DbFactory.ExecuteDataset(vsql, "sys_product_info");

            if (dsSpectrum.Tables[0].Rows.Count > 0)
            {
                return convertToModel(dsSpectrum.Tables[0].Rows[0]);
            }
            return null;
        }

        /**
         * 校验部件编号是否正确，是否是当前工位扫描的
         *  返回空则校验不通过，返回对象则校验通过
         */
        public SysProductParts validateParts(string productNo,string partsNo,string stage)
        {
            if (productNo==null || productNo=="" || partsNo==null || partsNo=="" || stage==null || stage=="") {
                return null;//为空时直接不校验视为不通过
            }
            SysProductInfo sysProductInfo =getProduct(productNo);

            if (sysProductInfo!=null) {
                SysProductPartsService sysProductPartsService = (SysProductPartsService)BeanUtil.getBean("sysProductPartsService");
                List<SysProductParts> sysProductPartsList= sysProductPartsService.getProductParts(sysProductInfo.ID);
                for (int i=0;i< sysProductPartsList.Count;i++) {
                    SysProductParts sysProductParts= sysProductPartsList[i];
                    string stageParts=sysProductParts.stage;
                    if (stageParts!=null && stageParts!="" && stageParts== stage) { //找到对应配置再比较
                        if (partsNo.IndexOf(sysProductParts.No1) > -1 && partsNo.IndexOf(sysProductParts.No2) > -1 && partsNo.IndexOf(sysProductParts.No3) > -1) {
                            return sysProductParts;
                        }
                    }
                }
            }
            
            return null;
        }
        /**
         * 添加时校验
         */
        public bool addValidate(string productNo, string partsNo, string stage)
        {
            if (validateParts(productNo, partsNo, stage)!=null) {
                return true;
            }
            return false;
        }

        /**
         * 将需要校验的产品编号传进来，并传入采集的多个产品编号
         */
        public Boolean validateProduct(string productNo, List<YyCollectInfo> collectList)
        {
             string reStr = null;
             bool boolVal=validateProduct(productNo, collectList,out reStr);

            return boolVal;
        }
        /**
         * 将需要校验的产品编号传进来，并传入采集的多个产品编号
         */
        public Boolean validateProduct(string productNo, List<YyCollectInfo> collectList,out string outstr)
        {
            outstr = "";
            if (productNo == null || productNo == "" || collectList == null || collectList.Count==0)
            {
                outstr = "产品码和采集列表位空，无法进行校验";
                return false;//为空时直接不校验视为不通过
            }
            SysProductInfo sysProductInfo = getProduct(productNo);
            string tipStr = "";
            if (sysProductInfo != null)
            {
                SysProductPartsService sysProductPartsService = (SysProductPartsService)BeanUtil.getBean("sysProductPartsService");
                List<SysProductParts> sysProductPartsList = sysProductPartsService.getProductParts(sysProductInfo.ID);
                /*if (sysProductPartsList != null && sysProductPartsList.Count > collectList.Count) {//数据量不足时直接报未采集完成
                    return false;
                }*/
                bool validateBool = true;
                Dictionary<string,List<Dictionary<string, bool>>> dict = new Dictionary<string, List<Dictionary<string, bool>>>();
                Dictionary<string,int> stationCountDict = new Dictionary<string, int>();

                for (int i = 0; i < sysProductPartsList.Count; i++)
                {

                    SysProductParts sysProductParts = sysProductPartsList[i];
                    string stageParts = sysProductParts.stage;
                    List<Dictionary<string, bool>> partsList = null;
                    if (dict.ContainsKey(stageParts))
                    {
                        partsList = dict[stageParts];
                    }
                    if (partsList == null )
                    {
                        partsList = new List<Dictionary<string, bool>>();
                        dict.Add(stageParts, partsList);
                    }


                    YyCollectInfo collectParts = null;
                    int count = 0;
                    bool valPart = false;
                    for (int j = 0; j < collectList.Count; j++)
                    {
                        YyCollectInfo yyCollectInfo = collectList[j];
                        if (yyCollectInfo.sourcePlcIp == null || yyCollectInfo.sourcePlcIp == "" || yyCollectInfo.partsCode == null || yyCollectInfo.partsCode == "" || yyCollectInfo.Status=="0")
                        {//删除状态的数据不做校验
                            continue;
                        }
                        if (stageParts != null && stageParts != "" && yyCollectInfo.sourcePlcIp == stageParts)
                        {
                            //collectParts = yyCollectInfo;
                            count++;
                            if (partsCodeValidate(yyCollectInfo.partsCode, sysProductParts))
                            {
                                valPart = true;
                                collectParts = yyCollectInfo;
                            }
                            if (!valPart) {//当为true后不再记录对象 只用于计数
                                collectParts = yyCollectInfo;
                            }
                           
                        }
                    }
                    if (!stationCountDict.ContainsKey(stageParts)) {
                        stationCountDict.Add(stageParts, count);
                    }
                    if (collectParts == null)
                    { //没有找到对应的配置 失败
                       /* Dictionary<string, bool> errorDict = new Dictionary<string, bool>();
                        errorDict.Add(sysProductParts.Name,false);
                        partsList.Add(errorDict);*/
                        continue;
                        //return false;
                    }
                    
                    if (partsCodeValidate(collectParts.partsCode, sysProductParts))
                    {
                        Dictionary<string, bool> errorDict = new Dictionary<string, bool>();
                        errorDict.Add(sysProductParts.Name, true);
                        partsList.Add(errorDict);
                    }
                    else
                    {   
                        Dictionary<string, bool> errorDict = new Dictionary<string, bool>();
                        errorDict.Add(sysProductParts.Name, false);
                        partsList.Add(errorDict);
                    }
                }
                //重新循环遍历输出采集状况日志
                foreach (string key in dict.Keys) {
                    List<Dictionary<string, bool>> partDict = dict[key];
                    int collectCount = stationCountDict[key];
                    if (partDict==null || partDict.Count==0) {
                        tipStr += " 产品码：" + productNo + "在工位：" + key +  " 上数据未开始采集\r\n";
                        validateBool = false;
                        continue;
                    }
                    int boolCount = 0;
                    string boolStr = "";
                    for (int k=0,len=partDict.Count;k<len;k++) {
                        Dictionary<string, bool> dictBool=partDict[k];
                        string fkey=dictBool.Keys.First();
                        if (dictBool[fkey])
                        {
                            boolCount++;
                        }
                        else {
                            boolStr += fkey+" ";
                        }
                    }
                    if (boolCount< partDict.Count) { //当合格数小于部件数时，即为校验未通过
                        validateBool = false;
                        if (collectCount < partDict.Count)
                        {//当工位的采集数量小于产品总数时 提示工位部件未采集 
                            tipStr += " 产品码：" + productNo + "在工位：" + key + " 的部件：" + boolStr + " 数据未采集\r\n";
                        }
                        else {
                            tipStr += " 产品码：" + productNo + "在工位：" + key + " 的部件：" + boolStr + " 数据未校验通过\r\n";
                        }
                    }

                }
                if (!validateBool) {
                    outstr = tipStr;
                }
                return  validateBool;
            }
            else {
                tipStr +=  " 产品码：" + productNo + "  未获取到配置信息，无法完成校验\r\n";
            }

            outstr = tipStr;
            return false;
        }

        public bool partsCodeValidate(string collectPartsCode, SysProductParts configParts) {
            string parts1 = configParts.No1;
            string parts2 = configParts.No2;
            string parts3 = configParts.No3;
            string[] parts1arr=parts1.Split(';','/');
            bool inParts1 = false;
            foreach (string parts1Obj in parts1arr) {
                if (collectPartsCode.IndexOf(parts1Obj)>-1) {
                    inParts1 = true;
                    break;
                }
            }
            if (inParts1 && collectPartsCode.IndexOf(parts2) > -1 && collectPartsCode.IndexOf(parts3) > -1)
            {
                return true;
            }
            return false;
        }
    }


}

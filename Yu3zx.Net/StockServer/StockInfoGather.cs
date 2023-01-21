using System.Text.RegularExpressions;

namespace Yu3zx.NetBase
{
    public class StockInfoGather
    {
        /// <summary>
        /// 新浪股票信息查询 "000001"代表上证指数
        /// </summary>
        /// <param name="ShareCode"></param>
        /// <returns></returns>
        public string[] SinaStockInfoRequest(string sharecode)
        {
            string postdate = "";//post内容
            string strResult = "";
            try
            {
                string newCode = GetShareCode(sharecode);
                string posturl = "http://hq.sinajs.cn/list=" + newCode;//上证(非深证)
                postdate = "";
                strResult = NetSocket.PostUrl(posturl, postdate);

                if (strResult.Length - sharecode.Length == 17)
                {
                    return new string[] { };//不存在
                }
            }
            catch
            {
                return new string[] { };//不存在
            }
            Regex regex = new Regex("\"[^\"]*\"");
            string result = regex.Match(strResult).Value.Replace("\"", "");
            string[] strResults = result.Split(',');
            return strResults;
        }
        #region 新浪股票信息格式
        //"股票名称：" + strlist[0] + "\r\n";
        //"今日开盘价：" + strlist[1] + "\r\n";
        //"昨日收盘价：" + strlist[2] + "\r\n";
        //"当前价格：" + strlist[3] + "\r\n";
        //"今日最高价：" + strlist[4] + "\r\n";
        //"今日最低价：" + strlist[5] + "\r\n";
        //"竞买价：" + strlist[6] + "\r\n";
        //"竞卖价：" + strlist[7] + "\r\n";
        //"成交股票数：" + strlist[8] + "\r\n";
        //"成交金额：" + strlist[9] + "\r\n";
        //"买一：" + strlist[10] + "\r\n";
        //"买一报价：" + strlist[11] + "\r\n";
        //"买二：" + strlist[12] + "\r\n";
        //"买二报价：" + strlist[13] + "\r\n";
        //"买三：" + strlist[14] + "\r\n";
        //"买三报价：" + strlist[15] + "\r\n";
        //"买四：" + strlist[16] + "\r\n";
        //"买四报价：" + strlist[17] + "\r\n";
        //"买五：" + strlist[18] + "\r\n";
        //"买五报价：" + strlist[19] + "\r\n";
        //"卖一：" + strlist[20] + "\r\n";
        //"卖一报价：" + strlist[21] + "\r\n";
        //"卖二：" + strlist[22] + "\r\n";
        //"卖二报价：" + strlist[23] + "\r\n";
        //"卖三：" + strlist[24] + "\r\n";
        //"卖三报价：" + strlist[25] + "\r\n";
        //"卖四：" + strlist[26] + "\r\n";
        //"卖四报价：" + strlist[27] + "\r\n";
        //"卖五：" + strlist[28] + "\r\n";
        //"卖五报价：" + strlist[29] + "\r\n";
        //"日期：" + strlist[30] + "\r\n";
        //"时间：" + strlist[31] + "\r\n";

        //:var hq_str_sh600835="上海机电,19.980,19.970,19.930,
        //19.980,19.860,19.910,19.920,3597082,71587064.000,42600,
        //19.910,36000,19.900,13200,19.890,23400,19.880,21197,
        //19.870,800,19.920,10700,19.940,12400,19.950,2000,19.960,
        //4800,19.970,2017-09-08,15:00:00,00";
        //==============上证指数计算方法======================
        //string[] strlist = strResult.Split(',');
        //textBox1.Text += strlist[3];
        //int z = ((int)Convert.ToSingle(strlist[3])- zz+50)*100-4900;
        //zz = (int)Convert.ToSingle(strlist[3]);
        //this.label1.Text = z.ToString();
        //i+=3;
        //if(z != zz)
        //this.graph1.CanelDrawing(i, z, 5);
        //指数名称，当前指数，今日变化值，今日变化百分比，成交量（手），成交额（万元）
        #endregion End 

        /// <summary>
        /// 腾讯证券接口
        /// </summary>
        /// <returns></returns>
        public string[] QQStockInfoRequest(string sharecode)
        {
            string[] strResults;
            string newCode = GetShareCode(sharecode);
            string posturl = @"http://qt.gtimg.cn/q=" + newCode;//http://qt.gtimg.cn/q=sz000858
            string postdate = "";
            string strResult = NetSocket.PostUrl(posturl, postdate);
            Regex regex = new Regex("\"[^\"]*\"");
            string result = regex.Match(strResult).Value.Replace("\"", "");
            strResults = result.Split('~');
            return strResults;
        }
        #region QQ证券接口
        //v_sz000858="51~五 粮 液~000858~67.37~67.20~66.99~216315~111252~104920~67.37~529~67.36~4~67.35~205~67.34~5~67.33~26~67.38~187~67.39~131~67.40~553~67.41~38~67.42~46~15:00:04/67.37/3015/S/20312055/9630|14:57:00/67.37/25/B/168390/9543|14:56:57/67.35/14/M/94291/9541|14:56:54/67.38/66/B/444571/9539|14:56:52/67.35/26/B/175115/9538|14:56:49/67.35/31/B/208738/9536~20180927150048~0.17~0.25~67.60~66.43~67.37/216315/1449181805~216315~144918~0.57~22.14~~67.60~66.43~1.74~2557.21~2615.04~4.57~73.92~60.48~0.70~-186~66.99~18.39~27.03"
        // 0: 未知（其它字符）＝51
        // 1: 股票名字
        // 2: 股票代码 ＝000858
        // 3: 当前价格 27.78
        // 4: 昨收 27.60
        // 5: 今开 27.70
        // 6: 成交量（手）
        // 7: 外盘
        // 8: 内盘
        // 9: 买一
        //10: 买一量（手）
        //11-18: 买二 买五
        //19: 卖一
        //20: 卖一量
        //21-28: 卖二 卖五
        //29: 最近逐笔成交，（有好几段）
        //30: 时间
        //31: 涨跌 点数
        //32: 涨跌%
        //33: 最高
        //34: 最低
        //35: 价格/成交量（手）/成交额
        //36: 成交量（手）
        //37: 成交额（万）
        //38: 换手率
        //39: 市盈率？变动
        //40: 
        //41: 最高
        //42: 最低
        //43: 振幅？
        //44: 流通市值
        //45: 总市值
        //46: 市净率（动？）
        //47: 涨停价
        //48: 跌停价
        //49: 量比
        #endregion End

        /// <summary>
        /// QQ获取实时资金流向
        /// </summary>
        /// <param name="sharecode"></param>
        /// <returns></returns>
        public string[] QQStockRealMoney(string sharecode)
        {
            string[] strResults;
            string newCode = GetShareCode(sharecode);
            string posturl = @"http://qt.gtimg.cn/q=ff_" + newCode;//http://qt.gtimg.cn/q=ff_sz000858
            string postdate = "";
            string strResult = NetSocket.PostUrl(posturl, postdate);
            Regex regex = new Regex("\"[^\"]*\"");
            string result = regex.Match(strResult).Value.Replace("\"", "");
            strResults = result.Split('~');
            return strResults;
        }
        #region 实时资金流向
        //v_ff_sz000858="sz000858~41773.67~48096.67~-6322.99~-5.53
        //~10200.89~14351.02~-4150.13~-3.63~114422.25~53015.90~59770.57~五 粮 液~20121221"; 
        // 0: 代码  
        // 1: 主力流入  
        // 2: 主力流出  
        // 3: 主力净流入  
        // 4: 主力净流入/资金流入流出总和  
        // 5: 散户流入  
        // 6: 散户流出  
        // 7: 散户净流入  
        // 8: 散户净流入/资金流入流出总和  
        // 9: 资金流入流出总和1+2+5+6  
        //10: 未知  
        //11: 未知  
        //12: 股票名字  
        //13: 日期  
        //14: 前一交易日
        //15: 前二交易日
        //16: 前三交易日
        //17: 前四交易日
        //18
        //19
        //20: 数据日期时间
        #endregion End

        /// <summary>
        /// 获取盘口信息-即时买卖信息
        /// </summary>
        /// <param name="sharecode"></param>
        /// <returns></returns>
        public string[] QQStockPKinfo(string sharecode)
        {
            string[] strResults;
            string newCode = GetShareCode(sharecode);
            string posturl = @"http://qt.gtimg.cn/q=s_pk" + newCode;//http://qt.gtimg.cn/q=s_pksz000858
            string postdate = "";
            string strResult = NetSocket.PostUrl(posturl, postdate);
            Regex regex = new Regex("\"[^\"]*\"");
            string result = regex.Match(strResult).Value.Replace("\"", "");
            strResults = result.Split('~');
            return strResults;
        }
        #region 盘口信息
        //v_s_pksz000858="0.196~0.258~0.221~0.325";  
        //0: 买盘大单  
        //1: 买盘小单  
        //2: 卖盘大单  
        //3: 卖盘小单
        #endregion End

        /// <summary>
        /// 获取简要信息
        /// </summary>
        /// <param name="sharecode"></param>
        /// <returns></returns>
        public string[] QQStockIncInfo(string sharecode)
        {
            string[] strResults;
            string newCode = GetShareCode(sharecode);
            string posturl = @"http://qt.gtimg.cn/q=s_" + newCode;//http://qt.gtimg.cn/q=s_sz000858
            string postdate = "";
            string strResult = NetSocket.PostUrl(posturl, postdate);
            Regex regex = new Regex("\"[^\"]*\"");
            string result = regex.Match(strResult).Value.Replace("\"", "");
            strResults = result.Split('~');
            return strResults;
        }
        #region 盘口信息2
        //v_s_sz000858="51~五 粮 液~000858~27.78~0.18~0.65~417909~116339~~1054.52";  
        //0: 未知（指令码）  
        //1: 股票名字  
        //2: 股票代码  
        //3: 当前价格  
        //4: 涨跌额（元）
        //5: 涨跌%  
        //6: 成交量（手）  
        //7: 成交额（万）  
        //8:   
        //9: 总市值
        #endregion End

        private string GetShareCode(string oldCode)
        {
            string newCode = "";
            if (oldCode.Length < 2)
                return oldCode;
            string bChars = oldCode.Substring(0, 2);
            bChars = bChars.ToLower();
            switch (bChars)
            {
                case "60":
                    newCode = "sh" + oldCode;//上海证券
                    break;
                case "00":
                    newCode = "sz" + oldCode;//深圳证券
                    break;
                case "sh":
                    newCode = oldCode;
                    break;
                case "sz":
                    newCode = oldCode;
                    break;
                default:
                    newCode = oldCode;
                    break;
            }
            return newCode;
        }
        #region 股票代码说明
        //600开头的股票是上证A股，属于大盘股，其中6006开头的股票是最早上市的股票，
        //6016开头的股票为大盘蓝筹股;
        //900开头的股票是上证B股;
        //000开头的股票是深证A股，001、002开头的股票也都属于深证A股，其中002开头的股票是深证A股中小企业股票;
        //200开头的股票是深证B股;
        //300开头的股票是创业板股票;
        //400开头的股票是三板市场股票。
        //沪市权证以580开头，深市权证以031开头;
        //新股申购代码：沪市以730开头，深市新股申购的代码与深市股票买卖代码一样;
        //配股代码：沪市以700开头，深市以080开头。
        //沪市新股申购的代码是以730打头，
        //权证，沪市是580打头深市是031打头。
        //深市新股申购的代码与深市股票买卖代码一样，如：中信证券在深市市值配售代码是003030。
        //上证A股的代码是以600、601或603打头。
        //沪市B股的代码是以900打头，
        //......
        #endregion End
    }
}

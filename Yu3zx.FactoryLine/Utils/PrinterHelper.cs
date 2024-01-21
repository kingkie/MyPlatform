using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;

using FastReport;
using FastReport.Data;
using FastReport.Utils;

using Yu3zx.FactoryLine.DataModels;


namespace Yu3zx.FactoryLine.Utils
{
    public class PrinterHelper
    {
        /// <summary>
        /// 打印总垛数据，并移除打印的,打印报表
        /// </summary>
        /// <param name="packNum">总包数装成垛</param>
        public static void PrintFabricList(List<CartonBoxInfo> lCartons)
        {
            try
            {
                //一垛总包数
                List<BoxDetail> Boxes = new List<BoxDetail>();
                //增加装箱信息
                //BoxInfo info = new BoxInfo();
                //List<BoxInfo> BoxInfos = new List<BoxInfo>();
                string strBatchNo = string.Empty;
                string strColorNum = string.Empty;
                string strQualityString = string.Empty;
                string strSpecs = string.Empty;
                decimal sumRoll = 0;
                try
                {
                    for (int i = 0; i < lCartons.Count; i++)
                    {
                        CartonBoxInfo carton = lCartons[i];
                        string strReelNums = carton.ReelNums;
                        string strProdLens = carton.ProdLens;
                        List<string> lReelNums = new List<string>();
                        List<string> lProdLens = new List<string>();
                        if(!string.IsNullOrEmpty(strReelNums))
                        {
                            lReelNums.AddRange(strReelNums.Split(new string[] { "" }, StringSplitOptions.RemoveEmptyEntries));
                        }
                        if (!string.IsNullOrEmpty(strProdLens))
                        {
                            lProdLens.AddRange(strProdLens.Split(new string[] { "" }, StringSplitOptions.RemoveEmptyEntries));
                        }

                        BoxDetail detail = new BoxDetail();
                        for (int j = 0; j < Math.Max(lReelNums.Count, lProdLens.Count); j++)
                        {
                            if (j == 0 && i == 0)
                            {
                                strBatchNo = carton.BatchNo;
                                strColorNum = carton.ColorNum;
                                strQualityString = carton.QualityString;
                                strSpecs = carton.Specs;
                            }
                            int iReel = 0;
                            decimal dLen = 0.0M;

                            if(lReelNums.Count > j)
                            {
                                try
                                {
                                    iReel = int.Parse(lReelNums[j]);
                                }
                                catch
                                { }
                            }
                            if(lProdLens.Count > j)
                            {
                                try
                                {
                                    dLen = decimal.Parse(lProdLens[j]);
                                }
                                catch
                                { }
                            }

                            switch (j)
                            {
                                case 0:
                                    detail.RollNum1 = dLen;//
                                    detail.ReelNum1 = iReel;
                                    break;
                                case 1:
                                    detail.RollNum2 = dLen;
                                    detail.ReelNum2 = iReel;
                                    break;
                                case 2:
                                    detail.RollNum3 = dLen;
                                    detail.ReelNum3 = iReel;
                                    break;
                                case 3:
                                    detail.RollNum4 = dLen;
                                    detail.ReelNum4 = iReel;
                                    break;
                                case 4:
                                    detail.RollNum5 = dLen;
                                    detail.ReelNum5 = iReel;
                                    break;
                                case 5:
                                    detail.RollNum6 = dLen;
                                    detail.ReelNum6 = iReel;
                                    break;
                            }
                        }

                        sumRoll = sumRoll + detail.RollSum;//总计
                        detail.BoxNum = carton.BoxNum;
                        Boxes.Add(detail);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("L897:" + ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                //-------------------
                var FDataSet = new DataSet();
                DataTable table = ListToDataTable(Boxes);
                table.TableName = "Products";
                FDataSet.Tables.Add(table);
                string filePath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory , "\\Report\\cartonreport.frx");
                if (File.Exists(filePath))
                {
                }
                else
                {
                    //Log.Instance.LogWrite("L 1.0,文件不存在:" + filePath);
                    MessageBox.Show("L 1.0,文件不存在:" + filePath);
                    return;
                }
                Report report = new Report();
                report.Load(filePath);

                Config.ReportSettings.ShowProgress = false;//不显示进度

                Parameter BatchNo = new Parameter();
                BatchNo.Name = "BatchNo";
                BatchNo.DataType = typeof(string);
                BatchNo.Value = strBatchNo;

                Parameter colorNum = new Parameter();
                colorNum.Name = "ColorNum";
                colorNum.DataType = typeof(string);
                colorNum.Value = strColorNum;

                Parameter QualityString = new Parameter();
                QualityString.Name = "QualityString";
                QualityString.DataType = typeof(string);
                QualityString.Value = strQualityString;

                Parameter Specs = new Parameter();
                Specs.Name = "Specs";
                Specs.DataType = typeof(string);
                Specs.Value = strSpecs;

                Parameter paraTotal = new Parameter();
                paraTotal.Name = "ParaTotal";
                paraTotal.DataType = typeof(string);
                paraTotal.Value = sumRoll.ToString();

                report.Parameters.Add(BatchNo);
                report.Parameters.Add(colorNum);
                report.Parameters.Add(QualityString);
                report.Parameters.Add(Specs);
                report.Parameters.Add(paraTotal);
                report.RegisterData(FDataSet, "NorthWind");//NorthWind
                report.SmoothGraphics = true;

                report.Prepare();
                report.PrintSettings.ShowDialog = false;

                report.Print();
                report.Dispose();
                Console.WriteLine("总包数打印！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("L977:" + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
    }
}

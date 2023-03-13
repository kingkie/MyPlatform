using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yu3zx.DapperExtend;
using Yu3zx.FactoryLine.Models;

namespace Yu3zx.FactoryLine.Views
{
    /// <summary>
    /// ProductPlanSearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProductPlanSearchPage : Page
    {
        public ProductPlanSearchPage()
        {
            InitializeComponent();
            dgView.LoadingRow += DgView_LoadingRow;
        }

        private void DgView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int lNum = (cboProduceLine.SelectedIndex + 1);
                using (var db = new DapperContext("MySqlDbConnection"))
                {
                    if (chkTime.IsChecked == true)
                    {
                        string strBatch = txtBatchNo.Text.Trim();
                        if (string.IsNullOrEmpty(strBatch))
                        {
                            var resultt = db.Select<ProductPlan>(u => u.ProduceTime >= DateTime.Parse(dptBegin.DateTimeStr) && u.ProduceTime <= DateTime.Parse(dptEnd.DateTimeStr) && (lNum > 0? u.LineNum == lNum.ToString(): true));
                            dgView.ItemsSource = resultt;
                        }
                        else
                        {
                            var resultt1 = db.Select<ProductPlan>(u => u.ProduceTime >= DateTime.Parse(dptBegin.DateTimeStr) && u.ProduceTime <= DateTime.Parse(dptEnd.DateTimeStr) && u.BatchNo == strBatch && (lNum > 0 ? u.LineNum == lNum.ToString() : true));
                            dgView.ItemsSource = resultt1;
                        }
                    }
                    else
                    {
                        string strBatchNo = txtBatchNo.Text.Trim();
                        if (string.IsNullOrEmpty(strBatchNo))
                        {
                            var result = db.Select<ProductPlan>(u=>u.LineNum == (cboProduceLine.SelectedIndex + 1).ToString());
                            dgView.ItemsSource = result;
                        }
                        else
                        {
                            var result = db.Select<ProductPlan>(u => u.BatchNo == strBatchNo && u.LineNum == lNum.ToString());//(lNum > 0 ? u.LineNum == lNum.ToString() : true)
                            dgView.ItemsSource = result;
                        }
                    }

                    //var query =
                    //    Query<FabricClothItem>.Builder(db)
                    //        .Select(u => new { u.Age, u.UserName })
                    //        .Where(u => u.Age > 10 && u.Age < 30)
                    //        .Top(3)
                    //        .OrderBy(u => new { u.Age, u.Id })
                    //        .OrderByDesc(u => new { u.AddTime, u.Pwd });

                    //var result1 = db.Select(query: query);

                    //var result4 = db.Select<FabricClothItem>("select * from tuser where valid=@valid", new { valid = 1 });

                    Console.WriteLine("查询出数据条数:");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void mnuUpdate_Click(object sender, RoutedEventArgs e)
        {
            var item = dgView.SelectedItem as ProductPlan;
            if (item != null)
            {
                Console.WriteLine("Data:" + item.BatchNo);
                using (var db = new DapperContext("MySqlDbConnection"))
                {
                    try
                    {
                        var rtnB = db.Update("update productplan set BatchNo=@BatchNo,ColorNum=@ColorNum,Specs=@Specs,LineNum=@LineNum,QualityString=@QualityString,ProduceNum=@ProduceNum where Id=@Id", item);// new { BatchNo = item., Id = item.Id }
                        if (rtnB)
                        {
                            Console.WriteLine("更新成功！");
                            dgView.UpdateLayout();
                        }
                        else
                        {
                            Console.WriteLine("更新失败！");

                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
    }
}

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
using Yu3zx.FactoryLine.DataModels;

namespace Yu3zx.FactoryLine.Views
{
    /// <summary>
    /// FabricClothPage.xaml 的交互逻辑
    /// </summary>
    public partial class FabricClothSearchPage : Page
    {
        public FabricClothSearchPage()
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
                using (var db = new DapperContext("MySqlDbConnection"))
                {
                    if(chkTime.IsChecked == true)
                    {
                        string strBatch = txtBatchNo.Text.Trim();
                        if(string.IsNullOrEmpty(strBatch))
                        {
                            var resultt = db.Select<FabricClothItem>(u => u.AddTime >= DateTime.Parse( dptBegin.DateTimeStr) && u.AddTime <= DateTime.Parse(dptEnd.DateTimeStr));
                            dgView.ItemsSource = resultt;
                        }
                        else
                        {
                            var resultt1 = db.Select<FabricClothItem>(u => u.AddTime >= DateTime.Parse(dptBegin.DateTimeStr) && u.AddTime <= DateTime.Parse(dptEnd.DateTimeStr) && u.BatchNo  == strBatch);
                            dgView.ItemsSource = resultt1;
                        }
                    }
                    else
                    {
                        string strBatchNo = txtBatchNo.Text.Trim();
                        if(string.IsNullOrEmpty(strBatchNo))
                        {
                            var result = db.Select<FabricClothItem>();
                            dgView.ItemsSource = result;
                        }
                        else
                        {
                            var result = db.Select<FabricClothItem>(u => u.BatchNo == strBatchNo);
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
    }
}

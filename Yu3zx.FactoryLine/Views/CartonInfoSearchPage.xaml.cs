using System;
using System.Windows;
using System.Windows.Controls;
using Yu3zx.DapperExtend;
using Yu3zx.FactoryLine.DataModels;

namespace Yu3zx.FactoryLine.Views
{
    /// <summary>
    /// CartonInfoSearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class CartonInfoSearchPage : Page
    {
        public CartonInfoSearchPage()
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
                    if (chkTime.IsChecked == true)
                    {
                        string strBatch = txtBatchNo.Text.Trim();
                        if (string.IsNullOrEmpty(strBatch))
                        {
                            var resultt = db.Select<CartonBoxInfo>(u => u.AddTime >= DateTime.Parse(dptBegin.DateTimeStr) && u.AddTime <= DateTime.Parse(dptEnd.DateTimeStr));
                            dgView.ItemsSource = resultt;
                        }
                        else
                        {
                            var resultt1 = db.Select<CartonBoxInfo>(u => u.AddTime >= DateTime.Parse(dptBegin.DateTimeStr) && u.AddTime <= DateTime.Parse(dptEnd.DateTimeStr) && u.BatchNo == strBatch);
                            dgView.ItemsSource = resultt1;
                        }
                    }
                    else
                    {
                        string strBatchNo = txtBatchNo.Text.Trim();
                        if (string.IsNullOrEmpty(strBatchNo))
                        {
                            var result = db.Select<CartonBoxInfo>();
                            dgView.ItemsSource = result;
                        }
                        else
                        {
                            var result = db.Select<CartonBoxInfo>(u => u.BatchNo == strBatchNo);
                            dgView.ItemsSource = result;
                        }
                    }

                    Console.WriteLine("查询出数据条数:");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

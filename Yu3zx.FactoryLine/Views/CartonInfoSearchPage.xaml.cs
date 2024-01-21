using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Yu3zx.DapperExtend;
using Yu3zx.FactoryLine.DataModels;
using Yu3zx.FactoryLine.Utils;

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

        private List<CartonBoxInfo> CartonBoxes
        {
            get;
            set;
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
                            CartonBoxes = resultt;
                        }
                        else
                        {
                            var resultt1 = db.Select<CartonBoxInfo>(u => u.AddTime >= DateTime.Parse(dptBegin.DateTimeStr) && u.AddTime <= DateTime.Parse(dptEnd.DateTimeStr) && u.BatchNo == strBatch);
                            dgView.ItemsSource = resultt1;
                            CartonBoxes = resultt1;
                        }
                    }
                    else
                    {
                        string strBatchNo = txtBatchNo.Text.Trim();
                        if (string.IsNullOrEmpty(strBatchNo))
                        {
                            var result = db.Select<CartonBoxInfo>();
                            dgView.ItemsSource = result;
                            CartonBoxes = result;
                        }
                        else
                        {
                            var result = db.Select<CartonBoxInfo>(u => u.BatchNo == strBatchNo);
                            dgView.ItemsSource = result;
                            CartonBoxes = result;
                        }
                    }
                    db.Dispose();
                    Console.WriteLine("查询出数据条数:");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CartonBoxes == null)
                {
                    MessageBox.Show("没有数据，请重新查询");
                    return;
                }
                else
                {
                    PrinterHelper.PrintFabricList(CartonBoxes);
                }
            }
            catch
            { }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dptBegin.DateTimeStr = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            dptEnd.DateTimeStr = DateTime.Now.ToString("yyyy-MM-dd 23:59:00");
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

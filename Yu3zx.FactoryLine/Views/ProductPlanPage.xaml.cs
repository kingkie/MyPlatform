﻿using System;
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
    /// ProductPlanPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProductPlanPage : Page
    {
        public ProductPlanPage()
        {
            InitializeComponent();
            dptProduce.DateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = new ProductPlan()
                {
                    BatchNo = txtBatchNo.Text,
                    ColorNum = txtColor.Text,
                    Specs = txtSpecs.Text,
                    LineNum = cboProduceLine.SelectedIndex + 1,
                    ProduceNum = float.Parse(txtProduceNum.Text),
                    ProduceTime = DateTime.Parse(dptProduce.DateTimeStr),
                    AddTime = DateTime.Now
                };

                using (var db = new DapperContext("MySqlDbConnection"))
                {
                    var result = db.Insert(model);
                    if (result)
                    {
                        Console.WriteLine("添加成功");
                    }
                    else
                    {
                        Console.WriteLine("添加失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

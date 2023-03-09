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
            if(string.IsNullOrEmpty(txtBatchNo.Text) || string.IsNullOrEmpty(txtFabricWidth.Text) || string.IsNullOrEmpty(txtProduceNum.Text))
            {
                MessageBox.Show("请填写完整数据！");
                return;
            }
            if (string.IsNullOrEmpty(txtQualityString.Text) || string.IsNullOrEmpty(txtSpecs.Text) || string.IsNullOrEmpty(txtColor.Text))
            {
                MessageBox.Show("请填写完整数据！");
                return;
            }
            try
            {
                var model = new ProductPlan()
                {
                    BatchNo = txtBatchNo.Text,
                    ColorNum = txtColor.Text,
                    Specs = txtSpecs.Text,
                    LineNum = (cboProduceLine.SelectedIndex + 1).ToString(),
                    ProduceNum = float.Parse(txtProduceNum.Text),
                    QualityString = txtQualityString.Text,
                    ProduceTime = DateTime.Parse(dptProduce.DateTimeStr),
                    AddTime = DateTime.Now,
                    FabricWidth = int.Parse(txtFabricWidth.Text)
                };

                using (var db = new DapperContext("MySqlDbConnection"))
                {
                    var result = db.Insert(model);
                    if (result)
                    {
                        txtShow.Text = "计划批次：" + model.BatchNo + "添加成功!";
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

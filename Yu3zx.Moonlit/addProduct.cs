using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;
using Yu3zx.Moonlit.service;

namespace Yu3zx.Moonlit
{
    public partial class addProduct : Form
    {
        private SysProductInfo _productInfo;

        public SysProductInfo GetProductInfo
        {
            get { return _productInfo; }
        }

        private List<SysProductParts> _sysProductPartsList;
        public List<SysProductParts> GetSysProductPartsList
        {
            get { return _sysProductPartsList; }
        }


        public addProduct()
        {
            InitializeComponent();
        }
        private string op_type;
        public string GetOpType{
             get { return op_type; }
        }
        public addProduct(SysProductInfo _scope)
        {
            InitializeComponent();
            _productInfo = _scope;
            if (_productInfo == null)
            {
                this.Text += "-新增";
                _productInfo = new SysProductInfo();
                _productInfo.createTime = DateTime.Now;
                op_type = "add";
            }
            else
            {

                fdProductName.Text= _productInfo.Name ;
                fdProductNo.Text=_productInfo.No ;
                fdProductCode.Text=_productInfo.Code;
                fdProductCode2.Text = _productInfo.Code2;
                fdProductCode3.Text = _productInfo.Code3;
                fdInfo.Text= _productInfo.Info ;
                fdProductPrintName.Text = _productInfo.printName;
                //btnSysScope.Enabled = false;

                SysProductPartsService sysProductPartsService = (SysProductPartsService)BeanUtil.getBean("sysProductPartsService");
                List<SysProductParts> partsLits = sysProductPartsService.getProductParts(_productInfo.ID);
                if (partsLits != null && partsLits.Count > 0)
                {
                    int iRow = 0;
                    for (int i = 0, len = partsLits.Count; i < len; i++)
                    {
                        SysProductParts spi = partsLits[i];
                        iRow = product_part_list.Rows.Add();

                        product_part_list.Rows[iRow].Cells["partSerial"].Value = (i+1).ToString();
                        product_part_list.Rows[iRow].Cells["partName"].Value = partsLits[i].Name;
                        product_part_list.Rows[iRow].Cells["partStage"].Value = partsLits[i].stage;
                        product_part_list.Rows[iRow].Cells["partData"].Value = partsLits[i].No1;
                        product_part_list.Rows[iRow].Cells["partNo2"].Value = partsLits[i].No2;
                        product_part_list.Rows[iRow].Cells["partNo3"].Value = partsLits[i].No3;

                    }
                }                
                this.Text += "-修改";
                op_type = "update";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void productSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fdProductName.Text))
            {
                MessageBox.Show("产品名称不能为空！");
                return;
            }
            if (string.IsNullOrWhiteSpace(fdProductNo.Text))
            {
                MessageBox.Show("产品编号不能为空！");
                return;
            }
            if (string.IsNullOrWhiteSpace(fdProductCode.Text))
            {
                MessageBox.Show("产品识别码不能为空！");
                return;
            }
            _productInfo.Name = fdProductName.Text;
            _productInfo.No = fdProductNo.Text;
            _productInfo.Code = fdProductCode.Text;
            _productInfo.Code2 = fdProductCode2.Text;
            _productInfo.Code3 = fdProductCode3.Text;
            _productInfo.Info = fdInfo.Text;
            _productInfo.printName = fdProductPrintName.Text;
            int count1=product_part_list.Rows.Count;
            if (count1>0) {
                int colNum= product_part_list.Rows[0].Cells.Count;
                _sysProductPartsList = new List<SysProductParts>();
                for (int i=0;i< count1;i++) {
                    string name = (string)product_part_list.Rows[i].Cells[1].Value;
                    string stage = (string)product_part_list.Rows[i].Cells[2].Value;
                    if (name==null || name=="" || stage==null || stage=="") {//当这2个位空时视为无效数据
                        continue;
                    }
                    SysProductParts sysProductParts = new SysProductParts();
                    sysProductParts.Name = (string)product_part_list.Rows[i].Cells[1].Value;
                    sysProductParts.serial = (string)product_part_list.Rows[i].Cells[0].Value;
                    sysProductParts.stage = (string)product_part_list.Rows[i].Cells[2].Value;
                    sysProductParts.No1 = (string)product_part_list.Rows[i].Cells[3].Value;
                    sysProductParts.No2 = (string)product_part_list.Rows[i].Cells[4].Value;
                    sysProductParts.No3 = (string)product_part_list.Rows[i].Cells[5].Value;
                    _sysProductPartsList.Add(sysProductParts);
                    
                }
            }
            
            //修改时间每次保存时反馈
            _productInfo.alterTime = DateTime.Now;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void productCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void product_part_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (product_part_list.SelectedRows.Count > 0)
            {
                this.product_part_list.Rows.Remove(this.product_part_list.SelectedRows[0]);

            }
            else {
                MessageBox.Show("请选择需要删除的行", "提示", MessageBoxButtons.OK);

            }
        }
    }
}

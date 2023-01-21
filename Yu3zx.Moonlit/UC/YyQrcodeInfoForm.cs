using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.UC
{
    public partial class YyQrcodeInfoForm : Form
    {
        private YyQrcodeInfo _yyQrcodeInfo;

        public YyQrcodeInfo GetYyQrcodeInfo
        {
            get { return _yyQrcodeInfo; }
        }
        public YyQrcodeInfoForm()
        {
            InitializeComponent();
        }
        private string op_type;
        public string GetOpType
        {
            get { return op_type; }
        }
        public YyQrcodeInfoForm(YyQrcodeInfo _scope)
        {
            InitializeComponent();
            _yyQrcodeInfo = _scope;
            if (_yyQrcodeInfo == null)
            {
                this.Text += "-新增";
                _yyQrcodeInfo = new YyQrcodeInfo();
                //_yyQrcodeInfo.createTime = DateTime.Now;
                op_type = "add";
            }
            else
            {
                yyQrcodeInfo_totalCode.Text = _yyQrcodeInfo.totalCode;
                yyQrcodeInfo_totalQrCode.Text = _yyQrcodeInfo.totalQrCode;
                yyQrcodeInfo_printStatus.Text = _yyQrcodeInfo.printStatus;
                //yyQrcodeInfo_createTime.Text = _yyQrcodeInfo.createTime;
               // yyQrcodeInfo_printTime.Text = _yyQrcodeInfo.printTime;
                yyQrcodeInfo_Type.Text = _yyQrcodeInfo.Type;
                yyQrcodeInfo_sourceIp.Text = _yyQrcodeInfo.sourceIp;
                yyQrcodeInfo_qualifiedStatus.Text = _yyQrcodeInfo.qualifiedStatus;
                yyQrcodeInfo_Length.Text = _yyQrcodeInfo.Length;
                //btnSysScope.Enabled = false;
                string[] codeArr = _yyQrcodeInfo.totalQrCode.Split(';');
                
                for (int i=0,len= codeArr.Length;i<len;i++) {
                    if (i>16) {
                        break;
                    }
                    string code = codeArr[i];
                    if (i == 0)
                    {
                        yyQrcodeInfo_printTotalCode.Text = code;
                        continue;
                    }
                    TextBox fieldBox=(TextBox)this.Controls.Find("yyQrcodeInfo_partsCode" + i.ToString(), false)[0];
                    fieldBox.Text = code;
                }

                this.Text += "-修改";
                op_type = "update";
            }
        }


        private void yyQrcodeInfoSave_Click(object sender, EventArgs e)
        {
            _yyQrcodeInfo.totalCode = yyQrcodeInfo_totalCode.Text;
            //_yyQrcodeInfo.totalQrCode = yyQrcodeInfo_totalQrCode.Text;
            _yyQrcodeInfo.printStatus= yyQrcodeInfo_printStatus.Text;
            //_yyQrcodeInfo.createTime = yyQrcodeInfo_createTime.Text;
            //_yyQrcodeInfo.printTime = yyQrcodeInfo_printTime.Text;
            _yyQrcodeInfo.Type = yyQrcodeInfo_Type.Text;
            _yyQrcodeInfo.sourceIp = yyQrcodeInfo_sourceIp.Text;
            _yyQrcodeInfo.qualifiedStatus = yyQrcodeInfo_qualifiedStatus.Text;
            _yyQrcodeInfo.Length= yyQrcodeInfo_Length.Text;
            StringBuilder codeStr = new StringBuilder();
            for (int i = 1; i < 17; i++)
            {
                TextBox fieldBox = (TextBox)this.Controls.Find("yyQrcodeInfo_partsCode" + i.ToString(), false)[0];
                string boxTex = fieldBox.Text;
                if (boxTex!=null && boxTex.Trim()!="") {
                    codeStr.Append(fieldBox.Text).Append(";");
                }
                
            }
            string codeTostring = codeStr.ToString();
            if (codeTostring != null && codeTostring != "")
            {
                _yyQrcodeInfo.totalQrCode = yyQrcodeInfo_totalCode.Text + ";" + codeTostring.Trim(';');
            }
            else {//当数据被全部删除时,直接取原来的值
                _yyQrcodeInfo.totalQrCode = yyQrcodeInfo_totalQrCode.Text;
            }

            //修改时间每次保存时反馈
            //_productInfo.alterTime = DateTime.Now;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void yyQrcodeInfoCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

    }
}

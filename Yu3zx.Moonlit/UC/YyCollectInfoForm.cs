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

namespace Yu3zx.Moonlit.UC
{
    public partial class YyCollectInfoForm : Form
    {

        private YyCollectInfo _yyCollectInfo;

        public YyCollectInfo GetYyCollectInfo
        {
            get { return _yyCollectInfo; }
        }
        public YyCollectInfoForm()
        {
            InitializeComponent();
        }
        private string op_type;
        public string GetOpType
        {
            get { return op_type; }
        }
        public YyCollectInfoForm(YyCollectInfo _scope)
        {
            InitializeComponent();
            _yyCollectInfo = _scope;
            if (_yyCollectInfo == null)
            {
                this.Text += "-新增";
                _yyCollectInfo = new YyCollectInfo();
                _yyCollectInfo.createTime = DateTime.Now;
                op_type = "add";
            }
            else
            {
                yyCollectInfo_totalCode.Text = _yyCollectInfo.totalCode;
                yyCollectInfo_partsCode.Text = _yyCollectInfo.partsCode;
                string status = ModelsUtil.toString(_yyCollectInfo.Status);
                string statusStr = "";
                if (status == "0")
                {
                    statusStr = "已删除";
                }
                else
                {
                    statusStr = "正常";
                }
                yyCollectInfo_Status.Text = statusStr;
                yyCollectInfo_createTime.Text = _yyCollectInfo.createTime.ToString();
                yyCollectInfo_delTime.Text = _yyCollectInfo.delTime.ToString();
                //yyCollectInfo_sourceType.Text = _yyCollectInfo.sourceType;
                yyCollectInfo_sourcePlcIp.Text = _yyCollectInfo.sourcePlcIp;
                //yyCollectInfo_opId.Text = _yyCollectInfo.opId;
                fdProductNo.Text = _yyCollectInfo.ProductNo;
                //fdStation.Text = _yyCollectInfo.Station;
                this.Text += "-修改";
                op_type = "update";
            }
        }


        private void yyCollectInfoSave_Click(object sender, EventArgs e)
        {
            _yyCollectInfo.totalCode = yyCollectInfo_totalCode.Text;
            _yyCollectInfo.partsCode= yyCollectInfo_partsCode.Text;
            //_yyCollectInfo.Status = yyCollectInfo_Status.Text;
            //_yyCollectInfo.createTime = yyCollectInfo_createTime.Text;
            //_yyCollectInfo.delTime = yyCollectInfo_delTime.Text;
            //_yyCollectInfo.sourceType = yyCollectInfo_sourceType.Text;
            _yyCollectInfo.sourcePlcIp = yyCollectInfo_sourcePlcIp.Text;
            //_yyCollectInfo.opId = yyCollectInfo_opId.Text;

             _yyCollectInfo.ProductNo= fdProductNo.Text;
            _yyCollectInfo.Station = fdProductNo.Text;
            //修改时间每次保存时反馈
            //_productInfo.alterTime = DateTime.Now;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void yyCollectInfoCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

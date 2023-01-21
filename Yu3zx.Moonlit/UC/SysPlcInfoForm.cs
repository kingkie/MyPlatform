using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.UC
{
    public partial class SysPlcInfoForm : Form
    {
        private SysPlcInfo _sysPlcInfo;

        public SysPlcInfo GetSysPlcInfo
        {
            get { return _sysPlcInfo; }
        }
        public SysPlcInfoForm()
        {
            InitializeComponent();
        }
        private string op_type;
        public string GetOpType
        {
            get { return op_type; }
        }
        public SysPlcInfoForm(SysPlcInfo _scope)
        {
            InitializeComponent();
            _sysPlcInfo = _scope;
            if (_sysPlcInfo == null)
            {
                this.Text += "-新增";
                _sysPlcInfo = new SysPlcInfo();
                _sysPlcInfo.createTime = DateTime.Now;
                op_type = "add";
            }
            else
            {
                sysPlcInfo_Name.Text = _sysPlcInfo.Name;
                sysPlcInfo_Ip.Text = _sysPlcInfo.Ip;
                sysPlcInfo_Port.Text = _sysPlcInfo.Port;
                sysPlcInfo_Station.Text = _sysPlcInfo.Station;
                sysPlcInfo_delAddress.Text = _sysPlcInfo.delAddress;
                sysPlcInfo_totalAddress.Text = _sysPlcInfo.totalAddress;
                sysPlcInfo_partsAddress.Text = _sysPlcInfo.partsAddress;
                sysPlcInfo_validAddress.Text = _sysPlcInfo.validAddress;
                sysPlcInfo_writeAddress.Text = _sysPlcInfo.writeAddress;
                sysPlcInfo_Info.Text = _sysPlcInfo.Info;
                //btnSysScope.Enabled = false;
                this.Text += "-修改";
                op_type = "update";
            }
        }


        private void sysPlcInfoSave_Click(object sender, EventArgs e)
        {
            _sysPlcInfo.Name = sysPlcInfo_Name.Text;
            _sysPlcInfo.Ip = sysPlcInfo_Ip.Text;
            _sysPlcInfo.Port = sysPlcInfo_Port.Text;
            _sysPlcInfo.Station = sysPlcInfo_Station.Text;
            _sysPlcInfo.delAddress = sysPlcInfo_delAddress.Text;
            _sysPlcInfo.totalAddress = sysPlcInfo_totalAddress.Text;
            _sysPlcInfo.partsAddress = sysPlcInfo_partsAddress.Text;
            _sysPlcInfo.validAddress = sysPlcInfo_validAddress.Text;
            _sysPlcInfo.writeAddress = sysPlcInfo_writeAddress.Text;
            _sysPlcInfo.Info = sysPlcInfo_Info.Text;
            //_sysPlcInfo.opId = sysPlcInfo_opId.Text;
            //_sysPlcInfo.opName = opName.Text;

            //修改时间每次保存时反馈
            _sysPlcInfo.alterTime = DateTime.Now;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sysPlcInfoCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    }
}

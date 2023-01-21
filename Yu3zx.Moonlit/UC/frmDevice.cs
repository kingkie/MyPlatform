using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Acquisition;

namespace Yu3zx.Moonlit.UC
{
    public partial class frmDevice : Form
    {
        private AcqPlc acqplc = null;
        public frmDevice()
        {
            InitializeComponent();
        }

        public frmDevice(AcqPlc _acqplc)
        {
            InitializeComponent();
            acqplc = _acqplc;

            if(acqplc == null)
            {
                acqplc = new AcqPlc();
            }

            txtDevId.Text = acqplc.DeviceId;
            txtDevName.Text = acqplc.DeviceName;
            txtPLCAddr.Text = acqplc.Addr.ToString();
            txtPort.Text = acqplc.IpPort.ToString();
            txtServerIP.Text = acqplc.IPAddr.ToString();
            txtSiteNum.Text = acqplc.SiteOrder.ToString();
            txtCoilsAddr.Text = acqplc.CoilsAddr.ToString();
            txtRegsAddr.Text = acqplc.RegsAddr.ToString();
        }
        /// <summary>
        /// 传入的设备
        /// </summary>
        public AcqPlc Device
        {
            get { return acqplc; }
            set { acqplc = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(!CheckElement())
            {
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnTryLink_Click(object sender, EventArgs e)
        {
            if (!CheckElement())
            {
                return;
            }
            try
            {
                var rInit = acqplc.Init();
                if(!rInit.IsSuccess)
                {
                    MessageBox.Show(rInit.Message);
                }

                var rOpen = acqplc.OpenDevice();
                if (!rOpen.IsSuccess)
                {
                    MessageBox.Show(rOpen.Message);
                }
                else
                {
                    chkLinkState.Checked = acqplc.Connected;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("连接异常:" + ex.Message);
            }
        }

        private bool CheckElement()
        {
            if (string.IsNullOrEmpty(txtDevId.Text))
            {
                MessageBox.Show("设备ID不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtDevName.Text))
            {
                MessageBox.Show("设备名称不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtServerIP.Text))
            {
                MessageBox.Show("服务IP不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtSiteNum.Text))
            {
                MessageBox.Show("工位序号不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtCoilsAddr.Text))
            {
                MessageBox.Show("线圈地址不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtRegsAddr.Text))
            {
                MessageBox.Show("条码地址不能为空！");
                return false;
            }

            acqplc.DeviceId = txtDevId.Text;
            acqplc.DeviceName = txtDevName.Text;
            acqplc.IPAddr = txtServerIP.Text;
            acqplc.IpPort = int.Parse(txtPort.Text);
            acqplc.SiteOrder = int.Parse(txtSiteNum.Text);
            acqplc.Addr = byte.Parse(txtPLCAddr.Text);
            acqplc.RegsAddr = ushort.Parse(txtRegsAddr.Text);
            acqplc.CoilsAddr = ushort.Parse(txtCoilsAddr.Text);
            return true;
        }
    }
}

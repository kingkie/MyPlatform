using System;
using System.Windows.Forms;

namespace Yu3zx.Enroll
{
    public partial class CalmRegCodeFrm : Form
    {
        public CalmRegCodeFrm()
        {
            InitializeComponent();
        }

        private void btnCalm_Click(object sender, EventArgs e)
        {
            if(txtMacCode.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入注册码！");
                return;
            }
            txtRegister.Text = SoftRegisterManager.GetInstance().CalmRegString(txtMacCode.Text.Trim());

        }

        private void btnGetMac_Click(object sender, EventArgs e)
        {
            txtMacCode.Text = SoftRegisterManager.GetInstance().GetDiskVolumeSerialNumber();
        }

        private void btnGetCPU_Click(object sender, EventArgs e)
        {
            txtMacCode.Text = SoftRegisterManager.GetInstance().GetCpuSerialNumber();
        }
    }
}

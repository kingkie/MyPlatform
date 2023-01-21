using System;
using System.Windows.Forms;

namespace Yu3zx.Enroll
{
    public partial class EnrollFrm : Form
    {
        public EnrollFrm()
        {
            InitializeComponent();
        }

        private void EnrollFrm_Load(object sender, EventArgs e)
        {
            txtMacCode.Text = SoftRegisterManager.GetInstance().GetDiskVolumeSerialNumber();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string getCode = SoftRegisterManager.GetInstance().CalmRegString(txtMacCode.Text);
            if(getCode == txtRegister.Text)
            {
                SoftRegisterManager.GetInstance().RegisterString = getCode;
                SoftRegisterManager.GetInstance().RegisterTime = DateTime.Now;
                SoftRegisterManager.GetInstance().Save();
                MessageBox.Show("注册成功，请重新打开！");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("输入的注册码有误！");
                txtRegister.Focus();
            }
        }
    }
}

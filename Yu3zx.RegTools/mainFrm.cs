using System;
using System.Windows.Forms;
using Yu3zx.Enroll;

namespace Yu3zx.RegTools
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }

        private void btnRegTool_Click(object sender, EventArgs e)
        {
            CalmRegCodeFrm crcf = new CalmRegCodeFrm();
            crcf.Show();
        }

        private void btnZxing_Click(object sender, EventArgs e)
        {
            //ZxFrm zxfrm = new ZxFrm();
            //zxfrm.Show();
        }
    }
}

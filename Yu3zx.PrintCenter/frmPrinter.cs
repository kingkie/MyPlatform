using System;
using System.Windows.Forms;

namespace Yu3zx.PrintCenter
{
    public partial class frmPrinter : Form
    {
        public frmPrinter()
        {
            InitializeComponent();
        }

        private void frmPrinter_Load(object sender, EventArgs e)
        {
            //获取打印机
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cboInitprinter.Items.Add(printer);
            }
            cboInitprinter.SelectedIndex = 0;
        }

        private void btnItem_Click(object sender, EventArgs e)
        {

        }

        private void btnFabric_Click(object sender, EventArgs e)
        {

        }

        private void btnCarton_Click(object sender, EventArgs e)
        {

        }

    }
}

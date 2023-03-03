using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

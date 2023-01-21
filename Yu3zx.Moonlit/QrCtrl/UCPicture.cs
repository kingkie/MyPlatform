using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yu3zx.Moonlit.QrCtrl
{
    public partial class UCPicture : PictureBox
    {
        public UCPicture()
        {
            InitializeComponent();
        }

        string codeText = "";
        public string QRCodeText
        {
            set
            {
                codeText = value;
                //this.Image = GetQRCode(codeText);
            }
            get { return codeText; }
        }

        //private Bitmap GetQRCode(string text)
        //{
        //    if (string.IsNullOrEmpty(text)) return null;
        //    ZXing.BarcodeWriter bw = new ZXing.BarcodeWriter();
        //    SizeF sizeF = new SizeF();
        //    sizeF.Height = this.SizeF.Height;
        //    sizeF.Width = this.SizeF.Width;
        //    if (this.RootReport.ReportUnit == DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter)
        //    {
        //        sizeF.Height = sizeF.Height * 0.3937007874015748F;
        //        sizeF.Width = sizeF.Width * 0.3937007874015748F;
        //    }
        //    Size size = sizeF.ToSize();
        //    bw.Options.Height = size.Height - 2;
        //    bw.Options.Width = size.Width - 2;
        //    bw.Options.Margin = 0;
        //    bw.Format = ZXing.BarcodeFormat.QR_CODE;
        //    return bw.Write(text);
        //}
    }
}

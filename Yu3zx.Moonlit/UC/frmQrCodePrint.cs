using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Moonlit.bean;
using Yu3zx.Moonlit.Models;
using Yu3zx.Moonlit.service;

namespace Yu3zx.Moonlit.UC
{
    public partial class frmQrCodePrint : CCWin.CCSkinMain //Form
    {
        mainFrm frmMain = null;
        public frmQrCodePrint()
        {
            InitializeComponent();
        }

        public frmQrCodePrint(mainFrm frmmain)
        {
            InitializeComponent();
            frmMain = frmmain;
        }

        private void txtQrCode_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSeach_Click(sender,e);
            }
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            string strQrcode = txtQrCode.Text.Trim();
            txtQrCode.Text = string.Empty;
            if (string.IsNullOrEmpty(strQrcode))
            {
                txtShow.Text += "查询条码数据不能为空！";
                return;
            }
            else
            {
                SysProductInfoService spIS = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                var productInfo = spIS.getProduct(strQrcode);

                string printName = string.Empty;
                if(productInfo == null)
                {
                    MessageBox.Show("未找到" + strQrcode + "配置！");
                    return;
                }
                else
                {
                    printName = productInfo.printName;
                }

                YyQrcodeInfoService yyQIS = (YyQrcodeInfoService)BeanUtil.getBean("yyQrcodeInfoService");
                List<YyQrcodeInfo> lyyQrInfos = yyQIS.list(strQrcode);
                if(lyyQrInfos != null && lyyQrInfos.Count > 0)
                {
                    qrCode.SetPicString = lyyQrInfos[0].totalQrCode;
                    qrCode.QrContent = printName;

                    Margins margin = new Margins(10, 10, 10, 10);//左右上下
                    imgPrint.DefaultPageSettings.Margins = margin;
                    imgPrint.Print();

                    txtShow.Text = "打印总二维码：" + lyyQrInfos[0].totalCode;
                }
                else
                {
                    YyCollectInfoService ycis1 = (YyCollectInfoService)BeanUtil.getBean("yyCollectInfoService");
                    List<YyCollectInfo> ycParts = ycis1.list(strQrcode);
                    if(ycParts == null || ycParts.Count < 1)
                    {
                        txtShow.Text = "总二维码：" + strQrcode + "还未采集或未采集完成！";
                        return;
                    }
                    else
                    {
                        SysProductInfoService spis = (SysProductInfoService)BeanUtil.getBean("sysProductInfoService");
                        bool bCanParcket = spis.validateProduct(strQrcode,ycParts);
                        if(bCanParcket)
                        {
                            YyQrcodeInfo yyQrInfo = new YyQrcodeInfo();
                            yyQrInfo.totalCode = strQrcode;
                            yyQrInfo.totalQrCode = PackQrcode(ycParts);
                            yyQrInfo.createTime = DateTime.Now;
                            yyQrInfo.printTime = DateTime.Now;
                            yyQrInfo.printStatus = "1";
                            yyQIS.insert(yyQrInfo);

                            qrCode.SetPicString = yyQrInfo.totalQrCode;
                            qrCode.QrContent = printName;

                            Margins margin = new Margins((int)nudLeft.Value, (int)nudRight.Value, (int)nudUp.Value,  (int)nudDown.Value);//左右上下
                            imgPrint.DefaultPageSettings.Margins = margin;
                            imgPrint.Print();

                            txtShow.Text = "打印总二维码：" + strQrcode;
                        }
                        else
                        {
                            txtShow.Text = "总二维码：" + strQrcode + "还未采集完整！";
                            return;
                        }
                    }
                }
            }
        }

        private string PackQrcode(List<YyCollectInfo> lParts)
        {
            if(lParts == null || lParts.Count == 0)
            {
                return "";
            }
            StringBuilder sbPack = new StringBuilder();
            //for(int j= 0; j < 4;j++)
            //{
                for (int i = 0; i < lParts.Count; i++)
                {
                    sbPack.Append(lParts[i].partsCode).Append(";");
                }
            //}

            return sbPack.ToString().Trim(';');
        }

        private void imgPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap newBitmap = new Bitmap(qrCode.Width, qrCode.Height);
            qrCode.DrawToBitmap(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height));
            e.Graphics.Clear(Color.White);
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawImage(newBitmap, 0, 0, newBitmap.Width, newBitmap.Height);
        }

        private void frmQrCodePrint_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}

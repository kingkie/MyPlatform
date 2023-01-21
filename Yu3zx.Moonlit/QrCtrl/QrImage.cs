using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Yu3zx.Moonlit.QrCtrl
{
    public partial class QrImage : UserControl
    {
        private const double millimererTopixel = 25.4;

        private Font lblFont = new Font(new FontFamily("黑体"), 7f,FontStyle.Regular,GraphicsUnit.Pixel);

        private string defaultString = "yu3zx.com";

        private float fontweight = 9f;

        public QrImage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 二维码底部文字
        /// </summary>
        public string QrContent
        {
            get { return defaultString; }
            set
            {
                defaultString = value;
            }
        }

        public Font QrFont
        {
            set
            {
                lblFont = value;
            }
            get { return lblFont; }
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        public float FontSize
        {
            get { return fontweight; }
            set
            {
                fontweight = value;
            }
        }
        /// <summary>
        /// 打印时左边偏移
        /// </summary>
        public int PrintMarginLeft
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 打印时右边偏移
        /// </summary>
        public int PrintMarginRight
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 打印时上边偏移
        /// </summary>
        public int PrintMarginTop
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 打印时下边偏移
        /// </summary>
        public int PrintMarginBottom
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageWidth">打印纸张宽度(mm)</param>
        /// <param name="pageHeight">打印纸张高度(mm)</param>
        public void SetPage(int pageWidth,int pageHeight)
        {
            this.Height = (int)MillimeterToPixel(this.Handle, pageHeight, 2);
            this.Width = (int)MillimeterToPixel(this.Handle, pageWidth, 1);
            SetQrImage(QrContent);
        }

        public void SetQrImage(string qrString)
        {
            if(string.IsNullOrEmpty(qrString))
            {
                return;
            }
            //defaultString = qrString;
            int ucH = this.Height;
            int ucW = this.Width;
            Font testFont = new Font(new FontFamily("宋体"), FontSize);
            int fontHeight = GetLabelH(this, QrContent, testFont);
            int fontWidth = GetLabelW(this, QrContent, testFont);
            fontHeight = 10;
            ucH = ucH - fontHeight;
            if (ucH > ucW)
            {
                ucH = ucW;
            }
            else
            {
                ucW = ucH;
            }

            int margin = ucW % 4;//减少尺寸
            ucW = ucW - margin;
            ucH = ucH - margin;

            int sizeW = 100;//
            int sizeH = 100;//
            //Bitmap qrBitmap = QrBuildHelper.TCreateQRCode(qrString,ucW,1);//QrBuildHelper.Zxing_QRCode(qrString,ucW,ucH)
            //Bitmap qrBitmap = QrBuildHelper.Zxing_Create(qrString, ucW, ucH,out rectMargin);
            Bitmap qrBitmap = Zxing_QRCode(qrString, sizeW, sizeH);
            qrBitmap.SetResolution(96, 96);
            SeatXY seat = GetFirstSeat(qrBitmap, Color.Black);
            sizeW = qrBitmap.Width;//生成正方形包括白边的大小
            sizeH = qrBitmap.Height;//生成正方形包括白边的大小
            int WMargin = seat.Xray;//白边大小
            int HMargin = seat.Yray;//白边大小

            if (WMargin == -1)
            {
                WMargin = 0;
            }
            if (HMargin == -1)
            {
                HMargin = 0;
            }
            int startX = (int)((this.Width - sizeW) / 4); //ucW + WMargin?
            //Bitmap qrBitmap = ImageHelper.GetThumbnail(qrBitmap1, ucH ,ucW);
            //Bitmap qrBitmap = ImageHelper.ZoomPicture(qrBitmap1, ucW, ucH);
            Rectangle dst = new Rectangle();
            dst.X = (this.Width - sizeW) / 2 + (WMargin - 1);// startX + (WMargin/2 - 1);
            dst.Y = 10;// margin+ HMargin/2;//
            dst.Width = sizeW - 2 * WMargin;// sizeW - 2 * (WMargin - 1); //ucW //
            dst.Height = sizeW - 2 * WMargin;// 100;// sizeW - 2 * (HMargin - 1);//ucH

            Rectangle sourceR = new Rectangle();
            if (WMargin > 0)
            {
                sourceR.X = WMargin - 1;
            }
            else
            {
                sourceR.X = 0;
            }
            if (HMargin > 0)
            {
                sourceR.Y = HMargin - 1;
            }
            else
            {
                sourceR.Y = 0;
            }
            sourceR.Width = 100;// sizeW - 2 * (WMargin - 1);//ucW
            sourceR.Height = 100;// sizeH - 2 * (HMargin - 1);//ucH


            //Graphics gImg = qrPic.CreateGraphics();
            if (qrPic.Image == null)
            {
                qrPic.Image = new Bitmap(this.Width,this.Height);
            }
            Graphics gImg = Graphics.FromImage(qrPic.Image);
            gImg.Clear(Color.White);
            //设置质量
            gImg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gImg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //指定高质量显示水印图片质量
            //gImg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //gImg.DrawImage(qrBitmap, new Rectangle(startX + rectMargin, 2, ucW - rectMargin, ucH - rectMargin), new Rectangle(rectMargin , rectMargin, ucW - rectMargin, ucH - rectMargin), GraphicsUnit.Pixel);
            gImg.DrawImage(qrBitmap, dst, sourceR, GraphicsUnit.Pixel);
            //g.DrawString("Hello!", f, Brushes.Green, pf);
            if(fontWidth > this.Width)
            {
                Font f1 = new Font(new FontFamily("宋体"), FontSize);//, FontStyle.Bold //9f
                //gImg.DrawString("ZJQ-99SDFIIFDASD-SFSDFSDF", f1, Brushes.Black, 2, 83);
                gImg.DrawString(QrContent, f1, Brushes.Black, 0f, 83);//2 *
            }
            else
            {
                Font f1 = new Font(new FontFamily("宋体"), FontSize);//, FontStyle.Bold//9f
                gImg.DrawString(QrContent, f1, Brushes.Black, (this.Width - fontWidth) /4f-5, 83);//(ucH)2 *
            }
            qrPic.Refresh();
        }

        public void PrintQrImg()
        {
            Margins margin = new Margins(PrintMarginLeft, PrintMarginRight, PrintMarginTop, PrintMarginBottom);//左右上下
            printDoc.DefaultPageSettings.Margins = margin;
            printDoc.Print();
        }

        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(qrPic.Image, 0, 0, qrPic.Image.Width, qrPic.Image.Height);

            int imgWidth = qrPic.Width;
            int imgHeight = qrPic.Height;

            e.Graphics.DrawImage(qrPic.Image, 0, 0, imgWidth, imgHeight);
            return;
            Bitmap newBitmap = new Bitmap(imgWidth, imgHeight);
            Graphics g = e.Graphics;

            qrPic.DrawToBitmap(newBitmap, new Rectangle(0, 0, imgWidth, imgHeight));

            g.Clear(Color.White);

            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            // 指定高质量、低速度呈现。  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            //e.Graphics.Clear(Color.White);
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //e.Graphics.DrawImage(newBitmap, 0, 0, newBitmap.Width, newBitmap.Height);

            g.DrawImage(newBitmap, 0, 0, imgWidth, imgHeight);

        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        private Bitmap Zxing_QRCode(string text, int width, int height)
        {
            try
            {
                ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();
                writer.Format = ZXing.BarcodeFormat.QR_CODE;
                ZXing.QrCode.QrCodeEncodingOptions options = new ZXing.QrCode.QrCodeEncodingOptions()
                {
                    DisableECI = true,//设置内容编码
                    CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                    Width = width,
                    Height = height,
                    Margin = 0,//设置二维码的边距,单位不是固定像素
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.L,
                    PureBarcode = true,
                    //QrVersion = 12
                };

                writer.Options = options;
                Bitmap map = writer.Write(text);
                return map;
            }
            catch
            {
                return null;
            }
        }

        private void QrImage_Resize(object sender, EventArgs e)
        {
            //SetQrImage(QrContent);
        }

        private void QrImage_Load(object sender, EventArgs e)
        {
            SetQrImage(QrContent);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;
        private void CaptureScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }
        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        /// <summary>
        /// 毫米转换成像素
        /// </summary>
        /// <param name="handle">父窗体handle</param>
        /// <param name="length">length是毫米</param>
        /// <param name="direct">1代表x方向  2代表y方向</param>
        /// <returns></returns>
        private double MillimeterToPixel(IntPtr handle, int length, int direct)
        {
            //System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(handle);
            float dpi = g.DpiX;
            if (direct == 2)
            {
                dpi = g.DpiY;
            }
            //1英寸=25.4mm=96DPI，那么1mm=96/25.4DPI
            return (((double)dpi / millimererTopixel) * (double)length);
        }
        /// <summary>
        /// 获取第一个颜色值坐标
        /// </summary>
        /// <param name="imgSource"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private SeatXY GetFirstSeat(Bitmap imgSource, Color color)
        {
            if (imgSource == null)
            {
                return new SeatXY(-1, -1);
            }
            for (int i = 0; i < imgSource.Width; i++)
            {
                for (int j = 0; j < imgSource.Height; j++)
                {
                    Color getPixel = imgSource.GetPixel(i, j);
                    if (getPixel.R < 125 || getPixel.G < 125 || getPixel.B < 125)
                    {
                        return new SeatXY(i, j);
                    }
                }
            }
            return new SeatXY(-1, -1);
        }

        private int GetLabelH(Control ctrl, string str, Font f)
        {
            Graphics g = ctrl.CreateGraphics();
            g.PageUnit = GraphicsUnit.Pixel;
            SizeF StrSize = g.MeasureString(str, f);
            return (int)StrSize.Height;
        }

        private int GetLabelW(Control ctrl, string str, Font f)
        {
            Graphics g = ctrl.CreateGraphics();
            g.PageUnit = GraphicsUnit.Pixel;
            SizeF StrSize = g.MeasureString(str, f);
            return (int)StrSize.Width;
        }
    }

    public struct SeatXY
    {
        public SeatXY(int x,int y)
        {
            Xray = x;
            Yray = y;
        }
        public int Xray;
        public int Yray;
    }
}

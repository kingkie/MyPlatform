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
    public partial class UC_QrCode : UserControl
    {
        private string _mQrContent = "801000.19090910-0001.50000.TY63002";
        private string _mPicString = "yu3zx.com";
        private Font _fontsize = new Font(new FontFamily("黑体"),18);
        private float fontweight = 18;
        public UC_QrCode()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置中间显示内容
        /// </summary>
        public string QrContent
        {
            set
            {
                _mQrContent = value;
                lblContent.Text = _mQrContent;
            }
        }

        public Font QrFontSize
        {
            set
            {
                _fontsize = value;
                lblContent.Font = _fontsize;
            }
            get { return _fontsize; }
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
                lblContent.Font = new Font(new FontFamily("黑体"), fontweight);
            }
        } 

        /// <summary>
        /// 设置二维码
        /// </summary>
        public string SetPicString
        {
            set
            {
                _mPicString = value;
                SetPic();
            }
            get
            {
                return _mPicString;
            }
        }

        public void SetPic()
        {
            QrImgCtrl.Text = _mPicString;
            
        }
    }
}

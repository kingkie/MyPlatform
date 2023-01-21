using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Yu3zx.Moonlit.QrCtrl
{
    public partial class UcPages : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler OnPageChanged;
        public UcPages()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 属性改变触发
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int pageIndex = 1;

        /// <summary>
        /// 当前分页
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                this.PageIndexAndPageCount = $"{PageIndex}/{PageCount}";
            }
        }

        private int pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                 this.PageIndexAndPageCount = $"{PageIndex}/{PageCount}";
            }
        }

        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
            }
        }

        private string strPiAndPc = string.Empty;
        protected string PageIndexAndPageCount
        {
            get { return $"{PageIndex}/{PageCount}"; }
            set
            {
                if(strPiAndPc != value)
                {
                    strPiAndPc = value;
                    OnPropertyChanged("PageIndexAndPageCount");
                }
            }
        }

        private string strWhere = string.Empty;

        public string FilterString
        {
            get { return strWhere; }
            set { strWhere = value; }
        }

        private int pageNum = 0;

        private void SetCtrlEnabled()
        {
            if(PageCount <= 1)
            {
                btnForward.Enabled = false;
                btnJumpGo.Enabled = false;
                btnNext.Enabled = false;
            }
            else if(PageIndex == 1)
            {
                btnForward.Enabled = false;
            }
            else if(PageIndex == PageCount)
            {
                btnNext.Enabled = false;
            }
        }

        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            Regex g = new Regex(@"^[0-9]\d*$");
            bool bMatch = g.IsMatch(txtPageSize.Text);
            if(bMatch)
            {
                pageSize = int.Parse(txtPageSize.Text);

            }
            else
            {
                txtPageSize.Text = pageSize.ToString();
            }
        }

        private void btnJumpGo_Click(object sender, EventArgs e)
        {
            Regex g = new Regex(@"^[0-9]\d*$");
            bool bMatch = g.IsMatch(txtPageNum.Text);
            if (bMatch)
            {
                int iGoJump = int.Parse(txtPageNum.Text);
            }
            else
            {

            }
        }
    }
}

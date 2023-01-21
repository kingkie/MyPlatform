using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Acquisition;
using Yu3zx.Moonlit.Models;

namespace Yu3zx.Moonlit.QrCtrl
{
    public partial class UCDevStatus : UserControl
    {
        private string devName = string.Empty;
        private string devId = string.Empty;
        private bool status = true;//状态良好
        private DevModel devplc = null;
        public UCDevStatus()
        {
            InitializeComponent();
            lblDevName.DataBindings.Add("Text", devplc, "DevName", false, DataSourceUpdateMode.OnPropertyChanged, "", "");
            btnStatus.DataBindings.Add("BackColor", devplc, "StatusColor", false, DataSourceUpdateMode.OnPropertyChanged, Color.Red, null);
        }

        public UCDevStatus(DevModel dev)
        {
            InitializeComponent();
            devplc = dev;
            if(devplc == null)
            {
                devplc = new DevModel();
            }
            lblDevName.DataBindings.Add("Text", devplc, "DevName", false, DataSourceUpdateMode.OnPropertyChanged, "", "");
            btnStatus.DataBindings.Add("BackColor", devplc, "StatusColor", false, DataSourceUpdateMode.OnPropertyChanged, Color.Red, null);
        }

        /// <summary>
        /// 绑定的设备
        /// </summary>
        public DevModel BindDevice
        {
            set { devplc = value; }
            get { return devplc; }
        }
    }
}

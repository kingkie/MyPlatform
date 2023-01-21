using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yu3zx.Moonlit
{
    public partial class frmSplash : Form,ISplashForm
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        #region ISplashForm

        void ISplashForm.SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        #endregion
    }

    /// <summary>
    /// interface for Splash Screen
    /// </summary>
    public interface ISplashForm
    {
        void SetStatusInfo(string NewStatusInfo);
    }
}

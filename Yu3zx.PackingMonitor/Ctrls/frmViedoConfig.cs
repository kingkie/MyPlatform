using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yu3zx.PackingMonitor.Ctrls
{
    public partial class frmViedoConfig : Form
    {
        public frmViedoConfig()
        {
            InitializeComponent();
        }

        public VideoCaptureDevice VideoSource
        {
            get;
            set;
        }
        /// <summary>
        /// 当前选择的摄像头
        /// </summary>
        public int CurrentIndex
        {
            get;
            set;
        } = 0;

        public int CameraRate
        {
            get;
            set;
        } = 0;

        private void frmViedoConfig_Load(object sender, EventArgs e)
        {
            if (CamerasManager.CreateInstance().VideoDevices != null)
            {
                for(int i = 0; i < CamerasManager.CreateInstance().VideoDevices.Count;i++)
                {
                    cboVideo.Items.Add(CamerasManager.CreateInstance().VideoDevices[i].Name);
                }
            }
            if (CamerasManager.CreateInstance().VideoDevices.Count >0)
            {
                cboVideo.SelectedIndex = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if(cboVideo.SelectedIndex > -1)
            {
                VideoSource = CamerasManager.CreateInstance().GetVideoSource(cboVideo.SelectedIndex,cboRate.SelectedIndex);
                CurrentIndex = cboVideo.SelectedIndex;
                CameraRate = cboRate.SelectedIndex;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cboVideo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboVideo.SelectedIndex> -1)
            {
                cboRate.Items.Clear();
                VideoCaptureDevice videoSource = new VideoCaptureDevice(CamerasManager.CreateInstance().VideoDevices[0].MonikerString);
                for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                {
                    string strWH = videoSource.VideoCapabilities[i].FrameSize.Width + "*" + videoSource.VideoCapabilities[i].FrameSize.Height;
                    cboRate.Items.Add(strWH);
                }
                if(cboRate.Items.Count > 0)
                {
                    cboRate.SelectedIndex = 0;
                }
            }
        }
    }
}

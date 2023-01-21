using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.PackingMonitor.Ctrls;

namespace Yu3zx.PackingMonitor
{
    public partial class cameraFrm : Form
    {
        /// <summary>
        /// 声明Camera类
        /// </summary>
        AviCamera aviCa = null;
        private const int WM_CLOSE = 0x10;//关闭窗口句柄
        private bool snapFlag = false;
        public cameraFrm()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// byte转图片
        /// </summary>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            try
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(streamByte))
                {
                    Bitmap bitmap = new Bitmap(stream);
                    return bitmap;
                }

                //System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
                //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                //return img;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// AVI数据
        /// </summary>
        /// <param name="data"></param>
        private void AviCa_RecievedFrame(byte[] data)
        {
            return;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            aviCa = new AviCamera(picViedo.Handle, picViedo.Width, picViedo.Height);
            aviCa.RecievedFrame += AviCa_RecievedFrame;
            try
            {
                aviCa.StartWebCam();
            }
            catch (Exception)
            {
                MessageBox.Show("未能初始化摄像头！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                aviCa = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                aviCa.CloseWebcam();
            }
            catch (Exception)
            {
                MessageBox.Show("摄像头关闭失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                aviCa = null;
            }
        }

        private void cameraFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                aviCa.CloseWebcam();

            }
            catch (Exception)
            {
                aviCa = null;
            }
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            string strDirectory = string.Format("SnapPictures\\{0}\\", DateTime.Now.ToString("yyyy年MM月"));
            if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(strDirectory);
            }
            string filename = Application.StartupPath +"\\" + string.Format("SnapPictures\\{0}\\Qr{1}-{2}.jpg", DateTime.Now.ToString("yyyy年MM月"), DateTime.Now.ToString("ddHHmmssfff"), "Test");

            
            aviCa.SnapImage(filename);
        }
    }
}

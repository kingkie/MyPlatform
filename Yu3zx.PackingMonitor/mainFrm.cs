using AForge.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Yu3zx.DapperExtend;
using Yu3zx.PackingMonitor.Ctrls;
using Yu3zx.PackingMonitor.Models;
using Yu3zx.Enroll;

namespace Yu3zx.PackingMonitor
{
    public partial class mainFrm : CCWin.CCSkinMain
    {
        private bool snapBegin = false;
        private bool canSnap = false;
        private int SpaceTimes = 5;

        private int SnapNum = 3;//拍摄张数

        private Thread thSnap = null;
        public mainFrm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.UpdateStyles();
        }

        #region 专用属性
        private string decodeStr = string.Empty;
        private string DecodeQrContent
        {
            get
            {
                string decodemsg = decodeStr;
                decodeStr = string.Empty;
                return decodemsg;
            }
            set { decodeStr = value; }
        }

        private string decodeImgName = string.Empty;
        private string DecodeImgName
        {
            get
            {
                string decodeimg = decodeImgName;
                decodeImgName = string.Empty;
                return decodeimg;
            }
            set { decodeImgName = value; }
        }

        #endregion End

        private void btnServer_Click(object sender, EventArgs e)
        {
            if(btnServer.Text == "启动服务")
            {
                if (thSnap != null)
                {
                    thSnap.Abort();
                    thSnap = null;
                }
                if(!videoQrcode.IsRunning)
                {
                    MessageBox.Show("扫描摄像头没有打开，请打开！");
                    return;
                }
                if (!videoMonitor.IsRunning)
                {
                    MessageBox.Show("监控摄像头没有打开，请打开！");
                    return;
                }
                if(MainManager.GetInstance().DeviceComm != null && MainManager.GetInstance().DeviceComm.IsOpen)
                {

                }
                else
                {
                    MessageBox.Show("串口未打开！");
                    return;
                }
                thSnap = new Thread(SnapDoWork);
                thSnap.IsBackground = true;
                thSnap.Name = "SnapAndPack";
                thSnap.Start();
                btnServer.Text = "停止服务";
            }
            else
            {
                if(thSnap != null)
                {
                    thSnap.Abort();
                    thSnap = null;
                }
                btnServer.Text = "启动服务";
            }
        }

        private bool StartDevice(VideoSourcePlayer player,int cameraSerialNum)
        {
            try
            {
                if(player.IsRunning)
                {
                    player.WaitForStop();
                }
            }
            catch
            { }
            try
            {
                player.VideoSource = CamerasManager.CreateInstance().GetVideoSource(cameraSerialNum);
                if (player.VideoSource == null)
                {
                    return false;
                }
                else
                {
                    player.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool StartDevice(VideoSourcePlayer player, int cameraSerialNum, int cameraRate = 0)
        {
            try
            {
                if (player.IsRunning)
                {
                    player.WaitForStop();
                }
            }
            catch
            { }
            try
            {
                player.VideoSource = CamerasManager.CreateInstance().GetVideoSource(cameraSerialNum, cameraRate);
                if (player.VideoSource == null)
                {
                    return false;
                }
                else
                {
                    player.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool StopDevice(VideoSourcePlayer player)
        {
            try
            {
                if (player.IsRunning)
                {
                    player.WaitForStop();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ViedoMonitor_NewFrame(object sender, ref Bitmap image)
        {
            //只进行拍照
            return;
        }

        private bool isDeCodeBusy = false;
        private void VideoQrcode_NewFrame(object sender, ref Bitmap image)
        {
            return;//不进行识别
            if(isDeCodeBusy)
            {
                return;
            }
            if (!snapBegin)
            {
                isDeCodeBusy = false;
                return;
            }
            try
            {
                string formatStr = string.Empty;
                string qrStr = QrHelper.QrDecode(image,out formatStr);
                
                if (!string.IsNullOrEmpty(qrStr) && qrStr != decodeStr)
                {
                    isDeCodeBusy = true;
                    string strDirectory = string.Format("SnapPictures\\{0}\\", DateTime.Now.ToString("yyyy-MM"));
                    if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
                    {
                        System.IO.Directory.CreateDirectory(strDirectory);
                    }
                    string filename = "QRmain-" + DateTime.Now.ToString("ddHHmmssfff") + ".jpg";
                    string filepathstr = string.Format("SnapPictures\\{0}\\{1}", DateTime.Now.ToString("yyyy-MM"), filename);
                    DecodeImgName = filepathstr + ";" ;
                    DecodeQrContent = qrStr;
                    SaveImage(image, filepathstr);

                    isDeCodeBusy = false;
                }
                else
                {
                    //DecodeImgName = string.Empty;
                    //DecodeQrContent = string.Empty;
                    isDeCodeBusy = false;
                    return;
                }
            }
            catch(Exception ex)
            {
                Thread.Sleep(200);
            }
        }

        private void SaveImage(Bitmap image,string filepath)
        {
            using (System.IO.FileStream stream = new System.IO.FileStream(filepath, System.IO.FileMode.CreateNew))
            {
                //关键质量控制  
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff  
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {

                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100); //质量（范围0-100）
                image.Save(stream, ici, ep);
                //image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// 保存到数据库
        /// </summary>
        /// <param name="qrFilePath">解码的图片保存地址</param>
        /// <param name="mFilesPath">对应箱内图片</param>
        private void SaveDataBase(string qrFilePath,string mFilesPath,string qrStr = "")
        {
            MonitorRecord mrData = new MonitorRecord();
            mrData.MonitorTime = DateTime.Now;
            mrData.MonitorImgFiles = mFilesPath;
            mrData.QrImgFile = qrFilePath;
            mrData.QrContent = qrStr;
            using (var db = new DapperContext("SQLiteDbConnection"))
            {
                var result = db.Insert(mrData);
                if (!result)
                {
                    //MessageBox.Show("添加失败");
                }
            }
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            //if(DateTime.Now.Year != 2020)
            //{
            //    MessageBox.Show("产品试用已经到期！");
            //    this.Close();
            //    return;
            //}
            if (SoftRegisterManager.GetInstance().CheckCode())
            {
                if (SoftRegisterManager.GetInstance().ErrMsg != "")
                {
                    this.Text = this.Text + "(试用版)";
                    if (MessageBox.Show(SoftRegisterManager.GetInstance().ErrMsg, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        EnrollFrm enroll = new EnrollFrm();
                        enroll.ShowDialog();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                if (MessageBox.Show("软件未注册无法使用，是否立即注册！", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    EnrollFrm enroll = new EnrollFrm();
                    enroll.ShowDialog();
                    Environment.Exit(0);
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            videoQrcode.NewFrame += VideoQrcode_NewFrame;
            videoMonitor.NewFrame += ViedoMonitor_NewFrame;
            dptBeginTime.Value = DateTime.Now.AddDays(-1);
            dptEndTime.Value = DateTime.Now.AddHours(1);
            //=================================
            this.WindowState = FormWindowState.Maximized;

            InitComm();

            InitConfig();

            //btnOpenCamera_Click(null, null);
            //打开摄像头
            //if (StartDevice(videoQrcode, ProgramManager.GetInstance().QrCamera))
            //{
            //    canSnap = true;
            //}
            //else
            //{
            //    canSnap = false;
            //    MessageBox.Show("箱外摄像头打开出错，请检查！");
            //}
            //if (StartDevice(videoMonitor, ProgramManager.GetInstance().MonitorCamera))
            //{
            //    canSnap = true;
            //}
            //else
            //{
            //    canSnap = false;
            //    MessageBox.Show("箱内摄像头打开出错，请检查！");
            //}
            //===============================
            CommOpen();
            //btnOpen_Click(null, null);
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            Bitmap imgCur = videoQrcode.GetCurrentVideoFrame();
            string strDirectory = string.Format("SnapPictures\\{0}\\", DateTime.Now.ToString("yyyy年MM月"));
            if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(strDirectory);
            }
            string filename = string.Format("SnapPictures\\{0}\\Qr{1}-{2}.jpg", DateTime.Now.ToString("yyyy年MM月"), DateTime.Now.ToString("ddHHmmssfff"), "Current");
            using (System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.CreateNew))
            {
                //关键质量控制  
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff  
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {

                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100); //质量（范围0-100）

                imgCur.Save(stream, ici, ep);
                //image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void mainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopDevice(videoQrcode);
            StopDevice(videoMonitor);
        }

        private void videoQrcode_Click(object sender, EventArgs e)
        {
            frmViedoConfig viedoConfig = new frmViedoConfig();
            if (viedoConfig.ShowDialog() == DialogResult.OK)
            {
                if(videoQrcode.IsRunning)
                {
                    videoQrcode.WaitForStop();
                }
                ProgramManager.GetInstance().QrCamera = viedoConfig.CurrentIndex;
                ProgramManager.GetInstance().QrCameraRate = viedoConfig.CameraRate;
                videoQrcode.VideoSource = viedoConfig.VideoSource;
                videoQrcode.Start();
                ProgramManager.GetInstance().Save();
            }
        }

        private void videoMonitor_Click(object sender, EventArgs e)
        {
            frmViedoConfig viedoConfig = new frmViedoConfig();
            if(viedoConfig.ShowDialog() == DialogResult.OK)
            {
                if (videoMonitor.IsRunning)
                {
                    videoMonitor.WaitForStop();
                }
                ProgramManager.GetInstance().MonitorCamera = viedoConfig.CurrentIndex;
                ProgramManager.GetInstance().MonitorCameraRate = viedoConfig.CameraRate;
                videoMonitor.VideoSource = viedoConfig.VideoSource;
                videoMonitor.Start();
                ProgramManager.GetInstance().Save();
            }
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            string strQrContent = txtQrContent.Text.Trim();
            string strBeginTime = dptBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strEndTime = dptEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            List<MonitorRecord> lRecords = null;
            if(string.IsNullOrEmpty(strQrContent))
            {
                using (var db = new DapperContext("SQLiteDbConnection"))
                {
                    lRecords = db.Select<MonitorRecord>("select * from monitorrecordflow where monitortime >= @BeginTime and monitortime <= @EndTime", new { BeginTime = strBeginTime, EndTime = strEndTime });
                }
            }
            else
            {
                using (var db = new DapperContext("SQLiteDbConnection"))
                {
                     lRecords = db.Select<MonitorRecord>("select * from monitorrecordflow where monitortime >= @BeginTime and monitortime <= @EndTime and qrcontent like  @QrContent", new { BeginTime = strBeginTime, EndTime = strEndTime, QrContent = (strQrContent+"%") });
                }
            }
            if(lRecords != null)
            {
                dgvShow.Rows.Clear();
                int iRow = 0;
                for (int i = 0; i < lRecords.Count; i++)
                {
                    iRow = dgvShow.Rows.Add();
                    dgvShow.Rows[iRow].Cells["id"].Value = i + 1;
                    dgvShow.Rows[iRow].Cells["qrContent"].Value = lRecords[i].QrContent;
                    string strQrImgs = lRecords[i].QrImgFile;
                    string[] qrImgs = strQrImgs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if(qrImgs != null && qrImgs.Length > 0)
                    {
                        dgvShow.Rows[iRow].Cells["qrImgUrl"].Value = qrImgs[0];
                        //if (qrImgs.Length > 1)
                        //{
                        //    dgvShow.Rows[iRow].Cells["qrImgUrl1"].Value = qrImgs[1];
                        //}
                    }

                    string strImgs = lRecords[i].MonitorImgFiles;
                    string[] imgs = strImgs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if(imgs != null && imgs.Length > 0)
                    {
                        dgvShow.Rows[iRow].Cells["monitorUrl1"].Value = imgs[0];
                        if(imgs.Length > 1)
                        {
                            dgvShow.Rows[iRow].Cells["monitorUrl2"].Value = imgs[1];
                        }
                        //if (imgs.Length > 2)
                        //{
                        //    dgvShow.Rows[iRow].Cells["monitorUrl3"].Value = imgs[2];
                        //}
                    }
                    dgvShow.Rows[iRow].Cells["addTime"].Value = lRecords[i].MonitorTime;
                }
            }
        }

        private void dgvShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //string strType = dgvShow.Columns[e.ColumnIndex].GetType().ToString();
                if (dgvShow.Columns[e.ColumnIndex].GetType() != typeof(DataGridViewLinkColumn))
                {
                    return;
                }
                string strUrl = dgvShow[e.ColumnIndex, e.RowIndex].Value.ToString();
                if(!string.IsNullOrEmpty(strUrl))
                {
                    System.Diagnostics.Process.Start(strUrl);
                }
            }
            catch
            {
                MessageBox.Show("文件不存在，请检查！");
            }
        }

        private void SnapDoWork()
        {
            string formatStr = string.Empty;
            string strQr = string.Empty;
            string strDirectory = string.Empty;
            string defaultFolder = ProgramManager.GetInstance().ImgsFolder;
            if (!string.IsNullOrEmpty(defaultFolder))
            {
                if (!System.IO.Directory.Exists(defaultFolder))//如果不存在就创建file文件夹
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(defaultFolder);
                        //strDirectory = defaultFolder;
                        strDirectory = string.Format(defaultFolder + "\\{0}\\", DateTime.Now.ToString("yyyy年MM月dd日"));
                        if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
                        {
                            System.IO.Directory.CreateDirectory(strDirectory);
                        }
                    }
                    catch
                    {
                        strDirectory = string.Format("SnapPictures\\{0}\\", DateTime.Now.ToString("yyyy年MM月dd日"));
                        if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
                        {
                            System.IO.Directory.CreateDirectory(strDirectory);
                        }
                    }
                }
                else
                {
                    strDirectory = string.Format(defaultFolder + "\\{0}\\", DateTime.Now.ToString("yyyy年MM月dd日"));
                    if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
                    {
                        System.IO.Directory.CreateDirectory(strDirectory);
                    }
                }
            }
            else
            {
                strDirectory = string.Format("SnapPictures\\{0}\\", DateTime.Now.ToString("yyyy年MM月dd日"));
                if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
                {
                    System.IO.Directory.CreateDirectory(strDirectory);
                }
            }
            while (true)
            {
                this.Invoke((EventHandler)delegate
                {
                    try
                    {
                        if (!videoQrcode.IsRunning)
                        {

                            videoQrcode.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logs.Log.Instance.LogWrite("打开QR摄像头异常：" + ex.StackTrace);
                    }
                    try
                    {
                        if (!videoMonitor.IsRunning)
                        {
                            videoMonitor.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logs.Log.Instance.LogWrite("打开VM摄像头异常：" + ex.Message);
                        Logs.Log.Instance.LogWrite("打开VM摄像头异常：" + ex.StackTrace);
                    }
                });
                try
                {
                    //读取到位标志寄存器
                    byte[] cmdBytes = new byte[] {0x3A,0x30,0x31,0x30,0x33,0x31,0x30,0x30,0x30,0x30,0x30,0x30, 0x31,0x45,0x42,0x0D,0x0A };
                    byte[] snapData = ReadCmd(MainManager.GetInstance().DeviceComm, cmdBytes);
                    bool isSnapFlag = false;
                    if(snapData != null && snapData.Length > 0)
                    {
                        Logs.Log.Instance.LogWrite("接收到数据：" + snapData.Length.ToString(), Logs.MsgLevel.Info);
                        if (snapData.Length > 11)
                        {
                            if (snapData[0] == 0x3A && snapData[9] == 0x36 && snapData[10] == 0x34) //数据是100
                            {
                                isSnapFlag = true;
                                snapBegin = true;//设置开始解码
                            }
                            else
                            {
                                snapBegin = false;
                                isSnapFlag = false;
                            }
                        }
                        else
                        {
                            snapBegin = false;
                            isSnapFlag = false;
                        }
                    }
                    else
                    {
                        snapBegin = false;
                        isSnapFlag = false;
                    }
                    //Thread.Sleep(500);//2秒钟识别
                    StringBuilder qrImgsSb = new StringBuilder();
                    StringBuilder sbPackImgs = new StringBuilder();

                    string strQrFile = DecodeImgName;
                    //========-------------=========
                    if(isSnapFlag)
                    {
                        //Logs.Log.Instance.LogWrite("开始拍照", Logs.MsgLevel.Info);
                        Bitmap imgQr = null;// videoQrcode.GetCurrentVideoFrame();
                        Bitmap imgCurrent = null;// videoMonitor.GetCurrentVideoFrame();
                        this.Invoke((EventHandler)delegate 
                        {
                            imgQr = videoQrcode.GetCurrentVideoFrame();
                            imgCurrent = videoMonitor.GetCurrentVideoFrame();
                        });
                        strQr = QrHelper.QrDecode(imgQr, out formatStr);

                        Bitmap imgQrAndCurrent = ImageHelper.CombineImage(imgQr, imgCurrent);

                        string fileOneName = "QrAndCurrent_" + DateTime.Now.ToString("ddHHmmssfff") + ".jpg";
                        if(string.IsNullOrEmpty(strQr))
                        {
                            Thread.Sleep(400);
                            Bitmap imgQr1 = null; // videoQrcode.GetCurrentVideoFrame();
                            this.Invoke((EventHandler)delegate 
                            {
                                imgQr1 = videoQrcode.GetCurrentVideoFrame();
                            });
                            strQr = QrHelper.QrDecode(imgQr1, out formatStr);
                        }
                        string filepathstr = string.Format(strDirectory + "{0}", fileOneName);
                        SaveImage(imgQrAndCurrent, filepathstr);
                        qrImgsSb.Append(filepathstr).Append(";");
                        //Logs.Log.Instance.LogWrite("保存图片" + filepathstr, Logs.MsgLevel.Info);
                        //保存到数据库
                        SaveDataBase(qrImgsSb.ToString(), sbPackImgs.ToString(), strQr);
                        snapBegin = false;
                        //---------发送反馈操作成功指令----------
                        //返回一个指令 : 01 06 10 0A 00 88 57 0D 0A
                        byte[] dealBytes = new byte[] { 0x3A, 0x30, 0x31, 0x30, 0x36, 0x31, 0x30, 0x30, 0x41, 0x30, 0x30, 0x38, 0x38, 0x35, 0x37, 0x0D, 0x0A };
                        SendCmd(MainManager.GetInstance().DeviceComm, dealBytes);
                        Thread.Sleep(500);
                    }
                }
                catch(Exception ex)
                {
                    Logs.Log.Instance.LogWrite("异常:" + ex.StackTrace,Logs.MsgLevel.Err);
                }
            }
        }

        private void NoUse()
        {
            try
            {
                DecodeQrContent = string.Empty;//先清空读取的
                DecodeImgName = string.Empty;
                //读取到位标志寄存器
                byte[] cmdBytes = new byte[] { 0x3A, 0x30, 0x31, 0x30, 0x33, 0x31, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x45, 0x42, 0x0D, 0x0A };
                byte[] snapData = ReadCmd(MainManager.GetInstance().DeviceComm, cmdBytes);
                bool isSnapFlag = false;
                if (snapData != null && snapData.Length > 0)
                {
                    if (snapData.Length > 11)
                    {
                        if (snapData[0] == 0x3A && snapData[9] == 0x36 && snapData[10] == 0x34) //数据是100
                        {
                            isSnapFlag = true;
                            snapBegin = true;//设置开始解码
                        }
                        else
                        {
                            snapBegin = false;
                            isSnapFlag = false;
                        }
                    }
                    else
                    {
                        snapBegin = false;
                        isSnapFlag = false;
                    }
                }
                else
                {
                    snapBegin = false;
                    isSnapFlag = false;
                }
                Thread.Sleep(2000);//2秒钟识别
                StringBuilder qrImgsSb = new StringBuilder();
                string strQr = DecodeQrContent;
                string strQrFile = DecodeImgName;
                //========-------------=========
                string strDirectory = string.Format("SnapPictures\\{0}\\", DateTime.Now.ToString("yyyy-MM"));
                if (!System.IO.Directory.Exists(strDirectory))//如果不存在就创建file文件夹
                {
                    System.IO.Directory.CreateDirectory(strDirectory);
                }
                //
                if (isSnapFlag)
                {
                    //判断有没有识别到
                    if (string.IsNullOrEmpty(strQr))
                    {
                        //返回一个指令（未找到二维码） : 01 06 10 0A 00 22 AD 0D 0A
                        byte[] findBytes = new byte[] { 0x3A, 0x30, 0x31, 0x30, 0x36, 0x31, 0x30, 0x30, 0x41, 0x30, 0x30, 0x32, 0x32, 0x42, 0x44, 0x0D, 0x0A };
                        SendCmd(MainManager.GetInstance().DeviceComm, findBytes);
                        //在拍一张
                        Bitmap image = videoQrcode.GetCurrentVideoFrame();
                        string filename = "QRmain1-" + DateTime.Now.ToString("ddHHmmssfff") + ".jpg";

                        string filepathstr = string.Format("SnapPictures\\{0}\\{1}", DateTime.Now.ToString("yyyy-MM"), filename);
                        SaveImage(image, filepathstr);
                        qrImgsSb.Append(filepathstr).Append(";");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        //保存拍到的QR文件
                        if (!string.IsNullOrEmpty(strQrFile))
                        {
                            qrImgsSb.Append(strQrFile);
                        }
                    }
                    string strQrNew = DecodeQrContent;
                    string strQrNewFile = DecodeImgName;
                    if (string.IsNullOrEmpty(strQrNew) && string.IsNullOrEmpty(strQr))
                    {
                        //在拍一张
                        Bitmap image = videoQrcode.GetCurrentVideoFrame();
                        string fileOneName = "QRslave-" + DateTime.Now.ToString("ddHHmmssfff") + ".jpg";

                        string filepathstr = string.Format("SnapPictures\\{0}\\{1}", DateTime.Now.ToString("yyyy-MM"), fileOneName);
                        SaveImage(image, filepathstr);
                        qrImgsSb.Append(filepathstr).Append(";");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strQr))
                        {
                            strQr = strQrNew;
                        }
                        if (!string.IsNullOrEmpty(strQrNewFile))
                        {
                            qrImgsSb.Append(strQrNewFile);
                        }
                    }
                    StringBuilder sbPackImgs = new StringBuilder();
                    for (int i = 0; i < 3; i++)
                    {
                        string monitorName = $"M{i.ToString()}-" + DateTime.Now.ToString("ddHHmmssfff") + ".jpg";
                        Bitmap imgMonitor = videoMonitor.GetCurrentVideoFrame();
                        string strPath = string.Format("SnapPictures\\{0}\\{1}", DateTime.Now.ToString("yyyy-MM"), monitorName);
                        SaveImage(imgMonitor, strPath);
                        sbPackImgs.Append(strPath).Append(";");
                        Thread.Sleep(250);
                    }
                    //保存到数据库
                    SaveDataBase(qrImgsSb.ToString(), sbPackImgs.ToString(), strQr);
                    snapBegin = false;
                    //---------发送反馈操作成功指令----------
                    //返回一个指令 : 01 06 10 0A 00 88 57 0D 0A
                    byte[] dealBytes = new byte[] { 0x3A, 0x30, 0x31, 0x30, 0x36, 0x31, 0x30, 0x30, 0x41, 0x30, 0x30, 0x38, 0x38, 0x35, 0x37, 0x0D, 0x0A };
                    SendCmd(MainManager.GetInstance().DeviceComm, dealBytes);
                }
            }
            catch (Exception ex)
            {
                Logs.Log.Instance.LogWrite("异常:" + ex.Message);
                Logs.Log.Instance.LogWrite("异常:" + ex.StackTrace);
            }
        }

        private void InitComm()
        {
            cboComm.Items.AddRange(MainManager.GetInstance().GetPorts());
            cboComm.SelectedItem = ProgramManager.GetInstance().PortName;
            cboRate.SelectedItem = ProgramManager.GetInstance().BaudRate.ToString();

            cboParity.SelectedItem = ProgramManager.GetInstance().Parity;
            cboStopBits.SelectedIndex = ProgramManager.GetInstance().StopBit;

            txtDataBits.Text = ProgramManager.GetInstance().DataBit.ToString();
            chkDTR1.Checked = ProgramManager.GetInstance().DTR;
            chkRTS1.Checked = ProgramManager.GetInstance().RTS;

            MainManager.GetInstance().DeviceComm = new System.IO.Ports.SerialPort();
            MainManager.GetInstance().DeviceComm.DataReceived += Comm_DataReceived;
        }

        private void InitConfig()
        {
            txtFolderPath.Text = ProgramManager.GetInstance().ImgsFolder;
        }

        private byte[] ReadCmd(SerialPort port, byte[] cmdBytes)
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Write(cmdBytes, 0, cmdBytes.Length);
                    Thread.Sleep(400);

                    int n = port.BytesToRead;
                    if(n > 0)
                    {
                        byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                        port.Read(buf, 0, n);//读取缓冲数据
                        return buf;
                    }
                    else
                    {
                        return new byte[] { };
                    }
                }
                else
                {
                    MessageBox.Show("端口未打开！");
                    return new byte[] { };
                }
            }
            catch
            {
                return new byte[] { };
            }
        }

        private bool SendCmd(SerialPort port, byte[] cmdBytes)
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Write(cmdBytes, 0, cmdBytes.Length);
                    Thread.Sleep(400);
                    int n = port.BytesToRead;
                    if (n > 0)
                    {
                        byte[] buf = new byte[n];
                        port.Read(buf, 0, n);
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("端口未打开！");
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void Comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(MainManager.GetInstance().DeviceComm.IsOpen)
            {
                MainManager.GetInstance().DeviceComm.Close();
            }
            else
            {
                if (cboComm.SelectedIndex < 0 || cboRate.SelectedIndex < 0)
                {
                    MessageBox.Show("请查看串口号或者波特率是否选择了！");
                    return;
                }
                if (cboStopBits.SelectedIndex < 1 || cboParity.SelectedIndex < 0)
                {
                    MessageBox.Show("请查看停止位或者校验位是否选择了！");
                    return;
                }


                //关闭时点击，则设置好端口，波特率后打开
                MainManager.GetInstance().DeviceComm.PortName = cboComm.Text;
                MainManager.GetInstance().DeviceComm.BaudRate = int.Parse(cboRate.Text);
                MainManager.GetInstance().DeviceComm.DataBits = int.Parse(txtDataBits.Text);//8;
                switch(cboStopBits.SelectedIndex)
                {
                    case 0:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.None;
                        break;
                    case 1:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.One;
                        break;
                    case 2:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.Two;
                        break;
                    case 3:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.OnePointFive;
                        break;
                }

                MainManager.GetInstance().DeviceComm.Parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);
                MainManager.GetInstance().DeviceComm.DtrEnable = chkDTR1.Checked;
                MainManager.GetInstance().DeviceComm.RtsEnable = chkRTS1.Checked;

                try
                {
                    MainManager.GetInstance().DeviceComm.Open();
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    MainManager.GetInstance().DeviceComm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show("打开串口异常:" + ex.Message);
                    return;
                }
            }

            btnOpen.Text = MainManager.GetInstance().DeviceComm.IsOpen ? "Close" : "Open";
            {
                bool isopen = MainManager.GetInstance().DeviceComm.IsOpen;
                chkDTR1.Enabled = !isopen;
                chkRTS1.Enabled = !isopen;
                cboComm.Enabled = !isopen;
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if(cboStopBits.SelectedIndex < 1)
                {
                    MessageBox.Show("停止位至少为1！");
                    return;
                }
                ProgramManager.GetInstance().PortName = cboComm.Text;
                ProgramManager.GetInstance().BaudRate = int.Parse(cboRate.Text);
                ProgramManager.GetInstance().DataBit = int.Parse(txtDataBits.Text);
                ProgramManager.GetInstance().Parity = cboParity.Text;
                ProgramManager.GetInstance().StopBit = cboStopBits.SelectedIndex;
                ProgramManager.GetInstance().DTR = chkDTR1.Checked;
                ProgramManager.GetInstance().RTS = chkRTS1.Checked;
                ProgramManager.GetInstance().Save();
                MessageBox.Show("保存成功！");
            }
            catch
            {

            }
        }

        private void btnOpenCamera_Click(object sender, EventArgs e)
        {
            if(btnOpenCamera.Text == "打开摄像头")
            {
                //打开摄像头
                canSnap = false;
                if (StartDevice(videoQrcode, ProgramManager.GetInstance().QrCamera,ProgramManager.GetInstance().QrCameraRate))
                {
                    canSnap = true;
                }
                else
                {
                    canSnap = false;
                    MessageBox.Show("箱外摄像头打开出错，请检查！");
                }
                if (StartDevice(videoMonitor, ProgramManager.GetInstance().MonitorCamera,ProgramManager.GetInstance().MonitorCameraRate))
                {
                    if(canSnap)
                    {
                        canSnap = true;
                    }
                }
                else
                {
                    canSnap = false;
                    MessageBox.Show("箱内摄像头打开出错，请检查！");
                }
                btnOpenCamera.Text = "关闭摄像头";
                //if (canSnap)
                //{
                //    btnOpenCamera.Text = "关闭摄像头";
                //}
            }
            else
            {
                StopDevice(videoQrcode);
                StopDevice(videoMonitor);
                btnOpenCamera.Text = "打开摄像头";
            }
        }

        private void CommOpen()
        {
            if (MainManager.GetInstance().DeviceComm.IsOpen)
            {
                MainManager.GetInstance().DeviceComm.Close();
            }
            else
            {
                //关闭时点击，则设置好端口，波特率后打开
                MainManager.GetInstance().DeviceComm.PortName = ProgramManager.GetInstance().PortName;
                MainManager.GetInstance().DeviceComm.BaudRate = ProgramManager.GetInstance().BaudRate;
                MainManager.GetInstance().DeviceComm.DataBits = ProgramManager.GetInstance().DataBit;//8;
                switch (ProgramManager.GetInstance().StopBit)
                {
                    case 0:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.None;
                        break;
                    case 1:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.One;
                        break;
                    case 2:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.Two;
                        break;
                    case 3:
                        MainManager.GetInstance().DeviceComm.StopBits = StopBits.OnePointFive;
                        break;
                }
                MainManager.GetInstance().DeviceComm.Parity = (Parity)Enum.Parse(typeof(Parity), ProgramManager.GetInstance().Parity);
                MainManager.GetInstance().DeviceComm.DtrEnable = chkDTR1.Checked;
                MainManager.GetInstance().DeviceComm.RtsEnable = chkRTS1.Checked;

                try
                {
                    MainManager.GetInstance().DeviceComm.Open();
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    MainManager.GetInstance().DeviceComm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show("打开串口异常:" + ex.Message);
                    return;
                }
            }

            btnOpen.Text = MainManager.GetInstance().DeviceComm.IsOpen ? "Close" : "Open";
            {
                bool isopen = MainManager.GetInstance().DeviceComm.IsOpen;
                chkDTR1.Enabled = !isopen;
                chkRTS1.Enabled = !isopen;
                cboComm.Enabled = !isopen;
            }
        }

        private void btnFileFolders_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if(folderBrowser.ShowDialog()== DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowser.SelectedPath;
                ProgramManager.GetInstance().ImgsFolder = folderBrowser.SelectedPath;
            }
        }
    }
}

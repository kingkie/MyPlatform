using AForge.Controls;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;

namespace RH.UI.Richer.Helper
{
    public class VideoCamera
    {
        private VideoSourcePlayer player;
        private Stopwatch sw = new Stopwatch();
        private Random rd = new Random();

        /// <summary>
        /// 10帧计数
        /// </summary>
        private int face_g = 0;

        private FilterInfoCollection videoDevices;
        /// <summary>
        /// 是否正在识别处理中
        /// </summary>
        bool busy = false;

        public int VideoUsed
        {
            get;
            set;
        }  = 0; //当前使用的视频源索引

        /// <summary>
        /// 人脸核验正确处理事件变量
        /// </summary>
        public event Action<string, Bitmap> FaceDetect;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="player">传入</param>
        public VideoCamera(VideoSourcePlayer player)
        {
            this.player = player;
            player.NewFrame += Player_NewFrame;

            DeviceId = "video" + DateTime.Now.ToString("fff") + rd.Next(100,999).ToString();

            DeviceName = "摄像头";
        }

        /// <summary>
        /// 返回设备列表
        /// </summary>
        public FilterInfoCollection VideoDevices
        {
            get { return videoDevices; }
        }

        public string DeviceId
        {
            get;
            set;
        }

        public string DeviceName
        {
            get;
            set;
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="idFace">人脸摄像数据</param>
        /// <returns>返回是否开启成功</returns>
        public bool Start()
        {
            try
            {
                player.VideoSource = GetVideoSource(VideoUsed);
                if (player.VideoSource == null)
                {
                    return false;
                }
                else
                {
                    player.Start();
                    //return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭摄像头
        /// </summary>
        public void Stop()
        {
            if (player.IsRunning)
            {
                player.WaitForStop();
            }
        }

        private void Player_NewFrame(object sender, ref Bitmap image)
        {
            //if (image != null)
            //{
            //    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //}


       }

        /// <summary>
        /// 试着打开摄像头
        /// </summary>
        /// <returns>返回是否成功</returns>
        private bool TryOpenVideo()
        {
            if (videoDevices == null)
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            }
            //没有检测到摄像头
            if (videoDevices.Count == 0)
            {
                return false;
            }
            if (VideoUsed >= videoDevices.Count)
            {
                return false;
            }

            while (VideoUsed < videoDevices.Count)
            {
                player.VideoSource = GetVideoSource(VideoUsed);
                player.Start();
                Thread.Sleep(8000);
                //player.VideoSource.FramesReceived
                //Bitmap bitmap = player.GetCurrentVideoFrame();
                if (player.VideoSource != null && player.VideoSource.FramesReceived > 0)
                {
                    return true;
                }
                else
                {
                    VideoUsed++;
                    continue;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取视频源
        /// </summary>
        /// <returns>返回视频源</returns>
        private VideoCaptureDevice GetVideoSource(int idx)
        {
            if (videoDevices == null)
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            }
            //没有检测到摄像头
            if (videoDevices.Count == 0)
            {
                return null;
            }
            if (idx > videoDevices.Count)
            {
                return null;
            }
            //连接第一个摄像头
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[idx].MonikerString);
            VideoCapabilities videoResolution = videoSource.VideoCapabilities[idx];//.First(ii => ii.FrameSize.Width == p.VideoSource.VideoCapabilities.Max(jj => jj.FrameSize.Width)); //获取摄像头最高的分辨率
                                                                                   //var videoResolution = videoSource.VideoCapabilities[videoSource.VideoCapabilities.Length - 1]; //.First(ii => ii.FrameSize.Width == p.VideoSource.VideoCapabilities.Max(jj => jj.FrameSize.Width)); //获取摄像头最高的分辨率
            videoSource.VideoResolution = videoResolution;
            // videoSource.FramesReceived
            return videoSource;
        }

        /// <summary>
        /// 获取视频源
        /// </summary>
        /// <returns>返回视频源</returns>
        private VideoCaptureDevice GetVideoSourceN(int useIndex)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //没有检测到摄像头
            if (videoDevices.Count == 0)
            {
                return null;
            }

            if(useIndex > videoDevices.Count)
            {
                return null;
            }
            //连接第一个摄像头
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[useIndex].MonikerString);
            VideoCapabilities videoResolution = videoSource.VideoCapabilities.Where(item => item.FrameSize.Width >= 640).OrderBy(item => item.FrameSize.Width).First();
            videoSource.VideoResolution = videoResolution;
            return videoSource;
        }
    }
}

using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Yu3zx.PackingMonitor.Ctrls
{
    public class CamerasManager
    {
        private FilterInfoCollection videoDevices;
        #region 单例
        private static CamerasManager instance = null;
        private static object singleLock = new object(); //锁同步

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static CamerasManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new CamerasManager();
                }
            }
            return instance;
        }
        #endregion End

        public FilterInfoCollection VideoDevices
        {
            get
            {
                if (videoDevices == null)
                {
                    videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                }
                return videoDevices;
            }
        }

        /// <summary>
        /// 获取视频源
        /// </summary>
        /// <returns>返回视频源</returns>
        public VideoCaptureDevice GetVideoSource(int idx)
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
            VideoCapabilities videoResolution = videoSource.VideoCapabilities.First(ii => ii.FrameSize.Width == videoSource.VideoCapabilities.Max(jj => jj.FrameSize.Width));
            videoSource.VideoResolution = videoResolution;
            // videoSource.FramesReceived
            return videoSource;
        }

        /// <summary>
        /// 获取视频源
        /// </summary>
        /// <returns>返回视频源</returns>
        public VideoCaptureDevice GetVideoSource(int idx,int rate)
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
            VideoCapabilities videoResolution ;
            if (rate > -1 && videoSource.VideoCapabilities.Length >= rate)
            {
                videoResolution = videoSource.VideoCapabilities[rate];
            }
            else
            {
                videoResolution = videoSource.VideoCapabilities.First(ii => ii.FrameSize.Width == videoSource.VideoCapabilities.Max(jj => jj.FrameSize.Width));
            }
            videoSource.VideoResolution = videoResolution;

            return videoSource;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Yu3zx.PackingMonitor.Ctrls
{
    /// <summary>
    /// Camera 的摘要说明。
    /// </summary>
    public class AviCamera
    {
        private IntPtr lwndC;
        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;


        // 构造函数
        public AviCamera(IntPtr handle, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
        }

        // 帧回调的委托
        public delegate void RecievedFrameEventHandler(byte[] data);
        public event RecievedFrameEventHandler RecievedFrame;
        private AviCapture.FrameEventHandler mFrameEventHandler;

        // 关闭摄像头
        public void CloseWebcam()
        {
            this.capDriverDisconnect(this.lwndC);
        }

        // 开启摄像头
        public void StartWebCam()
        {
            byte[] lpszName = new byte[100];
            byte[] lpszVer = new byte[100];

            AviCapture.capGetDriverDescriptionA(0, lpszName, 100, lpszVer, 100);
            this.lwndC = AviCapture.capCreateCaptureWindowA(lpszName, AviCapture.WS_VISIBLE + AviCapture.WS_CHILD, 0, 0, mWidth, mHeight, mControlPtr, 0);

            if (this.capDriverConnect(this.lwndC, 0))
            {
                this.capPreviewRate(this.lwndC, 66);
                this.capPreview(this.lwndC, true);
                AviCapture.BITMAPINFO bitmapinfo = new AviCapture.BITMAPINFO();
                bitmapinfo.bmiHeader.biSize = AviCapture.SizeOf(bitmapinfo.bmiHeader);
                bitmapinfo.bmiHeader.biWidth = this.mWidth;
                bitmapinfo.bmiHeader.biHeight = this.mHeight;
                bitmapinfo.bmiHeader.biPlanes = 1;
                bitmapinfo.bmiHeader.biBitCount = 24;
                this.capSetVideoFormat(this.lwndC, ref bitmapinfo, AviCapture.SizeOf(bitmapinfo));
                this.mFrameEventHandler = new AviCapture.FrameEventHandler(FrameCallBack);
                this.capSetCallbackOnFrame(this.lwndC, this.mFrameEventHandler);
                AviCapture.SetWindowPos(this.lwndC, 0, 0, 0, mWidth, mHeight, 6);
            }
        }
        // 以下为私有函数
        private bool capDriverConnect(IntPtr lwnd, short i)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_DRIVER_CONNECT, i, 0);
        }

        private bool capDriverDisconnect(IntPtr lwnd)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }

        private bool capPreview(IntPtr lwnd, bool f)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_SET_PREVIEW, f, 0);
        }

        private bool capPreviewRate(IntPtr lwnd, short wMS)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_SET_PREVIEWRATE, wMS, 0);
        }

        private bool capSetCallbackOnFrame(IntPtr lwnd, AviCapture.FrameEventHandler lpProc)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
        }

        private bool capSetVideoFormat(IntPtr hCapWnd, ref AviCapture.BITMAPINFO BmpFormat, int CapFormatSize)
        {
            return AviCapture.SendMessage(hCapWnd, AviCapture.WM_CAP_SET_VIDEOFORMAT, CapFormatSize, ref BmpFormat);
        }

        ///  
        /// 抓图  
        ///  
        /// 要保存bmp文件的路径  
        public void SnapImage(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            AviCapture.SendMessage(mControlPtr, AviCapture.WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }

        private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
        {
            AviCapture.VIDEOHDR videoHeader = new AviCapture.VIDEOHDR();
            byte[] VideoData;
            videoHeader = (AviCapture.VIDEOHDR)AviCapture.GetStructure(lpVHdr, videoHeader);
            VideoData = new byte[videoHeader.dwBytesUsed];
            AviCapture.Copy(videoHeader.lpData, VideoData);
            if (this.RecievedFrame != null)
                this.RecievedFrame(VideoData);
        }
    }
}

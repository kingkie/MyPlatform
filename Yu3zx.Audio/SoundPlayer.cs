using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectSound;
using System.Threading;
using System.Windows.Forms;

namespace Yu3zx.Audio
{
    /// <summary>
    /// 使用说明，主要使用Microsoft.DirectX.DirectSound组件进行PCM流播放
    /// Device dev = new Device();
    /// dev.SetCooperativeLevel(this, CooperativeLevel.Normal);
    /// WaveFormat wf = DirectSoundManager.CreateWaveFormat(24000, 16, 2);//采样率，采样点数，声道数
    /// SoundPlayer splay = new SoundPlayer(this, dev, wf);
    /// splay.Write(byte[]);//开启一条线程不断写入数据开始播放
    /// </summary>
    public class SoundPlayer : IDisposable
    {
        #region 私有成员

        private const int MaxLatencyMs = 300;

        private const int NumberRecordNotifications = 4;

        private readonly CircularBuffer circularBuffer;

        private readonly int m_BufferBytes;

        private readonly bool m_OwnsDevice;

        private readonly int notifySize;

        private readonly BufferPositionNotify[] positionNotify;

        private bool isRunning;

        private SecondaryBuffer m_Buffer;

        private Device m_Device;

        private int nextWriteOffset;

        private AutoResetEvent notificationEvent;

        private Notify notify;

        private Thread notifyThread;

        #endregion

        #region 构造函数

        public SoundPlayer(Control owner, WaveFormat format)
            : this(owner, null, format)
        {

        }

        public SoundPlayer(Control owner, Device device, WaveFormat format)
        {
            positionNotify = new BufferPositionNotify[5];
            notificationEvent = null;
            notify = null;
            notifyThread = null;
            notifySize = 0;
            m_Device = device;
            if (m_Device == null)
            {
                m_Device = new Device();
                m_Device.SetCooperativeLevel(owner, CooperativeLevel.Normal);
                m_OwnsDevice = true;
            }
            // 设定通知的大小, 大小为播放一秒钟声音所需要的字节。这里为什么除以8，我不清楚
            notifySize = (1024 > (format.AverageBytesPerSecond / 8)) ? (1024) : ((format.AverageBytesPerSecond / 8));
            notifySize = (notifySize - (notifySize % format.BlockAlign));
            m_BufferBytes = (notifySize * 4); //整体缓冲区的大小
            BufferDescription desc = new BufferDescription(format);

            //缓冲区具有控制音量的能力；
            desc.ControlVolume = true;

            //缓冲区具有控制位置的能力。
            desc.ControlPositionNotify = true;

            //设置缓冲区能取到当前的播放位置
            desc.CanGetCurrentPosition = true;

            //缓冲区不具有控制3D音效的能力；
            desc.Control3D = false;

            //Specifies whether the buffer supports effects processing.
            desc.ControlEffects = false;

            //缓冲区具有控制频率的能力；
            desc.ControlFrequency = true;

            //缓冲区具有控制左右声道的能力；
            desc.ControlPan = true;

            //设置是否使用全局缓存
            desc.GlobalFocus = true;

            //设置缓冲区大小为整个缓冲区的大小
            desc.BufferBytes = m_BufferBytes;

            //创建辅助缓冲区
            m_Buffer = new SecondaryBuffer(desc, m_Device);

            //创建环形缓冲区
            circularBuffer = new CircularBuffer((m_BufferBytes * 10));

            InitNotifications();
            m_Buffer.Play(0, BufferPlayFlags.Looping);
        }

        public SoundPlayer(Control owner, int sr, short bps, short ch)
            : this(owner, null, DirectSoundManager.CreateWaveFormat(sr, bps, ch))
        {

        }

        public SoundPlayer(Control owner, Device device, int sr, short bps, short ch)
            : this(owner, device, DirectSoundManager.CreateWaveFormat(sr, bps, ch))
        {

        }
        #endregion

        #region 公开属性

        public int BitsPerSample
        {
            get { return m_Buffer.Format.BitsPerSample; }
        }

        public int Channels
        {
            get { return m_Buffer.Format.Channels; }
        }

        public Device Device
        {
            get { return m_Device; }
        }

        public int SamplingRate
        {
            get { return m_Buffer.Format.SamplesPerSecond; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Stop();
            if (m_Buffer != null)
            {
                m_Buffer.Dispose();
                m_Buffer = null;
            }
            if (m_OwnsDevice && (m_Device != null))
            {
                m_Device.Dispose();
                m_Device = null;
            }
            GC.SuppressFinalize(this);
        }

        ~SoundPlayer()
        {
            Dispose();
        }

        #endregion

        #region 私有方法

        private void InitNotifications()
        {
            notifyThread = new Thread(NotifyThreadHandler);
            isRunning = true;
            notifyThread.IsBackground = true;
            notifyThread.Start();
            notificationEvent = new AutoResetEvent(false);
            notify = new Notify(m_Buffer);

            //把整个缓冲区分成4个缓冲区片段，每播放4分之一就会给写线程发送一个信号
            for (int i = 0; i < 4; i = (i + 1))
            {
                positionNotify[i].Offset = (((notifySize * i) + notifySize) - 1);
                positionNotify[i].EventNotifyHandle = notificationEvent.SafeWaitHandle.DangerousGetHandle();
            }
            notify.SetNotificationPositions(positionNotify, 4);
            nextWriteOffset = 0;
        }

        private void NotifyThreadHandler()
        {
            while (isRunning)
            {
                try
                {
                    notificationEvent.WaitOne(-1, true);
                    Play();
                }
                catch (Exception)
                {
                }
            }
        }
        /// <summary>
        /// 播放缓冲区数据
        /// </summary>
        public void Play()
        {
            try
            {
                try
                {
                    int currentPlayPosition;
                    int currentWritePosition;
                    m_Buffer.GetCurrentPosition(out currentPlayPosition, out currentWritePosition);

                    //得到刚刚播放完的缓冲区片段，这个片段需要用新的数据去填充
                    int lockSize = (currentWritePosition - nextWriteOffset);

                    //todo:这里不知道什么时候会发生
                    if (lockSize < 0)
                    {
                        lockSize = (lockSize + m_BufferBytes);
                    }

                    //对齐需要填充的缓冲区片段
                    lockSize = (lockSize - (lockSize % notifySize));
                    if (0 != lockSize)
                    {
                        if (lockSize == m_BufferBytes)
                        {
                        }
                        byte[] data = new byte[lockSize];
                        if (circularBuffer.Read(data) > 0)
                        {
                            m_Buffer.Write(nextWriteOffset, data, LockFlag.None);
                            nextWriteOffset = (nextWriteOffset + lockSize);

                            //如果完整写完一次缓冲区，那么把写数据指针放到缓冲区的最开始，
                            //因为前面设置了m_Buffer.Play(0, BufferPlayFlags.Looping);
                            //所以系统在播放缓冲区后会自动重新开始播放缓冲区起始处的声音数据
                            nextWriteOffset = (nextWriteOffset % m_BufferBytes);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            finally
            {
            }
        }

        #endregion

        #region 公开方法
        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            isRunning = false;
            if (m_Buffer != null)
            {
                m_Buffer.Stop();
            }
        }
        /// <summary>
        /// 接收数据写入缓冲区并使播放
        /// </summary>
        /// <param name="data"></param>
        public void Write(byte[] data)
        {
            try
            {
                //Console.WriteLine("播放声音:{0}", data.Length);
                circularBuffer.Write(data);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}

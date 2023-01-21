using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectSound;

namespace Yu3zx.Audio
{
    public class DirectSoundManager
    {
        /// <summary>
        /// 创建格式
        /// </summary>
        /// <param name="hz">SamplesPerSecond，采样率如：44100Hz</param>
        /// <param name="bits">BitsPerSample，每个采样点数；8-bit或16-bit</param>
        /// <param name="channels">声道的设置，值为1时是单声道，为2时是双声道</param>
        /// <returns></returns>
        public static WaveFormat CreateWaveFormat(int hz, short bits, short channels)
        {

            WaveFormat format = new WaveFormat();

            //声音的格式，通常使用WAVE_FORMAT_PCM来设定，

            //因为PCM是比较常用的声音格式。

            format.FormatTag = WaveFormatTag.Pcm;

            //采样率（单位：赫兹）典型值：11025、22050、44100Hz

            format.SamplesPerSecond = hz;

            //每个采样点数；8-bit或16-bit；

            format.BitsPerSample = bits;

            //声道的设置，当其值为1时是单声道，为2时是双声道；

            format.Channels = channels;

            //每个采样点字节数

            format.BlockAlign = (short)(format.Channels * (format.BitsPerSample / 8));

            //平均传输率,每秒的数据流量

            format.AverageBytesPerSecond = format.BlockAlign * format.SamplesPerSecond;

            return format;

        }

        #region 格式属性

        public static WaveFormat DefaultFormat
        {
            get
            {
                return WaveFormat_8000_8_1;
            }
        }

        public static WaveFormat WaveFormat_11025_8_1
        {
            get
            {
                return CreateWaveFormat(0x2b11, 8, 1);
            }
        }

        public static WaveFormat WaveFormat_22050_16_2
        {
            get
            {
                return CreateWaveFormat(0x5622, 0x10, 2);
            }
        }

        public static WaveFormat WaveFormat_44100_16_2
        {
            get
            {
                return CreateWaveFormat(0xac44, 0x10, 2);
            }
        }

        public static WaveFormat WaveFormat_8000_8_1
        {
            get
            {
                return CreateWaveFormat(0x1f40, 8, 1);
            }
        }
        #endregion

    }
}

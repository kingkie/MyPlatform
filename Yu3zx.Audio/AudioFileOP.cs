using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Yu3zx.Audio
{
    public class AudioFileOP
    {
        /// <summary>
        /// 加载WAV头
        /// </summary>
        /// <param name="ms"></param>  音频流
        public static void GenerateWav(FileStream ms)
        {
            if (ms == null)
                return;
            byte[] wav_header = new byte[60]{
	            0x52, 0x49, 0x46, 0x46,   //riff                                               
	            0xff, 0xff, 0xff, 0xff,   //文件长                                           
	            0x57, 0x41, 0x56, 0x45,   //wave                                                
	            0x66, 0x6d, 0x74, 0x20,   //fmt                                              
	            0x14, 0, 0, 0,            //pcwaveformat 长度                                        
	            0x11, 0,                															
	            0x01, 0,                                                                 
	            0x80, 0x3e, 0, 0,  //采样率0x80, 0x3e, 0, 0, 16k                                    
	            0xd7, 0x0f, 0, 0,  // 每秒字节数 0xd7, 0x0f, 0, 0, 16k     
	            0x00, 0x01,                                                           
	            0x04, 0,                                                              
	            0x02, 0,
	            0xf9,0x01,														
	            0x66,0x61,0x63,0x74,	//fact	   									
	            0x04,0,0,0,				//fact长度									
	            0xff, 0xff, 0xff, 0xff,	//fact值												 
	            0x64, 0x61, 0x74, 0x61, //data                                                      
	            0xff, 0xff, 0xff, 0xff  //音频数据长度                                     
            };
            ms.Seek(0, SeekOrigin.Begin);
            ms.Write(wav_header, 0, wav_header.Length);
        }
        /// <summary>
        /// 修改WAV文件相关参数
        /// </summary>
        /// <param name="fs"></param>  文件流
        public static void ModifyFile(FileStream fs)
        {
            BinaryWriter mWriter = new BinaryWriter(fs);
            // 回写长度信息
            mWriter.Seek(4, SeekOrigin.Begin);
            mWriter.Write((int)(fs.Length - 8));   // 写文件长度
            mWriter.Seek(56, SeekOrigin.Begin);
            mWriter.Write(fs.Length - 60);                // 写数据长度
            mWriter.Close();
            mWriter = null;
        }
    }
}

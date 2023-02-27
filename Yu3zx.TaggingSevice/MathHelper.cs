using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class MathHelper
    {
        /// <summary>
        /// short转bytes
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static byte[] ShortToBytes(short number)
        {
            byte[] bShort = new byte[2];
            bShort[0] = (byte)(number >> 8);
            bShort[1] = (byte)(number & 255);
            return bShort;
        }

        public static byte[] IntToBytes(int value)
        {
            byte[] src = new byte[4];
            src[3] = (byte)((value >> 24) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }

        public static byte[] Short2Bytes(short num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            return bytes;
        }

        public static byte[] Int2Bytes(int num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            return bytes;
        }
    }
}

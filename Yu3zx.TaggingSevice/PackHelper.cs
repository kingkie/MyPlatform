using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class PackHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lBQuality"></param>
        /// <returns></returns>
        public byte[] BuildBTypeValue(List<int> lBQuality)
        {
            int iMin = Math.Min(16, lBQuality.Count);
            byte[] fByte = new byte[2] { 0x00, 0x00 };
            for (int i = 0; i < iMin; i++)
            {
                if (lBQuality[i] > 16)
                {
                    continue;
                }
                else if (lBQuality[i] > -1 && lBQuality[i] < 8)
                {
                    fByte[1] = SetBitValue(fByte[1], lBQuality[i]);
                }
                else if (lBQuality[i] > 7 && lBQuality[i] < 16)
                {
                    fByte[0] = SetBitValue(fByte[0], lBQuality[i] % 8);
                }
            }
            return fByte;
        }

        private byte SetBitValue(byte data, int index)
        {
            if (index > 7 || index < 0)
            {
                return data;
            }
            int vBit = 1 << index;
            vBit = vBit & 0xFF;
            data = (byte)(data | vBit);
            return data;
        }
    }
}

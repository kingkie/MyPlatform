using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NJBX.Audio
{
    public class SoundUtil
    {

        private static byte[] ADPCM2PCM(byte[] data)
        {
            byte[] convertedData = new byte[(data.Length) * 4];//扩大4倍

            stepSize = 7;
            newSample = 0;
            index = 0;

            var writeCounter = 0;
            for (var x = 4; x < data.Length; x++)
            {
                // First 4 bytes of a block contain initialization information 
                if ((x % blockSize) < 4)
                {
                    if (x % blockSize == 0) // New block 
                    {
                        // set predictor/NewSample and index from 
                        // the preamble of the block. 
                        newSample = (short)(data[x + 1] | data[x]);
                        index = data[x + 2];
                    }
                    continue;
                }
                // Get the first 4 bits from the byte array, 
                var convertedSample = calculateNewSample((byte)(data[x] >> 4)); // convert 4 bit ADPCM sample to 16 bit PCM sample 
                // Store 16 bit PCM sample into output byte array 
                convertedData[writeCounter++] = (byte)((byte)convertedSample >> 8);
                convertedData[writeCounter++] = (byte)((byte)convertedSample & 0x0ff);
                // Convert the next 4 bits of the 8 bit array. 
                convertedSample = calculateNewSample((byte)(data[x] & 0x0f)); // convert 4 bit ADPCM sample to 16 bit PCM sample. 
                // Store 16 bit PCM sample into output byte array 
                convertedData[writeCounter++] = (byte)(convertedSample >> 8);
                convertedData[writeCounter++] = (byte)(convertedSample & 0x0ff);
            }
            // Conversion complete, return data 
            return convertedData;
        }
        static int stepSize = 0;
        static int newSample;
        static int index;
        private static short calculateNewSample(byte sample)
        {
            //Debug.Assert(sample < 16, "Bad sample!");

            var indexTable = new int[16] { -1, -1, -1, -1, 2, 4, 6, 8, -1, -1, -1, -1, 2, 4, 6, 8 };

            var stepSizeTable = new int[89] { 7, 8, 9, 10, 11, 12, 13, 14, 16, 17,  
                                        19, 21, 23, 25, 28, 31, 34, 37, 41, 45,  
                                        50, 55, 60, 66, 73, 80, 88, 97, 107, 118,  
                                        130, 143, 157, 173, 190, 209, 230, 253, 279, 307, 
                                        337, 371, 408, 449, 494, 544, 598, 658, 724, 796, 
                                        876, 963, 1060, 1166, 1282, 1411, 1552, 1707, 1878, 2066,  
                                        2272, 2499, 2749, 3024, 3327, 3660, 4026, 4428, 4871, 5358, 
                                        5894, 6484, 7132, 7845, 8630, 9493, 10442, 11487, 12635, 13899,  
                                        15289, 16818, 18500, 20350, 22385, 24623, 27086, 29794, 32767};

            var sign = sample & 8;
            var delta = sample & 7;
            var difference = stepSize >> 3;

            // originalsample + 0.5 * stepSize / 4 + stepSize / 8 optimization. 
            //http://www.cs.columbia.edu/~hgs/audio/dvi/p34.jpg 

            if ((delta & 4) != 0)
                difference += stepSize;
            if ((delta & 2) != 0)
                difference += stepSize >> 1;
            if ((delta & 1) != 0)
                difference += stepSize >> 2;

            if (sign != 0)
                newSample -= (short)difference;
            else
                newSample += (short)difference;

            // Increment index 
            index += indexTable[sample];

            index = (int)MathHelper.Clamp(index, 0, 88);

            newSample = (short)MathHelper.Clamp(newSample, -32768, 32767); // clamp between appropriate ranges 

            // compute new stepSize. 
            stepSize = stepSizeTable[index];

            return (short)newSample;
        }
    }
}

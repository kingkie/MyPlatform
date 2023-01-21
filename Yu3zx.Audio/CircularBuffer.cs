using System;

namespace Yu3zx.Audio
{
    public class CircularBuffer
    {
        private readonly byte[] buffer;

        private int readLoop;

        private int readPosition;

        private int writeLoop;

        private int writePosition;

        public int Length
        {
            get
            {
                return this.buffer.Length;
            }
        }
        /// <summary>
        /// 读定位指针
        /// </summary>
        public int ReadPosition
        {
            get
            {
                return this.readPosition;
            }
        }
        /// <summary>
        /// 写定位指针
        /// </summary>
        public int WritePosition
        {
            get
            {
                return this.writePosition;
            }
        }

        public CircularBuffer(int fixedCapacity)
        {
            this.buffer = new byte[fixedCapacity];
        }
        /// <summary>
        /// 读取缓冲区
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Read(byte[] data)
        {
            int num = this.Length - this.readPosition;
            num %= this.Length;
            int result;
            if (this.readLoop * this.Length + this.readPosition + data.Length > this.writeLoop * this.Length + this.writePosition)
            {
                result = -1;
            }
            else
            {
                if (num >= data.Length)
                {
                    Array.Copy(this.buffer, this.readPosition, data, 0, data.Length);
                    this.readPosition += data.Length;
                }
                else
                {
                    Array.Copy(this.buffer, this.readPosition, data, 0, num);
                    this.readPosition += num;
                    this.readPosition %= this.Length;
                    Array.Copy(this.buffer, this.readPosition, data, num, data.Length - num);
                    this.readPosition += data.Length - num;
                    this.readLoop++;
                }
                this.readPosition %= this.Length;
                result = data.Length;
            }
            return result;
        }
        /// <summary>
        /// 写缓冲区
        /// </summary>
        /// <param name="data"></param>
        public void Write(byte[] data)
        {
            int num = this.Length - this.writePosition;
            num %= this.Length;
            if (num >= data.Length)
            {
                Array.Copy(data, 0, this.buffer, this.writePosition, data.Length);
                this.writePosition += data.Length;
            }
            else
            {
                Array.Copy(data, 0, this.buffer, this.writePosition, num);
                this.writePosition += num;
                this.writePosition %= this.Length;
                Array.Copy(data, num, this.buffer, this.writePosition, data.Length - num);
                this.writePosition += data.Length - num;
                this.writeLoop++;
            }
            this.writePosition %= this.Length;
        }
    }
}

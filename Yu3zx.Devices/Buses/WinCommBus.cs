using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yu3zx.Devices.Interfaces;
using System.IO.Ports;
using Newtonsoft.Json;

namespace Yu3zx.Devices.Buses
{
    public class WinCommBus:IBus
    {
        private string _busid = "comm" + DateTime.Now.ToString("MMddHHmmfff");
        private string _busname = "Windows串口总线";
        private string _PortName;
        private int _BaudRate;
        private int _databits;
        private StopBits _stopbits;
        private Parity _parity;

        private SerialPort _serialport = new SerialPort();
        public WinCommBus()
        {

        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string BusId
        {
            set { _busid = value; }
            get { return _busid; }
        }
        /// <summary>
        /// 总线名称
        /// </summary>
        public string BusName
        {
            set { _busname = value; }
            get { return _busname; }
        }

        /// <summary>
        /// 种类名称
        /// </summary>
        public string FriendlyName
        {
            get
            {
                return "Windows串口(RS232)";
            }
        }

        /// <summary>
        /// 串口号
        /// </summary>
        public string WinPortName
        {
            get { return _PortName; }
            set 
            { 
                _PortName = value;
                if (_serialport != null)
                    _serialport.PortName = _PortName;
            }
        }
        /// <summary>
        /// 波特率
        /// </summary>
        public int WinBaudRate
        {
            get { return _BaudRate; }
            set 
            { 
                _BaudRate = value;
                if (_serialport != null)
                    _serialport.BaudRate = _BaudRate;
            }
        }
        /// <summary>
        /// 数据位
        /// </summary>
        public int WinDataBits
        {
            get { return _databits; }
            set 
            { 
                _databits = value;
                if (_serialport != null)
                    _serialport.DataBits = _databits;
            }

        }
        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits WinStopBits
        {
            get { return _stopbits; }
            set 
            { 
                _stopbits = value;
                if (_serialport != null)
                    _serialport.StopBits = _stopbits;
            }
        }
        /// <summary>
        /// 校验方式
        /// </summary>
        public Parity WinParity
        {
            get { return _parity; }
            set 
            { 
                _parity = value;
                if (_serialport != null)
                    _serialport.Parity = _parity;
            }
        }

        #region BUS接口实现

        public void Init()
        {
            if (_serialport == null)
            {
                _serialport = new SerialPort();
            }
            //_serialport.PortName = _PortName;
            //_serialport.BaudRate = _BaudRate;
            //_serialport.DataBits = _databits;
            //_serialport.StopBits = _stopbits;
            //_serialport.Parity = _parity;
            _serialport.ReadTimeout = SerialPort.InfiniteTimeout;
            _serialport.WriteTimeout = SerialPort.InfiniteTimeout;
            _serialport.ReadBufferSize = 1024 * 10;
            _serialport.WriteBufferSize = 1024 * 10;
        }

        public bool Write(byte[] buf)
        {
            if (_serialport != null)
            {
                _serialport.Write(buf,0,buf.Length);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Read(byte[] buf, int count, ref int bytesread)
        {
            if (_serialport != null)
            {
                bytesread = _serialport.Read(buf, 0, count);
                return true;
            }
            else
            {
                return false;
            }
        }
        [JsonIgnore]
        public bool Connected
        {
            get { return _serialport.IsOpen; }
        }

        public bool Open()
        {
            if (!_serialport.IsOpen)
            {
                _serialport.Open();
            }
            return true;
        }

        public bool Close()
        {
            _serialport.Close();
            return true;
        }
        #endregion End
    }
}

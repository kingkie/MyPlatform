using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Yu3zx.PackingMonitor
{
    public class MainManager
    {
        private static object syncObj = new object();
        private static MainManager instance = null;
        public static MainManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = new MainManager();
                }
            }
            return instance;
        }

        public SerialPort DeviceComm = null;

        public bool VideoOpen = false;

        public string[] GetPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            return ports;
        }
    }
}

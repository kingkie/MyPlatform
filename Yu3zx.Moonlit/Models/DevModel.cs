using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class DevModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string devName = string.Empty;

        private string devId = string.Empty;

        private bool linkable = false;

        private Color statusColor = Color.Red;

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DevId
        {
            get { return devId; }
            set
            {
                if (value != devId)
                {
                    devId = value;
                    OnPropertyChanged("DevId");
                }
            }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName
        {
            get { return devName; }
            set
            {
                if (value != devName)
                {
                    devName = value;
                    OnPropertyChanged("DevName");
                }
            }
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool ConnectStatus
        {
            get { return linkable; }
            set
            {
                if (value != linkable)
                {
                    linkable = value;
                    if (linkable)
                    {
                        StatusColor = Color.Green;
                    }
                    else
                    {
                        StatusColor = Color.Red;
                    }
                }
            }
        }

        public Color StatusColor
        {
            get { return statusColor; }
            set
            {
                if (value != statusColor)
                {
                    statusColor = value;
                    OnPropertyChanged("StatusColor");
                }
            }
        }

        /// <summary>
        /// 属性改变触发
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

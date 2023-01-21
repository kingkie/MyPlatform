using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Yu3zx.Devices.Interfaces;
using System.ComponentModel;

namespace Yu3zx.Devices
{
    public class Channel : INotifyPropertyChanged
    {
        private string _varnodeid;
        private object _objValue;
        /// <summary>
        /// 通道绑定变量Id
        /// </summary>
        public string VarNodeId
        {
            set { _varnodeid = value; }
            get { return _varnodeid; }
        }
        /// <summary>
        /// 通道Id
        /// </summary>
        public string ChannelId
        {
            set;
            get;
        }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName
        {
            set;
            get;
        }

        //不需要每个通道增加处理数据
        //public IProcessor DataProcessor
        //{
        //    set;
        //    get;
        //}

        /// <summary>
        /// 通道值
        /// </summary>
        [JsonIgnore]
        public object ChannelValue 
        {
            get { return _objValue; }
            set 
            { 
                _objValue = value;
                ValueChangedDeal();
            }
        }

        /// <summary>
        /// 数据改变时触发
        /// </summary>
        private void ValueChangedDeal()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性改变触发
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

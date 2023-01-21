using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Modols
{
    /// <summary>
    /// 报警模型
    /// </summary>
    public class Alarm
    {
        private string _alarmid = "";
        private string _alarmsource = "";
        private string _alarminfo = "";
        private DateTime _alarmaddtime = DateTime.Now;
        private DateTime _removetime = DateTime.MaxValue;
        private string _alarmname = "";
        private bool _iskeep = true;

        public Alarm()
        {
        }
        public Alarm(string _tmpalarmid, string _tmpalarmsource, string _tmpalarminfo)
        {
            _alarmid = _tmpalarmid;
            _alarmsource = _tmpalarmsource;
            _alarminfo = _tmpalarminfo;
        }

        /// <summary>
        /// 报警ID
        /// </summary>
        public string DevId
        {
            set { _alarmid = value; }
            get { return _alarmid;}
        }
        /// <summary>
        /// 报警名称
        /// </summary>
        public string AlarmName
        {
            set { _alarmname = value; }
            get { return _alarmname; }
        }
        /// <summary>
        /// 报警源，设备ID，系统标志
        /// </summary>
        public string AlarmSource
        {
            set { _alarmsource = value; }
            get { return _alarmsource; }
        }
        /// <summary>
        /// 报警信息
        /// </summary>
        public string AlarmMsg
        {
            set { _alarminfo = value; }
            get { return _alarminfo; }
        }
        /// <summary>
        /// 报警开始时间
        /// </summary>
        public DateTime AlarmAddTime
        {
            set { _alarmaddtime = value; }
            get { return _alarmaddtime; }
        }
        /// <summary>报警消除时间 </summary>
        public DateTime AlarmRemoveTime
        {
            get { return this._removetime; }
            set { _removetime = value; }
        }

        public bool IsKeep
        {
            set { _iskeep = value; }
            get { return _iskeep; }
        }

    }
}

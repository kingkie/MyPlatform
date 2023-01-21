using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Modols
{
    public class AlarmManager
    {
        #region 单例定义
        private static object syncObj = new object();
        private static AlarmManager instance = null;
        public static AlarmManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    instance = new AlarmManager();
                }
            }
            return instance;
        }
        #endregion End

        public List<Alarm> AlarmList;

        public AlarmManager()
        {
            AlarmList = new List<Alarm>();
        }

        /// <summary>
        /// 增加一个报警
        /// </summary>
        /// <param name="_alarm"></param>
        public void AddAlarm(Alarm _alarm)
        {
            AlarmList.Add(_alarm);
        }
        /// <summary>
        /// 增加一个报警
        /// </summary>
        /// <param name="_alarmid"></param>
        /// <param name="_alarmsource"></param>
        /// <param name="_alarminfo"></param>
        public void AddAlarm(string _alarmid, string _alarmsource, string _alarminfo)
        {
            Alarm _alarm = new Alarm();
            _alarm.AlarmAddTime = DateTime.Now;
            _alarm.DevId = _alarmid;
            _alarm.AlarmMsg = _alarminfo;
            _alarm.AlarmSource = _alarmsource;
            AlarmList.Add(_alarm);
        }
        /// <summary>
        /// 清除所有报警
        /// </summary>
        public void ClearAllAlarm()
        {
            AlarmList.Clear();
        }
        /// <summary>
        /// 清除某个报警
        /// </summary>
        public void RemoveAlarm(string _alarmsource)
        {
            foreach(Alarm tmp in AlarmList)
            {
                if(tmp.AlarmSource == _alarmsource)
                    AlarmList.Remove(tmp);
            }
        }

    }
}

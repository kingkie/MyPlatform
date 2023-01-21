using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class SysPersonInfo
    {
        private string _id;
        private string _name;
        private string _login_name;
        private string _pwd;
        private DateTime _create_time;
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        public string loginName
        {
            set { _login_name = value; }
            get { return _login_name; }
        }
        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        public DateTime createTime
        {
            set { _create_time = value; }
            get { return _create_time; }
        }
    }
}

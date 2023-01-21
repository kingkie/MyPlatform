using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class SysPlcInfo
    {
        private int _id;
        private string _name;
        private string _ip;
        private string _port;
        private string _station;
        private string _del_address;
        private string _total_address;
        private string _parts_address;
        private string _valid_address;
        private string _write_address;
        private string _info;
        private DateTime _create_time;
        private DateTime _alter_time;
        private string _op_id;
        private string _op_name;

        public int ID { set { _id = value; } get { return _id; } }
        public string Name { set { _name = value; } get { return _name; } }
        public string Ip { set { _ip = value; } get { return _ip; } }
        public string Port { set { _port = value; } get { return _port; } }
        public string Station { set { _station = value; } get { return _station; } }
        public string delAddress { set { _del_address = value; } get { return _del_address; } }
        public string totalAddress { set { _total_address = value; } get { return _total_address; } }
        public string partsAddress { set { _parts_address = value; } get { return _parts_address; } }
        public string validAddress { set { _valid_address = value; } get { return _valid_address; } }
        public string writeAddress { set { _write_address = value; } get { return _write_address; } }
        public string Info { set { _info = value; } get { return _info; } }
        public DateTime createTime { set { _create_time = value; } get { return _create_time; } }
        public DateTime alterTime { set { _alter_time = value; } get { return _alter_time; } }
        public string opId { set { _op_id = value; } get { return _op_id; } }
        public string opName { set { _op_name = value; } get { return _op_name; } }

    }
}

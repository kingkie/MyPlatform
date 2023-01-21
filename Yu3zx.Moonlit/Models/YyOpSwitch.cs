using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class YyOpSwitch
    {
        private int _id;
        private string _action_name;
        private string _action_no;
        private DateTime _create_time;
        private string _op_id;
        private string _op_name;


        public int ID { set { _id = value; } get { return _id; } }
        public string actionName { set { _action_name = value; } get { return _action_name; } }
        public string actionNo { set { _action_no = value; } get { return _action_no; } }
        public DateTime createTime { set { _create_time = value; } get { return _create_time; } }
        public string opId { set { _op_id = value; } get { return _op_id; } }
        public string opName { set { _op_name = value; } get { return _op_name; } }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class YyCollectInfo
    {
        private int _id;
        private string _total_code;
        private string _parts_code;
        private string _status;
        private DateTime _create_time;
        private DateTime _del_time;
        private string _source_type;
        private string _source_plc_ip;
        private string _op_id;
        private string _product_no;
        private string _station;

        public int ID { set { _id = value; } get { return _id; } }
        public string totalCode { set { _total_code = value; } get { return _total_code; } }
        public string partsCode { set { _parts_code = value; } get { return _parts_code; } }
        public string Status { set { _status = value; } get { return _status; } }
        public DateTime createTime { set { _create_time = value; } get { return _create_time; } }
        public DateTime delTime { set { _del_time = value; } get { return _del_time; } }
        public string sourceType { set { _source_type = value; } get { return _source_type; } }
        public string sourcePlcIp { set { _source_plc_ip = value; } get { return _source_plc_ip; } }
        public string opId { set { _op_id = value; } get { return _op_id; } }

        public string ProductNo { set { _product_no = value; } get { return _product_no; } }

        public string Station { set { _station = value; } get { return _station; } }
    }
}

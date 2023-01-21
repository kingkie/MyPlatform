using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class YyQrcodeInfo
    {
        private int _id;
        private string _total_code;
        private string _total_qr_code;
        private string _length;
        private DateTime _create_time;
        private DateTime _print_time;
        private string _type;
        private string _source_ip;
        private string _print_status;
        private string _qualified_status;
        private string _product_no;


        public int ID { set { _id = value; } get { return _id; } }
        public string totalCode { set { _total_code = value; } get { return _total_code; } }
        public string totalQrCode { set { _total_qr_code = value; } get { return _total_qr_code; } }
        public string Length { set { _length = value; } get { return _length; } }
        public DateTime createTime { set { _create_time = value; } get { return _create_time; } }
        public DateTime printTime { set { _print_time = value; } get { return _print_time; } }
        public string Type { set { _type = value; } get { return _type; } }
        public string sourceIp { set { _source_ip = value; } get { return _source_ip; } }
        public string printStatus { set { _print_status = value; } get { return _print_status; } }
        public string qualifiedStatus { set { _qualified_status = value; } get { return _qualified_status; } }
        public string ProductNo { set { _product_no = value; } get { return _product_no; } }

    }
}

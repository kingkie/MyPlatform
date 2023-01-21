using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class SysBaseInfo
    {
        private int _id;
        private string _data_ip;
        private string _data_port;
        private string _data_name;
        private string _data_user;
        private string _data_pwd;
        private string _collect_rate;
        private string _logo_path;
        private string _company_name;
        private string _workshop_name;
        private string _line_name;
        private string _sign;
        private string _qrcode_length;
        private DateTime _create_time;
        private DateTime _alter_time;
        private string _op_id;
        private string _op_name;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public string dataIp
        {
            set { _data_ip = value; }
            get { return _data_ip; }
        }
        public string dataPort
        {
            set { _data_port = value; }
            get { return _data_port; }
        }
        public string dataName
        {
            set { _data_name = value; }
            get { return _data_name; }
        }
        public string dataUser
        {
            set { _data_user = value; }
            get { return _data_user; }
        }
        public string dataPwd
        {
            set { _data_pwd = value; }
            get { return _data_pwd; }
        }
        public string collectRate
        {
            set { _collect_rate = value; }
            get { return _collect_rate; }
        }
        public string logoPath
        {
            set { _logo_path = value; }
            get { return _logo_path; }
        }
        public string companyName
        {
            set { _company_name = value; }
            get { return _company_name; }
        }
        public string workshopName
        {
            set { _workshop_name = value; }
            get { return _workshop_name; }
        }

        public string lineName
        {
            set { _line_name = value; }
            get { return _line_name; }
        }

        public string Sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        public string qrcodeLength
        {
            set { _qrcode_length = value; }
            get { return _qrcode_length; }
        }
        public string opId
        {
            set { _op_id = value; }
            get { return _op_id; }
        }
        public string opName
        {
            set { _op_name = value; }
            get { return _op_name; }
        }
        public DateTime createTime
        {
            set { _create_time = value; }
            get { return _create_time; }
        }
        public DateTime alterTime
        {
            set { _alter_time = value; }
            get { return _alter_time; }
        }
    }
}

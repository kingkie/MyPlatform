using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class SysProductInfo
    {
        //id,name,no,code,info,create_time,alter_time,op_id,op_name

        private int _id;
        private string _name;
        private string _no;
        private string _code;
        private string _code2;
        private string _code3;
        private string _info;
        private DateTime _create_time;
        private DateTime _alter_time;
        private string _op_id;
        private string _print_name;
        private string _op_name;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public string No
        {
            set { _no = value; }
            get { return _no; }
        }
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        public string Code2
        {
            set { _code2 = value; }
            get { return _code2; }
        }
        public string Code3
        {
            set { _code3 = value; }
            get { return _code3; }
        }
        public string Info
        {
            set { _info = value; }
            get { return _info; }
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
        public string printName
        {
            set { _print_name = value; }
            get { return _print_name; }
        }

    }
}

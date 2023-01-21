using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class SysProductParts
    {
        private int _id;
        private string _name;
        private string _serial;//序号
        private string _stage;//工位
        private string _no1;
        private string _no2;
        private string _no3;
        private string _no4;
        private string _no5;
        private int _product_id;
        private int _plc_id;

        public int ID { set { _id = value; } get { return _id; } }
        public string Name { set { _name = value; } get { return _name; } }
        public string No1 { set { _no1 = value; } get { return _no1; } }
        public string No2 { set { _no2 = value; } get { return _no2; } }
        public string No3 { set { _no3 = value; } get { return _no3; } }
        public string No4 { set { _no4 = value; } get { return _no4; } }
        public string No5 { set { _no5 = value; } get { return _no5; } }
        public int productId { set { _product_id = value; } get { return _product_id; } }
        public int plcId { set { _plc_id = value; } get { return _plc_id; } }
        public string serial { set { _serial = value; } get { return _serial; } }
        public string stage { set { _stage = value; } get { return _stage; } }

    }
}

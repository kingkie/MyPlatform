using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.Models
{
    public class LicenseModel
    {
        private string _license;
        private string _company;
        private string _create;
        private string _time;
        private string _valid;
        private string _type;
        public string License { set { _license = value; } get { return _license; } }
        public string Company { set { _company = value; } get { return _company; } }
        public string Create { set { _create = value; } get { return _create; } }
        public string Time { set { _time = value; } get { return _time; } }
        public string Valid { set { _valid = value; } get { return _valid; } }
        public string Type { set { _type = value; } get { return _type; } }

    }
}

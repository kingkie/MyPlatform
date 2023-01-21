using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.bean
{
    public class UserUtil
    {
        
        private static string op_id;
        private static string op_name;
        private static DateTime login_time;
        public static string OpId {
            set { op_id = value; }
            get { return op_id; }
        }
        public static string OpName
        {
            set { op_name = value; }
            get { return op_name; }
        }
        public static DateTime LoginTime
        {
            set { login_time = value; }
            get { return login_time; }
        }
        public static void setUser(string opId,string opName) {
            op_id = opId;
            op_name= opName;
            login_time = DateTime.Now;
        }
    }
}

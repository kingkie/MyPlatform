using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.bean
{
    class ModelsUtil
    {
        public static string toString(Object str)
        {
            if (str==null || str=="") {
                return null;
            }
            return str.ToString();
        }
        }
}

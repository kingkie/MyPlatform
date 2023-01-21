using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.Moonlit.bean
{
   public class PageBean<T>
    {

        private int _page;
        private int _row_num;
        private int _total;
        private int _total_page;
        private List<T> _list;
        public int Page {
            set { _page = value; }
            get { return _page; }
        }
        public int RowNum
        {
            set { _row_num = value; }
            get { return _row_num; }
        }

        public int Total
        {
            set { _total = value; }
            get { return _total; }
        }

        public int TotalPage
        {
            set { _total_page = value; }
            get { return _total_page; }
        }

        public List<T> List
        {
            set { _list = value; }
            get { return _list; }
        }
    }
}

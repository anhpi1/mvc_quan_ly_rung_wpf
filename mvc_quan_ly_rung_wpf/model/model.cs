using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc_quan_ly_rung_wpf.model
{
    public class tinh<V>
    {
        public int code { get; set; }
        public string name { get; set; }
        public int muc_do_hanh_chinh_id { get; set; }
        public int co_so_quan_ly_code { get; set; }
        public V data { get; set; }

        public tinh()
        {
            data = (V)Activator.CreateInstance(typeof(V));
        }
    }

    public class huyen<V>
    {
        public int code { get; set; }
        public string name { get; set; }
        public int muc_do_hanh_chinh_id { get; set; }
        public int co_so_quan_ly_code { get; set; }
        public V data { get; set; }

        public huyen()
        {
            data = (V)Activator.CreateInstance(typeof(V));
        }
    }

    public class xa
    {
        public int code { get; set; }
        public string name { get; set; }
        public int muc_do_hanh_chinh_id { get; set; }
        public int co_so_quan_ly_code { get; set; }
    }

}

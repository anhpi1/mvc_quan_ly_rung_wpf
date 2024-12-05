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
        public string muc_do_hanh_chinh_name { get; set; }
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
        public string muc_do_hanh_chinh_name { get; set; }
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
        public string muc_do_hanh_chinh_name { get; set; }
        public int co_so_quan_ly_code { get; set; }
    }
    public class nameofid
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class thong_tin_user
    {
        public string user_name { get; set; }
        public string mat_khau { get; set; }
        public string phone { get; set; }
        public string ho_va_ten { get; set; }
    }
    public class lanh_dao<V>
    {
        public string user_name { get; set; }
        public bool is_hoat_dong { get; set; }
        public string nguoi_quan_ly_user_name { get; set; }
        public int vai_tro_nguoi_dung_id { get; set; }
        public string vai_tro_nguoi_dung_name { get; set; }
        public int co_so_quan_ly_code { get; set; }
        public V data { get; set; }

        public lanh_dao()
        {
            data = (V)Activator.CreateInstance(typeof(V));
        }

    }

    public class quan_ly<V>
    {
        public string user_name { get; set; }
        public bool is_hoat_dong { get; set; }
        public string nguoi_quan_ly_user_name { get; set; }
        public int vai_tro_nguoi_dung_id { get; set; }
        public string vai_tro_nguoi_dung_name { get; set; }
        public int co_so_quan_ly_code { get; set; }
        public V data { get; set; }

        public quan_ly()
        {
            data = (V)Activator.CreateInstance(typeof(V));
        }
    }
    public class nhan_vien
    {
        public string user_name { get; set; }
        public bool is_hoat_dong { get; set; }
        public string nguoi_quan_ly_user_name { get; set; }
        public int vai_tro_nguoi_dung_id { get; set; }
        public string vai_tro_nguoi_dung_name { get; set; }
        public int co_so_quan_ly_code { get; set; }
        
    }
}

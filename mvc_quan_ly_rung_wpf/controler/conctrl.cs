using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
using mvc_quan_ly_rung_wpf.model;
namespace mvc_quan_ly_rung_wpf.controler
{
    public class Program
    {
        public static void phan_quyen_cho_nguoi_dung<T>(T user, string user_name, string nguoi_quan_ly, int vai_tro_nguoi_dung_id, int co_so_quan_ly_code)
        {
            lanh_dao<List<quan_ly<List<nhan_vien>>>> foundNhanVienLanhDao = null;
            quan_ly<List<nhan_vien>> foundNhanVienQuanLy = null;
            nhan_vien foundNhanVienNhanVien = null;
            var temp = TimKiemTheoUserName(user, user_name);
            if (temp is lanh_dao<List<quan_ly<List<nhan_vien>>>> lanhDao)
            {
                foundNhanVienLanhDao = temp as lanh_dao<List<quan_ly<List<nhan_vien>>>>;
                if (foundNhanVienLanhDao != null && foundNhanVienLanhDao.is_hoat_dong)
                {
                    MessageBox.Show("Tài khoản đang hoạt động không thể sửa");
                    return;
                }

                chinh_sua_sql.chinh_sua_mot_user(
                    foundNhanVienLanhDao.user_name,
                    false,
                    nguoi_quan_ly ?? foundNhanVienLanhDao.nguoi_quan_ly_user_name,
                    vai_tro_nguoi_dung_id != 0 ? vai_tro_nguoi_dung_id : foundNhanVienLanhDao.vai_tro_nguoi_dung_id,
                    co_so_quan_ly_code != 0 ? co_so_quan_ly_code : foundNhanVienLanhDao.co_so_quan_ly_code
                );
            }
            else if (temp is quan_ly<List<nhan_vien>> quanLy)
            {
                foundNhanVienQuanLy = temp as quan_ly<List<nhan_vien>>;
                if (foundNhanVienQuanLy != null && foundNhanVienQuanLy.is_hoat_dong)
                {
                    MessageBox.Show("Tài khoản đang hoạt động không thể sửa");
                    return;
                }

                chinh_sua_sql.chinh_sua_mot_user(
                    foundNhanVienQuanLy.user_name,
                    false,
                    nguoi_quan_ly ?? foundNhanVienQuanLy.nguoi_quan_ly_user_name,
                    vai_tro_nguoi_dung_id != 0 ? vai_tro_nguoi_dung_id : foundNhanVienQuanLy.vai_tro_nguoi_dung_id,
                    co_so_quan_ly_code != 0 ? co_so_quan_ly_code : foundNhanVienQuanLy.co_so_quan_ly_code
                );
            }
            else if (temp is nhan_vien nhanVien)
            {
                foundNhanVienNhanVien = temp as nhan_vien;
                if (foundNhanVienNhanVien != null && foundNhanVienNhanVien.is_hoat_dong)
                {
                    MessageBox.Show("Tài khoản đang hoạt động không thể sửa");
                    return;
                }

                chinh_sua_sql.chinh_sua_mot_user(
                    foundNhanVienNhanVien.user_name,
                    false,
                    nguoi_quan_ly ?? foundNhanVienNhanVien.nguoi_quan_ly_user_name,
                    vai_tro_nguoi_dung_id != 0 ? vai_tro_nguoi_dung_id : foundNhanVienNhanVien.vai_tro_nguoi_dung_id,
                    co_so_quan_ly_code != 0 ? co_so_quan_ly_code : foundNhanVienNhanVien.co_so_quan_ly_code
                );
            }
            else
            {
                MessageBox.Show("Không có quyền quản lý");
            }
        }


        public static object TimKiemTheoUserName(object obj, string userName)
        {
            if (obj == null)
                return null;

            // Nếu đối tượng có thuộc tính `user_name`, kiểm tra giá trị
            var userNameProp = obj.GetType().GetProperty("user_name");
            if (userNameProp != null && userNameProp.GetValue(obj)?.ToString() == userName)
            {
                return obj;
            }

            // Nếu đối tượng là danh sách, duyệt qua từng phần tử
            if (obj is IEnumerable<object> list)
            {
                foreach (var item in list)
                {
                    var found = TimKiemTheoUserName(item, userName);
                    if (found != null)
                        return found;
                }
            }

            // Nếu đối tượng có thuộc tính `data`, tiếp tục tìm kiếm trong `data`
            var dataProp = obj.GetType().GetProperty("data");
            if (dataProp != null)
            {
                var dataValue = dataProp.GetValue(obj);
                return TimKiemTheoUserName(dataValue, userName);
            }

            return null;
        }
    }
}

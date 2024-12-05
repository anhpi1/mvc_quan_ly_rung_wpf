using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using mvc_quan_ly_rung_wpf.controler;
using mvc_quan_ly_rung_wpf.model;

namespace mvc_quan_ly_rung_wpf.view
{
    /// <summary>
    /// Interaction logic for chinh_sua_user.xaml
    /// </summary>
    public partial class chinh_sua_user : UserControl
    {

        public lanh_dao<List<quan_ly<List<nhan_vien>>>> _lanhdao = null;
        public quan_ly<List<nhan_vien>> _quanly = null;
        public nhan_vien _nhanvien = null;

        public chinh_sua_user(string _user_name, bool _is_hoat_dong, string _nguoi_quan_ly_user_name, int _vai_tro_nguoi_dung_id, int _co_so_quan_ly_code, lanh_dao<List<quan_ly<List<nhan_vien>>>> lanhdao, quan_ly<List<nhan_vien>> quanly, nhan_vien nhanvien)
        {
            InitializeComponent();

            // Gán giá trị từ tham số vào TextBox
            user_name.Text = _user_name;
            is_hoat_dong.Text = _is_hoat_dong.ToString();
            nguoi_quan_ly_user_name.Text = _nguoi_quan_ly_user_name;
            vai_tro_nguoi_dung_id.Text = _vai_tro_nguoi_dung_id.ToString();
            co_so_quan_ly_code.Text = _co_so_quan_ly_code.ToString();

            // Gán các tham số cho trường lớp
            _lanhdao = lanhdao;
            _quanly = quanly;
            _nhanvien = nhanvien;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra và chuyển đổi giá trị
            if (int.TryParse(vai_tro_nguoi_dung_id.Text, out int vai_tro_id) &&
                int.TryParse(co_so_quan_ly_code.Text, out int co_so_code))
            {
                // Nếu `_lanhdao` tồn tại, gọi hàm `phan_quyen_cho_nguoi_dung`
                if (_lanhdao != null)
                {
                    Program.phan_quyen_cho_nguoi_dung<lanh_dao<List<quan_ly<List<nhan_vien>>>>>(
                        _lanhdao,
                        user_name.Text,
                        nguoi_quan_ly_user_name.Text,
                        vai_tro_id,
                        co_so_code
                    );
                }
                else if (_quanly != null)
                {
                    Program.phan_quyen_cho_nguoi_dung<quan_ly<List<nhan_vien>>>(
                        _quanly,
                        user_name.Text,
                        nguoi_quan_ly_user_name.Text,
                        vai_tro_id,
                        co_so_code
                    );
                }
                else if (_nhanvien != null)
                {
                    Program.phan_quyen_cho_nguoi_dung<nhan_vien>(
                        _nhanvien,
                        user_name.Text,
                        nguoi_quan_ly_user_name.Text,
                        vai_tro_id,
                        co_so_code
                    );
                }
                else
                {
                    MessageBox.Show("người dùng không tồn tại!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho vai trò hoặc cơ sở quản lý.");
            }
        }
    }
}

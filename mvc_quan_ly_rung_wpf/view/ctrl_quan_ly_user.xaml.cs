using System;
using System.Collections.Generic;
using System.Linq;
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
using mvc_quan_ly_rung_wpf.model;
using mvc_quan_ly_rung_wpf.controler;
using System.Reflection.Emit;

namespace mvc_quan_ly_rung_wpf.view
{
    /// <summary>
    /// Interaction logic for ctrl_quan_ly_nhan_vien.xaml
    /// </summary>
    public partial class ctrl_quan_ly_user : UserControl
    {


        public ctrl_quan_ly_user()
        {
            InitializeComponent();
            show_danh_sach_quan_ly_user showme = new show_danh_sach_quan_ly_user();
            if (showme.danh_sach_quan_ly == null || showme.danh_sach_quan_ly.Count == 0)
            {
                MessageBox.Show("Danh sách quản lý trống.");
            }

            TreeViewData.ItemsSource = showme.danh_sach_quan_ly;


        }
        private void TreeViewData_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Lấy đối tượng được chọn trong TreeView
            var selectedItem = e.NewValue;

            if (selectedItem != null)
            {
                // Kiểm tra kiểu dữ liệu của đối tượng
                if (selectedItem is lanh_dao<List<quan_ly<List<nhan_vien>>>> lanhDao)
                {
                    
                    var myctrl = new chinh_sua_user(lanhDao.user_name, lanhDao.is_hoat_dong,lanhDao.nguoi_quan_ly_user_name, lanhDao.vai_tro_nguoi_dung_id, lanhDao.co_so_quan_ly_code, lanhDao,null,null);

                    
                    noi_dung.Content=myctrl;
                    

                }
                else if (selectedItem is quan_ly<List<nhan_vien>> quanLy)
                {


                    var myctrl = new chinh_sua_user(quanLy.user_name, quanLy.is_hoat_dong, quanLy.nguoi_quan_ly_user_name, quanLy.vai_tro_nguoi_dung_id, quanLy.co_so_quan_ly_code,null,quanLy,null);

                    noi_dung.Content = myctrl;
                }
                else if (selectedItem is nhan_vien nhanVien)
                {
                    var myctrl = new chinh_sua_user(nhanVien.user_name, nhanVien.is_hoat_dong, nhanVien.nguoi_quan_ly_user_name, nhanVien.vai_tro_nguoi_dung_id, nhanVien.co_so_quan_ly_code,null,null,nhanVien);

                    noi_dung.Content = myctrl;
                }
            }

        }
    }
}

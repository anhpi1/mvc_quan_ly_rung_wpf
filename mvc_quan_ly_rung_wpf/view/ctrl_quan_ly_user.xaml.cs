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
                    MessageBox.Show ($"Lãnh Đạo: {lanhDao.user_name}");
                }
                else if (selectedItem is quan_ly<List<nhan_vien>> quanLy)
                {
                    MessageBox.Show ( $"Quản Lý: {quanLy.user_name}");
                }
                else if (selectedItem is nhan_vien nhanVien)
                {
                    MessageBox.Show($"Nhân Viên: {nhanVien.user_name}");
                }
            }

        }
    }
}

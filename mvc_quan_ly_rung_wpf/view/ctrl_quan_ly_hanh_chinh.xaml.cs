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
    /// Interaction logic for ctrl_quan_ly_hanh_chinh.xaml
    /// </summary>
    public partial class ctrl_quan_ly_hanh_chinh : UserControl
    {
        public ctrl_quan_ly_hanh_chinh()
        {
            InitializeComponent();
            show_danh_sach_quan_ly_hanh_chinh showme = new show_danh_sach_quan_ly_hanh_chinh();
            TreeViewData.ItemsSource = showme.danh_sach_quan_ly;
            


        }
        private void TreeViewData_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Lấy đối tượng được chọn trong TreeView
            var selectedItem = e.NewValue;

            if (selectedItem != null)
            {
                // Kiểm tra kiểu dữ liệu của đối tượng
                if (selectedItem is tinh<List<huyen<List<xa>>>> tinh)
                {
                    MessageBox.Show($"Lãnh Đạo: {tinh.name}");
                }
                else if (selectedItem is huyen<List<xa>> huyen)
                {
                    MessageBox.Show($"Quản Lý: {huyen.name}");
                }
                else if (selectedItem is xa xa)
                {
                    MessageBox.Show($"Nhân Viên: {xa.name}");
                }
            }

        }



    }
}

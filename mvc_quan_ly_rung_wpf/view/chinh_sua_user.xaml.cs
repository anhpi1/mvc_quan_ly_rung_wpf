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

namespace mvc_quan_ly_rung_wpf.view
{
    /// <summary>
    /// Interaction logic for chinh_sua_user.xaml
    /// </summary>
    public partial class chinh_sua_user : UserControl
    {
        public string _user_name { get; set; }
        public bool _is_hoat_dong { get; set; }
        public string _nguoi_quan_ly_user_name { get; set; }
        public int _vai_tro_nguoi_dung_id { get; set; }
        public int _co_so_quan_ly_code { get; set; }

        
        public chinh_sua_user()
        {
            InitializeComponent();
            user_name.Text = _user_name; // Gán giá trị string vào TextBox
            is_hoat_dong.Text = _is_hoat_dong.ToString(); // Gán giá trị bool dưới dạng string vào TextBox
            nguoi_quan_ly_user_name.Text = _nguoi_quan_ly_user_name; // Gán giá trị string vào TextBox
            vai_tro_nguoi_dung_id.Text = _vai_tro_nguoi_dung_id.ToString(); // Gán giá trị int dưới dạng string vào TextBox
            co_so_quan_ly_code.Text = _co_so_quan_ly_code.ToString(); // Gán giá trị int dưới dạng string vào TextBox

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

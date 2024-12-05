using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using mvc_quan_ly_rung_wpf.view;

namespace mvc_quan_ly_rung_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myctrl.Content = new ctrl_quan_ly_user();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myctrl.Content = new ctrl_quan_ly_hanh_chinh();
        }
    }
}
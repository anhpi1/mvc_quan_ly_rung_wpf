﻿using System;
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
    /// Interaction logic for myctrl.xaml
    /// </summary>
    public partial class myctrl : UserControl
    {
        public myctrl()
        {
            InitializeComponent();
            

            show_danh_sach_quan_ly showme = new show_danh_sach_quan_ly();
            if (showme.danh_sach_quan_ly == null || showme.danh_sach_quan_ly.Count == 0)
            {
                MessageBox.Show("Danh sách quản lý trống.");
            }
            else
            {
                MessageBox.Show("Dữ liệu đã được load thành công.");
            }
            lss.ItemsSource = showme.danh_sach_quan_ly;
        }

        private void click1(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp =sender as StackPanel;
            if (sp != null)
            {
                ListBox tbx = sp.FindName("tb11") as ListBox;
                if (tbx != null)
                {
                    tbx.Visibility = tbx.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }
        private void click2(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            if (sp != null)
            {
                ListBox tbx = sp.FindName("tb22") as ListBox;
                if (tbx != null)
                {
                    tbx.Visibility = tbx.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }
    }
}

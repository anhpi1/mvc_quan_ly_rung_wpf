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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.mysql mysq = new model.mysql();
            mysq.IsConnectMySqlSuccess();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
namespace mvc_quan_ly_rung_wpf.model
{
    internal class mysql
    {
        public bool IsConnectMySqlSuccess()
        {
            // Lấy chuỗi kết nối từ cấu hình
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); // Mở kết nối
                    MessageBox.Show ("Connection successful!");
                    return true; // Kết nối thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
                return false; // Kết nối thất bại
            }
        }

    }
}

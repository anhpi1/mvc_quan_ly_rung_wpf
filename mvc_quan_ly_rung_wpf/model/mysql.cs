using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Windows.Themes;
using mvc_quan_ly_rung_wpf.model;
using mvc_quan_ly_rung_wpf.view;
using MySql.Data.MySqlClient;
using mvc_quan_ly_rung_wpf.controler;
using static mvc_quan_ly_rung_wpf.controler.Program;
public class show_danh_sach_quan_ly_hanh_chinh
{
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    public List<tinh<List<huyen<List<xa>>>>> danh_sach_quan_ly { get; set; }
    public List<nameofid> _nameofid { get; set; }

    public show_danh_sach_quan_ly_hanh_chinh()
    {
        _nameofid = new List<nameofid>();
        danh_sach_quan_ly = new List<tinh<List<huyen<List<xa>>>>>();


        string query = "SELECT * FROM muc_do_hanh_chinh;";

        // Kết nối tới MySQL và thực thi truy vấn
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                // Mở kết nối
                connection.Open();

                // Tạo MySqlCommand
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Thực thi và đọc dữ liệu
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Lặp qua các dòng dữ liệu
                        while (reader.Read())
                        {
                            // Đọc từng cột (thay đổi theo cấu trúc bảng)
                            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                            string name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");
                            _nameofid.Add(new nameofid { id = id, name = name });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Lấy danh sách dữ liệu từ cơ sở dữ liệu
                var tinhList = GetDataHanhChinh(conn, "SELECT * FROM co_so_hanh_chinh WHERE muc_do_hanh_chinh_id = 1;") ?? new List<dynamic>();
                var huyenList = GetDataHanhChinh(conn, "SELECT * FROM co_so_hanh_chinh WHERE muc_do_hanh_chinh_id = 2;") ?? new List<dynamic>();
                var xaList = GetDataHanhChinh(conn, "SELECT * FROM co_so_hanh_chinh WHERE muc_do_hanh_chinh_id = 3;") ?? new List<dynamic>();

                if (tinhList == null || tinhList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu cho cấp tỉnh.");
                    return; // Kết thúc nếu không có dữ liệu cấp tỉnh
                }

                foreach (var tinhRow in tinhList)
                {
                    var _tinh = new tinh<List<huyen<List<xa>>>>()
                    {
                        code = tinhRow.code,
                        name = tinhRow.name,
                        muc_do_hanh_chinh_id = tinhRow.muc_do_hanh_chinh_id,
                        muc_do_hanh_chinh_name = GetNameHanhChinh(_nameofid, tinhRow.muc_do_hanh_chinh_id),
                        co_so_quan_ly_code = tinhRow.co_so_quan_ly_code,

                        data = new List<huyen<List<xa>>>()
                    };


                    // Lọc huyện theo tỉnh
                    var huyenForTinh = huyenList.Where(h => h.co_so_quan_ly_code == _tinh.code).ToList();

                    if (huyenForTinh == null || huyenForTinh.Count == 0)
                    {
                        MessageBox.Show($"Tỉnh {_tinh.name} không có huyện nào.");
                        continue; // Bỏ qua tỉnh này nếu không có huyện
                    }

                    foreach (var huyenRow in huyenForTinh)
                    {
                        var _huyen = new huyen<List<xa>>()
                        {
                            code = huyenRow.code,
                            name = huyenRow.name,
                            muc_do_hanh_chinh_id = huyenRow.muc_do_hanh_chinh_id,
                            muc_do_hanh_chinh_name = GetNameHanhChinh(_nameofid, huyenRow.muc_do_hanh_chinh_id),
                            co_so_quan_ly_code = huyenRow.co_so_quan_ly_code,
                            data = new List<xa>()
                        };

                        // Lọc xã theo huyện
                        var xaForHuyen = xaList.Where(x => x.co_so_quan_ly_code == _huyen.code).ToList();

                        if (xaForHuyen == null || xaForHuyen.Count == 0)
                        {
                            MessageBox.Show($"Huyện {_huyen.name} không có xã nào.");
                            continue; // Bỏ qua huyện này nếu không có xã
                        }

                        foreach (var xaRow in xaForHuyen)
                        {
                            var _xa = new xa()
                            {
                                code = xaRow.code,
                                name = xaRow.name,
                                muc_do_hanh_chinh_id = xaRow.muc_do_hanh_chinh_id,
                                muc_do_hanh_chinh_name = GetNameHanhChinh(_nameofid, xaRow.muc_do_hanh_chinh_id),
                                co_so_quan_ly_code = xaRow.co_so_quan_ly_code
                            };
                            _huyen.data.Add(_xa);
                        }

                        _tinh.data.Add(_huyen);
                    }

                    danh_sach_quan_ly.Add(_tinh);
                }
            }
            
        }
        catch (Exception ex)
        {
            string errorDetails = $"Lỗi: {ex.Message}\nStackTrace: {ex.StackTrace}";
            var errorWindow = new ErrorWindow(errorDetails);
            errorWindow.ShowDialog(); // Hiển thị cửa sổ lỗi
        }
        


    }

    static string GetNameHanhChinh(List<nameofid> list, int _id)
    {
        // Tìm phần tử có id bằng _id
        nameofid result = list.FirstOrDefault(item => item.id == _id);

        // Kiểm tra nếu không tìm thấy
        if (result == null)
        {
            return "Không tìm thấy";
        }

        // Trả về thuộc tính name của phần tử tìm được
        return result.name;
    }
    public static void PrintNames(List<nameofid> list)
    {
        // Kiểm tra nếu danh sách null hoặc trống
        if (list == null || !list.Any())
        {
            MessageBox.Show("Danh sách trống hoặc chưa khởi tạo.");
            return;
        }

        // Lặp qua danh sách và in tên
        foreach (var item in list)
        {
            MessageBox.Show(item.name);
        }
    }



    private List<dynamic> GetDataHanhChinh(MySqlConnection conn, string query)
    {
        var result = new List<dynamic>();

        using (MySqlCommand cmd = new MySqlCommand(query, conn))
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                result.Add(new
                {
                    code = reader.IsDBNull(reader.GetOrdinal("code")) ? 0 : reader.GetInt32("code"), // Nếu NULL thì gán giá trị mặc định
                    name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name"),
                    muc_do_hanh_chinh_id = reader.IsDBNull(reader.GetOrdinal("muc_do_hanh_chinh_id")) ? 0 : reader.GetInt32("muc_do_hanh_chinh_id"),
                    co_so_quan_ly_code = reader.IsDBNull(reader.GetOrdinal("co_so_quan_ly_code")) ? 0 : reader.GetInt32("co_so_quan_ly_code")

                });
            }
        }

        return result;
    }
}
public class show_danh_sach_quan_ly_user
{
    public List<lanh_dao<List<quan_ly<List<nhan_vien>>>>> danh_sach_quan_ly { get; set; }
    public List<nameofid> _nameofid { get; set; }
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    public show_danh_sach_quan_ly_user()
    {
        _nameofid = new List<nameofid>();
        danh_sach_quan_ly = new List<lanh_dao<List<quan_ly<List<nhan_vien>>>>>();


        string query = "SELECT * FROM vai_tro_nguoi_dung;";

        // Kết nối tới MySQL và thực thi truy vấn
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                // Mở kết nối
                connection.Open();

                // Tạo MySqlCommand
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Thực thi và đọc dữ liệu
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Lặp qua các dòng dữ liệu
                        while (reader.Read())
                        {
                            // Đọc từng cột (thay đổi theo cấu trúc bảng)
                            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                            string name = reader.IsDBNull(reader.GetOrdinal("vai_tro")) ? string.Empty : reader.GetString("vai_tro");
                            _nameofid.Add(new nameofid { id = id, name = name });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Lấy danh sách dữ liệu từ cơ sở dữ liệu
                var lanhdaoList = GetDataUser(conn, "SELECT * FROM user WHERE vai_tro_nguoi_dung_id = 1;") ?? new List<dynamic>();
                var quanlyList = GetDataUser(conn, "SELECT * FROM user WHERE vai_tro_nguoi_dung_id = 2;") ?? new List<dynamic>();
                var nhanvienList = GetDataUser(conn, "SELECT * FROM user WHERE vai_tro_nguoi_dung_id = 3;") ?? new List<dynamic>();

                if (lanhdaoList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu cho cấp tỉnh.");
                    return; // Kết thúc nếu không có dữ liệu cấp tỉnh
                }

                foreach (var lanhdaoRow in lanhdaoList)
                {
                    var _lanhdao = new lanh_dao<List<quan_ly<List<nhan_vien>>>>()
                    {
                        user_name = lanhdaoRow.user_name,
                        is_hoat_dong = lanhdaoRow.is_hoat_dong,
                        nguoi_quan_ly_user_name = lanhdaoRow.nguoi_quan_ly_user_name,
                        vai_tro_nguoi_dung_id = lanhdaoRow.vai_tro_nguoi_dung_id,
                        vai_tro_nguoi_dung_name = GetNameuser(_nameofid, lanhdaoRow.vai_tro_nguoi_dung_id),
                        co_so_quan_ly_code = lanhdaoRow.co_so_quan_ly_code, // Chỉnh sửa tại đây

                        data = new List<quan_ly<List<nhan_vien>>>()
                    };

                    // Lọc quản lý theo lãnh đạo
                    var quanlyforlanhdao = quanlyList.Where(h => h.nguoi_quan_ly_user_name == _lanhdao.user_name).ToList();

                    if (quanlyforlanhdao.Count == 0)
                    {
                        MessageBox.Show($"Lãnh đạo {_lanhdao.user_name} không có quản lý nào.");
                        continue; // Bỏ qua nếu không có quản lý
                    }

                    foreach (var quanlyRow in quanlyforlanhdao)
                    {
                        var _quanly = new quan_ly<List<nhan_vien>>()
                        {
                            user_name = quanlyRow.user_name, // Cập nhật đúng dữ liệu
                            is_hoat_dong = quanlyRow.is_hoat_dong, // Cập nhật đúng dữ liệu
                            nguoi_quan_ly_user_name = quanlyRow.nguoi_quan_ly_user_name, // Cập nhật đúng dữ liệu
                            vai_tro_nguoi_dung_id = quanlyRow.vai_tro_nguoi_dung_id, // Cập nhật đúng dữ liệu
                            vai_tro_nguoi_dung_name = GetNameuser(_nameofid, quanlyRow.vai_tro_nguoi_dung_id),
                            co_so_quan_ly_code = quanlyRow.co_so_quan_ly_code, // Chỉnh sửa tại đây

                            data = new List<nhan_vien>()
                        };

                        // Lọc nhân viên theo quản lý
                        var nhanvienforquanly = nhanvienList.Where(x => x.nguoi_quan_ly_user_name == _quanly.user_name).ToList();

                        if (nhanvienforquanly.Count == 0)
                        {
                            MessageBox.Show($"Quản lý {_quanly.user_name} không có nhân viên nào.");
                            continue; // Bỏ qua nếu không có nhân viên
                        }

                        foreach (var nhanvienRow in nhanvienforquanly)
                        {
                            var _nhanvien = new nhan_vien()
                            {
                                user_name = nhanvienRow.user_name, // Cập nhật đúng dữ liệu
                                is_hoat_dong = nhanvienRow.is_hoat_dong, // Cập nhật đúng dữ liệu
                                nguoi_quan_ly_user_name = nhanvienRow.nguoi_quan_ly_user_name, // Cập nhật đúng dữ liệu
                                vai_tro_nguoi_dung_id = nhanvienRow.vai_tro_nguoi_dung_id, // Cập nhật đúng dữ liệu
                                vai_tro_nguoi_dung_name = GetNameuser(_nameofid, nhanvienRow.vai_tro_nguoi_dung_id),
                                co_so_quan_ly_code = nhanvienRow.co_so_quan_ly_code // Cập nhật đúng dữ liệu
                            };
                            _quanly.data.Add(_nhanvien);
                        }

                        _lanhdao.data.Add(_quanly);
                    }

                    danh_sach_quan_ly.Add(_lanhdao); // Đảm bảo danh sách đã được khởi tạo
                }
            }
            
        }
        catch (Exception ex)
        {
            string errorDetails = $"Lỗi: {ex.Message}\nStackTrace: {ex.StackTrace}";
            var errorWindow = new ErrorWindow(errorDetails);
            errorWindow.ShowDialog(); // Hiển thị cửa sổ lỗi
        }

        var quanLy = danh_sach_quan_ly.FirstOrDefault();

        }
        static string GetNameuser(List<nameofid> list, int _id)
    {
        // Tìm phần tử có id bằng _id
        nameofid result = list.FirstOrDefault(item => item.id == _id);

        // Kiểm tra nếu không tìm thấy
        if (result == null)
        {
            return "Không tìm thấy";
        }

        // Trả về thuộc tính name của phần tử tìm được
        return result.name;
    }
    public static void PrintNames(List<nameofid> list)
    {
        // Kiểm tra nếu danh sách null hoặc trống
        if (list == null || !list.Any())
        {
            MessageBox.Show("Danh sách trống hoặc chưa khởi tạo.");
            return;
        }

        // Lặp qua danh sách và in tên
        foreach (var item in list)
        {
            MessageBox.Show(item.name);
        }
    }



    private List<dynamic> GetDataUser(MySqlConnection conn, string query)
    {
        var result = new List<dynamic>();

        using (MySqlCommand cmd = new MySqlCommand(query, conn))
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                result.Add(new
                {
                    user_name = reader.IsDBNull(reader.GetOrdinal("user_name")) ? string.Empty : reader.GetString("user_name"), // Nếu NULL thì gán giá trị mặc định
                    is_hoat_dong = reader.IsDBNull(reader.GetOrdinal("is_hoat_dong")) ? false : reader.GetBoolean("is_hoat_dong"), // Nếu NULL thì gán giá trị mặc định là false
                    nguoi_quan_ly_user_name = reader.IsDBNull(reader.GetOrdinal("nguoi_quan_ly_user_name")) ? string.Empty : reader.GetString("nguoi_quan_ly_user_name"),
                    vai_tro_nguoi_dung_id = reader.IsDBNull(reader.GetOrdinal("vai_tro_nguoi_dung_id")) ? 0 : reader.GetInt32("vai_tro_nguoi_dung_id"),
                    co_so_quan_ly_code = reader.IsDBNull(reader.GetOrdinal("co_so_quan_ly_code")) ? 0 : reader.GetInt32("co_so_quan_ly_code")
                });
            }
        }

        return result;
    }

}
public class chinh_sua_sql
{
    private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

    public static void chinh_sua_mot_co_so_hanh_chinh(int id, string name, int muc_do_hanh_chinh_id, int co_so_quan_ly_code)
    {
        try
        {
            // Kết nối đến cơ sở dữ liệu
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Mở kết nối

                // Câu lệnh SQL để cập nhật nhiều trường theo ID
                string query = "UPDATE co_so_hanh_chinh SET name = @name, muc_do_hanh_chinh_id = @muc_do_hanh_chinh_id, co_so_quan_ly_code = @co_so_quan_ly_code WHERE id = @id";

                // Sử dụng MySqlCommand để thực thi câu lệnh SQL
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@muc_do_hanh_chinh_id", muc_do_hanh_chinh_id);
                    cmd.Parameters.AddWithValue("@co_so_quan_ly_code", co_so_quan_ly_code);
                    cmd.Parameters.AddWithValue("@id", id);

                    // Thực thi câu lệnh UPDATE
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi với ID này.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ
            MessageBox.Show($"Lỗi: {ex.Message}");
        }
    }
    public static void chinh_sua_mot_user(string user_name, bool is_hoat_dong, string nguoi_quan_ly_user_name, int vai_tro_nguoi_dung_id, int co_so_quan_ly_code)
    {
        try
        {
            // Kết nối đến cơ sở dữ liệu
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Mở kết nối

                // Câu lệnh SQL để cập nhật nhiều trường theo user_name
                string query = "UPDATE user SET is_hoat_dong = @is_hoat_dong, nguoi_quan_ly_user_name = @nguoi_quan_ly_user_name, vai_tro_nguoi_dung_id = @vai_tro_nguoi_dung_id, co_so_quan_ly_code = @co_so_quan_ly_code WHERE user_name = @user_name";

                // Sử dụng MySqlCommand để thực thi câu lệnh SQL
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@is_hoat_dong", is_hoat_dong);
                    cmd.Parameters.AddWithValue("@nguoi_quan_ly_user_name", nguoi_quan_ly_user_name);
                    cmd.Parameters.AddWithValue("@vai_tro_nguoi_dung_id", vai_tro_nguoi_dung_id);
                    cmd.Parameters.AddWithValue("@co_so_quan_ly_code", co_so_quan_ly_code);
                    cmd.Parameters.AddWithValue("@user_name", user_name);

                    // Thực thi câu lệnh UPDATE
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi với user_name này.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ
            MessageBox.Show($"Lỗi: {ex.Message}");
        }
    }


}

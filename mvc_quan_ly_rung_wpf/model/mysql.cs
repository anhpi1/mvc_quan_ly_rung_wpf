using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using mvc_quan_ly_rung_wpf.model;
using mvc_quan_ly_rung_wpf.view;
using MySql.Data.MySqlClient;
public class show_danh_sach_quan_ly
{
    public List<tinh<List<huyen<List<xa>>>>> danh_sach_quan_ly { get; set; }

    public show_danh_sach_quan_ly()
    {
        danh_sach_quan_ly = new List<tinh<List<huyen<List<xa>>>>>();

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Lấy danh sách dữ liệu từ cơ sở dữ liệu
                var tinhList = GetData(conn, "SELECT * FROM co_so_hanh_chinh WHERE muc_do_hanh_chinh_id = 1;") ?? new List<dynamic>();
                var huyenList = GetData(conn, "SELECT * FROM co_so_hanh_chinh WHERE muc_do_hanh_chinh_id = 2;") ?? new List<dynamic>();
                var xaList = GetData(conn, "SELECT * FROM co_so_hanh_chinh WHERE muc_do_hanh_chinh_id = 3;") ?? new List<dynamic>();

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


    private List<dynamic> GetData(MySqlConnection conn, string query)
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


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdminDAL
    {
        private string connectionString = @"Data Source=DESKTOP-IM2GMIB\MSSQLSERVER01;Initial Catalog=QL_VatLieuXayDung;Integrated Security=True";

        public bool IsValidLogin(string tenDangNhap, string matKhau)
        {
            // Kiểm tra tên đăng nhập và mật khẩu cũ với cơ sở dữ liệu
            string query = "SELECT COUNT(*) FROM TaiKhoanQuanLy WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;  // Nếu count > 0, có nghĩa là tên đăng nhập và mật khẩu đúng
            }
        }

        public bool UpdatePassword(string tenDangNhap, string newPassword)
        {
            // Cập nhật mật khẩu mới cho tên đăng nhập
            string query = "UPDATE TaiKhoanQuanLy SET MatKhau = @MatKhau WHERE TenDangNhap = @TenDangNhap";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MatKhau", newPassword);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;  // Nếu có dòng bị ảnh hưởng, tức là cập nhật thành công
            }
        }
    }
}

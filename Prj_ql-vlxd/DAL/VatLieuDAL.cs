using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VatLieuDAL
    {
        private string connectionString = @"Data Source=DESKTOP-IM2GMIB\MSSQLSERVER01;Initial Catalog=QL_VatLieuXayDung;Integrated Security=True";

        // Lấy tất cả vật liệu
        public DataTable GetAllVatLieu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM VatLieu";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Tìm kiếm vật liệu theo tên
        public DataTable SearchByName(string tenVL)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM VatLieu WHERE TenVatLieu LIKE @Ten";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", "%" + tenVL + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Thêm vật liệu mới
        public bool InsertVatLieu(VatLieuDTO vl)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO VatLieu (TenVatLieu, GiaNhap, GiaXuat, SoLuongTon) VALUES (@Ten, @GiaNhap, @GiaXuat, @SoLuongTon)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", vl.TenVatLieu);
                cmd.Parameters.AddWithValue("@GiaNhap", vl.GiaNhap);
                cmd.Parameters.AddWithValue("@GiaXuat", vl.GiaXuat);
                cmd.Parameters.AddWithValue("@SoLuongTon", vl.SoLuongTon);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật thông tin vật liệu
        public bool UpdateVatLieu(VatLieuDTO vl)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE VatLieu SET TenVatLieu=@Ten, GiaNhap=@GiaNhap, GiaXuat=@GiaXuat, SoLuongTon=@SoLuongTon WHERE MaVatLieu=@Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", vl.TenVatLieu);
                cmd.Parameters.AddWithValue("@GiaNhap", vl.GiaNhap);
                cmd.Parameters.AddWithValue("@GiaXuat", vl.GiaXuat);
                cmd.Parameters.AddWithValue("@SoLuongTon", vl.SoLuongTon);
                cmd.Parameters.AddWithValue("@Ma", vl.MaVatLieu);  // Vẫn giữ MaVatLieu để xác định bản ghi cần cập nhật
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

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
    public class NhapKhoDAL
    {
        private string connectionString = @"Data Source=DESKTOP-IM2GMIB\MSSQLSERVER01;Initial Catalog=QL_VatLieuXayDung;Integrated Security=True";

        public DataTable LoadNhaCungCap()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaNhaCungCap, TenNhaCungCap FROM NhaCungCap";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LoadVatLieu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaVatLieu, TenVatLieu, GiaNhap FROM VatLieu";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ThemPhieuNhap(NhapKhoDTO dto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO NhapKho (MaVatLieu, SoLuongNhap, GiaNhap, NgayNhap, MaNhaCungCap)
                             VALUES (@MaVL, @SL, @Gia, @Ngay, @NCC)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaVL", dto.MaVatLieu);
                cmd.Parameters.AddWithValue("@SL", dto.SoLuongNhap);
                cmd.Parameters.AddWithValue("@Gia", dto.GiaNhap);
                cmd.Parameters.AddWithValue("@Ngay", dto.NgayNhap);
                cmd.Parameters.AddWithValue("@NCC", dto.MaNhaCungCap);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void CapNhatSoLuongTon(int maVL, int soLuongThem)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE VatLieu SET SoLuongTon += @SoLuong WHERE MaVatLieu = @MaVL";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoLuong", soLuongThem);
                cmd.Parameters.AddWithValue("@MaVL", maVL);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

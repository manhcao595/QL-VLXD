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
    public class XuatKhoDAL
    {
        private string connectionString = @"Data Source=DESKTOP-IM2GMIB\MSSQLSERVER01;Initial Catalog=QL_VatLieuXayDung;Integrated Security=True";

        public void ThemPhieuXuat(XuatKhoDTO dto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO XuatKho (MaVatLieu, SoLuongXuat, GiaXuat, NgayXuat, DonViNhan) " +
                             "VALUES (@MaVatLieu, @SoLuongXuat, @GiaXuat, @NgayXuat, @DonViNhan)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaVatLieu", dto.MaVatLieu);
                cmd.Parameters.AddWithValue("@SoLuongXuat", dto.SoLuongXuat);
                cmd.Parameters.AddWithValue("@GiaXuat", dto.GiaXuat);
                cmd.Parameters.AddWithValue("@NgayXuat", dto.NgayXuat);
                cmd.Parameters.AddWithValue("@DonViNhan", dto.DonViNhan);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CapNhatSoLuongTon(int maVL, int soLuongXuat)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE VatLieu SET SoLuongTon = SoLuongTon - @SoLuongXuat WHERE MaVatLieu = @MaVatLieu";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SoLuongXuat", soLuongXuat);
                cmd.Parameters.AddWithValue("@MaVatLieu", maVL);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetVatLieu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT MaVatLieu, TenVatLieu, GiaXuat FROM VatLieu WHERE SoLuongTon > 0";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}

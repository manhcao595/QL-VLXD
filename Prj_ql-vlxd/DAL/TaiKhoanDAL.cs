using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaiKhoanDAL
    {

        public static bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoanQuanLy WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
            SqlParameter[] parameters = {
            new SqlParameter("@TenDangNhap", tenDangNhap),
            new SqlParameter("@MatKhau", matKhau)
        };

            object result = DataProvider.ExecuteScalar(query, parameters);
            return (int)result == 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class bctkDAL
    {
        private DataProvider dataProvider = new DataProvider();

        public DataTable GetNhapKhoTheoKhoangNgay(DateTime tuNgay, DateTime denNgay)
        {
            string query = @"
        SELECT VL.TenVatLieu, NK.SoLuongNhap AS SoLuong, NK.GiaNhap AS DonGia, NK.NgayNhap AS NgayGiaoDich
        FROM NhapKho NK
        JOIN VatLieu VL ON NK.MaVatLieu = VL.MaVatLieu
        WHERE NK.NgayNhap >= @TuNgay AND NK.NgayNhap < DATEADD(DAY, 1, @DenNgay)";

            SqlParameter[] parameters = {
            new SqlParameter("@TuNgay", tuNgay.Date),
            new SqlParameter("@DenNgay", denNgay.Date)
        };

            return dataProvider.ExecuteQuery(query, parameters);
        }

        public DataTable GetXuatKhoTheoKhoangNgay(DateTime tuNgay, DateTime denNgay)
        {
            string query = @"
        SELECT VL.TenVatLieu, XK.SoLuongXuat AS SoLuong, XK.GiaXuat AS DonGia, XK.NgayXuat AS NgayGiaoDich
        FROM XuatKho XK
        JOIN VatLieu VL ON XK.MaVatLieu = VL.MaVatLieu
        WHERE XK.NgayXuat >= @TuNgay AND XK.NgayXuat < DATEADD(DAY, 1, @DenNgay)";

            SqlParameter[] parameters = {
            new SqlParameter("@TuNgay", tuNgay.Date),
            new SqlParameter("@DenNgay", denNgay.Date)
        };

            return dataProvider.ExecuteQuery(query, parameters);
        }
    }
}
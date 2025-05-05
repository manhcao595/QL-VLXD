using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class bctkBLL
    {
        private bctkDAL dal = new bctkDAL();

        public DataTable LayNhapKhoTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            return dal.GetNhapKhoTheoKhoangNgay(tuNgay, denNgay);
        }

        public DataTable LayXuatKhoTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            return dal.GetXuatKhoTheoKhoangNgay(tuNgay, denNgay);
        }

        public decimal TinhTongTien(DataTable data)
        {
            decimal tong = 0;
            foreach (DataRow row in data.Rows)
            {
                int soLuong = Convert.ToInt32(row["SoLuong"]);
                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                tong += soLuong * donGia;
            }
            return tong;
        }
    }
}

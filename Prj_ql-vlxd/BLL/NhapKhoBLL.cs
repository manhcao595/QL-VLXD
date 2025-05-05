using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NhapKhoBLL
    {
        NhapKhoDAL dal = new NhapKhoDAL();

        public DataTable GetNhaCungCap() => dal.LoadNhaCungCap();
        public DataTable GetVatLieu() => dal.LoadVatLieu();
        public bool ThemPhieuNhap(NhapKhoDTO dto) => dal.ThemPhieuNhap(dto);
        public void CapNhatSoLuongTon(int maVL, int soLuong) => dal.CapNhatSoLuongTon(maVL, soLuong);
    }
}

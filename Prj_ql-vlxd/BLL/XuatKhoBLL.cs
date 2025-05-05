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
    public class XuatKhoBLL
    {
        private XuatKhoDAL dal = new XuatKhoDAL();

        public void ThemPhieuXuat(XuatKhoDTO dto)
        {
            dal.ThemPhieuXuat(dto);
            dal.CapNhatSoLuongTon(dto.MaVatLieu, dto.SoLuongXuat);
        }

        public DataTable GetVatLieu()
        {
            return dal.GetVatLieu();
        }
    }
}

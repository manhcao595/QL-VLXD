using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TaiKhoanBLL
    {
        public static bool DangNhap(string tenDangNhap, string matKhau)
        {
            return TaiKhoanDAL.KiemTraDangNhap(tenDangNhap, matKhau);
        }
    }
}

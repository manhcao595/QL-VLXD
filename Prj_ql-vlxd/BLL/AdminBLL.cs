using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AdminBLL
    {
        private AdminDAL taiKhoanDAL = new AdminDAL();

        // Kiểm tra mật khẩu cũ
        public bool ValidateLogin(string tenDangNhap, string matKhau)
        {
            return taiKhoanDAL.IsValidLogin(tenDangNhap, matKhau);
        }

        public bool ChangePassword(string tenDangNhap, string newPassword)
        {
            return taiKhoanDAL.UpdatePassword(tenDangNhap, newPassword);
        }
    }
}

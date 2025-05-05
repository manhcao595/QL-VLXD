using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VatLieuDTO
    {
        public int MaVatLieu { get; set; }
        public string TenVatLieu { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaXuat { get; set; }
        public int SoLuongTon { get; set; }
    }
}

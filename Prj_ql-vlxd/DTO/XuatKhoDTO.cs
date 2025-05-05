using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class XuatKhoDTO
    {
        public int MaVatLieu { get; set; }
        public int SoLuongXuat { get; set; }
        public decimal GiaXuat { get; set; }
        public DateTime NgayXuat { get; set; }
        public string DonViNhan { get; set; }
    }
}

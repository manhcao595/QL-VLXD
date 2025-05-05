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
    public class VatLieuBLL
    {
        VatLieuDAL dal = new VatLieuDAL();

        public DataTable GetAllVatLieu() => dal.GetAllVatLieu();

        public DataTable SearchByName(string ten) => dal.SearchByName(ten);

        public bool AddVatLieu(VatLieuDTO vl) => dal.InsertVatLieu(vl);

        public bool UpdateVatLieu(VatLieuDTO vl) => dal.UpdateVatLieu(vl);
    }
}

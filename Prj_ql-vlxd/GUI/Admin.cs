using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Admin: Form
    {
        // Khởi tạo
        private VatLieuBLL bllVL = new VatLieuBLL();
        private NhapKhoBLL bllNK = new NhapKhoBLL();
        private XuatKhoBLL bllXK = new XuatKhoBLL();
        private bctkBLL bctkBLL = new bctkBLL();
        private AdminBLL bllTaiKhoan = new AdminBLL();

        public Admin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtCurPass.PasswordChar = '●';  // Dấu '*' cho mật khẩu cũ
            txtNewPass.PasswordChar = '●';  // Dấu '*' cho mật khẩu mới
            txtConfPass.PasswordChar = '●'; // Dấu '*' cho mật khẩu xác nhận

            LoadData();
            LoadVatLieuXuat();

            cboNhaCC.DataSource = bllNK.GetNhaCungCap();
            cboNhaCC.DisplayMember = "TenNhaCungCap";
            cboNhaCC.ValueMember = "MaNhaCungCap";

            dgvNhapKho.DataSource = bllNK.GetVatLieu();
            if (!dgvNhapKho.Columns.Contains("Chon"))
            {
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "Chọn";
                chk.Name = "Chon";
                chk.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa header
                dgvNhapKho.Columns.Insert(0, chk);
            }

            dgvNhapKho.Columns.Add("SoLuongNhap", "Số lượng nhập");
            dgvNhapKho.Columns.Add("ThanhTien", "Thành tiền");
        }

        // Hàm
        void LoadData()
        {
            dgvVatLieu.DataSource = bllVL.GetAllVatLieu();
        }

        private void LoadVatLieuXuat()
        {
            dgvXuatKho.Columns.Clear();
            dgvXuatKho.DataSource = bllXK.GetVatLieu();

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Chọn";
            chk.Name = "Chon";
            chk.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvXuatKho.Columns.Insert(0, chk);

            dgvXuatKho.Columns.Add("SoLuongXuat", "Số lượng xuất");
            dgvXuatKho.Columns.Add("ThanhTienXuat", "Thành tiền");

            dgvXuatKho.Columns["SoLuongXuat"].ReadOnly = true;
        }
        // Vật Liệu
        private void btnThem_Click(object sender, EventArgs e)
        {
            VatLieuDTO vl = new VatLieuDTO
            {
                TenVatLieu = txtTenVL.Text,
                GiaNhap = decimal.Parse(txtGiaNhap.Text),
                GiaXuat = decimal.Parse(txtGiaXuat.Text),
                SoLuongTon = int.Parse(txtSL.Text),
            };
            if (bllVL.AddVatLieu(vl))
            {
                MessageBox.Show("Thêm thành công");
                txtTenVL.Clear();
                txtGiaNhap.Clear();
                txtGiaXuat.Clear();
                txtSL.Clear();
                LoadData();
            }
            else MessageBox.Show("Lỗi khi thêm");
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvVatLieu.CurrentRow != null)
            {
                int ma = Convert.ToInt32(dgvVatLieu.CurrentRow.Cells["MaVatLieu"].Value);
                VatLieuDTO vl = new VatLieuDTO
                {
                    MaVatLieu = ma,
                    TenVatLieu = txtTenVL.Text,
                    GiaNhap = decimal.Parse(txtGiaNhap.Text),
                    GiaXuat = decimal.Parse(txtGiaXuat.Text),
                    SoLuongTon = int.Parse(txtSL.Text),
                };
                if (bllVL.UpdateVatLieu(vl))
                {
                    MessageBox.Show("Cập nhật thành công");
                    txtTenVL.Clear();
                    txtGiaNhap.Clear();
                    txtGiaXuat.Clear();
                    txtSL.Clear();
                    LoadData();
                }
                else MessageBox.Show("Lỗi khi cập nhật");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text;
            dgvVatLieu.DataSource = bllVL.SearchByName(keyword);
        }

        // Nhập Kho

        private void dgvNhapKho_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNhapKho.Columns[e.ColumnIndex].Name == "SoLuongNhap")
            {
                foreach (DataGridViewRow row in dgvNhapKho.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chon"].Value) == true)
                    {
                        int soLuong = 0;
                        decimal gia = Convert.ToDecimal(row.Cells["GiaNhap"].Value);
                        if (row.Cells["SoLuongNhap"].Value != null)
                            int.TryParse(row.Cells["SoLuongNhap"].Value.ToString(), out soLuong);

                        row.Cells["ThanhTien"].Value = gia * soLuong;
                    }
                }

                TinhTongTien();
            }
        }
        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataGridViewRow row in dgvNhapKho.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Chon"].Value) == true)
                {
                    if (row.Cells["ThanhTien"].Value != null)
                        tong += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
            }
            txtTongTienNhap.Text = tong.ToString("N0");
        }

        private void dgvNhapKho_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Chỉ cho sửa cột SoLuongNhap nếu checkbox "Chọn" được tick
            if (dgvNhapKho.Columns[e.ColumnIndex].Name == "SoLuongNhap")
            {
                bool isChecked = Convert.ToBoolean(dgvNhapKho.Rows[e.RowIndex].Cells["Chon"].Value);
                if (!isChecked)
                {
                    e.Cancel = true; // Không cho sửa nếu chưa chọn
                }
            }
            else if (dgvNhapKho.Columns[e.ColumnIndex].Name != "Chon")
            {
                // Không cho sửa bất kỳ cột nào khác ngoài Chon và SoLuongNhap
                e.Cancel = true;
            }
        }

        private void btnThemPN_Click(object sender, EventArgs e)
        {
            if (cboNhaCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.");
                return;
            }

            int maNCC = Convert.ToInt32(cboNhaCC.SelectedValue);
            DateTime ngayNhap = dtpNN.Value;

            bool daNhap = false;

            foreach (DataGridViewRow row in dgvNhapKho.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Chon"].Value) == true)
                {
                    if (row.Cells["SoLuongNhap"].Value == null || Convert.ToInt32(row.Cells["SoLuongNhap"].Value) <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập số lượng hợp lệ cho vật liệu đã chọn.");
                        return;
                    }

                    int maVL = Convert.ToInt32(row.Cells["MaVatLieu"].Value);
                    int soLuong = Convert.ToInt32(row.Cells["SoLuongNhap"].Value);
                    decimal gia = Convert.ToDecimal(row.Cells["GiaNhap"].Value);

                    NhapKhoDTO dto = new NhapKhoDTO
                    {
                        MaVatLieu = maVL,
                        SoLuongNhap = soLuong,
                        GiaNhap = gia,
                        NgayNhap = ngayNhap,
                        MaNhaCungCap = maNCC
                    };

                    bllNK.ThemPhieuNhap(dto);
                    bllNK.CapNhatSoLuongTon(maVL, soLuong);

                    daNhap = true;
                }
            }

            if (!daNhap)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một vật liệu để nhập.");
                return;
            }

            MessageBox.Show("Thêm phiếu nhập thành công!");

            // Cập nhật lại tổng tiền
            TinhTongTien();

            // Hiển thị bản in trước
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDocument1;
            preview.ShowDialog();

            // Reset lại dữ liệu trong DataGridView (chỉ reset cột chọn và số lượng)
            foreach (DataGridViewRow row in dgvNhapKho.Rows)
            {
                row.Cells["Chon"].Value = false;
                row.Cells["SoLuongNhap"].Value = null;
                row.Cells["ThanhTien"].Value = 0;
            }

            txtTongTienNhap.Text = "0";
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 14);
            float y = 100;
            float x = 100;

            // In tiêu đề
            e.Graphics.DrawString("PHIẾU NHẬP KHO", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, x + 100, y);
            y += 40;

            // In thông tin nhà cung cấp và ngày
            e.Graphics.DrawString($"Nhà cung cấp: {cboNhaCC.Text}", font, Brushes.Black, x, y); y += 30;
            e.Graphics.DrawString($"Ngày nhập: {dtpNN.Value.ToShortDateString()}", font, Brushes.Black, x, y); y += 30;
            e.Graphics.DrawString($"Tổng tiền: {txtTongTienNhap.Text} VND", font, Brushes.Black, x, y); y += 40;

            // In tiêu đề bảng
            e.Graphics.DrawString("Tên vật liệu", font, Brushes.Black, x, y);
            e.Graphics.DrawString("Đơn giá", font, Brushes.Black, x + 200, y);
            e.Graphics.DrawString("SL", font, Brushes.Black, x + 300, y);
            e.Graphics.DrawString("Thành tiền", font, Brushes.Black, x + 350, y);
            y += 25;

            // In từng dòng vật liệu đã chọn
            foreach (DataGridViewRow row in dgvNhapKho.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Chon"].Value) == true)
                {
                    string ten = row.Cells["TenVatLieu"].Value?.ToString();
                    string gia = row.Cells["GiaNhap"].Value?.ToString();
                    string sl = row.Cells["SoLuongNhap"].Value?.ToString();
                    string tt = row.Cells["ThanhTien"].Value?.ToString();

                    e.Graphics.DrawString(ten, font, Brushes.Black, x, y);
                    e.Graphics.DrawString(gia, font, Brushes.Black, x + 200, y);
                    e.Graphics.DrawString(sl, font, Brushes.Black, x + 300, y);
                    e.Graphics.DrawString(tt, font, Brushes.Black, x + 350, y);
                    y += 25;
                }
            }

            // Ký tên
            y += 40;
            e.Graphics.DrawString("Người nhập kho", font, Brushes.Black, x + 350, y);
        }

        // Xuất Kho
        private void dgvXuatKho_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvXuatKho.Columns[e.ColumnIndex].Name == "Chon")
            {
                bool isChecked = Convert.ToBoolean(dgvXuatKho.Rows[e.RowIndex].Cells["Chon"].Value);
                dgvXuatKho.Rows[e.RowIndex].Cells["SoLuongXuat"].ReadOnly = !isChecked;

                if (!isChecked)
                {
                    dgvXuatKho.Rows[e.RowIndex].Cells["SoLuongXuat"].Value = null;
                    dgvXuatKho.Rows[e.RowIndex].Cells["ThanhTienXuat"].Value = null;
                }
            }
            else if (dgvXuatKho.Columns[e.ColumnIndex].Name == "SoLuongXuat")
            {
                var row = dgvXuatKho.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["SoLuongXuat"].Value?.ToString(), out int sl))
                {
                    decimal gia = Convert.ToDecimal(row.Cells["GiaXuat"].Value);
                    row.Cells["ThanhTienXuat"].Value = sl * gia;
                    TinhTongTienXuat();
                }
            }
        }
        private void TinhTongTienXuat()
        {
            decimal tong = 0;
            foreach (DataGridViewRow row in dgvXuatKho.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Chon"].Value))
                {
                    tong += Convert.ToDecimal(row.Cells["ThanhTienXuat"].Value ?? 0);
                }
            }
            txtTongTienXuat.Text = tong.ToString("N0");
        }

        private List<XuatKhoDTO> danhSachXuatPreview;

        private void btnThemPX_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu textbox Đơn vị nhận trống
            if (string.IsNullOrWhiteSpace(txtDonViNhan.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn vị nhận trước khi xuất hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng lại nếu đơn vị nhận trống
            }

            // Tiếp tục thực hiện thêm phiếu xuất
            danhSachXuatPreview = new List<XuatKhoDTO>();
            string donViNhan = txtDonViNhan.Text.Trim();
            DateTime ngayXuat = dtpNX.Value;

            foreach (DataGridViewRow row in dgvXuatKho.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Chon"].Value))
                {
                    XuatKhoDTO dto = new XuatKhoDTO
                    {
                        MaVatLieu = Convert.ToInt32(row.Cells["MaVatLieu"].Value),
                        SoLuongXuat = Convert.ToInt32(row.Cells["SoLuongXuat"].Value),
                        GiaXuat = Convert.ToDecimal(row.Cells["GiaXuat"].Value),
                        NgayXuat = ngayXuat,
                        DonViNhan = donViNhan
                    };

                    // Thêm vào CSDL
                    bllXK.ThemPhieuXuat(dto);

                    // ✅ Thêm vào danh sách in preview
                    danhSachXuatPreview.Add(dto);
                }
            }

            if (danhSachXuatPreview.Count > 0)
            {
                printPreviewDialog2.Document = printDocument2;
                printPreviewDialog2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xuất!");
            }

            LoadVatLieuXuat();
            txtTongTienXuat.Clear();
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 14);
            float y = 100;
            float x = 100;

            // In tiêu đề
            e.Graphics.DrawString("PHIẾU XUẤT KHO", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, x + 100, y);
            y += 40;

            // In thông tin đơn vị nhận và ngày xuất
            e.Graphics.DrawString($"Đơn vị nhận: {txtDonViNhan.Text}", font, Brushes.Black, x, y); y += 30;
            e.Graphics.DrawString($"Ngày xuất: {dtpNX.Value.ToShortDateString()}", font, Brushes.Black, x, y); y += 30;
            e.Graphics.DrawString($"Tổng tiền: {txtTongTienXuat.Text} VND", font, Brushes.Black, x, y); y += 40;

            // In tiêu đề bảng
            e.Graphics.DrawString("Tên vật liệu", font, Brushes.Black, x, y);
            e.Graphics.DrawString("Đơn giá", font, Brushes.Black, x + 200, y);
            e.Graphics.DrawString("Số lượng", font, Brushes.Black, x + 300, y);
            e.Graphics.DrawString("Thành tiền", font, Brushes.Black, x + 400, y);
            y += 25;

            // In từng dòng vật liệu đã chọn
            decimal tongTien = 0;
            foreach (var item in danhSachXuatPreview)
            {
                string ten = dgvXuatKho.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => Convert.ToInt32(r.Cells["MaVatLieu"].Value) == item.MaVatLieu)?
                    .Cells["TenVatLieu"].Value?.ToString() ?? "N/A";

                decimal thanhTien = item.SoLuongXuat * item.GiaXuat;
                tongTien += thanhTien;

                e.Graphics.DrawString(ten, font, Brushes.Black, x, y);
                e.Graphics.DrawString(item.GiaXuat.ToString("N0"), font, Brushes.Black, x + 200, y);
                e.Graphics.DrawString(item.SoLuongXuat.ToString(), font, Brushes.Black, x + 300, y);
                e.Graphics.DrawString(thanhTien.ToString("N0"), font, Brushes.Black, x + 400, y);
                y += 25;
            }

            // In tổng tiền
            y += 20;
            e.Graphics.DrawString("-------------------------------------------------------------------", font, Brushes.Black, x, y);
            y += 25;
            e.Graphics.DrawString($"TỔNG TIỀN: {tongTien.ToString("N0")} VND", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x, y);

            // In ký tên
            y += 40;
            e.Graphics.DrawString("Người xuất kho", font, Brushes.Black, x + 350, y);
        }

        // Báo cáo thống kê
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date; // Chỉ lấy ngày mà không có giờ
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); // Đảm bảo lấy hết ngày cuối cùng

                // Nhập kho
                DataTable dtNhap = bctkBLL.LayNhapKhoTheoNgay(tuNgay, denNgay);
                dgvListPN.DataSource = dtNhap; // Cập nhật dữ liệu vào DataGridView
                txtTongTienNhap.Text = bctkBLL.TinhTongTien(dtNhap).ToString("N0");

                // Xuất kho
                DataTable dtXuat = bctkBLL.LayXuatKhoTheoNgay(tuNgay, denNgay);
                dgvListPX.DataSource = dtXuat; // Cập nhật dữ liệu vào DataGridView
                txtTongTienXuat.Text = bctkBLL.TinhTongTien(dtXuat).ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thống kê: " + ex.Message);
            }
        }
        // Quản lý
        private void btnCN_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTDN.Text.Trim();
            string matKhauCung = txtCurPass.Text.Trim();
            string matKhauMoi = txtNewPass.Text.Trim();
            string matKhauXacNhan = txtConfPass.Text.Trim();

            // Kiểm tra các trường thông tin không để trống
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhauCung) ||
                string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(matKhauXacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Kiểm tra mật khẩu mới và mật khẩu xác nhận trùng khớp
            if (matKhauMoi != matKhauXacNhan)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu xác nhận không khớp.");
                txtTDN.Clear();
                txtCurPass.Clear();
                txtNewPass.Clear();
                txtConfPass.Clear();
                return;
            }

            // Kiểm tra tên đăng nhập và mật khẩu cũ có đúng hay không
            if (bllTaiKhoan.ValidateLogin(tenDangNhap, matKhauCung))
            {
                // Cập nhật mật khẩu mới
                if (bllTaiKhoan.ChangePassword(tenDangNhap, matKhauMoi))
                {
                    MessageBox.Show("Mật khẩu đã được cập nhật thành công.");
                    txtTDN.Clear();
                    txtCurPass.Clear();
                    txtNewPass.Clear();
                    txtConfPass.Clear();
                }
                else
                {
                    MessageBox.Show("Cập nhật mật khẩu không thành công. Vui lòng thử lại.");
                    txtTDN.Clear();
                    txtCurPass.Clear();
                    txtNewPass.Clear();
                    txtConfPass.Clear();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu cũ không chính xác.");
                txtTDN.Clear();
                txtCurPass.Clear();
                txtNewPass.Clear();
                txtConfPass.Clear();
            }

        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide(); // Ẩn form Admin
                Login loginForm = new Login();
                loginForm.ShowDialog(); // Hiển thị lại form Login
                this.Close();
            }
        }
    }
}

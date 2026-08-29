using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmKhachHang : Form
    {
        private const string SoDienThoaiKhachLe = "0000000000";

        private int? khachHangDangChonId;
        private bool dangLamMoiBieuMau;

        public FrmKhachHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            cboLocTrangThai.SelectedIndex = 0;
            dtpNgaySinh.MaxDate = DateTime.Today;
            dtpNgaySinh.Checked = false;
            KhoiTaoGiaoDienTuyBien();
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void KhoiTaoGiaoDienTuyBien()
        {
            System.Drawing.Color headerColor = System.Drawing.Color.FromArgb(27, 39, 53);
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.AllowUserToDeleteRows = false;
            dgvKhachHang.AllowUserToResizeRows = false;
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            dgvKhachHang.BorderStyle = BorderStyle.None;
            dgvKhachHang.ColumnHeadersHeight = 34;
            dgvKhachHang.Dock = DockStyle.Fill;
            dgvKhachHang.EnableHeadersVisualStyles = false;
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.RowTemplate.Height = 30;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            dgvKhachHang.ThemeStyle.HeaderStyle.BackColor = headerColor;
            dgvKhachHang.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvKhachHang.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dgvKhachHang.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(214, 182, 116);
            dgvKhachHang.ThemeStyle.RowsStyle.SelectionForeColor = headerColor;

            if (dgvKhachHang.Columns.Count == 0)
            {
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã KH", DataPropertyName = "MaKhachHang", Width = 85, ReadOnly = true });
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ tên", DataPropertyName = "HoTen", Width = 160, ReadOnly = true });
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số điện thoại", DataPropertyName = "SoDienThoai", Width = 120, ReadOnly = true });
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Width = 175, ReadOnly = true });
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày sinh", DataPropertyName = "NgaySinhHienThi", Width = 95, ReadOnly = true });
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Điểm", DataPropertyName = "DiemTichLuy", Width = 75, ReadOnly = true });
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nhận email", DataPropertyName = "NhanEmail", Width = 95, ReadOnly = true });
                dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng thái", DataPropertyName = "TrangThai", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 115, ReadOnly = true });
            }
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }

            TaiDanhSach();
            LamMoiBieuMau();
        }

        private bool KiemTraPhienDangNhap(bool hienThongBao)
        {
            bool coQuyen = CurrentUserSession.DaDangNhap;
            if (!coQuyen && hienThongBao)
            {
                MessageBox.Show(
                    "Bạn cần đăng nhập để sử dụng chức năng quản lý khách hàng.",
                    "Chưa đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return coQuyen;
        }

        private void TaiDanhSach(int? khachHangCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<KhachHang> truyVan = db.KhachHangs.AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        truyVan = truyVan.Where(kh =>
                            kh.HoTen.Contains(tuKhoa) ||
                            kh.SoDienThoai.Contains(tuKhoa) ||
                            (kh.Email != null && kh.Email.Contains(tuKhoa)) ||
                            (kh.DiaChi != null && kh.DiaChi.Contains(tuKhoa)));
                    }

                    if (trangThai == 1)
                    {
                        truyVan = truyVan.Where(kh => kh.DangHoatDong);
                    }
                    else if (trangThai == 2)
                    {
                        truyVan = truyVan.Where(kh => !kh.DangHoatDong);
                    }
                    else if (trangThai == 3)
                    {
                        truyVan = truyVan.Where(kh =>
                            kh.DangHoatDong &&
                            kh.ChoPhepNhanEmail &&
                            kh.Email != null &&
                            kh.SoDienThoai != SoDienThoaiKhachLe);
                    }

                    List<KhachHangHienThi> danhSach = truyVan
                        .OrderByDescending(kh => kh.SoDienThoai == SoDienThoaiKhachLe)
                        .ThenByDescending(kh => kh.DangHoatDong)
                        .ThenBy(kh => kh.KhachHangId)
                        .ToList()
                        .Select(kh => new KhachHangHienThi(kh))
                        .ToList();

                    dgvKhachHang.DataSource = danhSach;
                    lblSoKetQua.Text = $"{danhSach.Count} khách hàng";
                }

                if (khachHangCanChonId.HasValue)
                {
                    ChonDong(khachHangCanChonId.Value);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách khách hàng. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void ChonDong(int khachHangId)
        {
            foreach (DataGridViewRow row in dgvKhachHang.Rows)
            {
                var item = row.DataBoundItem as KhachHangHienThi;
                if (item?.KhachHangId != khachHangId)
                {
                    continue;
                }

                row.Selected = true;
                dgvKhachHang.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoiBieuMau)
            {
                return;
            }

            var item = dgvKhachHang.CurrentRow?.DataBoundItem as KhachHangHienThi;
            if (item == null)
            {
                return;
            }

            khachHangDangChonId = item.KhachHangId;
            txtMaKhachHang.Text = item.MaKhachHang;
            txtHoTen.Text = item.HoTen;
            txtSoDienThoai.Text = item.SoDienThoai;
            txtEmail.Text = item.Email ?? string.Empty;
            txtDiaChi.Text = item.DiaChi ?? string.Empty;
            txtDiemTichLuy.Text = item.DiemTichLuy.ToString("N0");
            chkChoPhepNhanEmail.Checked = item.ChoPhepNhanEmail;
            chkDangHoatDong.Checked = item.DangHoatDong;

            if (item.NgaySinh.HasValue)
            {
                dtpNgaySinh.Value = item.NgaySinh.Value;
                dtpNgaySinh.Checked = true;
            }
            else
            {
                dtpNgaySinh.Checked = false;
            }

            bool laKhachLe = item.LaKhachLe;
            BatTatTruongNhap(!laKhachLe);
            btnCapNhat.Enabled = !laKhachLe;
            btnDoiTrangThai.Enabled = !laKhachLe;
            btnDoiTrangThai.Text = item.DangHoatDong ? "Ngừng hoạt động" : "Khôi phục";
            lblThongBao.Text = laKhachLe
                ? "Khách lẻ là bản ghi hệ thống, không được sửa hoặc khóa."
                : string.Empty;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            TaiDanhSach();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            cboLocTrangThai.SelectedIndex = 0;
            TaiDanhSach();
            LamMoiBieuMau();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true))
            {
                return;
            }

            ThongTinKhachHangNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu))
            {
                return;
            }

            if (duLieu.SoDienThoai == SoDienThoaiKhachLe)
            {
                HienThiLoi("Số điện thoại 0000000000 được dành riêng cho bản ghi Khách lẻ.");
                return;
            }

            try
            {
                int khachHangMoiId;
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (SoDienThoaiDaTonTai(db, duLieu.SoDienThoai, null))
                    {
                        HienThiLoi("Số điện thoại đã được sử dụng bởi khách hàng khác.");
                        return;
                    }

                    var khachHang = new KhachHang
                    {
                        HoTen = duLieu.HoTen,
                        SoDienThoai = duLieu.SoDienThoai,
                        Email = duLieu.Email,
                        DiaChi = duLieu.DiaChi,
                        NgaySinh = duLieu.NgaySinh,
                        ChoPhepNhanEmail = duLieu.ChoPhepNhanEmail,
                        DiemTichLuy = 0,
                        DangHoatDong = true
                    };

                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    khachHangMoiId = khachHang.KhachHangId;
                }

                TaiDanhSach(khachHangMoiId);
                MessageBox.Show(
                    $"Đã thêm khách hàng KH{khachHangMoiId:000000}.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu khách hàng. Số điện thoại có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thêm khách hàng. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !khachHangDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn khách hàng cần cập nhật.");
                return;
            }

            ThongTinKhachHangNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu))
            {
                return;
            }

            int khachHangId = khachHangDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.KhachHangId == khachHangId);
                    if (khachHang == null)
                    {
                        HienThiLoi("Khách hàng không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    if (LaKhachLe(khachHang))
                    {
                        HienThiLoi("Không được sửa bản ghi Khách lẻ của hệ thống.");
                        return;
                    }

                    if (duLieu.SoDienThoai == SoDienThoaiKhachLe)
                    {
                        HienThiLoi("Số điện thoại 0000000000 được dành riêng cho bản ghi Khách lẻ.");
                        return;
                    }

                    if (SoDienThoaiDaTonTai(db, duLieu.SoDienThoai, khachHangId))
                    {
                        HienThiLoi("Số điện thoại đã được sử dụng bởi khách hàng khác.");
                        return;
                    }

                    khachHang.HoTen = duLieu.HoTen;
                    khachHang.SoDienThoai = duLieu.SoDienThoai;
                    khachHang.Email = duLieu.Email;
                    khachHang.DiaChi = duLieu.DiaChi;
                    khachHang.NgaySinh = duLieu.NgaySinh;
                    khachHang.ChoPhepNhanEmail = duLieu.ChoPhepNhanEmail;
                    db.SaveChanges();
                }

                TaiDanhSach(khachHangId);
                MessageBox.Show(
                    "Đã cập nhật thông tin khách hàng.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cập nhật. Số điện thoại có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật khách hàng. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnDoiTrangThai_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !khachHangDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn khách hàng cần thay đổi trạng thái.");
                return;
            }

            int khachHangId = khachHangDangChonId.Value;
            bool trangThaiMoi = !chkDangHoatDong.Checked;
            string hanhDong = trangThaiMoi ? "khôi phục" : "ngừng hoạt động";
            if (MessageBox.Show(
                    $"Bạn có chắc muốn {hanhDong} khách hàng {txtHoTen.Text.Trim()}?",
                    "Xác nhận thay đổi trạng thái",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.KhachHangId == khachHangId);
                    if (khachHang == null)
                    {
                        HienThiLoi("Khách hàng không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    if (LaKhachLe(khachHang))
                    {
                        HienThiLoi("Không được khóa bản ghi Khách lẻ của hệ thống.");
                        return;
                    }

                    khachHang.DangHoatDong = trangThaiMoi;
                    db.SaveChanges();
                }

                TaiDanhSach(khachHangId);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thay đổi trạng thái khách hàng.");
            }
        }

        private void btnLamMoiBieuMau_Click(object sender, EventArgs e)
        {
            LamMoiBieuMau();
        }

        private void LamMoiBieuMau()
        {
            dangLamMoiBieuMau = true;
            try
            {
                khachHangDangChonId = null;
                txtMaKhachHang.Text = "Tự động tạo";
                txtHoTen.Clear();
                txtSoDienThoai.Clear();
                txtEmail.Clear();
                txtDiaChi.Clear();
                txtDiemTichLuy.Text = "0";
                dtpNgaySinh.Value = DateTime.Today;
                dtpNgaySinh.Checked = false;
                chkChoPhepNhanEmail.Checked = false;
                chkDangHoatDong.Checked = true;
                btnDoiTrangThai.Text = "Ngừng hoạt động";
                btnCapNhat.Enabled = true;
                btnDoiTrangThai.Enabled = true;
                BatTatTruongNhap(true);
                lblThongBao.Text = string.Empty;
                dgvKhachHang.ClearSelection();
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }

            txtHoTen.Focus();
        }

        private void BatTatTruongNhap(bool bat)
        {
            txtHoTen.ReadOnly = !bat;
            txtSoDienThoai.ReadOnly = !bat;
            txtEmail.ReadOnly = !bat;
            txtDiaChi.ReadOnly = !bat;
            dtpNgaySinh.Enabled = bat;
            chkChoPhepNhanEmail.Enabled = bat;
        }

        private bool ThuLayDuLieuNhap(out ThongTinKhachHangNhap duLieu)
        {
            duLieu = null;
            string hoTen = txtHoTen.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = ChuanHoaTuyChon(txtEmail.Text);
            string diaChi = ChuanHoaTuyChon(txtDiaChi.Text);

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                HienThiLoi("Họ tên khách hàng không được để trống.");
                txtHoTen.Focus();
                return false;
            }

            if (soDienThoai.Length < 9 || soDienThoai.Length > 15 ||
                soDienThoai.Any(kyTu => !char.IsDigit(kyTu)))
            {
                HienThiLoi("Số điện thoại phải gồm từ 9 đến 15 chữ số.");
                txtSoDienThoai.Focus();
                return false;
            }

            if (email != null && !EmailHopLe(email))
            {
                HienThiLoi("Địa chỉ email không hợp lệ.");
                txtEmail.Focus();
                return false;
            }

            if (chkChoPhepNhanEmail.Checked && email == null)
            {
                HienThiLoi("Cần nhập email trước khi cho phép khách hàng nhận email.");
                txtEmail.Focus();
                return false;
            }

            DateTime? ngaySinh = dtpNgaySinh.Checked
                ? dtpNgaySinh.Value.Date
                : (DateTime?)null;
            if (ngaySinh.HasValue && ngaySinh.Value > DateTime.Today)
            {
                HienThiLoi("Ngày sinh không được lớn hơn ngày hiện tại.");
                return false;
            }

            duLieu = new ThongTinKhachHangNhap
            {
                HoTen = hoTen,
                SoDienThoai = soDienThoai,
                Email = email,
                DiaChi = diaChi,
                NgaySinh = ngaySinh,
                ChoPhepNhanEmail = chkChoPhepNhanEmail.Checked
            };
            lblThongBao.Text = string.Empty;
            return true;
        }

        private static bool SoDienThoaiDaTonTai(
            QL_CuaHangDaQuy_PNJEntities db,
            string soDienThoai,
            int? boQuaKhachHangId)
        {
            return db.KhachHangs.Any(kh =>
                kh.SoDienThoai == soDienThoai &&
                (!boQuaKhachHangId.HasValue || kh.KhachHangId != boQuaKhachHangId.Value));
        }

        private static bool LaKhachLe(KhachHang khachHang)
        {
            return khachHang.SoDienThoai == SoDienThoaiKhachLe;
        }

        private static string ChuanHoaTuyChon(string giaTri)
        {
            string ketQua = (giaTri ?? string.Empty).Trim();
            return ketQua.Length == 0 ? null : ketQua;
        }

        private static bool EmailHopLe(string email)
        {
            if (email.Length > 254)
            {
                return false;
            }

            try
            {
                return new MailAddress(email).Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void HienThiLoi(string noiDung)
        {
            lblThongBao.Text = "* " + noiDung;
        }

        private sealed class ThongTinKhachHangNhap
        {
            public string HoTen { get; set; }
            public string SoDienThoai { get; set; }
            public string Email { get; set; }
            public string DiaChi { get; set; }
            public DateTime? NgaySinh { get; set; }
            public bool ChoPhepNhanEmail { get; set; }
        }

        private sealed class KhachHangHienThi
        {
            public KhachHangHienThi(KhachHang khachHang)
            {
                KhachHangId = khachHang.KhachHangId;
                MaKhachHang = $"KH{khachHang.KhachHangId:000000}";
                HoTen = khachHang.HoTen;
                SoDienThoai = khachHang.SoDienThoai;
                Email = khachHang.Email;
                DiaChi = khachHang.DiaChi;
                NgaySinh = khachHang.NgaySinh;
                NgaySinhHienThi = khachHang.NgaySinh?.ToString("dd/MM/yyyy") ?? string.Empty;
                ChoPhepNhanEmail = khachHang.ChoPhepNhanEmail;
                DiemTichLuy = khachHang.DiemTichLuy;
                DangHoatDong = khachHang.DangHoatDong;
                NhanEmail = khachHang.ChoPhepNhanEmail ? "Đồng ý" : "Không";
                TrangThai = khachHang.DangHoatDong ? "Đang hoạt động" : "Ngừng hoạt động";
                LaKhachLe = khachHang.SoDienThoai == SoDienThoaiKhachLe;
            }

            public int KhachHangId { get; }
            public string MaKhachHang { get; }
            public string HoTen { get; }
            public string SoDienThoai { get; }
            public string Email { get; }
            public string DiaChi { get; }
            public DateTime? NgaySinh { get; }
            public string NgaySinhHienThi { get; }
            public bool ChoPhepNhanEmail { get; }
            public int DiemTichLuy { get; }
            public bool DangHoatDong { get; }
            public string NhanEmail { get; }
            public string TrangThai { get; }
            public bool LaKhachLe { get; }
        }
    }
}

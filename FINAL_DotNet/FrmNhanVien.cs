using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmNhanVien : Form
    {
        private int? nhanVienDangChonId;

        public FrmNhanVien()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            cboLocTrangThai.SelectedIndex = 0;
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.MaxDate = DateTime.Today;
            dtpNgaySinh.Checked = false;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }

            TaiDanhSach();
            LamMoiBieuMau();
        }

        private bool KiemTraQuyenQuanTri(bool hienThongBao)
        {
            bool coQuyen = CurrentUserSession.DaDangNhap &&
                           CurrentUserSession.HienTai.LaQuanTriVien;

            if (!coQuyen && hienThongBao)
            {
                MessageBox.Show(
                    "Chỉ quản trị viên được sử dụng chức năng quản lý nhân viên.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return coQuyen;
        }

        private void TaiDanhSach(int? nhanVienCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<NhanVien> truyVan = db.NhanViens.AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        truyVan = truyVan.Where(nv =>
                            nv.HoTen.Contains(tuKhoa) ||
                            (nv.SoDienThoai != null && nv.SoDienThoai.Contains(tuKhoa)) ||
                            (nv.Email != null && nv.Email.Contains(tuKhoa)) ||
                            nv.ChucVu.Contains(tuKhoa));
                    }

                    if (trangThai == 1)
                    {
                        truyVan = truyVan.Where(nv => nv.DangLamViec);
                    }
                    else if (trangThai == 2)
                    {
                        truyVan = truyVan.Where(nv => !nv.DangLamViec);
                    }

                    List<NhanVienHienThi> danhSach = truyVan
                        .OrderByDescending(nv => nv.DangLamViec)
                        .ThenBy(nv => nv.NhanVienId)
                        .ToList()
                        .Select(nv => new NhanVienHienThi(nv))
                        .ToList();

                    dgvNhanVien.DataSource = danhSach;
                    lblSoKetQua.Text = $"{danhSach.Count} nhân viên";
                }

                if (nhanVienCanChonId.HasValue)
                {
                    ChonDong(nhanVienCanChonId.Value);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách nhân viên. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void ChonDong(int nhanVienId)
        {
            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                var item = row.DataBoundItem as NhanVienHienThi;
                if (item?.NhanVienId != nhanVienId)
                {
                    continue;
                }

                row.Selected = true;
                dgvNhanVien.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            var item = dgvNhanVien.CurrentRow?.DataBoundItem as NhanVienHienThi;
            if (item == null)
            {
                return;
            }

            nhanVienDangChonId = item.NhanVienId;
            txtMaNhanVien.Text = item.MaNhanVien;
            txtHoTen.Text = item.HoTen;
            cboGioiTinh.SelectedItem = item.GioiTinh ?? string.Empty;
            if (cboGioiTinh.SelectedIndex < 0)
            {
                cboGioiTinh.SelectedIndex = 0;
            }

            if (item.NgaySinh.HasValue)
            {
                dtpNgaySinh.Value = item.NgaySinh.Value;
                dtpNgaySinh.Checked = true;
            }
            else
            {
                dtpNgaySinh.Checked = false;
            }

            txtSoDienThoai.Text = item.SoDienThoai ?? string.Empty;
            txtEmail.Text = item.Email ?? string.Empty;
            txtDiaChi.Text = item.DiaChi ?? string.Empty;
            txtChucVu.Text = item.ChucVu;
            chkDangLamViec.Checked = item.DangLamViec;
            btnDoiTrangThai.Text = item.DangLamViec ? "Ngừng làm việc" : "Khôi phục";
            lblThongBao.Text = string.Empty;
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
            if (!KiemTraQuyenQuanTri(true))
            {
                return;
            }

            ThongTinNhanVienNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu))
            {
                return;
            }

            try
            {
                int nhanVienMoiId;
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (SoDienThoaiDaTonTai(db, duLieu.SoDienThoai, null))
                    {
                        HienThiLoi("Số điện thoại đã được sử dụng bởi nhân viên khác.");
                        return;
                    }

                    var nhanVien = new NhanVien
                    {
                        HoTen = duLieu.HoTen,
                        GioiTinh = duLieu.GioiTinh,
                        NgaySinh = duLieu.NgaySinh,
                        SoDienThoai = duLieu.SoDienThoai,
                        Email = duLieu.Email,
                        DiaChi = duLieu.DiaChi,
                        ChucVu = duLieu.ChucVu,
                        DangLamViec = true
                    };

                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    nhanVienMoiId = nhanVien.NhanVienId;
                }

                TaiDanhSach(nhanVienMoiId);
                MessageBox.Show(
                    $"Đã thêm nhân viên NV{nhanVienMoiId:000000}.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu nhân viên. Số điện thoại có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thêm nhân viên. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !nhanVienDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn nhân viên cần cập nhật.");
                return;
            }

            ThongTinNhanVienNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu))
            {
                return;
            }

            int nhanVienId = nhanVienDangChonId.Value;

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (SoDienThoaiDaTonTai(db, duLieu.SoDienThoai, nhanVienId))
                    {
                        HienThiLoi("Số điện thoại đã được sử dụng bởi nhân viên khác.");
                        return;
                    }

                    var nhanVien = db.NhanViens.SingleOrDefault(nv => nv.NhanVienId == nhanVienId);
                    if (nhanVien == null)
                    {
                        HienThiLoi("Nhân viên không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    nhanVien.HoTen = duLieu.HoTen;
                    nhanVien.GioiTinh = duLieu.GioiTinh;
                    nhanVien.NgaySinh = duLieu.NgaySinh;
                    nhanVien.SoDienThoai = duLieu.SoDienThoai;
                    nhanVien.Email = duLieu.Email;
                    nhanVien.DiaChi = duLieu.DiaChi;
                    nhanVien.ChucVu = duLieu.ChucVu;
                    db.SaveChanges();
                }

                TaiDanhSach(nhanVienId);
                MessageBox.Show(
                    "Đã cập nhật thông tin nhân viên.",
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
                HienThiLoi("Không thể cập nhật nhân viên. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnDoiTrangThai_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !nhanVienDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn nhân viên cần thay đổi trạng thái.");
                return;
            }

            int nhanVienId = nhanVienDangChonId.Value;
            if (nhanVienId == CurrentUserSession.HienTai.NhanVienId && chkDangLamViec.Checked)
            {
                HienThiLoi("Bạn không thể ngừng làm việc cho chính tài khoản đang đăng nhập.");
                return;
            }

            bool trangThaiMoi = !chkDangLamViec.Checked;
            string hanhDong = trangThaiMoi ? "khôi phục" : "ngừng làm việc";
            if (MessageBox.Show(
                    $"Bạn có chắc muốn {hanhDong} nhân viên {txtHoTen.Text.Trim()}?",
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
                    var nhanVien = db.NhanViens.SingleOrDefault(nv => nv.NhanVienId == nhanVienId);
                    if (nhanVien == null)
                    {
                        HienThiLoi("Nhân viên không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    nhanVien.DangLamViec = trangThaiMoi;
                    db.SaveChanges();
                }

                TaiDanhSach(nhanVienId);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thay đổi trạng thái nhân viên.");
            }
        }

        private void btnLamMoiBieuMau_Click(object sender, EventArgs e)
        {
            LamMoiBieuMau();
        }

        private void LamMoiBieuMau()
        {
            nhanVienDangChonId = null;
            txtMaNhanVien.Text = "Tự động tạo";
            txtHoTen.Clear();
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Today;
            dtpNgaySinh.Checked = false;
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtChucVu.Clear();
            chkDangLamViec.Checked = true;
            btnDoiTrangThai.Text = "Ngừng làm việc";
            lblThongBao.Text = string.Empty;
            dgvNhanVien.ClearSelection();
            txtHoTen.Focus();
        }

        private bool ThuLayDuLieuNhap(out ThongTinNhanVienNhap duLieu)
        {
            duLieu = null;
            string hoTen = txtHoTen.Text.Trim();
            string chucVu = txtChucVu.Text.Trim();
            string soDienThoai = ChuanHoaTuyChon(txtSoDienThoai.Text);
            string email = ChuanHoaTuyChon(txtEmail.Text);
            string diaChi = ChuanHoaTuyChon(txtDiaChi.Text);
            string gioiTinh = cboGioiTinh.SelectedIndex <= 0
                ? null
                : cboGioiTinh.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                HienThiLoi("Họ tên nhân viên không được để trống.");
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(chucVu))
            {
                HienThiLoi("Chức vụ không được để trống.");
                txtChucVu.Focus();
                return false;
            }

            if (soDienThoai != null &&
                (soDienThoai.Length < 9 || soDienThoai.Length > 15 ||
                 soDienThoai.Any(kyTu => !char.IsDigit(kyTu))))
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

            DateTime? ngaySinh = dtpNgaySinh.Checked
                ? dtpNgaySinh.Value.Date
                : (DateTime?)null;

            if (ngaySinh.HasValue && ngaySinh.Value > DateTime.Today)
            {
                HienThiLoi("Ngày sinh không được lớn hơn ngày hiện tại.");
                return false;
            }

            duLieu = new ThongTinNhanVienNhap
            {
                HoTen = hoTen,
                GioiTinh = gioiTinh,
                NgaySinh = ngaySinh,
                SoDienThoai = soDienThoai,
                Email = email,
                DiaChi = diaChi,
                ChucVu = chucVu
            };
            lblThongBao.Text = string.Empty;
            return true;
        }

        private static bool SoDienThoaiDaTonTai(
            QL_CuaHangDaQuy_PNJEntities db,
            string soDienThoai,
            int? boQuaNhanVienId)
        {
            return soDienThoai != null && db.NhanViens.Any(nv =>
                nv.SoDienThoai == soDienThoai &&
                (!boQuaNhanVienId.HasValue || nv.NhanVienId != boQuaNhanVienId.Value));
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

        private sealed class ThongTinNhanVienNhap
        {
            public string HoTen { get; set; }
            public string GioiTinh { get; set; }
            public DateTime? NgaySinh { get; set; }
            public string SoDienThoai { get; set; }
            public string Email { get; set; }
            public string DiaChi { get; set; }
            public string ChucVu { get; set; }
        }

        private sealed class NhanVienHienThi
        {
            public NhanVienHienThi(NhanVien nhanVien)
            {
                NhanVienId = nhanVien.NhanVienId;
                MaNhanVien = $"NV{nhanVien.NhanVienId:000000}";
                HoTen = nhanVien.HoTen;
                GioiTinh = nhanVien.GioiTinh;
                NgaySinh = nhanVien.NgaySinh;
                NgaySinhHienThi = nhanVien.NgaySinh?.ToString("dd/MM/yyyy") ?? string.Empty;
                SoDienThoai = nhanVien.SoDienThoai;
                Email = nhanVien.Email;
                DiaChi = nhanVien.DiaChi;
                ChucVu = nhanVien.ChucVu;
                DangLamViec = nhanVien.DangLamViec;
                TrangThai = nhanVien.DangLamViec ? "Đang làm việc" : "Đã nghỉ";
            }

            public int NhanVienId { get; }
            public string MaNhanVien { get; }
            public string HoTen { get; }
            public string GioiTinh { get; }
            public DateTime? NgaySinh { get; }
            public string NgaySinhHienThi { get; }
            public string SoDienThoai { get; }
            public string Email { get; }
            public string DiaChi { get; }
            public string ChucVu { get; }
            public bool DangLamViec { get; }
            public string TrangThai { get; }
        }
    }
}

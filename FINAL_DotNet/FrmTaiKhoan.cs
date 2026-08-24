using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmTaiKhoan : Form
    {
        private static readonly Regex MauTenDangNhap =
            new Regex("^[A-Za-z0-9._-]{3,50}$", RegexOptions.Compiled);

        private int? taiKhoanDangChonId;
        private bool dangLamMoiBieuMau;

        public FrmTaiKhoan()
        {
            InitializeComponent();
            cboLocTrangThai.SelectedIndex = 0;
            cboVaiTro.SelectedIndex = 1;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmTaiKhoan_Load(object sender, EventArgs e)
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
                    "Chỉ quản trị viên được sử dụng chức năng quản lý tài khoản.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return coQuyen;
        }

        private void TaiDanhSach(int? taiKhoanCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<TaiKhoan> truyVan = db.TaiKhoans
                        .Include(tk => tk.NhanVien)
                        .AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        truyVan = truyVan.Where(tk =>
                            tk.TenDangNhap.Contains(tuKhoa) ||
                            tk.NhanVien.HoTen.Contains(tuKhoa) ||
                            tk.NhanVien.ChucVu.Contains(tuKhoa) ||
                            tk.VaiTro.Contains(tuKhoa));
                    }

                    if (trangThai == 1)
                    {
                        truyVan = truyVan.Where(tk => tk.DangHoatDong);
                    }
                    else if (trangThai == 2)
                    {
                        truyVan = truyVan.Where(tk => !tk.DangHoatDong);
                    }
                    else if (trangThai == 3)
                    {
                        truyVan = truyVan.Where(tk => tk.PhaiDoiMatKhau);
                    }

                    List<TaiKhoanHienThi> danhSach = truyVan
                        .OrderByDescending(tk => tk.DangHoatDong)
                        .ThenBy(tk => tk.TaiKhoanId)
                        .ToList()
                        .Select(tk => new TaiKhoanHienThi(tk))
                        .ToList();

                    dgvTaiKhoan.DataSource = danhSach;
                    lblSoKetQua.Text = $"{danhSach.Count} tài khoản";
                }

                if (taiKhoanCanChonId.HasValue)
                {
                    ChonDong(taiKhoanCanChonId.Value);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách tài khoản. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void TaiNhanVienCoTheCapTaiKhoan(int? nhanVienHienTaiId = null)
        {
            using (var db = DatabaseConnection.CreateContext())
            {
                List<int> daCoTaiKhoan = db.TaiKhoans
                    .AsNoTracking()
                    .Select(tk => tk.NhanVienId)
                    .ToList();

                List<NhanVienLuaChon> danhSach = db.NhanViens
                    .AsNoTracking()
                    .Where(nv =>
                        (nhanVienHienTaiId.HasValue && nv.NhanVienId == nhanVienHienTaiId.Value) ||
                        (nv.DangLamViec && !daCoTaiKhoan.Contains(nv.NhanVienId)))
                    .OrderBy(nv => nv.HoTen)
                    .ToList()
                    .Select(nv => new NhanVienLuaChon(nv))
                    .ToList();

                cboNhanVien.DataSource = danhSach;
                cboNhanVien.DisplayMember = nameof(NhanVienLuaChon.NoiDungHienThi);
                cboNhanVien.ValueMember = nameof(NhanVienLuaChon.NhanVienId);
                cboNhanVien.SelectedIndex = danhSach.Count > 0 ? 0 : -1;
            }
        }

        private void ChonDong(int taiKhoanId)
        {
            foreach (DataGridViewRow row in dgvTaiKhoan.Rows)
            {
                var item = row.DataBoundItem as TaiKhoanHienThi;
                if (item?.TaiKhoanId != taiKhoanId)
                {
                    continue;
                }

                row.Selected = true;
                dgvTaiKhoan.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void dgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoiBieuMau)
            {
                return;
            }

            var item = dgvTaiKhoan.CurrentRow?.DataBoundItem as TaiKhoanHienThi;
            if (item == null)
            {
                return;
            }

            taiKhoanDangChonId = item.TaiKhoanId;
            TaiNhanVienCoTheCapTaiKhoan(item.NhanVienId);
            cboNhanVien.SelectedValue = item.NhanVienId;
            cboNhanVien.Enabled = false;
            txtMaTaiKhoan.Text = item.MaTaiKhoan;
            txtTenDangNhap.Text = item.TenDangNhap;
            cboVaiTro.SelectedItem = item.VaiTro;
            chkDangHoatDong.Checked = item.DangHoatDong;
            chkPhaiDoiMatKhau.Checked = item.PhaiDoiMatKhau;
            txtMatKhauTam.Clear();
            txtXacNhanMatKhau.Clear();
            btnDoiTrangThai.Text = item.DangHoatDong ? "Khóa tài khoản" : "Mở tài khoản";
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

        private void btnCapTaiKhoan_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true))
            {
                return;
            }

            ThongTinTaiKhoanNhap duLieu;
            if (!ThuLayDuLieuNhap(true, out duLieu))
            {
                return;
            }

            try
            {
                int taiKhoanMoiId;
                using (var db = DatabaseConnection.CreateContext())
                {
                    var nhanVien = db.NhanViens.SingleOrDefault(nv => nv.NhanVienId == duLieu.NhanVienId);
                    if (nhanVien == null || !nhanVien.DangLamViec)
                    {
                        HienThiLoi("Nhân viên không tồn tại hoặc đã ngừng làm việc.");
                        return;
                    }

                    if (db.TaiKhoans.Any(tk => tk.NhanVienId == duLieu.NhanVienId))
                    {
                        HienThiLoi("Nhân viên này đã có tài khoản.");
                        return;
                    }

                    if (TenDangNhapDaTonTai(db, duLieu.TenDangNhap, null))
                    {
                        HienThiLoi("Tên đăng nhập đã được sử dụng.");
                        return;
                    }

                    var taiKhoan = new TaiKhoan
                    {
                        NhanVienId = duLieu.NhanVienId,
                        TenDangNhap = duLieu.TenDangNhap,
                        MatKhauHash = BCrypt.Net.BCrypt.HashPassword(duLieu.MatKhauTam),
                        VaiTro = duLieu.VaiTro,
                        PhaiDoiMatKhau = true,
                        DangHoatDong = true
                    };

                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    taiKhoanMoiId = taiKhoan.TaiKhoanId;
                }

                TaiDanhSach(taiKhoanMoiId);
                MessageBox.Show(
                    "Đã cấp tài khoản. Người dùng phải đổi mật khẩu ở lần đăng nhập tiếp theo.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cấp tài khoản. Nhân viên hoặc tên đăng nhập có thể đã được sử dụng.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cấp tài khoản. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !taiKhoanDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn tài khoản cần cập nhật.");
                return;
            }

            ThongTinTaiKhoanNhap duLieu;
            if (!ThuLayDuLieuNhap(false, out duLieu))
            {
                return;
            }

            int taiKhoanId = taiKhoanDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var taiKhoan = db.TaiKhoans.SingleOrDefault(tk => tk.TaiKhoanId == taiKhoanId);
                    if (taiKhoan == null)
                    {
                        HienThiLoi("Tài khoản không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    if (TenDangNhapDaTonTai(db, duLieu.TenDangNhap, taiKhoanId))
                    {
                        HienThiLoi("Tên đăng nhập đã được sử dụng.");
                        return;
                    }

                    bool dangHaQuyen = taiKhoan.VaiTro == "ADMIN" && duLieu.VaiTro != "ADMIN";
                    if (dangHaQuyen && taiKhoanId == CurrentUserSession.HienTai.TaiKhoanId)
                    {
                        HienThiLoi("Bạn không thể hạ quyền tài khoản đang đăng nhập.");
                        return;
                    }

                    if (dangHaQuyen && !ConQuanTriVienHoatDongKhac(db, taiKhoanId))
                    {
                        HienThiLoi("Không thể hạ quyền quản trị viên đang hoạt động cuối cùng.");
                        return;
                    }

                    taiKhoan.TenDangNhap = duLieu.TenDangNhap;
                    taiKhoan.VaiTro = duLieu.VaiTro;
                    db.SaveChanges();
                }

                TaiDanhSach(taiKhoanId);
                MessageBox.Show(
                    "Đã cập nhật tên đăng nhập và vai trò.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cập nhật. Tên đăng nhập có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật tài khoản.");
            }
        }

        private void btnDoiTrangThai_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !taiKhoanDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn tài khoản cần khóa hoặc mở.");
                return;
            }

            int taiKhoanId = taiKhoanDangChonId.Value;
            bool trangThaiMoi = !chkDangHoatDong.Checked;
            if (!trangThaiMoi && taiKhoanId == CurrentUserSession.HienTai.TaiKhoanId)
            {
                HienThiLoi("Bạn không thể khóa tài khoản đang đăng nhập.");
                return;
            }

            string hanhDong = trangThaiMoi ? "mở" : "khóa";
            if (MessageBox.Show(
                    $"Bạn có chắc muốn {hanhDong} tài khoản {txtTenDangNhap.Text.Trim()}?",
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
                    var taiKhoan = db.TaiKhoans.SingleOrDefault(tk => tk.TaiKhoanId == taiKhoanId);
                    if (taiKhoan == null)
                    {
                        HienThiLoi("Tài khoản không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    if (!trangThaiMoi && taiKhoan.VaiTro == "ADMIN" &&
                        !ConQuanTriVienHoatDongKhac(db, taiKhoanId))
                    {
                        HienThiLoi("Không thể khóa quản trị viên đang hoạt động cuối cùng.");
                        return;
                    }

                    taiKhoan.DangHoatDong = trangThaiMoi;
                    db.SaveChanges();
                }

                TaiDanhSach(taiKhoanId);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thay đổi trạng thái tài khoản.");
            }
        }

        private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !taiKhoanDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn tài khoản cần đặt lại mật khẩu.");
                return;
            }

            string matKhau;
            if (!ThuLayMatKhau(out matKhau))
            {
                return;
            }

            if (MessageBox.Show(
                    $"Đặt mật khẩu tạm mới cho tài khoản {txtTenDangNhap.Text.Trim()}?",
                    "Xác nhận đặt lại mật khẩu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int taiKhoanId = taiKhoanDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var taiKhoan = db.TaiKhoans.SingleOrDefault(tk => tk.TaiKhoanId == taiKhoanId);
                    if (taiKhoan == null)
                    {
                        HienThiLoi("Tài khoản không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    taiKhoan.MatKhauHash = BCrypt.Net.BCrypt.HashPassword(matKhau);
                    taiKhoan.PhaiDoiMatKhau = true;
                    db.SaveChanges();
                }

                txtMatKhauTam.Clear();
                txtXacNhanMatKhau.Clear();
                TaiDanhSach(taiKhoanId);
                MessageBox.Show(
                    "Đã đặt mật khẩu tạm. Người dùng phải đổi mật khẩu ở lần đăng nhập tiếp theo.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể đặt lại mật khẩu.");
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
                taiKhoanDangChonId = null;
                txtMaTaiKhoan.Text = "Tự động tạo";
                txtTenDangNhap.Clear();
                cboVaiTro.SelectedItem = "NHANVIEN";
                txtMatKhauTam.Clear();
                txtXacNhanMatKhau.Clear();
                chkDangHoatDong.Checked = true;
                chkPhaiDoiMatKhau.Checked = true;
                btnDoiTrangThai.Text = "Khóa tài khoản";
                cboNhanVien.Enabled = true;
                lblThongBao.Text = string.Empty;
                dgvTaiKhoan.ClearSelection();

                try
                {
                    TaiNhanVienCoTheCapTaiKhoan();
                }
                catch (Exception)
                {
                    HienThiLoi("Không thể tải danh sách nhân viên chưa có tài khoản.");
                }
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }

            txtTenDangNhap.Focus();
        }

        private bool ThuLayDuLieuNhap(bool canMatKhau, out ThongTinTaiKhoanNhap duLieu)
        {
            duLieu = null;
            var nhanVien = cboNhanVien.SelectedItem as NhanVienLuaChon;
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string vaiTro = cboVaiTro.SelectedItem as string;

            if (nhanVien == null)
            {
                HienThiLoi("Không có nhân viên đang làm việc phù hợp để cấp tài khoản.");
                return false;
            }

            if (!MauTenDangNhap.IsMatch(tenDangNhap))
            {
                HienThiLoi("Tên đăng nhập dài 3–50 ký tự, chỉ gồm chữ, số, dấu chấm, gạch dưới hoặc gạch ngang.");
                txtTenDangNhap.Focus();
                return false;
            }

            if (vaiTro != "ADMIN" && vaiTro != "NHANVIEN")
            {
                HienThiLoi("Vui lòng chọn vai trò hợp lệ.");
                return false;
            }

            string matKhau = null;
            if (canMatKhau && !ThuLayMatKhau(out matKhau))
            {
                return false;
            }

            duLieu = new ThongTinTaiKhoanNhap
            {
                NhanVienId = nhanVien.NhanVienId,
                TenDangNhap = tenDangNhap,
                VaiTro = vaiTro,
                MatKhauTam = matKhau
            };
            lblThongBao.Text = string.Empty;
            return true;
        }

        private bool ThuLayMatKhau(out string matKhau)
        {
            matKhau = txtMatKhauTam.Text;
            if (matKhau.Length < 8)
            {
                HienThiLoi("Mật khẩu tạm phải có ít nhất 8 ký tự.");
                txtMatKhauTam.Focus();
                return false;
            }

            if (Encoding.UTF8.GetByteCount(matKhau) > 72)
            {
                HienThiLoi("Mật khẩu tạm không được vượt quá 72 byte UTF-8.");
                txtMatKhauTam.Focus();
                return false;
            }

            if (matKhau != txtXacNhanMatKhau.Text)
            {
                HienThiLoi("Xác nhận mật khẩu không khớp.");
                txtXacNhanMatKhau.Focus();
                return false;
            }

            return true;
        }

        private static bool TenDangNhapDaTonTai(
            QL_CuaHangDaQuy_PNJEntities db,
            string tenDangNhap,
            int? boQuaTaiKhoanId)
        {
            return db.TaiKhoans.Any(tk =>
                tk.TenDangNhap == tenDangNhap &&
                (!boQuaTaiKhoanId.HasValue || tk.TaiKhoanId != boQuaTaiKhoanId.Value));
        }

        private static bool ConQuanTriVienHoatDongKhac(
            QL_CuaHangDaQuy_PNJEntities db,
            int taiKhoanBiLoaiTruId)
        {
            return db.TaiKhoans.Any(tk =>
                tk.TaiKhoanId != taiKhoanBiLoaiTruId &&
                tk.VaiTro == "ADMIN" &&
                tk.DangHoatDong &&
                tk.NhanVien.DangLamViec);
        }

        private void HienThiLoi(string noiDung)
        {
            lblThongBao.Text = "* " + noiDung;
        }

        private sealed class ThongTinTaiKhoanNhap
        {
            public int NhanVienId { get; set; }
            public string TenDangNhap { get; set; }
            public string VaiTro { get; set; }
            public string MatKhauTam { get; set; }
        }

        private sealed class NhanVienLuaChon
        {
            public NhanVienLuaChon(NhanVien nhanVien)
            {
                NhanVienId = nhanVien.NhanVienId;
                NoiDungHienThi = $"NV{nhanVien.NhanVienId:000000} - {nhanVien.HoTen}";
            }

            public int NhanVienId { get; }
            public string NoiDungHienThi { get; }
        }

        private sealed class TaiKhoanHienThi
        {
            public TaiKhoanHienThi(TaiKhoan taiKhoan)
            {
                TaiKhoanId = taiKhoan.TaiKhoanId;
                NhanVienId = taiKhoan.NhanVienId;
                MaTaiKhoan = $"TK{taiKhoan.TaiKhoanId:000000}";
                MaNhanVien = $"NV{taiKhoan.NhanVienId:000000}";
                TenDangNhap = taiKhoan.TenDangNhap;
                HoTen = taiKhoan.NhanVien.HoTen;
                ChucVu = taiKhoan.NhanVien.ChucVu;
                VaiTro = taiKhoan.VaiTro;
                DangHoatDong = taiKhoan.DangHoatDong;
                PhaiDoiMatKhau = taiKhoan.PhaiDoiMatKhau;
                TrangThai = taiKhoan.DangHoatDong ? "Đang hoạt động" : "Đã khóa";
                DoiMatKhau = taiKhoan.PhaiDoiMatKhau ? "Bắt buộc đổi" : "Bình thường";
                TrangThaiNhanVien = taiKhoan.NhanVien.DangLamViec ? "Đang làm việc" : "Đã nghỉ";
            }

            public int TaiKhoanId { get; }
            public int NhanVienId { get; }
            public string MaTaiKhoan { get; }
            public string MaNhanVien { get; }
            public string TenDangNhap { get; }
            public string HoTen { get; }
            public string ChucVu { get; }
            public string VaiTro { get; }
            public bool DangHoatDong { get; }
            public bool PhaiDoiMatKhau { get; }
            public string TrangThai { get; }
            public string DoiMatKhau { get; }
            public string TrangThaiNhanVien { get; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmNhaCungCap : Form
    {
        private int? nhaCungCapDangChonId;
        private bool dangLamMoiBieuMau;

        public FrmNhaCungCap()
        {
            InitializeComponent();
            cboLocTrangThai.SelectedIndex = 0;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmNhaCungCap_Load(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }

            LamMoiBieuMau();
            dangLamMoiBieuMau = true;
            try
            {
                TaiDanhSach();
                dgvNhaCungCap.ClearSelection();
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }
        }

        private bool KiemTraQuyenQuanTri(bool hienThongBao)
        {
            bool coQuyen = CurrentUserSession.DaDangNhap && CurrentUserSession.HienTai.LaQuanTriVien;
            if (!coQuyen && hienThongBao)
            {
                MessageBox.Show(
                    "Chỉ quản trị viên được sử dụng chức năng quản lý nhà cung cấp.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return coQuyen;
        }

        private void TaiDanhSach(int? nhaCungCapCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<NhaCungCap> truyVan = db.NhaCungCaps
                        .Include(ncc => ncc.PhieuNhaps)
                        .AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        truyVan = truyVan.Where(ncc =>
                            ncc.TenNhaCungCap.Contains(tuKhoa) ||
                            ncc.SoDienThoai.Contains(tuKhoa) ||
                            (ncc.NguoiLienHe != null && ncc.NguoiLienHe.Contains(tuKhoa)) ||
                            (ncc.Email != null && ncc.Email.Contains(tuKhoa)) ||
                            (ncc.DiaChi != null && ncc.DiaChi.Contains(tuKhoa)));
                    }

                    if (trangThai == 1)
                    {
                        truyVan = truyVan.Where(ncc => ncc.DangHoatDong);
                    }
                    else if (trangThai == 2)
                    {
                        truyVan = truyVan.Where(ncc => !ncc.DangHoatDong);
                    }

                    List<NhaCungCapHienThi> danhSach = truyVan
                        .OrderByDescending(ncc => ncc.DangHoatDong)
                        .ThenBy(ncc => ncc.NhaCungCapId)
                        .ToList()
                        .Select(ncc => new NhaCungCapHienThi(ncc))
                        .ToList();

                    dgvNhaCungCap.DataSource = danhSach;
                    lblSoKetQua.Text = $"{danhSach.Count} nhà cung cấp";
                }

                if (nhaCungCapCanChonId.HasValue)
                {
                    ChonDong(nhaCungCapCanChonId.Value);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách nhà cung cấp. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void ChonDong(int nhaCungCapId)
        {
            foreach (DataGridViewRow row in dgvNhaCungCap.Rows)
            {
                var item = row.DataBoundItem as NhaCungCapHienThi;
                if (item?.NhaCungCapId != nhaCungCapId)
                {
                    continue;
                }

                row.Selected = true;
                dgvNhaCungCap.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void dgvNhaCungCap_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoiBieuMau)
            {
                return;
            }

            var item = dgvNhaCungCap.CurrentRow?.DataBoundItem as NhaCungCapHienThi;
            if (item == null)
            {
                return;
            }

            nhaCungCapDangChonId = item.NhaCungCapId;
            txtMaNhaCungCap.Text = item.MaNhaCungCap;
            txtTenNhaCungCap.Text = item.TenNhaCungCap;
            txtNguoiLienHe.Text = item.NguoiLienHe ?? string.Empty;
            txtSoDienThoai.Text = item.SoDienThoai;
            txtEmail.Text = item.Email ?? string.Empty;
            txtDiaChi.Text = item.DiaChi ?? string.Empty;
            chkDangHoatDong.Checked = item.DangHoatDong;
            btnXoaHoacTrangThai.Text = item.DangHoatDong
                ? (item.SoPhieuNhap > 0 ? "Ngừng hoạt động" : "Xóa nhà cung cấp")
                : "Khôi phục";
            lblThongBao.Text = string.Empty;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
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
            if (!KiemTraQuyenQuanTri(true)) return;

            ThongTinNhaCungCapNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu)) return;

            try
            {
                int nhaCungCapMoiId;
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (TenNhaCungCapDaTonTai(db, duLieu.TenNhaCungCap, null))
                    {
                        HienThiLoi("Tên nhà cung cấp đã tồn tại.");
                        return;
                    }

                    if (SoDienThoaiDaTonTai(db, duLieu.SoDienThoai, null))
                    {
                        HienThiLoi("Số điện thoại đã được sử dụng bởi nhà cung cấp khác.");
                        return;
                    }

                    var nhaCungCap = new NhaCungCap
                    {
                        TenNhaCungCap = duLieu.TenNhaCungCap,
                        NguoiLienHe = duLieu.NguoiLienHe,
                        SoDienThoai = duLieu.SoDienThoai,
                        Email = duLieu.Email,
                        DiaChi = duLieu.DiaChi,
                        DangHoatDong = true
                    };
                    db.NhaCungCaps.Add(nhaCungCap);
                    db.SaveChanges();
                    nhaCungCapMoiId = nhaCungCap.NhaCungCapId;
                }

                TaiDanhSach(nhaCungCapMoiId);
                MessageBox.Show(
                    $"Đã thêm nhà cung cấp NCC{nhaCungCapMoiId:000000}.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu. Tên hoặc số điện thoại nhà cung cấp có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thêm nhà cung cấp. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !nhaCungCapDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn nhà cung cấp cần cập nhật.");
                return;
            }

            ThongTinNhaCungCapNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu)) return;
            int nhaCungCapId = nhaCungCapDangChonId.Value;

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (TenNhaCungCapDaTonTai(db, duLieu.TenNhaCungCap, nhaCungCapId))
                    {
                        HienThiLoi("Tên nhà cung cấp đã tồn tại.");
                        return;
                    }

                    if (SoDienThoaiDaTonTai(db, duLieu.SoDienThoai, nhaCungCapId))
                    {
                        HienThiLoi("Số điện thoại đã được sử dụng bởi nhà cung cấp khác.");
                        return;
                    }

                    var nhaCungCap = db.NhaCungCaps.SingleOrDefault(ncc => ncc.NhaCungCapId == nhaCungCapId);
                    if (nhaCungCap == null)
                    {
                        HienThiLoi("Nhà cung cấp không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    nhaCungCap.TenNhaCungCap = duLieu.TenNhaCungCap;
                    nhaCungCap.NguoiLienHe = duLieu.NguoiLienHe;
                    nhaCungCap.SoDienThoai = duLieu.SoDienThoai;
                    nhaCungCap.Email = duLieu.Email;
                    nhaCungCap.DiaChi = duLieu.DiaChi;
                    db.SaveChanges();
                }

                TaiDanhSach(nhaCungCapId);
                MessageBox.Show("Đã cập nhật nhà cung cấp.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cập nhật. Tên hoặc số điện thoại có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật nhà cung cấp.");
            }
        }

        private void btnXoaHoacTrangThai_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !nhaCungCapDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn nhà cung cấp cần xử lý.");
                return;
            }

            int nhaCungCapId = nhaCungCapDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var nhaCungCap = db.NhaCungCaps
                        .Include(ncc => ncc.PhieuNhaps)
                        .SingleOrDefault(ncc => ncc.NhaCungCapId == nhaCungCapId);
                    if (nhaCungCap == null)
                    {
                        HienThiLoi("Nhà cung cấp không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    bool coPhieuNhap = nhaCungCap.PhieuNhaps.Any();
                    string hanhDong = !nhaCungCap.DangHoatDong
                        ? "khôi phục"
                        : (coPhieuNhap ? "ngừng hoạt động" : "xóa");
                    if (MessageBox.Show(
                            $"Bạn có chắc muốn {hanhDong} nhà cung cấp {nhaCungCap.TenNhaCungCap}?",
                            "Xác nhận",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    if (!nhaCungCap.DangHoatDong)
                    {
                        nhaCungCap.DangHoatDong = true;
                        db.SaveChanges();
                        TaiDanhSach(nhaCungCapId);
                    }
                    else if (coPhieuNhap)
                    {
                        nhaCungCap.DangHoatDong = false;
                        db.SaveChanges();
                        TaiDanhSach(nhaCungCapId);
                    }
                    else
                    {
                        db.NhaCungCaps.Remove(nhaCungCap);
                        db.SaveChanges();
                        TaiDanhSach();
                        LamMoiBieuMau();
                    }
                }
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Nhà cung cấp đã phát sinh phiếu nhập và không thể xóa. Hãy tải lại rồi ngừng hoạt động.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thay đổi trạng thái nhà cung cấp.");
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
                nhaCungCapDangChonId = null;
                txtMaNhaCungCap.Text = "Tự động tạo";
                txtTenNhaCungCap.Clear();
                txtNguoiLienHe.Clear();
                txtSoDienThoai.Clear();
                txtEmail.Clear();
                txtDiaChi.Clear();
                chkDangHoatDong.Checked = true;
                btnXoaHoacTrangThai.Text = "Xóa nhà cung cấp";
                lblThongBao.Text = string.Empty;
                dgvNhaCungCap.ClearSelection();
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }

            txtTenNhaCungCap.Focus();
        }

        private bool ThuLayDuLieuNhap(out ThongTinNhaCungCapNhap duLieu)
        {
            duLieu = null;
            string tenNhaCungCap = txtTenNhaCungCap.Text.Trim();
            string nguoiLienHe = ChuanHoaTuyChon(txtNguoiLienHe.Text);
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = ChuanHoaTuyChon(txtEmail.Text);
            string diaChi = ChuanHoaTuyChon(txtDiaChi.Text);

            if (string.IsNullOrWhiteSpace(tenNhaCungCap))
            {
                HienThiLoi("Tên nhà cung cấp không được để trống.");
                txtTenNhaCungCap.Focus();
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

            duLieu = new ThongTinNhaCungCapNhap
            {
                TenNhaCungCap = tenNhaCungCap,
                NguoiLienHe = nguoiLienHe,
                SoDienThoai = soDienThoai,
                Email = email,
                DiaChi = diaChi
            };
            lblThongBao.Text = string.Empty;
            return true;
        }

        private static bool TenNhaCungCapDaTonTai(
            QL_CuaHangDaQuy_PNJEntities db,
            string tenNhaCungCap,
            int? boQuaNhaCungCapId)
        {
            return db.NhaCungCaps.Any(ncc =>
                ncc.TenNhaCungCap == tenNhaCungCap &&
                (!boQuaNhaCungCapId.HasValue || ncc.NhaCungCapId != boQuaNhaCungCapId.Value));
        }

        private static bool SoDienThoaiDaTonTai(
            QL_CuaHangDaQuy_PNJEntities db,
            string soDienThoai,
            int? boQuaNhaCungCapId)
        {
            return db.NhaCungCaps.Any(ncc =>
                ncc.SoDienThoai == soDienThoai &&
                (!boQuaNhaCungCapId.HasValue || ncc.NhaCungCapId != boQuaNhaCungCapId.Value));
        }

        private static string ChuanHoaTuyChon(string giaTri)
        {
            string ketQua = (giaTri ?? string.Empty).Trim();
            return ketQua.Length == 0 ? null : ketQua;
        }

        private static bool EmailHopLe(string email)
        {
            if (email.Length > 254) return false;
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

        private sealed class ThongTinNhaCungCapNhap
        {
            public string TenNhaCungCap { get; set; }
            public string NguoiLienHe { get; set; }
            public string SoDienThoai { get; set; }
            public string Email { get; set; }
            public string DiaChi { get; set; }
        }

        private sealed class NhaCungCapHienThi
        {
            public NhaCungCapHienThi(NhaCungCap nhaCungCap)
            {
                NhaCungCapId = nhaCungCap.NhaCungCapId;
                MaNhaCungCap = $"NCC{nhaCungCap.NhaCungCapId:000000}";
                TenNhaCungCap = nhaCungCap.TenNhaCungCap;
                NguoiLienHe = nhaCungCap.NguoiLienHe;
                SoDienThoai = nhaCungCap.SoDienThoai;
                Email = nhaCungCap.Email;
                DiaChi = nhaCungCap.DiaChi;
                SoPhieuNhap = nhaCungCap.PhieuNhaps.Count;
                DangHoatDong = nhaCungCap.DangHoatDong;
                TrangThai = nhaCungCap.DangHoatDong ? "Đang hoạt động" : "Ngừng hoạt động";
            }

            public int NhaCungCapId { get; }
            public string MaNhaCungCap { get; }
            public string TenNhaCungCap { get; }
            public string NguoiLienHe { get; }
            public string SoDienThoai { get; }
            public string Email { get; }
            public string DiaChi { get; }
            public int SoPhieuNhap { get; }
            public bool DangHoatDong { get; }
            public string TrangThai { get; }
        }
    }
}

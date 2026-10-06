using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmDanhMuc : Form
    {
        private int? danhMucDangChonId;
        private bool dangLamMoiBieuMau;

        public FrmDanhMuc()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            cboLocTrangThai.SelectedIndex = 0;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
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
                dgvDanhMuc.ClearSelection();
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
                    "Chỉ quản trị viên được sử dụng chức năng quản lý danh mục.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return coQuyen;
        }

        private void TaiDanhSach(int? danhMucCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<DanhMuc> truyVan = db.DanhMucs
                        .Include(dm => dm.SanPhams)
                        .AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        truyVan = truyVan.Where(dm =>
                            dm.TenDanhMuc.Contains(tuKhoa) ||
                            (dm.MoTa != null && dm.MoTa.Contains(tuKhoa)));
                    }

                    if (trangThai == 1)
                    {
                        truyVan = truyVan.Where(dm => dm.DangHoatDong);
                    }
                    else if (trangThai == 2)
                    {
                        truyVan = truyVan.Where(dm => !dm.DangHoatDong);
                    }

                    List<DanhMucHienThi> danhSach = truyVan
                        .OrderByDescending(dm => dm.DangHoatDong)
                        .ThenBy(dm => dm.DanhMucId)
                        .ToList()
                        .Select(dm => new DanhMucHienThi(dm))
                        .ToList();

                    dgvDanhMuc.DataSource = danhSach;
                    lblSoKetQua.Text = $"{danhSach.Count} danh mục";
                }

                if (danhMucCanChonId.HasValue)
                {
                    ChonDong(danhMucCanChonId.Value);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách danh mục. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void ChonDong(int danhMucId)
        {
            foreach (DataGridViewRow row in dgvDanhMuc.Rows)
            {
                var item = row.DataBoundItem as DanhMucHienThi;
                if (item?.DanhMucId != danhMucId)
                {
                    continue;
                }

                row.Selected = true;
                dgvDanhMuc.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void dgvDanhMuc_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoiBieuMau)
            {
                return;
            }

            if (dgvDanhMuc.SelectedCells.Count == 0 && dgvDanhMuc.SelectedRows.Count == 0)
            {
                btnXoaDanhMuc.Enabled = false;
                return;
            }

            var item = dgvDanhMuc.CurrentRow?.DataBoundItem as DanhMucHienThi;
            if (item == null)
            {
                btnXoaDanhMuc.Enabled = false;
                return;
            }

            danhMucDangChonId = item.DanhMucId;
            txtMaDanhMuc.Text = item.MaDanhMuc;
            txtTenDanhMuc.Text = item.TenDanhMuc;
            txtMoTa.Text = item.MoTa ?? string.Empty;
            chkDangHoatDong.Checked = item.DangHoatDong;
            btnXoaHoacTrangThai.Text = item.DangHoatDong ? "Ngừng hoạt động" : "Khôi phục";
            btnXoaDanhMuc.Enabled = item.SoSanPham == 0;
            lblThongBao.Text = item.SoSanPham > 0
                ? "Chỉ xóa danh mục chưa có sản phẩm; có thể dùng Ngừng hoạt động."
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
            if (!KiemTraQuyenQuanTri(true))
            {
                return;
            }

            string tenDanhMuc;
            string moTa;
            if (!ThuLayDuLieuNhap(out tenDanhMuc, out moTa))
            {
                return;
            }

            try
            {
                int danhMucMoiId;
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (TenDanhMucDaTonTai(db, tenDanhMuc, null))
                    {
                        HienThiLoi("Tên danh mục đã tồn tại.");
                        return;
                    }

                    var danhMuc = new DanhMuc
                    {
                        TenDanhMuc = tenDanhMuc,
                        MoTa = moTa,
                        DangHoatDong = true
                    };
                    db.DanhMucs.Add(danhMuc);
                    db.SaveChanges();
                    danhMucMoiId = danhMuc.DanhMucId;
                }

                TaiDanhSach(danhMucMoiId);
                MessageBox.Show(
                    $"Đã thêm danh mục DM{danhMucMoiId:000000}.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu danh mục. Tên danh mục có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thêm danh mục. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !danhMucDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn danh mục cần cập nhật.");
                return;
            }

            string tenDanhMuc;
            string moTa;
            if (!ThuLayDuLieuNhap(out tenDanhMuc, out moTa))
            {
                return;
            }

            int danhMucId = danhMucDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (TenDanhMucDaTonTai(db, tenDanhMuc, danhMucId))
                    {
                        HienThiLoi("Tên danh mục đã tồn tại.");
                        return;
                    }

                    var danhMuc = db.DanhMucs.SingleOrDefault(dm => dm.DanhMucId == danhMucId);
                    if (danhMuc == null)
                    {
                        HienThiLoi("Danh mục không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    danhMuc.TenDanhMuc = tenDanhMuc;
                    danhMuc.MoTa = moTa;
                    db.SaveChanges();
                }

                TaiDanhSach(danhMucId);
                MessageBox.Show("Đã cập nhật danh mục.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cập nhật. Tên danh mục có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật danh mục.");
            }
        }

        private void btnXoaHoacTrangThai_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !danhMucDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn danh mục cần thay đổi trạng thái.");
                return;
            }

            int danhMucId = danhMucDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var danhMuc = db.DanhMucs.SingleOrDefault(dm => dm.DanhMucId == danhMucId);
                    if (danhMuc == null)
                    {
                        HienThiLoi("Danh mục không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    string hanhDong = danhMuc.DangHoatDong ? "ngừng hoạt động" : "khôi phục";
                    if (MessageBox.Show(
                            $"Bạn có chắc muốn {hanhDong} danh mục {danhMuc.TenDanhMuc}?",
                            "Xác nhận",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    danhMuc.DangHoatDong = !danhMuc.DangHoatDong;
                    db.SaveChanges();
                    TaiDanhSach(danhMucId);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thay đổi trạng thái danh mục.");
            }
        }

        private void btnXoaDanhMuc_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !danhMucDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn danh mục cần xóa.");
                return;
            }

            int danhMucId = danhMucDangChonId.Value;
            if (MessageBox.Show(
                    $"Xóa vĩnh viễn danh mục DM{danhMucId:000000} - {txtTenDanhMuc.Text.Trim()}?",
                    "Xác nhận xóa danh mục",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var danhMuc = db.DanhMucs.SingleOrDefault(dm => dm.DanhMucId == danhMucId);
                    if (danhMuc == null)
                    {
                        TaiDanhSach();
                        LamMoiBieuMau();
                        HienThiLoi("Danh mục không còn tồn tại trong CSDL.");
                        return;
                    }

                    if (db.SanPhams.Any(sp => sp.DanhMucId == danhMucId))
                    {
                        TaiDanhSach(danhMucId);
                        HienThiLoi("Danh mục đã có sản phẩm; hãy dùng Ngừng hoạt động.");
                        return;
                    }

                    db.DanhMucs.Remove(danhMuc);
                    db.SaveChanges();
                }

                TaiDanhSach();
                LamMoiBieuMau();
                MessageBox.Show("Đã xóa danh mục.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                TaiDanhSach(danhMucId);
                HienThiLoi("Danh mục vừa phát sinh sản phẩm; không thể xóa.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể xóa danh mục. Hãy kiểm tra kết nối CSDL.");
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
                danhMucDangChonId = null;
                txtMaDanhMuc.Text = "Tự động tạo";
                txtTenDanhMuc.Clear();
                txtMoTa.Clear();
                chkDangHoatDong.Checked = true;
                btnXoaHoacTrangThai.Text = "Ngừng hoạt động";
                btnXoaDanhMuc.Enabled = false;
                lblThongBao.Text = string.Empty;
                dgvDanhMuc.ClearSelection();
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }

            txtTenDanhMuc.Focus();
        }

        private bool ThuLayDuLieuNhap(out string tenDanhMuc, out string moTa)
        {
            tenDanhMuc = txtTenDanhMuc.Text.Trim();
            moTa = ChuanHoaTuyChon(txtMoTa.Text);
            if (string.IsNullOrWhiteSpace(tenDanhMuc))
            {
                HienThiLoi("Tên danh mục không được để trống.");
                txtTenDanhMuc.Focus();
                return false;
            }

            lblThongBao.Text = string.Empty;
            return true;
        }

        private static bool TenDanhMucDaTonTai(
            QL_CuaHangDaQuy_PNJEntities db,
            string tenDanhMuc,
            int? boQuaDanhMucId)
        {
            return db.DanhMucs.Any(dm =>
                dm.TenDanhMuc == tenDanhMuc &&
                (!boQuaDanhMucId.HasValue || dm.DanhMucId != boQuaDanhMucId.Value));
        }

        private static string ChuanHoaTuyChon(string giaTri)
        {
            string ketQua = (giaTri ?? string.Empty).Trim();
            return ketQua.Length == 0 ? null : ketQua;
        }

        private void HienThiLoi(string noiDung)
        {
            lblThongBao.Text = "* " + noiDung;
        }

        private sealed class DanhMucHienThi
        {
            public DanhMucHienThi(DanhMuc danhMuc)
            {
                DanhMucId = danhMuc.DanhMucId;
                MaDanhMuc = $"DM{danhMuc.DanhMucId:000000}";
                TenDanhMuc = danhMuc.TenDanhMuc;
                MoTa = danhMuc.MoTa;
                SoSanPham = danhMuc.SanPhams.Count;
                DangHoatDong = danhMuc.DangHoatDong;
                TrangThai = danhMuc.DangHoatDong ? "Đang hoạt động" : "Ngừng hoạt động";
            }

            public int DanhMucId { get; }
            public string MaDanhMuc { get; }
            public string TenDanhMuc { get; }
            public string MoTa { get; }
            public int SoSanPham { get; }
            public bool DangHoatDong { get; }
            public string TrangThai { get; }
        }
    }
}

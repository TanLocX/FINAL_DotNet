using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmBaoHanh : Form
    {
        private readonly List<SanPhamDaBan> tatCaSanPhamDaBan = new List<SanPhamDaBan>();
        private int? phieuBaoHanhDangChonId;
        private string trangThaiBanDau;
        private bool dangLamMoi;

        public FrmBaoHanh()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            cboLocTrangThai.SelectedIndex = 0;
            cboLocHanBaoHanh.SelectedIndex = 0;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmBaoHanh_Load(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }
            dangLamMoi = true;
            try
            {
                bool sanSang = TaiSanPhamDaBan();
                LamMoiTiepNhanNoiBo();
                if (sanSang)
                    TaiDanhSachPhieu();
                else
                    lblSoKetQua.Text = "0 phiếu bảo hành";
                dgvPhieuBaoHanh.ClearSelection();
                splitChinh.SplitterDistance = Math.Max(
                    splitChinh.Panel1MinSize,
                    Math.Min(205, splitChinh.Height / 2));
            }
            finally
            {
                dangLamMoi = false;
            }
        }

        private bool KiemTraPhienDangNhap(bool hienThongBao)
        {
            bool hopLe = CurrentUserSession.DaDangNhap;
            if (!hopLe && hienThongBao)
            {
                MessageBox.Show("Phiên đăng nhập đã kết thúc. Vui lòng đăng nhập lại.", "Chưa đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return hopLe;
        }

        private bool TaiSanPhamDaBan()
        {
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var danhSach = db.ChiTietHoaDons
                        .Include(ct => ct.HoaDon.KhachHang)
                        .Include(ct => ct.SanPham)
                        .Include(ct => ct.PhieuBaoHanhs)
                        .AsNoTracking()
                        .Where(ct => ct.HoaDon.TrangThai == "DA_THANH_TOAN")
                        .OrderByDescending(ct => ct.HoaDon.NgayLap)
                        .ThenByDescending(ct => ct.ChiTietHoaDonId)
                        .ToList()
                        .Select(ct => new SanPhamDaBan(ct))
                        .ToList();
                    tatCaSanPhamDaBan.Clear();
                    tatCaSanPhamDaBan.AddRange(danhSach);
                }
                LocSanPhamDaBan();
                return true;
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải sản phẩm đã bán. Hãy kiểm tra kết nối CSDL.");
                return false;
            }
        }

        private void LocSanPhamDaBan()
        {
            string tuKhoa = txtTimSanPhamDaBan.Text.Trim();
            IEnumerable<SanPhamDaBan> danhSach = tatCaSanPhamDaBan;
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                danhSach = danhSach.Where(item =>
                    item.MaHoaDon.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    item.TenKhachHang.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    item.SoDienThoai.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    item.TenSanPham.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            cboSanPhamDaBan.DataSource = danhSach.ToList();
            if (cboSanPhamDaBan.Items.Count == 0) XoaThongTinSanPhamDaBan();
        }

        private void btnTimSanPhamDaBan_Click(object sender, EventArgs e) => LocSanPhamDaBan();

        private void txtTimSanPhamDaBan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            LocSanPhamDaBan();
        }

        private void cboSanPhamDaBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            HienThiSanPhamDaBan(cboSanPhamDaBan.SelectedItem as SanPhamDaBan);
        }

        private void HienThiSanPhamDaBan(SanPhamDaBan item)
        {
            if (item == null)
            {
                XoaThongTinSanPhamDaBan();
                return;
            }
            lblKhachHangTiepNhan.Text = item.TenKhachHang + " - " + item.SoDienThoai;
            lblHoaDonTiepNhan.Text = item.MaHoaDon + " ngày " + item.NgayLap.ToString("dd/MM/yyyy");
            lblSanPhamTiepNhan.Text = item.MaSanPham + " - " + item.TenSanPham;
            lblHanBaoHanhTiepNhan.Text = item.ThongTinHanBaoHanh;
            lblHanBaoHanhTiepNhan.ForeColor = item.ConHanBaoHanh ? Color.FromArgb(35, 125, 96) : Color.Firebrick;
            lblSoLanBaoHanh.Text = item.SoLanBaoHanh + " lần bảo hành trước";
        }

        private void XoaThongTinSanPhamDaBan()
        {
            lblKhachHangTiepNhan.Text = "--";
            lblHoaDonTiepNhan.Text = "--";
            lblSanPhamTiepNhan.Text = "--";
            lblHanBaoHanhTiepNhan.Text = "--";
            lblSoLanBaoHanh.Text = "0 lần bảo hành trước";
        }

        private void TaiDanhSachPhieu(int? phieuCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int? maPhieu = ThuDocMaPhieu(tuKhoa);
                int? maHoaDon = ThuDocMaHoaDon(tuKhoa);
                int trangThai = cboLocTrangThai.SelectedIndex;
                int hanBaoHanh = cboLocHanBaoHanh.SelectedIndex;
                DateTime homNay = DateTime.Today;
                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<PhieuBaoHanh> truyVan = db.PhieuBaoHanhs
                        .Include(pbh => pbh.ChiTietHoaDon.HoaDon.KhachHang)
                        .Include(pbh => pbh.ChiTietHoaDon.SanPham)
                        .AsNoTracking();
                    if (dtpTuNgay.Checked)
                    {
                        DateTime tuNgay = dtpTuNgay.Value.Date;
                        truyVan = truyVan.Where(pbh => pbh.NgayTiepNhan >= tuNgay);
                    }
                    if (dtpDenNgay.Checked)
                    {
                        DateTime denNgayKeTiep = dtpDenNgay.Value.Date.AddDays(1);
                        truyVan = truyVan.Where(pbh => pbh.NgayTiepNhan < denNgayKeTiep);
                    }
                    string maTrangThai = LayMaTrangThaiLoc(trangThai);
                    if (maTrangThai != null) truyVan = truyVan.Where(pbh => pbh.TrangThai == maTrangThai);
                    if (hanBaoHanh == 1)
                        truyVan = truyVan.Where(pbh => pbh.ChiTietHoaDon.HanBaoHanh.HasValue && pbh.ChiTietHoaDon.HanBaoHanh.Value >= homNay);
                    else if (hanBaoHanh == 2)
                        truyVan = truyVan.Where(pbh => pbh.ChiTietHoaDon.HanBaoHanh.HasValue && pbh.ChiTietHoaDon.HanBaoHanh.Value < homNay);
                    else if (hanBaoHanh == 3)
                        truyVan = truyVan.Where(pbh => !pbh.ChiTietHoaDon.HanBaoHanh.HasValue);
                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        if (maPhieu.HasValue)
                            truyVan = truyVan.Where(pbh => pbh.PhieuBaoHanhId == maPhieu.Value);
                        else if (maHoaDon.HasValue)
                            truyVan = truyVan.Where(pbh => pbh.ChiTietHoaDon.HoaDonId == maHoaDon.Value);
                        else
                            truyVan = truyVan.Where(pbh =>
                                pbh.ChiTietHoaDon.HoaDon.KhachHang.HoTen.Contains(tuKhoa) ||
                                pbh.ChiTietHoaDon.HoaDon.KhachHang.SoDienThoai.Contains(tuKhoa) ||
                                pbh.ChiTietHoaDon.SanPham.TenSanPham.Contains(tuKhoa));
                    }
                    List<PhieuBaoHanhHienThi> danhSach = truyVan
                        .OrderByDescending(pbh => pbh.NgayTiepNhan)
                        .ThenByDescending(pbh => pbh.PhieuBaoHanhId)
                        .ToList()
                        .Select(pbh => new PhieuBaoHanhHienThi(pbh))
                        .ToList();
                    dgvPhieuBaoHanh.DataSource = danhSach;
                    lblSoKetQua.Text = danhSach.Count + " phiếu bảo hành";
                }
                if (phieuCanChonId.HasValue) ChonDongPhieu(phieuCanChonId.Value);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách bảo hành. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private static string LayMaTrangThaiLoc(int index)
        {
            switch (index)
            {
                case 1: return "TIEP_NHAN";
                case 2: return "DANG_XU_LY";
                case 3: return "HOAN_THANH";
                case 4: return "DA_TRA";
                default: return null;
            }
        }

        private static int? ThuDocMaPhieu(string giaTri)
        {
            if (string.IsNullOrWhiteSpace(giaTri) || !giaTri.Trim().StartsWith("PBH", StringComparison.OrdinalIgnoreCase))
                return null;
            int id;
            return int.TryParse(giaTri.Trim().Substring(3), out id) && id > 0 ? (int?)id : null;
        }

        private static int? ThuDocMaHoaDon(string giaTri)
        {
            if (string.IsNullOrWhiteSpace(giaTri) || !giaTri.Trim().StartsWith("HD", StringComparison.OrdinalIgnoreCase))
                return null;
            int id;
            return int.TryParse(giaTri.Trim().Substring(2), out id) && id > 0 ? (int?)id : null;
        }

        private void ChonDongPhieu(int id)
        {
            foreach (DataGridViewRow row in dgvPhieuBaoHanh.Rows)
            {
                var item = row.DataBoundItem as PhieuBaoHanhHienThi;
                if (item?.PhieuBaoHanhId != id) continue;
                row.Selected = true;
                dgvPhieuBaoHanh.CurrentCell = row.Cells[0];
                return;
            }
        }

        private void dgvPhieuBaoHanh_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            var item = dgvPhieuBaoHanh.CurrentRow?.DataBoundItem as PhieuBaoHanhHienThi;
            if (item == null) return;
            phieuBaoHanhDangChonId = item.PhieuBaoHanhId;
            trangThaiBanDau = item.TrangThai;
            lblMaPhieuXuLy.Text = item.MaPhieuBaoHanh;
            lblKhachHangXuLy.Text = item.TenKhachHang + " - " + item.SoDienThoai;
            lblSanPhamXuLy.Text = item.MaHoaDon + " / " + item.TenSanPham;
            lblNgayTiepNhanXuLy.Text = item.NgayTiepNhan.ToString("dd/MM/yyyy HH:mm");
            lblHanBaoHanhXuLy.Text = item.ThongTinHanBaoHanh;
            txtNoiDungXuLy.Text = item.NoiDungBaoHanh;
            txtGhiChuXuLy.Text = item.GhiChu ?? string.Empty;
            dtpNgayTraDuKienXuLy.Checked = item.NgayTraDuKien.HasValue;
            if (item.NgayTraDuKien.HasValue) dtpNgayTraDuKienXuLy.Value = item.NgayTraDuKien.Value;
            dtpNgayTraThucTe.Checked = item.NgayTraThucTe.HasValue;
            if (item.NgayTraThucTe.HasValue) dtpNgayTraThucTe.Value = item.NgayTraThucTe.Value;
            TaiLuaChonTrangThai(item.TrangThai);
            btnTiepNhan.Enabled = false;
            btnCapNhat.Enabled = true;
            btnXemBaoCao.Enabled = true;
            tabBaoHanh.SelectedTab = tabXuLy;
            lblThongBao.Text = string.Empty;
        }

        private void TaiLuaChonTrangThai(string trangThai)
        {
            cboTrangThaiXuLy.Items.Clear();
            cboTrangThaiXuLy.Items.Add(new LuaChonTrangThai(trangThai));
            string tiepTheo = TrangThaiTiepTheo(trangThai);
            if (tiepTheo != null) cboTrangThaiXuLy.Items.Add(new LuaChonTrangThai(tiepTheo));
            cboTrangThaiXuLy.SelectedIndex = 0;
            CapNhatQuyenNhapNgayTra();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !phieuBaoHanhDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn phiếu bảo hành cần xem báo cáo.");
                return;
            }
            try
            {
                CauHinhBaoCao cauHinh = BaoCaoService.TaoPhieuBaoHanh(phieuBaoHanhDangChonId.Value);
                using (var xemTruoc = new FrmXemBaoCao(cauHinh)) xemTruoc.ShowDialog(this);
            }
            catch (InvalidOperationException ex)
            {
                HienThiLoi(ex.Message);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tạo phiếu tiếp nhận bảo hành. Hãy kiểm tra kết nối CSDL và cấu hình ReportViewer.");
            }
        }

        private static string TrangThaiTiepTheo(string trangThai)
        {
            switch (trangThai)
            {
                case "TIEP_NHAN": return "DANG_XU_LY";
                case "DANG_XU_LY": return "HOAN_THANH";
                case "HOAN_THANH": return "DA_TRA";
                default: return null;
            }
        }

        private void cboTrangThaiXuLy_SelectedIndexChanged(object sender, EventArgs e) => CapNhatQuyenNhapNgayTra();

        private void CapNhatQuyenNhapNgayTra()
        {
            var luaChon = cboTrangThaiXuLy.SelectedItem as LuaChonTrangThai;
            bool choNhap = luaChon != null && (luaChon.Ma == "HOAN_THANH" || luaChon.Ma == "DA_TRA");
            dtpNgayTraThucTe.Enabled = choNhap;
            if (!choNhap) dtpNgayTraThucTe.Checked = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Checked && dtpDenNgay.Checked && dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                HienThiLoi("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }
            TaiDanhSachPhieu();
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            btnTimKiem_Click(sender, EventArgs.Empty);
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            dtpTuNgay.Checked = false;
            dtpDenNgay.Checked = false;
            cboLocTrangThai.SelectedIndex = 0;
            cboLocHanBaoHanh.SelectedIndex = 0;
            TaiSanPhamDaBan();
            TaiDanhSachPhieu();
        }

        private void btnTiepNhan_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            var sanPham = cboSanPhamDaBan.SelectedItem as SanPhamDaBan;
            if (sanPham == null)
            {
                HienThiLoi("Vui lòng chọn sản phẩm đã bán cần bảo hành.");
                return;
            }
            string noiDung = txtNoiDungTiepNhan.Text.Trim();
            if (string.IsNullOrWhiteSpace(noiDung))
            {
                HienThiLoi("Nội dung bảo hành không được để trống.");
                return;
            }
            DateTime ngayTiepNhan = DateTime.Now;
            DateTime? ngayTraDuKien = dtpNgayTraDuKien.Checked ? (DateTime?)dtpNgayTraDuKien.Value.Date : null;
            if (ngayTraDuKien.HasValue && ngayTraDuKien.Value < ngayTiepNhan.Date)
            {
                HienThiLoi("Ngày trả dự kiến không được nhỏ hơn ngày tiếp nhận.");
                return;
            }
            if (!sanPham.ConHanBaoHanh && MessageBox.Show(
                sanPham.ThongTinHanBaoHanh + ". Bạn vẫn muốn tiếp nhận sản phẩm này?",
                "Sản phẩm ngoài hạn bảo hành", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                int idMoi;
                using (var db = DatabaseConnection.CreateContext())
                {
                    bool hopLe = db.ChiTietHoaDons.Any(ct =>
                        ct.ChiTietHoaDonId == sanPham.ChiTietHoaDonId &&
                        ct.HoaDon.TrangThai == "DA_THANH_TOAN");
                    if (!hopLe)
                    {
                        HienThiLoi("Hóa đơn đã bị hủy hoặc sản phẩm không còn hợp lệ. Hãy tải lại.");
                        return;
                    }
                    var phieu = new PhieuBaoHanh
                    {
                        ChiTietHoaDonId = sanPham.ChiTietHoaDonId,
                        NgayTiepNhan = ngayTiepNhan,
                        NoiDungBaoHanh = noiDung,
                        TrangThai = "TIEP_NHAN",
                        NgayTraDuKien = ngayTraDuKien,
                        NgayTraThucTe = null,
                        GhiChu = ChuanHoaTuyChon(txtGhiChuTiepNhan.Text)
                    };
                    db.PhieuBaoHanhs.Add(phieu);
                    db.SaveChanges();
                    idMoi = phieu.PhieuBaoHanhId;
                }
                TaiSanPhamDaBan();
                TaiDanhSachPhieu(idMoi);
                MessageBox.Show($"Đã tiếp nhận phiếu bảo hành PBH{idMoi:000000}.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu phiếu bảo hành vì dữ liệu đã thay đổi.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tiếp nhận bảo hành. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !phieuBaoHanhDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn phiếu bảo hành cần cập nhật.");
                return;
            }
            var luaChon = cboTrangThaiXuLy.SelectedItem as LuaChonTrangThai;
            if (luaChon == null)
            {
                HienThiLoi("Trạng thái xử lý không hợp lệ.");
                return;
            }
            string trangThaiMoi = luaChon.Ma;
            if (trangThaiMoi != trangThaiBanDau && trangThaiMoi != TrangThaiTiepTheo(trangThaiBanDau))
            {
                HienThiLoi("Chỉ được chuyển phiếu sang trạng thái kế tiếp.");
                return;
            }
            DateTime? ngayTraDuKien = dtpNgayTraDuKienXuLy.Checked ? (DateTime?)dtpNgayTraDuKienXuLy.Value.Date : null;
            DateTime? ngayTraThucTe = dtpNgayTraThucTe.Checked ? (DateTime?)dtpNgayTraThucTe.Value : null;
            if ((trangThaiMoi == "TIEP_NHAN" || trangThaiMoi == "DANG_XU_LY") && ngayTraThucTe.HasValue)
            {
                HienThiLoi("Chỉ nhập ngày trả thực tế khi phiếu đã hoàn thành hoặc đã trả.");
                return;
            }
            if (trangThaiMoi == "DA_TRA" && !ngayTraThucTe.HasValue)
            {
                HienThiLoi("Phiếu đã trả phải có ngày trả thực tế.");
                return;
            }
            int id = phieuBaoHanhDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var phieu = db.PhieuBaoHanhs.SingleOrDefault(pbh => pbh.PhieuBaoHanhId == id);
                    if (phieu == null)
                    {
                        HienThiLoi("Phiếu bảo hành không còn tồn tại trong CSDL.");
                        return;
                    }
                    if (phieu.TrangThai != trangThaiBanDau)
                    {
                        HienThiLoi("Trạng thái phiếu vừa được thay đổi. Hãy tải lại.");
                        return;
                    }
                    if (ngayTraDuKien.HasValue && ngayTraDuKien.Value < phieu.NgayTiepNhan.Date)
                    {
                        HienThiLoi("Ngày trả dự kiến không được nhỏ hơn ngày tiếp nhận.");
                        return;
                    }
                    if (ngayTraThucTe.HasValue && ngayTraThucTe.Value < phieu.NgayTiepNhan)
                    {
                        HienThiLoi("Ngày trả thực tế không được nhỏ hơn ngày tiếp nhận.");
                        return;
                    }
                    phieu.TrangThai = trangThaiMoi;
                    phieu.NgayTraDuKien = ngayTraDuKien;
                    phieu.NgayTraThucTe = ngayTraThucTe;
                    phieu.GhiChu = ChuanHoaTuyChon(txtGhiChuXuLy.Text);
                    db.SaveChanges();
                }
                TaiDanhSachPhieu(id);
                MessageBox.Show("Đã cập nhật phiếu bảo hành.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cập nhật phiếu bảo hành vì dữ liệu đã thay đổi.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật phiếu bảo hành. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoiTiepNhan();

        private void LamMoiTiepNhan()
        {
            dangLamMoi = true;
            try { LamMoiTiepNhanNoiBo(); }
            finally { dangLamMoi = false; }
        }

        private void LamMoiTiepNhanNoiBo()
        {
            phieuBaoHanhDangChonId = null;
            trangThaiBanDau = null;
            txtTimSanPhamDaBan.Clear();
            LocSanPhamDaBan();
            if (cboSanPhamDaBan.Items.Count > 0) cboSanPhamDaBan.SelectedIndex = 0;
            HienThiSanPhamDaBan(cboSanPhamDaBan.SelectedItem as SanPhamDaBan);
            txtNoiDungTiepNhan.Clear();
            txtGhiChuTiepNhan.Clear();
            dtpNgayTraDuKien.Checked = true;
            dtpNgayTraDuKien.Value = DateTime.Today.AddDays(7);
            dgvPhieuBaoHanh.ClearSelection();
            btnTiepNhan.Enabled = true;
            btnCapNhat.Enabled = false;
            btnXemBaoCao.Enabled = false;
            tabBaoHanh.SelectedTab = tabTiepNhan;
            lblThongBao.Text = string.Empty;
        }

        private static string ChuanHoaTuyChon(string giaTri)
        {
            string ketQua = (giaTri ?? string.Empty).Trim();
            return ketQua.Length == 0 ? null : ketQua;
        }

        private void HienThiLoi(string noiDung) => lblThongBao.Text = "* " + noiDung;

        private sealed class SanPhamDaBan
        {
            public SanPhamDaBan(ChiTietHoaDon chiTiet)
            {
                ChiTietHoaDonId = chiTiet.ChiTietHoaDonId;
                MaHoaDon = $"HD{chiTiet.HoaDonId:000000}";
                NgayLap = chiTiet.HoaDon.NgayLap;
                TenKhachHang = chiTiet.HoaDon.KhachHang.HoTen;
                SoDienThoai = chiTiet.HoaDon.KhachHang.SoDienThoai;
                MaSanPham = $"SP{chiTiet.SanPhamId:000000}";
                TenSanPham = chiTiet.SanPham.TenSanPham;
                HanBaoHanh = chiTiet.HanBaoHanh;
                SoLanBaoHanh = chiTiet.PhieuBaoHanhs.Count;
            }
            public int ChiTietHoaDonId { get; }
            public string MaHoaDon { get; }
            public DateTime NgayLap { get; }
            public string TenKhachHang { get; }
            public string SoDienThoai { get; }
            public string MaSanPham { get; }
            public string TenSanPham { get; }
            public DateTime? HanBaoHanh { get; }
            public int SoLanBaoHanh { get; }
            public bool ConHanBaoHanh => HanBaoHanh.HasValue && HanBaoHanh.Value.Date >= DateTime.Today;
            public string ThongTinHanBaoHanh => !HanBaoHanh.HasValue
                ? "Không có thông tin hạn bảo hành"
                : ConHanBaoHanh
                    ? "Còn hạn đến " + HanBaoHanh.Value.ToString("dd/MM/yyyy")
                    : "Đã hết hạn ngày " + HanBaoHanh.Value.ToString("dd/MM/yyyy");
            public override string ToString() => $"{MaHoaDon} - {TenKhachHang} - {TenSanPham}";
        }

        private sealed class LuaChonTrangThai
        {
            public LuaChonTrangThai(string ma) { Ma = ma; }
            public string Ma { get; }
            public override string ToString() => TenTrangThai(Ma);
        }

        private sealed class PhieuBaoHanhHienThi
        {
            public PhieuBaoHanhHienThi(PhieuBaoHanh phieu)
            {
                PhieuBaoHanhId = phieu.PhieuBaoHanhId;
                MaPhieuBaoHanh = $"PBH{phieu.PhieuBaoHanhId:000000}";
                NgayTiepNhan = phieu.NgayTiepNhan;
                NgayTiepNhanHienThi = phieu.NgayTiepNhan.ToString("dd/MM/yyyy HH:mm");
                MaHoaDon = $"HD{phieu.ChiTietHoaDon.HoaDonId:000000}";
                TenKhachHang = phieu.ChiTietHoaDon.HoaDon.KhachHang.HoTen;
                SoDienThoai = phieu.ChiTietHoaDon.HoaDon.KhachHang.SoDienThoai;
                TenSanPham = phieu.ChiTietHoaDon.SanPham.TenSanPham;
                HanBaoHanh = phieu.ChiTietHoaDon.HanBaoHanh;
                TrangThai = phieu.TrangThai;
                TrangThaiHienThi = TenTrangThai(phieu.TrangThai);
                NoiDungBaoHanh = phieu.NoiDungBaoHanh;
                NgayTraDuKien = phieu.NgayTraDuKien;
                NgayTraThucTe = phieu.NgayTraThucTe;
                GhiChu = phieu.GhiChu;
            }
            public int PhieuBaoHanhId { get; }
            public string MaPhieuBaoHanh { get; }
            public DateTime NgayTiepNhan { get; }
            public string NgayTiepNhanHienThi { get; }
            public string MaHoaDon { get; }
            public string TenKhachHang { get; }
            public string SoDienThoai { get; }
            public string TenSanPham { get; }
            public DateTime? HanBaoHanh { get; }
            public string ThongTinHanBaoHanh => !HanBaoHanh.HasValue ? "Không có hạn"
                : HanBaoHanh.Value.Date >= DateTime.Today ? "Còn hạn" : "Hết hạn";
            public string TrangThai { get; }
            public string TrangThaiHienThi { get; }
            public string NoiDungBaoHanh { get; }
            public DateTime? NgayTraDuKien { get; }
            public DateTime? NgayTraThucTe { get; }
            public string GhiChu { get; }
        }

        private static string TenTrangThai(string ma)
        {
            switch (ma)
            {
                case "TIEP_NHAN": return "Tiếp nhận";
                case "DANG_XU_LY": return "Đang xử lý";
                case "HOAN_THANH": return "Hoàn thành";
                case "DA_TRA": return "Đã trả";
                default: return ma;
            }
        }
    }
}

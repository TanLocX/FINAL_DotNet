using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace FINAL_DotNet
{
    public partial class FrmHoaDon : Form
    {
        private int? hoaDonDangChonId;
        private HoaDonHienThi hoaDonDangChon;
        private bool dangLamMoi;

        public FrmHoaDon()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            KhoiTaoGiaoDienTuyBien();
            cboLocTrangThai.SelectedIndex = 0;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void KhoiTaoGiaoDienTuyBien()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(27, 39, 53),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            headerStyle.SelectionBackColor = headerStyle.BackColor;

            CauHinhLuoi(dgvHoaDon, headerStyle);
            if (dgvHoaDon.Columns.Count == 0)
            {
                dgvHoaDon.Columns.Add(TaoCot("Mã HĐ", "MaHoaDon", 82));
                dgvHoaDon.Columns.Add(TaoCot("Ngày lập", "NgayLapHienThi", 128));
                dgvHoaDon.Columns.Add(TaoCot("Khách hàng", "TenKhachHang", 175));
                dgvHoaDon.Columns.Add(TaoCot("Nhân viên", "TenNhanVien", 140));
                DataGridViewTextBoxColumn cotTongHoaDon = TaoCot("Phải trả", "ThanhTien", 120);
                cotTongHoaDon.DefaultCellStyle.Format = "N0";
                cotTongHoaDon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvHoaDon.Columns.Add(cotTongHoaDon);
                dgvHoaDon.Columns.Add(TaoCot("Thanh toán", "PhuongThucThanhToan", 110));
                dgvHoaDon.Columns.Add(TaoCot("Trạng thái", "TrangThaiHienThi", 112));
            }

            CauHinhLuoi(dgvChiTietHoaDon, headerStyle);
            if (dgvChiTietHoaDon.Columns.Count == 0)
            {
                dgvChiTietHoaDon.Columns.Add(TaoCot("Mã SP", "MaSanPham", 85));
                dgvChiTietHoaDon.Columns.Add(TaoCot("Tên sản phẩm", "TenSanPham", 350));
                dgvChiTietHoaDon.Columns.Add(TaoCot("Số lượng", "SoLuong", 80));
                DataGridViewTextBoxColumn cotGiaChiTiet = TaoCot("Đơn giá", "DonGiaBan", 130);
                cotGiaChiTiet.DefaultCellStyle.Format = "N0";
                dgvChiTietHoaDon.Columns.Add(cotGiaChiTiet);
                DataGridViewTextBoxColumn cotTienChiTiet = TaoCot("Thành tiền", "ThanhTien", 150);
                cotTienChiTiet.DefaultCellStyle.Format = "N0";
                dgvChiTietHoaDon.Columns.Add(cotTienChiTiet);
                dgvChiTietHoaDon.Columns.Add(TaoCot("Bảo hành đến", "HanBaoHanhHienThi", 120));
            }
        }

        private static void CauHinhLuoi(Guna2DataGridView grid, DataGridViewCellStyle headerStyle)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoGenerateColumns = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle = headerStyle;
            grid.ColumnHeadersHeight = 32;
            grid.Dock = DockStyle.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 28;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(27, 39, 53);
            grid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(214, 182, 116);
            grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(27, 39, 53);
        }

        private static DataGridViewTextBoxColumn TaoCot(string tieuDe, string thuocTinh, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                HeaderText = tieuDe,
                DataPropertyName = thuocTinh,
                Width = width,
                ReadOnly = true
            };
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }

            dangLamMoi = true;
            try
            {
                bool sanSang = TaiDuLieuLuaChon();
                if (sanSang)
                    TaiDanhSachHoaDon();
                else
                    lblSoKetQua.Text = "0 hóa đơn";
                dgvHoaDon.ClearSelection();
                dgvChiTietHoaDon.DataSource = null;
                btnHuyHoaDon.Enabled = false;
                btnInHoaDon.Enabled = false;
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
                MessageBox.Show(
                    "Phiên đăng nhập đã kết thúc. Vui lòng đăng nhập lại.",
                    "Chưa đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            return hopLe;
        }

        private bool TaiDuLieuLuaChon()
        {
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var khachHang = db.KhachHangs.AsNoTracking()
                        .OrderBy(kh => kh.HoTen)
                        .Select(kh => new LuaChonKhachHang
                        {
                            Id = kh.KhachHangId,
                            Ten = kh.HoTen,
                            SoDienThoai = kh.SoDienThoai,
                            DangHoatDong = kh.DangHoatDong
                        }).ToList()
                        .OrderByDescending(kh => kh.Ten == "Khách lẻ")
                        .ThenBy(kh => kh.Ten)
                        .ToList();

                    var khachHangLoc = khachHang.Select(kh => kh.SaoChep()).ToList();
                    khachHangLoc.Insert(0, new LuaChonKhachHang { Id = null, Ten = "Tất cả khách hàng", DangHoatDong = true });
                    cboLocKhachHang.DataSource = khachHangLoc;
                }
                return true;
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách khách hàng. Hãy kiểm tra kết nối CSDL.");
                return false;
            }
        }

        private void TaiDanhSachHoaDon(int? hoaDonCanChonId = null)
        {
            decimal? tienTu;
            decimal? tienDen;
            if (!ThuLayKhoangTien(out tienTu, out tienDen)) return;

            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int? maHoaDon = ThuDocMaHoaDon(tuKhoa);
                int? khachHangId = (cboLocKhachHang.SelectedItem as LuaChonKhachHang)?.Id;
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<HoaDon> truyVan = db.HoaDons
                        .Include(hd => hd.KhachHang)
                        .Include(hd => hd.NhanVien)
                        .Include(hd => hd.ChiTietHoaDons.Select(ct => ct.SanPham))
                        .AsNoTracking();
                    if (dtpTuNgay.Checked)
                    {
                        DateTime tuNgay = dtpTuNgay.Value.Date;
                        truyVan = truyVan.Where(hd => hd.NgayLap >= tuNgay);
                    }
                    if (dtpDenNgay.Checked)
                    {
                        DateTime denNgayKeTiep = dtpDenNgay.Value.Date.AddDays(1);
                        truyVan = truyVan.Where(hd => hd.NgayLap < denNgayKeTiep);
                    }
                    if (khachHangId.HasValue)
                        truyVan = truyVan.Where(hd => hd.KhachHangId == khachHangId.Value);
                    if (trangThai == 1)
                        truyVan = truyVan.Where(hd => hd.TrangThai == "DA_THANH_TOAN");
                    else if (trangThai == 2)
                        truyVan = truyVan.Where(hd => hd.TrangThai == "DA_HUY");
                    if (tienTu.HasValue) truyVan = truyVan.Where(hd => hd.ThanhTien >= tienTu.Value);
                    if (tienDen.HasValue) truyVan = truyVan.Where(hd => hd.ThanhTien <= tienDen.Value);
                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        if (maHoaDon.HasValue)
                            truyVan = truyVan.Where(hd => hd.HoaDonId == maHoaDon.Value);
                        else
                            truyVan = truyVan.Where(hd =>
                                hd.KhachHang.HoTen.Contains(tuKhoa) ||
                                hd.NhanVien.HoTen.Contains(tuKhoa) ||
                                hd.ChiTietHoaDons.Any(ct => ct.SanPham.TenSanPham.Contains(tuKhoa)));
                    }

                    List<HoaDonHienThi> danhSach = truyVan
                        .OrderByDescending(hd => hd.NgayLap)
                        .ThenByDescending(hd => hd.HoaDonId)
                        .ToList()
                        .Select(hd => new HoaDonHienThi(hd))
                        .ToList();
                    dgvHoaDon.DataSource = danhSach;
                    lblSoKetQua.Text = danhSach.Count + " hóa đơn";
                }
                if (hoaDonCanChonId.HasValue) ChonDongHoaDon(hoaDonCanChonId.Value);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải lịch sử hóa đơn. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private bool ThuLayKhoangTien(out decimal? tienTu, out decimal? tienDen)
        {
            tienTu = null;
            tienDen = null;
            decimal giaTri;
            if (!string.IsNullOrWhiteSpace(txtTienTu.Text))
            {
                if (!decimal.TryParse(txtTienTu.Text.Trim(), out giaTri) || giaTri < 0)
                {
                    HienThiLoi("Số tiền từ phải là số không âm.");
                    return false;
                }
                tienTu = giaTri;
            }
            if (!string.IsNullOrWhiteSpace(txtTienDen.Text))
            {
                if (!decimal.TryParse(txtTienDen.Text.Trim(), out giaTri) || giaTri < 0)
                {
                    HienThiLoi("Số tiền đến phải là số không âm.");
                    return false;
                }
                tienDen = giaTri;
            }
            if (tienTu.HasValue && tienDen.HasValue && tienTu.Value > tienDen.Value)
            {
                HienThiLoi("Số tiền từ không được lớn hơn số tiền đến.");
                return false;
            }
            lblThongBao.Text = string.Empty;
            return true;
        }

        private static int? ThuDocMaHoaDon(string giaTri)
        {
            if (string.IsNullOrWhiteSpace(giaTri) || !giaTri.Trim().StartsWith("HD", StringComparison.OrdinalIgnoreCase))
                return null;
            int id;
            return int.TryParse(giaTri.Trim().Substring(2), out id) && id > 0 ? (int?)id : null;
        }

        private void ChonDongHoaDon(int id)
        {
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                var item = row.DataBoundItem as HoaDonHienThi;
                if (item?.HoaDonId != id) continue;
                row.Selected = true;
                dgvHoaDon.CurrentCell = row.Cells[0];
                return;
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            var item = dgvHoaDon.CurrentRow?.DataBoundItem as HoaDonHienThi;
            if (item == null) return;
            hoaDonDangChonId = item.HoaDonId;
            hoaDonDangChon = item;
            lblMaHoaDonChiTiet.Text = item.MaHoaDon;
            lblNgayLapChiTiet.Text = item.NgayLap.ToString("dd/MM/yyyy HH:mm");
            lblKhachHangChiTiet.Text = item.TenKhachHang;
            lblNhanVienChiTiet.Text = item.TenNhanVien;
            lblThanhToanChiTiet.Text = item.PhuongThucThanhToan;
            lblTrangThaiChiTiet.Text = item.TrangThaiHienThi;
            lblTienChiTiet.Text = $"Tổng {item.TongTien:N0} - Giảm {item.GiamGia:N0} = {item.ThanhTien:N0} đ";
            dgvChiTietHoaDon.DataSource = item.ChiTiet.Select(ct => ct.SaoChep()).ToList();
            btnHuyHoaDon.Enabled = item.TrangThai == "DA_THANH_TOAN";
            btnInHoaDon.Enabled = item.TrangThai == "DA_THANH_TOAN";
            lblThongBao.Text = string.Empty;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Checked && dtpDenNgay.Checked && dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                HienThiLoi("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }
            TaiDanhSachHoaDon();
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
            txtTienTu.Clear();
            txtTienDen.Clear();
            dtpTuNgay.Checked = false;
            dtpDenNgay.Checked = false;
            if (cboLocKhachHang.Items.Count > 0) cboLocKhachHang.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndex = 0;
            TaiDanhSachHoaDon();
        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !hoaDonDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn hóa đơn cần hủy.");
                return;
            }
            int id = hoaDonDangChonId.Value;
            if (MessageBox.Show(
                $"Bạn có chắc muốn hủy hóa đơn HD{id:000000}? Tồn kho sẽ được hoàn lại.",
                "Xác nhận hủy hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    var hoaDon = db.HoaDons
                        .Include(hd => hd.ChiTietHoaDons.Select(ct => ct.SanPham))
                        .SingleOrDefault(hd => hd.HoaDonId == id);
                    if (hoaDon == null)
                    {
                        HienThiLoi("Hóa đơn không còn tồn tại trong CSDL.");
                        return;
                    }
                    if (hoaDon.TrangThai != "DA_THANH_TOAN")
                    {
                        HienThiLoi("Hóa đơn này đã được hủy trước đó.");
                        return;
                    }
                    foreach (var dong in hoaDon.ChiTietHoaDons)
                    {
                        if ((long)dong.SanPham.SoLuongTon + dong.SoLuong > int.MaxValue)
                        {
                            HienThiLoi("Tồn kho sau hoàn tác vượt quá giới hạn cho phép.");
                            return;
                        }
                    }
                    foreach (var dong in hoaDon.ChiTietHoaDons)
                        dong.SanPham.SoLuongTon += dong.SoLuong;
                    hoaDon.TrangThai = "DA_HUY";
                    db.SaveChanges();
                    transaction.Commit();
                }
                TaiDuLieuLuaChon();
                TaiDanhSachHoaDon(id);
                MessageBox.Show("Đã hủy hóa đơn và hoàn lại tồn kho.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể hủy hóa đơn vì dữ liệu vừa được thay đổi.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể hủy hóa đơn. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !hoaDonDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn hóa đơn đã thanh toán cần xem báo cáo.");
                return;
            }
            try
            {
                CauHinhBaoCao cauHinh = BaoCaoService.TaoHoaDon(hoaDonDangChonId.Value);
                using (var xemTruoc = new FrmXemBaoCao(cauHinh))
                {
                    xemTruoc.ShowDialog(this);
                }
            }
            catch (InvalidOperationException ex)
            {
                HienThiLoi(ex.Message);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tạo báo cáo hóa đơn. Hãy kiểm tra kết nối CSDL và cấu hình ReportViewer.");
            }
        }

        private void HienThiLoi(string noiDung) => lblThongBao.Text = "* " + noiDung;

        private sealed class LuaChonKhachHang
        {
            public int? Id { get; set; }
            public string Ten { get; set; }
            public string SoDienThoai { get; set; }
            public bool DangHoatDong { get; set; }
            public LuaChonKhachHang SaoChep() => (LuaChonKhachHang)MemberwiseClone();
            public override string ToString() => string.IsNullOrWhiteSpace(SoDienThoai) ? Ten : $"{Ten} - {SoDienThoai}";
        }

        private sealed class DongChiTiet
        {
            public int SanPhamId { get; set; }
            public string MaSanPham { get; set; }
            public string TenSanPham { get; set; }
            public int TonKhoHienTai { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGiaBan { get; set; }
            public decimal ThanhTien => SoLuong * DonGiaBan;
            public DateTime? HanBaoHanh { get; set; }
            public string HanBaoHanhHienThi => HanBaoHanh.HasValue ? HanBaoHanh.Value.ToString("dd/MM/yyyy") : "Không có";
            public DongChiTiet SaoChep() => (DongChiTiet)MemberwiseClone();
        }

        private sealed class HoaDonHienThi
        {
            public HoaDonHienThi(HoaDon hoaDon)
            {
                HoaDonId = hoaDon.HoaDonId;
                MaHoaDon = $"HD{hoaDon.HoaDonId:000000}";
                NgayLap = hoaDon.NgayLap;
                NgayLapHienThi = hoaDon.NgayLap.ToString("dd/MM/yyyy HH:mm");
                TenKhachHang = hoaDon.KhachHang.HoTen;
                TenNhanVien = hoaDon.NhanVien.HoTen;
                TongTien = hoaDon.TongTien;
                GiamGia = hoaDon.GiamGia;
                ThanhTien = hoaDon.ThanhTien;
                PhuongThucThanhToan = hoaDon.PhuongThucThanhToan;
                TrangThai = hoaDon.TrangThai;
                TrangThaiHienThi = hoaDon.TrangThai == "DA_THANH_TOAN" ? "Đã thanh toán" : "Đã hủy";
                ChiTiet = hoaDon.ChiTietHoaDons
                    .OrderBy(ct => ct.ChiTietHoaDonId)
                    .Select(ct => new DongChiTiet
                    {
                        SanPhamId = ct.SanPhamId,
                        MaSanPham = $"SP{ct.SanPhamId:000000}",
                        TenSanPham = ct.SanPham.TenSanPham,
                        TonKhoHienTai = ct.SanPham.SoLuongTon,
                        SoLuong = ct.SoLuong,
                        DonGiaBan = ct.DonGiaBan,
                        HanBaoHanh = ct.HanBaoHanh
                    }).ToList();
            }
            public int HoaDonId { get; }
            public string MaHoaDon { get; }
            public DateTime NgayLap { get; }
            public string NgayLapHienThi { get; }
            public string TenKhachHang { get; }
            public string TenNhanVien { get; }
            public decimal TongTien { get; }
            public decimal GiamGia { get; }
            public decimal ThanhTien { get; }
            public string PhuongThucThanhToan { get; }
            public string TrangThai { get; }
            public string TrangThaiHienThi { get; }
            public List<DongChiTiet> ChiTiet { get; }
        }
    }
}

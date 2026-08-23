using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmBanHang : Form
    {
        private const decimal GiaTriTienToiDa = 9999999999999999.99M;
        private readonly bool moLichSuBanDau;
        private readonly List<DongBanHang> gioHang = new List<DongBanHang>();
        private int? hoaDonDangChonId;
        private HoaDonHienThi hoaDonDangChon;
        private bool dangLamMoi;

        public FrmBanHang() : this(false)
        {
        }

        protected FrmBanHang(bool moLichSuBanDau)
        {
            this.moLichSuBanDau = moLichSuBanDau;
            InitializeComponent();
            cboLocTrangThai.SelectedIndex = 0;
            cboPhuongThucThanhToan.SelectedIndex = 0;
            numSoLuong.Maximum = int.MaxValue;
            numGiamGia.Maximum = 9999999999999999M;
        }

        private void FrmBanHang_Load(object sender, EventArgs e)
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
                LamMoiHoaDonNoiBo();
                if (sanSang)
                    TaiDanhSachHoaDon();
                else
                    lblSoKetQua.Text = "0 hóa đơn";
                dgvHoaDon.ClearSelection();
                splitChinh.SplitterDistance = Math.Max(
                    splitChinh.Panel1MinSize,
                    Math.Min(205, splitChinh.Height / 2));
                if (moLichSuBanDau) tabBanHang.SelectedTab = tabLichSu;
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
                    cboKhachHang.DataSource = khachHang
                        .Where(kh => kh.DangHoatDong)
                        .Select(kh => kh.SaoChep())
                        .ToList();
                    var khachHangLoc = khachHang.Select(kh => kh.SaoChep()).ToList();
                    khachHangLoc.Insert(0, new LuaChonKhachHang { Id = null, Ten = "Tất cả khách hàng", DangHoatDong = true });
                    cboLocKhachHang.DataSource = khachHangLoc;

                    cboSanPham.DataSource = db.SanPhams.AsNoTracking()
                        .Where(sp => sp.DangKinhDoanh)
                        .OrderBy(sp => sp.TenSanPham)
                        .Select(sp => new LuaChonSanPham
                        {
                            Id = sp.SanPhamId,
                            Ten = sp.TenSanPham,
                            GiaBan = sp.GiaBan,
                            SoLuongTon = sp.SoLuongTon
                        }).ToList();
                }
                return true;
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải khách hàng và sản phẩm. Hãy kiểm tra kết nối CSDL.");
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
            btnLuuHoaDon.Enabled = false;
            btnHuyHoaDon.Enabled = item.TrangThai == "DA_THANH_TOAN";
            btnInHoaDon.Enabled = true;
            tabBanHang.SelectedTab = tabLichSu;
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

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            var sanPham = cboSanPham.SelectedItem as LuaChonSanPham;
            if (sanPham == null) return;
            lblTonKho.Text = "Tồn kho: " + sanPham.SoLuongTon;
            lblDonGiaBan.Text = sanPham.GiaBan.ToString("N0") + " đ";
        }

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            var sanPham = cboSanPham.SelectedItem as LuaChonSanPham;
            if (sanPham == null)
            {
                HienThiLoi("Vui lòng chọn sản phẩm.");
                return;
            }
            int soLuong = Decimal.ToInt32(numSoLuong.Value);
            if (soLuong <= 0)
            {
                HienThiLoi("Số lượng bán phải lớn hơn 0.");
                return;
            }
            if (sanPham.SoLuongTon < soLuong)
            {
                HienThiLoi($"Sản phẩm chỉ còn {sanPham.SoLuongTon} trong kho.");
                return;
            }
            DateTime? hanBaoHanh = dtpHanBaoHanh.Checked ? (DateTime?)dtpHanBaoHanh.Value.Date : null;
            if (hanBaoHanh.HasValue && hanBaoHanh.Value < DateTime.Today)
            {
                HienThiLoi("Hạn bảo hành không được nhỏ hơn ngày bán.");
                return;
            }
            decimal thanhTien;
            try { thanhTien = checked(soLuong * sanPham.GiaBan); }
            catch (OverflowException)
            {
                HienThiLoi("Thành tiền vượt quá giới hạn cho phép.");
                return;
            }
            if (thanhTien > GiaTriTienToiDa)
            {
                HienThiLoi("Thành tiền vượt quá giới hạn lưu trữ của CSDL.");
                return;
            }
            var dong = gioHang.SingleOrDefault(item => item.SanPhamId == sanPham.Id);
            if (dong == null)
            {
                gioHang.Add(new DongBanHang
                {
                    SanPhamId = sanPham.Id,
                    MaSanPham = $"SP{sanPham.Id:000000}",
                    TenSanPham = sanPham.Ten,
                    TonKhoHienTai = sanPham.SoLuongTon,
                    SoLuong = soLuong,
                    DonGiaBan = sanPham.GiaBan,
                    HanBaoHanh = hanBaoHanh
                });
            }
            else
            {
                dong.SoLuong = soLuong;
                dong.DonGiaBan = sanPham.GiaBan;
                dong.HanBaoHanh = hanBaoHanh;
            }
            if (!TaiGioHang()) return;
            LamMoiDongBan();
            lblThongBao.Text = string.Empty;
        }

        private void dgvGioHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            var dong = dgvGioHang.CurrentRow?.DataBoundItem as DongBanHang;
            if (dong == null) return;
            ChonSanPham(dong.SanPhamId);
            numSoLuong.Value = Math.Min(numSoLuong.Maximum, dong.SoLuong);
            dtpHanBaoHanh.Checked = dong.HanBaoHanh.HasValue;
            if (dong.HanBaoHanh.HasValue) dtpHanBaoHanh.Value = dong.HanBaoHanh.Value;
            btnThemDong.Text = "Cập nhật dòng";
        }

        private void ChonSanPham(int sanPhamId)
        {
            for (int i = 0; i < cboSanPham.Items.Count; i++)
            {
                if ((cboSanPham.Items[i] as LuaChonSanPham)?.Id != sanPhamId) continue;
                cboSanPham.SelectedIndex = i;
                return;
            }
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            var dong = dgvGioHang.CurrentRow?.DataBoundItem as DongBanHang;
            if (dong == null)
            {
                HienThiLoi("Vui lòng chọn dòng sản phẩm cần xóa.");
                return;
            }
            gioHang.RemoveAll(item => item.SanPhamId == dong.SanPhamId);
            TaiGioHang();
            LamMoiDongBan();
        }

        private void btnMoiDong_Click(object sender, EventArgs e) => LamMoiDongBan();
        private void numGiamGia_ValueChanged(object sender, EventArgs e) => CapNhatTongTien();

        private bool TaiGioHang()
        {
            decimal tongTien;
            try { tongTien = gioHang.Sum(dong => dong.ThanhTien); }
            catch (OverflowException)
            {
                HienThiLoi("Tổng tiền hóa đơn vượt quá giới hạn cho phép.");
                return false;
            }
            if (tongTien > GiaTriTienToiDa)
            {
                HienThiLoi("Tổng tiền hóa đơn vượt quá giới hạn lưu trữ của CSDL.");
                return false;
            }
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = gioHang.Select(dong => dong.SaoChep()).ToList();
            dgvGioHang.ClearSelection();
            lblSoDong.Text = gioHang.Count + " sản phẩm";
            CapNhatTongTien();
            return true;
        }

        private void CapNhatTongTien()
        {
            decimal tongTien;
            try { tongTien = gioHang.Sum(dong => dong.ThanhTien); }
            catch (OverflowException) { tongTien = GiaTriTienToiDa; }
            decimal giamGia = numGiamGia.Value;
            decimal thanhTien = Math.Max(0, tongTien - giamGia);
            lblTongTien.Text = $"Tổng: {tongTien:N0} đ   |   Phải trả: {thanhTien:N0} đ";
        }

        private void LamMoiDongBan()
        {
            dgvGioHang.ClearSelection();
            if (cboSanPham.Items.Count > 0) cboSanPham.SelectedIndex = 0;
            var sanPham = cboSanPham.SelectedItem as LuaChonSanPham;
            numSoLuong.Value = 1;
            dtpHanBaoHanh.Checked = true;
            dtpHanBaoHanh.Value = DateTime.Today.AddYears(1);
            lblTonKho.Text = sanPham == null ? "Tồn kho: --" : "Tồn kho: " + sanPham.SoLuongTon;
            lblDonGiaBan.Text = sanPham == null ? "--" : sanPham.GiaBan.ToString("N0") + " đ";
            btnThemDong.Text = "Thêm sản phẩm";
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            var khachHang = cboKhachHang.SelectedItem as LuaChonKhachHang;
            if (khachHang?.Id == null)
            {
                HienThiLoi("Vui lòng chọn khách hàng.");
                return;
            }
            if (gioHang.Count == 0)
            {
                HienThiLoi("Hóa đơn phải có ít nhất một sản phẩm.");
                return;
            }
            if (!TaiGioHang()) return;
            decimal tongTien = gioHang.Sum(dong => dong.ThanhTien);
            decimal giamGia = numGiamGia.Value;
            if (giamGia > tongTien)
            {
                HienThiLoi("Giảm giá không được lớn hơn tổng tiền.");
                return;
            }
            string phuongThuc = cboPhuongThucThanhToan.Text.Trim();
            if (string.IsNullOrWhiteSpace(phuongThuc))
            {
                HienThiLoi("Vui lòng chọn phương thức thanh toán.");
                return;
            }

            try
            {
                int hoaDonMoiId;
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    int nhanVienId = CurrentUserSession.HienTai.NhanVienId;
                    if (!db.NhanViens.Any(nv => nv.NhanVienId == nhanVienId && nv.DangLamViec))
                    {
                        HienThiLoi("Nhân viên của phiên đăng nhập không còn hoạt động.");
                        return;
                    }
                    if (!db.KhachHangs.Any(kh => kh.KhachHangId == khachHang.Id.Value && kh.DangHoatDong))
                    {
                        HienThiLoi("Khách hàng đã ngừng hoạt động. Hãy tải lại danh sách.");
                        return;
                    }
                    var ids = gioHang.Select(dong => dong.SanPhamId).ToList();
                    var sanPhams = db.SanPhams.Where(sp => ids.Contains(sp.SanPhamId)).ToList();
                    if (sanPhams.Count != ids.Count || sanPhams.Any(sp => !sp.DangKinhDoanh))
                    {
                        HienThiLoi("Có sản phẩm không còn kinh doanh. Hãy tải lại danh sách.");
                        return;
                    }
                    foreach (var dong in gioHang)
                    {
                        var sanPham = sanPhams.Single(sp => sp.SanPhamId == dong.SanPhamId);
                        if (sanPham.SoLuongTon < dong.SoLuong)
                        {
                            HienThiLoi($"Tồn kho {sanPham.TenSanPham} chỉ còn {sanPham.SoLuongTon}.");
                            return;
                        }
                        if (sanPham.GiaBan != dong.DonGiaBan)
                        {
                            HienThiLoi($"Giá bán {sanPham.TenSanPham} vừa thay đổi. Hãy tạo lại dòng sản phẩm.");
                            return;
                        }
                    }
                    DateTime ngayLap = DateTime.Now;
                    if (gioHang.Any(dong => dong.HanBaoHanh.HasValue && dong.HanBaoHanh.Value.Date < ngayLap.Date))
                    {
                        HienThiLoi("Có hạn bảo hành nhỏ hơn ngày lập hóa đơn.");
                        return;
                    }
                    var hoaDon = new HoaDon
                    {
                        NhanVienId = nhanVienId,
                        KhachHangId = khachHang.Id.Value,
                        NgayLap = ngayLap,
                        TongTien = tongTien,
                        GiamGia = giamGia,
                        ThanhTien = tongTien - giamGia,
                        PhuongThucThanhToan = phuongThuc,
                        TrangThai = "DA_THANH_TOAN"
                    };
                    db.HoaDons.Add(hoaDon);
                    foreach (var dong in gioHang)
                    {
                        hoaDon.ChiTietHoaDons.Add(new ChiTietHoaDon
                        {
                            SanPhamId = dong.SanPhamId,
                            SoLuong = dong.SoLuong,
                            DonGiaBan = dong.DonGiaBan,
                            HanBaoHanh = dong.HanBaoHanh
                        });
                        sanPhams.Single(sp => sp.SanPhamId == dong.SanPhamId).SoLuongTon -= dong.SoLuong;
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    hoaDonMoiId = hoaDon.HoaDonId;
                }
                TaiDuLieuLuaChon();
                TaiDanhSachHoaDon(hoaDonMoiId);
                MessageBox.Show($"Đã thanh toán hóa đơn HD{hoaDonMoiId:000000}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu hóa đơn. Dữ liệu có thể đã thay đổi hoặc bị trùng.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể lập hóa đơn. Hãy kiểm tra kết nối CSDL.");
            }
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
            if (!KiemTraPhienDangNhap(true) || hoaDonDangChon == null)
            {
                HienThiLoi("Vui lòng chọn hóa đơn cần in.");
                return;
            }
            try
            {
                using (var taiLieu = new PrintDocument())
                using (var xemTruoc = new PrintPreviewDialog())
                {
                    taiLieu.DocumentName = "HoaDon_" + hoaDonDangChon.MaHoaDon;
                    taiLieu.PrintPage += InTrangHoaDon;
                    xemTruoc.Document = taiLieu;
                    xemTruoc.Width = 900;
                    xemTruoc.Height = 700;
                    xemTruoc.ShowDialog(this);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tạo bản in hóa đơn. Hãy kiểm tra máy in.");
            }
        }

        private void InTrangHoaDon(object sender, PrintPageEventArgs e)
        {
            var hoaDon = hoaDonDangChon;
            if (hoaDon == null) return;
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            using (var tieuDe = new Font("Segoe UI", 16F, FontStyle.Bold))
            using (var dam = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (var thuong = new Font("Segoe UI", 10F))
            using (var nho = new Font("Segoe UI", 8F))
            {
                e.Graphics.DrawString("PNJ MANAGER - HÓA ĐƠN BÁN HÀNG", tieuDe, Brushes.Black, x, y);
                y += 38;
                e.Graphics.DrawString($"Mã: {hoaDon.MaHoaDon}    Ngày: {hoaDon.NgayLap:dd/MM/yyyy HH:mm}", thuong, Brushes.Black, x, y);
                y += 24;
                e.Graphics.DrawString($"Khách hàng: {hoaDon.TenKhachHang}    Nhân viên: {hoaDon.TenNhanVien}", thuong, Brushes.Black, x, y);
                y += 30;
                e.Graphics.DrawString("Sản phẩm", dam, Brushes.Black, x, y);
                e.Graphics.DrawString("SL", dam, Brushes.Black, x + 360, y);
                e.Graphics.DrawString("Đơn giá", dam, Brushes.Black, x + 420, y);
                e.Graphics.DrawString("Thành tiền", dam, Brushes.Black, x + 550, y);
                y += 24;
                foreach (var dong in hoaDon.ChiTiet)
                {
                    e.Graphics.DrawString(dong.TenSanPham, thuong, Brushes.Black, new RectangleF(x, y, 350, 22));
                    e.Graphics.DrawString(dong.SoLuong.ToString(), thuong, Brushes.Black, x + 360, y);
                    e.Graphics.DrawString(dong.DonGiaBan.ToString("N0"), thuong, Brushes.Black, x + 420, y);
                    e.Graphics.DrawString(dong.ThanhTien.ToString("N0"), thuong, Brushes.Black, x + 550, y);
                    y += 23;
                    if (dong.HanBaoHanh.HasValue)
                    {
                        e.Graphics.DrawString("Bảo hành đến " + dong.HanBaoHanh.Value.ToString("dd/MM/yyyy"), nho, Brushes.DimGray, x + 12, y);
                        y += 18;
                    }
                }
                y += 12;
                e.Graphics.DrawString($"Tổng tiền: {hoaDon.TongTien:N0} đ", dam, Brushes.Black, x + 420, y);
                y += 24;
                e.Graphics.DrawString($"Giảm giá: {hoaDon.GiamGia:N0} đ", thuong, Brushes.Black, x + 420, y);
                y += 24;
                e.Graphics.DrawString($"Thanh toán: {hoaDon.ThanhTien:N0} đ", dam, Brushes.Black, x + 420, y);
                y += 30;
                e.Graphics.DrawString($"Phương thức: {hoaDon.PhuongThucThanhToan} - {hoaDon.TrangThaiHienThi}", thuong, Brushes.Black, x, y);
            }
            e.HasMorePages = false;
        }

        private void btnHoaDonMoi_Click(object sender, EventArgs e) => LamMoiHoaDon();

        private void LamMoiHoaDon()
        {
            dangLamMoi = true;
            try { LamMoiHoaDonNoiBo(); }
            finally { dangLamMoi = false; }
        }

        private void LamMoiHoaDonNoiBo()
        {
            hoaDonDangChonId = null;
            hoaDonDangChon = null;
            gioHang.Clear();
            if (cboKhachHang.Items.Count > 0) cboKhachHang.SelectedIndex = 0;
            lblNhanVienLap.Text = CurrentUserSession.DaDangNhap ? CurrentUserSession.HienTai.HoTen : string.Empty;
            lblNgayLap.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            cboPhuongThucThanhToan.SelectedIndex = 0;
            numGiamGia.Value = 0;
            TaiGioHang();
            LamMoiDongBan();
            dgvHoaDon.ClearSelection();
            dgvChiTietHoaDon.DataSource = null;
            btnLuuHoaDon.Enabled = true;
            btnHuyHoaDon.Enabled = false;
            btnInHoaDon.Enabled = false;
            tabBanHang.SelectedTab = tabLapHoaDon;
            lblThongBao.Text = string.Empty;
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

        private sealed class LuaChonSanPham
        {
            public int Id { get; set; }
            public string Ten { get; set; }
            public decimal GiaBan { get; set; }
            public int SoLuongTon { get; set; }
            public override string ToString() => $"SP{Id:000000} - {Ten}";
        }

        private sealed class DongBanHang
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
            public DongBanHang SaoChep() => (DongBanHang)MemberwiseClone();
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
                    .Select(ct => new DongBanHang
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
            public List<DongBanHang> ChiTiet { get; }
        }
    }
}

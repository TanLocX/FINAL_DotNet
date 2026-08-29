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
    public partial class FrmBanHang : Form
    {
        private const decimal GiaTriTienToiDa = 9999999999999999.99M;
        private readonly List<DongBanHang> gioHang = new List<DongBanHang>();
        private bool dangLamMoi;

        public FrmBanHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            KhoiTaoGiaoDienTuyBien();
            cboPhuongThucThanhToan.SelectedIndex = 0;
            numSoLuong.Maximum = int.MaxValue;
            numGiamGia.Maximum = 9999999999999999M;
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

            CauHinhLuoi(dgvGioHang, headerStyle);
            if (dgvGioHang.Columns.Count == 0)
            {
                dgvGioHang.Columns.Add(TaoCot("Mã SP", "MaSanPham", 85));
                dgvGioHang.Columns.Add(TaoCot("Tên sản phẩm", "TenSanPham", 280));
                dgvGioHang.Columns.Add(TaoCot("Tồn", "TonKhoHienTai", 65));
                dgvGioHang.Columns.Add(TaoCot("Số lượng", "SoLuong", 75));
                DataGridViewTextBoxColumn cotGiaBan = TaoCot("Đơn giá", "DonGiaBan", 125);
                cotGiaBan.DefaultCellStyle.Format = "N0";
                dgvGioHang.Columns.Add(cotGiaBan);
                DataGridViewTextBoxColumn cotThanhTien = TaoCot("Thành tiền", "ThanhTien", 140);
                cotThanhTien.DefaultCellStyle.Format = "N0";
                dgvGioHang.Columns.Add(cotThanhTien);
                dgvGioHang.Columns.Add(TaoCot("Bảo hành đến", "HanBaoHanhHienThi", 115));
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
                TaiDuLieuLuaChon();
                LamMoiHoaDonNoiBo();
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
                LamMoiHoaDon();
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

        private void btnHoaDonMoi_Click(object sender, EventArgs e) => LamMoiHoaDon();

        private void LamMoiHoaDon()
        {
            dangLamMoi = true;
            try { LamMoiHoaDonNoiBo(); }
            finally { dangLamMoi = false; }
        }

        private void LamMoiHoaDonNoiBo()
        {
            gioHang.Clear();
            if (cboKhachHang.Items.Count > 0) cboKhachHang.SelectedIndex = 0;
            lblNhanVienLap.Text = CurrentUserSession.DaDangNhap ? CurrentUserSession.HienTai.HoTen : string.Empty;
            lblNgayLap.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            cboPhuongThucThanhToan.SelectedIndex = 0;
            numGiamGia.Value = 0;
            TaiGioHang();
            LamMoiDongBan();
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
    }
}

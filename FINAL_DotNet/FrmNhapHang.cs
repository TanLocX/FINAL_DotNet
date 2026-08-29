using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmNhapHang : Form
    {
        private const decimal GiaTriTienToiDa = 9999999999999999.99M;
        private readonly List<DongNhapHang> gioNhapHang = new List<DongNhapHang>();
        private int? phieuNhapDangChonId;
        private bool dangLamMoi;

        public FrmNhapHang()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            cboLocTrangThai.SelectedIndex = 0;
            numSoLuong.Maximum = int.MaxValue;
            numDonGiaNhap.Maximum = 9999999999999999M;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmNhapHang_Load(object sender, EventArgs e)
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
                LamMoiPhieuNoiBo();
                if (sanSang)
                    TaiDanhSachPhieu();
                else
                    lblSoKetQua.Text = "0 phiếu nhập";
                dgvPhieuNhap.ClearSelection();
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
                    var nhaCungCap = db.NhaCungCaps.AsNoTracking()
                        .OrderBy(ncc => ncc.TenNhaCungCap)
                        .Select(ncc => new LuaChonNhaCungCap
                        {
                            Id = ncc.NhaCungCapId,
                            Ten = ncc.TenNhaCungCap,
                            DangHoatDong = ncc.DangHoatDong
                        }).ToList();

                    cboNhaCungCap.DataSource = nhaCungCap
                        .Where(ncc => ncc.DangHoatDong)
                        .Select(ncc => ncc.SaoChep())
                        .ToList();
                    var nhaCungCapLoc = nhaCungCap.Select(ncc => ncc.SaoChep()).ToList();
                    nhaCungCapLoc.Insert(0, new LuaChonNhaCungCap { Id = null, Ten = "Tất cả nhà cung cấp", DangHoatDong = true });
                    cboLocNhaCungCap.DataSource = nhaCungCapLoc;

                    cboSanPham.DataSource = db.SanPhams.AsNoTracking()
                        .Where(sp => sp.DangKinhDoanh)
                        .OrderBy(sp => sp.TenSanPham)
                        .Select(sp => new LuaChonSanPham
                        {
                            Id = sp.SanPhamId,
                            Ten = sp.TenSanPham,
                            GiaVon = sp.GiaVon,
                            SoLuongTon = sp.SoLuongTon
                        }).ToList();
                }
                return true;
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải nhà cung cấp và sản phẩm. Hãy kiểm tra kết nối CSDL.");
                return false;
            }
        }

        private void TaiDanhSachPhieu(int? phieuCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int? maPhieu = ThuDocMaPhieu(tuKhoa);
                int? nhaCungCapId = (cboLocNhaCungCap.SelectedItem as LuaChonNhaCungCap)?.Id;
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<PhieuNhap> truyVan = db.PhieuNhaps
                        .Include(pn => pn.NhaCungCap)
                        .Include(pn => pn.NhanVien)
                        .Include(pn => pn.ChiTietPhieuNhaps.Select(ct => ct.SanPham))
                        .AsNoTracking();

                    if (dtpTuNgay.Checked)
                    {
                        DateTime tuNgay = dtpTuNgay.Value.Date;
                        truyVan = truyVan.Where(pn => pn.NgayNhap >= tuNgay);
                    }
                    if (dtpDenNgay.Checked)
                    {
                        DateTime denNgayKeTiep = dtpDenNgay.Value.Date.AddDays(1);
                        truyVan = truyVan.Where(pn => pn.NgayNhap < denNgayKeTiep);
                    }
                    if (nhaCungCapId.HasValue)
                        truyVan = truyVan.Where(pn => pn.NhaCungCapId == nhaCungCapId.Value);
                    if (trangThai == 1)
                        truyVan = truyVan.Where(pn => pn.TrangThai == "HOAN_THANH");
                    else if (trangThai == 2)
                        truyVan = truyVan.Where(pn => pn.TrangThai == "DA_HUY");
                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        if (maPhieu.HasValue)
                            truyVan = truyVan.Where(pn => pn.PhieuNhapId == maPhieu.Value);
                        else
                            truyVan = truyVan.Where(pn =>
                                pn.NhanVien.HoTen.Contains(tuKhoa) ||
                                pn.ChiTietPhieuNhaps.Any(ct => ct.SanPham.TenSanPham.Contains(tuKhoa)));
                    }

                    List<PhieuNhapHienThi> danhSach = truyVan
                        .OrderByDescending(pn => pn.NgayNhap)
                        .ThenByDescending(pn => pn.PhieuNhapId)
                        .ToList()
                        .Select(pn => new PhieuNhapHienThi(pn))
                        .ToList();
                    dgvPhieuNhap.DataSource = danhSach;
                    lblSoKetQua.Text = danhSach.Count + " phiếu nhập";
                }
                if (phieuCanChonId.HasValue) ChonDongPhieu(phieuCanChonId.Value);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải lịch sử nhập hàng. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private static int? ThuDocMaPhieu(string giaTri)
        {
            if (string.IsNullOrWhiteSpace(giaTri) || !giaTri.Trim().StartsWith("PN", StringComparison.OrdinalIgnoreCase))
                return null;
            int id;
            return int.TryParse(giaTri.Trim().Substring(2), out id) && id > 0 ? (int?)id : null;
        }

        private void ChonDongPhieu(int id)
        {
            foreach (DataGridViewRow row in dgvPhieuNhap.Rows)
            {
                var item = row.DataBoundItem as PhieuNhapHienThi;
                if (item?.PhieuNhapId != id) continue;
                row.Selected = true;
                dgvPhieuNhap.CurrentCell = row.Cells[0];
                return;
            }
        }

        private void dgvPhieuNhap_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            var item = dgvPhieuNhap.CurrentRow?.DataBoundItem as PhieuNhapHienThi;
            if (item == null) return;

            phieuNhapDangChonId = item.PhieuNhapId;
            lblMaPhieuChiTiet.Text = item.MaPhieuNhap;
            lblNgayNhapChiTiet.Text = item.NgayNhap.ToString("dd/MM/yyyy HH:mm");
            lblNhaCungCapChiTiet.Text = item.TenNhaCungCap;
            lblNhanVienChiTiet.Text = item.TenNhanVien;
            lblTrangThaiChiTiet.Text = item.TrangThaiHienThi;
            lblTongTienChiTiet.Text = item.TongTienNhap.ToString("N0") + " đ";
            txtGhiChuChiTiet.Text = item.GhiChu ?? string.Empty;
            dgvChiTietPhieu.DataSource = item.ChiTiet.Select(ct => ct.SaoChep()).ToList();
            btnLuuPhieu.Enabled = false;
            btnHuyPhieu.Enabled = item.TrangThai == "HOAN_THANH";
            btnXemBaoCao.Enabled = item.TrangThai == "HOAN_THANH";
            tabNhapHang.SelectedTab = tabChiTiet;
            lblThongBao.Text = string.Empty;
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

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !phieuNhapDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn phiếu nhập đã hoàn thành cần xem báo cáo.");
                return;
            }
            try
            {
                CauHinhBaoCao cauHinh = BaoCaoService.TaoPhieuNhap(phieuNhapDangChonId.Value);
                using (var xemTruoc = new FrmXemBaoCao(cauHinh)) xemTruoc.ShowDialog(this);
            }
            catch (InvalidOperationException ex)
            {
                HienThiLoi(ex.Message);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tạo báo cáo phiếu nhập. Hãy kiểm tra kết nối CSDL và cấu hình ReportViewer.");
            }
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
            if (cboLocNhaCungCap.Items.Count > 0) cboLocNhaCungCap.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndex = 0;
            TaiDanhSachPhieu();
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            var sanPham = cboSanPham.SelectedItem as LuaChonSanPham;
            if (sanPham == null) return;
            lblTonKhoHienTai.Text = "Tồn hiện tại: " + sanPham.SoLuongTon;
            numDonGiaNhap.Value = Math.Min(numDonGiaNhap.Maximum, Math.Max(0, sanPham.GiaVon));
        }

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            var sanPham = cboSanPham.SelectedItem as LuaChonSanPham;
            if (sanPham == null)
            {
                HienThiLoi("Vui lòng chọn sản phẩm cần nhập.");
                return;
            }
            if (numSoLuong.Value <= 0)
            {
                HienThiLoi("Số lượng nhập phải lớn hơn 0.");
                return;
            }
            if (numDonGiaNhap.Value <= 0)
            {
                HienThiLoi("Đơn giá nhập phải lớn hơn 0.");
                return;
            }
            int soLuong = Decimal.ToInt32(numSoLuong.Value);
            if ((long)sanPham.SoLuongTon + soLuong > int.MaxValue)
            {
                HienThiLoi("Số lượng sau khi nhập vượt quá giới hạn cho phép.");
                return;
            }
            decimal thanhTien;
            try { thanhTien = checked(soLuong * numDonGiaNhap.Value); }
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

            var dong = gioNhapHang.SingleOrDefault(item => item.SanPhamId == sanPham.Id);
            if (dong == null)
            {
                gioNhapHang.Add(new DongNhapHang
                {
                    SanPhamId = sanPham.Id,
                    MaSanPham = $"SP{sanPham.Id:000000}",
                    TenSanPham = sanPham.Ten,
                    TonKhoHienTai = sanPham.SoLuongTon,
                    SoLuong = soLuong,
                    DonGiaNhap = numDonGiaNhap.Value
                });
            }
            else
            {
                dong.SoLuong = soLuong;
                dong.DonGiaNhap = numDonGiaNhap.Value;
            }
            if (!TaiGioNhapHang()) return;
            LamMoiDongNhap();
            lblThongBao.Text = string.Empty;
        }

        private void dgvGioNhap_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoi) return;
            var dong = dgvGioNhap.CurrentRow?.DataBoundItem as DongNhapHang;
            if (dong == null) return;
            ChonSanPham(dong.SanPhamId);
            numSoLuong.Value = Math.Min(numSoLuong.Maximum, dong.SoLuong);
            numDonGiaNhap.Value = Math.Min(numDonGiaNhap.Maximum, dong.DonGiaNhap);
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
            var dong = dgvGioNhap.CurrentRow?.DataBoundItem as DongNhapHang;
            if (dong == null)
            {
                HienThiLoi("Vui lòng chọn dòng sản phẩm cần xóa.");
                return;
            }
            gioNhapHang.RemoveAll(item => item.SanPhamId == dong.SanPhamId);
            TaiGioNhapHang();
            LamMoiDongNhap();
        }

        private void btnMoiDong_Click(object sender, EventArgs e) => LamMoiDongNhap();

        private bool TaiGioNhapHang()
        {
            decimal tongTien;
            try { tongTien = gioNhapHang.Sum(dong => dong.ThanhTien); }
            catch (OverflowException)
            {
                HienThiLoi("Tổng tiền phiếu nhập vượt quá giới hạn cho phép.");
                return false;
            }
            if (tongTien > GiaTriTienToiDa)
            {
                HienThiLoi("Tổng tiền phiếu nhập vượt quá giới hạn lưu trữ của CSDL.");
                return false;
            }
            dgvGioNhap.DataSource = null;
            dgvGioNhap.DataSource = gioNhapHang.Select(dong => dong.SaoChep()).ToList();
            dgvGioNhap.ClearSelection();
            lblTongTienLapPhieu.Text = "Tổng tiền: " + tongTien.ToString("N0") + " đ";
            lblSoDong.Text = gioNhapHang.Count + " sản phẩm";
            return true;
        }

        private void LamMoiDongNhap()
        {
            dgvGioNhap.ClearSelection();
            if (cboSanPham.Items.Count > 0) cboSanPham.SelectedIndex = 0;
            var sanPham = cboSanPham.SelectedItem as LuaChonSanPham;
            numSoLuong.Value = 1;
            numDonGiaNhap.Value = sanPham == null ? 0 : Math.Min(numDonGiaNhap.Maximum, sanPham.GiaVon);
            lblTonKhoHienTai.Text = sanPham == null ? "Tồn hiện tại: --" : "Tồn hiện tại: " + sanPham.SoLuongTon;
            btnThemDong.Text = "Thêm sản phẩm";
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            var nhaCungCap = cboNhaCungCap.SelectedItem as LuaChonNhaCungCap;
            if (nhaCungCap?.Id == null)
            {
                HienThiLoi("Vui lòng chọn nhà cung cấp.");
                return;
            }
            if (gioNhapHang.Count == 0)
            {
                HienThiLoi("Phiếu nhập phải có ít nhất một sản phẩm.");
                return;
            }
            if (!TaiGioNhapHang()) return;
            string ghiChu = ChuanHoaTuyChon(txtGhiChu.Text);

            try
            {
                int phieuMoiId;
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    int nhanVienId = CurrentUserSession.HienTai.NhanVienId;
                    if (!db.NhanViens.Any(nv => nv.NhanVienId == nhanVienId && nv.DangLamViec))
                    {
                        HienThiLoi("Nhân viên của phiên đăng nhập không còn hoạt động.");
                        return;
                    }
                    if (!db.NhaCungCaps.Any(ncc => ncc.NhaCungCapId == nhaCungCap.Id.Value && ncc.DangHoatDong))
                    {
                        HienThiLoi("Nhà cung cấp đã ngừng hoạt động. Hãy tải lại danh sách.");
                        return;
                    }
                    var ids = gioNhapHang.Select(dong => dong.SanPhamId).ToList();
                    var sanPhams = db.SanPhams.Where(sp => ids.Contains(sp.SanPhamId)).ToList();
                    if (sanPhams.Count != ids.Count || sanPhams.Any(sp => !sp.DangKinhDoanh))
                    {
                        HienThiLoi("Có sản phẩm không còn kinh doanh. Hãy tải lại danh sách.");
                        return;
                    }
                    foreach (var dong in gioNhapHang)
                    {
                        var sanPham = sanPhams.Single(sp => sp.SanPhamId == dong.SanPhamId);
                        if ((long)sanPham.SoLuongTon + dong.SoLuong > int.MaxValue)
                        {
                            HienThiLoi("Tồn kho của " + sanPham.TenSanPham + " vượt quá giới hạn.");
                            return;
                        }
                    }

                    var phieu = new PhieuNhap
                    {
                        NhanVienId = nhanVienId,
                        NhaCungCapId = nhaCungCap.Id.Value,
                        NgayNhap = DateTime.Now,
                        TongTienNhap = gioNhapHang.Sum(dong => dong.ThanhTien),
                        TrangThai = "HOAN_THANH",
                        GhiChu = ghiChu
                    };
                    db.PhieuNhaps.Add(phieu);
                    foreach (var dong in gioNhapHang)
                    {
                        phieu.ChiTietPhieuNhaps.Add(new ChiTietPhieuNhap
                        {
                            SanPhamId = dong.SanPhamId,
                            SoLuong = dong.SoLuong,
                            DonGiaNhap = dong.DonGiaNhap
                        });
                        var sanPham = sanPhams.Single(sp => sp.SanPhamId == dong.SanPhamId);
                        sanPham.SoLuongTon += dong.SoLuong;
                        sanPham.GiaVon = dong.DonGiaNhap;
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    phieuMoiId = phieu.PhieuNhapId;
                }
                TaiDuLieuLuaChon();
                TaiDanhSachPhieu(phieuMoiId);
                MessageBox.Show($"Đã lập phiếu nhập PN{phieuMoiId:000000}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu phiếu nhập. Dữ liệu có thể đã thay đổi hoặc bị trùng.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể lập phiếu nhập. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !phieuNhapDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn phiếu nhập cần hủy.");
                return;
            }
            int id = phieuNhapDangChonId.Value;
            if (MessageBox.Show(
                $"Bạn có chắc muốn hủy phiếu nhập PN{id:000000}? Tồn kho sẽ được hoàn tác.",
                "Xác nhận hủy phiếu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    var phieu = db.PhieuNhaps
                        .Include(pn => pn.ChiTietPhieuNhaps.Select(ct => ct.SanPham))
                        .SingleOrDefault(pn => pn.PhieuNhapId == id);
                    if (phieu == null)
                    {
                        HienThiLoi("Phiếu nhập không còn tồn tại trong CSDL.");
                        return;
                    }
                    if (phieu.TrangThai != "HOAN_THANH")
                    {
                        HienThiLoi("Phiếu nhập này đã được hủy trước đó.");
                        return;
                    }
                    foreach (var dong in phieu.ChiTietPhieuNhaps)
                    {
                        if (dong.SanPham.SoLuongTon < dong.SoLuong)
                        {
                            HienThiLoi($"Không thể hủy vì tồn kho {dong.SanPham.TenSanPham} chỉ còn {dong.SanPham.SoLuongTon}.");
                            return;
                        }
                    }
                    foreach (var dong in phieu.ChiTietPhieuNhaps)
                    {
                        dong.SanPham.SoLuongTon -= dong.SoLuong;
                        var lanNhapHopLeGanNhat = db.ChiTietPhieuNhaps
                            .Where(ct => ct.SanPhamId == dong.SanPhamId
                                && ct.PhieuNhapId != id
                                && ct.PhieuNhap.TrangThai == "HOAN_THANH")
                            .OrderByDescending(ct => ct.PhieuNhap.NgayNhap)
                            .ThenByDescending(ct => ct.PhieuNhapId)
                            .FirstOrDefault();
                        dong.SanPham.GiaVon = lanNhapHopLeGanNhat == null ? 0 : lanNhapHopLeGanNhat.DonGiaNhap;
                    }
                    phieu.TrangThai = "DA_HUY";
                    db.SaveChanges();
                    transaction.Commit();
                }
                TaiDuLieuLuaChon();
                TaiDanhSachPhieu(id);
                MessageBox.Show("Đã hủy phiếu nhập và hoàn tác tồn kho.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể hủy phiếu nhập vì dữ liệu vừa được thay đổi.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể hủy phiếu nhập. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnPhieuMoi_Click(object sender, EventArgs e) => LamMoiPhieu();

        private void LamMoiPhieu()
        {
            dangLamMoi = true;
            try { LamMoiPhieuNoiBo(); }
            finally { dangLamMoi = false; }
        }

        private void LamMoiPhieuNoiBo()
        {
            phieuNhapDangChonId = null;
            gioNhapHang.Clear();
            if (cboNhaCungCap.Items.Count > 0) cboNhaCungCap.SelectedIndex = 0;
            lblNhanVienLapPhieu.Text = CurrentUserSession.DaDangNhap ? CurrentUserSession.HienTai.HoTen : string.Empty;
            lblNgayLapPhieu.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtGhiChu.Clear();
            TaiGioNhapHang();
            LamMoiDongNhap();
            dgvPhieuNhap.ClearSelection();
            dgvChiTietPhieu.DataSource = null;
            btnLuuPhieu.Enabled = true;
            btnHuyPhieu.Enabled = false;
            btnXemBaoCao.Enabled = false;
            tabNhapHang.SelectedTab = tabLapPhieu;
            lblThongBao.Text = string.Empty;
        }

        private static string ChuanHoaTuyChon(string giaTri)
        {
            string ketQua = (giaTri ?? string.Empty).Trim();
            return ketQua.Length == 0 ? null : ketQua;
        }

        private void HienThiLoi(string noiDung) => lblThongBao.Text = "* " + noiDung;

        private sealed class LuaChonNhaCungCap
        {
            public int? Id { get; set; }
            public string Ten { get; set; }
            public bool DangHoatDong { get; set; }
            public LuaChonNhaCungCap SaoChep() => (LuaChonNhaCungCap)MemberwiseClone();
            public override string ToString() => Ten;
        }

        private sealed class LuaChonSanPham
        {
            public int Id { get; set; }
            public string Ten { get; set; }
            public decimal GiaVon { get; set; }
            public int SoLuongTon { get; set; }
            public override string ToString() => $"SP{Id:000000} - {Ten}";
        }

        private sealed class DongNhapHang
        {
            public int SanPhamId { get; set; }
            public string MaSanPham { get; set; }
            public string TenSanPham { get; set; }
            public int TonKhoHienTai { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGiaNhap { get; set; }
            public decimal ThanhTien => SoLuong * DonGiaNhap;
            public DongNhapHang SaoChep() => (DongNhapHang)MemberwiseClone();
        }

        private sealed class PhieuNhapHienThi
        {
            public PhieuNhapHienThi(PhieuNhap phieu)
            {
                PhieuNhapId = phieu.PhieuNhapId;
                MaPhieuNhap = $"PN{phieu.PhieuNhapId:000000}";
                NgayNhap = phieu.NgayNhap;
                NgayNhapHienThi = phieu.NgayNhap.ToString("dd/MM/yyyy HH:mm");
                TenNhaCungCap = phieu.NhaCungCap.TenNhaCungCap;
                TenNhanVien = phieu.NhanVien.HoTen;
                TongTienNhap = phieu.TongTienNhap;
                TrangThai = phieu.TrangThai;
                TrangThaiHienThi = phieu.TrangThai == "HOAN_THANH" ? "Hoàn thành" : "Đã hủy";
                GhiChu = phieu.GhiChu;
                ChiTiet = phieu.ChiTietPhieuNhaps
                    .OrderBy(ct => ct.ChiTietPhieuNhapId)
                    .Select(ct => new DongNhapHang
                    {
                        SanPhamId = ct.SanPhamId,
                        MaSanPham = $"SP{ct.SanPhamId:000000}",
                        TenSanPham = ct.SanPham.TenSanPham,
                        TonKhoHienTai = ct.SanPham.SoLuongTon,
                        SoLuong = ct.SoLuong,
                        DonGiaNhap = ct.DonGiaNhap
                    }).ToList();
            }
            public int PhieuNhapId { get; }
            public string MaPhieuNhap { get; }
            public DateTime NgayNhap { get; }
            public string NgayNhapHienThi { get; }
            public string TenNhaCungCap { get; }
            public string TenNhanVien { get; }
            public decimal TongTienNhap { get; }
            public string TrangThai { get; }
            public string TrangThaiHienThi { get; }
            public string GhiChu { get; }
            public List<DongNhapHang> ChiTiet { get; }
        }
    }
}

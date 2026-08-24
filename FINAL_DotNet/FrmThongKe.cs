using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FINAL_DotNet
{
    public partial class FrmThongKe : Form
    {
        private static readonly CultureInfo VanHoaVietNam = CultureInfo.GetCultureInfo("vi-VN");
        private readonly bool moTabPhanTich;
        private bool dangKhoiTao = true;
        private DuLieuThongKe duLieuHienTai;

        public FrmThongKe(bool moTabPhanTich = false)
        {
            this.moTabPhanTich = moTabPhanTich;
            InitializeComponent();
            cboKhoangThoiGian.SelectedIndex = 2;
            ApDungKhoangThoiGian(2);
            dangKhoiTao = false;
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            if (!CurrentUserSession.DaDangNhap)
            {
                MessageBox.Show("Phiên đăng nhập đã kết thúc. Vui lòng đăng nhập lại.", "Chưa đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeginInvoke(new Action(Close));
                return;
            }

            if (moTabPhanTich) tabChinh.SelectedTab = tabPhanTich;
            TaiVaHienThiDuLieu();
        }

        private void cboKhoangThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangKhoiTao) return;
            ApDungKhoangThoiGian(cboKhoangThoiGian.SelectedIndex);
        }

        private void ApDungKhoangThoiGian(int index)
        {
            DateTime homNay = DateTime.Today;
            switch (index)
            {
                case 0:
                    dtpTuNgay.Value = homNay;
                    dtpDenNgay.Value = homNay;
                    break;
                case 1:
                    dtpTuNgay.Value = homNay.AddDays(-6);
                    dtpDenNgay.Value = homNay;
                    break;
                case 2:
                    dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
                    dtpDenNgay.Value = homNay;
                    break;
                case 3:
                    dtpTuNgay.Value = new DateTime(homNay.Year, 1, 1);
                    dtpDenNgay.Value = homNay;
                    break;
            }

            bool choSua = index == 4;
            dtpTuNgay.Enabled = choSua;
            dtpDenNgay.Enabled = choSua;
        }

        private void btnTaiLai_Click(object sender, EventArgs e) => TaiVaHienThiDuLieu();

        private void TaiVaHienThiDuLieu()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;
            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnTaiLai.Enabled = false;
                lblTrangThaiTai.Text = "Đang tổng hợp dữ liệu...";
                Cursor = Cursors.WaitCursor;
                duLieuHienTai = TaiDuLieu(tuNgay, denNgay, decimal.ToInt32(nudNguongTon.Value));
                HienThiDuLieu(duLieuHienTai);
                lblTrangThaiTai.Text = "Đã cập nhật lúc " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception)
            {
                duLieuHienTai = null;
                lblTrangThaiTai.Text = "Không thể tải dữ liệu";
                MessageBox.Show("Không thể tổng hợp thống kê. Hãy kiểm tra kết nối CSDL và thử lại.",
                    "Lỗi thống kê", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnTaiLai.Enabled = true;
            }
        }

        private static DuLieuThongKe TaiDuLieu(DateTime tuNgay, DateTime denNgay, int nguongTon)
        {
            DateTime denNgayKeTiep = denNgay.AddDays(1);
            DateTime homNay = DateTime.Today;
            DateTime hanSapHet = homNay.AddDays(31);

            using (var db = DatabaseConnection.CreateContext())
            {
                List<HoaDon> hoaDons = db.HoaDons
                    .Include("KhachHang")
                    .Include("NhanVien")
                    .Include("ChiTietHoaDons.SanPham.DanhMuc")
                    .Include("ChiTietHoaDons.SanPham.ChiTietChatLieux.ChatLieu")
                    .AsNoTracking()
                    .Where(hd => hd.TrangThai == "DA_THANH_TOAN" && hd.NgayLap >= tuNgay && hd.NgayLap < denNgayKeTiep)
                    .ToList();

                List<PhieuNhap> phieuNhaps = db.PhieuNhaps
                    .Include("NhaCungCap")
                    .Include("NhanVien")
                    .Include("ChiTietPhieuNhaps.SanPham")
                    .AsNoTracking()
                    .Where(pn => pn.TrangThai == "HOAN_THANH" && pn.NgayNhap >= tuNgay && pn.NgayNhap < denNgayKeTiep)
                    .ToList();

                List<PhieuBaoHanh> baoHanhs = db.PhieuBaoHanhs
                    .Include("ChiTietHoaDon.HoaDon.KhachHang")
                    .Include("ChiTietHoaDon.SanPham")
                    .AsNoTracking()
                    .Where(pbh => pbh.NgayTiepNhan >= tuNgay && pbh.NgayTiepNhan < denNgayKeTiep)
                    .ToList();

                List<NhatKyGuiEmail> emailLogs = db.NhatKyGuiEmails
                    .Include("KhachHang")
                    .Include("HoaDon")
                    .Include("MauEmail")
                    .Include("TaiKhoan.NhanVien")
                    .AsNoTracking()
                    .Where(nk => nk.ThoiGianGui >= tuNgay && nk.ThoiGianGui < denNgayKeTiep)
                    .ToList();

                List<SanPham> sanPhams = db.SanPhams
                    .Include("DanhMuc")
                    .Include("ChiTietChatLieux.ChatLieu")
                    .AsNoTracking()
                    .OrderBy(sp => sp.SanPhamId)
                    .ToList();

                List<ChiTietHoaDon> sapHetHan = db.ChiTietHoaDons
                    .Include("HoaDon.KhachHang")
                    .Include("SanPham")
                    .AsNoTracking()
                    .Where(ct => ct.HoaDon.TrangThai == "DA_THANH_TOAN" && ct.HanBaoHanh.HasValue &&
                                 ct.HanBaoHanh.Value >= homNay && ct.HanBaoHanh.Value < hanSapHet)
                    .OrderBy(ct => ct.HanBaoHanh)
                    .ToList();

                return DuLieuThongKe.Tao(tuNgay, denNgay, nguongTon, hoaDons, phieuNhaps, baoHanhs, emailLogs, sanPhams, sapHetHan);
            }
        }

        private void HienThiDuLieu(DuLieuThongKe duLieu)
        {
            lblDoanhThu.Text = DinhDangTien(duLieu.DoanhThu);
            lblSoHoaDon.Text = duLieu.SoHoaDon.ToString("N0", VanHoaVietNam);
            lblTrungBinhHoaDon.Text = DinhDangTien(duLieu.GiaTriTrungBinhHoaDon);
            lblTienNhap.Text = DinhDangTien(duLieu.TongTienNhap);
            lblTonThap.Text = duLieu.TonThap.Count.ToString("N0", VanHoaVietNam);
            lblBaoHanhDangXuLy.Text = duLieu.SoBaoHanhChuaTra.ToString("N0", VanHoaVietNam);
            lblBaoHanhSapHetHan.Text = duLieu.BaoHanhSapHetHan.Count.ToString("N0", VanHoaVietNam);
            lblTyLeEmail.Text = duLieu.TongEmail == 0
                ? "Chưa có email"
                : duLieu.TyLeEmailThanhCong.ToString("P1", VanHoaVietNam);

            VeBieuDoThoiGian(chartDoanhThuNgay, duLieu.DoanhThuTheoThoiGian);
            VeBieuDoCot(chartSanPhamBanChay, duLieu.SanPhamBanChay, false, true);
            VeBieuDoCot(chartDanhMuc, duLieu.DoanhThuTheoDanhMuc, true, true);
            VeBieuDoCot(chartChatLieu, duLieu.DoanhThuTheoChatLieu, true, true);
            VeBieuDoCot(chartNhanVien, duLieu.DoanhThuTheoNhanVien, true, true);
            VeBieuDoCot(chartNhaCungCap, duLieu.TienNhapTheoNhaCungCap, true, true);
            VeBieuDoCot(chartNhapTheoThang, duLieu.TienNhapTheoThang, true, false);
            VeBieuDoCot(chartSanPhamNhap, duLieu.SoLuongNhapTheoSanPham, false, true);
            VeBieuDoTron(chartBaoHanh, duLieu.BaoHanhTheoTrangThai);
            VeBieuDoEmail(chartEmail, duLieu.EmailTheoMau);

            dgvTonThap.DataSource = duLieu.TonThap;
            dgvSapHetHan.DataSource = duLieu.BaoHanhSapHetHan;
            DinhDangBangVanHanh();
        }

        private static string DinhDangTien(decimal giaTri) => giaTri.ToString("N0", VanHoaVietNam) + " đ";

        private static void KhoiTaoBieuDo(Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            var area = new ChartArea("Chinh");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 233, 237);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart.ChartAreas.Add(area);
        }

        private static void HienThiBieuDoRong(Chart chart)
        {
            chart.Titles.Add(new Title("Không có dữ liệu trong khoảng đã chọn",
                Docking.Top, new Font("Segoe UI", 9F), Color.Gray));
        }

        private static void VeBieuDoThoiGian(Chart chart, IList<DiemThongKe> duLieu)
        {
            KhoiTaoBieuDo(chart);
            if (duLieu.Count == 0) { HienThiBieuDoRong(chart); return; }
            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.SplineArea,
                Color = Color.FromArgb(110, 204, 164, 87),
                BorderColor = Color.FromArgb(173, 126, 48),
                BorderWidth = 2,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double,
                IsValueShownAsLabel = duLieu.Count <= 12
            };
            series.LabelFormat = "#,##0,, 'tr'";
            series.ToolTip = "#VALX: #VALY{N0} đ";
            foreach (DiemThongKe item in duLieu) series.Points.AddXY(item.Ten, item.GiaTri);
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0,, 'tr'";
            chart.Series.Add(series);
        }

        private static void VeBieuDoCot(Chart chart, IList<DiemThongKe> duLieu, bool laTien, bool thanhNgang)
        {
            KhoiTaoBieuDo(chart);
            if (duLieu.Count == 0) { HienThiBieuDoRong(chart); return; }
            var series = new Series("Giá trị")
            {
                ChartType = thanhNgang ? SeriesChartType.Bar : SeriesChartType.Column,
                Color = Color.FromArgb(42, 106, 133),
                IsValueShownAsLabel = duLieu.Count <= 8,
                YValueType = ChartValueType.Double
            };
            series.LabelFormat = laTien ? "#,##0,, 'tr'" : "#,##0";
            series.ToolTip = laTien ? "#VALX: #VALY{N0} đ" : "#VALX: #VALY{N0}";
            foreach (DiemThongKe item in duLieu.Reverse()) series.Points.AddXY(item.Ten, item.GiaTri);
            if (laTien) chart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0,, 'tr'";
            chart.Series.Add(series);
        }

        private static void VeBieuDoTron(Chart chart, IList<DiemThongKe> duLieu)
        {
            KhoiTaoBieuDo(chart);
            if (duLieu.Count == 0) { HienThiBieuDoRong(chart); return; }
            var legend = new Legend("ChuThich") { Docking = Docking.Right, Font = new Font("Segoe UI", 8F) };
            chart.Legends.Add(legend);
            var series = new Series("Bảo hành")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                Label = "#VALY",
                LegendText = "#VALX",
                ToolTip = "#VALX: #VALY phiếu"
            };
            foreach (DiemThongKe item in duLieu) series.Points.AddXY(item.Ten, item.GiaTri);
            chart.Series.Add(series);
        }

        private static void VeBieuDoEmail(Chart chart, IList<ThongKeEmailTheoMau> duLieu)
        {
            KhoiTaoBieuDo(chart);
            if (duLieu.Count == 0) { HienThiBieuDoRong(chart); return; }
            chart.Legends.Add(new Legend("ChuThich") { Docking = Docking.Top, Font = new Font("Segoe UI", 8F) });
            var thanhCong = new Series("Thành công") { ChartType = SeriesChartType.StackedBar, Color = Color.FromArgb(41, 128, 97), IsValueShownAsLabel = true };
            var thatBai = new Series("Thất bại") { ChartType = SeriesChartType.StackedBar, Color = Color.FromArgb(183, 70, 77), IsValueShownAsLabel = true };
            foreach (ThongKeEmailTheoMau item in duLieu.Reverse())
            {
                thanhCong.Points.AddXY(item.TenMau, item.ThanhCong);
                thatBai.Points.AddXY(item.TenMau, item.ThatBai);
            }
            chart.Series.Add(thanhCong);
            chart.Series.Add(thatBai);
        }

        private void DinhDangBangVanHanh()
        {
            if (dgvTonThap.Columns.Count > 0)
            {
                dgvTonThap.Columns[nameof(DongTonThap.MaSanPham)].HeaderText = "Mã SP";
                dgvTonThap.Columns[nameof(DongTonThap.TenSanPham)].HeaderText = "Sản phẩm";
                dgvTonThap.Columns[nameof(DongTonThap.DanhMuc)].HeaderText = "Danh mục";
                dgvTonThap.Columns[nameof(DongTonThap.SoLuongTon)].HeaderText = "Tồn";
            }
            if (dgvSapHetHan.Columns.Count > 0)
            {
                dgvSapHetHan.Columns[nameof(DongBaoHanhSapHetHan.MaHoaDon)].HeaderText = "Hóa đơn";
                dgvSapHetHan.Columns[nameof(DongBaoHanhSapHetHan.KhachHang)].HeaderText = "Khách hàng";
                dgvSapHetHan.Columns[nameof(DongBaoHanhSapHetHan.SanPham)].HeaderText = "Sản phẩm";
                dgvSapHetHan.Columns[nameof(DongBaoHanhSapHetHan.HanBaoHanh)].HeaderText = "Hạn BH";
                dgvSapHetHan.Columns[nameof(DongBaoHanhSapHetHan.HanBaoHanh)].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void btnXuatSanPham_Click(object sender, EventArgs e) => XuatExcel(LoaiXuatExcel.SanPham);
        private void btnXuatHoaDon_Click(object sender, EventArgs e) => XuatExcel(LoaiXuatExcel.HoaDon);
        private void btnXuatNhapHang_Click(object sender, EventArgs e) => XuatExcel(LoaiXuatExcel.NhapHang);
        private void btnXuatBaoHanh_Click(object sender, EventArgs e) => XuatExcel(LoaiXuatExcel.BaoHanh);
        private void btnXuatNhatKyEmail_Click(object sender, EventArgs e) => XuatExcel(LoaiXuatExcel.NhatKyEmail);

        private void XuatExcel(LoaiXuatExcel loai)
        {
            if (duLieuHienTai == null)
            {
                MessageBox.Show("Hãy tải dữ liệu thống kê trước khi xuất Excel.", "Chưa có dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ThongTinXuatExcel thongTin = duLieuHienTai.LayDuLieuXuat(loai);
            using (var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xlsx",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = thongTin.TenFile + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx",
                Title = "Chọn nơi lưu file Excel"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    XlsxExportService.Xuat(dialog.FileName, thongTin.TenTrangTinh, thongTin.CacCot, thongTin.CacDong);
                    MessageBox.Show("Đã xuất " + thongTin.CacDong.Count.ToString("N0", VanHoaVietNam) +
                                    " dòng dữ liệu đến:\n" + dialog.FileName,
                        "Xuất Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xuất file Excel. " + ex.Message, "Lỗi xuất dữ liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private enum LoaiXuatExcel { SanPham, HoaDon, NhapHang, BaoHanh, NhatKyEmail }

        private sealed class DuLieuThongKe
        {
            public decimal DoanhThu { get; private set; }
            public int SoHoaDon { get; private set; }
            public decimal GiaTriTrungBinhHoaDon { get; private set; }
            public decimal TongTienNhap { get; private set; }
            public int SoBaoHanhChuaTra { get; private set; }
            public int TongEmail { get; private set; }
            public decimal TyLeEmailThanhCong { get; private set; }
            public List<DiemThongKe> DoanhThuTheoThoiGian { get; private set; }
            public List<DiemThongKe> SanPhamBanChay { get; private set; }
            public List<DiemThongKe> DoanhThuTheoDanhMuc { get; private set; }
            public List<DiemThongKe> DoanhThuTheoChatLieu { get; private set; }
            public List<DiemThongKe> DoanhThuTheoNhanVien { get; private set; }
            public List<DiemThongKe> TienNhapTheoNhaCungCap { get; private set; }
            public List<DiemThongKe> TienNhapTheoThang { get; private set; }
            public List<DiemThongKe> SoLuongNhapTheoSanPham { get; private set; }
            public List<DiemThongKe> BaoHanhTheoTrangThai { get; private set; }
            public List<ThongKeEmailTheoMau> EmailTheoMau { get; private set; }
            public List<DongTonThap> TonThap { get; private set; }
            public List<DongBaoHanhSapHetHan> BaoHanhSapHetHan { get; private set; }
            private List<DongSanPhamXuat> SanPhamXuat { get; set; }
            private List<DongHoaDonXuat> HoaDonXuat { get; set; }
            private List<DongNhapHangXuat> NhapHangXuat { get; set; }
            private List<DongBaoHanhXuat> BaoHanhXuat { get; set; }
            private List<DongEmailXuat> EmailXuat { get; set; }

            public static DuLieuThongKe Tao(DateTime tuNgay, DateTime denNgay, int nguongTon,
                IList<HoaDon> hoaDons, IList<PhieuNhap> phieuNhaps, IList<PhieuBaoHanh> baoHanhs,
                IList<NhatKyGuiEmail> emailLogs, IList<SanPham> sanPhams, IList<ChiTietHoaDon> sapHetHan)
            {
                var ketQua = new DuLieuThongKe
                {
                    DoanhThu = hoaDons.Sum(hd => hd.ThanhTien),
                    SoHoaDon = hoaDons.Count,
                    TongTienNhap = phieuNhaps.Sum(pn => pn.TongTienNhap),
                    SoBaoHanhChuaTra = baoHanhs.Count(pbh => pbh.TrangThai != "DA_TRA"),
                    TongEmail = emailLogs.Count
                };
                ketQua.GiaTriTrungBinhHoaDon = ketQua.SoHoaDon == 0 ? 0 : ketQua.DoanhThu / ketQua.SoHoaDon;
                int emailThanhCong = emailLogs.Count(nk => nk.TrangThai == "THANH_CONG");
                ketQua.TyLeEmailThanhCong = ketQua.TongEmail == 0 ? 0 : (decimal)emailThanhCong / ketQua.TongEmail;

                bool gomTheoThang = (denNgay - tuNgay).TotalDays > 62;
                ketQua.DoanhThuTheoThoiGian = hoaDons
                    .GroupBy(hd => gomTheoThang
                        ? new DateTime(hd.NgayLap.Year, hd.NgayLap.Month, 1)
                        : hd.NgayLap.Date)
                    .OrderBy(nhom => nhom.Key)
                    .Select(nhom => new DiemThongKe(
                        nhom.Key.ToString(gomTheoThang ? "MM/yyyy" : "dd/MM"),
                        nhom.Sum(hd => hd.ThanhTien)))
                    .ToList();

                var cacDongBan = hoaDons.SelectMany(hd => hd.ChiTietHoaDons.Select(ct => new
                {
                    HoaDon = hd,
                    ChiTiet = ct,
                    DoanhThuDong = GiaTriThucNhanDong(hd, ct)
                })).ToList();

                ketQua.SanPhamBanChay = cacDongBan
                    .GroupBy(x => x.ChiTiet.SanPham?.TenSanPham ?? "Sản phẩm không xác định")
                    .Select(nhom => new DiemThongKe(nhom.Key, nhom.Sum(x => x.ChiTiet.SoLuong)))
                    .OrderByDescending(x => x.GiaTri).Take(8).ToList();
                ketQua.DoanhThuTheoDanhMuc = cacDongBan
                    .GroupBy(x => x.ChiTiet.SanPham?.DanhMuc?.TenDanhMuc ?? "Chưa phân loại")
                    .Select(nhom => new DiemThongKe(nhom.Key, nhom.Sum(x => x.DoanhThuDong)))
                    .OrderByDescending(x => x.GiaTri).Take(8).ToList();
                ketQua.DoanhThuTheoNhanVien = hoaDons
                    .GroupBy(hd => hd.NhanVien?.HoTen ?? "Không xác định")
                    .Select(nhom => new DiemThongKe(nhom.Key, nhom.Sum(hd => hd.ThanhTien)))
                    .OrderByDescending(x => x.GiaTri).Take(8).ToList();

                var doanhThuChatLieu = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
                foreach (var dong in cacDongBan)
                {
                    List<ChiTietChatLieu> thanhPhan = dong.ChiTiet.SanPham?.ChiTietChatLieux?.ToList() ?? new List<ChiTietChatLieu>();
                    decimal tongTrongLuong = thanhPhan.Sum(tp => tp.TrongLuong);
                    if (thanhPhan.Count == 0 || tongTrongLuong <= 0)
                        CongGiaTri(doanhThuChatLieu, "Chưa khai báo", dong.DoanhThuDong);
                    else
                        foreach (ChiTietChatLieu tp in thanhPhan)
                            CongGiaTri(doanhThuChatLieu, tp.ChatLieu?.TenChatLieu ?? "Không xác định", dong.DoanhThuDong * tp.TrongLuong / tongTrongLuong);
                }
                ketQua.DoanhThuTheoChatLieu = doanhThuChatLieu
                    .Select(item => new DiemThongKe(item.Key, item.Value))
                    .OrderByDescending(x => x.GiaTri).Take(8).ToList();

                ketQua.TienNhapTheoNhaCungCap = phieuNhaps
                    .GroupBy(pn => pn.NhaCungCap?.TenNhaCungCap ?? "Không xác định")
                    .Select(nhom => new DiemThongKe(nhom.Key, nhom.Sum(pn => pn.TongTienNhap)))
                    .OrderByDescending(x => x.GiaTri).Take(8).ToList();
                ketQua.TienNhapTheoThang = phieuNhaps
                    .GroupBy(pn => new DateTime(pn.NgayNhap.Year, pn.NgayNhap.Month, 1))
                    .OrderBy(nhom => nhom.Key)
                    .Select(nhom => new DiemThongKe(nhom.Key.ToString("MM/yyyy"), nhom.Sum(pn => pn.TongTienNhap)))
                    .ToList();
                ketQua.SoLuongNhapTheoSanPham = phieuNhaps
                    .SelectMany(pn => pn.ChiTietPhieuNhaps)
                    .GroupBy(ct => ct.SanPham?.TenSanPham ?? "Không xác định")
                    .Select(nhom => new DiemThongKe(nhom.Key, nhom.Sum(ct => ct.SoLuong)))
                    .OrderByDescending(x => x.GiaTri).Take(8).ToList();
                ketQua.BaoHanhTheoTrangThai = baoHanhs
                    .GroupBy(pbh => TenTrangThaiBaoHanh(pbh.TrangThai))
                    .Select(nhom => new DiemThongKe(nhom.Key, nhom.Count()))
                    .OrderByDescending(x => x.GiaTri).ToList();
                ketQua.EmailTheoMau = emailLogs
                    .GroupBy(nk => nk.MauEmail?.TenMau ?? "Không dùng mẫu")
                    .Select(nhom => new ThongKeEmailTheoMau(nhom.Key,
                        nhom.Count(nk => nk.TrangThai == "THANH_CONG"),
                        nhom.Count(nk => nk.TrangThai != "THANH_CONG")))
                    .OrderByDescending(x => x.ThanhCong + x.ThatBai).Take(8).ToList();

                ketQua.TonThap = sanPhams.Where(sp => sp.DangKinhDoanh && sp.SoLuongTon <= nguongTon)
                    .OrderBy(sp => sp.SoLuongTon).ThenBy(sp => sp.TenSanPham)
                    .Select(sp => new DongTonThap(sp)).ToList();
                ketQua.BaoHanhSapHetHan = sapHetHan.Select(ct => new DongBaoHanhSapHetHan(ct)).ToList();

                ketQua.SanPhamXuat = sanPhams.Select(sp => new DongSanPhamXuat(sp)).ToList();
                ketQua.HoaDonXuat = hoaDons.OrderByDescending(hd => hd.NgayLap).Select(hd => new DongHoaDonXuat(hd)).ToList();
                ketQua.NhapHangXuat = phieuNhaps.OrderByDescending(pn => pn.NgayNhap).Select(pn => new DongNhapHangXuat(pn)).ToList();
                ketQua.BaoHanhXuat = baoHanhs.OrderByDescending(pbh => pbh.NgayTiepNhan).Select(pbh => new DongBaoHanhXuat(pbh)).ToList();
                ketQua.EmailXuat = emailLogs.OrderByDescending(nk => nk.ThoiGianGui).Select(nk => new DongEmailXuat(nk)).ToList();
                return ketQua;
            }

            public ThongTinXuatExcel LayDuLieuXuat(LoaiXuatExcel loai)
            {
                switch (loai)
                {
                    case LoaiXuatExcel.SanPham: return TaoXuatSanPham(SanPhamXuat);
                    case LoaiXuatExcel.HoaDon: return TaoXuatHoaDon(HoaDonXuat);
                    case LoaiXuatExcel.NhapHang: return TaoXuatNhapHang(NhapHangXuat);
                    case LoaiXuatExcel.BaoHanh: return TaoXuatBaoHanh(BaoHanhXuat);
                    default: return TaoXuatEmail(EmailXuat);
                }
            }

            private static decimal GiaTriThucNhanDong(HoaDon hoaDon, ChiTietHoaDon chiTiet)
            {
                decimal giaTriDong = chiTiet.ThanhTien ?? chiTiet.DonGiaBan * chiTiet.SoLuong;
                return hoaDon.TongTien <= 0 ? giaTriDong : giaTriDong * hoaDon.ThanhTien / hoaDon.TongTien;
            }

            private static void CongGiaTri(IDictionary<string, decimal> duLieu, string khoa, decimal giaTri)
            {
                decimal hienTai;
                duLieu.TryGetValue(khoa, out hienTai);
                duLieu[khoa] = hienTai + giaTri;
            }
        }

        private sealed class DiemThongKe
        {
            public DiemThongKe(string ten, decimal giaTri) { Ten = ten; GiaTri = giaTri; }
            public string Ten { get; }
            public decimal GiaTri { get; }
        }

        private sealed class ThongKeEmailTheoMau
        {
            public ThongKeEmailTheoMau(string tenMau, int thanhCong, int thatBai) { TenMau = tenMau; ThanhCong = thanhCong; ThatBai = thatBai; }
            public string TenMau { get; }
            public int ThanhCong { get; }
            public int ThatBai { get; }
        }

        private sealed class DongTonThap
        {
            public DongTonThap(SanPham sp) { MaSanPham = $"SP{sp.SanPhamId:000000}"; TenSanPham = sp.TenSanPham; DanhMuc = sp.DanhMuc?.TenDanhMuc ?? "--"; SoLuongTon = sp.SoLuongTon; }
            public string MaSanPham { get; }
            public string TenSanPham { get; }
            public string DanhMuc { get; }
            public int SoLuongTon { get; }
        }

        private sealed class DongBaoHanhSapHetHan
        {
            public DongBaoHanhSapHetHan(ChiTietHoaDon ct) { MaHoaDon = $"HD{ct.HoaDonId:000000}"; KhachHang = ct.HoaDon?.KhachHang?.HoTen ?? "--"; SanPham = ct.SanPham?.TenSanPham ?? "--"; HanBaoHanh = ct.HanBaoHanh.GetValueOrDefault(); }
            public string MaHoaDon { get; }
            public string KhachHang { get; }
            public string SanPham { get; }
            public DateTime HanBaoHanh { get; }
        }

        private sealed class DongSanPhamXuat
        {
            public DongSanPhamXuat(SanPham sp)
            {
                Ma = $"SP{sp.SanPhamId:000000}"; Ten = sp.TenSanPham; DanhMuc = sp.DanhMuc?.TenDanhMuc ?? string.Empty;
                ChatLieu = string.Join(", ", sp.ChiTietChatLieux.Select(tp => (tp.ChatLieu?.TenChatLieu ?? "--") + " " + tp.TrongLuong.ToString("0.###", VanHoaVietNam) + " " + tp.DonViTinh));
                GiaVon = sp.GiaVon; GiaBan = sp.GiaBan; TonKho = sp.SoLuongTon; TrangThai = sp.DangKinhDoanh ? "Đang kinh doanh" : "Ngừng kinh doanh";
            }
            public string Ma, Ten, DanhMuc, ChatLieu, TrangThai; public decimal GiaVon, GiaBan; public int TonKho;
        }

        private sealed class DongHoaDonXuat
        {
            public DongHoaDonXuat(HoaDon hd) { Ma = $"HD{hd.HoaDonId:000000}"; Ngay = hd.NgayLap; KhachHang = hd.KhachHang?.HoTen ?? "--"; NhanVien = hd.NhanVien?.HoTen ?? "--"; TongTien = hd.TongTien; GiamGia = hd.GiamGia; ThanhTien = hd.ThanhTien; ThanhToan = hd.PhuongThucThanhToan; SoMatHang = hd.ChiTietHoaDons.Count; }
            public string Ma, KhachHang, NhanVien, ThanhToan; public DateTime Ngay; public decimal TongTien, GiamGia, ThanhTien; public int SoMatHang;
        }

        private sealed class DongNhapHangXuat
        {
            public DongNhapHangXuat(PhieuNhap pn) { Ma = $"PN{pn.PhieuNhapId:000000}"; Ngay = pn.NgayNhap; NhaCungCap = pn.NhaCungCap?.TenNhaCungCap ?? "--"; NhanVien = pn.NhanVien?.HoTen ?? "--"; TongTien = pn.TongTienNhap; SoMatHang = pn.ChiTietPhieuNhaps.Count; GhiChu = pn.GhiChu; }
            public string Ma, NhaCungCap, NhanVien, GhiChu; public DateTime Ngay; public decimal TongTien; public int SoMatHang;
        }

        private sealed class DongBaoHanhXuat
        {
            public DongBaoHanhXuat(PhieuBaoHanh pbh)
            {
                Ma = $"PBH{pbh.PhieuBaoHanhId:000000}"; NgayTiepNhan = pbh.NgayTiepNhan; MaHoaDon = $"HD{pbh.ChiTietHoaDon.HoaDonId:000000}";
                KhachHang = pbh.ChiTietHoaDon.HoaDon?.KhachHang?.HoTen ?? "--"; SanPham = pbh.ChiTietHoaDon.SanPham?.TenSanPham ?? "--";
                HanBaoHanh = pbh.ChiTietHoaDon.HanBaoHanh; NoiDung = pbh.NoiDungBaoHanh; TrangThai = TenTrangThaiBaoHanh(pbh.TrangThai);
                NgayTraDuKien = pbh.NgayTraDuKien; NgayTraThucTe = pbh.NgayTraThucTe; GhiChu = pbh.GhiChu;
            }
            public string Ma, MaHoaDon, KhachHang, SanPham, NoiDung, TrangThai, GhiChu; public DateTime NgayTiepNhan; public DateTime? HanBaoHanh, NgayTraDuKien, NgayTraThucTe;
        }

        private sealed class DongEmailXuat
        {
            public DongEmailXuat(NhatKyGuiEmail nk)
            {
                ThoiGian = nk.ThoiGianGui; EmailNhan = nk.EmailNhan; KhachHang = nk.KhachHang?.HoTen ?? string.Empty;
                MaHoaDon = nk.HoaDonId.HasValue ? $"HD{nk.HoaDonId:000000}" : string.Empty; MauEmail = nk.MauEmail?.TenMau ?? string.Empty;
                TieuDe = nk.TieuDe; LoaiGui = nk.LoaiGui == "HANG_LOAT" ? "Hàng loạt" : "Đơn";
                TrangThai = nk.TrangThai == "THANH_CONG" ? "Thành công" : "Thất bại";
                NguoiGui = nk.TaiKhoan?.NhanVien?.HoTen ?? nk.TaiKhoan?.TenDangNhap ?? "--"; GhiChu = nk.GhiChu;
            }
            public string EmailNhan, KhachHang, MaHoaDon, MauEmail, TieuDe, LoaiGui, TrangThai, NguoiGui, GhiChu; public DateTime ThoiGian;
        }

        private sealed class ThongTinXuatExcel
        {
            public ThongTinXuatExcel(string tenFile, string tenTrangTinh, IReadOnlyList<CotXuatExcel> cacCot, List<object[]> cacDong) { TenFile = tenFile; TenTrangTinh = tenTrangTinh; CacCot = cacCot; CacDong = cacDong; }
            public string TenFile { get; }
            public string TenTrangTinh { get; }
            public IReadOnlyList<CotXuatExcel> CacCot { get; }
            public List<object[]> CacDong { get; }
        }

        private static ThongTinXuatExcel TaoXuatSanPham(IEnumerable<DongSanPhamXuat> ds) => new ThongTinXuatExcel("DanhSachSanPham", "Sản phẩm",
            new[] { C("Mã sản phẩm", 14), C("Tên sản phẩm", 32), C("Danh mục", 22), C("Chất liệu", 38), C("Giá vốn", 18, KieuDuLieuExcel.TienTe), C("Giá bán", 18, KieuDuLieuExcel.TienTe), C("Tồn kho", 12, KieuDuLieuExcel.SoNguyen), C("Trạng thái", 20) },
            ds.Select(x => new object[] { x.Ma, x.Ten, x.DanhMuc, x.ChatLieu, x.GiaVon, x.GiaBan, x.TonKho, x.TrangThai }).ToList());

        private static ThongTinXuatExcel TaoXuatHoaDon(IEnumerable<DongHoaDonXuat> ds) => new ThongTinXuatExcel("HoaDonDaThanhToan", "Hóa đơn",
            new[] { C("Mã hóa đơn", 14), C("Ngày lập", 20, KieuDuLieuExcel.NgayGio), C("Khách hàng", 28), C("Nhân viên", 26), C("Số mặt hàng", 14, KieuDuLieuExcel.SoNguyen), C("Tổng tiền", 18, KieuDuLieuExcel.TienTe), C("Giảm giá", 18, KieuDuLieuExcel.TienTe), C("Thành tiền", 18, KieuDuLieuExcel.TienTe), C("Thanh toán", 18) },
            ds.Select(x => new object[] { x.Ma, x.Ngay, x.KhachHang, x.NhanVien, x.SoMatHang, x.TongTien, x.GiamGia, x.ThanhTien, x.ThanhToan }).ToList());

        private static ThongTinXuatExcel TaoXuatNhapHang(IEnumerable<DongNhapHangXuat> ds) => new ThongTinXuatExcel("PhieuNhapHoanThanh", "Nhập hàng",
            new[] { C("Mã phiếu nhập", 16), C("Ngày nhập", 20, KieuDuLieuExcel.NgayGio), C("Nhà cung cấp", 30), C("Nhân viên", 26), C("Số mặt hàng", 14, KieuDuLieuExcel.SoNguyen), C("Tổng tiền nhập", 20, KieuDuLieuExcel.TienTe), C("Ghi chú", 36) },
            ds.Select(x => new object[] { x.Ma, x.Ngay, x.NhaCungCap, x.NhanVien, x.SoMatHang, x.TongTien, x.GhiChu }).ToList());

        private static ThongTinXuatExcel TaoXuatBaoHanh(IEnumerable<DongBaoHanhXuat> ds) => new ThongTinXuatExcel("PhieuBaoHanh", "Bảo hành",
            new[] { C("Mã phiếu", 14), C("Ngày tiếp nhận", 20, KieuDuLieuExcel.NgayGio), C("Mã hóa đơn", 14), C("Khách hàng", 28), C("Sản phẩm", 32), C("Hạn bảo hành", 16, KieuDuLieuExcel.Ngay), C("Nội dung", 38), C("Trạng thái", 18), C("Ngày trả dự kiến", 18, KieuDuLieuExcel.Ngay), C("Ngày trả thực tế", 18, KieuDuLieuExcel.Ngay), C("Ghi chú", 34) },
            ds.Select(x => new object[] { x.Ma, x.NgayTiepNhan, x.MaHoaDon, x.KhachHang, x.SanPham, x.HanBaoHanh, x.NoiDung, x.TrangThai, x.NgayTraDuKien, x.NgayTraThucTe, x.GhiChu }).ToList());

        private static ThongTinXuatExcel TaoXuatEmail(IEnumerable<DongEmailXuat> ds) => new ThongTinXuatExcel("NhatKyGuiEmail", "Nhật ký email",
            new[] { C("Thời gian gửi", 20, KieuDuLieuExcel.NgayGio), C("Email nhận", 32), C("Khách hàng", 28), C("Mã hóa đơn", 14), C("Mẫu email", 26), C("Tiêu đề", 42), C("Loại gửi", 14), C("Trạng thái", 16), C("Người gửi", 26), C("Ghi chú", 42) },
            ds.Select(x => new object[] { x.ThoiGian, x.EmailNhan, x.KhachHang, x.MaHoaDon, x.MauEmail, x.TieuDe, x.LoaiGui, x.TrangThai, x.NguoiGui, x.GhiChu }).ToList());

        private static CotXuatExcel C(string ten, double rong, KieuDuLieuExcel kieu = KieuDuLieuExcel.VanBan) => new CotXuatExcel(ten, rong, kieu);

        private static string TenTrangThaiBaoHanh(string ma)
        {
            switch (ma)
            {
                case "TIEP_NHAN": return "Tiếp nhận";
                case "DANG_XU_LY": return "Đang xử lý";
                case "HOAN_THANH": return "Hoàn thành";
                case "DA_TRA": return "Đã trả";
                default: return string.IsNullOrWhiteSpace(ma) ? "Không xác định" : ma;
            }
        }
    }
}

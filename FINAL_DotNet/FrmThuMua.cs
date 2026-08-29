using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmThuMua : Form
    {
        private const string CotMaNguon = "MaPhieuNguon";
        private const string CotNgay = "NgayThuMua";
        private const string CotNhanVien = "MaNhanVien";
        private const string CotKhachHang = "SoDienThoaiKhachHang";
        private const string CotChatLieu = "TenChatLieu";
        private const string CotSanPham = "MaSanPham";
        private const string CotTenMon = "TenSanPhamThu";
        private const string CotTrongLuong = "TrongLuong";
        private const string CotDonVi = "DonViTinh";
        private const string CotDonGia = "DonGiaThuMua";
        private const string CotTrangThai = "TrangThai";
        private const string CotGhiChu = "GhiChu";
        private static readonly string[] CacCotBatBuoc =
        {
            CotMaNguon, CotNgay, CotNhanVien, CotKhachHang, CotChatLieu,
            CotTenMon, CotTrongLuong, CotDonVi, CotDonGia, CotTrangThai
        };
        private static readonly CultureInfo VanHoaVietNam = CultureInfo.GetCultureInfo("vi-VN");

        private List<DongImportThuMua> cacDongImport = new List<DongImportThuMua>();
        private List<PhieuThuMuaHienThi> cacPhieuHienTai = new List<PhieuThuMuaHienThi>();
        private int? phieuDangChonId;

        public FrmThuMua()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            cboTrangThai.SelectedIndex = 0;
            GanSuKien();
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void GanSuKien()
        {
            Load += FrmThuMua_Load;
            btnChonTep.Click += btnChonTep_Click;
            btnTaiMau.Click += btnTaiMau_Click;
            btnKiemTra.Click += (sender, args) => ThucHienKiemTraExcel(txtTepExcel.Text);
            btnImport.Click += btnImport_Click;
            btnXuatLoi.Click += btnXuatLoi_Click;
            btnTim.Click += (sender, args) => TaiDanhSach();
            btnTaiLai.Click += btnTaiLai_Click;
            btnXuatExcel.Click += btnXuatExcel_Click;
            btnXemBaoCao.Click += btnXemBaoCao_Click;
            dgvPhieu.SelectionChanged += dgvPhieu_SelectionChanged;
            txtTuKhoa.KeyDown += (sender, args) =>
            {
                if (args.KeyCode != Keys.Enter) return;
                args.SuppressKeyPress = true;
                TaiDanhSach();
            };

            // Kéo thả Drag & Drop file Excel
            this.DragEnter += FrmThuMua_DragEnter;
            this.DragDrop += FrmThuMua_DragDrop;
            tabImport.DragEnter += FrmThuMua_DragEnter;
            tabImport.DragDrop += FrmThuMua_DragDrop;
            pnlCommandImport.DragEnter += FrmThuMua_DragEnter;
            pnlCommandImport.DragDrop += FrmThuMua_DragDrop;
            txtTepExcel.DragEnter += FrmThuMua_DragEnter;
            txtTepExcel.DragDrop += FrmThuMua_DragDrop;
            dgvXemTruoc.DragEnter += FrmThuMua_DragEnter;
            dgvXemTruoc.DragDrop += FrmThuMua_DragDrop;
        }

        private void FrmThuMua_Load(object sender, EventArgs e)
        {
            if (!CurrentUserSession.DaDangNhap)
            {
                MessageBox.Show("Phiên đăng nhập đã kết thúc.", "Chưa đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeginInvoke(new Action(Close));
                return;
            }
            bool laQuanTri = CurrentUserSession.HienTai.LaQuanTriVien;
            btnChonTep.Enabled = laQuanTri;
            btnKiemTra.Enabled = laQuanTri;
            btnImport.Enabled = false;
            btnXuatLoi.Enabled = false;
            if (!laQuanTri)
                lblKetQuaImport.Text = "Chỉ quản trị viên được import. Bạn vẫn có thể tra cứu và xuất báo cáo.";
            TaiDanhSach();
        }

        private void FrmThuMua_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void FrmThuMua_DragDrop(object sender, DragEventArgs e)
        {
            if (!CurrentUserSession.HienTai.LaQuanTriVien) return;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0 && Path.GetExtension(files[0]).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                txtTepExcel.Text = files[0];
                ThucHienKiemTraExcel(files[0]);
            }
        }

        private void btnChonTep_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                Title = "Chọn file dữ liệu thu mua"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                txtTepExcel.Text = dialog.FileName;
                ThucHienKiemTraExcel(dialog.FileName);
            }
        }

        private void btnTaiMau_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                AddExtension = true,
                FileName = "MauImportThuMua.xlsx",
                Title = "Lưu file Excel mẫu"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                var columns = new[]
                {
                    C(CotMaNguon, 18), C(CotNgay, 20, KieuDuLieuExcel.NgayGio), C(CotNhanVien, 15),
                    C(CotKhachHang, 23), C(CotChatLieu, 22), C(CotSanPham, 15), C(CotTenMon, 32),
                    C(CotTrongLuong, 15, KieuDuLieuExcel.SoThapPhan), C(CotDonVi, 14),
                    C(CotDonGia, 20, KieuDuLieuExcel.TienTe), C(CotTrangThai, 18), C(CotGhiChu, 35)
                };
                var rows = new List<object[]>
                {
                    new object[] { "TM-2026-001", DateTime.Today.AddDays(-1).AddHours(9), "NV000005", "0912000001", "Vàng 24K", "SP000008", "Nhẫn vàng cũ", 6.2M, "gram", 2000000M, "HOAN_THANH", "Dòng mẫu - có thể xóa" },
                    new object[] { "TM-2026-001", DateTime.Today.AddDays(-1).AddHours(9), "NV000005", "0912000001", "Vàng 18K", string.Empty, "Dây chuyền vàng cũ", 9.5M, "gram", 1450000M, "HOAN_THANH", "" }
                };
                try
                {
                    XlsxExportService.Xuat(dialog.FileName, "Thu mua", columns, rows);
                    if (MessageBox.Show("Đã tạo file mẫu tại:\n" + dialog.FileName + "\n\nBạn có muốn mở file Excel mẫu ngay không?",
                            "Tạo file mẫu thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tạo file mẫu. " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ThucHienKiemTraExcel(string duongDanFile)
        {
            if (!CurrentUserSession.HienTai.LaQuanTriVien) return;
            if (string.IsNullOrWhiteSpace(duongDanFile) || !File.Exists(duongDanFile))
            {
                lblKetQuaImport.Text = "Vui lòng chọn file .xlsx hợp lệ.";
                lblKetQuaImport.ForeColor = Color.Firebrick;
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                prgImport.Value = 20;
                BangTinhXlsx bangTinh = XlsxImportService.DocTrangTinhDauTien(duongDanFile);
                string thieuCot = CacCotBatBuoc.FirstOrDefault(cot =>
                    !bangTinh.CacCot.Any(header => string.Equals(header, cot, StringComparison.OrdinalIgnoreCase)));
                if (thieuCot != null) throw new InvalidOperationException("File Excel thiếu cột bắt buộc: " + thieuCot + ".");
                if (bangTinh.CacDong.Count == 0) throw new InvalidOperationException("File Excel không có dòng dữ liệu.");

                prgImport.Value = 60;
                cacDongImport = KiemTraCacDong(bangTinh.CacDong);
                dgvXemTruoc.DataSource = cacDongImport.Select(item => item.TaoHienThi()).ToList();
                int soLoi = cacDongImport.Count(item => !item.HopLe);
                int soPhieu = cacDongImport.Where(item => item.HopLe)
                    .Select(item => item.MaPhieuNguon).Distinct(StringComparer.OrdinalIgnoreCase).Count();

                prgImport.Value = 100;
                lblKetQuaImport.Text = soLoi == 0
                    ? $"Hợp lệ: {cacDongImport.Count} dòng / {soPhieu} phiếu. Có thể import vào CSDL."
                    : $"Có {soLoi}/{cacDongImport.Count} dòng lỗi. Bạn có thể bấm 'Xuất danh sách lỗi' để chỉnh sửa.";
                lblKetQuaImport.ForeColor = soLoi == 0 ? Color.FromArgb(30, 115, 75) : Color.Firebrick;
                btnImport.Enabled = soLoi == 0 && soPhieu > 0;
                btnXuatLoi.Enabled = soLoi > 0;
                DinhDangLuoiLoi();
            }
            catch (Exception ex)
            {
                prgImport.Value = 0;
                cacDongImport.Clear();
                dgvXemTruoc.DataSource = null;
                btnImport.Enabled = false;
                btnXuatLoi.Enabled = false;
                lblKetQuaImport.ForeColor = Color.Firebrick;
                lblKetQuaImport.Text = ex.Message;
            }
            finally { Cursor = Cursors.Default; }
        }

        private List<DongImportThuMua> KiemTraCacDong(IEnumerable<DongBangTinhXlsx> rows)
        {
            var result = rows.Select(TaoDongImport).ToList();
            bool tuDongTaoKhach = chkTuDongTaoKhach.Checked;

            using (var db = DatabaseConnection.CreateContext())
            {
                var employees = db.NhanViens.AsNoTracking().ToDictionary(item => item.NhanVienId);
                var customers = db.KhachHangs.AsNoTracking().ToList()
                    .Where(item => !string.IsNullOrWhiteSpace(item.SoDienThoai))
                    .ToDictionary(item => item.SoDienThoai.Trim(), StringComparer.OrdinalIgnoreCase);
                var materials = db.ChatLieux.AsNoTracking().ToList()
                    .ToDictionary(item => item.TenChatLieu.Trim(), StringComparer.OrdinalIgnoreCase);
                var products = db.SanPhams.AsNoTracking().ToDictionary(item => item.SanPhamId);
                var importedMarkers = db.PhieuThuMuas.AsNoTracking().Select(item => item.MaPhieuNguon).ToList()
                    .Where(item => !string.IsNullOrWhiteSpace(item))
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (DongImportThuMua row in result)
                {
                    if (row.MaPhieuNguon != null && importedMarkers.Contains(row.MaPhieuNguon))
                        row.ThemLoi("Mã phiếu nguồn đã được import");
                    if (row.NhanVienId.HasValue && !employees.ContainsKey(row.NhanVienId.Value))
                        row.ThemLoi("Không tìm thấy nhân viên");

                    KhachHang customer;
                    if (row.SoDienThoaiKhachHang != null && customers.TryGetValue(row.SoDienThoaiKhachHang, out customer))
                    {
                        row.KhachHangId = customer.KhachHangId;
                    }
                    else
                    {
                        if (tuDongTaoKhach && !string.IsNullOrWhiteSpace(row.SoDienThoaiKhachHang))
                        {
                            row.KhachHangId = null; // Sẽ tự động tạo mới khi import
                        }
                        else
                        {
                            row.ThemLoi("Không tìm thấy khách hàng theo số điện thoại");
                        }
                    }

                    ChatLieu material;
                    if (row.TenChatLieu != null && materials.TryGetValue(row.TenChatLieu, out material))
                        row.ChatLieuId = material.ChatLieuId;
                    else row.ThemLoi("Không tìm thấy chất liệu");

                    if (row.SanPhamId.HasValue && !products.ContainsKey(row.SanPhamId.Value))
                        row.ThemLoi("Không tìm thấy sản phẩm");
                }
            }

            foreach (IGrouping<string, DongImportThuMua> group in result
                         .Where(item => item.MaPhieuNguon != null)
                         .GroupBy(item => item.MaPhieuNguon, StringComparer.OrdinalIgnoreCase))
            {
                DongImportThuMua first = group.First();
                foreach (DongImportThuMua row in group.Skip(1))
                {
                    if (row.NgayThuMua != first.NgayThuMua || row.NhanVienId != first.NhanVienId ||
                        row.SoDienThoaiKhachHang != first.SoDienThoaiKhachHang || row.TrangThai != first.TrangThai ||
                        !string.Equals(row.GhiChu, first.GhiChu, StringComparison.Ordinal))
                        row.ThemLoi("Thông tin đầu phiếu không đồng nhất với các dòng cùng MaPhieuNguon");
                }
                var duplicateProducts = group.Where(item => item.SanPhamId.HasValue)
                    .GroupBy(item => item.SanPhamId.Value).Where(item => item.Count() > 1).Select(item => item.Key).ToList();
                foreach (DongImportThuMua row in group.Where(item =>
                             item.SanPhamId.HasValue && duplicateProducts.Contains(item.SanPhamId.Value)))
                    row.ThemLoi("Một sản phẩm không được lặp trong cùng phiếu");

                decimal total = group.Sum(item => Math.Round(
                    checked(item.TrongLuong * item.DonGiaThuMua), 2, MidpointRounding.AwayFromZero));
                if (total > 9999999999999999.99M)
                    foreach (DongImportThuMua row in group)
                        row.ThemLoi("Tổng tiền phiếu vượt giới hạn DECIMAL(18,2)");
            }
            return result;
        }

        private static DongImportThuMua TaoDongImport(DongBangTinhXlsx source)
        {
            var row = new DongImportThuMua { SoDongExcel = source.SoDong };
            row.MaPhieuNguon = ChuanHoaMaNguon(DocChuoi(source.Lay(CotMaNguon)), row);
            row.NgayThuMua = DocNgay(source.Lay(CotNgay), CotNgay, row);
            row.NhanVienId = DocMaId(DocChuoi(source.Lay(CotNhanVien)), "NV", CotNhanVien, false, row);
            row.SoDienThoaiKhachHang = DocChuoi(source.Lay(CotKhachHang));
            row.TenChatLieu = DocChuoi(source.Lay(CotChatLieu));
            row.SanPhamId = DocMaId(DocChuoi(source.Lay(CotSanPham)), "SP", CotSanPham, true, row);
            row.TenSanPhamThu = DocChuoi(source.Lay(CotTenMon));
            row.TrongLuong = DocSoDuong(source.Lay(CotTrongLuong), CotTrongLuong, row);
            row.DonViTinh = DocChuoi(source.Lay(CotDonVi));
            row.DonGiaThuMua = DocSoDuong(source.Lay(CotDonGia), CotDonGia, row);
            row.TrangThai = (DocChuoi(source.Lay(CotTrangThai)) ?? string.Empty).ToUpperInvariant();
            row.GhiChu = DocChuoi(source.Lay(CotGhiChu));

            if (row.MaPhieuNguon == null) row.ThemLoi("Thiếu MaPhieuNguon");
            if (!row.NgayThuMua.HasValue) row.ThemLoi("Ngày thu mua không hợp lệ");
            if (!row.NhanVienId.HasValue) row.ThemLoi("Mã nhân viên không hợp lệ");
            if (string.IsNullOrWhiteSpace(row.SoDienThoaiKhachHang)) row.ThemLoi("Thiếu số điện thoại khách hàng");
            if (string.IsNullOrWhiteSpace(row.TenChatLieu)) row.ThemLoi("Thiếu tên chất liệu");
            if (string.IsNullOrWhiteSpace(row.TenSanPhamThu)) row.ThemLoi("Thiếu tên sản phẩm thu");
            if (row.TrongLuong <= 0) row.ThemLoi("Trọng lượng phải lớn hơn 0");
            if (string.IsNullOrWhiteSpace(row.DonViTinh)) row.ThemLoi("Thiếu đơn vị tính");
            if (row.DonGiaThuMua <= 0) row.ThemLoi("Đơn giá phải lớn hơn 0");
            if (row.TrangThai != "HOAN_THANH" && row.TrangThai != "DA_HUY")
                row.ThemLoi("Trạng thái chỉ nhận HOAN_THANH hoặc DA_HUY");
            if (row.TenSanPhamThu != null && row.TenSanPhamThu.Length > 150) row.ThemLoi("Tên sản phẩm thu vượt 150 ký tự");
            if (row.DonViTinh != null && row.DonViTinh.Length > 20) row.ThemLoi("Đơn vị tính vượt 20 ký tự");
            if (row.TrongLuong > 9999999.999M) row.ThemLoi("Trọng lượng vượt giới hạn DECIMAL(10,3)");
            if (row.DonGiaThuMua > 9999999999999999.99M) row.ThemLoi("Đơn giá vượt giới hạn DECIMAL(18,2)");
            if (row.GhiChu != null && row.GhiChu.Length > 500) row.ThemLoi("Ghi chú vượt 500 ký tự");
            return row;
        }

        private void btnXuatLoi_Click(object sender, EventArgs e)
        {
            var invalidRows = cacDongImport.Where(item => !item.HopLe).ToList();
            if (invalidRows.Count == 0)
            {
                MessageBox.Show("Hiện tại không có dòng dữ liệu lỗi nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                AddExtension = true,
                FileName = "DanhSachLoi_Import_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx",
                Title = "Lưu danh sách dòng lỗi"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                var columns = new[]
                {
                    C("Dòng Excel", 12, KieuDuLieuExcel.SoNguyen),
                    C(CotMaNguon, 18), C(CotNgay, 20, KieuDuLieuExcel.NgayGio), C(CotNhanVien, 15),
                    C(CotKhachHang, 23), C(CotChatLieu, 22), C(CotSanPham, 15), C(CotTenMon, 32),
                    C(CotTrongLuong, 15, KieuDuLieuExcel.SoThapPhan), C(CotDonVi, 14),
                    C(CotDonGia, 20, KieuDuLieuExcel.TienTe), C(CotTrangThai, 18), C(CotGhiChu, 35),
                    C("Chi tiết lỗi cần sửa", 45)
                };
                List<object[]> rows = invalidRows.Select(item => new object[]
                {
                    item.SoDongExcel, item.MaPhieuNguon ?? string.Empty, item.NgayThuMua,
                    item.NhanVienId.HasValue ? $"NV{item.NhanVienId:000000}" : string.Empty,
                    item.SoDienThoaiKhachHang ?? string.Empty, item.TenChatLieu ?? string.Empty,
                    item.SanPhamId.HasValue ? $"SP{item.SanPhamId:000000}" : string.Empty,
                    item.TenSanPhamThu ?? string.Empty, item.TrongLuong, item.DonViTinh ?? string.Empty,
                    item.DonGiaThuMua, item.TrangThai ?? string.Empty, item.GhiChu ?? string.Empty,
                    string.Join("; ", item.LayCacLoi())
                }).ToList();

                try
                {
                    XlsxExportService.Xuat(dialog.FileName, "Danh sách lỗi", columns, rows);
                    if (MessageBox.Show("Đã xuất " + rows.Count + " dòng lỗi đến:\n" + dialog.FileName +
                                        "\n\nBạn có muốn mở file Excel lỗi ngay không?",
                            "Xuất file lỗi thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xuất file lỗi. " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!CurrentUserSession.HienTai.LaQuanTriVien || cacDongImport.Count == 0 || cacDongImport.Any(item => !item.HopLe))
                return;
            int soPhieu = cacDongImport.Select(item => item.MaPhieuNguon).Distinct(StringComparer.OrdinalIgnoreCase).Count();
            if (MessageBox.Show($"Import {soPhieu} phiếu / {cacDongImport.Count} dòng vào CSDL?",
                    "Xác nhận import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                prgImport.Value = 10;
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    List<string> markers = db.PhieuThuMuas.Select(item => item.MaPhieuNguon).ToList();
                    var existing = markers.Where(item => !string.IsNullOrWhiteSpace(item))
                        .ToHashSet(StringComparer.OrdinalIgnoreCase);

                    var groups = cacDongImport.GroupBy(item => item.MaPhieuNguon, StringComparer.OrdinalIgnoreCase).ToList();
                    int processed = 0;

                    foreach (IGrouping<string, DongImportThuMua> group in groups)
                    {
                        if (existing.Contains(group.Key))
                            throw new InvalidOperationException("Mã phiếu nguồn đã được import: " + group.Key + ".");
                        DongImportThuMua first = group.First();

                        if (!db.NhanViens.Any(item => item.NhanVienId == first.NhanVienId.Value))
                            throw new InvalidOperationException("Nhân viên vừa thay đổi. Hãy kiểm tra file lại.");

                        int finalKhachHangId;
                        if (first.KhachHangId.HasValue)
                        {
                            finalKhachHangId = first.KhachHangId.Value;
                        }
                        else
                        {
                            // Tự động tạo khách hàng mới nếu chưa có
                            string sdt = first.SoDienThoaiKhachHang;
                            KhachHang newCust = db.KhachHangs.FirstOrDefault(k => k.SoDienThoai == sdt);
                            if (newCust == null)
                            {
                                newCust = new KhachHang
                                {
                                    HoTen = "Khách hàng " + sdt,
                                    SoDienThoai = sdt,
                                    DiaChi = "Tạo tự động từ file Excel",
                                    DiemTichLuy = 0,
                                    ChoPhepNhanEmail = false,
                                    DangHoatDong = true
                                };
                                db.KhachHangs.Add(newCust);
                                db.SaveChanges();
                            }
                            finalKhachHangId = newCust.KhachHangId;
                        }

                        var materialIds = group.Select(item => item.ChatLieuId.Value).Distinct().ToList();
                        if (db.ChatLieux.Count(item => materialIds.Contains(item.ChatLieuId)) != materialIds.Count)
                            throw new InvalidOperationException("Chất liệu vừa thay đổi. Hãy kiểm tra file lại.");

                        decimal total = group.Sum(item => Math.Round(
                            checked(item.TrongLuong * item.DonGiaThuMua), 2, MidpointRounding.AwayFromZero));
                        var receipt = new PhieuThuMua
                        {
                            MaPhieuNguon = group.Key,
                            NhanVienId = first.NhanVienId.Value,
                            KhachHangId = finalKhachHangId,
                            NgayThuMua = first.NgayThuMua.Value,
                            TongTienThuMua = total,
                            TrangThai = first.TrangThai,
                            GhiChu = first.GhiChu
                        };
                        db.PhieuThuMuas.Add(receipt);
                        foreach (DongImportThuMua row in group)
                        {
                            receipt.ChiTietPhieuThuMuas.Add(new ChiTietPhieuThuMua
                            {
                                ChatLieuId = row.ChatLieuId.Value,
                                SanPhamId = row.SanPhamId,
                                TenSanPhamThu = row.TenSanPhamThu,
                                TrongLuong = row.TrongLuong,
                                DonViTinh = row.DonViTinh,
                                DonGiaThuMua = row.DonGiaThuMua
                            });
                        }

                        processed++;
                        prgImport.Value = Math.Min(95, 10 + (int)(processed * 80.0 / groups.Count));
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }

                prgImport.Value = 100;
                lblKetQuaImport.ForeColor = Color.FromArgb(30, 115, 75);
                lblKetQuaImport.Text = $"Import thành công {soPhieu} phiếu / {cacDongImport.Count} dòng.";
                btnImport.Enabled = false;
                btnXuatLoi.Enabled = false;
                TaiDanhSach();
                tabChinh.SelectedIndex = 1;
                MessageBox.Show("Import dữ liệu thu mua thành công.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                prgImport.Value = 0;
                lblKetQuaImport.Text = "Không thể import vì dữ liệu vi phạm khóa hoặc ràng buộc CSDL.";
                lblKetQuaImport.ForeColor = Color.Firebrick;
            }
            catch (Exception ex)
            {
                prgImport.Value = 0;
                lblKetQuaImport.Text = "Import thất bại: " + ex.Message;
                lblKetQuaImport.ForeColor = Color.Firebrick;
            }
            finally { Cursor = Cursors.Default; }
        }

        private void TaiDanhSach()
        {
            if (dtpTuNgay.Checked && dtpDenNgay.Checked && dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                HienThiLoi("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }
            try
            {
                string keyword = txtTuKhoa.Text.Trim();
                int? receiptId = DocMaHienThi(keyword, "PTM");
                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<PhieuThuMua> query = db.PhieuThuMuas
                        .Include(item => item.NhanVien)
                        .Include(item => item.KhachHang)
                        .Include(item => item.ChiTietPhieuThuMuas.Select(detail => detail.ChatLieu))
                        .Include(item => item.ChiTietPhieuThuMuas.Select(detail => detail.SanPham))
                        .AsNoTracking();
                    if (dtpTuNgay.Checked)
                    {
                        DateTime from = dtpTuNgay.Value.Date;
                        query = query.Where(item => item.NgayThuMua >= from);
                    }
                    if (dtpDenNgay.Checked)
                    {
                        DateTime to = dtpDenNgay.Value.Date.AddDays(1);
                        query = query.Where(item => item.NgayThuMua < to);
                    }
                    if (cboTrangThai.SelectedIndex == 1) query = query.Where(item => item.TrangThai == "HOAN_THANH");
                    else if (cboTrangThai.SelectedIndex == 2) query = query.Where(item => item.TrangThai == "DA_HUY");
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        if (receiptId.HasValue) query = query.Where(item => item.PhieuThuMuaId == receiptId.Value);
                        else query = query.Where(item =>
                            item.KhachHang.HoTen.Contains(keyword) ||
                            item.KhachHang.SoDienThoai.Contains(keyword) ||
                            item.NhanVien.HoTen.Contains(keyword) ||
                            (item.MaPhieuNguon != null && item.MaPhieuNguon.Contains(keyword)) ||
                            (item.GhiChu != null && item.GhiChu.Contains(keyword)) ||
                            item.ChiTietPhieuThuMuas.Any(detail =>
                                detail.TenSanPhamThu.Contains(keyword) || detail.ChatLieu.TenChatLieu.Contains(keyword)));
                    }
                    cacPhieuHienTai = query.OrderByDescending(item => item.NgayThuMua)
                        .ThenByDescending(item => item.PhieuThuMuaId).ToList()
                        .Select(item => new PhieuThuMuaHienThi(item)).ToList();
                }
                dgvPhieu.DataSource = cacPhieuHienTai;
                dgvChiTiet.DataSource = null;
                dgvPhieu.ClearSelection();
                phieuDangChonId = null;
                CapNhatThongKe();
                HienThiLoi(string.Empty);
                DinhDangLuoiTraCuu();
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải dữ liệu thu mua. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void CapNhatThongKe()
        {
            List<PhieuThuMuaHienThi> completed = cacPhieuHienTai.Where(item => item.TrangThai == "HOAN_THANH").ToList();
            lblSoPhieu.Text = cacPhieuHienTai.Count.ToString("N0", VanHoaVietNam);
            lblTongTien.Text = completed.Sum(item => item.TongTienThuMua).ToString("N0", VanHoaVietNam) + " đ";
            lblTongTrongLuong.Text = completed.Sum(item => item.ChiTiet.Sum(detail => detail.TrongLuong)).ToString("N3", VanHoaVietNam);
            lblSoKhachHang.Text = completed.Select(item => item.KhachHangId).Distinct().Count().ToString("N0", VanHoaVietNam);
        }

        private void dgvPhieu_SelectionChanged(object sender, EventArgs e)
        {
            var item = dgvPhieu.CurrentRow?.DataBoundItem as PhieuThuMuaHienThi;
            if (item == null) return;
            phieuDangChonId = item.PhieuThuMuaId;
            dgvChiTiet.DataSource = item.ChiTiet;
            DinhDangLuoiChiTiet();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            dtpTuNgay.Checked = false;
            dtpDenNgay.Checked = false;
            cboTrangThai.SelectedIndex = 0;
            TaiDanhSach();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (cacPhieuHienTai.Count == 0)
            {
                HienThiLoi("Không có dữ liệu để xuất Excel.");
                return;
            }
            using (var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                AddExtension = true,
                FileName = "DuLieuThuMua_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                var columns = new[]
                {
                    C("Mã phiếu", 15), C("Mã nguồn", 18), C("Ngày thu mua", 20, KieuDuLieuExcel.NgayGio),
                    C("Khách hàng", 28), C("Số điện thoại", 18), C("Nhân viên", 26), C("Chất liệu", 20),
                    C("Mã sản phẩm", 15), C("Tên sản phẩm thu", 32), C("Trọng lượng", 15, KieuDuLieuExcel.SoThapPhan),
                    C("Đơn vị", 12), C("Đơn giá", 18, KieuDuLieuExcel.TienTe), C("Thành tiền", 18, KieuDuLieuExcel.TienTe),
                    C("Trạng thái", 16), C("Ghi chú", 35)
                };
                List<object[]> rows = cacPhieuHienTai.SelectMany(receipt => receipt.ChiTiet.Select(detail => new object[]
                {
                    receipt.MaPhieu, receipt.MaPhieuNguon, receipt.NgayThuMua, receipt.TenKhachHang,
                    receipt.SoDienThoai, receipt.TenNhanVien, detail.TenChatLieu, detail.MaSanPham,
                    detail.TenSanPhamThu, detail.TrongLuong, detail.DonViTinh, detail.DonGiaThuMua,
                    detail.ThanhTien, receipt.TrangThaiHienThi, receipt.GhiChuNguoiDung
                })).ToList();
                try
                {
                    XlsxExportService.Xuat(dialog.FileName, "Dữ liệu thu mua", columns, rows);
                    if (MessageBox.Show("Đã xuất " + rows.Count + " dòng đến:\n" + dialog.FileName +
                                        "\n\nBạn có muốn mở file Excel ngay không?",
                            "Xuất Excel thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(dialog.FileName);
                    }
                }
                catch (Exception ex) { HienThiLoi("Không thể xuất Excel. " + ex.Message); }
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (!phieuDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn phiếu cần xem Report.");
                return;
            }
            try
            {
                using (var form = new FrmXemBaoCao(BaoCaoService.TaoPhieuThuMua(phieuDangChonId.Value)))
                    form.ShowDialog(this);
            }
            catch (InvalidOperationException ex) { HienThiLoi(ex.Message); }
            catch (Exception) { HienThiLoi("Không thể tạo Report thu mua."); }
        }

        private void DinhDangLuoiLoi()
        {
            if (dgvXemTruoc.Columns.Count == 0) return;
            foreach (DataGridViewRow row in dgvXemTruoc.Rows)
            {
                var item = row.DataBoundItem as DongImportHienThi;
                if (item != null && !item.HopLe) row.DefaultCellStyle.BackColor = Color.MistyRose;
            }
        }

        private void DinhDangLuoiTraCuu()
        {
            if (dgvPhieu.Columns.Count == 0) return;
            AnCot(dgvPhieu, "PhieuThuMuaId", "KhachHangId", "TrangThai", "GhiChuNguoiDung", "ChiTiet");
            DinhDangCot(dgvPhieu, "MaPhieu", "Mã phiếu");
            DinhDangCot(dgvPhieu, "MaPhieuNguon", "Mã nguồn");
            DinhDangCot(dgvPhieu, "NgayThuMua", "Ngày thu mua", "dd/MM/yyyy HH:mm");
            DinhDangCot(dgvPhieu, "TenKhachHang", "Khách hàng");
            DinhDangCot(dgvPhieu, "SoDienThoai", "Điện thoại");
            DinhDangCot(dgvPhieu, "TenNhanVien", "Nhân viên");
            DinhDangCot(dgvPhieu, "TongTienThuMua", "Tổng tiền", "N0");
            DinhDangCot(dgvPhieu, "TrangThaiHienThi", "Trạng thái");
        }

        private void DinhDangLuoiChiTiet()
        {
            if (dgvChiTiet.Columns.Count == 0) return;
            DinhDangCot(dgvChiTiet, "TenChatLieu", "Chất liệu");
            DinhDangCot(dgvChiTiet, "MaSanPham", "Mã sản phẩm");
            DinhDangCot(dgvChiTiet, "TenSanPhamThu", "Tên sản phẩm thu");
            DinhDangCot(dgvChiTiet, "TrongLuong", "Trọng lượng", "N3");
            DinhDangCot(dgvChiTiet, "DonViTinh", "Đơn vị");
            DinhDangCot(dgvChiTiet, "DonGiaThuMua", "Đơn giá", "N0");
            DinhDangCot(dgvChiTiet, "ThanhTien", "Thành tiền", "N0");
        }

        private static void AnCot(DataGridView grid, params string[] names)
        {
            foreach (string name in names) if (grid.Columns.Contains(name)) grid.Columns[name].Visible = false;
        }

        private static void DinhDangCot(DataGridView grid, string name, string header, string format = null)
        {
            if (!grid.Columns.Contains(name)) return;
            grid.Columns[name].HeaderText = header;
            if (format != null) grid.Columns[name].DefaultCellStyle.Format = format;
        }

        private void HienThiLoi(string message)
        {
            lblThongBao.Text = string.IsNullOrWhiteSpace(message) ? string.Empty : "* " + message;
            lblThongBao.ForeColor = Color.Firebrick;
        }

        private static string ChuanHoaMaNguon(string value, DongImportThuMua row)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            string result = value.Trim().ToUpperInvariant();
            if (result.Length > 50 || !Regex.IsMatch(result, @"^[A-Z0-9_.-]+$"))
            {
                row.ThemLoi("MaPhieuNguon chỉ gồm A-Z, 0-9, _, . hoặc - và tối đa 50 ký tự");
                return result;
            }
            return result;
        }

        private static string DocChuoi(object value)
        {
            string result = Convert.ToString(value, CultureInfo.CurrentCulture)?.Trim();
            return string.IsNullOrWhiteSpace(result) ? null : result;
        }

        private static DateTime? DocNgay(object value, string column, DongImportThuMua row)
        {
            if (value is DateTime) return (DateTime)value;
            string text = DocChuoi(value);
            DateTime result;
            if (text != null && (DateTime.TryParse(text, VanHoaVietNam, DateTimeStyles.AllowWhiteSpaces, out result) ||
                                 DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result)))
                return result;
            return null;
        }

        private static decimal DocSoDuong(object value, string column, DongImportThuMua row)
        {
            if (value == null) return 0;
            if (!(value is string))
            {
                try { return Convert.ToDecimal(value, CultureInfo.InvariantCulture); }
                catch { return 0; }
            }
            decimal result;
            string text = DocChuoi(value);
            if (decimal.TryParse(text, NumberStyles.Number, VanHoaVietNam, out result)) return result;
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out result) ? result : 0;
        }

        private static int? DocMaId(string value, string prefix, string column, bool optional, DongImportThuMua row)
        {
            if (string.IsNullOrWhiteSpace(value)) return optional ? null : (int?)null;
            string text = value.Trim();
            if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) text = text.Substring(prefix.Length);
            int result;
            if (int.TryParse(text, out result) && result > 0) return result;

            row.ThemLoi(column + " không hợp lệ");
            return null;
        }

        private static int? DocMaHienThi(string value, string prefix)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Trim().StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) return null;
            int result;
            return int.TryParse(value.Trim().Substring(prefix.Length), out result) && result > 0 ? (int?)result : null;
        }

        private static CotXuatExcel C(string title, double width, KieuDuLieuExcel type = KieuDuLieuExcel.VanBan)
            => new CotXuatExcel(title, width, type);

        private sealed class DongImportThuMua
        {
            private readonly List<string> errors = new List<string>();
            public int SoDongExcel { get; set; }
            public string MaPhieuNguon { get; set; }
            public DateTime? NgayThuMua { get; set; }
            public int? NhanVienId { get; set; }
            public string SoDienThoaiKhachHang { get; set; }
            public int? KhachHangId { get; set; }
            public string TenChatLieu { get; set; }
            public int? ChatLieuId { get; set; }
            public int? SanPhamId { get; set; }
            public string TenSanPhamThu { get; set; }
            public decimal TrongLuong { get; set; }
            public string DonViTinh { get; set; }
            public decimal DonGiaThuMua { get; set; }
            public string TrangThai { get; set; }
            public string GhiChu { get; set; }
            public bool HopLe => errors.Count == 0;
            public void ThemLoi(string error) { if (!errors.Contains(error)) errors.Add(error); }
            public IReadOnlyList<string> LayCacLoi() => errors;
            public DongImportHienThi TaoHienThi() => new DongImportHienThi(this, string.Join("; ", errors));
        }

        private sealed class DongImportHienThi
        {
            public DongImportHienThi(DongImportThuMua item, string error)
            {
                SoDong = item.SoDongExcel; MaPhieuNguon = item.MaPhieuNguon; NgayThuMua = item.NgayThuMua;
                MaNhanVien = item.NhanVienId.HasValue ? $"NV{item.NhanVienId:000000}" : string.Empty;
                SoDienThoaiKhachHang = item.SoDienThoaiKhachHang; TenChatLieu = item.TenChatLieu;
                MaSanPham = item.SanPhamId.HasValue ? $"SP{item.SanPhamId:000000}" : string.Empty;
                TenSanPhamThu = item.TenSanPhamThu; TrongLuong = item.TrongLuong; DonViTinh = item.DonViTinh;
                DonGiaThuMua = item.DonGiaThuMua; TrangThai = item.TrangThai; Loi = error; HopLe = item.HopLe;
            }
            public int SoDong { get; }
            public string MaPhieuNguon { get; }
            public DateTime? NgayThuMua { get; }
            public string MaNhanVien { get; }
            public string SoDienThoaiKhachHang { get; }
            public string TenChatLieu { get; }
            public string MaSanPham { get; }
            public string TenSanPhamThu { get; }
            public decimal TrongLuong { get; }
            public string DonViTinh { get; }
            public decimal DonGiaThuMua { get; }
            public string TrangThai { get; }
            public string Loi { get; }
            public bool HopLe { get; }
        }

        private sealed class PhieuThuMuaHienThi
        {
            public PhieuThuMuaHienThi(PhieuThuMua item)
            {
                PhieuThuMuaId = item.PhieuThuMuaId; MaPhieu = $"PTM{item.PhieuThuMuaId:000000}";
                MaPhieuNguon = item.MaPhieuNguon ?? string.Empty; NgayThuMua = item.NgayThuMua;
                KhachHangId = item.KhachHangId; TenKhachHang = item.KhachHang.HoTen; SoDienThoai = item.KhachHang.SoDienThoai;
                TenNhanVien = item.NhanVien.HoTen; TongTienThuMua = item.TongTienThuMua; TrangThai = item.TrangThai;
                TrangThaiHienThi = item.TrangThai == "HOAN_THANH" ? "Hoàn thành" : "Đã hủy";
                GhiChuNguoiDung = item.GhiChu ?? string.Empty;
                ChiTiet = item.ChiTietPhieuThuMuas.OrderBy(detail => detail.ChiTietPhieuThuMuaId)
                    .Select(detail => new ChiTietThuMuaHienThi(detail)).ToList();
            }
            public int PhieuThuMuaId { get; }
            public string MaPhieu { get; }
            public string MaPhieuNguon { get; }
            public DateTime NgayThuMua { get; }
            public int KhachHangId { get; }
            public string TenKhachHang { get; }
            public string SoDienThoai { get; }
            public string TenNhanVien { get; }
            public decimal TongTienThuMua { get; }
            public string TrangThai { get; }
            public string TrangThaiHienThi { get; }
            public string GhiChuNguoiDung { get; }
            public List<ChiTietThuMuaHienThi> ChiTiet { get; }
        }

        private sealed class ChiTietThuMuaHienThi
        {
            public ChiTietThuMuaHienThi(ChiTietPhieuThuMua item)
            {
                TenChatLieu = item.ChatLieu.TenChatLieu;
                MaSanPham = item.SanPhamId.HasValue ? $"SP{item.SanPhamId:000000}" : string.Empty;
                TenSanPhamThu = item.TenSanPhamThu; TrongLuong = item.TrongLuong; DonViTinh = item.DonViTinh;
                DonGiaThuMua = item.DonGiaThuMua; ThanhTien = item.ThanhTien ?? item.TrongLuong * item.DonGiaThuMua;
            }
            public string TenChatLieu { get; }
            public string MaSanPham { get; }
            public string TenSanPhamThu { get; }
            public decimal TrongLuong { get; }
            public string DonViTinh { get; }
            public decimal DonGiaThuMua { get; }
            public decimal ThanhTien { get; }
        }
    }
}

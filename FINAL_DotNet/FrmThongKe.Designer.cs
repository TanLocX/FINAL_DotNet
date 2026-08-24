using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FINAL_DotNet
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlBoLoc;
        private DateTimePicker dtpTuNgay;
        private DateTimePicker dtpDenNgay;
        private ComboBox cboKhoangThoiGian;
        private NumericUpDown nudNguongTon;
        private Button btnTaiLai;
        private Label lblTrangThaiTai;
        private TabControl tabChinh;
        private TabPage tabTongQuan;
        private TabPage tabPhanTich;
        private TabPage tabXuatDuLieu;
        private Label lblDoanhThu;
        private Label lblSoHoaDon;
        private Label lblTrungBinhHoaDon;
        private Label lblTienNhap;
        private Label lblTonThap;
        private Label lblBaoHanhDangXuLy;
        private Label lblBaoHanhSapHetHan;
        private Label lblTyLeEmail;
        private Chart chartDoanhThuNgay;
        private Chart chartSanPhamBanChay;
        private Chart chartDanhMuc;
        private Chart chartChatLieu;
        private Chart chartNhanVien;
        private Chart chartNhaCungCap;
        private Chart chartNhapTheoThang;
        private Chart chartSanPhamNhap;
        private Chart chartBaoHanh;
        private Chart chartEmail;
        private DataGridView dgvTonThap;
        private DataGridView dgvSapHetHan;
        private Button btnXuatSanPham;
        private Button btnXuatHoaDon;
        private Button btnXuatNhapHang;
        private Button btnXuatBaoHanh;
        private Button btnXuatNhatKyEmail;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlBoLoc = new Panel();
            this.dtpTuNgay = new DateTimePicker();
            this.dtpDenNgay = new DateTimePicker();
            this.cboKhoangThoiGian = new ComboBox();
            this.nudNguongTon = new NumericUpDown();
            this.btnTaiLai = new Button();
            this.lblTrangThaiTai = new Label();
            this.tabChinh = new TabControl();
            this.tabTongQuan = new TabPage();
            this.tabPhanTich = new TabPage();
            this.tabXuatDuLieu = new TabPage();

            this.BackColor = MauNen();
            this.ClientSize = new Size(1050, 720);
            this.Font = new Font("Segoe UI", 9F);
            this.Name = "FrmThongKe";
            this.Text = "Tổng quan và thống kê";
            this.Load += new System.EventHandler(this.FrmThongKe_Load);

            TaoBoLoc();
            TaoTabTongQuan();
            TaoTabPhanTich();
            TaoTabXuatDuLieu();

            this.tabChinh.Dock = DockStyle.Fill;
            this.tabChinh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.tabChinh.Padding = new Point(18, 7);
            this.tabChinh.Controls.Add(this.tabTongQuan);
            this.tabChinh.Controls.Add(this.tabPhanTich);
            this.tabChinh.Controls.Add(this.tabXuatDuLieu);

            this.Controls.Add(this.tabChinh);
            this.Controls.Add(this.pnlBoLoc);
        }

        private void TaoBoLoc()
        {
            this.pnlBoLoc.Dock = DockStyle.Top;
            this.pnlBoLoc.Height = 76;
            this.pnlBoLoc.BackColor = Color.White;
            this.pnlBoLoc.Padding = new Padding(14, 9, 14, 8);

            Label lblKhoang = TaoNhanBoLoc("Khoảng thời gian", 14);
            CauHinhCombo(this.cboKhoangThoiGian, 14, 31, 145);
            this.cboKhoangThoiGian.Items.AddRange(new object[] { "Hôm nay", "7 ngày gần đây", "Tháng này", "Năm này", "Tùy chọn" });
            this.cboKhoangThoiGian.SelectedIndexChanged += new System.EventHandler(this.cboKhoangThoiGian_SelectedIndexChanged);

            Label lblTu = TaoNhanBoLoc("Từ ngày", 174);
            CauHinhNgay(this.dtpTuNgay, 174, 31);
            Label lblDen = TaoNhanBoLoc("Đến ngày", 310);
            CauHinhNgay(this.dtpDenNgay, 310, 31);

            Label lblNguong = TaoNhanBoLoc("Tồn thấp ≤", 446);
            this.nudNguongTon.Location = new Point(446, 31);
            this.nudNguongTon.Size = new Size(86, 25);
            this.nudNguongTon.Minimum = 0;
            this.nudNguongTon.Maximum = 9999;
            this.nudNguongTon.Value = 5;

            this.btnTaiLai.BackColor = MauVang();
            this.btnTaiLai.FlatStyle = FlatStyle.Flat;
            this.btnTaiLai.FlatAppearance.BorderSize = 0;
            this.btnTaiLai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnTaiLai.ForeColor = Color.FromArgb(24, 32, 43);
            this.btnTaiLai.Location = new Point(548, 27);
            this.btnTaiLai.Size = new Size(112, 32);
            this.btnTaiLai.Text = "Làm mới";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);

            this.lblTrangThaiTai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblTrangThaiTai.AutoEllipsis = true;
            this.lblTrangThaiTai.ForeColor = MauChuPhu();
            this.lblTrangThaiTai.Location = new Point(675, 29);
            this.lblTrangThaiTai.Size = new Size(355, 28);
            this.lblTrangThaiTai.Text = "Chưa tải dữ liệu";
            this.lblTrangThaiTai.TextAlign = ContentAlignment.MiddleRight;

            this.pnlBoLoc.Controls.AddRange(new Control[] { lblKhoang, this.cboKhoangThoiGian, lblTu, this.dtpTuNgay,
                lblDen, this.dtpDenNgay, lblNguong, this.nudNguongTon, this.btnTaiLai, this.lblTrangThaiTai });
        }

        private void TaoTabTongQuan()
        {
            this.tabTongQuan.Text = "Tổng quan";
            this.tabTongQuan.BackColor = MauNen();
            this.tabTongQuan.Padding = new Padding(10);

            var boCuc = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = MauNen()
            };
            boCuc.RowStyles.Add(new RowStyle(SizeType.Absolute, 164F));
            boCuc.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var kpi = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2, BackColor = MauNen() };
            for (int i = 0; i < 4; i++) kpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            kpi.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            kpi.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.lblDoanhThu = ThemTheKpi(kpi, "DOANH THU", 0, 0, Color.FromArgb(24, 102, 78));
            this.lblSoHoaDon = ThemTheKpi(kpi, "HÓA ĐƠN", 1, 0, Color.FromArgb(42, 88, 141));
            this.lblTrungBinhHoaDon = ThemTheKpi(kpi, "TB / HÓA ĐƠN", 2, 0, Color.FromArgb(118, 82, 36));
            this.lblTienNhap = ThemTheKpi(kpi, "TIỀN NHẬP", 3, 0, Color.FromArgb(126, 67, 80));
            this.lblTonThap = ThemTheKpi(kpi, "SẢN PHẨM TỒN THẤP", 0, 1, Color.FromArgb(183, 91, 38));
            this.lblBaoHanhDangXuLy = ThemTheKpi(kpi, "BẢO HÀNH CHƯA TRẢ", 1, 1, Color.FromArgb(92, 78, 156));
            this.lblBaoHanhSapHetHan = ThemTheKpi(kpi, "SẮP HẾT BẢO HÀNH", 2, 1, Color.FromArgb(175, 51, 71));
            this.lblTyLeEmail = ThemTheKpi(kpi, "EMAIL THÀNH CÔNG", 3, 1, Color.FromArgb(38, 116, 132));

            var charts = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, BackColor = MauNen(), Padding = new Padding(0, 8, 0, 0) };
            charts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            charts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            this.chartDoanhThuNgay = TaoBieuDo();
            this.chartSanPhamBanChay = TaoBieuDo();
            charts.Controls.Add(TaoKhung("Doanh thu theo ngày", this.chartDoanhThuNgay), 0, 0);
            charts.Controls.Add(TaoKhung("Top sản phẩm bán chạy", this.chartSanPhamBanChay), 1, 0);

            boCuc.Controls.Add(kpi, 0, 0);
            boCuc.Controls.Add(charts, 0, 1);
            this.tabTongQuan.Controls.Add(boCuc);
        }

        private void TaoTabPhanTich()
        {
            this.tabPhanTich.Text = "Phân tích";
            this.tabPhanTich.BackColor = MauNen();
            this.tabPhanTich.Padding = new Padding(8);

            var tabNhom = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            var tabDoanhThu = new TabPage("Bán hàng và nhập hàng") { BackColor = MauNen(), Padding = new Padding(7) };
            var tabVanHanh = new TabPage("Tồn kho, bảo hành và email") { BackColor = MauNen(), Padding = new Padding(7) };

            var luoiBieuDo = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, BackColor = MauNen() };
            luoiBieuDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            luoiBieuDo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            luoiBieuDo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            luoiBieuDo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.chartDanhMuc = TaoBieuDo(); this.chartChatLieu = TaoBieuDo(); this.chartNhanVien = TaoBieuDo();
            this.chartNhaCungCap = TaoBieuDo(); this.chartNhapTheoThang = TaoBieuDo(); this.chartSanPhamNhap = TaoBieuDo();
            luoiBieuDo.Controls.Add(TaoKhung("Doanh thu theo danh mục", this.chartDanhMuc), 0, 0);
            luoiBieuDo.Controls.Add(TaoKhung("Doanh thu theo chất liệu", this.chartChatLieu), 1, 0);
            luoiBieuDo.Controls.Add(TaoKhung("Doanh thu theo nhân viên", this.chartNhanVien), 0, 1);
            var tabNhapHang = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), MinimumSize = new Size(40, 40) };
            var tabNhapThang = new TabPage("Theo tháng") { BackColor = Color.White, Padding = new Padding(2) };
            var tabNhapNcc = new TabPage("Nhà cung cấp") { BackColor = Color.White, Padding = new Padding(2) };
            var tabNhapSanPham = new TabPage("Sản phẩm") { BackColor = Color.White, Padding = new Padding(2) };
            this.chartNhapTheoThang.Dock = DockStyle.Fill; this.chartNhaCungCap.Dock = DockStyle.Fill; this.chartSanPhamNhap.Dock = DockStyle.Fill;
            tabNhapThang.Controls.Add(this.chartNhapTheoThang);
            tabNhapNcc.Controls.Add(this.chartNhaCungCap);
            tabNhapSanPham.Controls.Add(this.chartSanPhamNhap);
            tabNhapHang.TabPages.Add(tabNhapThang); tabNhapHang.TabPages.Add(tabNhapNcc); tabNhapHang.TabPages.Add(tabNhapSanPham);
            luoiBieuDo.Controls.Add(TaoKhung("Phân tích nhập hàng", tabNhapHang), 1, 1);
            tabDoanhThu.Controls.Add(luoiBieuDo);

            var luoiVanHanh = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, BackColor = MauNen() };
            luoiVanHanh.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            luoiVanHanh.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            luoiVanHanh.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            luoiVanHanh.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.dgvTonThap = TaoBang();
            this.dgvSapHetHan = TaoBang();
            this.chartBaoHanh = TaoBieuDo();
            this.chartEmail = TaoBieuDo();
            luoiVanHanh.Controls.Add(TaoKhung("Sản phẩm tồn thấp", this.dgvTonThap), 0, 0);
            luoiVanHanh.Controls.Add(TaoKhung("Hạn bảo hành trong 30 ngày", this.dgvSapHetHan), 1, 0);
            luoiVanHanh.Controls.Add(TaoKhung("Phiếu bảo hành theo trạng thái", this.chartBaoHanh), 0, 1);
            luoiVanHanh.Controls.Add(TaoKhung("Kết quả email theo mẫu", this.chartEmail), 1, 1);
            tabVanHanh.Controls.Add(luoiVanHanh);

            tabNhom.TabPages.Add(tabDoanhThu);
            tabNhom.TabPages.Add(tabVanHanh);
            this.tabPhanTich.Controls.Add(tabNhom);
        }

        private void TaoTabXuatDuLieu()
        {
            this.tabXuatDuLieu.Text = "Xuất dữ liệu";
            this.tabXuatDuLieu.BackColor = MauNen();
            this.tabXuatDuLieu.Padding = new Padding(24);

            var tieuDe = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = MauChu(),
                Location = new Point(24, 24),
                Text = "Xuất dữ liệu Excel (.xlsx)"
            };
            var moTa = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MauChuPhu(),
                Location = new Point(27, 62),
                Size = new Size(850, 48),
                Text = "Hóa đơn, nhập hàng, bảo hành và nhật ký email được lọc theo khoảng ngày phía trên. " +
                       "File có bộ lọc cột, cố định hàng tiêu đề và định dạng ngày / số tiền."
            };

            var luoi = new TableLayoutPanel { Location = new Point(24, 126), Size = new Size(760, 224), ColumnCount = 2, RowCount = 3 };
            luoi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            luoi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 3; i++) luoi.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            this.btnXuatSanPham = TaoNutXuat("Xuất danh sách sản phẩm", this.btnXuatSanPham_Click);
            this.btnXuatHoaDon = TaoNutXuat("Xuất hóa đơn đã thanh toán", this.btnXuatHoaDon_Click);
            this.btnXuatNhapHang = TaoNutXuat("Xuất phiếu nhập hoàn thành", this.btnXuatNhapHang_Click);
            this.btnXuatBaoHanh = TaoNutXuat("Xuất phiếu bảo hành", this.btnXuatBaoHanh_Click);
            this.btnXuatNhatKyEmail = TaoNutXuat("Xuất nhật ký gửi email", this.btnXuatNhatKyEmail_Click);
            luoi.Controls.Add(this.btnXuatSanPham, 0, 0);
            luoi.Controls.Add(this.btnXuatHoaDon, 1, 0);
            luoi.Controls.Add(this.btnXuatNhapHang, 0, 1);
            luoi.Controls.Add(this.btnXuatBaoHanh, 1, 1);
            luoi.Controls.Add(this.btnXuatNhatKyEmail, 0, 2);

            this.tabXuatDuLieu.Controls.Add(tieuDe);
            this.tabXuatDuLieu.Controls.Add(moTa);
            this.tabXuatDuLieu.Controls.Add(luoi);
        }

        private Label ThemTheKpi(TableLayoutPanel luoi, string tieuDe, int cot, int dong, Color mau)
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(5) };
            var vach = new Panel { BackColor = mau, Dock = DockStyle.Left, Width = 5 };
            var nhanTieuDe = new Label { AutoSize = false, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = MauChuPhu(), Location = new Point(16, 9), Size = new Size(205, 19), Text = tieuDe };
            var giaTri = new Label { AutoEllipsis = true, Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = mau, Location = new Point(16, 29), Size = new Size(215, 32), Text = "--", TextAlign = ContentAlignment.MiddleLeft };
            panel.Controls.Add(giaTri); panel.Controls.Add(nhanTieuDe); panel.Controls.Add(vach);
            luoi.Controls.Add(panel, cot, dong);
            return giaTri;
        }

        private static Panel TaoKhung(string tieuDe, Control noiDung)
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(5), Padding = new Padding(8, 34, 8, 8), MinimumSize = new Size(40, 40) };
            var label = new Label { AutoSize = false, Dock = DockStyle.Top, Height = 32, Location = new Point(8, 0), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = MauChu(), Text = tieuDe, TextAlign = ContentAlignment.MiddleLeft };
            noiDung.Dock = DockStyle.Fill;
            panel.Controls.Add(noiDung);
            panel.Controls.Add(label);
            label.BringToFront();
            return panel;
        }

        private static Chart TaoBieuDo()
        {
            var chart = new Chart { BackColor = Color.White, Palette = ChartColorPalette.None, MinimumSize = new Size(20, 20), Size = new Size(320, 180) };
            chart.PaletteCustomColors = new[] { Color.FromArgb(204, 164, 87), Color.FromArgb(34, 101, 128), Color.FromArgb(41, 128, 97), Color.FromArgb(170, 76, 88), Color.FromArgb(101, 82, 143) };
            return chart;
        }

        private static DataGridView TaoBang()
        {
            var bang = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 30,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            bang.EnableHeadersVisualStyles = false;
            bang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(27, 39, 53);
            bang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            bang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            bang.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F);
            bang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 218, 190);
            bang.DefaultCellStyle.SelectionForeColor = MauChu();
            return bang;
        }

        private static Button TaoNutXuat(string noiDung, System.EventHandler xuLy)
        {
            var nut = new Button
            {
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = MauChu(),
                Margin = new Padding(5),
                Text = noiDung,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                UseVisualStyleBackColor = false
            };
            nut.FlatAppearance.BorderColor = Color.FromArgb(217, 197, 153);
            nut.Click += xuLy;
            return nut;
        }

        private static Label TaoNhanBoLoc(string text, int x) => new Label { AutoSize = true, Font = new Font("Segoe UI", 8F, FontStyle.Bold), ForeColor = MauChuPhu(), Location = new Point(x, 9), Text = text };
        private static void CauHinhNgay(DateTimePicker picker, int x, int y) { picker.Format = DateTimePickerFormat.Custom; picker.CustomFormat = "dd/MM/yyyy"; picker.Location = new Point(x, y); picker.Size = new Size(124, 25); }
        private static void CauHinhCombo(ComboBox combo, int x, int y, int width) { combo.DropDownStyle = ComboBoxStyle.DropDownList; combo.Location = new Point(x, y); combo.Size = new Size(width, 25); }
        private static Color MauNen() => Color.FromArgb(242, 245, 248);
        private static Color MauChu() => Color.FromArgb(27, 39, 53);
        private static Color MauChuPhu() => Color.FromArgb(95, 106, 119);
        private static Color MauVang() => Color.FromArgb(222, 187, 116);
    }
}

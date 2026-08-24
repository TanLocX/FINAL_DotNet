namespace FINAL_DotNet
{
    partial class FrmNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.cboLocNhaCungCap = new System.Windows.Forms.ComboBox();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.splitChinh = new System.Windows.Forms.SplitContainer();
            this.dgvPhieuNhap = new System.Windows.Forms.DataGridView();
            this.tabNhapHang = new System.Windows.Forms.TabControl();
            this.tabLapPhieu = new System.Windows.Forms.TabPage();
            this.dgvGioNhap = new System.Windows.Forms.DataGridView();
            this.pnlDongNhap = new System.Windows.Forms.Panel();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.numDonGiaNhap = new System.Windows.Forms.NumericUpDown();
            this.lblTonKhoHienTai = new System.Windows.Forms.Label();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnMoiDong = new System.Windows.Forms.Button();
            this.lblSoDong = new System.Windows.Forms.Label();
            this.lblTongTienLapPhieu = new System.Windows.Forms.Label();
            this.pnlDauPhieu = new System.Windows.Forms.Panel();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblNhanVienLapPhieu = new System.Windows.Forms.Label();
            this.lblNgayLapPhieu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.tabChiTiet = new System.Windows.Forms.TabPage();
            this.dgvChiTietPhieu = new System.Windows.Forms.DataGridView();
            this.pnlThongTinPhieu = new System.Windows.Forms.Panel();
            this.lblMaPhieuChiTiet = new System.Windows.Forms.Label();
            this.lblNgayNhapChiTiet = new System.Windows.Forms.Label();
            this.lblNhaCungCapChiTiet = new System.Windows.Forms.Label();
            this.lblNhanVienChiTiet = new System.Windows.Forms.Label();
            this.lblTrangThaiChiTiet = new System.Windows.Forms.Label();
            this.lblTongTienChiTiet = new System.Windows.Forms.Label();
            this.txtGhiChuChiTiet = new System.Windows.Forms.TextBox();
            this.pnlChan = new System.Windows.Forms.Panel();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.btnPhieuMoi = new System.Windows.Forms.Button();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).BeginInit();
            this.splitChinh.Panel1.SuspendLayout();
            this.splitChinh.Panel2.SuspendLayout();
            this.splitChinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).BeginInit();
            this.tabNhapHang.SuspendLayout();
            this.tabLapPhieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioNhap)).BeginInit();
            this.pnlDongNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGiaNhap)).BeginInit();
            this.pnlDauPhieu.SuspendLayout();
            this.tabChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPhieu)).BeginInit();
            this.pnlThongTinPhieu.SuspendLayout();
            this.pnlChan.SuspendLayout();
            this.SuspendLayout();

            this.pnlBoLoc.BackColor = System.Drawing.Color.White;
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBoLoc.Height = 96;
            this.pnlBoLoc.Size = new System.Drawing.Size(1000, 96);
            this.pnlBoLoc.Controls.Add(TaoNhan("Mã phiếu / nhân viên / sản phẩm", 16, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Từ ngày", 254, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Đến ngày", 404, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Nhà cung cấp", 554, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Trạng thái", 16, 55));
            this.txtTuKhoa.Location = new System.Drawing.Point(16, 27);
            this.txtTuKhoa.Size = new System.Drawing.Size(222, 23);
            this.txtTuKhoa.MaxLength = 150;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            CauHinhNgay(this.dtpTuNgay, 254, 27, 134);
            CauHinhNgay(this.dtpDenNgay, 404, 27, 134);
            CauHinhCombo(this.cboLocNhaCungCap, 554, 27, 220);
            CauHinhCombo(this.cboLocTrangThai, 16, 73, 160);
            this.cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Hoàn thành", "Đã hủy" });
            CauHinhNut(this.btnTimKiem, "Tìm kiếm", 192, 66, 96, MauXanh());
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            CauHinhNut(this.btnTaiLai, "Tải lại", 298, 66, 84, System.Drawing.Color.DimGray);
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            this.lblSoKetQua.Location = new System.Drawing.Point(554, 64);
            this.lblSoKetQua.Size = new System.Drawing.Size(220, 29);
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pnlBoLoc.Controls.Add(this.txtTuKhoa);
            this.pnlBoLoc.Controls.Add(this.dtpTuNgay);
            this.pnlBoLoc.Controls.Add(this.dtpDenNgay);
            this.pnlBoLoc.Controls.Add(this.cboLocNhaCungCap);
            this.pnlBoLoc.Controls.Add(this.cboLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.btnTaiLai);
            this.pnlBoLoc.Controls.Add(this.lblSoKetQua);

            this.splitChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChinh.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitChinh.SplitterDistance = 205;
            this.splitChinh.SplitterWidth = 6;
            this.splitChinh.Panel1.Controls.Add(this.dgvPhieuNhap);
            this.splitChinh.Panel2.Controls.Add(this.tabNhapHang);

            headerStyle.BackColor = System.Drawing.Color.FromArgb(27, 39, 53);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.SelectionBackColor = headerStyle.BackColor;
            CauHinhLuoi(this.dgvPhieuNhap, headerStyle);
            this.dgvPhieuNhap.Columns.Add(TaoCot("Mã phiếu", "MaPhieuNhap", 90));
            this.dgvPhieuNhap.Columns.Add(TaoCot("Ngày nhập", "NgayNhapHienThi", 130));
            this.dgvPhieuNhap.Columns.Add(TaoCot("Nhà cung cấp", "TenNhaCungCap", 205));
            this.dgvPhieuNhap.Columns.Add(TaoCot("Nhân viên", "TenNhanVien", 150));
            var cotTongPhieu = TaoCot("Tổng tiền", "TongTienNhap", 120);
            cotTongPhieu.DefaultCellStyle.Format = "N0";
            cotTongPhieu.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvPhieuNhap.Columns.Add(cotTongPhieu);
            this.dgvPhieuNhap.Columns.Add(TaoCot("Trạng thái", "TrangThaiHienThi", 100));
            this.dgvPhieuNhap.Columns.Add(TaoCot("Ghi chú", "GhiChu", 150));
            this.dgvPhieuNhap.SelectionChanged += new System.EventHandler(this.dgvPhieuNhap_SelectionChanged);

            this.tabNhapHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNhapHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabNhapHang.Controls.Add(this.tabLapPhieu);
            this.tabNhapHang.Controls.Add(this.tabChiTiet);
            this.tabLapPhieu.Text = "Lập phiếu nhập";
            this.tabLapPhieu.BackColor = System.Drawing.Color.White;
            this.tabLapPhieu.Padding = new System.Windows.Forms.Padding(6);
            this.tabLapPhieu.Controls.Add(this.dgvGioNhap);
            this.tabLapPhieu.Controls.Add(this.pnlDongNhap);
            this.tabLapPhieu.Controls.Add(this.pnlDauPhieu);
            this.tabChiTiet.Text = "Chi tiết phiếu đã lập";
            this.tabChiTiet.BackColor = System.Drawing.Color.White;
            this.tabChiTiet.Padding = new System.Windows.Forms.Padding(6);
            this.tabChiTiet.Controls.Add(this.dgvChiTietPhieu);
            this.tabChiTiet.Controls.Add(this.pnlThongTinPhieu);

            this.pnlDauPhieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDauPhieu.Height = 58;
            this.pnlDauPhieu.Size = new System.Drawing.Size(960, 58);
            this.pnlDauPhieu.Controls.Add(TaoNhan("Nhà cung cấp *", 4, 2));
            this.pnlDauPhieu.Controls.Add(TaoNhan("Nhân viên lập", 244, 2));
            this.pnlDauPhieu.Controls.Add(TaoNhan("Thời gian", 434, 2));
            this.pnlDauPhieu.Controls.Add(TaoNhan("Ghi chú", 584, 2));
            CauHinhCombo(this.cboNhaCungCap, 4, 23, 224);
            this.lblNhanVienLapPhieu.Location = new System.Drawing.Point(244, 24);
            this.lblNhanVienLapPhieu.Size = new System.Drawing.Size(174, 23);
            this.lblNhanVienLapPhieu.AutoEllipsis = true;
            this.lblNgayLapPhieu.Location = new System.Drawing.Point(434, 24);
            this.lblNgayLapPhieu.Size = new System.Drawing.Size(134, 23);
            this.txtGhiChu.Location = new System.Drawing.Point(584, 23);
            this.txtGhiChu.Size = new System.Drawing.Size(345, 23);
            this.txtGhiChu.MaxLength = 500;
            this.pnlDauPhieu.Controls.Add(this.cboNhaCungCap);
            this.pnlDauPhieu.Controls.Add(this.lblNhanVienLapPhieu);
            this.pnlDauPhieu.Controls.Add(this.lblNgayLapPhieu);
            this.pnlDauPhieu.Controls.Add(this.txtGhiChu);

            this.pnlDongNhap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDongNhap.Height = 70;
            this.pnlDongNhap.Size = new System.Drawing.Size(960, 70);
            this.pnlDongNhap.Controls.Add(TaoNhan("Sản phẩm *", 4, 2));
            this.pnlDongNhap.Controls.Add(TaoNhan("Số lượng *", 276, 2));
            this.pnlDongNhap.Controls.Add(TaoNhan("Đơn giá nhập *", 368, 2));
            CauHinhCombo(this.cboSanPham, 4, 23, 260);
            this.cboSanPham.SelectedIndexChanged += new System.EventHandler(this.cboSanPham_SelectedIndexChanged);
            this.lblTonKhoHienTai.Location = new System.Drawing.Point(4, 48);
            this.lblTonKhoHienTai.Size = new System.Drawing.Size(200, 20);
            CauHinhSo(this.numSoLuong, 276, 23, 80);
            this.numSoLuong.Minimum = 1;
            this.numSoLuong.Value = 1;
            CauHinhSo(this.numDonGiaNhap, 368, 23, 125);
            this.numDonGiaNhap.DecimalPlaces = 2;
            this.numDonGiaNhap.ThousandsSeparator = true;
            CauHinhNut(this.btnThemDong, "Thêm sản phẩm", 505, 20, 120, MauXanh());
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            CauHinhNut(this.btnXoaDong, "Xóa", 633, 20, 64, System.Drawing.Color.Firebrick);
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            CauHinhNut(this.btnMoiDong, "Nhập mới", 705, 20, 78, System.Drawing.Color.DimGray);
            this.btnMoiDong.Click += new System.EventHandler(this.btnMoiDong_Click);
            this.lblSoDong.Location = new System.Drawing.Point(795, 4);
            this.lblSoDong.Size = new System.Drawing.Size(155, 20);
            this.lblSoDong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTongTienLapPhieu.Location = new System.Drawing.Point(795, 26);
            this.lblTongTienLapPhieu.Size = new System.Drawing.Size(155, 34);
            this.lblTongTienLapPhieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongTienLapPhieu.ForeColor = MauXanh();
            this.lblTongTienLapPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pnlDongNhap.Controls.Add(this.cboSanPham);
            this.pnlDongNhap.Controls.Add(this.lblTonKhoHienTai);
            this.pnlDongNhap.Controls.Add(this.numSoLuong);
            this.pnlDongNhap.Controls.Add(this.numDonGiaNhap);
            this.pnlDongNhap.Controls.Add(this.btnThemDong);
            this.pnlDongNhap.Controls.Add(this.btnXoaDong);
            this.pnlDongNhap.Controls.Add(this.btnMoiDong);
            this.pnlDongNhap.Controls.Add(this.lblSoDong);
            this.pnlDongNhap.Controls.Add(this.lblTongTienLapPhieu);

            CauHinhLuoi(this.dgvGioNhap, headerStyle);
            this.dgvGioNhap.Columns.Add(TaoCot("Mã SP", "MaSanPham", 90));
            this.dgvGioNhap.Columns.Add(TaoCot("Tên sản phẩm", "TenSanPham", 330));
            this.dgvGioNhap.Columns.Add(TaoCot("Tồn hiện tại", "TonKhoHienTai", 95));
            this.dgvGioNhap.Columns.Add(TaoCot("Số lượng nhập", "SoLuong", 105));
            var cotDonGia = TaoCot("Đơn giá nhập", "DonGiaNhap", 130);
            cotDonGia.DefaultCellStyle.Format = "N0";
            this.dgvGioNhap.Columns.Add(cotDonGia);
            var cotThanhTien = TaoCot("Thành tiền", "ThanhTien", 160);
            cotThanhTien.DefaultCellStyle.Format = "N0";
            this.dgvGioNhap.Columns.Add(cotThanhTien);
            this.dgvGioNhap.SelectionChanged += new System.EventHandler(this.dgvGioNhap_SelectionChanged);

            this.pnlThongTinPhieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTinPhieu.Height = 76;
            this.pnlThongTinPhieu.Size = new System.Drawing.Size(960, 76);
            this.pnlThongTinPhieu.Controls.Add(TaoNhan("Mã phiếu", 4, 2));
            this.pnlThongTinPhieu.Controls.Add(TaoNhan("Ngày nhập", 114, 2));
            this.pnlThongTinPhieu.Controls.Add(TaoNhan("Nhà cung cấp", 264, 2));
            this.pnlThongTinPhieu.Controls.Add(TaoNhan("Nhân viên", 474, 2));
            this.pnlThongTinPhieu.Controls.Add(TaoNhan("Trạng thái", 634, 2));
            this.pnlThongTinPhieu.Controls.Add(TaoNhan("Tổng tiền", 754, 2));
            CauHinhGiaTri(this.lblMaPhieuChiTiet, 4, 23, 100);
            CauHinhGiaTri(this.lblNgayNhapChiTiet, 114, 23, 140);
            CauHinhGiaTri(this.lblNhaCungCapChiTiet, 264, 23, 200);
            CauHinhGiaTri(this.lblNhanVienChiTiet, 474, 23, 150);
            CauHinhGiaTri(this.lblTrangThaiChiTiet, 634, 23, 110);
            CauHinhGiaTri(this.lblTongTienChiTiet, 754, 23, 175);
            this.lblTongTienChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongTienChiTiet.ForeColor = MauXanh();
            this.txtGhiChuChiTiet.Location = new System.Drawing.Point(4, 49);
            this.txtGhiChuChiTiet.Size = new System.Drawing.Size(925, 23);
            this.txtGhiChuChiTiet.ReadOnly = true;
            this.pnlThongTinPhieu.Controls.Add(this.lblMaPhieuChiTiet);
            this.pnlThongTinPhieu.Controls.Add(this.lblNgayNhapChiTiet);
            this.pnlThongTinPhieu.Controls.Add(this.lblNhaCungCapChiTiet);
            this.pnlThongTinPhieu.Controls.Add(this.lblNhanVienChiTiet);
            this.pnlThongTinPhieu.Controls.Add(this.lblTrangThaiChiTiet);
            this.pnlThongTinPhieu.Controls.Add(this.lblTongTienChiTiet);
            this.pnlThongTinPhieu.Controls.Add(this.txtGhiChuChiTiet);

            CauHinhLuoi(this.dgvChiTietPhieu, headerStyle);
            this.dgvChiTietPhieu.Columns.Add(TaoCot("Mã SP", "MaSanPham", 90));
            this.dgvChiTietPhieu.Columns.Add(TaoCot("Tên sản phẩm", "TenSanPham", 400));
            this.dgvChiTietPhieu.Columns.Add(TaoCot("Số lượng", "SoLuong", 100));
            var cotGiaChiTiet = TaoCot("Đơn giá nhập", "DonGiaNhap", 140);
            cotGiaChiTiet.DefaultCellStyle.Format = "N0";
            this.dgvChiTietPhieu.Columns.Add(cotGiaChiTiet);
            var cotTienChiTiet = TaoCot("Thành tiền", "ThanhTien", 180);
            cotTienChiTiet.DefaultCellStyle.Format = "N0";
            this.dgvChiTietPhieu.Columns.Add(cotTienChiTiet);

            this.pnlChan.BackColor = System.Drawing.Color.White;
            this.pnlChan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChan.Height = 58;
            this.pnlChan.Size = new System.Drawing.Size(1000, 58);
            this.lblThongBao.ForeColor = System.Drawing.Color.Crimson;
            this.lblThongBao.Location = new System.Drawing.Point(14, 5);
            this.lblThongBao.Size = new System.Drawing.Size(410, 46);
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThongBao.AutoEllipsis = true;
            CauHinhNut(this.btnLuuPhieu, "Lưu phiếu nhập", 420, 12, 126, MauXanh());
            this.btnLuuPhieu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);
            CauHinhNut(this.btnXemBaoCao, "Xem báo cáo", 554, 12, 122, System.Drawing.Color.FromArgb(44, 95, 138));
            this.btnXemBaoCao.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnXemBaoCao.Enabled = false;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            CauHinhNut(this.btnHuyPhieu, "Hủy phiếu", 684, 12, 110, System.Drawing.Color.Firebrick);
            this.btnHuyPhieu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnHuyPhieu.Click += new System.EventHandler(this.btnHuyPhieu_Click);
            CauHinhNut(this.btnPhieuMoi, "Phiếu mới", 802, 12, 105, System.Drawing.Color.DimGray);
            this.btnPhieuMoi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnPhieuMoi.Click += new System.EventHandler(this.btnPhieuMoi_Click);
            this.pnlChan.Controls.Add(this.lblThongBao);
            this.pnlChan.Controls.Add(this.btnLuuPhieu);
            this.pnlChan.Controls.Add(this.btnXemBaoCao);
            this.pnlChan.Controls.Add(this.btnHuyPhieu);
            this.pnlChan.Controls.Add(this.btnPhieuMoi);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 248);
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitChinh);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlChan);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmNhapHang";
            this.Text = "Nhập hàng";
            this.Load += new System.EventHandler(this.FrmNhapHang_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitChinh.Panel1.ResumeLayout(false);
            this.splitChinh.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).EndInit();
            this.splitChinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).EndInit();
            this.tabNhapHang.ResumeLayout(false);
            this.tabLapPhieu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioNhap)).EndInit();
            this.pnlDongNhap.ResumeLayout(false);
            this.pnlDongNhap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGiaNhap)).EndInit();
            this.pnlDauPhieu.ResumeLayout(false);
            this.pnlDauPhieu.PerformLayout();
            this.tabChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPhieu)).EndInit();
            this.pnlThongTinPhieu.ResumeLayout(false);
            this.pnlThongTinPhieu.PerformLayout();
            this.pnlChan.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private static System.Windows.Forms.Label TaoNhan(string text, int x, int y)
        {
            return new System.Windows.Forms.Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(x, y),
                Text = text
            };
        }

        private static void CauHinhNgay(System.Windows.Forms.DateTimePicker control, int x, int y, int width)
        {
            control.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            control.CustomFormat = "dd/MM/yyyy";
            control.ShowCheckBox = true;
            control.Checked = false;
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, 23);
        }

        private static void CauHinhCombo(System.Windows.Forms.ComboBox control, int x, int y, int width)
        {
            control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, 23);
        }

        private static void CauHinhSo(System.Windows.Forms.NumericUpDown control, int x, int y, int width)
        {
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, 23);
            control.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        }

        private static void CauHinhNut(System.Windows.Forms.Button button, string text, int x, int y, int width, System.Drawing.Color color)
        {
            button.BackColor = color;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.ForeColor = System.Drawing.Color.White;
            button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            button.Location = new System.Drawing.Point(x, y);
            button.Size = new System.Drawing.Size(width, 32);
            button.Text = text;
            button.UseVisualStyleBackColor = false;
        }

        private static void CauHinhGiaTri(System.Windows.Forms.Label label, int x, int y, int width)
        {
            label.Location = new System.Drawing.Point(x, y);
            label.Size = new System.Drawing.Size(width, 22);
            label.AutoEllipsis = true;
        }

        private static void CauHinhLuoi(System.Windows.Forms.DataGridView grid, System.Windows.Forms.DataGridViewCellStyle headerStyle)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoGenerateColumns = false;
            grid.BackgroundColor = System.Drawing.Color.White;
            grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle = headerStyle;
            grid.ColumnHeadersHeight = 34;
            grid.Dock = System.Windows.Forms.DockStyle.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 29;
            grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        }

        private static System.Windows.Forms.DataGridViewTextBoxColumn TaoCot(string tieuDe, string thuocTinh, int width)
        {
            return new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = tieuDe,
                DataPropertyName = thuocTinh,
                Width = width,
                ReadOnly = true
            };
        }

        private static System.Drawing.Color MauXanh() => System.Drawing.Color.FromArgb(35, 125, 96);

        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.ComboBox cboLocNhaCungCap;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.SplitContainer splitChinh;
        private System.Windows.Forms.DataGridView dgvPhieuNhap;
        private System.Windows.Forms.TabControl tabNhapHang;
        private System.Windows.Forms.TabPage tabLapPhieu;
        private System.Windows.Forms.DataGridView dgvGioNhap;
        private System.Windows.Forms.Panel pnlDongNhap;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.NumericUpDown numDonGiaNhap;
        private System.Windows.Forms.Label lblTonKhoHienTai;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnMoiDong;
        private System.Windows.Forms.Label lblSoDong;
        private System.Windows.Forms.Label lblTongTienLapPhieu;
        private System.Windows.Forms.Panel pnlDauPhieu;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblNhanVienLapPhieu;
        private System.Windows.Forms.Label lblNgayLapPhieu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TabPage tabChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTietPhieu;
        private System.Windows.Forms.Panel pnlThongTinPhieu;
        private System.Windows.Forms.Label lblMaPhieuChiTiet;
        private System.Windows.Forms.Label lblNgayNhapChiTiet;
        private System.Windows.Forms.Label lblNhaCungCapChiTiet;
        private System.Windows.Forms.Label lblNhanVienChiTiet;
        private System.Windows.Forms.Label lblTrangThaiChiTiet;
        private System.Windows.Forms.Label lblTongTienChiTiet;
        private System.Windows.Forms.TextBox txtGhiChuChiTiet;
        private System.Windows.Forms.Panel pnlChan;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.Button btnHuyPhieu;
        private System.Windows.Forms.Button btnPhieuMoi;
    }
}

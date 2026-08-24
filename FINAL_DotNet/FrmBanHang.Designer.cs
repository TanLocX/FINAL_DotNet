namespace FINAL_DotNet
{
    partial class FrmBanHang
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
            this.cboLocKhachHang = new System.Windows.Forms.ComboBox();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.txtTienTu = new System.Windows.Forms.TextBox();
            this.txtTienDen = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.splitChinh = new System.Windows.Forms.SplitContainer();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.tabBanHang = new System.Windows.Forms.TabControl();
            this.tabLapHoaDon = new System.Windows.Forms.TabPage();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.pnlDongBan = new System.Windows.Forms.Panel();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblDonGiaBan = new System.Windows.Forms.Label();
            this.dtpHanBaoHanh = new System.Windows.Forms.DateTimePicker();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnMoiDong = new System.Windows.Forms.Button();
            this.lblSoDong = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.pnlDauHoaDon = new System.Windows.Forms.Panel();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblNhanVienLap = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.cboPhuongThucThanhToan = new System.Windows.Forms.ComboBox();
            this.numGiamGia = new System.Windows.Forms.NumericUpDown();
            this.tabLichSu = new System.Windows.Forms.TabPage();
            this.dgvChiTietHoaDon = new System.Windows.Forms.DataGridView();
            this.pnlThongTinHoaDon = new System.Windows.Forms.Panel();
            this.lblMaHoaDonChiTiet = new System.Windows.Forms.Label();
            this.lblNgayLapChiTiet = new System.Windows.Forms.Label();
            this.lblKhachHangChiTiet = new System.Windows.Forms.Label();
            this.lblNhanVienChiTiet = new System.Windows.Forms.Label();
            this.lblThanhToanChiTiet = new System.Windows.Forms.Label();
            this.lblTrangThaiChiTiet = new System.Windows.Forms.Label();
            this.lblTienChiTiet = new System.Windows.Forms.Label();
            this.pnlChan = new System.Windows.Forms.Panel();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.btnLuuHoaDon = new System.Windows.Forms.Button();
            this.btnHuyHoaDon = new System.Windows.Forms.Button();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnHoaDonMoi = new System.Windows.Forms.Button();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).BeginInit();
            this.splitChinh.Panel1.SuspendLayout();
            this.splitChinh.Panel2.SuspendLayout();
            this.splitChinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.tabBanHang.SuspendLayout();
            this.tabLapHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.pnlDongBan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.pnlDauHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGia)).BeginInit();
            this.tabLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).BeginInit();
            this.pnlThongTinHoaDon.SuspendLayout();
            this.pnlChan.SuspendLayout();
            this.SuspendLayout();

            this.pnlBoLoc.BackColor = System.Drawing.Color.White;
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBoLoc.Height = 96;
            this.pnlBoLoc.Size = new System.Drawing.Size(1000, 96);
            this.pnlBoLoc.Controls.Add(TaoNhan("Mã HĐ / khách / nhân viên / sản phẩm", 16, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Từ ngày", 238, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Đến ngày", 378, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Khách hàng", 518, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Trạng thái", 16, 55));
            this.pnlBoLoc.Controls.Add(TaoNhan("Tiền từ", 184, 55));
            this.pnlBoLoc.Controls.Add(TaoNhan("Tiền đến", 306, 55));
            this.txtTuKhoa.Location = new System.Drawing.Point(16, 27);
            this.txtTuKhoa.Size = new System.Drawing.Size(206, 23);
            this.txtTuKhoa.MaxLength = 150;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            CauHinhNgay(this.dtpTuNgay, 238, 27, 124);
            CauHinhNgay(this.dtpDenNgay, 378, 27, 124);
            CauHinhCombo(this.cboLocKhachHang, 518, 27, 220);
            CauHinhCombo(this.cboLocTrangThai, 16, 73, 152);
            this.cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Đã thanh toán", "Đã hủy" });
            this.txtTienTu.Location = new System.Drawing.Point(184, 73);
            this.txtTienTu.Size = new System.Drawing.Size(106, 23);
            this.txtTienDen.Location = new System.Drawing.Point(306, 73);
            this.txtTienDen.Size = new System.Drawing.Size(106, 23);
            CauHinhNut(this.btnTimKiem, "Tìm kiếm", 428, 66, 96, MauXanh());
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            CauHinhNut(this.btnTaiLai, "Tải lại", 532, 66, 82, System.Drawing.Color.DimGray);
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            this.lblSoKetQua.Location = new System.Drawing.Point(626, 64);
            this.lblSoKetQua.Size = new System.Drawing.Size(112, 29);
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pnlBoLoc.Controls.Add(this.txtTuKhoa);
            this.pnlBoLoc.Controls.Add(this.dtpTuNgay);
            this.pnlBoLoc.Controls.Add(this.dtpDenNgay);
            this.pnlBoLoc.Controls.Add(this.cboLocKhachHang);
            this.pnlBoLoc.Controls.Add(this.cboLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.txtTienTu);
            this.pnlBoLoc.Controls.Add(this.txtTienDen);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.btnTaiLai);
            this.pnlBoLoc.Controls.Add(this.lblSoKetQua);

            this.splitChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChinh.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitChinh.SplitterDistance = 205;
            this.splitChinh.SplitterWidth = 6;
            this.splitChinh.Panel1.Controls.Add(this.dgvHoaDon);
            this.splitChinh.Panel2.Controls.Add(this.tabBanHang);

            headerStyle.BackColor = System.Drawing.Color.FromArgb(27, 39, 53);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.SelectionBackColor = headerStyle.BackColor;
            CauHinhLuoi(this.dgvHoaDon, headerStyle);
            this.dgvHoaDon.Columns.Add(TaoCot("Mã HĐ", "MaHoaDon", 82));
            this.dgvHoaDon.Columns.Add(TaoCot("Ngày lập", "NgayLapHienThi", 128));
            this.dgvHoaDon.Columns.Add(TaoCot("Khách hàng", "TenKhachHang", 175));
            this.dgvHoaDon.Columns.Add(TaoCot("Nhân viên", "TenNhanVien", 140));
            var cotTongHoaDon = TaoCot("Phải trả", "ThanhTien", 120);
            cotTongHoaDon.DefaultCellStyle.Format = "N0";
            cotTongHoaDon.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvHoaDon.Columns.Add(cotTongHoaDon);
            this.dgvHoaDon.Columns.Add(TaoCot("Thanh toán", "PhuongThucThanhToan", 110));
            this.dgvHoaDon.Columns.Add(TaoCot("Trạng thái", "TrangThaiHienThi", 112));
            this.dgvHoaDon.SelectionChanged += new System.EventHandler(this.dgvHoaDon_SelectionChanged);

            this.tabBanHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBanHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabBanHang.Controls.Add(this.tabLapHoaDon);
            this.tabBanHang.Controls.Add(this.tabLichSu);
            this.tabLapHoaDon.Text = "Lập hóa đơn";
            this.tabLapHoaDon.BackColor = System.Drawing.Color.White;
            this.tabLapHoaDon.Padding = new System.Windows.Forms.Padding(6);
            this.tabLapHoaDon.Controls.Add(this.dgvGioHang);
            this.tabLapHoaDon.Controls.Add(this.pnlDongBan);
            this.tabLapHoaDon.Controls.Add(this.pnlDauHoaDon);
            this.tabLichSu.Text = "Chi tiết hóa đơn";
            this.tabLichSu.BackColor = System.Drawing.Color.White;
            this.tabLichSu.Padding = new System.Windows.Forms.Padding(6);
            this.tabLichSu.Controls.Add(this.dgvChiTietHoaDon);
            this.tabLichSu.Controls.Add(this.pnlThongTinHoaDon);

            this.pnlDauHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDauHoaDon.Height = 58;
            this.pnlDauHoaDon.Size = new System.Drawing.Size(960, 58);
            this.pnlDauHoaDon.Controls.Add(TaoNhan("Khách hàng *", 4, 2));
            this.pnlDauHoaDon.Controls.Add(TaoNhan("Nhân viên lập", 238, 2));
            this.pnlDauHoaDon.Controls.Add(TaoNhan("Thời gian", 406, 2));
            this.pnlDauHoaDon.Controls.Add(TaoNhan("Thanh toán *", 548, 2));
            this.pnlDauHoaDon.Controls.Add(TaoNhan("Giảm giá", 718, 2));
            CauHinhCombo(this.cboKhachHang, 4, 23, 218);
            this.lblNhanVienLap.Location = new System.Drawing.Point(238, 24);
            this.lblNhanVienLap.Size = new System.Drawing.Size(152, 23);
            this.lblNhanVienLap.AutoEllipsis = true;
            this.lblNgayLap.Location = new System.Drawing.Point(406, 24);
            this.lblNgayLap.Size = new System.Drawing.Size(126, 23);
            CauHinhCombo(this.cboPhuongThucThanhToan, 548, 23, 154);
            this.cboPhuongThucThanhToan.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ ngân hàng" });
            CauHinhSo(this.numGiamGia, 718, 23, 210);
            this.numGiamGia.DecimalPlaces = 2;
            this.numGiamGia.ThousandsSeparator = true;
            this.numGiamGia.ValueChanged += new System.EventHandler(this.numGiamGia_ValueChanged);
            this.pnlDauHoaDon.Controls.Add(this.cboKhachHang);
            this.pnlDauHoaDon.Controls.Add(this.lblNhanVienLap);
            this.pnlDauHoaDon.Controls.Add(this.lblNgayLap);
            this.pnlDauHoaDon.Controls.Add(this.cboPhuongThucThanhToan);
            this.pnlDauHoaDon.Controls.Add(this.numGiamGia);

            this.pnlDongBan.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDongBan.Height = 70;
            this.pnlDongBan.Size = new System.Drawing.Size(960, 70);
            this.pnlDongBan.Controls.Add(TaoNhan("Sản phẩm *", 4, 2));
            this.pnlDongBan.Controls.Add(TaoNhan("Số lượng *", 264, 2));
            this.pnlDongBan.Controls.Add(TaoNhan("Đơn giá", 348, 2));
            this.pnlDongBan.Controls.Add(TaoNhan("Hạn bảo hành", 452, 2));
            CauHinhCombo(this.cboSanPham, 4, 23, 244);
            this.cboSanPham.SelectedIndexChanged += new System.EventHandler(this.cboSanPham_SelectedIndexChanged);
            this.lblTonKho.Location = new System.Drawing.Point(4, 48);
            this.lblTonKho.Size = new System.Drawing.Size(190, 20);
            CauHinhSo(this.numSoLuong, 264, 23, 68);
            this.numSoLuong.Minimum = 1;
            this.numSoLuong.Value = 1;
            this.lblDonGiaBan.Location = new System.Drawing.Point(348, 25);
            this.lblDonGiaBan.Size = new System.Drawing.Size(90, 22);
            this.lblDonGiaBan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            CauHinhNgay(this.dtpHanBaoHanh, 452, 23, 126);
            CauHinhNut(this.btnThemDong, "Thêm sản phẩm", 592, 20, 120, MauXanh());
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            CauHinhNut(this.btnXoaDong, "Xóa", 720, 20, 62, System.Drawing.Color.Firebrick);
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            CauHinhNut(this.btnMoiDong, "Nhập mới", 790, 20, 78, System.Drawing.Color.DimGray);
            this.btnMoiDong.Click += new System.EventHandler(this.btnMoiDong_Click);
            this.lblSoDong.Location = new System.Drawing.Point(874, 2);
            this.lblSoDong.Size = new System.Drawing.Size(76, 20);
            this.lblSoDong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTongTien.Location = new System.Drawing.Point(680, 48);
            this.lblTongTien.Size = new System.Drawing.Size(270, 20);
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = MauXanh();
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pnlDongBan.Controls.Add(this.cboSanPham);
            this.pnlDongBan.Controls.Add(this.numSoLuong);
            this.pnlDongBan.Controls.Add(this.lblDonGiaBan);
            this.pnlDongBan.Controls.Add(this.dtpHanBaoHanh);
            this.pnlDongBan.Controls.Add(this.lblTonKho);
            this.pnlDongBan.Controls.Add(this.btnThemDong);
            this.pnlDongBan.Controls.Add(this.btnXoaDong);
            this.pnlDongBan.Controls.Add(this.btnMoiDong);
            this.pnlDongBan.Controls.Add(this.lblSoDong);
            this.pnlDongBan.Controls.Add(this.lblTongTien);

            CauHinhLuoi(this.dgvGioHang, headerStyle);
            this.dgvGioHang.Columns.Add(TaoCot("Mã SP", "MaSanPham", 85));
            this.dgvGioHang.Columns.Add(TaoCot("Tên sản phẩm", "TenSanPham", 280));
            this.dgvGioHang.Columns.Add(TaoCot("Tồn", "TonKhoHienTai", 65));
            this.dgvGioHang.Columns.Add(TaoCot("Số lượng", "SoLuong", 75));
            var cotGiaBan = TaoCot("Đơn giá", "DonGiaBan", 125);
            cotGiaBan.DefaultCellStyle.Format = "N0";
            this.dgvGioHang.Columns.Add(cotGiaBan);
            var cotThanhTien = TaoCot("Thành tiền", "ThanhTien", 140);
            cotThanhTien.DefaultCellStyle.Format = "N0";
            this.dgvGioHang.Columns.Add(cotThanhTien);
            this.dgvGioHang.Columns.Add(TaoCot("Bảo hành đến", "HanBaoHanhHienThi", 115));
            this.dgvGioHang.SelectionChanged += new System.EventHandler(this.dgvGioHang_SelectionChanged);

            this.pnlThongTinHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTinHoaDon.Height = 76;
            this.pnlThongTinHoaDon.Size = new System.Drawing.Size(960, 76);
            this.pnlThongTinHoaDon.Controls.Add(TaoNhan("Mã HĐ", 4, 2));
            this.pnlThongTinHoaDon.Controls.Add(TaoNhan("Ngày lập", 98, 2));
            this.pnlThongTinHoaDon.Controls.Add(TaoNhan("Khách hàng", 238, 2));
            this.pnlThongTinHoaDon.Controls.Add(TaoNhan("Nhân viên", 418, 2));
            this.pnlThongTinHoaDon.Controls.Add(TaoNhan("Thanh toán", 568, 2));
            this.pnlThongTinHoaDon.Controls.Add(TaoNhan("Trạng thái", 704, 2));
            CauHinhGiaTri(this.lblMaHoaDonChiTiet, 4, 23, 84);
            CauHinhGiaTri(this.lblNgayLapChiTiet, 98, 23, 130);
            CauHinhGiaTri(this.lblKhachHangChiTiet, 238, 23, 170);
            CauHinhGiaTri(this.lblNhanVienChiTiet, 418, 23, 140);
            CauHinhGiaTri(this.lblThanhToanChiTiet, 568, 23, 126);
            CauHinhGiaTri(this.lblTrangThaiChiTiet, 704, 23, 128);
            this.lblTienChiTiet.Location = new System.Drawing.Point(4, 49);
            this.lblTienChiTiet.Size = new System.Drawing.Size(925, 22);
            this.lblTienChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTienChiTiet.ForeColor = MauXanh();
            this.pnlThongTinHoaDon.Controls.Add(this.lblMaHoaDonChiTiet);
            this.pnlThongTinHoaDon.Controls.Add(this.lblNgayLapChiTiet);
            this.pnlThongTinHoaDon.Controls.Add(this.lblKhachHangChiTiet);
            this.pnlThongTinHoaDon.Controls.Add(this.lblNhanVienChiTiet);
            this.pnlThongTinHoaDon.Controls.Add(this.lblThanhToanChiTiet);
            this.pnlThongTinHoaDon.Controls.Add(this.lblTrangThaiChiTiet);
            this.pnlThongTinHoaDon.Controls.Add(this.lblTienChiTiet);

            CauHinhLuoi(this.dgvChiTietHoaDon, headerStyle);
            this.dgvChiTietHoaDon.Columns.Add(TaoCot("Mã SP", "MaSanPham", 85));
            this.dgvChiTietHoaDon.Columns.Add(TaoCot("Tên sản phẩm", "TenSanPham", 350));
            this.dgvChiTietHoaDon.Columns.Add(TaoCot("Số lượng", "SoLuong", 80));
            var cotGiaChiTiet = TaoCot("Đơn giá", "DonGiaBan", 130);
            cotGiaChiTiet.DefaultCellStyle.Format = "N0";
            this.dgvChiTietHoaDon.Columns.Add(cotGiaChiTiet);
            var cotTienChiTiet = TaoCot("Thành tiền", "ThanhTien", 150);
            cotTienChiTiet.DefaultCellStyle.Format = "N0";
            this.dgvChiTietHoaDon.Columns.Add(cotTienChiTiet);
            this.dgvChiTietHoaDon.Columns.Add(TaoCot("Bảo hành đến", "HanBaoHanhHienThi", 120));

            this.pnlChan.BackColor = System.Drawing.Color.White;
            this.pnlChan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChan.Height = 58;
            this.pnlChan.Size = new System.Drawing.Size(1000, 58);
            this.lblThongBao.ForeColor = System.Drawing.Color.Crimson;
            this.lblThongBao.Location = new System.Drawing.Point(14, 5);
            this.lblThongBao.Size = new System.Drawing.Size(410, 46);
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThongBao.AutoEllipsis = true;
            CauHinhNut(this.btnLuuHoaDon, "Thanh toán", 438, 12, 118, MauXanh());
            this.btnLuuHoaDon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnLuuHoaDon.Click += new System.EventHandler(this.btnLuuHoaDon_Click);
            CauHinhNut(this.btnHuyHoaDon, "Hủy hóa đơn", 564, 12, 120, System.Drawing.Color.Firebrick);
            this.btnHuyHoaDon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnHuyHoaDon.Click += new System.EventHandler(this.btnHuyHoaDon_Click);
            CauHinhNut(this.btnInHoaDon, "Xem báo cáo", 692, 12, 106, System.Drawing.Color.FromArgb(44, 95, 138));
            this.btnInHoaDon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            CauHinhNut(this.btnHoaDonMoi, "Hóa đơn mới", 806, 12, 110, System.Drawing.Color.DimGray);
            this.btnHoaDonMoi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnHoaDonMoi.Click += new System.EventHandler(this.btnHoaDonMoi_Click);
            this.pnlChan.Controls.Add(this.lblThongBao);
            this.pnlChan.Controls.Add(this.btnLuuHoaDon);
            this.pnlChan.Controls.Add(this.btnHuyHoaDon);
            this.pnlChan.Controls.Add(this.btnInHoaDon);
            this.pnlChan.Controls.Add(this.btnHoaDonMoi);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 248);
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitChinh);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlChan);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmBanHang";
            this.Text = "Bán hàng và hóa đơn";
            this.Load += new System.EventHandler(this.FrmBanHang_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitChinh.Panel1.ResumeLayout(false);
            this.splitChinh.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).EndInit();
            this.splitChinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.tabBanHang.ResumeLayout(false);
            this.tabLapHoaDon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.pnlDongBan.ResumeLayout(false);
            this.pnlDongBan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.pnlDauHoaDon.ResumeLayout(false);
            this.pnlDauHoaDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGia)).EndInit();
            this.tabLichSu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).EndInit();
            this.pnlThongTinHoaDon.ResumeLayout(false);
            this.pnlThongTinHoaDon.PerformLayout();
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
        private System.Windows.Forms.ComboBox cboLocKhachHang;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.TextBox txtTienTu;
        private System.Windows.Forms.TextBox txtTienDen;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.SplitContainer splitChinh;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.TabControl tabBanHang;
        private System.Windows.Forms.TabPage tabLapHoaDon;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Panel pnlDongBan;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label lblDonGiaBan;
        private System.Windows.Forms.DateTimePicker dtpHanBaoHanh;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnMoiDong;
        private System.Windows.Forms.Label lblSoDong;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Panel pnlDauHoaDon;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblNhanVienLap;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.ComboBox cboPhuongThucThanhToan;
        private System.Windows.Forms.NumericUpDown numGiamGia;
        private System.Windows.Forms.TabPage tabLichSu;
        private System.Windows.Forms.DataGridView dgvChiTietHoaDon;
        private System.Windows.Forms.Panel pnlThongTinHoaDon;
        private System.Windows.Forms.Label lblMaHoaDonChiTiet;
        private System.Windows.Forms.Label lblNgayLapChiTiet;
        private System.Windows.Forms.Label lblKhachHangChiTiet;
        private System.Windows.Forms.Label lblNhanVienChiTiet;
        private System.Windows.Forms.Label lblThanhToanChiTiet;
        private System.Windows.Forms.Label lblTrangThaiChiTiet;
        private System.Windows.Forms.Label lblTienChiTiet;
        private System.Windows.Forms.Panel pnlChan;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Button btnLuuHoaDon;
        private System.Windows.Forms.Button btnHuyHoaDon;
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Button btnHoaDonMoi;
    }
}

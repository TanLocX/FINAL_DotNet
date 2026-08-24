namespace FINAL_DotNet
{
    partial class FrmQuanLyEmail
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabChinh = new FINAL_DotNet.DarkGoldTabControl();
            this.tabSmtp = new System.Windows.Forms.TabPage();
            this.txtMayChuSmtp = new System.Windows.Forms.TextBox();
            this.nudCongSmtp = new System.Windows.Forms.NumericUpDown();
            this.chkSuDungSsl = new System.Windows.Forms.CheckBox();
            this.txtTaiKhoanSmtp = new System.Windows.Forms.TextBox();
            this.txtMatKhauSmtp = new System.Windows.Forms.TextBox();
            this.txtTenNguoiGui = new System.Windows.Forms.TextBox();
            this.btnLuuSmtp = new System.Windows.Forms.Button();
            this.lblTrangThaiSmtp = new System.Windows.Forms.Label();
            this.tabGuiDon = new System.Windows.Forms.TabPage();
            this.cboKhachHangDon = new System.Windows.Forms.ComboBox();
            this.cboHoaDonDon = new System.Windows.Forms.ComboBox();
            this.cboMauGuiDon = new System.Windows.Forms.ComboBox();
            this.txtEmailDon = new System.Windows.Forms.TextBox();
            this.txtTieuDeDon = new System.Windows.Forms.TextBox();
            this.txtNoiDungDon = new System.Windows.Forms.TextBox();
            this.lstTepDon = new System.Windows.Forms.ListBox();
            this.btnThemTepDon = new System.Windows.Forms.Button();
            this.btnXoaTepDon = new System.Windows.Forms.Button();
            this.btnGuiDon = new System.Windows.Forms.Button();
            this.lblTrangThaiGuiDon = new System.Windows.Forms.Label();
            this.tabHangLoat = new System.Windows.Forms.TabPage();
            this.cboMauHangLoat = new System.Windows.Forms.ComboBox();
            this.txtTieuDeHangLoat = new System.Windows.Forms.TextBox();
            this.txtNoiDungHangLoat = new System.Windows.Forms.TextBox();
            this.lblToken = new System.Windows.Forms.Label();
            this.dgvNguoiNhan = new System.Windows.Forms.DataGridView();
            this.lblSoNguoiNhan = new System.Windows.Forms.Label();
            this.btnTaiNguoiNhan = new System.Windows.Forms.Button();
            this.chkHenGio = new System.Windows.Forms.CheckBox();
            this.dtpHenGio = new FINAL_DotNet.DarkGoldDateTimePicker();
            this.progressHangLoat = new System.Windows.Forms.ProgressBar();
            this.lblTrangThaiHangLoat = new System.Windows.Forms.Label();
            this.btnGuiHangLoat = new System.Windows.Forms.Button();
            this.tabMauEmail = new System.Windows.Forms.TabPage();
            this.lstMauEmail = new System.Windows.Forms.ListBox();
            this.txtTenMau = new System.Windows.Forms.TextBox();
            this.txtTieuDeMau = new System.Windows.Forms.TextBox();
            this.txtNoiDungMau = new System.Windows.Forms.TextBox();
            this.chkMauHoatDong = new System.Windows.Forms.CheckBox();
            this.btnMauMoi = new System.Windows.Forms.Button();
            this.btnLuuMau = new System.Windows.Forms.Button();
            this.btnKhoaMau = new System.Windows.Forms.Button();
            this.btnTaoMauMacDinh = new System.Windows.Forms.Button();
            this.tabNhatKy = new System.Windows.Forms.TabPage();
            this.txtTimNhatKy = new System.Windows.Forms.TextBox();
            this.dtpTuNgayNhatKy = new FINAL_DotNet.DarkGoldDateTimePicker();
            this.dtpDenNgayNhatKy = new FINAL_DotNet.DarkGoldDateTimePicker();
            this.cboLocLoaiGui = new System.Windows.Forms.ComboBox();
            this.cboLocTrangThaiNhatKy = new System.Windows.Forms.ComboBox();
            this.cboLocMauNhatKy = new System.Windows.Forms.ComboBox();
            this.btnTimNhatKy = new System.Windows.Forms.Button();
            this.btnTaiLaiNhatKy = new System.Windows.Forms.Button();
            this.lblSoNhatKy = new System.Windows.Forms.Label();
            this.dgvNhatKy = new System.Windows.Forms.DataGridView();
            this.pnlChan = new System.Windows.Forms.Panel();
            this.lblLoi = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCongSmtp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhatKy)).BeginInit();
            this.SuspendLayout();

            this.tabChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabChinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tabChinh.Controls.Add(this.tabSmtp);
            this.tabChinh.Controls.Add(this.tabGuiDon);
            this.tabChinh.Controls.Add(this.tabHangLoat);
            this.tabChinh.Controls.Add(this.tabMauEmail);
            this.tabChinh.Controls.Add(this.tabNhatKy);

            CauHinhTrang(this.tabSmtp, "Cấu hình SMTP");
            CauHinhTrang(this.tabGuiDon, "Gửi email đơn");
            CauHinhTrang(this.tabHangLoat, "Gửi hàng loạt");
            CauHinhTrang(this.tabMauEmail, "Mẫu email");
            CauHinhTrang(this.tabNhatKy, "Nhật ký gửi");

            KhoiTaoTabSmtp();
            KhoiTaoTabGuiDon();
            KhoiTaoTabHangLoat();
            KhoiTaoTabMauEmail();
            KhoiTaoTabNhatKy();

            this.pnlChan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChan.Height = 48;
            this.pnlChan.BackColor = System.Drawing.Color.FromArgb(30, 27, 24);
            this.lblLoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoi.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.lblLoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLoi.ForeColor = System.Drawing.Color.Crimson;
            this.lblLoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoi.AutoEllipsis = true;
            this.pnlChan.Controls.Add(this.lblLoi);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(30, 27, 24);
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.tabChinh);
            this.Controls.Add(this.pnlChan);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmQuanLyEmail";
            this.Text = "Quản lý và gửi email chăm sóc khách hàng";
            this.Load += new System.EventHandler(this.FrmQuanLyEmail_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmQuanLyEmail_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudCongSmtp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhatKy)).EndInit();
            this.ResumeLayout(false);
        }

        private void KhoiTaoTabSmtp()
        {
            var card = TaoCard(30, 30, 910, 360);
            card.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            card.Controls.Add(TaoTieuDe("THÔNG TIN MÁY CHỦ SMTP", 24, 18, 500));
            card.Controls.Add(TaoNhan("Máy chủ SMTP *", 24, 68));
            card.Controls.Add(TaoNhan("Cổng *", 484, 68));
            CauHinhNhap(this.txtMayChuSmtp, 24, 90, 430, false);
            this.txtMayChuSmtp.MaxLength = 255;
            this.nudCongSmtp.Location = new System.Drawing.Point(484, 90);
            this.nudCongSmtp.Size = new System.Drawing.Size(130, 23);
            this.nudCongSmtp.Minimum = 1;
            this.nudCongSmtp.Maximum = 65535;
            this.nudCongSmtp.Value = 587;
            this.chkSuDungSsl.Location = new System.Drawing.Point(644, 90);
            this.chkSuDungSsl.AutoSize = true;
            this.chkSuDungSsl.Text = "Sử dụng SSL/TLS";
            this.chkSuDungSsl.ForeColor = MauChu();
            card.Controls.Add(TaoNhan("Tài khoản email *", 24, 132));
            card.Controls.Add(TaoNhan("Mật khẩu ứng dụng", 484, 132));
            CauHinhNhap(this.txtTaiKhoanSmtp, 24, 154, 430, false);
            this.txtTaiKhoanSmtp.MaxLength = 254;
            CauHinhNhap(this.txtMatKhauSmtp, 484, 154, 370, false);
            this.txtMatKhauSmtp.UseSystemPasswordChar = true;
            card.Controls.Add(TaoNhan("Tên người gửi", 24, 196));
            CauHinhNhap(this.txtTenNguoiGui, 24, 218, 430, false);
            this.txtTenNguoiGui.MaxLength = 100;
            var huongDan = new System.Windows.Forms.Label
            {
                Location = new System.Drawing.Point(484, 198), Size = new System.Drawing.Size(370, 56),
                ForeColor = MauChuPhu(), Text = "Mật khẩu được lưu trong biến môi trường người dùng, không lưu trong CSDL. Để trống khi lưu để giữ mật khẩu hiện tại."
            };
            CauHinhNut(this.btnLuuSmtp, "Lưu cấu hình", 24, 278, 155, MauVang());
            this.btnLuuSmtp.Click += new System.EventHandler(this.btnLuuSmtp_Click);
            this.lblTrangThaiSmtp.Location = new System.Drawing.Point(198, 278);
            this.lblTrangThaiSmtp.Size = new System.Drawing.Size(656, 36);
            this.lblTrangThaiSmtp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            card.Controls.Add(this.txtMayChuSmtp);
            card.Controls.Add(this.nudCongSmtp);
            card.Controls.Add(this.chkSuDungSsl);
            card.Controls.Add(this.txtTaiKhoanSmtp);
            card.Controls.Add(this.txtMatKhauSmtp);
            card.Controls.Add(this.txtTenNguoiGui);
            card.Controls.Add(huongDan);
            card.Controls.Add(this.btnLuuSmtp);
            card.Controls.Add(this.lblTrangThaiSmtp);
            this.tabSmtp.Controls.Add(card);
        }

        private void KhoiTaoTabGuiDon()
        {
            var card = TaoCard(18, 18, 948, 518);
            card.Anchor = TatCaNeo();
            card.Controls.Add(TaoNhan("Khách hàng", 18, 14));
            card.Controls.Add(TaoNhan("Hóa đơn đã thanh toán", 326, 14));
            card.Controls.Add(TaoNhan("Mẫu email", 644, 14));
            CauHinhCombo(this.cboKhachHangDon, 18, 36, 290);
            this.cboKhachHangDon.SelectedIndexChanged += new System.EventHandler(this.cboKhachHangDon_SelectedIndexChanged);
            CauHinhCombo(this.cboHoaDonDon, 326, 36, 300);
            CauHinhCombo(this.cboMauGuiDon, 644, 36, 280);
            this.cboMauGuiDon.SelectedIndexChanged += new System.EventHandler(this.cboMauGuiDon_SelectedIndexChanged);
            card.Controls.Add(TaoNhan("Email người nhận *", 18, 76));
            card.Controls.Add(TaoNhan("Tiêu đề *", 326, 76));
            CauHinhNhap(this.txtEmailDon, 18, 98, 290, false);
            this.txtEmailDon.MaxLength = 254;
            CauHinhNhap(this.txtTieuDeDon, 326, 98, 598, false);
            this.txtTieuDeDon.MaxLength = 255;
            card.Controls.Add(TaoNhan("Nội dung HTML hoặc văn bản *", 18, 138));
            CauHinhNhap(this.txtNoiDungDon, 18, 160, 598, true);
            this.txtNoiDungDon.Size = new System.Drawing.Size(598, 242);
            this.txtNoiDungDon.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            card.Controls.Add(TaoNhan("Tệp đính kèm", 636, 138));
            this.lstTepDon.Location = new System.Drawing.Point(636, 160);
            this.lstTepDon.Size = new System.Drawing.Size(288, 184);
            this.lstTepDon.BackColor = MauNhap();
            this.lstTepDon.ForeColor = MauChu();
            CauHinhNut(this.btnThemTepDon, "Thêm tệp...", 636, 354, 132, MauXanh());
            this.btnThemTepDon.Click += new System.EventHandler(this.btnThemTepDon_Click);
            CauHinhNut(this.btnXoaTepDon, "Xóa tệp", 776, 354, 110, System.Drawing.Color.DimGray);
            this.btnXoaTepDon.Click += new System.EventHandler(this.btnXoaTepDon_Click);
            CauHinhNut(this.btnGuiDon, "Gửi email", 18, 430, 155, MauVang());
            this.btnGuiDon.Click += new System.EventHandler(this.btnGuiDon_Click);
            this.lblTrangThaiGuiDon.Location = new System.Drawing.Point(190, 430);
            this.lblTrangThaiGuiDon.Size = new System.Drawing.Size(734, 36);
            this.lblTrangThaiGuiDon.ForeColor = MauChuPhu();
            this.lblTrangThaiGuiDon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrangThaiGuiDon.Text = "Sẵn sàng...";
            card.Controls.Add(this.cboKhachHangDon);
            card.Controls.Add(this.cboHoaDonDon);
            card.Controls.Add(this.cboMauGuiDon);
            card.Controls.Add(this.txtEmailDon);
            card.Controls.Add(this.txtTieuDeDon);
            card.Controls.Add(this.txtNoiDungDon);
            card.Controls.Add(this.lstTepDon);
            card.Controls.Add(this.btnThemTepDon);
            card.Controls.Add(this.btnXoaTepDon);
            card.Controls.Add(this.btnGuiDon);
            card.Controls.Add(this.lblTrangThaiGuiDon);
            this.tabGuiDon.Controls.Add(card);
        }

        private void KhoiTaoTabHangLoat()
        {
            this.tabHangLoat.Controls.Add(TaoNhan("Mẫu email", 16, 10));
            this.tabHangLoat.Controls.Add(TaoNhan("Tiêu đề hỗ trợ placeholder", 306, 10));
            CauHinhCombo(this.cboMauHangLoat, 16, 32, 272);
            this.cboMauHangLoat.SelectedIndexChanged += new System.EventHandler(this.cboMauHangLoat_SelectedIndexChanged);
            CauHinhNhap(this.txtTieuDeHangLoat, 306, 32, 660, false);
            this.txtTieuDeHangLoat.MaxLength = 255;
            this.tabHangLoat.Controls.Add(TaoNhan("Nội dung HTML hoặc văn bản", 16, 70));
            CauHinhNhap(this.txtNoiDungHangLoat, 16, 92, 950, true);
            this.txtNoiDungHangLoat.Size = new System.Drawing.Size(950, 112);
            this.txtNoiDungHangLoat.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.lblToken.Location = new System.Drawing.Point(16, 210);
            this.lblToken.Size = new System.Drawing.Size(950, 22);
            this.lblToken.ForeColor = System.Drawing.Color.FromArgb(232, 195, 75);
            this.lblToken.Text = "Placeholder: {HoTen}, {Sdt}, {Email}, {TenSanPham}, {TongTien}, {NgayMua}, {HanBaoHanh}, {MaHoaDon}";
            this.tabHangLoat.Controls.Add(this.lblToken);
            CauHinhLuoi(this.dgvNguoiNhan);
            this.dgvNguoiNhan.Location = new System.Drawing.Point(16, 238);
            this.dgvNguoiNhan.Size = new System.Drawing.Size(950, 226);
            this.dgvNguoiNhan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvNguoiNhan.Columns.Add(TaoCotChon("Gửi", "DuocChon", 45));
            this.dgvNguoiNhan.Columns.Add(TaoCot("Khách hàng", "HoTen", 155));
            this.dgvNguoiNhan.Columns.Add(TaoCot("Email", "Email", 190));
            this.dgvNguoiNhan.Columns.Add(TaoCot("Số ĐT", "SoDienThoai", 100));
            this.dgvNguoiNhan.Columns.Add(TaoCot("Hóa đơn", "MaHoaDon", 85));
            this.dgvNguoiNhan.Columns.Add(TaoCot("Sản phẩm gần nhất", "TenSanPham", 210));
            this.dgvNguoiNhan.Columns.Add(TaoCot("Giá trị", "TongTien", 110));
            this.lblSoNguoiNhan.Location = new System.Drawing.Point(16, 472);
            this.lblSoNguoiNhan.Size = new System.Drawing.Size(220, 28);
            this.lblSoNguoiNhan.ForeColor = MauChu();
            this.lblSoNguoiNhan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            CauHinhNut(this.btnTaiNguoiNhan, "Tải lại từ CSDL", 242, 468, 140, MauXanh());
            this.btnTaiNguoiNhan.Click += new System.EventHandler(this.btnTaiNguoiNhan_Click);
            this.chkHenGio.Location = new System.Drawing.Point(398, 477);
            this.chkHenGio.AutoSize = true;
            this.chkHenGio.Text = "Hẹn giờ gửi";
            this.chkHenGio.ForeColor = MauChu();
            this.chkHenGio.CheckedChanged += new System.EventHandler(this.chkHenGio_CheckedChanged);
            this.dtpHenGio.Location = new System.Drawing.Point(500, 474);
            this.dtpHenGio.Size = new System.Drawing.Size(190, 23);
            this.dtpHenGio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHenGio.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtpHenGio.Enabled = false;
            CauHinhNut(this.btnGuiHangLoat, "Bắt đầu gửi", 808, 468, 158, MauVang());
            this.btnGuiHangLoat.Click += new System.EventHandler(this.btnGuiHangLoat_Click);
            this.progressHangLoat.Location = new System.Drawing.Point(16, 510);
            this.progressHangLoat.Size = new System.Drawing.Size(366, 20);
            this.lblTrangThaiHangLoat.Location = new System.Drawing.Point(398, 504);
            this.lblTrangThaiHangLoat.Size = new System.Drawing.Size(568, 30);
            this.lblTrangThaiHangLoat.ForeColor = MauChuPhu();
            this.lblTrangThaiHangLoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrangThaiHangLoat.Text = "Sẵn sàng...";
            this.tabHangLoat.Controls.Add(this.cboMauHangLoat);
            this.tabHangLoat.Controls.Add(this.txtTieuDeHangLoat);
            this.tabHangLoat.Controls.Add(this.txtNoiDungHangLoat);
            this.tabHangLoat.Controls.Add(this.dgvNguoiNhan);
            this.tabHangLoat.Controls.Add(this.lblSoNguoiNhan);
            this.tabHangLoat.Controls.Add(this.btnTaiNguoiNhan);
            this.tabHangLoat.Controls.Add(this.chkHenGio);
            this.tabHangLoat.Controls.Add(this.dtpHenGio);
            this.tabHangLoat.Controls.Add(this.progressHangLoat);
            this.tabHangLoat.Controls.Add(this.lblTrangThaiHangLoat);
            this.tabHangLoat.Controls.Add(this.btnGuiHangLoat);
        }

        private void KhoiTaoTabMauEmail()
        {
            this.lstMauEmail.Location = new System.Drawing.Point(16, 18);
            this.lstMauEmail.Size = new System.Drawing.Size(280, 454);
            this.lstMauEmail.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lstMauEmail.BackColor = MauNhap();
            this.lstMauEmail.ForeColor = MauChu();
            this.lstMauEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstMauEmail.SelectedIndexChanged += new System.EventHandler(this.lstMauEmail_SelectedIndexChanged);
            var card = TaoCard(314, 18, 652, 454);
            card.Anchor = TatCaNeo();
            card.Controls.Add(TaoTieuDe("CHI TIẾT MẪU EMAIL TRONG CSDL", 18, 14, 500));
            card.Controls.Add(TaoNhan("Tên mẫu *", 18, 56));
            CauHinhNhap(this.txtTenMau, 18, 78, 606, false);
            this.txtTenMau.MaxLength = 100;
            card.Controls.Add(TaoNhan("Tiêu đề *", 18, 116));
            CauHinhNhap(this.txtTieuDeMau, 18, 138, 606, false);
            this.txtTieuDeMau.MaxLength = 255;
            card.Controls.Add(TaoNhan("Nội dung HTML hoặc văn bản *", 18, 176));
            CauHinhNhap(this.txtNoiDungMau, 18, 198, 606, true);
            this.txtNoiDungMau.Size = new System.Drawing.Size(606, 174);
            this.txtNoiDungMau.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.chkMauHoatDong.Location = new System.Drawing.Point(18, 388);
            this.chkMauHoatDong.AutoSize = true;
            this.chkMauHoatDong.Text = "Đang hoạt động";
            this.chkMauHoatDong.ForeColor = MauChu();
            card.Controls.Add(this.txtTenMau);
            card.Controls.Add(this.txtTieuDeMau);
            card.Controls.Add(this.txtNoiDungMau);
            card.Controls.Add(this.chkMauHoatDong);
            CauHinhNut(this.btnMauMoi, "Mẫu mới", 16, 486, 112, System.Drawing.Color.DimGray);
            this.btnMauMoi.Click += new System.EventHandler(this.btnMauMoi_Click);
            CauHinhNut(this.btnTaoMauMacDinh, "Tạo 3 mẫu mặc định", 136, 486, 160, MauXanh());
            this.btnTaoMauMacDinh.Click += new System.EventHandler(this.btnTaoMauMacDinh_Click);
            CauHinhNut(this.btnLuuMau, "Lưu vào CSDL", 586, 486, 142, MauVang());
            this.btnLuuMau.Click += new System.EventHandler(this.btnLuuMau_Click);
            CauHinhNut(this.btnKhoaMau, "Ngừng sử dụng", 738, 486, 152, System.Drawing.Color.FromArgb(198, 40, 40));
            this.btnKhoaMau.Click += new System.EventHandler(this.btnKhoaMau_Click);
            this.tabMauEmail.Controls.Add(this.lstMauEmail);
            this.tabMauEmail.Controls.Add(card);
            this.tabMauEmail.Controls.Add(this.btnMauMoi);
            this.tabMauEmail.Controls.Add(this.btnTaoMauMacDinh);
            this.tabMauEmail.Controls.Add(this.btnLuuMau);
            this.tabMauEmail.Controls.Add(this.btnKhoaMau);
        }

        private void KhoiTaoTabNhatKy()
        {
            this.tabNhatKy.Controls.Add(TaoNhan("Email / khách / tiêu đề", 16, 8));
            this.tabNhatKy.Controls.Add(TaoNhan("Từ ngày", 224, 8));
            this.tabNhatKy.Controls.Add(TaoNhan("Đến ngày", 354, 8));
            this.tabNhatKy.Controls.Add(TaoNhan("Loại gửi", 484, 8));
            this.tabNhatKy.Controls.Add(TaoNhan("Trạng thái", 616, 8));
            this.tabNhatKy.Controls.Add(TaoNhan("Mẫu email", 748, 8));
            CauHinhNhap(this.txtTimNhatKy, 16, 30, 192, false);
            this.txtTimNhatKy.MaxLength = 255;
            CauHinhNgay(this.dtpTuNgayNhatKy, 224, 30, 114);
            CauHinhNgay(this.dtpDenNgayNhatKy, 354, 30, 114);
            CauHinhCombo(this.cboLocLoaiGui, 484, 30, 116);
            this.cboLocLoaiGui.Items.AddRange(new object[] { "Tất cả", "Đơn", "Hàng loạt" });
            this.cboLocLoaiGui.SelectedIndex = 0;
            CauHinhCombo(this.cboLocTrangThaiNhatKy, 616, 30, 116);
            this.cboLocTrangThaiNhatKy.Items.AddRange(new object[] { "Tất cả", "Thành công", "Thất bại" });
            this.cboLocTrangThaiNhatKy.SelectedIndex = 0;
            CauHinhCombo(this.cboLocMauNhatKy, 748, 30, 218);
            CauHinhNut(this.btnTimNhatKy, "Tìm kiếm", 16, 66, 106, MauXanh());
            this.btnTimNhatKy.Click += new System.EventHandler(this.btnTimNhatKy_Click);
            CauHinhNut(this.btnTaiLaiNhatKy, "Tải lại", 130, 66, 90, System.Drawing.Color.DimGray);
            this.btnTaiLaiNhatKy.Click += new System.EventHandler(this.btnTaiLaiNhatKy_Click);
            this.lblSoNhatKy.Location = new System.Drawing.Point(238, 66);
            this.lblSoNhatKy.Size = new System.Drawing.Size(728, 34);
            this.lblSoNhatKy.ForeColor = MauChu();
            this.lblSoNhatKy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            CauHinhLuoi(this.dgvNhatKy);
            this.dgvNhatKy.Location = new System.Drawing.Point(16, 108);
            this.dgvNhatKy.Size = new System.Drawing.Size(950, 424);
            this.dgvNhatKy.Anchor = TatCaNeo();
            this.dgvNhatKy.Columns.Add(TaoCot("Thời gian gửi", "ThoiGian", 132));
            this.dgvNhatKy.Columns.Add(TaoCot("Email nhận", "EmailNhan", 180));
            this.dgvNhatKy.Columns.Add(TaoCot("Khách hàng", "KhachHang", 145));
            this.dgvNhatKy.Columns.Add(TaoCot("Hóa đơn", "HoaDon", 85));
            this.dgvNhatKy.Columns.Add(TaoCot("Mẫu", "MauEmail", 135));
            this.dgvNhatKy.Columns.Add(TaoCot("Tiêu đề", "TieuDe", 260));
            this.dgvNhatKy.Columns.Add(TaoCot("Loại", "LoaiGui", 80));
            this.dgvNhatKy.Columns.Add(TaoCot("Trạng thái", "TrangThai", 95));
            this.dgvNhatKy.Columns.Add(TaoCot("Người gửi", "NguoiGui", 130));
            this.dgvNhatKy.Columns.Add(TaoCot("Ghi chú / lỗi", "GhiChu", 280));
            this.tabNhatKy.Controls.Add(this.txtTimNhatKy);
            this.tabNhatKy.Controls.Add(this.dtpTuNgayNhatKy);
            this.tabNhatKy.Controls.Add(this.dtpDenNgayNhatKy);
            this.tabNhatKy.Controls.Add(this.cboLocLoaiGui);
            this.tabNhatKy.Controls.Add(this.cboLocTrangThaiNhatKy);
            this.tabNhatKy.Controls.Add(this.cboLocMauNhatKy);
            this.tabNhatKy.Controls.Add(this.btnTimNhatKy);
            this.tabNhatKy.Controls.Add(this.btnTaiLaiNhatKy);
            this.tabNhatKy.Controls.Add(this.lblSoNhatKy);
            this.tabNhatKy.Controls.Add(this.dgvNhatKy);
        }

        private static void CauHinhTrang(System.Windows.Forms.TabPage trang, string tieuDe)
        {
            trang.Text = tieuDe;
            trang.BackColor = System.Drawing.Color.FromArgb(30, 27, 24);
            trang.ForeColor = MauChu();
            trang.Padding = new System.Windows.Forms.Padding(6);
        }

        private static System.Windows.Forms.Panel TaoCard(int x, int y, int width, int height)
        {
            return new System.Windows.Forms.Panel
            {
                Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(width, height),
                BackColor = System.Drawing.Color.FromArgb(42, 38, 34), BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };
        }

        private static System.Windows.Forms.Label TaoNhan(string text, int x, int y)
        {
            return new System.Windows.Forms.Label
            {
                AutoSize = true, Location = new System.Drawing.Point(x, y), Text = text,
                ForeColor = MauChuPhu(), Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            };
        }

        private static System.Windows.Forms.Label TaoTieuDe(string text, int x, int y, int width)
        {
            return new System.Windows.Forms.Label
            {
                Location = new System.Drawing.Point(x, y), Size = new System.Drawing.Size(width, 30), Text = text,
                ForeColor = MauVang(), Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)
            };
        }

        private static void CauHinhNhap(System.Windows.Forms.TextBox control, int x, int y, int width, bool multiline)
        {
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, multiline ? 80 : 23);
            control.Multiline = multiline;
            control.BackColor = MauNhap();
            control.ForeColor = MauChu();
            control.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private static void CauHinhCombo(System.Windows.Forms.ComboBox control, int x, int y, int width)
        {
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, 23);
            control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            control.BackColor = MauNhap();
            control.ForeColor = MauChu();
        }

        private static void CauHinhNgay(System.Windows.Forms.DateTimePicker control, int x, int y, int width)
        {
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, 23);
            control.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            control.CustomFormat = "dd/MM/yyyy";
            control.ShowCheckBox = true;
            control.Checked = false;
        }

        private static void CauHinhNut(System.Windows.Forms.Button control, string text, int x, int y, int width, System.Drawing.Color color)
        {
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, 36);
            control.Text = text;
            control.BackColor = color;
            control.ForeColor = System.Drawing.Color.White;
            control.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            control.FlatAppearance.BorderSize = 0;
            control.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            control.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private static void CauHinhLuoi(System.Windows.Forms.DataGridView control)
        {
            control.AutoGenerateColumns = false;
            control.AllowUserToAddRows = false;
            control.AllowUserToDeleteRows = false;
            control.AllowUserToResizeRows = false;
            control.MultiSelect = false;
            control.RowHeadersVisible = false;
            control.BackgroundColor = System.Drawing.Color.FromArgb(42, 38, 34);
            control.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            control.EnableHeadersVisualStyles = false;
            control.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(58, 52, 46);
            control.ColumnHeadersDefaultCellStyle.ForeColor = MauChu();
            control.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            control.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(42, 38, 34);
            control.DefaultCellStyle.ForeColor = MauChu();
            control.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(70, 64, 57);
            control.DefaultCellStyle.SelectionForeColor = MauChu();
            control.RowTemplate.Height = 27;
            control.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        }

        private static System.Windows.Forms.DataGridViewTextBoxColumn TaoCot(string tieuDe, string thuocTinh, int width)
        {
            return new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                HeaderText = tieuDe, DataPropertyName = thuocTinh, Width = width, ReadOnly = true,
                SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            };
        }

        private static System.Windows.Forms.DataGridViewCheckBoxColumn TaoCotChon(string tieuDe, string thuocTinh, int width)
        {
            return new System.Windows.Forms.DataGridViewCheckBoxColumn
            {
                HeaderText = tieuDe, DataPropertyName = thuocTinh, Width = width
            };
        }

        private static System.Windows.Forms.AnchorStyles TatCaNeo()
        {
            return System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        }

        private static System.Drawing.Color MauNhap() => System.Drawing.Color.FromArgb(30, 27, 24);
        private static System.Drawing.Color MauChu() => System.Drawing.Color.FromArgb(245, 240, 235);
        private static System.Drawing.Color MauChuPhu() => System.Drawing.Color.FromArgb(180, 174, 168);
        private static System.Drawing.Color MauVang() => System.Drawing.Color.FromArgb(212, 175, 55);
        private static System.Drawing.Color MauXanh() => System.Drawing.Color.FromArgb(46, 125, 96);

        private System.Windows.Forms.TabControl tabChinh;
        private System.Windows.Forms.TabPage tabSmtp;
        private System.Windows.Forms.TextBox txtMayChuSmtp;
        private System.Windows.Forms.NumericUpDown nudCongSmtp;
        private System.Windows.Forms.CheckBox chkSuDungSsl;
        private System.Windows.Forms.TextBox txtTaiKhoanSmtp;
        private System.Windows.Forms.TextBox txtMatKhauSmtp;
        private System.Windows.Forms.TextBox txtTenNguoiGui;
        private System.Windows.Forms.Button btnLuuSmtp;
        private System.Windows.Forms.Label lblTrangThaiSmtp;
        private System.Windows.Forms.TabPage tabGuiDon;
        private System.Windows.Forms.ComboBox cboKhachHangDon;
        private System.Windows.Forms.ComboBox cboHoaDonDon;
        private System.Windows.Forms.ComboBox cboMauGuiDon;
        private System.Windows.Forms.TextBox txtEmailDon;
        private System.Windows.Forms.TextBox txtTieuDeDon;
        private System.Windows.Forms.TextBox txtNoiDungDon;
        private System.Windows.Forms.ListBox lstTepDon;
        private System.Windows.Forms.Button btnThemTepDon;
        private System.Windows.Forms.Button btnXoaTepDon;
        private System.Windows.Forms.Button btnGuiDon;
        private System.Windows.Forms.Label lblTrangThaiGuiDon;
        private System.Windows.Forms.TabPage tabHangLoat;
        private System.Windows.Forms.ComboBox cboMauHangLoat;
        private System.Windows.Forms.TextBox txtTieuDeHangLoat;
        private System.Windows.Forms.TextBox txtNoiDungHangLoat;
        private System.Windows.Forms.Label lblToken;
        private System.Windows.Forms.DataGridView dgvNguoiNhan;
        private System.Windows.Forms.Label lblSoNguoiNhan;
        private System.Windows.Forms.Button btnTaiNguoiNhan;
        private System.Windows.Forms.CheckBox chkHenGio;
        private System.Windows.Forms.DateTimePicker dtpHenGio;
        private System.Windows.Forms.ProgressBar progressHangLoat;
        private System.Windows.Forms.Label lblTrangThaiHangLoat;
        private System.Windows.Forms.Button btnGuiHangLoat;
        private System.Windows.Forms.TabPage tabMauEmail;
        private System.Windows.Forms.ListBox lstMauEmail;
        private System.Windows.Forms.TextBox txtTenMau;
        private System.Windows.Forms.TextBox txtTieuDeMau;
        private System.Windows.Forms.TextBox txtNoiDungMau;
        private System.Windows.Forms.CheckBox chkMauHoatDong;
        private System.Windows.Forms.Button btnMauMoi;
        private System.Windows.Forms.Button btnLuuMau;
        private System.Windows.Forms.Button btnKhoaMau;
        private System.Windows.Forms.Button btnTaoMauMacDinh;
        private System.Windows.Forms.TabPage tabNhatKy;
        private System.Windows.Forms.TextBox txtTimNhatKy;
        private System.Windows.Forms.DateTimePicker dtpTuNgayNhatKy;
        private System.Windows.Forms.DateTimePicker dtpDenNgayNhatKy;
        private System.Windows.Forms.ComboBox cboLocLoaiGui;
        private System.Windows.Forms.ComboBox cboLocTrangThaiNhatKy;
        private System.Windows.Forms.ComboBox cboLocMauNhatKy;
        private System.Windows.Forms.Button btnTimNhatKy;
        private System.Windows.Forms.Button btnTaiLaiNhatKy;
        private System.Windows.Forms.Label lblSoNhatKy;
        private System.Windows.Forms.DataGridView dgvNhatKy;
        private System.Windows.Forms.Panel pnlChan;
        private System.Windows.Forms.Label lblLoi;
    }
}

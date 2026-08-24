namespace FINAL_DotNet
{
    partial class FrmBaoHanh
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
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.cboLocHanBaoHanh = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.splitChinh = new System.Windows.Forms.SplitContainer();
            this.dgvPhieuBaoHanh = new System.Windows.Forms.DataGridView();
            this.tabBaoHanh = new System.Windows.Forms.TabControl();
            this.tabTiepNhan = new System.Windows.Forms.TabPage();
            this.pnlYeuCau = new System.Windows.Forms.Panel();
            this.txtNoiDungTiepNhan = new System.Windows.Forms.TextBox();
            this.dtpNgayTraDuKien = new System.Windows.Forms.DateTimePicker();
            this.txtGhiChuTiepNhan = new System.Windows.Forms.TextBox();
            this.pnlSanPhamDaBan = new System.Windows.Forms.Panel();
            this.txtTimSanPhamDaBan = new System.Windows.Forms.TextBox();
            this.btnTimSanPhamDaBan = new System.Windows.Forms.Button();
            this.cboSanPhamDaBan = new System.Windows.Forms.ComboBox();
            this.lblKhachHangTiepNhan = new System.Windows.Forms.Label();
            this.lblHoaDonTiepNhan = new System.Windows.Forms.Label();
            this.lblSanPhamTiepNhan = new System.Windows.Forms.Label();
            this.lblHanBaoHanhTiepNhan = new System.Windows.Forms.Label();
            this.lblSoLanBaoHanh = new System.Windows.Forms.Label();
            this.tabXuLy = new System.Windows.Forms.TabPage();
            this.pnlXuLy = new System.Windows.Forms.Panel();
            this.txtNoiDungXuLy = new System.Windows.Forms.TextBox();
            this.cboTrangThaiXuLy = new System.Windows.Forms.ComboBox();
            this.dtpNgayTraDuKienXuLy = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayTraThucTe = new System.Windows.Forms.DateTimePicker();
            this.txtGhiChuXuLy = new System.Windows.Forms.TextBox();
            this.pnlThongTinXuLy = new System.Windows.Forms.Panel();
            this.lblMaPhieuXuLy = new System.Windows.Forms.Label();
            this.lblKhachHangXuLy = new System.Windows.Forms.Label();
            this.lblSanPhamXuLy = new System.Windows.Forms.Label();
            this.lblNgayTiepNhanXuLy = new System.Windows.Forms.Label();
            this.lblHanBaoHanhXuLy = new System.Windows.Forms.Label();
            this.pnlChan = new System.Windows.Forms.Panel();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.btnTiepNhan = new System.Windows.Forms.Button();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).BeginInit();
            this.splitChinh.Panel1.SuspendLayout();
            this.splitChinh.Panel2.SuspendLayout();
            this.splitChinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuBaoHanh)).BeginInit();
            this.tabBaoHanh.SuspendLayout();
            this.tabTiepNhan.SuspendLayout();
            this.pnlYeuCau.SuspendLayout();
            this.pnlSanPhamDaBan.SuspendLayout();
            this.tabXuLy.SuspendLayout();
            this.pnlXuLy.SuspendLayout();
            this.pnlThongTinXuLy.SuspendLayout();
            this.pnlChan.SuspendLayout();
            this.SuspendLayout();

            this.pnlBoLoc.BackColor = System.Drawing.Color.White;
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBoLoc.Height = 96;
            this.pnlBoLoc.Size = new System.Drawing.Size(1000, 96);
            this.pnlBoLoc.Controls.Add(TaoNhan("Mã PBH / khách / HĐ / sản phẩm", 16, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Từ ngày", 238, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Đến ngày", 378, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Trạng thái", 518, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Hạn bảo hành", 16, 55));
            this.txtTuKhoa.Location = new System.Drawing.Point(16, 27);
            this.txtTuKhoa.Size = new System.Drawing.Size(206, 23);
            this.txtTuKhoa.MaxLength = 150;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            CauHinhNgay(this.dtpTuNgay, 238, 27, 124, false);
            CauHinhNgay(this.dtpDenNgay, 378, 27, 124, false);
            CauHinhCombo(this.cboLocTrangThai, 518, 27, 165);
            this.cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Tiếp nhận", "Đang xử lý", "Hoàn thành", "Đã trả" });
            CauHinhCombo(this.cboLocHanBaoHanh, 16, 73, 190);
            this.cboLocHanBaoHanh.Items.AddRange(new object[] { "Tất cả", "Còn hạn", "Hết hạn", "Không có hạn" });
            CauHinhNut(this.btnTimKiem, "Tìm kiếm", 222, 66, 96, MauXanh());
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            CauHinhNut(this.btnTaiLai, "Tải lại", 326, 66, 82, System.Drawing.Color.DimGray);
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            this.lblSoKetQua.Location = new System.Drawing.Point(500, 64);
            this.lblSoKetQua.Size = new System.Drawing.Size(183, 29);
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pnlBoLoc.Controls.Add(this.txtTuKhoa);
            this.pnlBoLoc.Controls.Add(this.dtpTuNgay);
            this.pnlBoLoc.Controls.Add(this.dtpDenNgay);
            this.pnlBoLoc.Controls.Add(this.cboLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.cboLocHanBaoHanh);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.btnTaiLai);
            this.pnlBoLoc.Controls.Add(this.lblSoKetQua);

            this.splitChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChinh.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitChinh.SplitterDistance = 205;
            this.splitChinh.SplitterWidth = 6;
            this.splitChinh.Panel1.Controls.Add(this.dgvPhieuBaoHanh);
            this.splitChinh.Panel2.Controls.Add(this.tabBaoHanh);

            headerStyle.BackColor = System.Drawing.Color.FromArgb(27, 39, 53);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.SelectionBackColor = headerStyle.BackColor;
            CauHinhLuoi(this.dgvPhieuBaoHanh, headerStyle);
            this.dgvPhieuBaoHanh.Columns.Add(TaoCot("Mã PBH", "MaPhieuBaoHanh", 92));
            this.dgvPhieuBaoHanh.Columns.Add(TaoCot("Ngày tiếp nhận", "NgayTiepNhanHienThi", 130));
            this.dgvPhieuBaoHanh.Columns.Add(TaoCot("Khách hàng", "TenKhachHang", 170));
            this.dgvPhieuBaoHanh.Columns.Add(TaoCot("Hóa đơn", "MaHoaDon", 88));
            this.dgvPhieuBaoHanh.Columns.Add(TaoCot("Sản phẩm", "TenSanPham", 220));
            this.dgvPhieuBaoHanh.Columns.Add(TaoCot("Bảo hành", "ThongTinHanBaoHanh", 95));
            this.dgvPhieuBaoHanh.Columns.Add(TaoCot("Trạng thái", "TrangThaiHienThi", 110));
            this.dgvPhieuBaoHanh.SelectionChanged += new System.EventHandler(this.dgvPhieuBaoHanh_SelectionChanged);

            this.tabBaoHanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBaoHanh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabBaoHanh.Controls.Add(this.tabTiepNhan);
            this.tabBaoHanh.Controls.Add(this.tabXuLy);
            this.tabTiepNhan.Text = "Tiếp nhận bảo hành";
            this.tabTiepNhan.BackColor = System.Drawing.Color.White;
            this.tabTiepNhan.Padding = new System.Windows.Forms.Padding(6);
            this.tabTiepNhan.Controls.Add(this.pnlYeuCau);
            this.tabTiepNhan.Controls.Add(this.pnlSanPhamDaBan);
            this.tabXuLy.Text = "Xử lý phiếu bảo hành";
            this.tabXuLy.BackColor = System.Drawing.Color.White;
            this.tabXuLy.Padding = new System.Windows.Forms.Padding(6);
            this.tabXuLy.Controls.Add(this.pnlXuLy);
            this.tabXuLy.Controls.Add(this.pnlThongTinXuLy);

            this.pnlSanPhamDaBan.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSanPhamDaBan.Height = 96;
            this.pnlSanPhamDaBan.Size = new System.Drawing.Size(960, 96);
            this.pnlSanPhamDaBan.Controls.Add(TaoNhan("Tìm HĐ / SĐT / khách / sản phẩm", 4, 2));
            this.pnlSanPhamDaBan.Controls.Add(TaoNhan("Sản phẩm thuộc hóa đơn đã thanh toán", 292, 2));
            this.txtTimSanPhamDaBan.Location = new System.Drawing.Point(4, 23);
            this.txtTimSanPhamDaBan.Size = new System.Drawing.Size(194, 23);
            this.txtTimSanPhamDaBan.MaxLength = 150;
            this.txtTimSanPhamDaBan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimSanPhamDaBan_KeyDown);
            CauHinhNut(this.btnTimSanPhamDaBan, "Tìm", 206, 20, 70, MauXanh());
            this.btnTimSanPhamDaBan.Click += new System.EventHandler(this.btnTimSanPhamDaBan_Click);
            CauHinhCombo(this.cboSanPhamDaBan, 292, 23, 638);
            this.cboSanPhamDaBan.SelectedIndexChanged += new System.EventHandler(this.cboSanPhamDaBan_SelectedIndexChanged);
            CauHinhGiaTri(this.lblKhachHangTiepNhan, 4, 55, 260);
            CauHinhGiaTri(this.lblHoaDonTiepNhan, 274, 55, 205);
            CauHinhGiaTri(this.lblSanPhamTiepNhan, 489, 55, 285);
            CauHinhGiaTri(this.lblHanBaoHanhTiepNhan, 4, 76, 300);
            CauHinhGiaTri(this.lblSoLanBaoHanh, 314, 76, 220);
            this.pnlSanPhamDaBan.Controls.Add(this.txtTimSanPhamDaBan);
            this.pnlSanPhamDaBan.Controls.Add(this.btnTimSanPhamDaBan);
            this.pnlSanPhamDaBan.Controls.Add(this.cboSanPhamDaBan);
            this.pnlSanPhamDaBan.Controls.Add(this.lblKhachHangTiepNhan);
            this.pnlSanPhamDaBan.Controls.Add(this.lblHoaDonTiepNhan);
            this.pnlSanPhamDaBan.Controls.Add(this.lblSanPhamTiepNhan);
            this.pnlSanPhamDaBan.Controls.Add(this.lblHanBaoHanhTiepNhan);
            this.pnlSanPhamDaBan.Controls.Add(this.lblSoLanBaoHanh);

            this.pnlYeuCau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlYeuCau.Controls.Add(TaoNhan("Nội dung bảo hành *", 4, 4));
            this.pnlYeuCau.Controls.Add(TaoNhan("Ngày trả dự kiến", 504, 4));
            this.pnlYeuCau.Controls.Add(TaoNhan("Ghi chú", 654, 4));
            this.txtNoiDungTiepNhan.Location = new System.Drawing.Point(4, 25);
            this.txtNoiDungTiepNhan.Size = new System.Drawing.Size(484, 76);
            this.txtNoiDungTiepNhan.Multiline = true;
            this.txtNoiDungTiepNhan.MaxLength = 500;
            this.txtNoiDungTiepNhan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            CauHinhNgay(this.dtpNgayTraDuKien, 504, 25, 134, true);
            this.txtGhiChuTiepNhan.Location = new System.Drawing.Point(654, 25);
            this.txtGhiChuTiepNhan.Size = new System.Drawing.Size(276, 76);
            this.txtGhiChuTiepNhan.Multiline = true;
            this.txtGhiChuTiepNhan.MaxLength = 500;
            this.txtGhiChuTiepNhan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pnlYeuCau.Controls.Add(this.txtNoiDungTiepNhan);
            this.pnlYeuCau.Controls.Add(this.dtpNgayTraDuKien);
            this.pnlYeuCau.Controls.Add(this.txtGhiChuTiepNhan);

            this.pnlThongTinXuLy.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTinXuLy.Height = 76;
            this.pnlThongTinXuLy.Size = new System.Drawing.Size(960, 76);
            this.pnlThongTinXuLy.Controls.Add(TaoNhan("Mã phiếu", 4, 2));
            this.pnlThongTinXuLy.Controls.Add(TaoNhan("Khách hàng", 104, 2));
            this.pnlThongTinXuLy.Controls.Add(TaoNhan("Hóa đơn / sản phẩm", 344, 2));
            this.pnlThongTinXuLy.Controls.Add(TaoNhan("Ngày tiếp nhận", 674, 2));
            CauHinhGiaTri(this.lblMaPhieuXuLy, 4, 23, 90);
            CauHinhGiaTri(this.lblKhachHangXuLy, 104, 23, 230);
            CauHinhGiaTri(this.lblSanPhamXuLy, 344, 23, 320);
            CauHinhGiaTri(this.lblNgayTiepNhanXuLy, 674, 23, 165);
            CauHinhGiaTri(this.lblHanBaoHanhXuLy, 4, 50, 350);
            this.pnlThongTinXuLy.Controls.Add(this.lblMaPhieuXuLy);
            this.pnlThongTinXuLy.Controls.Add(this.lblKhachHangXuLy);
            this.pnlThongTinXuLy.Controls.Add(this.lblSanPhamXuLy);
            this.pnlThongTinXuLy.Controls.Add(this.lblNgayTiepNhanXuLy);
            this.pnlThongTinXuLy.Controls.Add(this.lblHanBaoHanhXuLy);

            this.pnlXuLy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlXuLy.Controls.Add(TaoNhan("Nội dung tiếp nhận", 4, 4));
            this.pnlXuLy.Controls.Add(TaoNhan("Trạng thái", 374, 4));
            this.pnlXuLy.Controls.Add(TaoNhan("Ngày trả dự kiến", 514, 4));
            this.pnlXuLy.Controls.Add(TaoNhan("Ngày trả thực tế", 664, 4));
            this.pnlXuLy.Controls.Add(TaoNhan("Ghi chú xử lý", 4, 68));
            this.txtNoiDungXuLy.Location = new System.Drawing.Point(4, 25);
            this.txtNoiDungXuLy.Size = new System.Drawing.Size(354, 38);
            this.txtNoiDungXuLy.Multiline = true;
            this.txtNoiDungXuLy.ReadOnly = true;
            CauHinhCombo(this.cboTrangThaiXuLy, 374, 25, 124);
            this.cboTrangThaiXuLy.SelectedIndexChanged += new System.EventHandler(this.cboTrangThaiXuLy_SelectedIndexChanged);
            CauHinhNgay(this.dtpNgayTraDuKienXuLy, 514, 25, 134, true);
            CauHinhNgayGio(this.dtpNgayTraThucTe, 664, 25, 174);
            this.txtGhiChuXuLy.Location = new System.Drawing.Point(4, 89);
            this.txtGhiChuXuLy.Size = new System.Drawing.Size(834, 43);
            this.txtGhiChuXuLy.Multiline = true;
            this.txtGhiChuXuLy.MaxLength = 500;
            this.txtGhiChuXuLy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pnlXuLy.Controls.Add(this.txtNoiDungXuLy);
            this.pnlXuLy.Controls.Add(this.cboTrangThaiXuLy);
            this.pnlXuLy.Controls.Add(this.dtpNgayTraDuKienXuLy);
            this.pnlXuLy.Controls.Add(this.dtpNgayTraThucTe);
            this.pnlXuLy.Controls.Add(this.txtGhiChuXuLy);

            this.pnlChan.BackColor = System.Drawing.Color.White;
            this.pnlChan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChan.Height = 58;
            this.pnlChan.Size = new System.Drawing.Size(1000, 58);
            this.lblThongBao.ForeColor = System.Drawing.Color.Crimson;
            this.lblThongBao.Location = new System.Drawing.Point(14, 5);
            this.lblThongBao.Size = new System.Drawing.Size(420, 46);
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThongBao.AutoEllipsis = true;
            CauHinhNut(this.btnXemBaoCao, "Xem báo cáo", 440, 12, 116, System.Drawing.Color.FromArgb(44, 95, 138));
            this.btnXemBaoCao.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnXemBaoCao.Enabled = false;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            CauHinhNut(this.btnTiepNhan, "Tiếp nhận", 564, 12, 110, MauXanh());
            this.btnTiepNhan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnTiepNhan.Click += new System.EventHandler(this.btnTiepNhan_Click);
            CauHinhNut(this.btnCapNhat, "Cập nhật xử lý", 680, 12, 132, System.Drawing.Color.FromArgb(196, 148, 52));
            this.btnCapNhat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            CauHinhNut(this.btnLamMoi, "Phiếu mới", 820, 12, 100, System.Drawing.Color.DimGray);
            this.btnLamMoi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            this.pnlChan.Controls.Add(this.lblThongBao);
            this.pnlChan.Controls.Add(this.btnXemBaoCao);
            this.pnlChan.Controls.Add(this.btnTiepNhan);
            this.pnlChan.Controls.Add(this.btnCapNhat);
            this.pnlChan.Controls.Add(this.btnLamMoi);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 248);
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitChinh);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlChan);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmBaoHanh";
            this.Text = "Quản lý bảo hành";
            this.Load += new System.EventHandler(this.FrmBaoHanh_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitChinh.Panel1.ResumeLayout(false);
            this.splitChinh.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).EndInit();
            this.splitChinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuBaoHanh)).EndInit();
            this.tabBaoHanh.ResumeLayout(false);
            this.tabTiepNhan.ResumeLayout(false);
            this.pnlYeuCau.ResumeLayout(false);
            this.pnlYeuCau.PerformLayout();
            this.pnlSanPhamDaBan.ResumeLayout(false);
            this.pnlSanPhamDaBan.PerformLayout();
            this.tabXuLy.ResumeLayout(false);
            this.pnlXuLy.ResumeLayout(false);
            this.pnlXuLy.PerformLayout();
            this.pnlThongTinXuLy.ResumeLayout(false);
            this.pnlThongTinXuLy.PerformLayout();
            this.pnlChan.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private static System.Windows.Forms.Label TaoNhan(string text, int x, int y)
        {
            return new System.Windows.Forms.Label { AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold), Location = new System.Drawing.Point(x, y), Text = text };
        }

        private static void CauHinhNgay(System.Windows.Forms.DateTimePicker control, int x, int y, int width, bool checkMacDinh)
        {
            control.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            control.CustomFormat = "dd/MM/yyyy";
            control.ShowCheckBox = true;
            control.Checked = checkMacDinh;
            control.Location = new System.Drawing.Point(x, y);
            control.Size = new System.Drawing.Size(width, 23);
        }

        private static void CauHinhNgayGio(System.Windows.Forms.DateTimePicker control, int x, int y, int width)
        {
            control.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            control.CustomFormat = "dd/MM/yyyy HH:mm";
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
            label.Size = new System.Drawing.Size(width, 20);
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
            return new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = tieuDe, DataPropertyName = thuocTinh, Width = width, ReadOnly = true };
        }

        private static System.Drawing.Color MauXanh() => System.Drawing.Color.FromArgb(35, 125, 96);

        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.ComboBox cboLocHanBaoHanh;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.SplitContainer splitChinh;
        private System.Windows.Forms.DataGridView dgvPhieuBaoHanh;
        private System.Windows.Forms.TabControl tabBaoHanh;
        private System.Windows.Forms.TabPage tabTiepNhan;
        private System.Windows.Forms.Panel pnlYeuCau;
        private System.Windows.Forms.TextBox txtNoiDungTiepNhan;
        private System.Windows.Forms.DateTimePicker dtpNgayTraDuKien;
        private System.Windows.Forms.TextBox txtGhiChuTiepNhan;
        private System.Windows.Forms.Panel pnlSanPhamDaBan;
        private System.Windows.Forms.TextBox txtTimSanPhamDaBan;
        private System.Windows.Forms.Button btnTimSanPhamDaBan;
        private System.Windows.Forms.ComboBox cboSanPhamDaBan;
        private System.Windows.Forms.Label lblKhachHangTiepNhan;
        private System.Windows.Forms.Label lblHoaDonTiepNhan;
        private System.Windows.Forms.Label lblSanPhamTiepNhan;
        private System.Windows.Forms.Label lblHanBaoHanhTiepNhan;
        private System.Windows.Forms.Label lblSoLanBaoHanh;
        private System.Windows.Forms.TabPage tabXuLy;
        private System.Windows.Forms.Panel pnlXuLy;
        private System.Windows.Forms.TextBox txtNoiDungXuLy;
        private System.Windows.Forms.ComboBox cboTrangThaiXuLy;
        private System.Windows.Forms.DateTimePicker dtpNgayTraDuKienXuLy;
        private System.Windows.Forms.DateTimePicker dtpNgayTraThucTe;
        private System.Windows.Forms.TextBox txtGhiChuXuLy;
        private System.Windows.Forms.Panel pnlThongTinXuLy;
        private System.Windows.Forms.Label lblMaPhieuXuLy;
        private System.Windows.Forms.Label lblKhachHangXuLy;
        private System.Windows.Forms.Label lblSanPhamXuLy;
        private System.Windows.Forms.Label lblNgayTiepNhanXuLy;
        private System.Windows.Forms.Label lblHanBaoHanhXuLy;
        private System.Windows.Forms.Panel pnlChan;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Button btnTiepNhan;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnLamMoi;
    }
}

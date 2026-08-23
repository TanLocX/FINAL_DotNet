namespace FINAL_DotNet
{
    partial class FrmSanPham
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
            this.cboLocDanhMuc = new System.Windows.Forms.ComboBox();
            this.cboLocChatLieu = new System.Windows.Forms.ComboBox();
            this.txtGiaTu = new System.Windows.Forms.TextBox();
            this.txtGiaDen = new System.Windows.Forms.TextBox();
            this.cboLocTonKho = new System.Windows.Forms.ComboBox();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.splitChinh = new System.Windows.Forms.SplitContainer();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.tabBieuMau = new System.Windows.Forms.TabControl();
            this.tabThongTin = new System.Windows.Forms.TabPage();
            this.tableThongTin = new System.Windows.Forms.TableLayoutPanel();
            this.txtMaSanPham = new System.Windows.Forms.TextBox();
            this.txtTenSanPham = new System.Windows.Forms.TextBox();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.numGiaVon = new System.Windows.Forms.NumericUpDown();
            this.numGiaBan = new System.Windows.Forms.NumericUpDown();
            this.numSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.txtDuongDanAnh = new System.Windows.Forms.TextBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.txtMaVach = new System.Windows.Forms.TextBox();
            this.chkDangKinhDoanh = new System.Windows.Forms.CheckBox();
            this.picSanPham = new System.Windows.Forms.PictureBox();
            this.lblChuaCoAnh = new System.Windows.Forms.Label();
            this.tabThanhPhan = new System.Windows.Forms.TabPage();
            this.dgvThanhPhan = new System.Windows.Forms.DataGridView();
            this.pnlNhapThanhPhan = new System.Windows.Forms.Panel();
            this.cboChatLieu = new System.Windows.Forms.ComboBox();
            this.numTrongLuong = new System.Windows.Forms.NumericUpDown();
            this.cboDonViTinh = new System.Windows.Forms.ComboBox();
            this.btnLuuThanhPhan = new System.Windows.Forms.Button();
            this.btnXoaThanhPhan = new System.Windows.Forms.Button();
            this.btnMoiThanhPhan = new System.Windows.Forms.Button();
            this.lblSoThanhPhan = new System.Windows.Forms.Label();
            this.pnlChan = new System.Windows.Forms.Panel();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoaHoacTrangThai = new System.Windows.Forms.Button();
            this.btnLamMoiBieuMau = new System.Windows.Forms.Button();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).BeginInit();
            this.splitChinh.Panel1.SuspendLayout();
            this.splitChinh.Panel2.SuspendLayout();
            this.splitChinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.tabBieuMau.SuspendLayout();
            this.tabThongTin.SuspendLayout();
            this.tableThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaVon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSanPham)).BeginInit();
            this.tabThanhPhan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhPhan)).BeginInit();
            this.pnlNhapThanhPhan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrongLuong)).BeginInit();
            this.pnlChan.SuspendLayout();
            this.SuspendLayout();

            this.pnlBoLoc.BackColor = System.Drawing.Color.White;
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBoLoc.Height = 106;
            this.pnlBoLoc.Size = new System.Drawing.Size(1000, 106);
            this.pnlBoLoc.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlBoLoc.Controls.Add(TaoNhan("Mã / tên sản phẩm", 16, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Danh mục", 232, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Chất liệu", 420, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Giá bán từ", 608, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Giá bán đến", 730, 8));
            this.pnlBoLoc.Controls.Add(TaoNhan("Tồn kho", 16, 58));
            this.pnlBoLoc.Controls.Add(TaoNhan("Trạng thái", 232, 58));
            this.pnlBoLoc.Controls.Add(this.txtTuKhoa);
            this.pnlBoLoc.Controls.Add(this.cboLocDanhMuc);
            this.pnlBoLoc.Controls.Add(this.cboLocChatLieu);
            this.pnlBoLoc.Controls.Add(this.txtGiaTu);
            this.pnlBoLoc.Controls.Add(this.txtGiaDen);
            this.pnlBoLoc.Controls.Add(this.cboLocTonKho);
            this.pnlBoLoc.Controls.Add(this.cboLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.btnTaiLai);
            this.pnlBoLoc.Controls.Add(this.lblSoKetQua);

            this.txtTuKhoa.Location = new System.Drawing.Point(16, 28);
            this.txtTuKhoa.Size = new System.Drawing.Size(200, 23);
            this.txtTuKhoa.MaxLength = 150;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            CauHinhCombo(this.cboLocDanhMuc, 232, 28, 172);
            CauHinhCombo(this.cboLocChatLieu, 420, 28, 172);
            this.txtGiaTu.Location = new System.Drawing.Point(608, 28);
            this.txtGiaTu.Size = new System.Drawing.Size(108, 23);
            this.txtGiaDen.Location = new System.Drawing.Point(730, 28);
            this.txtGiaDen.Size = new System.Drawing.Size(108, 23);
            CauHinhCombo(this.cboLocTonKho, 16, 78, 200);
            this.cboLocTonKho.Items.AddRange(new object[] { "Tất cả", "Còn hàng", "Hết hàng", "Sắp hết (1-5)" });
            CauHinhCombo(this.cboLocTrangThai, 232, 78, 172);
            this.cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang kinh doanh", "Ngừng kinh doanh" });
            CauHinhNut(this.btnTimKiem, "Tìm kiếm", 420, 76, 96, MauXanh());
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            CauHinhNut(this.btnTaiLai, "Tải lại", 526, 76, 84, System.Drawing.Color.DimGray);
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            this.lblSoKetQua.Location = new System.Drawing.Point(625, 77);
            this.lblSoKetQua.Size = new System.Drawing.Size(210, 28);
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.splitChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChinh.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitChinh.SplitterDistance = 210;
            this.splitChinh.SplitterWidth = 6;
            this.splitChinh.Panel1.Controls.Add(this.dgvSanPham);
            this.splitChinh.Panel2.Controls.Add(this.tabBieuMau);

            headerStyle.BackColor = System.Drawing.Color.FromArgb(27, 39, 53);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.SelectionBackColor = headerStyle.BackColor;
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.AllowUserToDeleteRows = false;
            this.dgvSanPham.AllowUserToResizeRows = false;
            this.dgvSanPham.AutoGenerateColumns = false;
            this.dgvSanPham.BackgroundColor = System.Drawing.Color.White;
            this.dgvSanPham.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSanPham.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvSanPham.ColumnHeadersHeight = 34;
            this.dgvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanPham.EnableHeadersVisualStyles = false;
            this.dgvSanPham.MultiSelect = false;
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.RowHeadersVisible = false;
            this.dgvSanPham.RowTemplate.Height = 29;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Columns.Add(TaoCot("Mã SP", "MaSanPham", 82));
            this.dgvSanPham.Columns.Add(TaoCot("Tên sản phẩm", "TenSanPham", 210));
            this.dgvSanPham.Columns.Add(TaoCot("Danh mục", "TenDanhMuc", 125));
            var cotGia = TaoCot("Giá bán", "GiaBan", 105);
            cotGia.DefaultCellStyle.Format = "N0";
            cotGia.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvSanPham.Columns.Add(cotGia);
            this.dgvSanPham.Columns.Add(TaoCot("Tồn", "SoLuongTon", 55));
            this.dgvSanPham.Columns.Add(TaoCot("Chất liệu", "TomTatChatLieu", 180));
            this.dgvSanPham.Columns.Add(TaoCot("Trạng thái", "TrangThai", 125));
            this.dgvSanPham.SelectionChanged += new System.EventHandler(this.dgvSanPham_SelectionChanged);

            this.tabBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBieuMau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabBieuMau.Controls.Add(this.tabThongTin);
            this.tabBieuMau.Controls.Add(this.tabThanhPhan);
            this.tabThongTin.Text = "Thông tin sản phẩm";
            this.tabThongTin.BackColor = System.Drawing.Color.White;
            this.tabThongTin.Padding = new System.Windows.Forms.Padding(8);
            this.tabThongTin.Controls.Add(this.tableThongTin);
            this.tabThanhPhan.Text = "Thành phần chất liệu";
            this.tabThanhPhan.BackColor = System.Drawing.Color.White;
            this.tabThanhPhan.Padding = new System.Windows.Forms.Padding(8);
            this.tabThanhPhan.Controls.Add(this.dgvThanhPhan);
            this.tabThanhPhan.Controls.Add(this.pnlNhapThanhPhan);

            this.tableThongTin.ColumnCount = 6;
            this.tableThongTin.RowCount = 4;
            this.tableThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            for (int i = 0; i < 4; i++) this.tableThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            ThemDongThongTin(0, "Mã sản phẩm", this.txtMaSanPham, "Tên sản phẩm *", this.txtTenSanPham);
            ThemDongThongTin(1, "Danh mục *", this.cboDanhMuc, "Giá vốn", this.numGiaVon);
            ThemDongThongTin(2, "Giá bán", this.numGiaBan, "Số lượng tồn", this.numSoLuongTon);
            ThemDongThongTin(3, "Đường dẫn ảnh", this.txtDuongDanAnh, "Mã vạch", this.txtMaVach);
            this.tableThongTin.Controls.Add(this.picSanPham, 5, 0);
            this.tableThongTin.SetRowSpan(this.picSanPham, 3);
            this.tableThongTin.Controls.Add(this.chkDangKinhDoanh, 5, 3);

            this.txtMaSanPham.ReadOnly = true;
            this.txtMaVach.ReadOnly = true;
            this.txtTenSanPham.MaxLength = 150;
            this.txtDuongDanAnh.MaxLength = 500;
            this.txtDuongDanAnh.Leave += new System.EventHandler(this.txtDuongDanAnh_Leave);
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numGiaVon.DecimalPlaces = 2;
            this.numGiaVon.ThousandsSeparator = true;
            this.numGiaBan.DecimalPlaces = 2;
            this.numGiaBan.ThousandsSeparator = true;
            this.numSoLuongTon.ThousandsSeparator = true;
            foreach (System.Windows.Forms.Control control in new System.Windows.Forms.Control[] { this.txtMaSanPham, this.txtTenSanPham, this.cboDanhMuc, this.numGiaVon, this.numGiaBan, this.numSoLuongTon, this.txtDuongDanAnh, this.txtMaVach })
            {
                control.Dock = System.Windows.Forms.DockStyle.Fill;
                control.Margin = new System.Windows.Forms.Padding(3, 7, 8, 5);
            }
            this.btnChonAnh.Text = "Chọn ảnh...";
            this.btnChonAnh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChonAnh.Width = 78;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            this.txtDuongDanAnh.Controls.Add(this.btnChonAnh);
            this.picSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSanPham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSanPham.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSanPham.Margin = new System.Windows.Forms.Padding(5);
            this.lblChuaCoAnh.Text = "Chưa có ảnh";
            this.lblChuaCoAnh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChuaCoAnh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChuaCoAnh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picSanPham.Controls.Add(this.lblChuaCoAnh);
            this.lblChuaCoAnh.BringToFront();
            this.chkDangKinhDoanh.Text = "Đang kinh doanh";
            this.chkDangKinhDoanh.Enabled = false;
            this.chkDangKinhDoanh.Dock = System.Windows.Forms.DockStyle.Fill;

            this.pnlNhapThanhPhan.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNhapThanhPhan.Height = 72;
            this.pnlNhapThanhPhan.Size = new System.Drawing.Size(960, 72);
            this.pnlNhapThanhPhan.Controls.Add(TaoNhan("Chất liệu *", 4, 2));
            this.pnlNhapThanhPhan.Controls.Add(TaoNhan("Trọng lượng *", 246, 2));
            this.pnlNhapThanhPhan.Controls.Add(TaoNhan("Đơn vị *", 370, 2));
            CauHinhCombo(this.cboChatLieu, 4, 24, 226);
            CauHinhSo(this.numTrongLuong, 246, 24, 108);
            this.numTrongLuong.DecimalPlaces = 3;
            this.numTrongLuong.Increment = 0.001M;
            this.numTrongLuong.Minimum = 0;
            this.cboDonViTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboDonViTinh.Items.AddRange(new object[] { "Gram", "Carat", "Chỉ", "Lượng" });
            this.cboDonViTinh.Location = new System.Drawing.Point(370, 24);
            this.cboDonViTinh.Size = new System.Drawing.Size(96, 23);
            CauHinhNut(this.btnLuuThanhPhan, "Thêm thành phần", 482, 21, 142, MauXanh());
            this.btnLuuThanhPhan.Click += new System.EventHandler(this.btnLuuThanhPhan_Click);
            CauHinhNut(this.btnXoaThanhPhan, "Xóa", 632, 21, 70, System.Drawing.Color.Firebrick);
            this.btnXoaThanhPhan.Click += new System.EventHandler(this.btnXoaThanhPhan_Click);
            CauHinhNut(this.btnMoiThanhPhan, "Nhập mới", 710, 21, 84, System.Drawing.Color.DimGray);
            this.btnMoiThanhPhan.Click += new System.EventHandler(this.btnMoiThanhPhan_Click);
            this.lblSoThanhPhan.Location = new System.Drawing.Point(805, 24);
            this.lblSoThanhPhan.Size = new System.Drawing.Size(120, 25);
            this.lblSoThanhPhan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pnlNhapThanhPhan.Controls.Add(this.cboChatLieu);
            this.pnlNhapThanhPhan.Controls.Add(this.numTrongLuong);
            this.pnlNhapThanhPhan.Controls.Add(this.cboDonViTinh);
            this.pnlNhapThanhPhan.Controls.Add(this.btnLuuThanhPhan);
            this.pnlNhapThanhPhan.Controls.Add(this.btnXoaThanhPhan);
            this.pnlNhapThanhPhan.Controls.Add(this.btnMoiThanhPhan);
            this.pnlNhapThanhPhan.Controls.Add(this.lblSoThanhPhan);

            this.dgvThanhPhan.AllowUserToAddRows = false;
            this.dgvThanhPhan.AllowUserToDeleteRows = false;
            this.dgvThanhPhan.AutoGenerateColumns = false;
            this.dgvThanhPhan.BackgroundColor = System.Drawing.Color.White;
            this.dgvThanhPhan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvThanhPhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThanhPhan.ReadOnly = true;
            this.dgvThanhPhan.RowHeadersVisible = false;
            this.dgvThanhPhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThanhPhan.Columns.Add(TaoCot("Mã chất liệu", "MaChatLieu", 110));
            this.dgvThanhPhan.Columns.Add(TaoCot("Tên chất liệu", "TenChatLieu", 250));
            var cotTrongLuong = TaoCot("Trọng lượng", "TrongLuong", 130);
            cotTrongLuong.DefaultCellStyle.Format = "N3";
            this.dgvThanhPhan.Columns.Add(cotTrongLuong);
            this.dgvThanhPhan.Columns.Add(TaoCot("Đơn vị", "DonViTinh", 100));
            this.dgvThanhPhan.SelectionChanged += new System.EventHandler(this.dgvThanhPhan_SelectionChanged);

            this.pnlChan.BackColor = System.Drawing.Color.White;
            this.pnlChan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChan.Height = 58;
            this.pnlChan.Size = new System.Drawing.Size(1000, 58);
            this.pnlChan.Controls.Add(this.lblThongBao);
            this.pnlChan.Controls.Add(this.btnThem);
            this.pnlChan.Controls.Add(this.btnCapNhat);
            this.pnlChan.Controls.Add(this.btnXoaHoacTrangThai);
            this.pnlChan.Controls.Add(this.btnLamMoiBieuMau);
            this.lblThongBao.ForeColor = System.Drawing.Color.Crimson;
            this.lblThongBao.Location = new System.Drawing.Point(14, 5);
            this.lblThongBao.Size = new System.Drawing.Size(430, 46);
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThongBao.AutoEllipsis = true;
            CauHinhNut(this.btnThem, "Thêm", 458, 12, 90, MauXanh());
            this.btnThem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            CauHinhNut(this.btnCapNhat, "Cập nhật", 556, 12, 98, System.Drawing.Color.FromArgb(196, 148, 52));
            this.btnCapNhat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            CauHinhNut(this.btnXoaHoacTrangThai, "Xóa sản phẩm", 662, 12, 150, System.Drawing.Color.Firebrick);
            this.btnXoaHoacTrangThai.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnXoaHoacTrangThai.Click += new System.EventHandler(this.btnXoaHoacTrangThai_Click);
            CauHinhNut(this.btnLamMoiBieuMau, "Nhập mới", 820, 12, 100, System.Drawing.Color.DimGray);
            this.btnLamMoiBieuMau.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnLamMoiBieuMau.Click += new System.EventHandler(this.btnLamMoiBieuMau_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 248);
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitChinh);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlChan);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmSanPham";
            this.Text = "Quản lý sản phẩm";
            this.Load += new System.EventHandler(this.FrmSanPham_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitChinh.Panel1.ResumeLayout(false);
            this.splitChinh.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChinh)).EndInit();
            this.splitChinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.tabBieuMau.ResumeLayout(false);
            this.tabThongTin.ResumeLayout(false);
            this.tableThongTin.ResumeLayout(false);
            this.tableThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaVon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSanPham)).EndInit();
            this.tabThanhPhan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhPhan)).EndInit();
            this.pnlNhapThanhPhan.ResumeLayout(false);
            this.pnlNhapThanhPhan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrongLuong)).EndInit();
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

        private static void CauHinhCombo(System.Windows.Forms.ComboBox combo, int x, int y, int width)
        {
            combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            combo.Location = new System.Drawing.Point(x, y);
            combo.Size = new System.Drawing.Size(width, 23);
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

        private static System.Drawing.Color MauXanh() => System.Drawing.Color.FromArgb(35, 125, 96);

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

        private void ThemDongThongTin(int dong, string nhan1, System.Windows.Forms.Control control1, string nhan2, System.Windows.Forms.Control control2)
        {
            var label1 = TaoNhan(nhan1, 0, 0);
            var label2 = TaoNhan(nhan2, 0, 0);
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tableThongTin.Controls.Add(label1, 0, dong);
            this.tableThongTin.Controls.Add(control1, 1, dong);
            this.tableThongTin.Controls.Add(label2, 2, dong);
            this.tableThongTin.Controls.Add(control2, 3, dong);
        }

        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.ComboBox cboLocDanhMuc;
        private System.Windows.Forms.ComboBox cboLocChatLieu;
        private System.Windows.Forms.TextBox txtGiaTu;
        private System.Windows.Forms.TextBox txtGiaDen;
        private System.Windows.Forms.ComboBox cboLocTonKho;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.SplitContainer splitChinh;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.TabControl tabBieuMau;
        private System.Windows.Forms.TabPage tabThongTin;
        private System.Windows.Forms.TableLayoutPanel tableThongTin;
        private System.Windows.Forms.TextBox txtMaSanPham;
        private System.Windows.Forms.TextBox txtTenSanPham;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.NumericUpDown numGiaVon;
        private System.Windows.Forms.NumericUpDown numGiaBan;
        private System.Windows.Forms.NumericUpDown numSoLuongTon;
        private System.Windows.Forms.TextBox txtDuongDanAnh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.TextBox txtMaVach;
        private System.Windows.Forms.CheckBox chkDangKinhDoanh;
        private System.Windows.Forms.PictureBox picSanPham;
        private System.Windows.Forms.Label lblChuaCoAnh;
        private System.Windows.Forms.TabPage tabThanhPhan;
        private System.Windows.Forms.DataGridView dgvThanhPhan;
        private System.Windows.Forms.Panel pnlNhapThanhPhan;
        private System.Windows.Forms.ComboBox cboChatLieu;
        private System.Windows.Forms.NumericUpDown numTrongLuong;
        private System.Windows.Forms.ComboBox cboDonViTinh;
        private System.Windows.Forms.Button btnLuuThanhPhan;
        private System.Windows.Forms.Button btnXoaThanhPhan;
        private System.Windows.Forms.Button btnMoiThanhPhan;
        private System.Windows.Forms.Label lblSoThanhPhan;
        private System.Windows.Forms.Panel pnlChan;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoaHoacTrangThai;
        private System.Windows.Forms.Button btnLamMoiBieuMau;
    }
}

namespace FINAL_DotNet
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBoLoc = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSoKetQua = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnTaiLai = new Guna.UI2.WinForms.Guna2Button();
            this.btnTimKiem = new Guna.UI2.WinForms.Guna2Button();
            this.cboLocTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLocTrangThai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTuKhoa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTuKhoa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvDanhMuc = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colMaDanhMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenDanhMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBieuMau = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTieuDeBieuMau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tableBieuMau = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaDanhMuc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMaDanhMuc = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenDanhMuc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTenDanhMuc = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMoTa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblThongBao = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlThaoTac = new Guna.UI2.WinForms.Guna2Panel();
            this.chkDangHoatDong = new Guna.UI2.WinForms.Guna2CheckBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapNhat = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaHoacTrangThai = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoiBieuMau = new Guna.UI2.WinForms.Guna2Button();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).BeginInit();
            this.pnlBieuMau.SuspendLayout();
            this.tableBieuMau.SuspendLayout();
            this.pnlThaoTac.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoLoc
            // 
            this.pnlBoLoc.BackColor = System.Drawing.Color.Transparent;
            this.pnlBoLoc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlBoLoc.BorderRadius = 10;
            this.pnlBoLoc.BorderThickness = 1;
            this.pnlBoLoc.Controls.Add(this.lblSoKetQua);
            this.pnlBoLoc.Controls.Add(this.btnTaiLai);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.cboLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.lblLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.txtTuKhoa);
            this.pnlBoLoc.Controls.Add(this.lblTuKhoa);
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBoLoc.FillColor = System.Drawing.Color.White;
            this.pnlBoLoc.Location = new System.Drawing.Point(0, 0);
            this.pnlBoLoc.Name = "pnlBoLoc";
            this.pnlBoLoc.Padding = new System.Windows.Forms.Padding(12);
            this.pnlBoLoc.Size = new System.Drawing.Size(1000, 64);
            this.pnlBoLoc.TabIndex = 0;
            // 
            // lblSoKetQua
            // 
            this.lblSoKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoKetQua.AutoSize = false;
            this.lblSoKetQua.BackColor = System.Drawing.Color.Transparent;
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSoKetQua.Location = new System.Drawing.Point(825, 20);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Size = new System.Drawing.Size(160, 24);
            this.lblSoKetQua.TabIndex = 6;
            this.lblSoKetQua.Text = "0 danh mục";
            this.lblSoKetQua.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.Animated = true;
            this.btnTaiLai.BorderRadius = 6;
            this.btnTaiLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaiLai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnTaiLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTaiLai.ForeColor = System.Drawing.Color.White;
            this.btnTaiLai.Location = new System.Drawing.Point(623, 16);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(85, 32);
            this.btnTaiLai.TabIndex = 5;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Animated = true;
            this.btnTimKiem.BorderRadius = 6;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(125)))), ((int)(((byte)(96)))));
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(520, 16);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(95, 32);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.BorderRadius = 6;
            this.cboLocTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLocTrangThai.Items.AddRange(new object[] {
            "Tất cả",
            "Đang hoạt động",
            "Ngừng hoạt động"});
            this.cboLocTrangThai.Location = new System.Drawing.Point(340, 18);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(165, 28);
            this.cboLocTrangThai.TabIndex = 3;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.lblLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLocTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblLocTrangThai.Location = new System.Drawing.Point(338, 24);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(61, 15);
            this.lblLocTrangThai.TabIndex = 2;
            this.lblLocTrangThai.Text = "Trạng thái";
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.BorderRadius = 6;
            this.txtTuKhoa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTuKhoa.Location = new System.Drawing.Point(82, 18);
            this.txtTuKhoa.MaxLength = 100;
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.PlaceholderText = "Tìm tên danh mục, mô tả...";
            this.txtTuKhoa.Size = new System.Drawing.Size(240, 28);
            this.txtTuKhoa.TabIndex = 1;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.BackColor = System.Drawing.Color.Transparent;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTuKhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTuKhoa.Location = new System.Drawing.Point(18, 24);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(50, 15);
            this.lblTuKhoa.TabIndex = 0;
            this.lblTuKhoa.Text = "Từ khóa";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 64);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvDanhMuc);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 4);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlBieuMau);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.splitContainer.Size = new System.Drawing.Size(1000, 586);
            this.splitContainer.SplitterDistance = 330;
            this.splitContainer.TabIndex = 1;
            // 
            // dgvDanhMuc
            // 
            this.dgvDanhMuc.AllowUserToAddRows = false;
            this.dgvDanhMuc.AllowUserToDeleteRows = false;
            this.dgvDanhMuc.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvDanhMuc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhMuc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhMuc.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhMuc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDanhMuc.ColumnHeadersHeight = 34;
            this.dgvDanhMuc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaDanhMuc,
            this.colTenDanhMuc,
            this.colMoTa,
            this.colSoSanPham,
            this.colTrangThai});
            this.dgvDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhMuc.Location = new System.Drawing.Point(0, 6);
            this.dgvDanhMuc.MultiSelect = false;
            this.dgvDanhMuc.Name = "dgvDanhMuc";
            this.dgvDanhMuc.ReadOnly = true;
            this.dgvDanhMuc.RowHeadersVisible = false;
            this.dgvDanhMuc.RowTemplate.Height = 29;
            this.dgvDanhMuc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhMuc.Size = new System.Drawing.Size(1000, 320);
            this.dgvDanhMuc.TabIndex = 0;
            this.dgvDanhMuc.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            this.dgvDanhMuc.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dgvDanhMuc.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvDanhMuc.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDanhMuc.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.dgvDanhMuc.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dgvDanhMuc.SelectionChanged += new System.EventHandler(this.dgvDanhMuc_SelectionChanged);
            // 
            // colMaDanhMuc
            // 
            this.colMaDanhMuc.DataPropertyName = "MaDanhMuc";
            this.colMaDanhMuc.HeaderText = "Mã DM";
            this.colMaDanhMuc.Name = "colMaDanhMuc";
            this.colMaDanhMuc.ReadOnly = true;
            this.colMaDanhMuc.Width = 90;
            // 
            // colTenDanhMuc
            // 
            this.colTenDanhMuc.DataPropertyName = "TenDanhMuc";
            this.colTenDanhMuc.HeaderText = "Tên danh mục";
            this.colTenDanhMuc.Name = "colTenDanhMuc";
            this.colTenDanhMuc.ReadOnly = true;
            this.colTenDanhMuc.Width = 200;
            // 
            // colMoTa
            // 
            this.colMoTa.DataPropertyName = "MoTa";
            this.colMoTa.HeaderText = "Mô tả";
            this.colMoTa.Name = "colMoTa";
            this.colMoTa.ReadOnly = true;
            this.colMoTa.Width = 350;
            // 
            // colSoSanPham
            // 
            this.colSoSanPham.DataPropertyName = "SoSanPham";
            this.colSoSanPham.HeaderText = "Số sản phẩm";
            this.colSoSanPham.Name = "colSoSanPham";
            this.colSoSanPham.ReadOnly = true;
            this.colSoSanPham.Width = 110;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // pnlBieuMau
            // 
            this.pnlBieuMau.BackColor = System.Drawing.Color.Transparent;
            this.pnlBieuMau.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlBieuMau.BorderRadius = 10;
            this.pnlBieuMau.BorderThickness = 1;
            this.pnlBieuMau.Controls.Add(this.tableBieuMau);
            this.pnlBieuMau.Controls.Add(this.lblThongBao);
            this.pnlBieuMau.Controls.Add(this.pnlThaoTac);
            this.pnlBieuMau.Controls.Add(this.lblTieuDeBieuMau);
            this.pnlBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBieuMau.FillColor = System.Drawing.Color.White;
            this.pnlBieuMau.Location = new System.Drawing.Point(0, 4);
            this.pnlBieuMau.Name = "pnlBieuMau";
            this.pnlBieuMau.Padding = new System.Windows.Forms.Padding(12);
            this.pnlBieuMau.Size = new System.Drawing.Size(1000, 248);
            this.pnlBieuMau.TabIndex = 0;
            // 
            // lblTieuDeBieuMau
            // 
            this.lblTieuDeBieuMau.AutoSize = true;
            this.lblTieuDeBieuMau.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDeBieuMau.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeBieuMau.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBieuMau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.lblTieuDeBieuMau.Location = new System.Drawing.Point(12, 12);
            this.lblTieuDeBieuMau.Name = "lblTieuDeBieuMau";
            this.lblTieuDeBieuMau.Padding = new System.Windows.Forms.Padding(4, 0, 0, 6);
            this.lblTieuDeBieuMau.Size = new System.Drawing.Size(183, 26);
            this.lblTieuDeBieuMau.TabIndex = 0;
            this.lblTieuDeBieuMau.Text = "THÔNG TIN DANH MỤC";
            // 
            // tableBieuMau
            // 
            this.tableBieuMau.ColumnCount = 4;
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tableBieuMau.Controls.Add(this.lblMaDanhMuc, 0, 0);
            this.tableBieuMau.Controls.Add(this.txtMaDanhMuc, 1, 0);
            this.tableBieuMau.Controls.Add(this.lblTenDanhMuc, 2, 0);
            this.tableBieuMau.Controls.Add(this.txtTenDanhMuc, 3, 0);
            this.tableBieuMau.Controls.Add(this.lblMoTa, 0, 1);
            this.tableBieuMau.Controls.Add(this.txtMoTa, 1, 1);
            this.tableBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBieuMau.Location = new System.Drawing.Point(12, 38);
            this.tableBieuMau.Name = "tableBieuMau";
            this.tableBieuMau.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.tableBieuMau.RowCount = 2;
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableBieuMau.Size = new System.Drawing.Size(976, 114);
            this.tableBieuMau.TabIndex = 0;
            // 
            // lblMaDanhMuc
            // 
            this.lblMaDanhMuc.AutoSize = true;
            this.lblMaDanhMuc.BackColor = System.Drawing.Color.Transparent;
            this.lblMaDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaDanhMuc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblMaDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMaDanhMuc.Location = new System.Drawing.Point(9, 2);
            this.lblMaDanhMuc.Name = "lblMaDanhMuc";
            this.lblMaDanhMuc.Size = new System.Drawing.Size(109, 38);
            this.lblMaDanhMuc.TabIndex = 0;
            this.lblMaDanhMuc.Text = "Mã danh mục";
            this.lblMaDanhMuc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaDanhMuc
            // 
            this.txtMaDanhMuc.BorderRadius = 6;
            this.txtMaDanhMuc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaDanhMuc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtMaDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaDanhMuc.Location = new System.Drawing.Point(124, 5);
            this.txtMaDanhMuc.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.txtMaDanhMuc.Name = "txtMaDanhMuc";
            this.txtMaDanhMuc.ReadOnly = true;
            this.txtMaDanhMuc.Size = new System.Drawing.Size(292, 32);
            this.txtMaDanhMuc.TabIndex = 1;
            // 
            // lblTenDanhMuc
            // 
            this.lblTenDanhMuc.AutoSize = true;
            this.lblTenDanhMuc.BackColor = System.Drawing.Color.Transparent;
            this.lblTenDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenDanhMuc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTenDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTenDanhMuc.Location = new System.Drawing.Point(431, 2);
            this.lblTenDanhMuc.Name = "lblTenDanhMuc";
            this.lblTenDanhMuc.Size = new System.Drawing.Size(119, 38);
            this.lblTenDanhMuc.TabIndex = 2;
            this.lblTenDanhMuc.Text = "Tên danh mục (*)";
            this.lblTenDanhMuc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTenDanhMuc
            // 
            this.txtTenDanhMuc.BorderRadius = 6;
            this.txtTenDanhMuc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDanhMuc.Location = new System.Drawing.Point(556, 5);
            this.txtTenDanhMuc.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.txtTenDanhMuc.MaxLength = 100;
            this.txtTenDanhMuc.Name = "txtTenDanhMuc";
            this.txtTenDanhMuc.PlaceholderText = "Nhập tên danh mục...";
            this.txtTenDanhMuc.Size = new System.Drawing.Size(411, 32);
            this.txtTenDanhMuc.TabIndex = 3;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.BackColor = System.Drawing.Color.Transparent;
            this.lblMoTa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMoTa.Location = new System.Drawing.Point(9, 40);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(109, 72);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mô tả";
            this.lblMoTa.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMoTa
            // 
            this.tableBieuMau.SetColumnSpan(this.txtMoTa, 3);
            this.txtMoTa.BorderRadius = 6;
            this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMoTa.Location = new System.Drawing.Point(124, 43);
            this.txtMoTa.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.txtMoTa.MaxLength = 255;
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PlaceholderText = "Mô tả nhóm danh mục sản phẩm...";
            this.txtMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size = new System.Drawing.Size(843, 66);
            this.txtMoTa.TabIndex = 5;
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = false;
            this.lblThongBao.BackColor = System.Drawing.Color.Transparent;
            this.lblThongBao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThongBao.ForeColor = System.Drawing.Color.Crimson;
            this.lblThongBao.Location = new System.Drawing.Point(12, 152);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblThongBao.Size = new System.Drawing.Size(976, 28);
            this.lblThongBao.TabIndex = 1;
            this.lblThongBao.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlThaoTac
            // 
            this.pnlThaoTac.Controls.Add(this.chkDangHoatDong);
            this.pnlThaoTac.Controls.Add(this.btnThem);
            this.pnlThaoTac.Controls.Add(this.btnCapNhat);
            this.pnlThaoTac.Controls.Add(this.btnXoaHoacTrangThai);
            this.pnlThaoTac.Controls.Add(this.btnLamMoiBieuMau);
            this.pnlThaoTac.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThaoTac.Location = new System.Drawing.Point(12, 180);
            this.pnlThaoTac.Name = "pnlThaoTac";
            this.pnlThaoTac.Size = new System.Drawing.Size(976, 56);
            this.pnlThaoTac.TabIndex = 2;
            // 
            // chkDangHoatDong
            // 
            this.chkDangHoatDong.AutoSize = true;
            this.chkDangHoatDong.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.chkDangHoatDong.CheckedState.BorderRadius = 2;
            this.chkDangHoatDong.CheckedState.BorderThickness = 1;
            this.chkDangHoatDong.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.chkDangHoatDong.Enabled = false;
            this.chkDangHoatDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkDangHoatDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.chkDangHoatDong.Location = new System.Drawing.Point(8, 18);
            this.chkDangHoatDong.Name = "chkDangHoatDong";
            this.chkDangHoatDong.Size = new System.Drawing.Size(115, 19);
            this.chkDangHoatDong.TabIndex = 0;
            this.chkDangHoatDong.Text = "Đang hoạt động";
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.Animated = true;
            this.btnThem.BorderRadius = 6;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(125)))), ((int)(((byte)(96)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(468, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(108, 36);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhat.Animated = true;
            this.btnCapNhat.BorderRadius = 6;
            this.btnCapNhat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapNhat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(582, 10);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(108, 36);
            this.btnCapNhat.TabIndex = 2;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnXoaHoacTrangThai
            // 
            this.btnXoaHoacTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaHoacTrangThai.Animated = true;
            this.btnXoaHoacTrangThai.BorderRadius = 6;
            this.btnXoaHoacTrangThai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaHoacTrangThai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnXoaHoacTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaHoacTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnXoaHoacTrangThai.Location = new System.Drawing.Point(696, 10);
            this.btnXoaHoacTrangThai.Name = "btnXoaHoacTrangThai";
            this.btnXoaHoacTrangThai.Size = new System.Drawing.Size(150, 36);
            this.btnXoaHoacTrangThai.TabIndex = 3;
            this.btnXoaHoacTrangThai.Text = "Xóa danh mục";
            this.btnXoaHoacTrangThai.Click += new System.EventHandler(this.btnXoaHoacTrangThai_Click);
            // 
            // btnLamMoiBieuMau
            // 
            this.btnLamMoiBieuMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiBieuMau.Animated = true;
            this.btnLamMoiBieuMau.BorderRadius = 6;
            this.btnLamMoiBieuMau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoiBieuMau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnLamMoiBieuMau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoiBieuMau.ForeColor = System.Drawing.Color.White;
            this.btnLamMoiBieuMau.Location = new System.Drawing.Point(852, 10);
            this.btnLamMoiBieuMau.Name = "btnLamMoiBieuMau";
            this.btnLamMoiBieuMau.Size = new System.Drawing.Size(116, 36);
            this.btnLamMoiBieuMau.TabIndex = 4;
            this.btnLamMoiBieuMau.Text = "Làm mới";
            this.btnLamMoiBieuMau.Click += new System.EventHandler(this.btnLamMoiBieuMau_Click);
            // 
            // FrmDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBoLoc);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmDanhMuc";
            this.Text = "Quản lý danh mục sản phẩm";
            this.Load += new System.EventHandler(this.FrmDanhMuc_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMuc)).EndInit();
            this.pnlBieuMau.ResumeLayout(false);
            this.pnlBieuMau.PerformLayout();
            this.tableBieuMau.ResumeLayout(false);
            this.tableBieuMau.PerformLayout();
            this.pnlThaoTac.ResumeLayout(false);
            this.pnlThaoTac.PerformLayout();
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2Panel pnlBoLoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTuKhoa;
        private Guna.UI2.WinForms.Guna2TextBox txtTuKhoa;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLocTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox cboLocTrangThai;
        private Guna.UI2.WinForms.Guna2Button btnTimKiem;
        private Guna.UI2.WinForms.Guna2Button btnTaiLai;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoKetQua;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private Guna.UI2.WinForms.Guna2Panel pnlBieuMau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTieuDeBieuMau;
        private System.Windows.Forms.TableLayoutPanel tableBieuMau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMaDanhMuc;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDanhMuc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTenDanhMuc;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDanhMuc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMoTa;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThongBao;
        private Guna.UI2.WinForms.Guna2Panel pnlThaoTac;
        private Guna.UI2.WinForms.Guna2CheckBox chkDangHoatDong;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnCapNhat;
        private Guna.UI2.WinForms.Guna2Button btnXoaHoacTrangThai;
        private Guna.UI2.WinForms.Guna2Button btnLamMoiBieuMau;
    }
}

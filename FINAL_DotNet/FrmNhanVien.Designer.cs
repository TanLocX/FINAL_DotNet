namespace FINAL_DotNet
{
    partial class FrmNhanVien
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBoLoc = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSoKetQua = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnTaiLai = new Guna.UI2.WinForms.Guna2Button();
            this.btnTimKiem = new Guna.UI2.WinForms.Guna2Button();
            this.cboLocTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLocTrangThai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTuKhoa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTuKhoa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvNhanVien = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colMaNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChucVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBieuMau = new Guna.UI2.WinForms.Guna2Panel();
            this.tableBieuMau = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaNhanVien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMaNhanVien = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSoDienThoai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHoTen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblGioiTinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboGioiTinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblChucVu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtChucVu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNgaySinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblDiaChi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblThongBao = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlThaoTac = new Guna.UI2.WinForms.Guna2Panel();
            this.chkDangLamViec = new Guna.UI2.WinForms.Guna2CheckBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapNhat = new Guna.UI2.WinForms.Guna2Button();
            this.btnDoiTrangThai = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoiBieuMau = new Guna.UI2.WinForms.Guna2Button();
            this.lblTieuDeBieuMau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
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
            this.lblSoKetQua.Location = new System.Drawing.Point(855, 20);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Size = new System.Drawing.Size(130, 24);
            this.lblSoKetQua.TabIndex = 6;
            this.lblSoKetQua.Text = "0 nhân viên";
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
            this.btnTaiLai.Location = new System.Drawing.Point(746, 16);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(86, 32);
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
            this.btnTimKiem.Location = new System.Drawing.Point(650, 16);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 32);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.cboLocTrangThai.BorderRadius = 6;
            this.cboLocTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.FocusedColor = System.Drawing.Color.Empty;
            this.cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLocTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLocTrangThai.ItemHeight = 30;
            this.cboLocTrangThai.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Đang làm việc",
            "Đã nghỉ"});
            this.cboLocTrangThai.Location = new System.Drawing.Point(478, 18);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(154, 36);
            this.cboLocTrangThai.TabIndex = 3;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.lblLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLocTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblLocTrangThai.Location = new System.Drawing.Point(405, 24);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(56, 15);
            this.lblLocTrangThai.TabIndex = 2;
            this.lblLocTrangThai.Text = "Trạng thái";
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.BorderRadius = 6;
            this.txtTuKhoa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTuKhoa.DefaultText = "";
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTuKhoa.Location = new System.Drawing.Point(92, 18);
            this.txtTuKhoa.MaxLength = 150;
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.PlaceholderText = "Nhập mã, tên, SĐT, email...";
            this.txtTuKhoa.SelectedText = "";
            this.txtTuKhoa.Size = new System.Drawing.Size(295, 28);
            this.txtTuKhoa.TabIndex = 1;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.BackColor = System.Drawing.Color.Transparent;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTuKhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTuKhoa.Location = new System.Drawing.Point(17, 24);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(45, 15);
            this.lblTuKhoa.TabIndex = 0;
            this.lblTuKhoa.Text = "Từ khóa";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 64);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvNhanVien);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 4);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlBieuMau);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.splitContainer.Size = new System.Drawing.Size(1000, 586);
            this.splitContainer.SplitterDistance = 285;
            this.splitContainer.TabIndex = 1;
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvNhanVien.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNhanVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNhanVien.ColumnHeadersHeight = 34;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvNhanVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaNhanVien,
            this.colHoTen,
            this.colGioiTinh,
            this.colNgaySinh,
            this.colSoDienThoai,
            this.colEmail,
            this.colChucVu,
            this.colTrangThai});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNhanVien.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvNhanVien.Location = new System.Drawing.Point(0, 6);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.RowTemplate.Height = 29;
            this.dgvNhanVien.Size = new System.Drawing.Size(1000, 275);
            this.dgvNhanVien.TabIndex = 0;
            this.dgvNhanVien.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvNhanVien.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dgvNhanVien.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvNhanVien.ThemeStyle.HeaderStyle.Height = 34;
            this.dgvNhanVien.ThemeStyle.ReadOnly = true;
            this.dgvNhanVien.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvNhanVien.ThemeStyle.RowsStyle.Height = 29;
            this.dgvNhanVien.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.dgvNhanVien.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dgvNhanVien.SelectionChanged += new System.EventHandler(this.dgvNhanVien_SelectionChanged);
            // 
            // colMaNhanVien
            // 
            this.colMaNhanVien.DataPropertyName = "MaNhanVien";
            this.colMaNhanVien.HeaderText = "Mã NV";
            this.colMaNhanVien.Name = "colMaNhanVien";
            this.colMaNhanVien.ReadOnly = true;
            // 
            // colHoTen
            // 
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ tên";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.DataPropertyName = "GioiTinh";
            this.colGioiTinh.HeaderText = "Giới tính";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.ReadOnly = true;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.DataPropertyName = "NgaySinhHienThi";
            this.colNgaySinh.HeaderText = "Ngày sinh";
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.ReadOnly = true;
            // 
            // colSoDienThoai
            // 
            this.colSoDienThoai.DataPropertyName = "SoDienThoai";
            this.colSoDienThoai.HeaderText = "Số điện thoại";
            this.colSoDienThoai.Name = "colSoDienThoai";
            this.colSoDienThoai.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colChucVu
            // 
            this.colChucVu.DataPropertyName = "ChucVu";
            this.colChucVu.HeaderText = "Chức vụ";
            this.colChucVu.Name = "colChucVu";
            this.colChucVu.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 105;
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
            this.pnlBieuMau.Size = new System.Drawing.Size(1000, 293);
            this.pnlBieuMau.TabIndex = 0;
            // 
            // tableBieuMau
            // 
            this.tableBieuMau.ColumnCount = 4;
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.Controls.Add(this.lblMaNhanVien, 0, 0);
            this.tableBieuMau.Controls.Add(this.txtMaNhanVien, 1, 0);
            this.tableBieuMau.Controls.Add(this.lblSoDienThoai, 2, 0);
            this.tableBieuMau.Controls.Add(this.txtSoDienThoai, 3, 0);
            this.tableBieuMau.Controls.Add(this.lblHoTen, 0, 1);
            this.tableBieuMau.Controls.Add(this.txtHoTen, 1, 1);
            this.tableBieuMau.Controls.Add(this.lblEmail, 2, 1);
            this.tableBieuMau.Controls.Add(this.txtEmail, 3, 1);
            this.tableBieuMau.Controls.Add(this.lblGioiTinh, 0, 2);
            this.tableBieuMau.Controls.Add(this.cboGioiTinh, 1, 2);
            this.tableBieuMau.Controls.Add(this.lblChucVu, 2, 2);
            this.tableBieuMau.Controls.Add(this.txtChucVu, 3, 2);
            this.tableBieuMau.Controls.Add(this.lblNgaySinh, 0, 3);
            this.tableBieuMau.Controls.Add(this.dtpNgaySinh, 1, 3);
            this.tableBieuMau.Controls.Add(this.lblDiaChi, 2, 3);
            this.tableBieuMau.Controls.Add(this.txtDiaChi, 3, 3);
            this.tableBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBieuMau.Location = new System.Drawing.Point(12, 40);
            this.tableBieuMau.Name = "tableBieuMau";
            this.tableBieuMau.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.tableBieuMau.RowCount = 4;
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.Size = new System.Drawing.Size(976, 153);
            this.tableBieuMau.TabIndex = 1;
            // 
            // lblMaNhanVien
            // 
            this.lblMaNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.lblMaNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaNhanVien.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblMaNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMaNhanVien.Location = new System.Drawing.Point(9, 5);
            this.lblMaNhanVien.Name = "lblMaNhanVien";
            this.lblMaNhanVien.Size = new System.Drawing.Size(104, 31);
            this.lblMaNhanVien.TabIndex = 0;
            this.lblMaNhanVien.Text = "Mã nhân viên";
            this.lblMaNhanVien.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaNhanVien
            // 
            this.txtMaNhanVien.BorderRadius = 6;
            this.txtMaNhanVien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaNhanVien.DefaultText = "";
            this.txtMaNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaNhanVien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtMaNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaNhanVien.Location = new System.Drawing.Point(119, 5);
            this.txtMaNhanVien.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.PlaceholderText = "";
            this.txtMaNhanVien.ReadOnly = true;
            this.txtMaNhanVien.SelectedText = "";
            this.txtMaNhanVien.Size = new System.Drawing.Size(357, 31);
            this.txtMaNhanVien.TabIndex = 1;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.lblSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSoDienThoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSoDienThoai.Location = new System.Drawing.Point(491, 5);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(104, 31);
            this.lblSoDienThoai.TabIndex = 2;
            this.lblSoDienThoai.Text = "Số điện thoại";
            this.lblSoDienThoai.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BorderRadius = 6;
            this.txtSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoDienThoai.DefaultText = "";
            this.txtSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(601, 5);
            this.txtSoDienThoai.MaxLength = 15;
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "09xx xxx xxx";
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(366, 31);
            this.txtSoDienThoai.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.BackColor = System.Drawing.Color.Transparent;
            this.lblHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblHoTen.Location = new System.Drawing.Point(9, 42);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(104, 31);
            this.lblHoTen.TabIndex = 4;
            this.lblHoTen.Text = "Họ tên (*)";
            this.lblHoTen.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderRadius = 6;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHoTen.Location = new System.Drawing.Point(119, 42);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.txtHoTen.MaxLength = 150;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "Nhập họ tên nhân viên...";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(357, 31);
            this.txtHoTen.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEmail.Location = new System.Drawing.Point(491, 42);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(104, 31);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderRadius = 6;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(601, 42);
            this.txtEmail.MaxLength = 254;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "email@example.com";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(366, 31);
            this.txtEmail.TabIndex = 7;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.BackColor = System.Drawing.Color.Transparent;
            this.lblGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblGioiTinh.Location = new System.Drawing.Point(9, 79);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(104, 31);
            this.lblGioiTinh.TabIndex = 8;
            this.lblGioiTinh.Text = "Giới tính";
            this.lblGioiTinh.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.BackColor = System.Drawing.Color.Transparent;
            this.cboGioiTinh.BorderRadius = 6;
            this.cboGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGioiTinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.FocusedColor = System.Drawing.Color.Empty;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboGioiTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboGioiTinh.ItemHeight = 30;
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Không chọn",
            "Nam",
            "Nữ",
            "Khác"});
            this.cboGioiTinh.Location = new System.Drawing.Point(119, 79);
            this.cboGioiTinh.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(357, 36);
            this.cboGioiTinh.TabIndex = 9;
            // 
            // lblChucVu
            // 
            this.lblChucVu.BackColor = System.Drawing.Color.Transparent;
            this.lblChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChucVu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblChucVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblChucVu.Location = new System.Drawing.Point(491, 79);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Size = new System.Drawing.Size(104, 31);
            this.lblChucVu.TabIndex = 10;
            this.lblChucVu.Text = "Chức vụ (*)";
            this.lblChucVu.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtChucVu
            // 
            this.txtChucVu.BorderRadius = 6;
            this.txtChucVu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtChucVu.DefaultText = "";
            this.txtChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChucVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtChucVu.Location = new System.Drawing.Point(601, 79);
            this.txtChucVu.MaxLength = 50;
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.PlaceholderText = "Nhân viên bán hàng / Quản lý...";
            this.txtChucVu.SelectedText = "";
            this.txtChucVu.Size = new System.Drawing.Size(366, 31);
            this.txtChucVu.TabIndex = 11;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.BackColor = System.Drawing.Color.Transparent;
            this.lblNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblNgaySinh.Location = new System.Drawing.Point(9, 116);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(104, 32);
            this.lblNgaySinh.TabIndex = 12;
            this.lblNgaySinh.Text = "Ngày sinh";
            this.lblNgaySinh.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.BorderRadius = 6;
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgaySinh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgaySinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new System.Drawing.Point(119, 116);
            this.dtpNgaySinh.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.dtpNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.ShowCheckBox = true;
            this.dtpNgaySinh.Size = new System.Drawing.Size(357, 32);
            this.dtpNgaySinh.TabIndex = 13;
            this.dtpNgaySinh.Value = new System.DateTime(2026, 8, 29, 22, 4, 53, 197);
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.BackColor = System.Drawing.Color.Transparent;
            this.lblDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDiaChi.Location = new System.Drawing.Point(491, 116);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(104, 32);
            this.lblDiaChi.TabIndex = 14;
            this.lblDiaChi.Text = "Địa chỉ";
            this.lblDiaChi.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderRadius = 6;
            this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChi.DefaultText = "";
            this.txtDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiaChi.Location = new System.Drawing.Point(601, 116);
            this.txtDiaChi.MaxLength = 255;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "Địa chỉ cư trú...";
            this.txtDiaChi.SelectedText = "";
            this.txtDiaChi.Size = new System.Drawing.Size(366, 32);
            this.txtDiaChi.TabIndex = 15;
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = false;
            this.lblThongBao.BackColor = System.Drawing.Color.Transparent;
            this.lblThongBao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThongBao.ForeColor = System.Drawing.Color.Crimson;
            this.lblThongBao.Location = new System.Drawing.Point(12, 193);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblThongBao.Size = new System.Drawing.Size(976, 28);
            this.lblThongBao.TabIndex = 2;
            this.lblThongBao.Text = null;
            this.lblThongBao.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlThaoTac
            // 
            this.pnlThaoTac.Controls.Add(this.chkDangLamViec);
            this.pnlThaoTac.Controls.Add(this.btnThem);
            this.pnlThaoTac.Controls.Add(this.btnCapNhat);
            this.pnlThaoTac.Controls.Add(this.btnDoiTrangThai);
            this.pnlThaoTac.Controls.Add(this.btnLamMoiBieuMau);
            this.pnlThaoTac.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThaoTac.Location = new System.Drawing.Point(12, 221);
            this.pnlThaoTac.Name = "pnlThaoTac";
            this.pnlThaoTac.Size = new System.Drawing.Size(976, 60);
            this.pnlThaoTac.TabIndex = 3;
            // 
            // chkDangLamViec
            // 
            this.chkDangLamViec.AutoSize = true;
            this.chkDangLamViec.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.chkDangLamViec.CheckedState.BorderRadius = 2;
            this.chkDangLamViec.CheckedState.BorderThickness = 1;
            this.chkDangLamViec.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.chkDangLamViec.Enabled = false;
            this.chkDangLamViec.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkDangLamViec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.chkDangLamViec.Location = new System.Drawing.Point(8, 20);
            this.chkDangLamViec.Name = "chkDangLamViec";
            this.chkDangLamViec.Size = new System.Drawing.Size(104, 19);
            this.chkDangLamViec.TabIndex = 0;
            this.chkDangLamViec.Text = "Đang làm việc";
            this.chkDangLamViec.UncheckedState.BorderRadius = 0;
            this.chkDangLamViec.UncheckedState.BorderThickness = 0;
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
            this.btnThem.Location = new System.Drawing.Point(490, 12);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(110, 36);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm NV";
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
            this.btnCapNhat.Location = new System.Drawing.Point(608, 12);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(110, 36);
            this.btnCapNhat.TabIndex = 2;
            this.btnCapNhat.Text = "Lưu sửa";
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnDoiTrangThai
            // 
            this.btnDoiTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoiTrangThai.Animated = true;
            this.btnDoiTrangThai.BorderRadius = 6;
            this.btnDoiTrangThai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoiTrangThai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnDoiTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDoiTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnDoiTrangThai.Location = new System.Drawing.Point(726, 12);
            this.btnDoiTrangThai.Name = "btnDoiTrangThai";
            this.btnDoiTrangThai.Size = new System.Drawing.Size(130, 36);
            this.btnDoiTrangThai.TabIndex = 3;
            this.btnDoiTrangThai.Text = "Đổi trạng thái";
            this.btnDoiTrangThai.Click += new System.EventHandler(this.btnDoiTrangThai_Click);
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
            this.btnLamMoiBieuMau.Location = new System.Drawing.Point(864, 12);
            this.btnLamMoiBieuMau.Name = "btnLamMoiBieuMau";
            this.btnLamMoiBieuMau.Size = new System.Drawing.Size(104, 36);
            this.btnLamMoiBieuMau.TabIndex = 4;
            this.btnLamMoiBieuMau.Text = "Làm mới";
            this.btnLamMoiBieuMau.Click += new System.EventHandler(this.btnLamMoiBieuMau_Click);
            // 
            // lblTieuDeBieuMau
            // 
            this.lblTieuDeBieuMau.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDeBieuMau.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeBieuMau.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBieuMau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.lblTieuDeBieuMau.Location = new System.Drawing.Point(12, 12);
            this.lblTieuDeBieuMau.Name = "lblTieuDeBieuMau";
            this.lblTieuDeBieuMau.Padding = new System.Windows.Forms.Padding(4, 0, 0, 6);
            this.lblTieuDeBieuMau.Size = new System.Drawing.Size(976, 28);
            this.lblTieuDeBieuMau.TabIndex = 0;
            this.lblTieuDeBieuMau.Text = "THÔNG TIN NHÂN VIÊN";
            // 
            // FrmNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBoLoc);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmNhanVien";
            this.Text = "Quản lý nhân viên";
            this.Load += new System.EventHandler(this.FrmNhanVien_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
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
        private Guna.UI2.WinForms.Guna2DataGridView dgvNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChucVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private Guna.UI2.WinForms.Guna2Panel pnlBieuMau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTieuDeBieuMau;
        private System.Windows.Forms.TableLayoutPanel tableBieuMau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMaNhanVien;
        private Guna.UI2.WinForms.Guna2TextBox txtMaNhanVien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoDienThoai;
        private Guna.UI2.WinForms.Guna2TextBox txtSoDienThoai;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGioiTinh;
        private Guna.UI2.WinForms.Guna2ComboBox cboGioiTinh;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblChucVu;
        private Guna.UI2.WinForms.Guna2TextBox txtChucVu;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgaySinh;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgaySinh;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThongBao;
        private Guna.UI2.WinForms.Guna2Panel pnlThaoTac;
        private Guna.UI2.WinForms.Guna2CheckBox chkDangLamViec;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnCapNhat;
        private Guna.UI2.WinForms.Guna2Button btnDoiTrangThai;
        private Guna.UI2.WinForms.Guna2Button btnLamMoiBieuMau;
    }
}

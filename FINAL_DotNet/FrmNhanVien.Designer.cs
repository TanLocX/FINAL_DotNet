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
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle alternatingStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.lblLocTrangThai = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.colMaNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChucVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBieuMau = new System.Windows.Forms.Panel();
            this.tableBieuMau = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaNhanVien = new System.Windows.Forms.Label();
            this.txtMaNhanVien = new System.Windows.Forms.TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new FINAL_DotNet.DarkGoldDateTimePicker();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.pnlThaoTac = new System.Windows.Forms.Panel();
            this.chkDangLamViec = new System.Windows.Forms.CheckBox();
            this.btnLamMoiBieuMau = new System.Windows.Forms.Button();
            this.btnDoiTrangThai = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTieuDeBieuMau = new System.Windows.Forms.Label();
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
            this.pnlBoLoc.BackColor = System.Drawing.Color.White;
            this.pnlBoLoc.Controls.Add(this.lblSoKetQua);
            this.pnlBoLoc.Controls.Add(this.btnTaiLai);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.cboLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.lblLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.txtTuKhoa);
            this.pnlBoLoc.Controls.Add(this.lblTuKhoa);
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBoLoc.Location = new System.Drawing.Point(0, 0);
            this.pnlBoLoc.Name = "pnlBoLoc";
            this.pnlBoLoc.Size = new System.Drawing.Size(1000, 64);
            this.pnlBoLoc.TabIndex = 0;
            // 
            // lblSoKetQua
            // 
            this.lblSoKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(109)))), ((int)(((byte)(121)))));
            this.lblSoKetQua.Location = new System.Drawing.Point(865, 24);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Size = new System.Drawing.Size(118, 20);
            this.lblSoKetQua.TabIndex = 6;
            this.lblSoKetQua.Text = "0 nhân viên";
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.BackColor = System.Drawing.Color.White;
            this.btnTaiLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaiLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(80)))), ((int)(((byte)(94)))));
            this.btnTaiLai.Location = new System.Drawing.Point(746, 18);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(86, 32);
            this.btnTaiLai.TabIndex = 5;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(650, 18);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 32);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLocTrangThai.FormattingEnabled = true;
            this.cboLocTrangThai.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Đang làm việc",
            "Đã nghỉ"});
            this.cboLocTrangThai.Location = new System.Drawing.Point(478, 23);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(154, 23);
            this.cboLocTrangThai.TabIndex = 3;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocTrangThai.Location = new System.Drawing.Point(405, 27);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(67, 15);
            this.lblLocTrangThai.TabIndex = 2;
            this.lblLocTrangThai.Text = "Trạng thái";
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTuKhoa.Location = new System.Drawing.Point(92, 23);
            this.txtTuKhoa.MaxLength = 150;
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(295, 23);
            this.txtTuKhoa.TabIndex = 1;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuKhoa.Location = new System.Drawing.Point(17, 27);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(69, 15);
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
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 4);
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
            alternatingStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.dgvNhanVien.AlternatingRowsDefaultCellStyle = alternatingStyle;
            this.dgvNhanVien.AutoGenerateColumns = false;
            this.dgvNhanVien.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhanVien.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            headerStyle.SelectionForeColor = System.Drawing.Color.White;
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNhanVien.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvNhanVien.ColumnHeadersHeight = 36;
            this.dgvNhanVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaNhanVien,
            this.colHoTen,
            this.colGioiTinh,
            this.colNgaySinh,
            this.colSoDienThoai,
            this.colEmail,
            this.colChucVu,
            this.colTrangThai});
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.EnableHeadersVisualStyles = false;
            this.dgvNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvNhanVien.Location = new System.Drawing.Point(0, 8);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.RowTemplate.Height = 32;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(1000, 273);
            this.dgvNhanVien.TabIndex = 0;
            this.dgvNhanVien.SelectionChanged += new System.EventHandler(this.dgvNhanVien_SelectionChanged);
            // 
            // columns
            // 
            this.colMaNhanVien.DataPropertyName = "MaNhanVien";
            this.colMaNhanVien.HeaderText = "Mã NV";
            this.colMaNhanVien.Name = "colMaNhanVien";
            this.colMaNhanVien.ReadOnly = true;
            this.colMaNhanVien.Width = 80;
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ tên";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            this.colHoTen.Width = 150;
            this.colGioiTinh.DataPropertyName = "GioiTinh";
            this.colGioiTinh.HeaderText = "Giới tính";
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.ReadOnly = true;
            this.colGioiTinh.Width = 65;
            this.colNgaySinh.DataPropertyName = "NgaySinhHienThi";
            this.colNgaySinh.HeaderText = "Ngày sinh";
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.ReadOnly = true;
            this.colNgaySinh.Width = 85;
            this.colSoDienThoai.DataPropertyName = "SoDienThoai";
            this.colSoDienThoai.HeaderText = "Số điện thoại";
            this.colSoDienThoai.Name = "colSoDienThoai";
            this.colSoDienThoai.ReadOnly = true;
            this.colSoDienThoai.Width = 105;
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            this.colEmail.Width = 175;
            this.colChucVu.DataPropertyName = "ChucVu";
            this.colChucVu.HeaderText = "Chức vụ";
            this.colChucVu.Name = "colChucVu";
            this.colChucVu.ReadOnly = true;
            this.colChucVu.Width = 140;
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 105;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // pnlBieuMau
            // 
            this.pnlBieuMau.BackColor = System.Drawing.Color.White;
            this.pnlBieuMau.Controls.Add(this.tableBieuMau);
            this.pnlBieuMau.Controls.Add(this.lblThongBao);
            this.pnlBieuMau.Controls.Add(this.pnlThaoTac);
            this.pnlBieuMau.Controls.Add(this.lblTieuDeBieuMau);
            this.pnlBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBieuMau.Location = new System.Drawing.Point(0, 4);
            this.pnlBieuMau.Name = "pnlBieuMau";
            this.pnlBieuMau.Size = new System.Drawing.Size(1000, 293);
            this.pnlBieuMau.TabIndex = 0;
            // 
            // tableBieuMau
            // 
            this.tableBieuMau.ColumnCount = 4;
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
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
            this.tableBieuMau.Location = new System.Drawing.Point(0, 36);
            this.tableBieuMau.Name = "tableBieuMau";
            this.tableBieuMau.Padding = new System.Windows.Forms.Padding(14, 4, 14, 2);
            this.tableBieuMau.RowCount = 4;
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.Size = new System.Drawing.Size(1000, 169);
            this.tableBieuMau.TabIndex = 1;
            // 
            // form labels and inputs
            // 
            this.lblMaNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaNhanVien.Text = "Mã nhân viên";
            this.lblMaNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.txtMaNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaNhanVien.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtMaNhanVien.ReadOnly = true;
            this.lblSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoDienThoai.Text = "Số điện thoại";
            this.lblSoDienThoai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.txtSoDienThoai.MaxLength = 15;
            this.lblHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.Text = "Họ tên (*)";
            this.lblHoTen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtHoTen.MaxLength = 150;
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.txtEmail.MaxLength = 254;
            this.lblGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.Text = "Giới tính";
            this.lblGioiTinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboGioiTinh.Items.AddRange(new object[] { "Không chọn", "Nam", "Nữ", "Khác" });
            this.cboGioiTinh.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.lblChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChucVu.Text = "Chức vụ (*)";
            this.lblChucVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChucVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtChucVu.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.txtChucVu.MaxLength = 50;
            this.lblNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.Text = "Ngày sinh";
            this.lblNgaySinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.dtpNgaySinh.ShowCheckBox = true;
            this.lblDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.Text = "Địa chỉ";
            this.lblDiaChi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 4);
            this.txtDiaChi.MaxLength = 255;
            this.txtDiaChi.Multiline = true;
            // 
            // lblThongBao
            // 
            this.lblThongBao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThongBao.ForeColor = System.Drawing.Color.Firebrick;
            this.lblThongBao.Location = new System.Drawing.Point(0, 205);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.lblThongBao.Size = new System.Drawing.Size(1000, 28);
            this.lblThongBao.TabIndex = 2;
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlThaoTac
            // 
            this.pnlThaoTac.Controls.Add(this.chkDangLamViec);
            this.pnlThaoTac.Controls.Add(this.btnLamMoiBieuMau);
            this.pnlThaoTac.Controls.Add(this.btnDoiTrangThai);
            this.pnlThaoTac.Controls.Add(this.btnCapNhat);
            this.pnlThaoTac.Controls.Add(this.btnThem);
            this.pnlThaoTac.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThaoTac.Location = new System.Drawing.Point(0, 233);
            this.pnlThaoTac.Name = "pnlThaoTac";
            this.pnlThaoTac.Size = new System.Drawing.Size(1000, 60);
            this.pnlThaoTac.TabIndex = 3;
            // 
            // chkDangLamViec
            // 
            this.chkDangLamViec.AutoSize = true;
            this.chkDangLamViec.Enabled = false;
            this.chkDangLamViec.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkDangLamViec.Location = new System.Drawing.Point(19, 21);
            this.chkDangLamViec.Name = "chkDangLamViec";
            this.chkDangLamViec.Size = new System.Drawing.Size(103, 19);
            this.chkDangLamViec.TabIndex = 0;
            this.chkDangLamViec.Text = "Đang làm việc";
            this.chkDangLamViec.UseVisualStyleBackColor = true;
            // 
            // action buttons
            // 
            this.btnLamMoiBieuMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiBieuMau.BackColor = System.Drawing.Color.White;
            this.btnLamMoiBieuMau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiBieuMau.Location = new System.Drawing.Point(874, 13);
            this.btnLamMoiBieuMau.Name = "btnLamMoiBieuMau";
            this.btnLamMoiBieuMau.Size = new System.Drawing.Size(108, 34);
            this.btnLamMoiBieuMau.Text = "Làm mới";
            this.btnLamMoiBieuMau.UseVisualStyleBackColor = false;
            this.btnLamMoiBieuMau.Click += new System.EventHandler(this.btnLamMoiBieuMau_Click);
            this.btnDoiTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoiTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(94)))), ((int)(((byte)(66)))));
            this.btnDoiTrangThai.FlatAppearance.BorderSize = 0;
            this.btnDoiTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnDoiTrangThai.Location = new System.Drawing.Point(740, 13);
            this.btnDoiTrangThai.Name = "btnDoiTrangThai";
            this.btnDoiTrangThai.Size = new System.Drawing.Size(128, 34);
            this.btnDoiTrangThai.Text = "Ngừng làm việc";
            this.btnDoiTrangThai.UseVisualStyleBackColor = false;
            this.btnDoiTrangThai.Click += new System.EventHandler(this.btnDoiTrangThai_Click);
            this.btnCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(111)))), ((int)(((byte)(155)))));
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(626, 13);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(108, 34);
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(512, 13);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(108, 34);
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblTieuDeBieuMau
            // 
            this.lblTieuDeBieuMau.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeBieuMau.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBieuMau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(58)))));
            this.lblTieuDeBieuMau.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDeBieuMau.Name = "lblTieuDeBieuMau";
            this.lblTieuDeBieuMau.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.lblTieuDeBieuMau.Size = new System.Drawing.Size(1000, 36);
            this.lblTieuDeBieuMau.TabIndex = 0;
            this.lblTieuDeBieuMau.Text = "Thông tin nhân viên";
            this.lblTieuDeBieuMau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBoLoc);
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
            this.tableBieuMau.ResumeLayout(false);
            this.tableBieuMau.PerformLayout();
            this.pnlThaoTac.ResumeLayout(false);
            this.pnlThaoTac.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.Label lblLocTrangThai;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChucVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.Panel pnlBieuMau;
        private System.Windows.Forms.TableLayoutPanel tableBieuMau;
        private System.Windows.Forms.Label lblMaNhanVien;
        private System.Windows.Forms.TextBox txtMaNhanVien;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblChucVu;
        private System.Windows.Forms.TextBox txtChucVu;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Panel pnlThaoTac;
        private System.Windows.Forms.CheckBox chkDangLamViec;
        private System.Windows.Forms.Button btnLamMoiBieuMau;
        private System.Windows.Forms.Button btnDoiTrangThai;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTieuDeBieuMau;
    }
}

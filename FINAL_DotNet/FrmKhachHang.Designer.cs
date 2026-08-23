namespace FINAL_DotNet
{
    partial class FrmKhachHang
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
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            this.colMaKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiemTichLuy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNhanEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBieuMau = new System.Windows.Forms.Panel();
            this.tableBieuMau = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaKhachHang = new System.Windows.Forms.Label();
            this.txtMaKhachHang = new System.Windows.Forms.TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiemTichLuy = new System.Windows.Forms.Label();
            this.txtDiemTichLuy = new System.Windows.Forms.TextBox();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.pnlThaoTac = new System.Windows.Forms.Panel();
            this.chkChoPhepNhanEmail = new System.Windows.Forms.CheckBox();
            this.chkDangHoatDong = new System.Windows.Forms.CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
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
            this.pnlBoLoc.Size = new System.Drawing.Size(1050, 72);
            this.pnlBoLoc.TabIndex = 0;
            //
            // filter controls
            //
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuKhoa.Location = new System.Drawing.Point(18, 10);
            this.lblTuKhoa.Text = "Tìm kiếm";
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTuKhoa.Location = new System.Drawing.Point(20, 31);
            this.txtTuKhoa.MaxLength = 150;
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(320, 23);
            this.txtTuKhoa.TabIndex = 0;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocTrangThai.Location = new System.Drawing.Point(358, 10);
            this.lblLocTrangThai.Text = "Trạng thái";
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLocTrangThai.Items.AddRange(new object[] {
                "Tất cả", "Đang hoạt động", "Ngừng hoạt động", "Đồng ý nhận email"});
            this.cboLocTrangThai.Location = new System.Drawing.Point(360, 31);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(180, 23);
            this.cboLocTrangThai.TabIndex = 1;
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(137, 100, 28);
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(554, 29);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 28);
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            this.btnTaiLai.BackColor = System.Drawing.Color.White;
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Location = new System.Drawing.Point(662, 29);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(88, 28);
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            this.lblSoKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.ForeColor = System.Drawing.Color.FromArgb(91, 101, 113);
            this.lblSoKetQua.Location = new System.Drawing.Point(866, 29);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Size = new System.Drawing.Size(164, 28);
            this.lblSoKetQua.Text = "0 khách hàng";
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 72);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.Panel1.Controls.Add(this.dgvKhachHang);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(12, 10, 12, 4);
            this.splitContainer.Panel2.Controls.Add(this.pnlBieuMau);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(12, 4, 12, 10);
            this.splitContainer.Size = new System.Drawing.Size(1050, 628);
            this.splitContainer.SplitterDistance = 330;
            this.splitContainer.SplitterWidth = 4;
            this.splitContainer.TabIndex = 1;
            //
            // dgvKhachHang
            //
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AllowUserToResizeRows = false;
            alternatingStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 251);
            this.dgvKhachHang.AlternatingRowsDefaultCellStyle = alternatingStyle;
            this.dgvKhachHang.AutoGenerateColumns = false;
            this.dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(34, 45, 58);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(34, 45, 58);
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvKhachHang.ColumnHeadersHeight = 38;
            this.dgvKhachHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colMaKhachHang, this.colHoTen, this.colSoDienThoai, this.colEmail,
                this.colNgaySinh, this.colDiemTichLuy, this.colNhanEmail, this.colTrangThai});
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.EnableHeadersVisualStyles = false;
            this.dgvKhachHang.MultiSelect = false;
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.RowHeadersVisible = false;
            this.dgvKhachHang.RowTemplate.Height = 34;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.SelectionChanged += new System.EventHandler(this.dgvKhachHang_SelectionChanged);
            this.colMaKhachHang.DataPropertyName = "MaKhachHang";
            this.colMaKhachHang.HeaderText = "Mã KH";
            this.colMaKhachHang.Name = "colMaKhachHang";
            this.colMaKhachHang.ReadOnly = true;
            this.colMaKhachHang.Width = 85;
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ tên";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            this.colHoTen.Width = 155;
            this.colSoDienThoai.DataPropertyName = "SoDienThoai";
            this.colSoDienThoai.HeaderText = "Số điện thoại";
            this.colSoDienThoai.Name = "colSoDienThoai";
            this.colSoDienThoai.ReadOnly = true;
            this.colSoDienThoai.Width = 115;
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            this.colEmail.Width = 165;
            this.colNgaySinh.DataPropertyName = "NgaySinhHienThi";
            this.colNgaySinh.HeaderText = "Ngày sinh";
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.ReadOnly = true;
            this.colNgaySinh.Width = 90;
            this.colDiemTichLuy.DataPropertyName = "DiemTichLuy";
            this.colDiemTichLuy.HeaderText = "Điểm";
            this.colDiemTichLuy.Name = "colDiemTichLuy";
            this.colDiemTichLuy.ReadOnly = true;
            this.colDiemTichLuy.Width = 70;
            this.colNhanEmail.DataPropertyName = "NhanEmail";
            this.colNhanEmail.HeaderText = "Nhận email";
            this.colNhanEmail.Name = "colNhanEmail";
            this.colNhanEmail.ReadOnly = true;
            this.colNhanEmail.Width = 90;
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 115;
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
            this.pnlBieuMau.Name = "pnlBieuMau";
            //
            // tableBieuMau
            //
            this.tableBieuMau.ColumnCount = 4;
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.Controls.Add(this.lblMaKhachHang, 0, 0);
            this.tableBieuMau.Controls.Add(this.txtMaKhachHang, 1, 0);
            this.tableBieuMau.Controls.Add(this.lblSoDienThoai, 2, 0);
            this.tableBieuMau.Controls.Add(this.txtSoDienThoai, 3, 0);
            this.tableBieuMau.Controls.Add(this.lblHoTen, 0, 1);
            this.tableBieuMau.Controls.Add(this.txtHoTen, 1, 1);
            this.tableBieuMau.Controls.Add(this.lblEmail, 2, 1);
            this.tableBieuMau.Controls.Add(this.txtEmail, 3, 1);
            this.tableBieuMau.Controls.Add(this.lblNgaySinh, 0, 2);
            this.tableBieuMau.Controls.Add(this.dtpNgaySinh, 1, 2);
            this.tableBieuMau.Controls.Add(this.lblDiaChi, 2, 2);
            this.tableBieuMau.Controls.Add(this.txtDiaChi, 3, 2);
            this.tableBieuMau.Controls.Add(this.lblDiemTichLuy, 0, 3);
            this.tableBieuMau.Controls.Add(this.txtDiemTichLuy, 1, 3);
            this.tableBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBieuMau.Location = new System.Drawing.Point(0, 36);
            this.tableBieuMau.Name = "tableBieuMau";
            this.tableBieuMau.Padding = new System.Windows.Forms.Padding(14, 4, 14, 2);
            this.tableBieuMau.RowCount = 4;
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            //
            // form fields
            //
            this.lblMaKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaKhachHang.Text = "Mã khách hàng";
            this.lblMaKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaKhachHang.BackColor = System.Drawing.Color.FromArgb(245, 246, 248);
            this.txtMaKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaKhachHang.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtMaKhachHang.ReadOnly = true;
            this.lblSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoDienThoai.Text = "Số điện thoại (*)";
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
            this.lblNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.Text = "Ngày sinh";
            this.lblNgaySinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
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
            this.lblDiemTichLuy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiemTichLuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiemTichLuy.Text = "Điểm tích lũy";
            this.lblDiemTichLuy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDiemTichLuy.BackColor = System.Drawing.Color.FromArgb(245, 246, 248);
            this.txtDiemTichLuy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiemTichLuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemTichLuy.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtDiemTichLuy.ReadOnly = true;
            //
            // lblThongBao
            //
            this.lblThongBao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThongBao.ForeColor = System.Drawing.Color.Firebrick;
            this.lblThongBao.Location = new System.Drawing.Point(0, 205);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.lblThongBao.Size = new System.Drawing.Size(1026, 28);
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlThaoTac
            //
            this.pnlThaoTac.Controls.Add(this.chkChoPhepNhanEmail);
            this.pnlThaoTac.Controls.Add(this.chkDangHoatDong);
            this.pnlThaoTac.Controls.Add(this.btnLamMoiBieuMau);
            this.pnlThaoTac.Controls.Add(this.btnDoiTrangThai);
            this.pnlThaoTac.Controls.Add(this.btnCapNhat);
            this.pnlThaoTac.Controls.Add(this.btnThem);
            this.pnlThaoTac.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThaoTac.Location = new System.Drawing.Point(0, 233);
            this.pnlThaoTac.Name = "pnlThaoTac";
            this.pnlThaoTac.Size = new System.Drawing.Size(1026, 60);
            this.chkDangHoatDong.AutoSize = true;
            this.chkDangHoatDong.Enabled = false;
            this.chkDangHoatDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkDangHoatDong.Location = new System.Drawing.Point(18, 12);
            this.chkDangHoatDong.Text = "Đang hoạt động";
            this.chkChoPhepNhanEmail.AutoSize = true;
            this.chkChoPhepNhanEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkChoPhepNhanEmail.Location = new System.Drawing.Point(18, 35);
            this.chkChoPhepNhanEmail.Text = "Đồng ý nhận email";
            //
            // action buttons
            //
            this.btnLamMoiBieuMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiBieuMau.BackColor = System.Drawing.Color.White;
            this.btnLamMoiBieuMau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiBieuMau.Location = new System.Drawing.Point(902, 13);
            this.btnLamMoiBieuMau.Name = "btnLamMoiBieuMau";
            this.btnLamMoiBieuMau.Size = new System.Drawing.Size(108, 34);
            this.btnLamMoiBieuMau.Text = "Làm mới";
            this.btnLamMoiBieuMau.UseVisualStyleBackColor = false;
            this.btnLamMoiBieuMau.Click += new System.EventHandler(this.btnLamMoiBieuMau_Click);
            this.btnDoiTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoiTrangThai.BackColor = System.Drawing.Color.FromArgb(186, 94, 66);
            this.btnDoiTrangThai.FlatAppearance.BorderSize = 0;
            this.btnDoiTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnDoiTrangThai.Location = new System.Drawing.Point(750, 13);
            this.btnDoiTrangThai.Name = "btnDoiTrangThai";
            this.btnDoiTrangThai.Size = new System.Drawing.Size(146, 34);
            this.btnDoiTrangThai.Text = "Ngừng hoạt động";
            this.btnDoiTrangThai.UseVisualStyleBackColor = false;
            this.btnDoiTrangThai.Click += new System.EventHandler(this.btnDoiTrangThai_Click);
            this.btnCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(63, 111, 155);
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(636, 13);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(108, 34);
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(137, 100, 28);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(522, 13);
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
            this.lblTieuDeBieuMau.ForeColor = System.Drawing.Color.FromArgb(34, 45, 58);
            this.lblTieuDeBieuMau.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDeBieuMau.Name = "lblTieuDeBieuMau";
            this.lblTieuDeBieuMau.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.lblTieuDeBieuMau.Size = new System.Drawing.Size(1026, 36);
            this.lblTieuDeBieuMau.Text = "Thông tin khách hàng";
            this.lblTieuDeBieuMau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // FrmKhachHang
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 248);
            this.ClientSize = new System.Drawing.Size(1050, 700);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBoLoc);
            this.Name = "FrmKhachHang";
            this.Text = "Quản lý khách hàng";
            this.Load += new System.EventHandler(this.FrmKhachHang_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiemTichLuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNhanEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.Panel pnlBieuMau;
        private System.Windows.Forms.TableLayoutPanel tableBieuMau;
        private System.Windows.Forms.Label lblMaKhachHang;
        private System.Windows.Forms.TextBox txtMaKhachHang;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiemTichLuy;
        private System.Windows.Forms.TextBox txtDiemTichLuy;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Panel pnlThaoTac;
        private System.Windows.Forms.CheckBox chkChoPhepNhanEmail;
        private System.Windows.Forms.CheckBox chkDangHoatDong;
        private System.Windows.Forms.Button btnLamMoiBieuMau;
        private System.Windows.Forms.Button btnDoiTrangThai;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTieuDeBieuMau;
    }
}

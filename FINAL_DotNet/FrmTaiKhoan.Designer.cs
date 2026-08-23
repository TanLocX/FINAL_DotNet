namespace FINAL_DotNet
{
    partial class FrmTaiKhoan
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
            this.dgvTaiKhoan = new System.Windows.Forms.DataGridView();
            this.colMaTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenDangNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChucVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoiMatKhau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThaiNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBieuMau = new System.Windows.Forms.Panel();
            this.tableBieuMau = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaTaiKhoan = new System.Windows.Forms.Label();
            this.txtMaTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.cboVaiTro = new System.Windows.Forms.ComboBox();
            this.lblMatKhauTam = new System.Windows.Forms.Label();
            this.txtMatKhauTam = new System.Windows.Forms.TextBox();
            this.lblXacNhanMatKhau = new System.Windows.Forms.Label();
            this.txtXacNhanMatKhau = new System.Windows.Forms.TextBox();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.pnlThaoTac = new System.Windows.Forms.Panel();
            this.chkPhaiDoiMatKhau = new System.Windows.Forms.CheckBox();
            this.chkDangHoatDong = new System.Windows.Forms.CheckBox();
            this.btnLamMoiBieuMau = new System.Windows.Forms.Button();
            this.btnDatLaiMatKhau = new System.Windows.Forms.Button();
            this.btnDoiTrangThai = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnCapTaiKhoan = new System.Windows.Forms.Button();
            this.lblTieuDeBieuMau = new System.Windows.Forms.Label();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).BeginInit();
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
            this.pnlBoLoc.Size = new System.Drawing.Size(1100, 72);
            this.pnlBoLoc.TabIndex = 0;
            //
            // labels and filters
            //
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuKhoa.Location = new System.Drawing.Point(18, 10);
            this.lblTuKhoa.Text = "Tìm kiếm";
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTuKhoa.Location = new System.Drawing.Point(20, 31);
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
            this.cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang hoạt động", "Đã khóa", "Bắt buộc đổi mật khẩu" });
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
            this.lblSoKetQua.Location = new System.Drawing.Point(916, 29);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Size = new System.Drawing.Size(164, 28);
            this.lblSoKetQua.Text = "0 tài khoản";
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 72);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.Panel1.Controls.Add(this.dgvTaiKhoan);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(12, 10, 12, 4);
            this.splitContainer.Panel2.Controls.Add(this.pnlBieuMau);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(12, 4, 12, 10);
            this.splitContainer.Size = new System.Drawing.Size(1100, 628);
            this.splitContainer.SplitterDistance = 330;
            this.splitContainer.SplitterWidth = 4;
            this.splitContainer.TabIndex = 1;
            //
            // dgvTaiKhoan
            //
            this.dgvTaiKhoan.AllowUserToAddRows = false;
            this.dgvTaiKhoan.AllowUserToDeleteRows = false;
            this.dgvTaiKhoan.AllowUserToResizeRows = false;
            alternatingStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 251);
            this.dgvTaiKhoan.AlternatingRowsDefaultCellStyle = alternatingStyle;
            this.dgvTaiKhoan.AutoGenerateColumns = false;
            this.dgvTaiKhoan.BackgroundColor = System.Drawing.Color.White;
            this.dgvTaiKhoan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTaiKhoan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(34, 45, 58);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(34, 45, 58);
            this.dgvTaiKhoan.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvTaiKhoan.ColumnHeadersHeight = 38;
            this.dgvTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colMaTaiKhoan, this.colTenDangNhap, this.colMaNhanVien, this.colHoTen,
                this.colChucVu, this.colVaiTro, this.colTrangThai, this.colDoiMatKhau,
                this.colTrangThaiNhanVien});
            this.dgvTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaiKhoan.EnableHeadersVisualStyles = false;
            this.dgvTaiKhoan.MultiSelect = false;
            this.dgvTaiKhoan.Name = "dgvTaiKhoan";
            this.dgvTaiKhoan.ReadOnly = true;
            this.dgvTaiKhoan.RowHeadersVisible = false;
            this.dgvTaiKhoan.RowTemplate.Height = 34;
            this.dgvTaiKhoan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaiKhoan.SelectionChanged += new System.EventHandler(this.dgvTaiKhoan_SelectionChanged);
            this.colMaTaiKhoan.DataPropertyName = "MaTaiKhoan";
            this.colMaTaiKhoan.HeaderText = "Mã TK";
            this.colMaTaiKhoan.Name = "colMaTaiKhoan";
            this.colMaTaiKhoan.ReadOnly = true;
            this.colMaTaiKhoan.Width = 85;
            this.colTenDangNhap.DataPropertyName = "TenDangNhap";
            this.colTenDangNhap.HeaderText = "Tên đăng nhập";
            this.colTenDangNhap.Name = "colTenDangNhap";
            this.colTenDangNhap.ReadOnly = true;
            this.colTenDangNhap.Width = 125;
            this.colMaNhanVien.DataPropertyName = "MaNhanVien";
            this.colMaNhanVien.HeaderText = "Mã NV";
            this.colMaNhanVien.Name = "colMaNhanVien";
            this.colMaNhanVien.ReadOnly = true;
            this.colMaNhanVien.Width = 85;
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Nhân viên";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            this.colHoTen.Width = 165;
            this.colChucVu.DataPropertyName = "ChucVu";
            this.colChucVu.HeaderText = "Chức vụ";
            this.colChucVu.Name = "colChucVu";
            this.colChucVu.ReadOnly = true;
            this.colChucVu.Width = 120;
            this.colVaiTro.DataPropertyName = "VaiTro";
            this.colVaiTro.HeaderText = "Vai trò";
            this.colVaiTro.Name = "colVaiTro";
            this.colVaiTro.ReadOnly = true;
            this.colVaiTro.Width = 90;
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.HeaderText = "Tài khoản";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            this.colTrangThai.Width = 115;
            this.colDoiMatKhau.DataPropertyName = "DoiMatKhau";
            this.colDoiMatKhau.HeaderText = "Mật khẩu";
            this.colDoiMatKhau.Name = "colDoiMatKhau";
            this.colDoiMatKhau.ReadOnly = true;
            this.colDoiMatKhau.Width = 110;
            this.colTrangThaiNhanVien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThaiNhanVien.DataPropertyName = "TrangThaiNhanVien";
            this.colTrangThaiNhanVien.HeaderText = "Nhân sự";
            this.colTrangThaiNhanVien.MinimumWidth = 100;
            this.colTrangThaiNhanVien.Name = "colTrangThaiNhanVien";
            this.colTrangThaiNhanVien.ReadOnly = true;
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
            this.pnlBieuMau.TabIndex = 0;
            //
            // tableBieuMau
            //
            this.tableBieuMau.ColumnCount = 4;
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.Controls.Add(this.lblMaTaiKhoan, 0, 0);
            this.tableBieuMau.Controls.Add(this.txtMaTaiKhoan, 1, 0);
            this.tableBieuMau.Controls.Add(this.lblNhanVien, 2, 0);
            this.tableBieuMau.Controls.Add(this.cboNhanVien, 3, 0);
            this.tableBieuMau.Controls.Add(this.lblTenDangNhap, 0, 1);
            this.tableBieuMau.Controls.Add(this.txtTenDangNhap, 1, 1);
            this.tableBieuMau.Controls.Add(this.lblVaiTro, 2, 1);
            this.tableBieuMau.Controls.Add(this.cboVaiTro, 3, 1);
            this.tableBieuMau.Controls.Add(this.lblMatKhauTam, 0, 2);
            this.tableBieuMau.Controls.Add(this.txtMatKhauTam, 1, 2);
            this.tableBieuMau.Controls.Add(this.lblXacNhanMatKhau, 2, 2);
            this.tableBieuMau.Controls.Add(this.txtXacNhanMatKhau, 3, 2);
            this.tableBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBieuMau.Location = new System.Drawing.Point(0, 36);
            this.tableBieuMau.Name = "tableBieuMau";
            this.tableBieuMau.Padding = new System.Windows.Forms.Padding(14, 5, 14, 2);
            this.tableBieuMau.RowCount = 3;
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableBieuMau.TabIndex = 1;
            //
            // form fields
            //
            this.lblMaTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaTaiKhoan.Text = "Mã tài khoản";
            this.lblMaTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaTaiKhoan.BackColor = System.Drawing.Color.FromArgb(245, 246, 248);
            this.txtMaTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaTaiKhoan.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtMaTaiKhoan.ReadOnly = true;
            this.lblNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNhanVien.Text = "Nhân viên (*)";
            this.lblNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNhanVien.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.lblTenDangNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenDangNhap.Text = "Tên đăng nhập (*)";
            this.lblTenDangNhap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTenDangNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDangNhap.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtTenDangNhap.MaxLength = 50;
            this.lblVaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVaiTro.Text = "Vai trò (*)";
            this.lblVaiTro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboVaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboVaiTro.Items.AddRange(new object[] { "ADMIN", "NHANVIEN" });
            this.cboVaiTro.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.lblMatKhauTam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMatKhauTam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMatKhauTam.Text = "Mật khẩu tạm (*)";
            this.lblMatKhauTam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMatKhauTam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMatKhauTam.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhauTam.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtMatKhauTam.UseSystemPasswordChar = true;
            this.lblXacNhanMatKhau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblXacNhanMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblXacNhanMatKhau.Text = "Xác nhận mật khẩu";
            this.lblXacNhanMatKhau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtXacNhanMatKhau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXacNhanMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtXacNhanMatKhau.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.txtXacNhanMatKhau.UseSystemPasswordChar = true;
            //
            // lblThongBao
            //
            this.lblThongBao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThongBao.ForeColor = System.Drawing.Color.Firebrick;
            this.lblThongBao.Location = new System.Drawing.Point(0, 190);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.lblThongBao.Size = new System.Drawing.Size(1076, 28);
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlThaoTac
            //
            this.pnlThaoTac.Controls.Add(this.chkPhaiDoiMatKhau);
            this.pnlThaoTac.Controls.Add(this.chkDangHoatDong);
            this.pnlThaoTac.Controls.Add(this.btnLamMoiBieuMau);
            this.pnlThaoTac.Controls.Add(this.btnDatLaiMatKhau);
            this.pnlThaoTac.Controls.Add(this.btnDoiTrangThai);
            this.pnlThaoTac.Controls.Add(this.btnCapNhat);
            this.pnlThaoTac.Controls.Add(this.btnCapTaiKhoan);
            this.pnlThaoTac.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThaoTac.Location = new System.Drawing.Point(0, 218);
            this.pnlThaoTac.Name = "pnlThaoTac";
            this.pnlThaoTac.Size = new System.Drawing.Size(1076, 60);
            this.chkDangHoatDong.AutoSize = true;
            this.chkDangHoatDong.Enabled = false;
            this.chkDangHoatDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkDangHoatDong.Location = new System.Drawing.Point(18, 12);
            this.chkDangHoatDong.Text = "Đang hoạt động";
            this.chkPhaiDoiMatKhau.AutoSize = true;
            this.chkPhaiDoiMatKhau.Enabled = false;
            this.chkPhaiDoiMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkPhaiDoiMatKhau.Location = new System.Drawing.Point(18, 35);
            this.chkPhaiDoiMatKhau.Text = "Bắt buộc đổi mật khẩu";
            //
            // action buttons
            //
            this.btnLamMoiBieuMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiBieuMau.BackColor = System.Drawing.Color.White;
            this.btnLamMoiBieuMau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiBieuMau.Location = new System.Drawing.Point(952, 13);
            this.btnLamMoiBieuMau.Name = "btnLamMoiBieuMau";
            this.btnLamMoiBieuMau.Size = new System.Drawing.Size(108, 34);
            this.btnLamMoiBieuMau.Text = "Làm mới";
            this.btnLamMoiBieuMau.UseVisualStyleBackColor = false;
            this.btnLamMoiBieuMau.Click += new System.EventHandler(this.btnLamMoiBieuMau_Click);
            this.btnDatLaiMatKhau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDatLaiMatKhau.BackColor = System.Drawing.Color.FromArgb(83, 93, 106);
            this.btnDatLaiMatKhau.FlatAppearance.BorderSize = 0;
            this.btnDatLaiMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatLaiMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnDatLaiMatKhau.Location = new System.Drawing.Point(798, 13);
            this.btnDatLaiMatKhau.Name = "btnDatLaiMatKhau";
            this.btnDatLaiMatKhau.Size = new System.Drawing.Size(148, 34);
            this.btnDatLaiMatKhau.Text = "Đặt lại mật khẩu";
            this.btnDatLaiMatKhau.UseVisualStyleBackColor = false;
            this.btnDatLaiMatKhau.Click += new System.EventHandler(this.btnDatLaiMatKhau_Click);
            this.btnDoiTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoiTrangThai.BackColor = System.Drawing.Color.FromArgb(186, 94, 66);
            this.btnDoiTrangThai.FlatAppearance.BorderSize = 0;
            this.btnDoiTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnDoiTrangThai.Location = new System.Drawing.Point(648, 13);
            this.btnDoiTrangThai.Name = "btnDoiTrangThai";
            this.btnDoiTrangThai.Size = new System.Drawing.Size(144, 34);
            this.btnDoiTrangThai.Text = "Khóa tài khoản";
            this.btnDoiTrangThai.UseVisualStyleBackColor = false;
            this.btnDoiTrangThai.Click += new System.EventHandler(this.btnDoiTrangThai_Click);
            this.btnCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(63, 111, 155);
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(534, 13);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(108, 34);
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            this.btnCapTaiKhoan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapTaiKhoan.BackColor = System.Drawing.Color.FromArgb(137, 100, 28);
            this.btnCapTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnCapTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapTaiKhoan.ForeColor = System.Drawing.Color.White;
            this.btnCapTaiKhoan.Location = new System.Drawing.Point(402, 13);
            this.btnCapTaiKhoan.Name = "btnCapTaiKhoan";
            this.btnCapTaiKhoan.Size = new System.Drawing.Size(126, 34);
            this.btnCapTaiKhoan.Text = "Cấp tài khoản";
            this.btnCapTaiKhoan.UseVisualStyleBackColor = false;
            this.btnCapTaiKhoan.Click += new System.EventHandler(this.btnCapTaiKhoan_Click);
            //
            // lblTieuDeBieuMau
            //
            this.lblTieuDeBieuMau.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeBieuMau.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBieuMau.ForeColor = System.Drawing.Color.FromArgb(34, 45, 58);
            this.lblTieuDeBieuMau.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDeBieuMau.Name = "lblTieuDeBieuMau";
            this.lblTieuDeBieuMau.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.lblTieuDeBieuMau.Size = new System.Drawing.Size(1076, 36);
            this.lblTieuDeBieuMau.Text = "Thông tin tài khoản";
            this.lblTieuDeBieuMau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // FrmTaiKhoan
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 248);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBoLoc);
            this.Name = "FrmTaiKhoan";
            this.Text = "Quản lý tài khoản và phân quyền";
            this.Load += new System.EventHandler(this.FrmTaiKhoan_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDangNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChucVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVaiTro;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoiMatKhau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThaiNhanVien;
        private System.Windows.Forms.Panel pnlBieuMau;
        private System.Windows.Forms.TableLayoutPanel tableBieuMau;
        private System.Windows.Forms.Label lblMaTaiKhoan;
        private System.Windows.Forms.TextBox txtMaTaiKhoan;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.ComboBox cboVaiTro;
        private System.Windows.Forms.Label lblMatKhauTam;
        private System.Windows.Forms.TextBox txtMatKhauTam;
        private System.Windows.Forms.Label lblXacNhanMatKhau;
        private System.Windows.Forms.TextBox txtXacNhanMatKhau;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Panel pnlThaoTac;
        private System.Windows.Forms.CheckBox chkPhaiDoiMatKhau;
        private System.Windows.Forms.CheckBox chkDangHoatDong;
        private System.Windows.Forms.Button btnLamMoiBieuMau;
        private System.Windows.Forms.Button btnDatLaiMatKhau;
        private System.Windows.Forms.Button btnDoiTrangThai;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnCapTaiKhoan;
        private System.Windows.Forms.Label lblTieuDeBieuMau;
    }
}

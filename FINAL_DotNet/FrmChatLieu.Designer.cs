namespace FINAL_DotNet
{
    partial class FrmChatLieu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.lblLocTrangThai = new System.Windows.Forms.Label();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvChatLieu = new System.Windows.Forms.DataGridView();
            this.pnlBieuMau = new System.Windows.Forms.Panel();
            this.tableBieuMau = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaChatLieu = new System.Windows.Forms.Label();
            this.txtMaChatLieu = new System.Windows.Forms.TextBox();
            this.lblTenChatLieu = new System.Windows.Forms.Label();
            this.txtTenChatLieu = new System.Windows.Forms.TextBox();
            this.lblGiaMuaVao = new System.Windows.Forms.Label();
            this.numGiaMuaVao = new System.Windows.Forms.NumericUpDown();
            this.lblGiaBanRa = new System.Windows.Forms.Label();
            this.numGiaBanRa = new System.Windows.Forms.NumericUpDown();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.pnlThaoTac = new System.Windows.Forms.Panel();
            this.chkDangHoatDong = new System.Windows.Forms.CheckBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoaHoacTrangThai = new System.Windows.Forms.Button();
            this.btnLamMoiBieuMau = new System.Windows.Forms.Button();
            this.lblTieuDeBieuMau = new System.Windows.Forms.Label();
            this.pnlBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChatLieu)).BeginInit();
            this.pnlBieuMau.SuspendLayout();
            this.tableBieuMau.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaMuaVao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBanRa)).BeginInit();
            this.pnlThaoTac.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoLoc
            // 
            this.pnlBoLoc.BackColor = System.Drawing.Color.White;
            this.pnlBoLoc.Controls.Add(this.lblTuKhoa);
            this.pnlBoLoc.Controls.Add(this.txtTuKhoa);
            this.pnlBoLoc.Controls.Add(this.lblLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.cboLocTrangThai);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.btnTaiLai);
            this.pnlBoLoc.Controls.Add(this.lblSoKetQua);
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBoLoc.Location = new System.Drawing.Point(0, 0);
            this.pnlBoLoc.Name = "pnlBoLoc";
            this.pnlBoLoc.Size = new System.Drawing.Size(1050, 72);
            this.pnlBoLoc.TabIndex = 1;
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuKhoa.Location = new System.Drawing.Point(18, 10);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(59, 15);
            this.lblTuKhoa.TabIndex = 0;
            this.lblTuKhoa.Text = "Tìm kiếm";
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTuKhoa.Location = new System.Drawing.Point(20, 31);
            this.txtTuKhoa.MaxLength = 100;
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(300, 23);
            this.txtTuKhoa.TabIndex = 1;
            this.txtTuKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTuKhoa_KeyDown);
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocTrangThai.Location = new System.Drawing.Point(338, 10);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(62, 15);
            this.lblLocTrangThai.TabIndex = 2;
            this.lblLocTrangThai.Text = "Trạng thái";
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLocTrangThai.Items.AddRange(new object[] {
            "Tất cả",
            "Đang hoạt động",
            "Ngừng hoạt động"});
            this.cboLocTrangThai.Location = new System.Drawing.Point(340, 31);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(165, 23);
            this.cboLocTrangThai.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(520, 29);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(95, 28);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.BackColor = System.Drawing.Color.White;
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Location = new System.Drawing.Point(623, 29);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(85, 28);
            this.btnTaiLai.TabIndex = 5;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // lblSoKetQua
            // 
            this.lblSoKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(101)))), ((int)(((byte)(113)))));
            this.lblSoKetQua.Location = new System.Drawing.Point(870, 29);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Size = new System.Drawing.Size(160, 28);
            this.lblSoKetQua.TabIndex = 6;
            this.lblSoKetQua.Text = "0 chất liệu";
            this.lblSoKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 72);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvChatLieu);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(12, 10, 12, 4);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlBieuMau);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(12, 4, 12, 10);
            this.splitContainer.Size = new System.Drawing.Size(1050, 578);
            this.splitContainer.SplitterDistance = 330;
            this.splitContainer.TabIndex = 0;
            // 
            // dgvChatLieu
            // 
            this.dgvChatLieu.AllowUserToAddRows = false;
            this.dgvChatLieu.AllowUserToDeleteRows = false;
            this.dgvChatLieu.BackgroundColor = System.Drawing.Color.White;
            this.dgvChatLieu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(58)))));
            this.dgvChatLieu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChatLieu.ColumnHeadersHeight = 38;
            this.dgvChatLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChatLieu.EnableHeadersVisualStyles = false;
            this.dgvChatLieu.Location = new System.Drawing.Point(12, 10);
            this.dgvChatLieu.MultiSelect = false;
            this.dgvChatLieu.Name = "dgvChatLieu";
            this.dgvChatLieu.ReadOnly = true;
            this.dgvChatLieu.RowHeadersVisible = false;
            this.dgvChatLieu.RowTemplate.Height = 34;
            this.dgvChatLieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChatLieu.Size = new System.Drawing.Size(1026, 316);
            this.dgvChatLieu.TabIndex = 0;
            this.dgvChatLieu.SelectionChanged += new System.EventHandler(this.dgvChatLieu_SelectionChanged);
            // 
            // pnlBieuMau
            // 
            this.pnlBieuMau.BackColor = System.Drawing.Color.White;
            this.pnlBieuMau.Controls.Add(this.tableBieuMau);
            this.pnlBieuMau.Controls.Add(this.lblThongBao);
            this.pnlBieuMau.Controls.Add(this.pnlThaoTac);
            this.pnlBieuMau.Controls.Add(this.lblTieuDeBieuMau);
            this.pnlBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBieuMau.Location = new System.Drawing.Point(12, 4);
            this.pnlBieuMau.Name = "pnlBieuMau";
            this.pnlBieuMau.Size = new System.Drawing.Size(1026, 230);
            this.pnlBieuMau.TabIndex = 0;
            // 
            // tableBieuMau
            // 
            this.tableBieuMau.ColumnCount = 4;
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableBieuMau.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.Controls.Add(this.lblMaChatLieu, 0, 0);
            this.tableBieuMau.Controls.Add(this.txtMaChatLieu, 1, 0);
            this.tableBieuMau.Controls.Add(this.lblTenChatLieu, 2, 0);
            this.tableBieuMau.Controls.Add(this.txtTenChatLieu, 3, 0);
            this.tableBieuMau.Controls.Add(this.lblGiaMuaVao, 0, 1);
            this.tableBieuMau.Controls.Add(this.numGiaMuaVao, 1, 1);
            this.tableBieuMau.Controls.Add(this.lblGiaBanRa, 2, 1);
            this.tableBieuMau.Controls.Add(this.numGiaBanRa, 3, 1);
            this.tableBieuMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBieuMau.Location = new System.Drawing.Point(0, 36);
            this.tableBieuMau.Name = "tableBieuMau";
            this.tableBieuMau.Padding = new System.Windows.Forms.Padding(14, 9, 14, 4);
            this.tableBieuMau.RowCount = 2;
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBieuMau.Size = new System.Drawing.Size(1026, 106);
            this.tableBieuMau.TabIndex = 0;
            // 
            // lblMaChatLieu
            // 
            this.lblMaChatLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaChatLieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaChatLieu.Location = new System.Drawing.Point(17, 9);
            this.lblMaChatLieu.Name = "lblMaChatLieu";
            this.lblMaChatLieu.Size = new System.Drawing.Size(119, 46);
            this.lblMaChatLieu.TabIndex = 0;
            this.lblMaChatLieu.Text = "Mã chất liệu";
            this.lblMaChatLieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaChatLieu
            // 
            this.txtMaChatLieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.txtMaChatLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaChatLieu.Location = new System.Drawing.Point(142, 16);
            this.txtMaChatLieu.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.txtMaChatLieu.Name = "txtMaChatLieu";
            this.txtMaChatLieu.ReadOnly = true;
            this.txtMaChatLieu.Size = new System.Drawing.Size(359, 20);
            this.txtMaChatLieu.TabIndex = 1;
            // 
            // lblTenChatLieu
            // 
            this.lblTenChatLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenChatLieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenChatLieu.Location = new System.Drawing.Point(516, 9);
            this.lblTenChatLieu.Name = "lblTenChatLieu";
            this.lblTenChatLieu.Size = new System.Drawing.Size(119, 46);
            this.lblTenChatLieu.TabIndex = 2;
            this.lblTenChatLieu.Text = "Tên chất liệu (*)";
            this.lblTenChatLieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTenChatLieu
            // 
            this.txtTenChatLieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenChatLieu.Location = new System.Drawing.Point(641, 16);
            this.txtTenChatLieu.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.txtTenChatLieu.MaxLength = 100;
            this.txtTenChatLieu.Name = "txtTenChatLieu";
            this.txtTenChatLieu.Size = new System.Drawing.Size(368, 20);
            this.txtTenChatLieu.TabIndex = 3;
            // 
            // lblGiaMuaVao
            // 
            this.lblGiaMuaVao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGiaMuaVao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGiaMuaVao.Location = new System.Drawing.Point(17, 55);
            this.lblGiaMuaVao.Name = "lblGiaMuaVao";
            this.lblGiaMuaVao.Size = new System.Drawing.Size(119, 47);
            this.lblGiaMuaVao.TabIndex = 4;
            this.lblGiaMuaVao.Text = "Giá mua vào (*)";
            this.lblGiaMuaVao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numGiaMuaVao
            // 
            this.numGiaMuaVao.DecimalPlaces = 2;
            this.numGiaMuaVao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numGiaMuaVao.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGiaMuaVao.Location = new System.Drawing.Point(142, 62);
            this.numGiaMuaVao.Margin = new System.Windows.Forms.Padding(3, 7, 12, 6);
            this.numGiaMuaVao.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.numGiaMuaVao.Name = "numGiaMuaVao";
            this.numGiaMuaVao.Size = new System.Drawing.Size(359, 20);
            this.numGiaMuaVao.TabIndex = 5;
            this.numGiaMuaVao.ThousandsSeparator = true;
            // 
            // lblGiaBanRa
            // 
            this.lblGiaBanRa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGiaBanRa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGiaBanRa.Location = new System.Drawing.Point(516, 55);
            this.lblGiaBanRa.Name = "lblGiaBanRa";
            this.lblGiaBanRa.Size = new System.Drawing.Size(119, 47);
            this.lblGiaBanRa.TabIndex = 6;
            this.lblGiaBanRa.Text = "Giá bán ra (*)";
            this.lblGiaBanRa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numGiaBanRa
            // 
            this.numGiaBanRa.DecimalPlaces = 2;
            this.numGiaBanRa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numGiaBanRa.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGiaBanRa.Location = new System.Drawing.Point(641, 62);
            this.numGiaBanRa.Margin = new System.Windows.Forms.Padding(3, 7, 3, 6);
            this.numGiaBanRa.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.numGiaBanRa.Name = "numGiaBanRa";
            this.numGiaBanRa.Size = new System.Drawing.Size(368, 20);
            this.numGiaBanRa.TabIndex = 7;
            this.numGiaBanRa.ThousandsSeparator = true;
            // 
            // lblThongBao
            // 
            this.lblThongBao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblThongBao.ForeColor = System.Drawing.Color.Firebrick;
            this.lblThongBao.Location = new System.Drawing.Point(0, 142);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.lblThongBao.Size = new System.Drawing.Size(1026, 28);
            this.lblThongBao.TabIndex = 1;
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlThaoTac
            // 
            this.pnlThaoTac.Controls.Add(this.chkDangHoatDong);
            this.pnlThaoTac.Controls.Add(this.btnThem);
            this.pnlThaoTac.Controls.Add(this.btnCapNhat);
            this.pnlThaoTac.Controls.Add(this.btnXoaHoacTrangThai);
            this.pnlThaoTac.Controls.Add(this.btnLamMoiBieuMau);
            this.pnlThaoTac.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThaoTac.Location = new System.Drawing.Point(0, 170);
            this.pnlThaoTac.Name = "pnlThaoTac";
            this.pnlThaoTac.Size = new System.Drawing.Size(1026, 60);
            this.pnlThaoTac.TabIndex = 2;
            // 
            // chkDangHoatDong
            // 
            this.chkDangHoatDong.AutoSize = true;
            this.chkDangHoatDong.Enabled = false;
            this.chkDangHoatDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkDangHoatDong.Location = new System.Drawing.Point(18, 22);
            this.chkDangHoatDong.Name = "chkDangHoatDong";
            this.chkDangHoatDong.Size = new System.Drawing.Size(115, 19);
            this.chkDangHoatDong.TabIndex = 0;
            this.chkDangHoatDong.Text = "Đang hoạt động";
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(518, 13);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(108, 34);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(111)))), ((int)(((byte)(155)))));
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(632, 13);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(108, 34);
            this.btnCapNhat.TabIndex = 2;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnXoaHoacTrangThai
            // 
            this.btnXoaHoacTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaHoacTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(94)))), ((int)(((byte)(66)))));
            this.btnXoaHoacTrangThai.FlatAppearance.BorderSize = 0;
            this.btnXoaHoacTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaHoacTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnXoaHoacTrangThai.Location = new System.Drawing.Point(746, 13);
            this.btnXoaHoacTrangThai.Name = "btnXoaHoacTrangThai";
            this.btnXoaHoacTrangThai.Size = new System.Drawing.Size(150, 34);
            this.btnXoaHoacTrangThai.TabIndex = 3;
            this.btnXoaHoacTrangThai.Text = "Xóa chất liệu";
            this.btnXoaHoacTrangThai.UseVisualStyleBackColor = false;
            this.btnXoaHoacTrangThai.Click += new System.EventHandler(this.btnXoaHoacTrangThai_Click);
            // 
            // btnLamMoiBieuMau
            // 
            this.btnLamMoiBieuMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiBieuMau.BackColor = System.Drawing.Color.White;
            this.btnLamMoiBieuMau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiBieuMau.Location = new System.Drawing.Point(902, 13);
            this.btnLamMoiBieuMau.Name = "btnLamMoiBieuMau";
            this.btnLamMoiBieuMau.Size = new System.Drawing.Size(116, 34);
            this.btnLamMoiBieuMau.TabIndex = 4;
            this.btnLamMoiBieuMau.Text = "Làm mới";
            this.btnLamMoiBieuMau.UseVisualStyleBackColor = false;
            this.btnLamMoiBieuMau.Click += new System.EventHandler(this.btnLamMoiBieuMau_Click);
            // 
            // lblTieuDeBieuMau
            // 
            this.lblTieuDeBieuMau.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDeBieuMau.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeBieuMau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(58)))));
            this.lblTieuDeBieuMau.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDeBieuMau.Name = "lblTieuDeBieuMau";
            this.lblTieuDeBieuMau.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.lblTieuDeBieuMau.Size = new System.Drawing.Size(1026, 36);
            this.lblTieuDeBieuMau.TabIndex = 3;
            this.lblTieuDeBieuMau.Text = "Thông tin chất liệu và giá tham khảo";
            this.lblTieuDeBieuMau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmChatLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1050, 650);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBoLoc);
            this.Name = "FrmChatLieu";
            this.Text = "Quản lý chất liệu và giá tham khảo";
            this.Load += new System.EventHandler(this.FrmChatLieu_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChatLieu)).EndInit();
            this.pnlBieuMau.ResumeLayout(false);
            this.tableBieuMau.ResumeLayout(false);
            this.tableBieuMau.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaMuaVao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBanRa)).EndInit();
            this.pnlThaoTac.ResumeLayout(false);
            this.pnlThaoTac.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Label lblLocTrangThai;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvChatLieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaChatLieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenChatLieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaMuaVao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBanRa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.Panel pnlBieuMau;
        private System.Windows.Forms.Label lblTieuDeBieuMau;
        private System.Windows.Forms.TableLayoutPanel tableBieuMau;
        private System.Windows.Forms.Label lblMaChatLieu;
        private System.Windows.Forms.TextBox txtMaChatLieu;
        private System.Windows.Forms.Label lblTenChatLieu;
        private System.Windows.Forms.TextBox txtTenChatLieu;
        private System.Windows.Forms.Label lblGiaMuaVao;
        private System.Windows.Forms.NumericUpDown numGiaMuaVao;
        private System.Windows.Forms.Label lblGiaBanRa;
        private System.Windows.Forms.NumericUpDown numGiaBanRa;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Panel pnlThaoTac;
        private System.Windows.Forms.CheckBox chkDangHoatDong;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoaHoacTrangThai;
        private System.Windows.Forms.Button btnLamMoiBieuMau;
    }
}

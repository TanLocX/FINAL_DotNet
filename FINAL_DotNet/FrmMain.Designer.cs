namespace FINAL_DotNet
{
    partial class FrmMain
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
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.flowMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlNguoiDung = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            this.lblTenDangNhap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblHoTen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblThuongHieu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblVaiTro = new Guna.UI2.WinForms.Guna2Chip();
            this.lblTieuDeTrang = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlChaoMung = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMoTa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNoiDungChinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblChaoMung = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlSidebar.SuspendLayout();
            this.pnlNguoiDung.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlChaoMung.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.pnlSidebar.Controls.Add(this.flowMenu);
            this.pnlSidebar.Controls.Add(this.pnlNguoiDung);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(235, 761);
            this.pnlSidebar.TabIndex = 0;
            // 
            // flowMenu
            // 
            this.flowMenu.AutoScroll = true;
            this.flowMenu.BackColor = System.Drawing.Color.Transparent;
            this.flowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMenu.Location = new System.Drawing.Point(0, 76);
            this.flowMenu.Name = "flowMenu";
            this.flowMenu.Padding = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.flowMenu.Size = new System.Drawing.Size(235, 555);
            this.flowMenu.TabIndex = 1;
            this.flowMenu.WrapContents = false;
            this.flowMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.flowMenu_Paint);
            // 
            // pnlNguoiDung
            // 
            this.pnlNguoiDung.BackColor = System.Drawing.Color.Transparent;
            this.pnlNguoiDung.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.pnlNguoiDung.BorderRadius = 10;
            this.pnlNguoiDung.BorderThickness = 1;
            this.pnlNguoiDung.Controls.Add(this.btnDangXuat);
            this.pnlNguoiDung.Controls.Add(this.lblTenDangNhap);
            this.pnlNguoiDung.Controls.Add(this.lblHoTen);
            this.pnlNguoiDung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNguoiDung.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(31)))), ((int)(((byte)(44)))));
            this.pnlNguoiDung.Location = new System.Drawing.Point(0, 631);
            this.pnlNguoiDung.Margin = new System.Windows.Forms.Padding(8);
            this.pnlNguoiDung.Name = "pnlNguoiDung";
            this.pnlNguoiDung.Padding = new System.Windows.Forms.Padding(12);
            this.pnlNguoiDung.Size = new System.Drawing.Size(235, 130);
            this.pnlNguoiDung.TabIndex = 2;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDangXuat.Animated = true;
            this.btnDangXuat.BorderRadius = 8;
            this.btnDangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangXuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDangXuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDangXuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDangXuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDangXuat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(160)))), ((int)(((byte)(95)))));
            this.btnDangXuat.Location = new System.Drawing.Point(12, 78);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(211, 38);
            this.btnDangXuat.TabIndex = 2;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTenDangNhap.AutoSize = false;
            this.lblTenDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(182)))), ((int)(((byte)(194)))));
            this.lblTenDangNhap.Location = new System.Drawing.Point(12, 42);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(211, 22);
            this.lblTenDangNhap.TabIndex = 1;
            this.lblTenDangNhap.Text = "@tendangnhap";
            this.lblTenDangNhap.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHoTen
            // 
            this.lblHoTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHoTen.AutoSize = false;
            this.lblHoTen.BackColor = System.Drawing.Color.Transparent;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.White;
            this.lblHoTen.Location = new System.Drawing.Point(12, 14);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(211, 24);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Họ tên nhân viên";
            this.lblHoTen.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogo.Controls.Add(this.lblThuongHieu);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(235, 76);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblThuongHieu
            // 
            this.lblThuongHieu.AutoSize = false;
            this.lblThuongHieu.BackColor = System.Drawing.Color.Transparent;
            this.lblThuongHieu.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblThuongHieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.lblThuongHieu.Location = new System.Drawing.Point(18, 22);
            this.lblThuongHieu.Name = "lblThuongHieu";
            this.lblThuongHieu.Size = new System.Drawing.Size(200, 32);
            this.lblThuongHieu.TabIndex = 0;
            this.lblThuongHieu.Text = "PNJ MANAGER";
            this.lblThuongHieu.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlHeader.BorderThickness = 1;
            this.pnlHeader.Controls.Add(this.lblVaiTro);
            this.pnlHeader.Controls.Add(this.lblTieuDeTrang);
            this.pnlHeader.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlHeader.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.White;
            this.pnlHeader.Location = new System.Drawing.Point(235, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1149, 76);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVaiTro.BorderRadius = 8;
            this.lblVaiTro.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.lblVaiTro.IsClosable = false;
            this.lblVaiTro.Location = new System.Drawing.Point(950, 22);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(171, 32);
            this.lblVaiTro.TabIndex = 1;
            this.lblVaiTro.Text = "VAI TRÒ";
            // 
            // lblTieuDeTrang
            // 
            this.lblTieuDeTrang.AutoSize = false;
            this.lblTieuDeTrang.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDeTrang.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeTrang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(58)))));
            this.lblTieuDeTrang.Location = new System.Drawing.Point(27, 20);
            this.lblTieuDeTrang.Name = "lblTieuDeTrang";
            this.lblTieuDeTrang.Size = new System.Drawing.Size(500, 36);
            this.lblTieuDeTrang.TabIndex = 0;
            this.lblTieuDeTrang.Text = "Tổng quan hệ thống";
            this.lblTieuDeTrang.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.pnlContent.Controls.Add(this.pnlChaoMung);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.pnlContent.Location = new System.Drawing.Point(235, 76);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(28);
            this.pnlContent.Size = new System.Drawing.Size(1149, 685);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlChaoMung
            // 
            this.pnlChaoMung.BackColor = System.Drawing.Color.Transparent;
            this.pnlChaoMung.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlChaoMung.BorderRadius = 12;
            this.pnlChaoMung.BorderThickness = 1;
            this.pnlChaoMung.Controls.Add(this.lblMoTa);
            this.pnlChaoMung.Controls.Add(this.lblNoiDungChinh);
            this.pnlChaoMung.Controls.Add(this.lblChaoMung);
            this.pnlChaoMung.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChaoMung.FillColor = System.Drawing.Color.White;
            this.pnlChaoMung.Location = new System.Drawing.Point(28, 28);
            this.pnlChaoMung.Name = "pnlChaoMung";
            this.pnlChaoMung.Padding = new System.Windows.Forms.Padding(30);
            this.pnlChaoMung.Size = new System.Drawing.Size(1093, 205);
            this.pnlChaoMung.TabIndex = 0;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = false;
            this.lblMoTa.BackColor = System.Drawing.Color.Transparent;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(109)))), ((int)(((byte)(121)))));
            this.lblMoTa.Location = new System.Drawing.Point(30, 126);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(600, 26);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Chọn một chức năng từ menu để bắt đầu làm việc.";
            this.lblMoTa.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNoiDungChinh
            // 
            this.lblNoiDungChinh.AutoSize = false;
            this.lblNoiDungChinh.BackColor = System.Drawing.Color.Transparent;
            this.lblNoiDungChinh.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblNoiDungChinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.lblNoiDungChinh.Location = new System.Drawing.Point(30, 88);
            this.lblNoiDungChinh.Name = "lblNoiDungChinh";
            this.lblNoiDungChinh.Size = new System.Drawing.Size(700, 32);
            this.lblNoiDungChinh.TabIndex = 1;
            this.lblNoiDungChinh.Text = "HỆ THỐNG QUẢN LÝ CỬA HÀNG ĐÁ QUÝ PNJ";
            this.lblNoiDungChinh.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChaoMung
            // 
            this.lblChaoMung.AutoSize = false;
            this.lblChaoMung.BackColor = System.Drawing.Color.Transparent;
            this.lblChaoMung.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblChaoMung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(80)))), ((int)(((byte)(94)))));
            this.lblChaoMung.Location = new System.Drawing.Point(31, 42);
            this.lblChaoMung.Name = "lblChaoMung";
            this.lblChaoMung.Size = new System.Drawing.Size(400, 24);
            this.lblChaoMung.TabIndex = 0;
            this.lblChaoMung.Text = "Xin chào, nhân viên";
            this.lblChaoMung.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý cửa hàng đá quý PNJ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlNguoiDung.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlChaoMung.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Panel pnlLogo;
        private System.Windows.Forms.FlowLayoutPanel flowMenu;
        private Guna.UI2.WinForms.Guna2Panel pnlNguoiDung;
        private Guna.UI2.WinForms.Guna2Button btnDangXuat;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTenDangNhap;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHoTen;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThuongHieu;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Chip lblVaiTro;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTieuDeTrang;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2Panel pnlChaoMung;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMoTa;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNoiDungChinh;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblChaoMung;
    }
}

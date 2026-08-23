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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.flowMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlNguoiDung = new System.Windows.Forms.Panel();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblThuongHieu = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.lblTieuDeTrang = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlChaoMung = new System.Windows.Forms.Panel();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblNoiDungChinh = new System.Windows.Forms.Label();
            this.lblChaoMung = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlNguoiDung.SuspendLayout();
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
            this.pnlSidebar.Controls.Add(this.lblThuongHieu);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(235, 721);
            this.pnlSidebar.TabIndex = 0;
            // 
            // flowMenu
            // 
            this.flowMenu.AutoScroll = true;
            this.flowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMenu.Location = new System.Drawing.Point(0, 76);
            this.flowMenu.Name = "flowMenu";
            this.flowMenu.Padding = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.flowMenu.Size = new System.Drawing.Size(235, 523);
            this.flowMenu.TabIndex = 1;
            this.flowMenu.WrapContents = false;
            // 
            // pnlNguoiDung
            // 
            this.pnlNguoiDung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(31)))), ((int)(((byte)(44)))));
            this.pnlNguoiDung.Controls.Add(this.btnDangXuat);
            this.pnlNguoiDung.Controls.Add(this.lblTenDangNhap);
            this.pnlNguoiDung.Controls.Add(this.lblHoTen);
            this.pnlNguoiDung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNguoiDung.Location = new System.Drawing.Point(0, 599);
            this.pnlNguoiDung.Name = "pnlNguoiDung";
            this.pnlNguoiDung.Size = new System.Drawing.Size(235, 122);
            this.pnlNguoiDung.TabIndex = 2;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.btnDangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangXuat.FlatAppearance.BorderSize = 0;
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.Location = new System.Drawing.Point(16, 73);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(202, 34);
            this.btnDangXuat.TabIndex = 2;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = false;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoEllipsis = true;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(182)))), ((int)(((byte)(194)))));
            this.lblTenDangNhap.Location = new System.Drawing.Point(16, 39);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(202, 21);
            this.lblTenDangNhap.TabIndex = 1;
            this.lblTenDangNhap.Text = "@tendangnhap";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoEllipsis = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.White;
            this.lblHoTen.Location = new System.Drawing.Point(15, 13);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(203, 23);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Họ tên nhân viên";
            // 
            // lblThuongHieu
            // 
            this.lblThuongHieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblThuongHieu.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblThuongHieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.lblThuongHieu.Location = new System.Drawing.Point(0, 0);
            this.lblThuongHieu.Name = "lblThuongHieu";
            this.lblThuongHieu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblThuongHieu.Size = new System.Drawing.Size(235, 76);
            this.lblThuongHieu.TabIndex = 0;
            this.lblThuongHieu.Text = "PNJ MANAGER";
            this.lblThuongHieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblVaiTro);
            this.pnlHeader.Controls.Add(this.lblTieuDeTrang);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(235, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1029, 76);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVaiTro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.lblVaiTro.Location = new System.Drawing.Point(830, 22);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(171, 31);
            this.lblVaiTro.TabIndex = 1;
            this.lblVaiTro.Text = "VAI TRÒ";
            this.lblVaiTro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTieuDeTrang
            // 
            this.lblTieuDeTrang.AutoSize = true;
            this.lblTieuDeTrang.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeTrang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(58)))));
            this.lblTieuDeTrang.Location = new System.Drawing.Point(27, 20);
            this.lblTieuDeTrang.Name = "lblTieuDeTrang";
            this.lblTieuDeTrang.Size = new System.Drawing.Size(236, 32);
            this.lblTieuDeTrang.TabIndex = 0;
            this.lblTieuDeTrang.Text = "Tổng quan hệ thống";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.pnlContent.Controls.Add(this.pnlChaoMung);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(235, 76);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(28);
            this.pnlContent.Size = new System.Drawing.Size(1029, 645);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlChaoMung
            // 
            this.pnlChaoMung.BackColor = System.Drawing.Color.White;
            this.pnlChaoMung.Controls.Add(this.lblMoTa);
            this.pnlChaoMung.Controls.Add(this.lblNoiDungChinh);
            this.pnlChaoMung.Controls.Add(this.lblChaoMung);
            this.pnlChaoMung.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChaoMung.Location = new System.Drawing.Point(28, 28);
            this.pnlChaoMung.Name = "pnlChaoMung";
            this.pnlChaoMung.Padding = new System.Windows.Forms.Padding(30);
            this.pnlChaoMung.Size = new System.Drawing.Size(973, 205);
            this.pnlChaoMung.TabIndex = 0;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(109)))), ((int)(((byte)(121)))));
            this.lblMoTa.Location = new System.Drawing.Point(31, 132);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(326, 19);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Chọn một chức năng từ menu để bắt đầu làm việc.";
            // 
            // lblNoiDungChinh
            // 
            this.lblNoiDungChinh.AutoSize = true;
            this.lblNoiDungChinh.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblNoiDungChinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(100)))), ((int)(((byte)(28)))));
            this.lblNoiDungChinh.Location = new System.Drawing.Point(30, 88);
            this.lblNoiDungChinh.Name = "lblNoiDungChinh";
            this.lblNoiDungChinh.Size = new System.Drawing.Size(437, 28);
            this.lblNoiDungChinh.TabIndex = 1;
            this.lblNoiDungChinh.Text = "HỆ THỐNG QUẢN LÝ CỬA HÀNG ĐÁ QUÝ PNJ";
            // 
            // lblChaoMung
            // 
            this.lblChaoMung.AutoSize = true;
            this.lblChaoMung.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblChaoMung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(80)))), ((int)(((byte)(94)))));
            this.lblChaoMung.Location = new System.Drawing.Point(31, 42);
            this.lblChaoMung.Name = "lblChaoMung";
            this.lblChaoMung.Size = new System.Drawing.Size(142, 20);
            this.lblChaoMung.TabIndex = 0;
            this.lblChaoMung.Text = "Xin chào, nhân viên";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 721);
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
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlChaoMung.ResumeLayout(false);
            this.pnlChaoMung.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.FlowLayoutPanel flowMenu;
        private System.Windows.Forms.Panel pnlNguoiDung;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblThuongHieu;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.Label lblTieuDeTrang;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlChaoMung;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblNoiDungChinh;
        private System.Windows.Forms.Label lblChaoMung;
    }
}

namespace FINAL_DotNet
{
    partial class FormDangKy
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lbThongBaoLoi = new System.Windows.Forms.Label();
            this.btnDangKy = new Guna.UI2.WinForms.Guna2Button();
            this.txtNhapLaiMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenDangNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaNhanVien = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.btnQuayLai);
            this.guna2Panel1.Controls.Add(this.lbThongBaoLoi);
            this.guna2Panel1.Controls.Add(this.btnDangKy);
            this.guna2Panel1.Controls.Add(this.txtMaNhanVien);
            this.guna2Panel1.Controls.Add(this.txtNhapLaiMatKhau);
            this.guna2Panel1.Controls.Add(this.txtMatKhau);
            this.guna2Panel1.Controls.Add(this.txtTenDangNhap);
            this.guna2Panel1.Controls.Add(this.labelTitle);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(800, 550);
            this.guna2Panel1.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labelTitle.Location = new System.Drawing.Point(250, 30);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(301, 37);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.BorderRadius = 8;
            this.txtTenDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDangNhap.Location = new System.Drawing.Point(250, 100);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.PlaceholderText = "Tên đăng nhập";
            this.txtTenDangNhap.Size = new System.Drawing.Size(300, 40);
            this.txtTenDangNhap.TabIndex = 1;
            this.txtTenDangNhap.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderRadius = 8;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhau.Location = new System.Drawing.Point(250, 160);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.PlaceholderText = "Mật khẩu";
            this.txtMatKhau.Size = new System.Drawing.Size(300, 40);
            this.txtMatKhau.TabIndex = 2;
            this.txtMatKhau.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtNhapLaiMatKhau
            // 
            this.txtNhapLaiMatKhau.BorderRadius = 8;
            this.txtNhapLaiMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhapLaiMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNhapLaiMatKhau.Location = new System.Drawing.Point(250, 220);
            this.txtNhapLaiMatKhau.Name = "txtNhapLaiMatKhau";
            this.txtNhapLaiMatKhau.PasswordChar = '*';
            this.txtNhapLaiMatKhau.PlaceholderText = "Nhập lại mật khẩu";
            this.txtNhapLaiMatKhau.Size = new System.Drawing.Size(300, 40);
            this.txtNhapLaiMatKhau.TabIndex = 3;
            this.txtNhapLaiMatKhau.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtMaNhanVien
            // 
            this.txtMaNhanVien.BorderRadius = 8;
            this.txtMaNhanVien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaNhanVien.Location = new System.Drawing.Point(250, 280);
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.PlaceholderText = "Mã nhân viên (VD: NV001)";
            this.txtMaNhanVien.Size = new System.Drawing.Size(300, 40);
            this.txtMaNhanVien.TabIndex = 4;
            this.txtMaNhanVien.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // lbThongBaoLoi
            // 
            this.lbThongBaoLoi.AutoSize = true;
            this.lbThongBaoLoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbThongBaoLoi.ForeColor = System.Drawing.Color.Red;
            this.lbThongBaoLoi.Location = new System.Drawing.Point(250, 330);
            this.lbThongBaoLoi.Name = "lbThongBaoLoi";
            this.lbThongBaoLoi.Size = new System.Drawing.Size(0, 15);
            this.lbThongBaoLoi.TabIndex = 5;
            // 
            // btnDangKy
            // 
            this.btnDangKy.BorderRadius = 8;
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(250, 360);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(300, 45);
            this.btnDangKy.TabIndex = 6;
            this.btnDangKy.Text = "Đăng Ký";
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.FlatAppearance.BorderSize = 0;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnQuayLai.Location = new System.Drawing.Point(350, 420);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(120, 30);
            this.btnQuayLai.TabIndex = 7;
            this.btnQuayLai.Text = "Quay lại đăng nhập";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // FormDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "FormDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Ký - Hệ thống PNJ";
            this.Load += new System.EventHandler(this.FormDangKy_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label labelTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtNhapLaiMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtMaNhanVien;
        private System.Windows.Forms.Label lbThongBaoLoi;
        private Guna.UI2.WinForms.Guna2Button btnDangKy;
        private System.Windows.Forms.Button btnQuayLai;
    }
}
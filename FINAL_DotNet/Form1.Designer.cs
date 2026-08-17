namespace FINAL_DotNet
{
    partial class Form1
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
            this.txtTenDangNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnChuyenDangKy = new System.Windows.Forms.Button();
            this.lbThongBaoLoi = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.BorderRadius = 8;
            this.txtTenDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDangNhap.DefaultText = "";
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDangNhap.Location = new System.Drawing.Point(250, 139);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.PlaceholderText = "Tên đăng nhập";
            this.txtTenDangNhap.SelectedText = "";
            this.txtTenDangNhap.Size = new System.Drawing.Size(300, 40);
            this.txtTenDangNhap.TabIndex = 1;
            this.txtTenDangNhap.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderRadius = 8;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhau.Location = new System.Drawing.Point(250, 199);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.PlaceholderText = "Mật khẩu";
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.Size = new System.Drawing.Size(300, 40);
            this.txtMatKhau.TabIndex = 2;
            this.txtMatKhau.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BorderRadius = 8;
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(250, 269);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(300, 45);
            this.btnDangNhap.TabIndex = 3;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labelTitle.Location = new System.Drawing.Point(310, 30);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(181, 37);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "ĐĂNG NHẬP";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.btnChuyenDangKy);
            this.guna2Panel1.Controls.Add(this.lbThongBaoLoi);
            this.guna2Panel1.Controls.Add(this.btnDangNhap);
            this.guna2Panel1.Controls.Add(this.txtMatKhau);
            this.guna2Panel1.Controls.Add(this.txtTenDangNhap);
            this.guna2Panel1.Controls.Add(this.labelTitle);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(800, 450);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnChuyenDangKy
            // 
            this.btnChuyenDangKy.FlatAppearance.BorderSize = 0;
            this.btnChuyenDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuyenDangKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChuyenDangKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnChuyenDangKy.Location = new System.Drawing.Point(302, 320);
            this.btnChuyenDangKy.Name = "btnChuyenDangKy";
            this.btnChuyenDangKy.Size = new System.Drawing.Size(180, 30);
            this.btnChuyenDangKy.TabIndex = 6;
            this.btnChuyenDangKy.Text = "Chưa có tài khoản? Đăng ký";
            this.btnChuyenDangKy.UseVisualStyleBackColor = true;
            this.btnChuyenDangKy.Click += new System.EventHandler(this.btnChuyenDangKy_Click);
            // 
            // lbThongBaoLoi
            // 
            this.lbThongBaoLoi.AutoSize = true;
            this.lbThongBaoLoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbThongBaoLoi.ForeColor = System.Drawing.Color.Red;
            this.lbThongBaoLoi.Location = new System.Drawing.Point(250, 246);
            this.lbThongBaoLoi.Name = "lbThongBaoLoi";
            this.lbThongBaoLoi.Size = new System.Drawing.Size(0, 15);
            this.lbThongBaoLoi.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập - Hệ thống PNJ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label labelTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private System.Windows.Forms.Label lbThongBaoLoi;
        private System.Windows.Forms.Button btnChuyenDangKy;
    }
}

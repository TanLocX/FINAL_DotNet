using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    internal sealed class FormDoiMatKhau : Form
    {
        private readonly int taiKhoanId;
        private readonly TextBox txtMatKhauMoi;
        private readonly TextBox txtNhapLaiMatKhau;
        private readonly Label lblThongBao;

        public FormDoiMatKhau(int taiKhoanId)
        {
            this.taiKhoanId = taiKhoanId;

            Text = "Đổi mật khẩu - Hệ thống PNJ";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(420, 245);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblTieuDe = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(28, 22),
                Text = "ĐỔI MẬT KHẨU BẮT BUỘC"
            };

            var lblMatKhauMoi = new Label
            {
                AutoSize = true,
                Location = new Point(30, 73),
                Text = "Mật khẩu mới"
            };

            txtMatKhauMoi = new TextBox
            {
                Location = new Point(155, 69),
                PasswordChar = '*',
                Size = new Size(225, 25)
            };

            var lblNhapLai = new Label
            {
                AutoSize = true,
                Location = new Point(30, 112),
                Text = "Nhập lại mật khẩu"
            };

            txtNhapLaiMatKhau = new TextBox
            {
                Location = new Point(155, 108),
                PasswordChar = '*',
                Size = new Size(225, 25)
            };

            lblThongBao = new Label
            {
                AutoEllipsis = true,
                ForeColor = Color.Firebrick,
                Location = new Point(30, 145),
                Size = new Size(350, 32)
            };

            var btnLuu = new Button
            {
                Location = new Point(195, 185),
                Size = new Size(90, 32),
                Text = "Lưu"
            };
            btnLuu.Click += btnLuu_Click;

            var btnHuy = new Button
            {
                DialogResult = DialogResult.Cancel,
                Location = new Point(290, 185),
                Size = new Size(90, 32),
                Text = "Hủy"
            };

            Controls.Add(lblTieuDe);
            Controls.Add(lblMatKhauMoi);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(lblNhapLai);
            Controls.Add(txtNhapLaiMatKhau);
            Controls.Add(lblThongBao);
            Controls.Add(btnLuu);
            Controls.Add(btnHuy);

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string matKhauMoi = txtMatKhauMoi.Text;

            if (matKhauMoi.Length < 8)
            {
                lblThongBao.Text = "Mật khẩu phải có ít nhất 8 ký tự.";
                return;
            }

            if (matKhauMoi != txtNhapLaiMatKhau.Text)
            {
                lblThongBao.Text = "Mật khẩu nhập lại không khớp.";
                return;
            }

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var taiKhoan = db.TaiKhoans.SingleOrDefault(tk =>
                        tk.TaiKhoanId == taiKhoanId && tk.DangHoatDong);

                    if (taiKhoan == null)
                    {
                        lblThongBao.Text = "Tài khoản không còn hoạt động.";
                        return;
                    }

                    taiKhoan.MatKhauHash = BCrypt.Net.BCrypt.HashPassword(matKhauMoi);
                    taiKhoan.PhaiDoiMatKhau = false;
                    db.SaveChanges();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                lblThongBao.Text = "Không thể cập nhật mật khẩu. Hãy thử lại.";
            }
        }
    }
}

using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = string.Empty;

            // Tài khoản chỉ do quản trị viên cấp, không đăng ký công khai.
            btnChuyenDangKy.Visible = false;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = string.Empty;
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                lbThongBaoLoi.Text = "* Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";
                return;
            }

            btnDangNhap.Enabled = false;

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var taiKhoan = db.TaiKhoans
                        .Include(tk => tk.NhanVien)
                        .FirstOrDefault(tk =>
                            tk.TenDangNhap == tenDangNhap &&
                            tk.DangHoatDong &&
                            tk.NhanVien.DangLamViec);

                    if (taiKhoan == null || !KiemTraMatKhau(matKhau, taiKhoan.MatKhauHash))
                    {
                        lbThongBaoLoi.Text = "* Tên đăng nhập hoặc mật khẩu không chính xác!";
                        return;
                    }

                    if (taiKhoan.PhaiDoiMatKhau)
                    {
                        using (var formDoiMatKhau = new FormDoiMatKhau(taiKhoan.TaiKhoanId))
                        {
                            if (formDoiMatKhau.ShowDialog(this) != DialogResult.OK)
                            {
                                lbThongBaoLoi.Text = "* Bạn cần đổi mật khẩu trước khi tiếp tục.";
                                return;
                            }
                        }
                    }

                    CurrentUserSession.BatDau(
                        taiKhoan.TaiKhoanId,
                        taiKhoan.NhanVienId,
                        taiKhoan.TenDangNhap,
                        taiKhoan.NhanVien.HoTen,
                        taiKhoan.VaiTro);
                }
            }
            catch (Exception)
            {
                lbThongBaoLoi.Text = "* Không thể kết nối CSDL. Hãy kiểm tra Radmin VPN và cấu hình kết nối.";
                return;
            }
            finally
            {
                btnDangNhap.Enabled = true;
            }

            MoManHinhChinh();
        }

        private void MoManHinhChinh()
        {
            Hide();

            bool dangXuat = false;
            try
            {
                using (var formChinh = new FrmMain())
                {
                    formChinh.ShowDialog();
                    dangXuat = formChinh.DaYeuCauDangXuat;
                }
            }
            finally
            {
                CurrentUserSession.KetThuc();
                txtMatKhau.Clear();
            }

            if (dangXuat)
            {
                lbThongBaoLoi.Text = string.Empty;
                Show();
                Activate();
                txtTenDangNhap.Focus();
            }
            else
            {
                Close();
            }
        }

        private static bool KiemTraMatKhau(string matKhau, string matKhauHash)
        {
            if (string.IsNullOrWhiteSpace(matKhauHash))
            {
                return false;
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(matKhau, matKhauHash);
            }
            catch
            {
                return false;
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = string.Empty;
        }

        private void btnChuyenDangKy_Click(object sender, EventArgs e)
        {
            using (var formDangKy = new FormDangKy())
            {
                formDangKy.ShowDialog(this);
            }
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Vui lòng liên hệ quản trị viên để được đặt lại mật khẩu.",
                "Quên mật khẩu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '*')
            {
                txtMatKhau.PasswordChar = '\0';
                btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(94, 148, 255);
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
                btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(125, 137, 149);
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}

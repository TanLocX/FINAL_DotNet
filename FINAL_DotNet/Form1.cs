using System;
using System.Linq;
using System.Windows.Forms;
// Thêm thư viện BCrypt vào đây:
using BCrypt.Net;

namespace FINAL_DotNet
{
    public partial class Form1 : Form
    {
        QL_CuaHangDaQuy_PNJEntities db = new QL_CuaHangDaQuy_PNJEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = "";
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = "";
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lbThongBaoLoi.Text = "* Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";
                return;
            }

            try
            {
                // BƯỚC A: CHỈ TÌM TÀI KHOẢN THEO TÊN ĐĂNG NHẬP

                var taiKhoan = db.TaiKhoans.FirstOrDefault(tk => tk.TenDangNhap == username && tk.TrangThai == true);

                if (taiKhoan != null)
                {
                    // BƯỚC B: SỬ DỤNG BCRYPT ĐỂ XÁC THỰC MẬT KHẨU
                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, taiKhoan.MatKhau);

                    if (isPasswordCorrect)
                    {
                        string tenNhanVien = taiKhoan.NhanVien.HoTen;
                        MessageBox.Show($"Đăng nhập thành công!\nChào mừng {tenNhanVien} (Quyền: {taiKhoan.Quyen})",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // TODO: Mở form chính ở đây
                    }
                    else
                    {
                        // Sai mật khẩu
                        lbThongBaoLoi.Text = "* Tên đăng nhập hoặc mật khẩu không chính xác!";
                    }
                }
                else
                {
                    // Không tìm thấy tên đăng nhập hoặc tài khoản bị khóa
                    lbThongBaoLoi.Text = "* Tên đăng nhập hoặc mật khẩu không chính xác!";
                }
            }
            catch (Exception ex)
            {
                lbThongBaoLoi.Text = "* Lỗi kết nối CSDL: " + ex.Message;
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = "";
        }

        private void btnChuyenDangKy_Click(object sender, EventArgs e)
        {
            FormDangKy frm = new FormDangKy();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '*')
            {
                // Đang ẩn → hiện mật khẩu
                txtMatKhau.PasswordChar = '\0';
                btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(94, 148, 255); // xanh = đang hiện
            }
            else
            {
                // Đang hiện → ẩn mật khẩu
                txtMatKhau.PasswordChar = '*';
                btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(125, 137, 149); // xám = đang ẩn
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
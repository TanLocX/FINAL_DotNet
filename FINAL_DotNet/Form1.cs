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
            label1.Text = "";
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                label1.Text = "* Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";
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
                        label1.Text = "* Tên đăng nhập hoặc mật khẩu không chính xác!";
                    }
                }
                else
                {
                    // Không tìm thấy tên đăng nhập hoặc tài khoản bị khóa
                    label1.Text = "* Tên đăng nhập hoặc mật khẩu không chính xác!";
                }
            }
            catch (Exception ex)
            {
                label1.Text = "* Lỗi kết nối CSDL: " + ex.Message;
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        private void btnChuyenDangKy_Click(object sender, EventArgs e)
        {
            FormDangKy frm = new FormDangKy();
            frm.ShowDialog();
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
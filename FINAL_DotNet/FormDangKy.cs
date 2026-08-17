using System;
using System.Linq;
using System.Windows.Forms;
using BCrypt.Net;

namespace FINAL_DotNet
{
    public partial class FormDangKy : Form
    {
        QL_CuaHangDaQuy_PNJEntities db = new QL_CuaHangDaQuy_PNJEntities();

        public FormDangKy()
        {
            InitializeComponent();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = "";
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = "";
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();
            string confirmPassword = txtNhapLaiMatKhau.Text.Trim();
            string maNhanVien = txtMaNhanVien.Text.Trim();

            // 1. Kiểm tra đầu vào
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(maNhanVien))
            {
                lbThongBaoLoi.Text = "* Vui lòng điền đầy đủ tất cả các trường!";
                return;
            }

            if (password != confirmPassword)
            {
                lbThongBaoLoi.Text = "* Mật khẩu nhập lại không khớp!";
                return;
            }

            try
            {
                // 2. Kiểm tra trùng tên đăng nhập
                bool isExist = db.TaiKhoans.Any(tk => tk.TenDangNhap == username);
                if (isExist)
                {
                    lbThongBaoLoi.Text = "* Tên đăng nhập này đã tồn tại!";
                    return;
                }

                // 3. Kiểm tra Mã nhân viên có tồn tại trong bảng NhanVien chưa
                bool nVienExist = db.NhanViens.Any(nv => nv.MaNhanVien == maNhanVien);
                if (!nVienExist)
                {
                    lbThongBaoLoi.Text = "* Mã nhân viên không tồn tại trong hệ thống!";
                    return;
                }

                // Kiểm tra xem Nhân viên này đã có tài khoản chưa (mỗi NV 1 tài khoản)
                bool tkNhanVienExist = db.TaiKhoans.Any(tk => tk.MaNhanVien == maNhanVien);
                if (tkNhanVienExist)
                {
                    lbThongBaoLoi.Text = "* Nhân viên này đã được cấp tài khoản!";
                    return;
                }

                // 4. Mã hóa mật khẩu và tạo tài khoản
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                TaiKhoan taiKhoanMoi = new TaiKhoan()
                {
                    TenDangNhap = username,
                    MatKhau = hashedPassword,
                    MaNhanVien = maNhanVien,
                    Quyen = "User", // Quyền mặc định
                    TrangThai = true // Kích hoạt ngay
                };

                db.TaiKhoans.Add(taiKhoanMoi);
                db.SaveChanges();

                MessageBox.Show("Đăng ký tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form đăng ký, tự động trở về form đăng nhập
            }
            catch (Exception ex)
            {
                lbThongBaoLoi.Text = "* Lỗi kết nối CSDL: " + ex.Message;
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = "";
        }
    }
}

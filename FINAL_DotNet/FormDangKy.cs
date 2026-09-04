using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FormDangKy : Form
    {
        private Image anhNenHienTai;
        private bool isUpdatingLayout = false;
        private int lastCropWidth = -1;
        private int lastCropHeight = -1;

        public FormDangKy()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Resize += (s, e) => ThucHienCapNhat();
            this.FormClosed += (s, e) => {
                anhNenHienTai?.Dispose();
                anhNenHienTai = null;
                lastCropWidth = -1;
                lastCropHeight = -1;
            };
        }

        private void ThucHienCapNhat()
        {
            if (isUpdatingLayout) return;
            try
            {
                isUpdatingLayout = true;
                CapNhatAnhNenAutoCrop();
            }
            finally
            {
                isUpdatingLayout = false;
            }
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = string.Empty;
            ThucHienCapNhat();
        }

        private void CapNhatAnhNenAutoCrop()
        {
            if (guna2Panel1 == null || guna2Panel1.ClientSize.Width <= 0 || guna2Panel1.ClientSize.Height <= 0) return;

            int targetW = guna2Panel1.ClientSize.Width;
            int targetH = guna2Panel1.ClientSize.Height;

            if (targetW == lastCropWidth && targetH == lastCropHeight && anhNenHienTai != null)
            {
                return;
            }

            Image rawBg = Properties.Resources._99;
            if (rawBg == null) return;

            Bitmap cropped = ImageOptimizationHelper.CreateCoverCroppedImage(rawBg, targetW, targetH);
            if (cropped != null)
            {
                Image oldImg = anhNenHienTai;
                anhNenHienTai = cropped;
                lastCropWidth = targetW;
                lastCropHeight = targetH;
                guna2Panel1.BackgroundImageLayout = ImageLayout.None;
                guna2Panel1.BackgroundImage = anhNenHienTai;
                oldImg?.Dispose();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = string.Empty;
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string nhapLaiMatKhau = txtNhapLaiMatKhau.Text;
            string maNhanVien = txtMaNhanVien.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrEmpty(matKhau) ||
                string.IsNullOrEmpty(nhapLaiMatKhau) ||
                string.IsNullOrWhiteSpace(maNhanVien))
            {
                lbThongBaoLoi.Text = "* Vui lòng điền đầy đủ tất cả các trường!";
                return;
            }

            if (matKhau.Length < 8)
            {
                lbThongBaoLoi.Text = "* Mật khẩu phải có ít nhất 8 ký tự!";
                return;
            }

            if (matKhau != nhapLaiMatKhau)
            {
                lbThongBaoLoi.Text = "* Mật khẩu nhập lại không khớp!";
                return;
            }

            int nhanVienId;
            if (!ThuChuyenNhanVienId(maNhanVien, out nhanVienId))
            {
                lbThongBaoLoi.Text = "* Mã nhân viên không hợp lệ (ví dụ: NV000001)!";
                return;
            }

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (db.TaiKhoans.Any(tk => tk.TenDangNhap == tenDangNhap))
                    {
                        lbThongBaoLoi.Text = "* Tên đăng nhập này đã tồn tại!";
                        return;
                    }

                    var nhanVien = db.NhanViens.FirstOrDefault(nv =>
                        nv.NhanVienId == nhanVienId && nv.DangLamViec);

                    if (nhanVien == null)
                    {
                        lbThongBaoLoi.Text = "* Không tìm thấy nhân viên đang làm việc!";
                        return;
                    }

                    if (db.TaiKhoans.Any(tk => tk.NhanVienId == nhanVienId))
                    {
                        lbThongBaoLoi.Text = "* Nhân viên này đã được cấp tài khoản!";
                        return;
                    }

                    db.TaiKhoans.Add(new TaiKhoan
                    {
                        NhanVienId = nhanVienId,
                        TenDangNhap = tenDangNhap,
                        MatKhauHash = BCrypt.Net.BCrypt.HashPassword(matKhau),
                        VaiTro = "NHANVIEN",
                        PhaiDoiMatKhau = true,
                        DangHoatDong = true
                    });

                    db.SaveChanges();
                }

                MessageBox.Show(
                    "Cấp tài khoản thành công. Nhân viên phải đổi mật khẩu ở lần đăng nhập đầu tiên.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch (Exception)
            {
                lbThongBaoLoi.Text = "* Không thể kết nối CSDL. Hãy kiểm tra cấu hình kết nối.";
            }
        }

        private static bool ThuChuyenNhanVienId(string maNhanVien, out int nhanVienId)
        {
            string giaTri = maNhanVien.Trim();
            if (giaTri.StartsWith("NV", StringComparison.OrdinalIgnoreCase))
            {
                giaTri = giaTri.Substring(2);
            }

            return int.TryParse(giaTri, out nhanVienId) && nhanVienId > 0;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = string.Empty;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

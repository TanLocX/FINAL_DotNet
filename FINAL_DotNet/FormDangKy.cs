using System;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FormDangKy : Form
    {
        private static readonly Regex MauTenDangNhap =
            new Regex("^[A-Za-z0-9._-]{3,50}$", RegexOptions.Compiled);
        private static readonly Regex MauMaNhanVien =
            new Regex("^NV([0-9]{6})$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
            string soDienThoai = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrEmpty(matKhau) ||
                string.IsNullOrEmpty(nhapLaiMatKhau) ||
                string.IsNullOrWhiteSpace(maNhanVien) ||
                string.IsNullOrWhiteSpace(soDienThoai))
            {
                lbThongBaoLoi.Text = "* Vui lòng điền đầy đủ tất cả các trường!";
                return;
            }

            if (!MauTenDangNhap.IsMatch(tenDangNhap))
            {
                lbThongBaoLoi.Text = "* Tên đăng nhập dài 3–50 ký tự và chỉ gồm chữ, số, ., _ hoặc -!";
                return;
            }

            if (matKhau.Length < 8)
            {
                lbThongBaoLoi.Text = "* Mật khẩu phải có ít nhất 8 ký tự!";
                return;
            }

            if (Encoding.UTF8.GetByteCount(matKhau) > 72)
            {
                lbThongBaoLoi.Text = "* Mật khẩu không được vượt quá 72 byte UTF-8!";
                return;
            }

            if (matKhau != nhapLaiMatKhau)
            {
                lbThongBaoLoi.Text = "* Mật khẩu nhập lại không khớp!";
                return;
            }

            if (soDienThoai.Length < 9 || soDienThoai.Length > 15 ||
                soDienThoai.Any(kyTu => !char.IsDigit(kyTu)))
            {
                lbThongBaoLoi.Text = "* Số điện thoại phải gồm từ 9 đến 15 chữ số!";
                return;
            }

            int nhanVienId;
            if (!ThuChuyenNhanVienId(maNhanVien, out nhanVienId))
            {
                lbThongBaoLoi.Text = "* Mã nhân viên không hợp lệ (ví dụ: NV000001)!";
                return;
            }

            btnDangKy.Enabled = false;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var nhanVien = db.NhanViens.FirstOrDefault(nv =>
                        nv.NhanVienId == nhanVienId &&
                        nv.DangLamViec &&
                        nv.SoDienThoai == soDienThoai);

                    if (nhanVien == null)
                    {
                        lbThongBaoLoi.Text = "* Mã nhân viên hoặc số điện thoại không khớp hồ sơ đang làm việc!";
                        return;
                    }

                    if (db.TaiKhoans.Any(tk => tk.TenDangNhap == tenDangNhap))
                    {
                        lbThongBaoLoi.Text = "* Tên đăng nhập này đã tồn tại!";
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
                        MatKhauHash = BCrypt.Net.BCrypt.HashPassword(matKhau, 11),
                        VaiTro = "NHANVIEN",
                        PhaiDoiMatKhau = false,
                        DangHoatDong = true
                    });

                    db.SaveChanges();
                }

                MessageBox.Show(
                    "Đăng ký tài khoản thành công. Bạn có thể đăng nhập ngay.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch (DbUpdateException)
            {
                lbThongBaoLoi.Text = "* Không thể đăng ký. Nhân viên hoặc tên đăng nhập có thể đã được sử dụng.";
            }
            catch (Exception)
            {
                lbThongBaoLoi.Text = "* Không thể đăng ký. Hãy kiểm tra kết nối CSDL và thử lại.";
            }
            finally
            {
                btnDangKy.Enabled = true;
            }
        }

        private static bool ThuChuyenNhanVienId(string maNhanVien, out int nhanVienId)
        {
            nhanVienId = 0;
            Match ketQua = MauMaNhanVien.Match(maNhanVien.Trim());
            return ketQua.Success &&
                   int.TryParse(ketQua.Groups[1].Value, out nhanVienId) &&
                   nhanVienId > 0;
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

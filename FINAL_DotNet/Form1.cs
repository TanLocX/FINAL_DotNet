using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class Form1 : Form
    {
        private Image anhNenHienTai;
        private bool isUpdatingLayout = false;
        private int lastCropWidth = -1;
        private int lastCropHeight = -1;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Resize += Form1_Resize;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            anhNenHienTai?.Dispose();
            anhNenHienTai = null;
            lastCropWidth = -1;
            lastCropHeight = -1;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (isUpdatingLayout) return;
            try
            {
                isUpdatingLayout = true;
                CapNhatAnhNenAutoCrop();
                CanChinhViTriDangNhap();
            }
            finally
            {
                isUpdatingLayout = false;
            }
        }

        private void CapNhatAnhNenAutoCrop()
        {
            if (pnlNen == null || pnlNen.ClientSize.Width <= 0 || pnlNen.ClientSize.Height <= 0) return;

            int targetW = pnlNen.ClientSize.Width;
            int targetH = pnlNen.ClientSize.Height;

            if (targetW == lastCropWidth && targetH == lastCropHeight && anhNenHienTai != null)
            {
                return;
            }

            Image rawBg = Properties.Resources.Background;
            if (rawBg == null) return;

            Bitmap cropped = ImageOptimizationHelper.CreateCoverCroppedImage(rawBg, targetW, targetH);
            if (cropped != null)
            {
                Image oldImg = anhNenHienTai;
                anhNenHienTai = cropped;
                lastCropWidth = targetW;
                lastCropHeight = targetH;
                pnlNen.BackgroundImageLayout = ImageLayout.None;
                pnlNen.BackgroundImage = anhNenHienTai;
                oldImg?.Dispose();
            }
        }

        private void CanChinhViTriDangNhap()
        {
            if (pnlDangNhap == null || pnlNen == null) return;

            int rightMargin = Math.Max(40, (int)(pnlNen.ClientSize.Width * 0.08));
            int left = Math.Max(450, pnlNen.ClientSize.Width - pnlDangNhap.Width - rightMargin);

            pnlDangNhap.Left = left;

            if (guna2Panel4 != null) guna2Panel4.Left = left;
            if (guna2Panel2 != null) guna2Panel2.Left = left + (pnlDangNhap.Width - guna2Panel2.Width) / 2;
            if (guna2Panel1 != null) guna2Panel1.Left = left + (pnlDangNhap.Width - guna2Panel1.Width) / 2;
            if (label5 != null) label5.Left = left + (pnlDangNhap.Width - label5.Width) / 2;
            if (label9 != null) label9.Left = left + (pnlDangNhap.Width - label9.Width) / 2;
            if (label6 != null) label6.Left = left + (pnlDangNhap.Width - label6.Width) / 2;

            int totalStackHeight = 175 + pnlDangNhap.Height;
            int stackTop = Math.Max(12, (pnlNen.ClientSize.Height - totalStackHeight) / 2);

            if (guna2Panel1 != null) guna2Panel1.Top = stackTop;
            if (label5 != null) label5.Top = stackTop + 54;
            if (guna2Panel4 != null) guna2Panel4.Top = stackTop + 64;
            if (guna2Panel2 != null) guna2Panel2.Top = stackTop + 82;
            if (label9 != null) label9.Top = stackTop + 98;
            if (label6 != null) label6.Top = stackTop + 144;
            pnlDangNhap.Top = stackTop + 175;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbThongBaoLoi.Text = string.Empty;

            // Tài khoản chỉ do quản trị viên cấp, không đăng ký công khai.
            btnChuyenDangKy.Visible = false;

            if (!isUpdatingLayout)
            {
                try
                {
                    isUpdatingLayout = true;
                    CapNhatAnhNenAutoCrop();
                    CanChinhViTriDangNhap();
                }
                finally
                {
                    isUpdatingLayout = false;
                }
            }
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

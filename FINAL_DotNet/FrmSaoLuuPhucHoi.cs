using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmSaoLuuPhucHoi : Form
    {
        private ThongTinMayChuSaoLuu thongTinMayChu;
        private bool dangXuLy;

        public FrmSaoLuuPhucHoi()
        {
            InitializeComponent();
            LuxuryDarkGoldTheme.Apply(this);
        }

        private async void FrmSaoLuuPhucHoi_Load(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri())
            {
                BeginInvoke(new Action(Close));
                return;
            }
            txtTenFileSaoLuu.Text = SaoLuuPhucHoiService.TaoTenFileSaoLuu();
            await TaiThongTinVaLichSu();
        }

        private bool KiemTraQuyenQuanTri()
        {
            bool hopLe = CurrentUserSession.DaDangNhap && CurrentUserSession.HienTai.LaQuanTriVien;
            if (!hopLe)
                MessageBox.Show("Chỉ quản trị viên được sử dụng chức năng Backup/Restore.", "Không đủ quyền",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return hopLe;
        }

        private async Task TaiThongTinVaLichSu()
        {
            if (dangXuLy) return;
            DatTrangThaiXuLy(true, "Đang kết nối SQL Server và tải lịch sử sao lưu...");
            try
            {
                KetQuaTaiDuLieu ketQua = await Task.Run(() => new KetQuaTaiDuLieu
                {
                    ThongTin = SaoLuuPhucHoiService.LayThongTinMayChu()
                });
                try
                {
                    ketQua.LichSu = await Task.Run(() => SaoLuuPhucHoiService.LayLichSuSaoLuu());
                }
                catch (Exception ex)
                {
                    ketQua.LichSu = new List<BanSaoLuuHienThi>();
                    ketQua.CanhBaoLichSu = "Không đọc được lịch sử msdb: " + LayThongBaoLoi(ex);
                }
                thongTinMayChu = ketQua.ThongTin;
                HienThiThongTinMayChu();
                HienThiLichSu(ketQua.LichSu);
                lblTienTrinh.Text = string.IsNullOrWhiteSpace(ketQua.CanhBaoLichSu)
                    ? "Đã tải thông tin lúc " + DateTime.Now.ToString("HH:mm:ss")
                    : ketQua.CanhBaoLichSu;
            }
            catch (Exception ex)
            {
                lblTienTrinh.Text = "Không thể kết nối SQL Server.";
                MessageBox.Show("Không thể tải thông tin Backup/Restore. " + LayThongBaoLoi(ex),
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DatTrangThaiXuLy(false, lblTienTrinh.Text);
            }
        }

        private void HienThiThongTinMayChu()
        {
            lblMayChu.Text = thongTinMayChu.TenMayChu;
            lblCoSoDuLieu.Text = thongTinMayChu.TenCoSoDuLieu;
            lblPhienBan.Text = "SQL Server " + thongTinMayChu.PhienBanSqlServer;
            lblQuyen.Text = "Sao lưu: " + (thongTinMayChu.CoQuyenSaoLuu ? "Có quyền" : "Thiếu quyền") +
                            "  |  Phục hồi: " + (thongTinMayChu.CoQuyenPhucHoi ? "Có quyền" : "Thiếu quyền");
            lblQuyen.ForeColor = thongTinMayChu.CoQuyenSaoLuu && thongTinMayChu.CoQuyenPhucHoi
                ? Color.FromArgb(35, 125, 96) : Color.Firebrick;
            if (!string.IsNullOrWhiteSpace(thongTinMayChu.ThuMucSaoLuuMacDinh) && string.IsNullOrWhiteSpace(txtThuMucSaoLuu.Text))
                txtThuMucSaoLuu.Text = thongTinMayChu.ThuMucSaoLuuMacDinh;
            btnSaoLuu.Enabled = thongTinMayChu.CoQuyenSaoLuu;
            btnPhucHoi.Enabled = thongTinMayChu.CoQuyenPhucHoi;
        }

        private void HienThiLichSu(List<BanSaoLuuHienThi> danhSach)
        {
            dgvLichSu.DataSource = danhSach;
            lblSoBanSao.Text = danhSach.Count + " bản sao đầy đủ gần nhất";
            dgvLichSu.ClearSelection();
        }

        private async void btnSaoLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri() || dangXuLy) return;
            string thuMuc = txtThuMucSaoLuu.Text.Trim();
            string tenFile = txtTenFileSaoLuu.Text.Trim();
            if (string.IsNullOrWhiteSpace(thuMuc) || string.IsNullOrWhiteSpace(tenFile))
            {
                MessageBox.Show("Hãy nhập thư mục trên máy chủ SQL và tên file sao lưu.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IProgress<string> progress = new Progress<string>(noiDung => lblTienTrinh.Text = noiDung);
            DatTrangThaiXuLy(true, "Đang chuẩn bị sao lưu...");
            try
            {
                string duongDan = await Task.Run(() => SaoLuuPhucHoiService.TaoSaoLuu(thuMuc, tenFile, progress.Report));
                lblTienTrinh.Text = "Sao lưu thành công: " + duongDan;
                MessageBox.Show("Đã sao lưu và xác minh file:\n" + duongDan,
                    "Sao lưu thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenFileSaoLuu.Text = SaoLuuPhucHoiService.TaoTenFileSaoLuu();
                HienThiLichSu(await Task.Run(() => SaoLuuPhucHoiService.LayLichSuSaoLuu()));
            }
            catch (Exception ex)
            {
                lblTienTrinh.Text = "Sao lưu thất bại.";
                MessageBox.Show("Không thể sao lưu CSDL. " + LayThongBaoLoi(ex), "Lỗi sao lưu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DatTrangThaiXuLy(false, lblTienTrinh.Text);
            }
        }

        private async void btnPhucHoi_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri() || dangXuLy) return;
            string duongDan = txtDuongDanPhucHoi.Text.Trim();
            string thuMucAnToan = txtThuMucSaoLuu.Text.Trim();
            if (string.IsNullOrWhiteSpace(duongDan) || string.IsNullOrWhiteSpace(thuMucAnToan))
            {
                MessageBox.Show("Hãy chọn file .bak từ lịch sử và nhập thư mục tạo bản sao an toàn.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!XacNhanPhucHoi(duongDan)) return;

            IProgress<string> progress = new Progress<string>(noiDung => lblTienTrinh.Text = noiDung);
            DatTrangThaiXuLy(true, "Đang chuẩn bị phục hồi...");
            try
            {
                string banSaoAnToan = await Task.Run(() => SaoLuuPhucHoiService.PhucHoi(duongDan, thuMucAnToan, progress.Report));
                MessageBox.Show("Phục hồi CSDL thành công.\n\nBản sao trước phục hồi:\n" + banSaoAnToan +
                                "\n\nỨng dụng sẽ khởi động lại để nạp dữ liệu và phiên đăng nhập mới.",
                    "Phục hồi thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            catch (Exception ex)
            {
                lblTienTrinh.Text = "Phục hồi thất bại. CSDL đã được yêu cầu chuyển lại chế độ nhiều người dùng.";
                MessageBox.Show("Không thể phục hồi CSDL. " + LayThongBaoLoi(ex) +
                                "\n\nNếu CSDL vẫn ở trạng thái SINGLE_USER hoặc RESTORING, hãy kiểm tra bằng SSMS.",
                    "Lỗi phục hồi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DatTrangThaiXuLy(false, lblTienTrinh.Text);
            }
        }

        private bool XacNhanPhucHoi(string duongDan)
        {
            using (var dialog = new Form
            {
                Text = "Xác nhận phục hồi CSDL",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(560, 235),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F)
            })
            {
                var noiDung = new Label
                {
                    Location = new Point(18, 16), Size = new Size(524, 112),
                    ForeColor = Color.Firebrick,
                    Text = "CẢNH BÁO: Phục hồi sẽ ngắt toàn bộ kết nối và thay thế dữ liệu hiện tại.\n\n" +
                           "File: " + duongDan + "\n\nNhập PHUC HOI để tiếp tục."
                };
                var xacNhan = new TextBox { Location = new Point(18, 138), Size = new Size(524, 25) };
                var huy = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Location = new Point(344, 184), Size = new Size(92, 32) };
                var dongY = new Button { Text = "Phục hồi", DialogResult = DialogResult.OK, Location = new Point(446, 184), Size = new Size(96, 32), BackColor = Color.Firebrick, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                dongY.FlatAppearance.BorderSize = 0;
                dialog.Controls.AddRange(new Control[] { noiDung, xacNhan, huy, dongY });
                dialog.AcceptButton = dongY;
                dialog.CancelButton = huy;
                return dialog.ShowDialog(this) == DialogResult.OK &&
                       string.Equals(xacNhan.Text.Trim(), "PHUC HOI", StringComparison.OrdinalIgnoreCase);
            }
        }

        private void dgvLichSu_SelectionChanged(object sender, EventArgs e)
        {
            var item = dgvLichSu.CurrentRow?.DataBoundItem as BanSaoLuuHienThi;
            if (item != null) txtDuongDanPhucHoi.Text = item.DuongDan;
        }

        private async void btnTaiLai_Click(object sender, EventArgs e) => await TaiThongTinVaLichSu();
        private void btnTaoTenMoi_Click(object sender, EventArgs e) => txtTenFileSaoLuu.Text = SaoLuuPhucHoiService.TaoTenFileSaoLuu();

        private void DatTrangThaiXuLy(bool dangBan, string noiDung)
        {
            dangXuLy = dangBan;
            Cursor = dangBan ? Cursors.WaitCursor : Cursors.Default;
            btnTaiLai.Enabled = !dangBan;
            btnTaoTenMoi.Enabled = !dangBan;
            btnSaoLuu.Enabled = !dangBan && (thongTinMayChu?.CoQuyenSaoLuu ?? false);
            btnPhucHoi.Enabled = !dangBan && (thongTinMayChu?.CoQuyenPhucHoi ?? false);
            txtThuMucSaoLuu.Enabled = !dangBan;
            txtTenFileSaoLuu.Enabled = !dangBan;
            txtDuongDanPhucHoi.Enabled = !dangBan;
            dgvLichSu.Enabled = !dangBan;
            lblTienTrinh.Text = noiDung;
        }

        private static string LayThongBaoLoi(Exception exception)
        {
            Exception hienTai = exception;
            while (hienTai.InnerException != null) hienTai = hienTai.InnerException;
            return hienTai.Message;
        }

        private sealed class KetQuaTaiDuLieu
        {
            public ThongTinMayChuSaoLuu ThongTin { get; set; }
            public List<BanSaoLuuHienThi> LichSu { get; set; }
            public string CanhBaoLichSu { get; set; }
        }
    }
}

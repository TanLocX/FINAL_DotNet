using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
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
            dgvLichSu.AutoGenerateColumns = false;
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
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
            prgTienTrinh.Value = 0;
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
                prgTienTrinh.Value = 100;
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

            if (string.IsNullOrWhiteSpace(txtThuMucSaoLuu.Text))
            {
                string safeDir = !string.IsNullOrWhiteSpace(thongTinMayChu.ThuMucSaoLuuMacDinh)
                    ? thongTinMayChu.ThuMucSaoLuuMacDinh
                    : @"C:\PNJ_Backups";
                try
                {
                    if (!Directory.Exists(safeDir)) Directory.CreateDirectory(safeDir);
                }
                catch
                {
                    safeDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PNJ_Backups");
                    try
                    {
                        if (!Directory.Exists(safeDir)) Directory.CreateDirectory(safeDir);
                    }
                    catch
                    {
                        // Ignore directory creation errors if path is remote
                    }
                }
                txtThuMucSaoLuu.Text = safeDir;
            }

            btnSaoLuu.Enabled = thongTinMayChu.CoQuyenSaoLuu;
            btnPhucHoi.Enabled = thongTinMayChu.CoQuyenPhucHoi;
        }

        private void HienThiLichSu(List<BanSaoLuuHienThi> danhSach)
        {
            dgvLichSu.DataSource = danhSach;
            lblSoBanSao.Text = danhSach.Count + " bản sao gần nhất";
            if (danhSach != null && danhSach.Count > 0)
            {
                dgvLichSu.Rows[0].Selected = true;
                txtDuongDanPhucHoi.Text = danhSach[0].DuongDan;
            }
            else
            {
                dgvLichSu.ClearSelection();
            }
        }

        private void CapNhatTienTrinh(string noiDung)
        {
            lblTienTrinh.Text = noiDung;
            Match m = Regex.Match(noiDung, @"(\d+)\s*(?:percent|phần trăm|%)", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                int val;
                if (int.TryParse(m.Groups[1].Value, out val))
                {
                    prgTienTrinh.Value = Math.Max(0, Math.Min(100, val));
                }
            }
        }

        private void btnChonThuMuc_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục lưu trữ file sao lưu (.bak) trên máy chủ SQL";
                if (!string.IsNullOrWhiteSpace(txtThuMucSaoLuu.Text) && Directory.Exists(txtThuMucSaoLuu.Text))
                {
                    dialog.SelectedPath = txtThuMucSaoLuu.Text;
                }
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtThuMucSaoLuu.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnMoThuMuc_Click(object sender, EventArgs e)
        {
            string path = txtThuMucSaoLuu.Text.Trim();
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Chưa có đường dẫn thư mục sao lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (Directory.Exists(path))
                {
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("Thư mục không tồn tại trên máy hiện tại (hoặc nằm trên máy chủ SQL từ xa):\n" + path, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở thư mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonFilePhucHoi_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn file sao lưu CSDL (.bak) để phục hồi";
                dialog.Filter = "SQL Server Backup (*.bak)|*.bak|Tất cả file (*.*)|*.*";
                if (!string.IsNullOrWhiteSpace(txtThuMucSaoLuu.Text) && Directory.Exists(txtThuMucSaoLuu.Text))
                {
                    dialog.InitialDirectory = txtThuMucSaoLuu.Text;
                }
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtDuongDanPhucHoi.Text = dialog.FileName;
                }
            }
        }

        private async void btnXoaBanSao_Click(object sender, EventArgs e)
        {
            var item = dgvLichSu.CurrentRow?.DataBoundItem as BanSaoLuuHienThi;
            if (item == null || string.IsNullOrWhiteSpace(item.DuongDan))
            {
                MessageBox.Show("Hãy chọn 1 bản sao trong danh sách lịch sử để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa file sao lưu vật lý này khỏi ổ đĩa?\n\nFile: " + item.DuongDan,
                "Xác nhận xóa file backup",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            bool daXoa = await Task.Run(() => SaoLuuPhucHoiService.XoaBanSaoVatLy(item.DuongDan));
            if (daXoa)
            {
                lblTienTrinh.Text = "Đã xóa file sao lưu: " + item.DuongDan;
                MessageBox.Show("Đã xóa file sao lưu thành công khỏi ổ đĩa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await TaiThongTinVaLichSu();
            }
            else
            {
                MessageBox.Show("Không thể xóa file trực tiếp. File có thể không tồn tại trên ổ đĩa này, đang được SQL Server sử dụng hoặc nằm trên máy chủ từ xa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnSaoLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri() || dangXuLy) return;
            string thuMuc = txtThuMucSaoLuu.Text.Trim();
            string tenFile = txtTenFileSaoLuu.Text.Trim();
            bool nenDuLieu = chkNenBanSao.Checked;
            if (string.IsNullOrWhiteSpace(thuMuc) || string.IsNullOrWhiteSpace(tenFile))
            {
                MessageBox.Show("Hãy nhập thư mục trên máy chủ SQL và tên file sao lưu.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            prgTienTrinh.Value = 0;
            IProgress<string> progress = new Progress<string>(CapNhatTienTrinh);
            DatTrangThaiXuLy(true, "Đang chuẩn bị sao lưu...");
            try
            {
                string duongDan = await Task.Run(() => SaoLuuPhucHoiService.TaoSaoLuu(thuMuc, tenFile, nenDuLieu, progress.Report));
                prgTienTrinh.Value = 100;
                lblTienTrinh.Text = "Sao lưu thành công: " + duongDan;
                MessageBox.Show("Đã sao lưu và xác minh file:\n" + duongDan + (nenDuLieu ? "\n\n(Đã nén COMPRESSION - tiết kiệm dung lượng)" : ""),
                    "Sao lưu thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenFileSaoLuu.Text = SaoLuuPhucHoiService.TaoTenFileSaoLuu();
                HienThiLichSu(await Task.Run(() => SaoLuuPhucHoiService.LayLichSuSaoLuu()));
            }
            catch (Exception ex)
            {
                prgTienTrinh.Value = 0;
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
            bool nenDuLieu = chkNenBanSao.Checked;
            if (string.IsNullOrWhiteSpace(duongDan) || string.IsNullOrWhiteSpace(thuMucAnToan))
            {
                MessageBox.Show("Hãy chọn file .bak từ lịch sử và nhập thư mục tạo bản sao an toàn.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!XacNhanPhucHoi(duongDan)) return;

            prgTienTrinh.Value = 0;
            IProgress<string> progress = new Progress<string>(CapNhatTienTrinh);
            DatTrangThaiXuLy(true, "Đang chuẩn bị phục hồi...");
            try
            {
                string banSaoAnToan = await Task.Run(() => SaoLuuPhucHoiService.PhucHoi(duongDan, thuMucAnToan, nenDuLieu, progress.Report));
                prgTienTrinh.Value = 100;
                MessageBox.Show("Phục hồi CSDL thành công.\n\nBản sao trước phục hồi:\n" + banSaoAnToan +
                                "\n\nỨng dụng sẽ khởi động lại để nạp dữ liệu và phiên đăng nhập mới.",
                    "Phục hồi thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            catch (Exception ex)
            {
                prgTienTrinh.Value = 0;
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
            if (item != null && !string.IsNullOrWhiteSpace(item.DuongDan))
            {
                txtDuongDanPhucHoi.Text = item.DuongDan;
            }
        }

        private void dgvLichSu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var item = dgvLichSu.Rows[e.RowIndex].DataBoundItem as BanSaoLuuHienThi;
            if (item != null && !string.IsNullOrWhiteSpace(item.DuongDan))
            {
                txtDuongDanPhucHoi.Text = item.DuongDan;
            }
        }

        private async void btnTaiLai_Click(object sender, EventArgs e) => await TaiThongTinVaLichSu();
        private void btnTaoTenMoi_Click(object sender, EventArgs e) => txtTenFileSaoLuu.Text = SaoLuuPhucHoiService.TaoTenFileSaoLuu();

        private void DatTrangThaiXuLy(bool dangBan, string noiDung)
        {
            dangXuLy = dangBan;
            Cursor = dangBan ? Cursors.WaitCursor : Cursors.Default;
            btnTaiLai.Enabled = !dangBan;
            btnTaoTenMoi.Enabled = !dangBan;
            btnChonThuMuc.Enabled = !dangBan;
            btnMoThuMuc.Enabled = !dangBan;
            btnChonFilePhucHoi.Enabled = !dangBan;
            btnXoaBanSao.Enabled = !dangBan;
            chkNenBanSao.Enabled = !dangBan;
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

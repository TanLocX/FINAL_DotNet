using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmChatLieu : Form
    {
        private int? chatLieuDangChonId;
        private bool dangLamMoiBieuMau;

        public FrmChatLieu()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
            {
                return;
            }
            cboLocTrangThai.SelectedIndex = 0;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmChatLieu_Load(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }

            LamMoiBieuMau();
            dangLamMoiBieuMau = true;
            try
            {
                TaiDanhSach();
                dgvChatLieu.ClearSelection();
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }
        }

        private bool KiemTraQuyenQuanTri(bool hienThongBao)
        {
            bool coQuyen = CurrentUserSession.DaDangNhap && CurrentUserSession.HienTai.LaQuanTriVien;
            if (!coQuyen && hienThongBao)
            {
                MessageBox.Show(
                    "Chỉ quản trị viên được sử dụng chức năng quản lý chất liệu.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return coQuyen;
        }

        private void TaiDanhSach(int? chatLieuCanChonId = null)
        {
            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int trangThai = cboLocTrangThai.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<ChatLieu> truyVan = db.ChatLieux
                        .Include(cl => cl.ChiTietChatLieux)
                        .Include(cl => cl.ChiTietPhieuThuMuas)
                        .AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        truyVan = truyVan.Where(cl => cl.TenChatLieu.Contains(tuKhoa));
                    }

                    if (trangThai == 1)
                    {
                        truyVan = truyVan.Where(cl => cl.DangHoatDong);
                    }
                    else if (trangThai == 2)
                    {
                        truyVan = truyVan.Where(cl => !cl.DangHoatDong);
                    }

                    List<ChatLieuHienThi> danhSach = truyVan
                        .OrderByDescending(cl => cl.DangHoatDong)
                        .ThenBy(cl => cl.ChatLieuId)
                        .ToList()
                        .Select(cl => new ChatLieuHienThi(cl))
                        .ToList();

                    dgvChatLieu.DataSource = danhSach;
                    lblSoKetQua.Text = $"{danhSach.Count} chất liệu";
                }

                if (chatLieuCanChonId.HasValue)
                {
                    ChonDong(chatLieuCanChonId.Value);
                }
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách chất liệu. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void ChonDong(int chatLieuId)
        {
            foreach (DataGridViewRow row in dgvChatLieu.Rows)
            {
                var item = row.DataBoundItem as ChatLieuHienThi;
                if (item?.ChatLieuId != chatLieuId)
                {
                    continue;
                }

                row.Selected = true;
                dgvChatLieu.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void dgvChatLieu_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoiBieuMau)
            {
                return;
            }

            var item = dgvChatLieu.CurrentRow?.DataBoundItem as ChatLieuHienThi;
            if (item == null)
            {
                return;
            }

            chatLieuDangChonId = item.ChatLieuId;
            txtMaChatLieu.Text = item.MaChatLieu;
            txtTenChatLieu.Text = item.TenChatLieu;
            numGiaMuaVao.Value = GioiHanGia(item.GiaMuaVao, numGiaMuaVao.Maximum);
            numGiaBanRa.Value = GioiHanGia(item.GiaBanRa, numGiaBanRa.Maximum);
            chkDangHoatDong.Checked = item.DangHoatDong;
            btnXoaHoacTrangThai.Text = item.DangHoatDong
                ? (item.SoThamChieu > 0 ? "Ngừng hoạt động" : "Xóa chất liệu")
                : "Khôi phục";
            lblThongBao.Text = string.Empty;
        }

        private static decimal GioiHanGia(decimal giaTri, decimal giaTriToiDa)
        {
            if (giaTri < 0) return 0;
            return giaTri > giaTriToiDa ? giaTriToiDa : giaTri;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            TaiDanhSach();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            cboLocTrangThai.SelectedIndex = 0;
            TaiDanhSach();
            LamMoiBieuMau();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true)) return;

            ThongTinChatLieuNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu)) return;

            try
            {
                int chatLieuMoiId;
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (TenChatLieuDaTonTai(db, duLieu.TenChatLieu, null))
                    {
                        HienThiLoi("Tên chất liệu đã tồn tại.");
                        return;
                    }

                    var chatLieu = new ChatLieu
                    {
                        TenChatLieu = duLieu.TenChatLieu,
                        GiaMuaVao = duLieu.GiaMuaVao,
                        GiaBanRa = duLieu.GiaBanRa,
                        DangHoatDong = true
                    };
                    db.ChatLieux.Add(chatLieu);
                    db.SaveChanges();
                    chatLieuMoiId = chatLieu.ChatLieuId;
                }

                TaiDanhSach(chatLieuMoiId);
                MessageBox.Show(
                    $"Đã thêm chất liệu CL{chatLieuMoiId:000000}.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu chất liệu. Tên chất liệu có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thêm chất liệu. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !chatLieuDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn chất liệu cần cập nhật.");
                return;
            }

            ThongTinChatLieuNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu)) return;
            int chatLieuId = chatLieuDangChonId.Value;

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    if (TenChatLieuDaTonTai(db, duLieu.TenChatLieu, chatLieuId))
                    {
                        HienThiLoi("Tên chất liệu đã tồn tại.");
                        return;
                    }

                    var chatLieu = db.ChatLieux.SingleOrDefault(cl => cl.ChatLieuId == chatLieuId);
                    if (chatLieu == null)
                    {
                        HienThiLoi("Chất liệu không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    chatLieu.TenChatLieu = duLieu.TenChatLieu;
                    chatLieu.GiaMuaVao = duLieu.GiaMuaVao;
                    chatLieu.GiaBanRa = duLieu.GiaBanRa;
                    db.SaveChanges();
                }

                TaiDanhSach(chatLieuId);
                MessageBox.Show("Đã cập nhật chất liệu và giá tham khảo.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cập nhật. Tên chất liệu có thể đã tồn tại.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật chất liệu.");
            }
        }

        private void btnXoaHoacTrangThai_Click(object sender, EventArgs e)
        {
            if (!KiemTraQuyenQuanTri(true) || !chatLieuDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn chất liệu cần xử lý.");
                return;
            }

            int chatLieuId = chatLieuDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var chatLieu = db.ChatLieux
                        .Include(cl => cl.ChiTietChatLieux)
                        .Include(cl => cl.ChiTietPhieuThuMuas)
                        .SingleOrDefault(cl => cl.ChatLieuId == chatLieuId);
                    if (chatLieu == null)
                    {
                        HienThiLoi("Chất liệu không còn tồn tại trong CSDL.");
                        TaiDanhSach();
                        return;
                    }

                    bool daDuocSuDung = chatLieu.ChiTietChatLieux.Any() || chatLieu.ChiTietPhieuThuMuas.Any();
                    string hanhDong = !chatLieu.DangHoatDong
                        ? "khôi phục"
                        : (daDuocSuDung ? "ngừng hoạt động" : "xóa");
                    if (MessageBox.Show(
                            $"Bạn có chắc muốn {hanhDong} chất liệu {chatLieu.TenChatLieu}?",
                            "Xác nhận",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    if (!chatLieu.DangHoatDong)
                    {
                        chatLieu.DangHoatDong = true;
                        db.SaveChanges();
                        TaiDanhSach(chatLieuId);
                    }
                    else if (daDuocSuDung)
                    {
                        chatLieu.DangHoatDong = false;
                        db.SaveChanges();
                        TaiDanhSach(chatLieuId);
                    }
                    else
                    {
                        db.ChatLieux.Remove(chatLieu);
                        db.SaveChanges();
                        TaiDanhSach();
                        LamMoiBieuMau();
                    }
                }
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Chất liệu đã phát sinh tham chiếu và không thể xóa. Hãy tải lại rồi ngừng hoạt động.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thay đổi trạng thái chất liệu.");
            }
        }

        private void btnLamMoiBieuMau_Click(object sender, EventArgs e)
        {
            LamMoiBieuMau();
        }

        private void LamMoiBieuMau()
        {
            dangLamMoiBieuMau = true;
            try
            {
                chatLieuDangChonId = null;
                txtMaChatLieu.Text = "Tự động tạo";
                txtTenChatLieu.Clear();
                numGiaMuaVao.Value = 0;
                numGiaBanRa.Value = 0;
                chkDangHoatDong.Checked = true;
                btnXoaHoacTrangThai.Text = "Xóa chất liệu";
                lblThongBao.Text = string.Empty;
                dgvChatLieu.ClearSelection();
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }

            txtTenChatLieu.Focus();
        }

        private bool ThuLayDuLieuNhap(out ThongTinChatLieuNhap duLieu)
        {
            duLieu = null;
            string tenChatLieu = txtTenChatLieu.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenChatLieu))
            {
                HienThiLoi("Tên chất liệu không được để trống.");
                txtTenChatLieu.Focus();
                return false;
            }

            duLieu = new ThongTinChatLieuNhap
            {
                TenChatLieu = tenChatLieu,
                GiaMuaVao = numGiaMuaVao.Value,
                GiaBanRa = numGiaBanRa.Value
            };
            lblThongBao.Text = string.Empty;
            return true;
        }

        private static bool TenChatLieuDaTonTai(
            QL_CuaHangDaQuy_PNJEntities db,
            string tenChatLieu,
            int? boQuaChatLieuId)
        {
            return db.ChatLieux.Any(cl =>
                cl.TenChatLieu == tenChatLieu &&
                (!boQuaChatLieuId.HasValue || cl.ChatLieuId != boQuaChatLieuId.Value));
        }

        private void HienThiLoi(string noiDung)
        {
            lblThongBao.Text = "* " + noiDung;
        }

        private sealed class ThongTinChatLieuNhap
        {
            public string TenChatLieu { get; set; }
            public decimal GiaMuaVao { get; set; }
            public decimal GiaBanRa { get; set; }
        }

        private sealed class ChatLieuHienThi
        {
            public ChatLieuHienThi(ChatLieu chatLieu)
            {
                ChatLieuId = chatLieu.ChatLieuId;
                MaChatLieu = $"CL{chatLieu.ChatLieuId:000000}";
                TenChatLieu = chatLieu.TenChatLieu;
                GiaMuaVao = chatLieu.GiaMuaVao;
                GiaBanRa = chatLieu.GiaBanRa;
                SoSanPham = chatLieu.ChiTietChatLieux.Count;
                SoPhieuThuMua = chatLieu.ChiTietPhieuThuMuas.Count;
                SoThamChieu = SoSanPham + SoPhieuThuMua;
                DangHoatDong = chatLieu.DangHoatDong;
                TrangThai = chatLieu.DangHoatDong ? "Đang hoạt động" : "Ngừng hoạt động";
            }

            public int ChatLieuId { get; }
            public string MaChatLieu { get; }
            public string TenChatLieu { get; }
            public decimal GiaMuaVao { get; }
            public decimal GiaBanRa { get; }
            public int SoSanPham { get; }
            public int SoPhieuThuMua { get; }
            public int SoThamChieu { get; }
            public bool DangHoatDong { get; }
            public string TrangThai { get; }
        }
    }
}

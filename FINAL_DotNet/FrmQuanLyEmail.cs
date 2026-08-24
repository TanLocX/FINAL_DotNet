using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmQuanLyEmail : Form
    {
        private readonly DichVuGuiEmail dichVuGuiEmail = new DichVuGuiEmail();
        private readonly List<string> tepDinhKemDon = new List<string>();
        private readonly Timer boDemHenGio = new Timer();
        private List<NguoiNhanHangLoat> danhSachNguoiNhan = new List<NguoiNhanHangLoat>();
        private int? mauEmailDangChonId;
        private DateTime? thoiGianHenGui;
        private bool dangNapDuLieu;
        private bool dangGuiDon;
        private bool dangGuiHangLoat;

        public FrmQuanLyEmail()
        {
            InitializeComponent();
            boDemHenGio.Interval = 1000;
            boDemHenGio.Tick += boDemHenGio_Tick;
            LuxuryDarkGoldTheme.Apply(this);
        }

        private void FrmQuanLyEmail_Load(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }
            NapCauHinhSmtp();
            TaiToanBoDuLieu();
            dtpHenGio.Value = DateTime.Now.AddMinutes(5);
        }

        private void FrmQuanLyEmail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dangGuiDon || dangGuiHangLoat)
            {
                MessageBox.Show("Vui lòng chờ thao tác gửi email hiện tại hoàn tất.", "Đang gửi email",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            boDemHenGio.Stop();
        }

        private bool KiemTraPhienDangNhap(bool hienThongBao)
        {
            bool hopLe = CurrentUserSession.DaDangNhap;
            if (!hopLe && hienThongBao)
                MessageBox.Show("Phiên đăng nhập đã kết thúc. Vui lòng đăng nhập lại.", "Chưa đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return hopLe;
        }

        private void TaiToanBoDuLieu()
        {
            dangNapDuLieu = true;
            try
            {
                TaiMauEmail();
                TaiKhachHangGuiDon();
                TaiNguoiNhanHangLoat();
                TaiNhatKy();
                lblLoi.Text = string.Empty;
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải dữ liệu email. Hãy kiểm tra kết nối CSDL.");
            }
            finally
            {
                dangNapDuLieu = false;
            }
            cboKhachHangDon_SelectedIndexChanged(this, EventArgs.Empty);
            lstMauEmail_SelectedIndexChanged(this, EventArgs.Empty);
        }

        #region Cấu hình SMTP

        private void NapCauHinhSmtp()
        {
            CauHinhSmtp cauHinh = CauHinhSmtp.DocTuBienMoiTruong();
            txtMayChuSmtp.Text = cauHinh.MayChu;
            nudCongSmtp.Value = Math.Max(nudCongSmtp.Minimum, Math.Min(nudCongSmtp.Maximum, cauHinh.Cong));
            chkSuDungSsl.Checked = cauHinh.SuDungSsl;
            txtTaiKhoanSmtp.Text = cauHinh.TaiKhoanGui ?? string.Empty;
            txtMatKhauSmtp.Clear();
            txtTenNguoiGui.Text = cauHinh.TenNguoiGui;
            CapNhatTrangThaiSmtp(cauHinh);
        }

        private void btnLuuSmtp_Click(object sender, EventArgs e)
        {
            CauHinhSmtp hienTai = CauHinhSmtp.DocTuBienMoiTruong();
            var cauHinh = new CauHinhSmtp
            {
                MayChu = txtMayChuSmtp.Text.Trim(),
                Cong = Decimal.ToInt32(nudCongSmtp.Value),
                SuDungSsl = chkSuDungSsl.Checked,
                TaiKhoanGui = txtTaiKhoanSmtp.Text.Trim(),
                MatKhauUngDung = string.IsNullOrWhiteSpace(txtMatKhauSmtp.Text)
                    ? hienTai.MatKhauUngDung
                    : txtMatKhauSmtp.Text,
                TenNguoiGui = txtTenNguoiGui.Text.Trim()
            };
            string loi;
            if (!cauHinh.ThuKiemTra(out loi))
            {
                HienThiLoi(loi);
                return;
            }
            try
            {
                CauHinhSmtp.LuuVaoBienMoiTruong(cauHinh);
                txtMatKhauSmtp.Clear();
                CapNhatTrangThaiSmtp(cauHinh);
                lblLoi.Text = string.Empty;
                MessageBox.Show("Đã lưu cấu hình SMTP vào biến môi trường người dùng. Mật khẩu không được lưu trong CSDL.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể lưu biến môi trường SMTP cho người dùng hiện tại.");
            }
        }

        private void CapNhatTrangThaiSmtp(CauHinhSmtp cauHinh)
        {
            string loi;
            bool daCauHinh = cauHinh.ThuKiemTra(out loi);
            lblTrangThaiSmtp.Text = daCauHinh
                ? "Đã cấu hình email gửi: " + cauHinh.TaiKhoanGui
                : "Chưa sẵn sàng: " + loi;
            lblTrangThaiSmtp.ForeColor = daCauHinh ? Color.FromArgb(46, 125, 50) : Color.Firebrick;
        }

        #endregion

        #region Mẫu email

        private void TaiMauEmail(int? mauCanChonId = null)
        {
            List<MauEmailHienThi> tatCa;
            using (var db = DatabaseConnection.CreateContext())
            {
                tatCa = db.MauEmails.AsNoTracking()
                    .OrderByDescending(me => me.DangHoatDong)
                    .ThenBy(me => me.TenMau)
                    .ToList()
                    .Select(me => new MauEmailHienThi(me))
                    .ToList();
            }

            lstMauEmail.DataSource = tatCa;
            var luaChonGui = new List<LuaChonMauEmail> { new LuaChonMauEmail(null, "Tự soạn nội dung", null, null) };
            luaChonGui.AddRange(tatCa.Where(me => me.DangHoatDong)
                .Select(me => new LuaChonMauEmail(me.MauEmailId, me.TenMau, me.TieuDeMau, me.NoiDungMau)));
            GanNguonMauGui(cboMauGuiDon, luaChonGui);
            GanNguonMauGui(cboMauHangLoat, luaChonGui);

            var luaChonLoc = new List<LuaChonMauEmail> { new LuaChonMauEmail(null, "Tất cả mẫu", null, null) };
            luaChonLoc.AddRange(tatCa.Select(me => new LuaChonMauEmail(me.MauEmailId, me.TenMau, null, null)));
            GanNguonMauGui(cboLocMauNhatKy, luaChonLoc);

            if (mauCanChonId.HasValue)
            {
                for (int i = 0; i < lstMauEmail.Items.Count; i++)
                {
                    var item = lstMauEmail.Items[i] as MauEmailHienThi;
                    if (item?.MauEmailId != mauCanChonId.Value) continue;
                    lstMauEmail.SelectedIndex = i;
                    break;
                }
            }
            else if (lstMauEmail.Items.Count > 0)
            {
                lstMauEmail.SelectedIndex = 0;
            }
            else
            {
                LamMoiMauEmail();
            }
        }

        private static void GanNguonMauGui(ComboBox combo, IEnumerable<LuaChonMauEmail> nguon)
        {
            combo.DataSource = nguon.Select(item => item.SaoChep()).ToList();
            combo.SelectedIndex = combo.Items.Count > 0 ? 0 : -1;
        }

        private void lstMauEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNapDuLieu) return;
            var item = lstMauEmail.SelectedItem as MauEmailHienThi;
            if (item == null) return;
            mauEmailDangChonId = item.MauEmailId;
            txtTenMau.Text = item.TenMau;
            txtTieuDeMau.Text = item.TieuDeMau;
            txtNoiDungMau.Text = item.NoiDungMau;
            chkMauHoatDong.Checked = item.DangHoatDong;
            btnKhoaMau.Text = item.DangHoatDong ? "Ngừng sử dụng" : "Mở lại mẫu";
        }

        private void btnMauMoi_Click(object sender, EventArgs e) => LamMoiMauEmail();

        private void LamMoiMauEmail()
        {
            mauEmailDangChonId = null;
            lstMauEmail.ClearSelected();
            txtTenMau.Clear();
            txtTieuDeMau.Clear();
            txtNoiDungMau.Clear();
            chkMauHoatDong.Checked = true;
            btnKhoaMau.Text = "Ngừng sử dụng";
            txtTenMau.Focus();
        }

        private void btnLuuMau_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            string ten = txtTenMau.Text.Trim();
            string tieuDe = txtTieuDeMau.Text.Trim();
            string noiDung = txtNoiDungMau.Text.Trim();
            if (ten.Length == 0 || tieuDe.Length == 0 || noiDung.Length == 0)
            {
                HienThiLoi("Tên mẫu, tiêu đề và nội dung mẫu không được để trống.");
                return;
            }
            if (ten.Length > 100 || tieuDe.Length > 255)
            {
                HienThiLoi("Tên mẫu tối đa 100 ký tự và tiêu đề tối đa 255 ký tự.");
                return;
            }
            try
            {
                int id;
                using (var db = DatabaseConnection.CreateContext())
                {
                    int? idDangSua = mauEmailDangChonId;
                    bool biTrung = db.MauEmails.Any(me => me.TenMau == ten &&
                        (!idDangSua.HasValue || me.MauEmailId != idDangSua.Value));
                    if (biTrung)
                    {
                        HienThiLoi("Tên mẫu email đã tồn tại.");
                        return;
                    }
                    MauEmail mau;
                    if (idDangSua.HasValue)
                    {
                        mau = db.MauEmails.SingleOrDefault(me => me.MauEmailId == idDangSua.Value);
                        if (mau == null)
                        {
                            HienThiLoi("Mẫu email không còn tồn tại trong CSDL.");
                            return;
                        }
                    }
                    else
                    {
                        mau = new MauEmail();
                        db.MauEmails.Add(mau);
                    }
                    mau.TenMau = ten;
                    mau.TieuDeMau = tieuDe;
                    mau.NoiDungMau = noiDung;
                    mau.DangHoatDong = chkMauHoatDong.Checked;
                    mau.TaiKhoanCapNhatId = CurrentUserSession.HienTai.TaiKhoanId;
                    mau.NgayCapNhat = DateTime.Now;
                    db.SaveChanges();
                    id = mau.MauEmailId;
                }
                dangNapDuLieu = true;
                try { TaiMauEmail(id); }
                finally { dangNapDuLieu = false; }
                lstMauEmail_SelectedIndexChanged(this, EventArgs.Empty);
                lblLoi.Text = string.Empty;
                MessageBox.Show("Đã lưu mẫu email vào CSDL.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu mẫu email vì tên mẫu bị trùng hoặc dữ liệu vừa thay đổi.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể lưu mẫu email. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnKhoaMau_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !mauEmailDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn mẫu email cần thay đổi trạng thái.");
                return;
            }
            int id = mauEmailDangChonId.Value;
            try
            {
                bool trangThaiMoi;
                using (var db = DatabaseConnection.CreateContext())
                {
                    var mau = db.MauEmails.SingleOrDefault(me => me.MauEmailId == id);
                    if (mau == null)
                    {
                        HienThiLoi("Mẫu email không còn tồn tại trong CSDL.");
                        return;
                    }
                    trangThaiMoi = !mau.DangHoatDong;
                    mau.DangHoatDong = trangThaiMoi;
                    mau.TaiKhoanCapNhatId = CurrentUserSession.HienTai.TaiKhoanId;
                    mau.NgayCapNhat = DateTime.Now;
                    db.SaveChanges();
                }
                dangNapDuLieu = true;
                try { TaiMauEmail(id); }
                finally { dangNapDuLieu = false; }
                lstMauEmail_SelectedIndexChanged(this, EventArgs.Empty);
                MessageBox.Show(trangThaiMoi ? "Đã mở lại mẫu email." : "Đã ngừng sử dụng mẫu email.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật trạng thái mẫu email.");
            }
        }

        private void btnTaoMauMacDinh_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            try
            {
                int soMauMoi = 0;
                using (var db = DatabaseConnection.CreateContext())
                {
                    foreach (var mauMacDinh in TaoDanhSachMauMacDinh())
                    {
                        if (db.MauEmails.Any(me => me.TenMau == mauMacDinh.TenMau)) continue;
                        mauMacDinh.TaiKhoanCapNhatId = CurrentUserSession.HienTai.TaiKhoanId;
                        db.MauEmails.Add(mauMacDinh);
                        soMauMoi++;
                    }
                    db.SaveChanges();
                }
                dangNapDuLieu = true;
                try { TaiMauEmail(); }
                finally { dangNapDuLieu = false; }
                lstMauEmail_SelectedIndexChanged(this, EventArgs.Empty);
                MessageBox.Show(soMauMoi == 0 ? "Ba mẫu mặc định đã có trong CSDL."
                    : "Đã thêm " + soMauMoi + " mẫu email mặc định vào CSDL.", "Hoàn tất",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tạo mẫu email mặc định.");
            }
        }

        private static IEnumerable<MauEmail> TaoDanhSachMauMacDinh()
        {
            DateTime bayGio = DateTime.Now;
            return new[]
            {
                new MauEmail
                {
                    TenMau = "Hóa Đơn Trang Sức",
                    TieuDeMau = "[PNJ MANAGER] Hóa đơn mua hàng điện tử - Quý khách {HoTen}",
                    NoiDungMau = "<div style='max-width:600px;margin:auto;border:1px solid #ddd;border-radius:8px;padding:24px;font-family:Arial'>" +
                        "<h2 style='color:#d4af37;border-bottom:3px double #d4af37'>HÓA ĐƠN MUA HÀNG</h2>" +
                        "<p>Kính gửi <b>{HoTen}</b>, cảm ơn quý khách đã mua sắm tại PNJ MANAGER.</p>" +
                        "<table style='width:100%;border-collapse:collapse'><tr><td>Sản phẩm</td><td>{TenSanPham}</td></tr>" +
                        "<tr style='background:#f9f9f9'><td>Ngày giao dịch</td><td>{NgayMua}</td></tr>" +
                        "<tr><td>Tổng thanh toán</td><td style='color:#d32f2f;font-weight:bold'>{TongTien} VNĐ</td></tr></table>" +
                        "<p>Hóa đơn điện tử hỗ trợ đối chiếu bảo hành sản phẩm.</p><small>Đây là thư được gửi tự động.</small></div>",
                    DangHoatDong = true,
                    NgayCapNhat = bayGio
                },
                new MauEmail
                {
                    TenMau = "Nhắc Nhở Bảo Hành",
                    TieuDeMau = "[PNJ MANAGER] Nhắc nhở bảo dưỡng trang sức - Quý khách {HoTen}",
                    NoiDungMau = "<div style='max-width:600px;margin:auto;padding:24px;font-family:Arial'>" +
                        "<h2 style='color:#0d6efd;border-bottom:2px solid #0d6efd'>NHẮC LỊCH BẢO HÀNH</h2>" +
                        "<p>Kính gửi <b>{HoTen}</b>, sản phẩm <b>{TenSanPham}</b> có hạn bảo hành đến {HanBaoHanh}.</p>" +
                        "<p>Mời quý khách mang trang sức đến cửa hàng trước thời hạn để được làm sạch và bảo dưỡng.</p>" +
                        "<p>Bộ phận Chăm sóc Khách hàng - Hotline 1900.xxxx</p></div>",
                    DangHoatDong = true,
                    NgayCapNhat = bayGio
                },
                new MauEmail
                {
                    TenMau = "Tri Ân Sinh Nhật",
                    TieuDeMau = "[PNJ MANAGER] Chúc mừng sinh nhật Quý khách {HoTen}",
                    NoiDungMau = "<div style='max-width:600px;margin:auto;padding:24px;background:#fff9fb;border:1px solid #e83e8c;font-family:Arial'>" +
                        "<h2 style='color:#e83e8c'>CHÚC MỪNG SINH NHẬT</h2><p>Thân gửi <b>{HoTen}</b>, chúc quý khách tuổi mới rạng ngời và may mắn.</p>" +
                        "<div style='border:2px dashed #dc3545;background:#f8d7da;padding:16px;text-align:center;color:#721c24;font-size:20px'>" +
                        "MÃ ƯU ĐÃI: HPBD{Sdt}<br/><small>Trị giá 500.000 VNĐ</small></div>" +
                        "<p>Áp dụng đến hết tháng sinh nhật khi mua sắm tại cửa hàng.</p></div>",
                    DangHoatDong = true,
                    NgayCapNhat = bayGio
                }
            };
        }

        #endregion

        #region Gửi email đơn

        private void TaiKhachHangGuiDon()
        {
            List<KhachHangGuiEmail> danhSach;
            using (var db = DatabaseConnection.CreateContext())
            {
                danhSach = db.KhachHangs.AsNoTracking()
                    .Where(kh => kh.Email != null && kh.Email != "")
                    .OrderBy(kh => kh.HoTen)
                    .ToList()
                    .Select(kh => new KhachHangGuiEmail(kh))
                    .ToList();
            }
            danhSach.Insert(0, KhachHangGuiEmail.TaoNhapThuCong());
            cboKhachHangDon.DataSource = danhSach;
            cboKhachHangDon.SelectedIndex = 0;
        }

        private void cboKhachHangDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNapDuLieu) return;
            var khachHang = cboKhachHangDon.SelectedItem as KhachHangGuiEmail;
            txtEmailDon.Text = khachHang?.Email ?? string.Empty;
            TaiHoaDonCuaKhach(khachHang?.KhachHangId);
        }

        private void TaiHoaDonCuaKhach(int? khachHangId)
        {
            var danhSach = new List<HoaDonGuiEmail> { HoaDonGuiEmail.TaoKhongGan() };
            if (khachHangId.HasValue)
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    danhSach.AddRange(db.HoaDons
                        .Include(hd => hd.ChiTietHoaDons.Select(ct => ct.SanPham))
                        .AsNoTracking()
                        .Where(hd => hd.KhachHangId == khachHangId.Value && hd.TrangThai == "DA_THANH_TOAN")
                        .OrderByDescending(hd => hd.NgayLap)
                        .ToList()
                        .Select(hd => new HoaDonGuiEmail(hd)));
                }
            }
            cboHoaDonDon.DataSource = danhSach;
            cboHoaDonDon.SelectedIndex = 0;
        }

        private void cboMauGuiDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNapDuLieu) return;
            var mau = cboMauGuiDon.SelectedItem as LuaChonMauEmail;
            if (mau == null || !mau.MauEmailId.HasValue) return;
            txtTieuDeDon.Text = mau.TieuDeMau;
            txtNoiDungDon.Text = mau.NoiDungMau;
        }

        private void btnThemTepDon_Click(object sender, EventArgs e)
        {
            using (var hopThoai = new OpenFileDialog { Multiselect = true, Title = "Chọn tệp đính kèm" })
            {
                if (hopThoai.ShowDialog(this) != DialogResult.OK) return;
                foreach (string tep in hopThoai.FileNames)
                    if (!tepDinhKemDon.Contains(tep, StringComparer.OrdinalIgnoreCase)) tepDinhKemDon.Add(tep);
            }
            CapNhatDanhSachTepDon();
        }

        private void btnXoaTepDon_Click(object sender, EventArgs e)
        {
            if (lstTepDon.SelectedIndex < 0) return;
            tepDinhKemDon.RemoveAt(lstTepDon.SelectedIndex);
            CapNhatDanhSachTepDon();
        }

        private void CapNhatDanhSachTepDon()
        {
            lstTepDon.DataSource = null;
            lstTepDon.DataSource = tepDinhKemDon.Select(System.IO.Path.GetFileName).ToList();
        }

        private async void btnGuiDon_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            CauHinhSmtp cauHinh = CauHinhSmtp.DocTuBienMoiTruong();
            string loi;
            if (!cauHinh.ThuKiemTra(out loi))
            {
                HienThiLoi(loi + " Hãy mở tab Cấu hình SMTP.");
                return;
            }
            string emailNhan = txtEmailDon.Text.Trim();
            if (!CauHinhSmtp.EmailHopLe(emailNhan))
            {
                HienThiLoi("Email người nhận không hợp lệ.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTieuDeDon.Text) || string.IsNullOrWhiteSpace(txtNoiDungDon.Text))
            {
                HienThiLoi("Tiêu đề và nội dung email không được để trống.");
                return;
            }

            var khachHang = cboKhachHangDon.SelectedItem as KhachHangGuiEmail;
            var hoaDon = cboHoaDonDon.SelectedItem as HoaDonGuiEmail;
            DuLieuNguoiNhanEmail nguoiNhan = TaoDuLieuNguoiNhan(khachHang, hoaDon, emailNhan);
            string tieuDe = BoMayCaNhanHoaEmail.DienNoiDung(txtTieuDeDon.Text, nguoiNhan).Trim();
            string noiDung = BoMayCaNhanHoaEmail.DienNoiDung(txtNoiDungDon.Text, nguoiNhan);
            if (tieuDe.Length > 255)
            {
                HienThiLoi("Tiêu đề sau khi cá nhân hóa vượt quá 255 ký tự.");
                return;
            }

            btnGuiDon.Enabled = false;
            dangGuiDon = true;
            UseWaitCursor = true;
            lblTrangThaiGuiDon.Text = "Đang gửi...";
            bool thanhCong = false;
            string ghiChu = null;
            try
            {
                await dichVuGuiEmail.GuiAsync(cauHinh, emailNhan, tieuDe, noiDung, tepDinhKemDon);
                thanhCong = true;
                ghiChu = "Đã gửi email thành công.";
            }
            catch (Exception ex)
            {
                ghiChu = ChuanHoaLoiGui(ex, cauHinh);
            }

            string loiNhatKy = null;
            try
            {
                var mau = cboMauGuiDon.SelectedItem as LuaChonMauEmail;
                GhiNhatKy(nguoiNhan.KhachHangId, nguoiNhan.HoaDonId, mau?.MauEmailId,
                    emailNhan, tieuDe, "DON", thanhCong, ghiChu);
            }
            catch (Exception)
            {
                loiNhatKy = " Không thể ghi nhật ký vào CSDL.";
            }
            if (loiNhatKy == null)
            {
                try { TaiNhatKy(); }
                catch (Exception) { loiNhatKy = " Đã ghi nhật ký nhưng không thể làm mới danh sách."; }
            }
            dangGuiDon = false;
            btnGuiDon.Enabled = true;
            UseWaitCursor = false;

            if (thanhCong)
            {
                lblTrangThaiGuiDon.Text = "Gửi thành công lúc " + DateTime.Now.ToString("HH:mm:ss") + (loiNhatKy ?? string.Empty);
                MessageBox.Show("Email đã được gửi." + (loiNhatKy ?? string.Empty), "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblTrangThaiGuiDon.Text = "Gửi thất bại." + (loiNhatKy ?? string.Empty);
                HienThiLoi("Không thể gửi email: " + ghiChu + (loiNhatKy ?? string.Empty));
            }
        }

        #endregion

        #region Gửi hàng loạt từ CSDL

        private void TaiNguoiNhanHangLoat()
        {
            using (var db = DatabaseConnection.CreateContext())
            {
                var khachHang = db.KhachHangs.AsNoTracking()
                    .Where(kh => kh.KhachHangId != 1 && kh.HoTen != "Khách lẻ" && kh.DangHoatDong &&
                        kh.ChoPhepNhanEmail && kh.Email != null && kh.Email != "")
                    .OrderBy(kh => kh.HoTen)
                    .ToList();
                var ids = khachHang.Select(kh => kh.KhachHangId).ToList();
                var hoaDon = db.HoaDons
                    .Include(hd => hd.ChiTietHoaDons.Select(ct => ct.SanPham))
                    .AsNoTracking()
                    .Where(hd => ids.Contains(hd.KhachHangId) && hd.TrangThai == "DA_THANH_TOAN")
                    .OrderByDescending(hd => hd.NgayLap)
                    .ToList();
                danhSachNguoiNhan = khachHang
                    .Where(kh => CauHinhSmtp.EmailHopLe(kh.Email))
                    .Select(kh => new NguoiNhanHangLoat(kh,
                        hoaDon.FirstOrDefault(hd => hd.KhachHangId == kh.KhachHangId)))
                    .ToList();
            }
            dgvNguoiNhan.DataSource = danhSachNguoiNhan;
            lblSoNguoiNhan.Text = "Tổng số người nhận hợp lệ: " + danhSachNguoiNhan.Count;
        }

        private void cboMauHangLoat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNapDuLieu) return;
            var mau = cboMauHangLoat.SelectedItem as LuaChonMauEmail;
            if (mau == null || !mau.MauEmailId.HasValue) return;
            txtTieuDeHangLoat.Text = mau.TieuDeMau;
            txtNoiDungHangLoat.Text = mau.NoiDungMau;
        }

        private void chkHenGio_CheckedChanged(object sender, EventArgs e)
        {
            dtpHenGio.Enabled = chkHenGio.Checked && !dangGuiHangLoat;
        }

        private async void btnGuiHangLoat_Click(object sender, EventArgs e)
        {
            if (thoiGianHenGui.HasValue)
            {
                thoiGianHenGui = null;
                boDemHenGio.Stop();
                btnGuiHangLoat.Text = "Bắt đầu gửi";
                lblTrangThaiHangLoat.Text = "Đã hủy lịch gửi.";
                BatKhoaGuiHangLoat(false);
                return;
            }
            if (!KiemTraDuLieuGuiHangLoat()) return;
            if (chkHenGio.Checked)
            {
                if (dtpHenGio.Value <= DateTime.Now)
                {
                    HienThiLoi("Thời gian hẹn gửi phải lớn hơn thời gian hiện tại.");
                    return;
                }
                thoiGianHenGui = dtpHenGio.Value;
                BatKhoaGuiHangLoat(true);
                btnGuiHangLoat.Enabled = true;
                btnGuiHangLoat.Text = "Hủy lịch gửi";
                boDemHenGio.Start();
                CapNhatDemNguoc();
                return;
            }
            await GuiHangLoatAsync();
        }

        private async void boDemHenGio_Tick(object sender, EventArgs e)
        {
            if (!thoiGianHenGui.HasValue)
            {
                boDemHenGio.Stop();
                return;
            }
            if (DateTime.Now < thoiGianHenGui.Value)
            {
                CapNhatDemNguoc();
                return;
            }
            boDemHenGio.Stop();
            thoiGianHenGui = null;
            btnGuiHangLoat.Text = "Bắt đầu gửi";
            await GuiHangLoatAsync();
        }

        private void CapNhatDemNguoc()
        {
            TimeSpan conLai = thoiGianHenGui.Value - DateTime.Now;
            if (conLai < TimeSpan.Zero) conLai = TimeSpan.Zero;
            lblTrangThaiHangLoat.Text = "Sẽ gửi sau " +
                string.Format("{0:00}:{1:00}:{2:00}", (int)conLai.TotalHours, conLai.Minutes, conLai.Seconds);
        }

        private bool KiemTraDuLieuGuiHangLoat()
        {
            dgvNguoiNhan.EndEdit();
            if (!danhSachNguoiNhan.Any(item => item.DuocChon))
            {
                HienThiLoi("Vui lòng chọn ít nhất một khách hàng nhận email.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTieuDeHangLoat.Text) || string.IsNullOrWhiteSpace(txtNoiDungHangLoat.Text))
            {
                HienThiLoi("Tiêu đề và nội dung email hàng loạt không được để trống.");
                return false;
            }
            string loi;
            if (!CauHinhSmtp.DocTuBienMoiTruong().ThuKiemTra(out loi))
            {
                HienThiLoi(loi + " Hãy mở tab Cấu hình SMTP.");
                return false;
            }
            return true;
        }

        private async Task GuiHangLoatAsync()
        {
            if (!KiemTraDuLieuGuiHangLoat())
            {
                BatKhoaGuiHangLoat(false);
                return;
            }
            List<NguoiNhanHangLoat> danhSach = danhSachNguoiNhan.Where(item => item.DuocChon).ToList();
            CauHinhSmtp cauHinh = CauHinhSmtp.DocTuBienMoiTruong();
            var mau = cboMauHangLoat.SelectedItem as LuaChonMauEmail;
            int thanhCong = 0;
            int thatBai = 0;
            int loiNhatKy = 0;
            dangGuiHangLoat = true;
            BatKhoaGuiHangLoat(true);
            progressHangLoat.Minimum = 0;
            progressHangLoat.Maximum = danhSach.Count;
            progressHangLoat.Value = 0;
            UseWaitCursor = true;
            try
            {
                for (int i = 0; i < danhSach.Count; i++)
                {
                    if (IsDisposed || Disposing) return;
                    NguoiNhanHangLoat item = danhSach[i];
                    lblTrangThaiHangLoat.Text = "Đang gửi đến " + item.Email + " (" + (i + 1) + "/" + danhSach.Count + ")...";
                    DuLieuNguoiNhanEmail nguoiNhan = item.TaoDuLieu();
                    string tieuDe = BoMayCaNhanHoaEmail.DienNoiDung(txtTieuDeHangLoat.Text, nguoiNhan).Trim();
                    string noiDung = BoMayCaNhanHoaEmail.DienNoiDung(txtNoiDungHangLoat.Text, nguoiNhan);
                    bool guiThanhCong = false;
                    string ghiChu;
                    if (tieuDe.Length > 255)
                    {
                        ghiChu = "Tiêu đề sau khi cá nhân hóa vượt quá 255 ký tự.";
                    }
                    else
                    {
                        try
                        {
                            await dichVuGuiEmail.GuiAsync(cauHinh, item.Email, tieuDe, noiDung, null);
                            guiThanhCong = true;
                            ghiChu = "Đã gửi email thành công.";
                        }
                        catch (Exception ex)
                        {
                            ghiChu = ChuanHoaLoiGui(ex, cauHinh);
                        }
                    }
                    if (guiThanhCong) thanhCong++; else thatBai++;
                    try
                    {
                        GhiNhatKy(item.KhachHangId, item.HoaDonId, mau?.MauEmailId,
                            item.Email, CatChuoi(tieuDe, 255), "HANG_LOAT", guiThanhCong, ghiChu);
                    }
                    catch (Exception)
                    {
                        loiNhatKy++;
                    }
                    progressHangLoat.Value = i + 1;
                }
            }
            finally
            {
                dangGuiHangLoat = false;
                UseWaitCursor = false;
                BatKhoaGuiHangLoat(false);
                try { TaiNhatKy(); }
                catch (Exception) { HienThiLoi("Không thể làm mới nhật ký sau khi gửi hàng loạt."); }
            }
            lblTrangThaiHangLoat.Text = "Hoàn tất: " + thanhCong + " thành công, " + thatBai + " thất bại" +
                (loiNhatKy > 0 ? ", " + loiNhatKy + " lỗi ghi nhật ký" : string.Empty) + ".";
            MessageBox.Show(lblTrangThaiHangLoat.Text, "Kết quả gửi hàng loạt",
                MessageBoxButtons.OK, thatBai == 0 && loiNhatKy == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void BatKhoaGuiHangLoat(bool khoa)
        {
            cboMauHangLoat.Enabled = !khoa;
            txtTieuDeHangLoat.ReadOnly = khoa;
            txtNoiDungHangLoat.ReadOnly = khoa;
            dgvNguoiNhan.Enabled = !khoa;
            chkHenGio.Enabled = !khoa;
            dtpHenGio.Enabled = !khoa && chkHenGio.Checked;
            btnTaiNguoiNhan.Enabled = !khoa;
            btnGuiHangLoat.Enabled = !khoa;
        }

        private void btnTaiNguoiNhan_Click(object sender, EventArgs e)
        {
            try
            {
                TaiNguoiNhanHangLoat();
                lblLoi.Text = string.Empty;
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách người nhận từ CSDL.");
            }
        }

        #endregion

        #region Nhật ký

        private void TaiNhatKy()
        {
            string tuKhoa = txtTimNhatKy.Text.Trim();
            string loaiGui = cboLocLoaiGui.SelectedIndex == 1 ? "DON"
                : cboLocLoaiGui.SelectedIndex == 2 ? "HANG_LOAT" : null;
            string trangThai = cboLocTrangThaiNhatKy.SelectedIndex == 1 ? "THANH_CONG"
                : cboLocTrangThaiNhatKy.SelectedIndex == 2 ? "THAT_BAI" : null;
            var mau = cboLocMauNhatKy.SelectedItem as LuaChonMauEmail;
            using (var db = DatabaseConnection.CreateContext())
            {
                var truyVan = db.NhatKyGuiEmails
                    .Include(nk => nk.KhachHang)
                    .Include(nk => nk.HoaDon)
                    .Include(nk => nk.MauEmail)
                    .Include(nk => nk.TaiKhoan.NhanVien)
                    .AsNoTracking();
                if (dtpTuNgayNhatKy.Checked)
                {
                    DateTime tuNgay = dtpTuNgayNhatKy.Value.Date;
                    truyVan = truyVan.Where(nk => nk.ThoiGianGui >= tuNgay);
                }
                if (dtpDenNgayNhatKy.Checked)
                {
                    DateTime denNgay = dtpDenNgayNhatKy.Value.Date.AddDays(1);
                    truyVan = truyVan.Where(nk => nk.ThoiGianGui < denNgay);
                }
                if (loaiGui != null) truyVan = truyVan.Where(nk => nk.LoaiGui == loaiGui);
                if (trangThai != null) truyVan = truyVan.Where(nk => nk.TrangThai == trangThai);
                if (mau?.MauEmailId != null) truyVan = truyVan.Where(nk => nk.MauEmailId == mau.MauEmailId.Value);
                if (tuKhoa.Length > 0)
                    truyVan = truyVan.Where(nk => nk.EmailNhan.Contains(tuKhoa) || nk.TieuDe.Contains(tuKhoa) ||
                        (nk.KhachHang != null && nk.KhachHang.HoTen.Contains(tuKhoa)));
                List<NhatKyEmailHienThi> danhSach = truyVan
                    .OrderByDescending(nk => nk.ThoiGianGui)
                    .ThenByDescending(nk => nk.NhatKyGuiEmailId)
                    .Take(1000)
                    .ToList()
                    .Select(nk => new NhatKyEmailHienThi(nk))
                    .ToList();
                dgvNhatKy.DataSource = danhSach;
                lblSoNhatKy.Text = danhSach.Count + " bản ghi gần nhất";
            }
        }

        private void btnTimNhatKy_Click(object sender, EventArgs e)
        {
            if (dtpTuNgayNhatKy.Checked && dtpDenNgayNhatKy.Checked &&
                dtpTuNgayNhatKy.Value.Date > dtpDenNgayNhatKy.Value.Date)
            {
                HienThiLoi("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
                return;
            }
            try { TaiNhatKy(); }
            catch (Exception) { HienThiLoi("Không thể tải nhật ký gửi email."); }
        }

        private void btnTaiLaiNhatKy_Click(object sender, EventArgs e)
        {
            txtTimNhatKy.Clear();
            dtpTuNgayNhatKy.Checked = false;
            dtpDenNgayNhatKy.Checked = false;
            cboLocLoaiGui.SelectedIndex = 0;
            cboLocTrangThaiNhatKy.SelectedIndex = 0;
            if (cboLocMauNhatKy.Items.Count > 0) cboLocMauNhatKy.SelectedIndex = 0;
            btnTimNhatKy_Click(sender, EventArgs.Empty);
        }

        private void GhiNhatKy(int? khachHangId, int? hoaDonId, int? mauEmailId,
            string emailNhan, string tieuDe, string loaiGui, bool thanhCong, string ghiChu)
        {
            if (!KiemTraPhienDangNhap(false)) throw new InvalidOperationException("Phiên đăng nhập đã kết thúc.");
            using (var db = DatabaseConnection.CreateContext())
            {
                db.NhatKyGuiEmails.Add(new NhatKyGuiEmail
                {
                    TaiKhoanId = CurrentUserSession.HienTai.TaiKhoanId,
                    KhachHangId = khachHangId,
                    HoaDonId = hoaDonId,
                    MauEmailId = mauEmailId,
                    ThoiGianGui = DateTime.Now,
                    EmailNhan = CatChuoi(emailNhan, 254),
                    TieuDe = CatChuoi(tieuDe, 255),
                    LoaiGui = loaiGui,
                    TrangThai = thanhCong ? "THANH_CONG" : "THAT_BAI",
                    GhiChu = CatChuoi(ghiChu, 1000)
                });
                db.SaveChanges();
            }
        }

        #endregion

        private static DuLieuNguoiNhanEmail TaoDuLieuNguoiNhan(
            KhachHangGuiEmail khachHang, HoaDonGuiEmail hoaDon, string email)
        {
            return new DuLieuNguoiNhanEmail
            {
                KhachHangId = khachHang?.KhachHangId,
                HoaDonId = hoaDon?.HoaDonId,
                HoTen = khachHang?.HoTen ?? string.Empty,
                Email = email,
                SoDienThoai = khachHang?.SoDienThoai ?? string.Empty,
                TenSanPham = hoaDon?.TenSanPham ?? string.Empty,
                TongTien = hoaDon?.TongTien ?? string.Empty,
                NgayMua = hoaDon?.NgayMua ?? string.Empty,
                HanBaoHanh = hoaDon?.HanBaoHanh ?? string.Empty,
                MaHoaDon = hoaDon?.HoaDonId.HasValue == true ? hoaDon.MaHoaDon : string.Empty,
                GhiChu = string.Empty
            };
        }

        private static string ChuanHoaLoiGui(Exception ex, CauHinhSmtp cauHinh)
        {
            string loi = ex.GetBaseException().Message;
            if (!string.IsNullOrEmpty(cauHinh.MatKhauUngDung))
                loi = loi.Replace(cauHinh.MatKhauUngDung, "***");
            return CatChuoi(loi, 1000);
        }

        private static string CatChuoi(string giaTri, int doDai)
        {
            if (string.IsNullOrWhiteSpace(giaTri)) return null;
            string ketQua = giaTri.Trim();
            return ketQua.Length <= doDai ? ketQua : ketQua.Substring(0, doDai);
        }

        private void HienThiLoi(string noiDung)
        {
            lblLoi.Text = "* " + noiDung;
            lblLoi.ForeColor = Color.Crimson;
        }

        private sealed class MauEmailHienThi
        {
            public MauEmailHienThi(MauEmail mau)
            {
                MauEmailId = mau.MauEmailId;
                TenMau = mau.TenMau;
                TieuDeMau = mau.TieuDeMau;
                NoiDungMau = mau.NoiDungMau;
                DangHoatDong = mau.DangHoatDong;
            }
            public int MauEmailId { get; }
            public string TenMau { get; }
            public string TieuDeMau { get; }
            public string NoiDungMau { get; }
            public bool DangHoatDong { get; }
            public override string ToString() => TenMau + (DangHoatDong ? string.Empty : " (ngừng dùng)");
        }

        private sealed class LuaChonMauEmail
        {
            public LuaChonMauEmail(int? id, string ten, string tieuDe, string noiDung)
            {
                MauEmailId = id;
                TenMau = ten;
                TieuDeMau = tieuDe;
                NoiDungMau = noiDung;
            }
            public int? MauEmailId { get; }
            public string TenMau { get; }
            public string TieuDeMau { get; }
            public string NoiDungMau { get; }
            public LuaChonMauEmail SaoChep() => new LuaChonMauEmail(MauEmailId, TenMau, TieuDeMau, NoiDungMau);
            public override string ToString() => TenMau;
        }

        private sealed class KhachHangGuiEmail
        {
            private KhachHangGuiEmail() { }
            public KhachHangGuiEmail(KhachHang khachHang)
            {
                KhachHangId = khachHang.KhachHangId;
                HoTen = khachHang.HoTen;
                Email = khachHang.Email;
                SoDienThoai = khachHang.SoDienThoai;
            }
            public int? KhachHangId { get; private set; }
            public string HoTen { get; private set; }
            public string Email { get; private set; }
            public string SoDienThoai { get; private set; }
            public static KhachHangGuiEmail TaoNhapThuCong() => new KhachHangGuiEmail { HoTen = "Nhập email thủ công" };
            public override string ToString() => HoTen + (Email == null ? string.Empty : " - " + Email);
        }

        private sealed class HoaDonGuiEmail
        {
            private HoaDonGuiEmail() { }
            public HoaDonGuiEmail(HoaDon hoaDon)
            {
                HoaDonId = hoaDon.HoaDonId;
                MaHoaDon = $"HD{hoaDon.HoaDonId:000000}";
                NgayMua = hoaDon.NgayLap.ToString("dd/MM/yyyy");
                TongTien = hoaDon.ThanhTien.ToString("#,##0", CultureInfo.GetCultureInfo("vi-VN"));
                TenSanPham = string.Join(", ", hoaDon.ChiTietHoaDons.Select(ct => ct.SanPham.TenSanPham).Distinct());
                DateTime? han = hoaDon.ChiTietHoaDons.Where(ct => ct.HanBaoHanh.HasValue)
                    .Select(ct => ct.HanBaoHanh).OrderBy(value => value).FirstOrDefault();
                HanBaoHanh = han?.ToString("dd/MM/yyyy") ?? string.Empty;
            }
            public int? HoaDonId { get; private set; }
            public string MaHoaDon { get; private set; }
            public string NgayMua { get; private set; }
            public string TongTien { get; private set; }
            public string TenSanPham { get; private set; }
            public string HanBaoHanh { get; private set; }
            public static HoaDonGuiEmail TaoKhongGan() => new HoaDonGuiEmail { MaHoaDon = "Không gắn hóa đơn" };
            public override string ToString() => HoaDonId.HasValue ? MaHoaDon + " - " + NgayMua + " - " + TongTien + " VNĐ" : MaHoaDon;
        }

        private sealed class NguoiNhanHangLoat
        {
            private readonly DuLieuNguoiNhanEmail duLieu;
            public NguoiNhanHangLoat(KhachHang khachHang, HoaDon hoaDon)
            {
                DuocChon = true;
                KhachHangId = khachHang.KhachHangId;
                HoTen = khachHang.HoTen;
                Email = khachHang.Email;
                SoDienThoai = khachHang.SoDienThoai;
                var thongTinHoaDon = hoaDon == null ? null : new HoaDonGuiEmail(hoaDon);
                HoaDonId = thongTinHoaDon?.HoaDonId;
                MaHoaDon = thongTinHoaDon?.MaHoaDon ?? string.Empty;
                TenSanPham = thongTinHoaDon?.TenSanPham ?? string.Empty;
                TongTien = thongTinHoaDon?.TongTien ?? string.Empty;
                NgayMua = thongTinHoaDon?.NgayMua ?? string.Empty;
                HanBaoHanh = thongTinHoaDon?.HanBaoHanh ?? string.Empty;
                duLieu = new DuLieuNguoiNhanEmail
                {
                    KhachHangId = KhachHangId,
                    HoaDonId = HoaDonId,
                    HoTen = HoTen,
                    Email = Email,
                    SoDienThoai = SoDienThoai,
                    TenSanPham = TenSanPham,
                    TongTien = TongTien,
                    NgayMua = NgayMua,
                    HanBaoHanh = HanBaoHanh,
                    MaHoaDon = MaHoaDon
                };
            }
            public bool DuocChon { get; set; }
            public int KhachHangId { get; }
            public int? HoaDonId { get; }
            public string HoTen { get; }
            public string Email { get; }
            public string SoDienThoai { get; }
            public string MaHoaDon { get; }
            public string TenSanPham { get; }
            public string TongTien { get; }
            public string NgayMua { get; }
            public string HanBaoHanh { get; }
            public DuLieuNguoiNhanEmail TaoDuLieu() => duLieu;
        }

        private sealed class NhatKyEmailHienThi
        {
            public NhatKyEmailHienThi(NhatKyGuiEmail nhatKy)
            {
                ThoiGian = nhatKy.ThoiGianGui.ToString("dd/MM/yyyy HH:mm:ss");
                EmailNhan = nhatKy.EmailNhan;
                KhachHang = nhatKy.KhachHang?.HoTen ?? "--";
                HoaDon = nhatKy.HoaDon == null ? "--" : $"HD{nhatKy.HoaDonId:000000}";
                MauEmail = nhatKy.MauEmail?.TenMau ?? "Tự soạn";
                TieuDe = nhatKy.TieuDe;
                LoaiGui = nhatKy.LoaiGui == "DON" ? "Đơn" : "Hàng loạt";
                TrangThai = nhatKy.TrangThai == "THANH_CONG" ? "Thành công" : "Thất bại";
                NguoiGui = nhatKy.TaiKhoan?.NhanVien?.HoTen ?? "--";
                GhiChu = nhatKy.GhiChu;
            }
            public string ThoiGian { get; }
            public string EmailNhan { get; }
            public string KhachHang { get; }
            public string HoaDon { get; }
            public string MauEmail { get; }
            public string TieuDe { get; }
            public string LoaiGui { get; }
            public string TrangThai { get; }
            public string NguoiGui { get; }
            public string GhiChu { get; }
        }
    }
}

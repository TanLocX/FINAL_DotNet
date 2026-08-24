using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmSanPham : Form
    {
        private readonly List<ThanhPhanChatLieuNhap> thanhPhanDangNhap =
            new List<ThanhPhanChatLieuNhap>();
        private int? sanPhamDangChonId;
        private int? danhMucBanDauId;
        private bool dangLamMoiBieuMau;
        private TabPage tabMaQr;
        private PictureBox picMaQr;
        private Label lblNoiDungQr;
        private Label lblKetQuaQr;
        private Button btnLuuMaQr;
        private Button btnDocMaQr;

        public FrmSanPham()
        {
            InitializeComponent();
            KhoiTaoGiaoDienQr();
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTonKho.SelectedIndex = 0;
            cboDonViTinh.SelectedIndex = 0;
            numGiaVon.Maximum = 9999999999999999M;
            numGiaBan.Maximum = 9999999999999999M;
            numSoLuongTon.Maximum = int.MaxValue;
            numTrongLuong.Maximum = 9999999.999M;
            FormClosed += FrmSanPham_FormClosed;
        }

        private void FrmSanPham_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (picMaQr?.Image == null) return;
            Image anh = picMaQr.Image;
            picMaQr.Image = null;
            anh.Dispose();
        }

        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true))
            {
                BeginInvoke(new Action(Close));
                return;
            }

            dangLamMoiBieuMau = true;
            try
            {
                bool duLieuNenSanSang = TaiBoLoc();
                LamMoiBieuMauNoiBo();
                if (duLieuNenSanSang)
                    TaiDanhSach();
                else
                    lblSoKetQua.Text = "0 sản phẩm";
                dgvSanPham.ClearSelection();
                splitChinh.SplitterDistance = Math.Max(
                    splitChinh.Panel1MinSize,
                    Math.Min(205, splitChinh.Height / 2));
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }
        }

        private bool KiemTraPhienDangNhap(bool hienThongBao)
        {
            bool hopLe = CurrentUserSession.DaDangNhap;
            if (!hopLe && hienThongBao)
            {
                MessageBox.Show(
                    "Phiên đăng nhập đã kết thúc. Vui lòng đăng nhập lại.",
                    "Chưa đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return hopLe;
        }

        private bool TaiBoLoc()
        {
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    var danhMuc = db.DanhMucs.AsNoTracking()
                        .OrderBy(dm => dm.TenDanhMuc)
                        .Select(dm => new LuaChonId
                        {
                            Id = dm.DanhMucId,
                            Ten = dm.TenDanhMuc + (dm.DangHoatDong ? "" : " (ngừng hoạt động)"),
                            DangHoatDong = dm.DangHoatDong
                        })
                        .ToList();
                    cboDanhMuc.DataSource = danhMuc.Select(item => item.SaoChep()).ToList();
                    var danhMucLoc = danhMuc.Select(item => item.SaoChep()).ToList();
                    danhMucLoc.Insert(0, new LuaChonId { Id = null, Ten = "Tất cả danh mục", DangHoatDong = true });
                    cboLocDanhMuc.DataSource = danhMucLoc;

                    var chatLieu = db.ChatLieux.AsNoTracking()
                        .OrderBy(cl => cl.TenChatLieu)
                        .Select(cl => new LuaChonId
                        {
                            Id = cl.ChatLieuId,
                            Ten = cl.TenChatLieu + (cl.DangHoatDong ? "" : " (ngừng hoạt động)"),
                            DangHoatDong = cl.DangHoatDong
                        })
                        .ToList();
                    cboChatLieu.DataSource = chatLieu.Select(item => item.SaoChep()).ToList();
                    var chatLieuLoc = chatLieu.Select(item => item.SaoChep()).ToList();
                    chatLieuLoc.Insert(0, new LuaChonId { Id = null, Ten = "Tất cả chất liệu", DangHoatDong = true });
                    cboLocChatLieu.DataSource = chatLieuLoc;
                }
                return true;
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh mục và chất liệu. Hãy kiểm tra kết nối CSDL.");
                return false;
            }
        }

        private static void ChonGiaTri(ComboBox comboBox, int? id)
        {
            comboBox.SelectedIndex = -1;
            if (!id.HasValue) return;
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                var item = comboBox.Items[i] as LuaChonId;
                if (item?.Id == id)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }

        private void TaiDanhSach(int? sanPhamCanChonId = null)
        {
            decimal? giaTu;
            decimal? giaDen;
            if (!ThuLayKhoangGia(out giaTu, out giaDen)) return;

            try
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                int? maSanPham = ThuDocMaSanPham(tuKhoa);
                int? danhMucId = (cboLocDanhMuc.SelectedItem as LuaChonId)?.Id;
                int? chatLieuId = (cboLocChatLieu.SelectedItem as LuaChonId)?.Id;
                int trangThai = cboLocTrangThai.SelectedIndex;
                int tonKho = cboLocTonKho.SelectedIndex;

                using (var db = DatabaseConnection.CreateContext())
                {
                    IQueryable<SanPham> truyVan = db.SanPhams
                        .Include(sp => sp.DanhMuc)
                        .Include(sp => sp.ChiTietChatLieux.Select(ct => ct.ChatLieu))
                        .AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(tuKhoa))
                    {
                        if (maSanPham.HasValue)
                            truyVan = truyVan.Where(sp => sp.SanPhamId == maSanPham.Value);
                        else
                            truyVan = truyVan.Where(sp => sp.TenSanPham.Contains(tuKhoa));
                    }
                    if (danhMucId.HasValue)
                        truyVan = truyVan.Where(sp => sp.DanhMucId == danhMucId.Value);
                    if (chatLieuId.HasValue)
                        truyVan = truyVan.Where(sp => sp.ChiTietChatLieux.Any(ct => ct.ChatLieuId == chatLieuId.Value));
                    if (giaTu.HasValue)
                        truyVan = truyVan.Where(sp => sp.GiaBan >= giaTu.Value);
                    if (giaDen.HasValue)
                        truyVan = truyVan.Where(sp => sp.GiaBan <= giaDen.Value);
                    if (trangThai == 1)
                        truyVan = truyVan.Where(sp => sp.DangKinhDoanh);
                    else if (trangThai == 2)
                        truyVan = truyVan.Where(sp => !sp.DangKinhDoanh);
                    if (tonKho == 1)
                        truyVan = truyVan.Where(sp => sp.SoLuongTon > 0);
                    else if (tonKho == 2)
                        truyVan = truyVan.Where(sp => sp.SoLuongTon == 0);
                    else if (tonKho == 3)
                        truyVan = truyVan.Where(sp => sp.SoLuongTon > 0 && sp.SoLuongTon <= 5);

                    var sanPhamCoPhatSinh = new HashSet<int>(
                        db.ChiTietHoaDons.Select(ct => ct.SanPhamId)
                            .Union(db.ChiTietPhieuNhaps.Select(ct => ct.SanPhamId))
                            .Union(db.ChiTietPhieuThuMuas
                                .Where(ct => ct.SanPhamId.HasValue)
                                .Select(ct => ct.SanPhamId.Value))
                            .Distinct()
                            .ToList());
                    List<SanPhamHienThi> danhSach = truyVan
                        .OrderByDescending(sp => sp.DangKinhDoanh)
                        .ThenBy(sp => sp.SanPhamId)
                        .ToList()
                        .Select(sp => new SanPhamHienThi(sp, sanPhamCoPhatSinh.Contains(sp.SanPhamId)))
                        .ToList();
                    dgvSanPham.DataSource = danhSach;
                    lblSoKetQua.Text = danhSach.Count + " sản phẩm";
                }

                if (sanPhamCanChonId.HasValue) ChonDong(sanPhamCanChonId.Value);
            }
            catch (Exception)
            {
                HienThiLoi("Không thể tải danh sách sản phẩm. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private bool ThuLayKhoangGia(out decimal? giaTu, out decimal? giaDen)
        {
            giaTu = null;
            giaDen = null;
            decimal gia;
            if (!string.IsNullOrWhiteSpace(txtGiaTu.Text))
            {
                if (!decimal.TryParse(txtGiaTu.Text.Trim(), out gia) || gia < 0)
                {
                    HienThiLoi("Giá bán từ phải là số không âm.");
                    return false;
                }
                giaTu = gia;
            }
            if (!string.IsNullOrWhiteSpace(txtGiaDen.Text))
            {
                if (!decimal.TryParse(txtGiaDen.Text.Trim(), out gia) || gia < 0)
                {
                    HienThiLoi("Giá bán đến phải là số không âm.");
                    return false;
                }
                giaDen = gia;
            }
            if (giaTu.HasValue && giaDen.HasValue && giaTu.Value > giaDen.Value)
            {
                HienThiLoi("Giá bán từ không được lớn hơn giá bán đến.");
                return false;
            }
            lblThongBao.Text = string.Empty;
            return true;
        }

        private static int? ThuDocMaSanPham(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return null;
            string giaTri = tuKhoa.Trim();
            if (giaTri.StartsWith("SP", StringComparison.OrdinalIgnoreCase))
                giaTri = giaTri.Substring(2);
            int id;
            return int.TryParse(giaTri, out id) && id > 0 ? (int?)id : null;
        }

        private void ChonDong(int sanPhamId)
        {
            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                var item = row.DataBoundItem as SanPhamHienThi;
                if (item?.SanPhamId != sanPhamId) continue;
                row.Selected = true;
                dgvSanPham.CurrentCell = row.Cells[0];
                break;
            }
        }

        private void dgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoiBieuMau) return;
            var item = dgvSanPham.CurrentRow?.DataBoundItem as SanPhamHienThi;
            if (item == null) return;

            dangLamMoiBieuMau = true;
            try
            {
                sanPhamDangChonId = item.SanPhamId;
                danhMucBanDauId = item.DanhMucId;
                txtMaSanPham.Text = item.MaSanPham;
                txtMaVach.Text = item.MaSanPham;
                txtTenSanPham.Text = item.TenSanPham;
                ChonGiaTri(cboDanhMuc, item.DanhMucId);
                numGiaVon.Value = GioiHan(numGiaVon, item.GiaVon);
                numGiaBan.Value = GioiHan(numGiaBan, item.GiaBan);
                numSoLuongTon.Value = GioiHan(numSoLuongTon, item.SoLuongTon);
                txtDuongDanAnh.Text = item.DuongDanAnh ?? string.Empty;
                chkDangKinhDoanh.Checked = item.DangKinhDoanh;
                btnXoaHoacTrangThai.Text = item.DangKinhDoanh
                    ? (item.CoPhatSinh ? "Ngừng kinh doanh" : "Xóa sản phẩm")
                    : "Khôi phục";

                thanhPhanDangNhap.Clear();
                thanhPhanDangNhap.AddRange(item.ThanhPhan.Select(tp => tp.SaoChep()));
                TaiLuoiThanhPhan();
                LamMoiNhapThanhPhan();
                HienThiAnh(item.DuongDanAnh);
                HienThiMaQr(item.MaSanPham);
                tabBieuMau.SelectedTab = tabThongTin;
                lblThongBao.Text = string.Empty;
            }
            finally
            {
                dangLamMoiBieuMau = false;
            }
        }

        private static decimal GioiHan(NumericUpDown control, decimal value)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, value));
        }

        private void btnTimKiem_Click(object sender, EventArgs e) => TaiDanhSach();

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            TaiDanhSach();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            txtGiaTu.Clear();
            txtGiaDen.Clear();
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTonKho.SelectedIndex = 0;
            if (cboLocDanhMuc.Items.Count > 0) cboLocDanhMuc.SelectedIndex = 0;
            if (cboLocChatLieu.Items.Count > 0) cboLocChatLieu.SelectedIndex = 0;
            TaiDanhSach();
            LamMoiBieuMau();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            ThongTinSanPhamNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu)) return;

            try
            {
                int idMoi;
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction())
                {
                    var sanPham = new SanPham
                    {
                        DanhMucId = duLieu.DanhMucId,
                        TenSanPham = duLieu.TenSanPham,
                        GiaVon = duLieu.GiaVon,
                        GiaBan = duLieu.GiaBan,
                        SoLuongTon = duLieu.SoLuongTon,
                        DuongDanAnh = duLieu.DuongDanAnh,
                        DangKinhDoanh = true
                    };
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    LuuThanhPhan(db, sanPham.SanPhamId);
                    db.SaveChanges();
                    transaction.Commit();
                    idMoi = sanPham.SanPhamId;
                }
                TaiDanhSach(idMoi);
                MessageBox.Show($"Đã thêm sản phẩm SP{idMoi:000000}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể lưu sản phẩm. Dữ liệu có thể không còn hợp lệ.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thêm sản phẩm. Hãy kiểm tra kết nối CSDL.");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !sanPhamDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn sản phẩm cần cập nhật.");
                return;
            }
            ThongTinSanPhamNhap duLieu;
            if (!ThuLayDuLieuNhap(out duLieu)) return;
            int id = sanPhamDangChonId.Value;

            try
            {
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction())
                {
                    var sanPham = db.SanPhams.SingleOrDefault(sp => sp.SanPhamId == id);
                    if (sanPham == null)
                    {
                        HienThiLoi("Sản phẩm không còn tồn tại trong CSDL.");
                        return;
                    }
                    sanPham.DanhMucId = duLieu.DanhMucId;
                    sanPham.TenSanPham = duLieu.TenSanPham;
                    sanPham.GiaVon = duLieu.GiaVon;
                    sanPham.GiaBan = duLieu.GiaBan;
                    sanPham.SoLuongTon = duLieu.SoLuongTon;
                    sanPham.DuongDanAnh = duLieu.DuongDanAnh;

                    var thanhPhanCu = db.ChiTietChatLieux.Where(ct => ct.SanPhamId == id).ToList();
                    db.ChiTietChatLieux.RemoveRange(thanhPhanCu);
                    db.SaveChanges();
                    LuuThanhPhan(db, id);
                    db.SaveChanges();
                    transaction.Commit();
                }
                TaiDanhSach(id);
                MessageBox.Show("Đã cập nhật sản phẩm và thành phần chất liệu.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Không thể cập nhật sản phẩm. Dữ liệu có thể đã được sử dụng hoặc thay đổi.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể cập nhật sản phẩm.");
            }
        }

        private void LuuThanhPhan(QL_CuaHangDaQuy_PNJEntities db, int sanPhamId)
        {
            foreach (var item in thanhPhanDangNhap)
            {
                db.ChiTietChatLieux.Add(new ChiTietChatLieu
                {
                    SanPhamId = sanPhamId,
                    ChatLieuId = item.ChatLieuId,
                    TrongLuong = item.TrongLuong,
                    DonViTinh = item.DonViTinh
                });
            }
        }

        private void btnXoaHoacTrangThai_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true) || !sanPhamDangChonId.HasValue)
            {
                HienThiLoi("Vui lòng chọn sản phẩm cần xử lý.");
                return;
            }
            int id = sanPhamDangChonId.Value;
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                using (var transaction = db.Database.BeginTransaction())
                {
                    var sanPham = db.SanPhams.SingleOrDefault(sp => sp.SanPhamId == id);
                    if (sanPham == null)
                    {
                        HienThiLoi("Sản phẩm không còn tồn tại trong CSDL.");
                        return;
                    }
                    bool coPhatSinh = db.ChiTietHoaDons.Any(ct => ct.SanPhamId == id)
                        || db.ChiTietPhieuNhaps.Any(ct => ct.SanPhamId == id)
                        || db.ChiTietPhieuThuMuas.Any(ct => ct.SanPhamId == id);
                    string hanhDong = !sanPham.DangKinhDoanh ? "khôi phục"
                        : coPhatSinh ? "ngừng kinh doanh" : "xóa";
                    if (MessageBox.Show(
                        $"Bạn có chắc muốn {hanhDong} sản phẩm {sanPham.TenSanPham}?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;

                    if (!sanPham.DangKinhDoanh)
                    {
                        bool danhMucHoatDong = db.DanhMucs.Any(dm => dm.DanhMucId == sanPham.DanhMucId && dm.DangHoatDong);
                        if (!danhMucHoatDong)
                        {
                            HienThiLoi("Không thể khôi phục khi danh mục của sản phẩm đang ngừng hoạt động.");
                            return;
                        }
                        sanPham.DangKinhDoanh = true;
                    }
                    else if (coPhatSinh)
                    {
                        sanPham.DangKinhDoanh = false;
                    }
                    else
                    {
                        db.ChiTietChatLieux.RemoveRange(db.ChiTietChatLieux.Where(ct => ct.SanPhamId == id));
                        db.SanPhams.Remove(sanPham);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
                TaiDanhSach(sanPhamDangChonId);
                if (dgvSanPham.CurrentRow == null) LamMoiBieuMau();
            }
            catch (DbUpdateException)
            {
                HienThiLoi("Sản phẩm đã phát sinh tham chiếu và không thể xóa. Hãy tải lại rồi ngừng kinh doanh.");
            }
            catch (Exception)
            {
                HienThiLoi("Không thể thay đổi trạng thái sản phẩm.");
            }
        }

        private bool ThuLayDuLieuNhap(out ThongTinSanPhamNhap duLieu)
        {
            duLieu = null;
            string ten = txtTenSanPham.Text.Trim();
            var danhMuc = cboDanhMuc.SelectedItem as LuaChonId;
            if (string.IsNullOrWhiteSpace(ten))
            {
                HienThiLoi("Tên sản phẩm không được để trống.");
                tabBieuMau.SelectedTab = tabThongTin;
                txtTenSanPham.Focus();
                return false;
            }
            if (danhMuc?.Id == null)
            {
                HienThiLoi("Vui lòng chọn danh mục sản phẩm.");
                tabBieuMau.SelectedTab = tabThongTin;
                cboDanhMuc.Focus();
                return false;
            }
            if (!danhMuc.DangHoatDong && danhMuc.Id != danhMucBanDauId)
            {
                HienThiLoi("Không thể chuyển sản phẩm sang danh mục đã ngừng hoạt động.");
                return false;
            }
            string duongDan = ChuanHoaTuyChon(txtDuongDanAnh.Text);
            if (duongDan != null && Path.IsPathRooted(duongDan))
            {
                HienThiLoi("Đường dẫn ảnh phải là đường dẫn tương đối trong dự án.");
                return false;
            }
            duLieu = new ThongTinSanPhamNhap
            {
                DanhMucId = danhMuc.Id.Value,
                TenSanPham = ten,
                GiaVon = numGiaVon.Value,
                GiaBan = numGiaBan.Value,
                SoLuongTon = Decimal.ToInt32(numSoLuongTon.Value),
                DuongDanAnh = duongDan
            };
            lblThongBao.Text = string.Empty;
            return true;
        }

        private void btnLamMoiBieuMau_Click(object sender, EventArgs e) => LamMoiBieuMau();

        private void LamMoiBieuMau()
        {
            dangLamMoiBieuMau = true;
            try { LamMoiBieuMauNoiBo(); }
            finally { dangLamMoiBieuMau = false; }
            txtTenSanPham.Focus();
        }

        private void LamMoiBieuMauNoiBo()
        {
            sanPhamDangChonId = null;
            danhMucBanDauId = null;
            txtMaSanPham.Text = "Tự động tạo";
            txtMaVach.Text = "Tạo sau khi lưu";
            txtTenSanPham.Clear();
            if (cboDanhMuc.Items.Count > 0)
            {
                int viTri = -1;
                for (int i = 0; i < cboDanhMuc.Items.Count; i++)
                    if ((cboDanhMuc.Items[i] as LuaChonId)?.DangHoatDong == true) { viTri = i; break; }
                cboDanhMuc.SelectedIndex = viTri;
            }
            numGiaVon.Value = 0;
            numGiaBan.Value = 0;
            numSoLuongTon.Value = 0;
            txtDuongDanAnh.Clear();
            chkDangKinhDoanh.Checked = true;
            btnXoaHoacTrangThai.Text = "Xóa sản phẩm";
            thanhPhanDangNhap.Clear();
            TaiLuoiThanhPhan();
            LamMoiNhapThanhPhan();
            HienThiAnh(null);
            HienThiMaQr(null);
            lblThongBao.Text = string.Empty;
            tabBieuMau.SelectedTab = tabThongTin;
            dgvSanPham.ClearSelection();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh sản phẩm đã có trong dự án",
                Filter = "Tệp ảnh|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Tất cả tệp|*.*"
            })
            {
                string thuMucDuAn = TimThuMucDuAn();
                if (thuMucDuAn != null) dialog.InitialDirectory = Path.Combine(thuMucDuAn, "Resources");
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                if (thuMucDuAn == null || !NamTrongThuMuc(dialog.FileName, thuMucDuAn))
                {
                    HienThiLoi("Hãy đưa ảnh vào thư mục dự án (khuyến nghị Resources) rồi chọn lại.");
                    return;
                }
                txtDuongDanAnh.Text = LayDuongDanTuongDoi(thuMucDuAn, dialog.FileName);
                HienThiAnh(txtDuongDanAnh.Text);
            }
        }

        private void txtDuongDanAnh_Leave(object sender, EventArgs e) => HienThiAnh(txtDuongDanAnh.Text.Trim());

        private void KhoiTaoGiaoDienQr()
        {
            tabMaQr = new TabPage
            {
                Text = "QR sản phẩm",
                BackColor = Color.White,
                Padding = new Padding(12)
            };
            picMaQr = new PictureBox
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                Margin = new Padding(16),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            lblNoiDungQr = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 78),
                Text = "Chưa chọn sản phẩm",
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblKetQuaQr = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.DimGray,
                Text = "Chọn sản phẩm để sinh QR. Có thể lưu QR thành PNG hoặc đọc lại từ một file ảnh.",
                TextAlign = ContentAlignment.TopLeft
            };
            btnLuuMaQr = new Button();
            btnDocMaQr = new Button();
            CauHinhNut(btnLuuMaQr, "Lưu QR PNG", 0, 0, 125, MauXanh());
            CauHinhNut(btnDocMaQr, "Đọc QR từ ảnh", 0, 0, 145, Color.FromArgb(45, 91, 123));
            btnLuuMaQr.Enabled = false;
            btnLuuMaQr.Click += btnLuuMaQr_Click;
            btnDocMaQr.Click += btnDocMaQr_Click;

            var nut = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };
            btnLuuMaQr.Margin = new Padding(0, 0, 10, 0);
            btnDocMaQr.Margin = new Padding(0);
            nut.Controls.Add(btnLuuMaQr);
            nut.Controls.Add(btnDocMaQr);

            var thongTin = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, Padding = new Padding(12) };
            thongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            thongTin.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            thongTin.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            thongTin.Controls.Add(lblNoiDungQr, 0, 0);
            thongTin.Controls.Add(nut, 0, 1);
            thongTin.Controls.Add(lblKetQuaQr, 0, 2);

            var boCuc = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
            boCuc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            boCuc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            boCuc.Controls.Add(picMaQr, 0, 0);
            boCuc.Controls.Add(thongTin, 1, 0);
            tabMaQr.Controls.Add(boCuc);
            tabBieuMau.Controls.Add(tabMaQr);
        }

        private void HienThiMaQr(string maSanPham)
        {
            if (picMaQr.Image != null)
            {
                Image anhCu = picMaQr.Image;
                picMaQr.Image = null;
                anhCu.Dispose();
            }
            bool coMa = !string.IsNullOrWhiteSpace(maSanPham) && maSanPham.StartsWith("SP", StringComparison.OrdinalIgnoreCase);
            btnLuuMaQr.Enabled = coMa;
            lblNoiDungQr.Text = coMa ? "Nội dung QR: " + maSanPham : "Chưa chọn sản phẩm";
            lblKetQuaQr.ForeColor = Color.DimGray;
            lblKetQuaQr.Text = coMa
                ? "QR được sinh từ mã sản phẩm. Nhấn Lưu QR PNG để dùng khi demo hoặc Đọc QR từ ảnh để tìm lại sản phẩm."
                : "Chọn sản phẩm để sinh QR. Có thể lưu QR thành PNG hoặc đọc lại từ một file ảnh.";
            if (!coMa) return;

            try
            {
                picMaQr.Image = QrCodeService.TaoMaQr(maSanPham, 360);
            }
            catch (Exception)
            {
                btnLuuMaQr.Enabled = false;
                lblKetQuaQr.ForeColor = Color.Firebrick;
                lblKetQuaQr.Text = "Không thể sinh QR cho sản phẩm đã chọn.";
            }
        }

        private void btnLuuMaQr_Click(object sender, EventArgs e)
        {
            if (picMaQr.Image == null || !sanPhamDangChonId.HasValue)
            {
                lblKetQuaQr.ForeColor = Color.Firebrick;
                lblKetQuaQr.Text = "Vui lòng chọn sản phẩm trước khi lưu QR.";
                return;
            }
            using (var dialog = new SaveFileDialog
            {
                Title = "Lưu QR sản phẩm",
                Filter = "Ảnh PNG (*.png)|*.png",
                DefaultExt = "png",
                AddExtension = true,
                FileName = "QR_" + txtMaVach.Text + ".png"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    using (var anh = new Bitmap(picMaQr.Image))
                        anh.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    lblKetQuaQr.ForeColor = Color.FromArgb(30, 115, 75);
                    lblKetQuaQr.Text = "Đã lưu QR tại: " + dialog.FileName;
                }
                catch (Exception)
                {
                    lblKetQuaQr.ForeColor = Color.Firebrick;
                    lblKetQuaQr.Text = "Không thể lưu file QR. Hãy chọn thư mục khác rồi thử lại.";
                }
            }
        }

        private void btnDocMaQr_Click(object sender, EventArgs e)
        {
            if (!KiemTraPhienDangNhap(true)) return;
            using (var dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh QR sản phẩm",
                Filter = "Tệp ảnh|*.png;*.jpg;*.jpeg;*.bmp|Tất cả tệp|*.*"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    if (new FileInfo(dialog.FileName).Length > 20 * 1024 * 1024)
                        throw new InvalidOperationException("Ảnh QR không được lớn hơn 20 MB.");

                    string noiDung;
                    using (var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var anhNguon = Image.FromStream(stream))
                    using (var anh = new Bitmap(anhNguon))
                        noiDung = QrCodeService.DocMaQr(anh);

                    int? sanPhamId = ThuDocMaSanPhamTuQr(noiDung);
                    if (!sanPhamId.HasValue)
                        throw new InvalidOperationException("Ảnh không chứa QR sản phẩm hợp lệ dạng SP000001.");

                    using (var db = DatabaseConnection.CreateContext())
                    {
                        if (!db.SanPhams.AsNoTracking().Any(item => item.SanPhamId == sanPhamId.Value))
                            throw new InvalidOperationException("QR chứa mã sản phẩm không tồn tại trong CSDL.");
                    }

                    XoaBoLocSanPham();
                    txtTuKhoa.Text = noiDung.ToUpperInvariant();
                    TaiDanhSach(sanPhamId.Value);
                    tabBieuMau.SelectedTab = tabMaQr;
                    lblKetQuaQr.ForeColor = Color.FromArgb(30, 115, 75);
                    lblKetQuaQr.Text = "Đọc thành công " + noiDung.ToUpperInvariant() + " và đã chọn đúng sản phẩm.";
                }
                catch (InvalidOperationException ex)
                {
                    lblKetQuaQr.ForeColor = Color.Firebrick;
                    lblKetQuaQr.Text = ex.Message;
                }
                catch (Exception)
                {
                    lblKetQuaQr.ForeColor = Color.Firebrick;
                    lblKetQuaQr.Text = "Không thể đọc ảnh QR đã chọn.";
                }
            }
        }

        private static int? ThuDocMaSanPhamTuQr(string noiDung)
        {
            if (string.IsNullOrWhiteSpace(noiDung)) return null;
            string value = noiDung.Trim();
            if (!value.StartsWith("SP", StringComparison.OrdinalIgnoreCase) || value.Length <= 2) return null;
            string digits = value.Substring(2);
            if (digits.Any(character => character < '0' || character > '9')) return null;
            int id;
            return int.TryParse(digits, out id) && id > 0 ? (int?)id : null;
        }

        private void XoaBoLocSanPham()
        {
            txtGiaTu.Clear();
            txtGiaDen.Clear();
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTonKho.SelectedIndex = 0;
            if (cboLocDanhMuc.Items.Count > 0) cboLocDanhMuc.SelectedIndex = 0;
            if (cboLocChatLieu.Items.Count > 0) cboLocChatLieu.SelectedIndex = 0;
        }

        private void HienThiAnh(string duongDan)
        {
            if (picSanPham.Image != null)
            {
                var cu = picSanPham.Image;
                picSanPham.Image = null;
                cu.Dispose();
            }
            string tepAnh = TimTepAnh(duongDan);
            if (tepAnh == null)
            {
                lblChuaCoAnh.Visible = true;
                return;
            }
            try
            {
                using (var stream = new FileStream(tepAnh, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var anh = Image.FromStream(stream))
                    picSanPham.Image = new Bitmap(anh);
                lblChuaCoAnh.Visible = false;
            }
            catch
            {
                lblChuaCoAnh.Visible = true;
            }
        }

        private static string TimTepAnh(string duongDan)
        {
            if (string.IsNullOrWhiteSpace(duongDan) || Path.IsPathRooted(duongDan)) return null;
            string chuanHoa = duongDan.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            string ungVien = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, chuanHoa));
            if (File.Exists(ungVien)) return ungVien;
            string thuMucDuAn = TimThuMucDuAn();
            if (thuMucDuAn == null) return null;
            ungVien = Path.GetFullPath(Path.Combine(thuMucDuAn, chuanHoa));
            return File.Exists(ungVien) ? ungVien : null;
        }

        private static string TimThuMucDuAn()
        {
            var thuMuc = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (thuMuc != null)
            {
                if (File.Exists(Path.Combine(thuMuc.FullName, "FINAL_DotNet.csproj"))) return thuMuc.FullName;
                thuMuc = thuMuc.Parent;
            }
            return null;
        }

        private static bool NamTrongThuMuc(string tep, string thuMuc)
        {
            string goc = Path.GetFullPath(thuMuc).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string dayDu = Path.GetFullPath(tep);
            return dayDu.StartsWith(goc, StringComparison.OrdinalIgnoreCase);
        }

        private static string LayDuongDanTuongDoi(string thuMuc, string tep)
        {
            var goc = new Uri(Path.GetFullPath(thuMuc).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar);
            var dich = new Uri(Path.GetFullPath(tep));
            return Uri.UnescapeDataString(goc.MakeRelativeUri(dich).ToString()).Replace('/', '\\');
        }

        private void btnLuuThanhPhan_Click(object sender, EventArgs e)
        {
            var chatLieu = cboChatLieu.SelectedItem as LuaChonId;
            string donVi = cboDonViTinh.Text.Trim();
            if (chatLieu?.Id == null)
            {
                HienThiLoi("Vui lòng chọn chất liệu.");
                return;
            }
            var daCo = thanhPhanDangNhap.SingleOrDefault(tp => tp.ChatLieuId == chatLieu.Id.Value);
            if (!chatLieu.DangHoatDong && daCo == null)
            {
                HienThiLoi("Không thể thêm chất liệu đã ngừng hoạt động.");
                return;
            }
            if (numTrongLuong.Value <= 0)
            {
                HienThiLoi("Trọng lượng phải lớn hơn 0.");
                return;
            }
            if (string.IsNullOrWhiteSpace(donVi))
            {
                HienThiLoi("Đơn vị tính không được để trống.");
                return;
            }
            if (donVi.Length > 20)
            {
                HienThiLoi("Đơn vị tính không được vượt quá 20 ký tự.");
                return;
            }
            if (daCo == null)
            {
                thanhPhanDangNhap.Add(new ThanhPhanChatLieuNhap
                {
                    ChatLieuId = chatLieu.Id.Value,
                    MaChatLieu = $"CL{chatLieu.Id.Value:000000}",
                    TenChatLieu = chatLieu.Ten.Replace(" (ngừng hoạt động)", string.Empty),
                    TrongLuong = numTrongLuong.Value,
                    DonViTinh = donVi
                });
            }
            else
            {
                daCo.TrongLuong = numTrongLuong.Value;
                daCo.DonViTinh = donVi;
            }
            TaiLuoiThanhPhan();
            LamMoiNhapThanhPhan();
            lblThongBao.Text = string.Empty;
        }

        private void dgvThanhPhan_SelectionChanged(object sender, EventArgs e)
        {
            if (dangLamMoiBieuMau) return;
            var item = dgvThanhPhan.CurrentRow?.DataBoundItem as ThanhPhanChatLieuNhap;
            if (item == null) return;
            ChonGiaTri(cboChatLieu, item.ChatLieuId);
            numTrongLuong.Value = GioiHan(numTrongLuong, item.TrongLuong);
            cboDonViTinh.Text = item.DonViTinh;
            btnLuuThanhPhan.Text = "Cập nhật thành phần";
        }

        private void btnXoaThanhPhan_Click(object sender, EventArgs e)
        {
            var item = dgvThanhPhan.CurrentRow?.DataBoundItem as ThanhPhanChatLieuNhap;
            if (item == null)
            {
                HienThiLoi("Vui lòng chọn thành phần cần xóa.");
                return;
            }
            thanhPhanDangNhap.RemoveAll(tp => tp.ChatLieuId == item.ChatLieuId);
            TaiLuoiThanhPhan();
            LamMoiNhapThanhPhan();
        }

        private void btnMoiThanhPhan_Click(object sender, EventArgs e) => LamMoiNhapThanhPhan();

        private void TaiLuoiThanhPhan()
        {
            dgvThanhPhan.DataSource = null;
            dgvThanhPhan.DataSource = thanhPhanDangNhap.Select(tp => tp.SaoChep()).ToList();
            lblSoThanhPhan.Text = thanhPhanDangNhap.Count + " thành phần";
            dgvThanhPhan.ClearSelection();
        }

        private void LamMoiNhapThanhPhan()
        {
            dgvThanhPhan.ClearSelection();
            int viTri = -1;
            for (int i = 0; i < cboChatLieu.Items.Count; i++)
                if ((cboChatLieu.Items[i] as LuaChonId)?.DangHoatDong == true) { viTri = i; break; }
            cboChatLieu.SelectedIndex = viTri;
            numTrongLuong.Value = 0.001M;
            cboDonViTinh.Text = "Gram";
            btnLuuThanhPhan.Text = "Thêm thành phần";
        }

        private static string ChuanHoaTuyChon(string giaTri)
        {
            string ketQua = (giaTri ?? string.Empty).Trim();
            return ketQua.Length == 0 ? null : ketQua;
        }

        private void HienThiLoi(string noiDung)
        {
            lblThongBao.Text = "* " + noiDung;
        }

        private sealed class LuaChonId
        {
            public int? Id { get; set; }
            public string Ten { get; set; }
            public bool DangHoatDong { get; set; }
            public LuaChonId SaoChep() => (LuaChonId)MemberwiseClone();
            public override string ToString() => Ten;
        }

        private sealed class ThongTinSanPhamNhap
        {
            public int DanhMucId { get; set; }
            public string TenSanPham { get; set; }
            public decimal GiaVon { get; set; }
            public decimal GiaBan { get; set; }
            public int SoLuongTon { get; set; }
            public string DuongDanAnh { get; set; }
        }

        private sealed class ThanhPhanChatLieuNhap
        {
            public int ChatLieuId { get; set; }
            public string MaChatLieu { get; set; }
            public string TenChatLieu { get; set; }
            public decimal TrongLuong { get; set; }
            public string DonViTinh { get; set; }
            public ThanhPhanChatLieuNhap SaoChep() => (ThanhPhanChatLieuNhap)MemberwiseClone();
        }

        private sealed class SanPhamHienThi
        {
            public SanPhamHienThi(SanPham sanPham, bool coPhatSinh)
            {
                SanPhamId = sanPham.SanPhamId;
                MaSanPham = $"SP{sanPham.SanPhamId:000000}";
                DanhMucId = sanPham.DanhMucId;
                TenSanPham = sanPham.TenSanPham;
                TenDanhMuc = sanPham.DanhMuc?.TenDanhMuc;
                GiaVon = sanPham.GiaVon;
                GiaBan = sanPham.GiaBan;
                SoLuongTon = sanPham.SoLuongTon;
                DuongDanAnh = sanPham.DuongDanAnh;
                DangKinhDoanh = sanPham.DangKinhDoanh;
                TrangThai = sanPham.DangKinhDoanh ? "Đang kinh doanh" : "Ngừng kinh doanh";
                CoPhatSinh = coPhatSinh;
                ThanhPhan = sanPham.ChiTietChatLieux
                    .OrderBy(ct => ct.ChatLieu.TenChatLieu)
                    .Select(ct => new ThanhPhanChatLieuNhap
                    {
                        ChatLieuId = ct.ChatLieuId,
                        MaChatLieu = $"CL{ct.ChatLieuId:000000}",
                        TenChatLieu = ct.ChatLieu.TenChatLieu,
                        TrongLuong = ct.TrongLuong,
                        DonViTinh = ct.DonViTinh
                    }).ToList();
                TomTatChatLieu = ThanhPhan.Count == 0
                    ? "Chưa khai báo"
                    : string.Join(", ", ThanhPhan.Select(tp => tp.TenChatLieu));
            }
            public int SanPhamId { get; }
            public string MaSanPham { get; }
            public int DanhMucId { get; }
            public string TenSanPham { get; }
            public string TenDanhMuc { get; }
            public decimal GiaVon { get; }
            public decimal GiaBan { get; }
            public int SoLuongTon { get; }
            public string DuongDanAnh { get; }
            public bool DangKinhDoanh { get; }
            public string TrangThai { get; }
            public bool CoPhatSinh { get; }
            public string TomTatChatLieu { get; }
            public List<ThanhPhanChatLieuNhap> ThanhPhan { get; }
        }
    }
}

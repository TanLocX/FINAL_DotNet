using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace FINAL_DotNet
{
    public partial class FrmMain : Form
    {
        private ThongTinPhienDangNhap phienDangNhap;
        private readonly List<Control> mucQuanTri = new List<Control>();
        private Form formConHienTai;
        private bool dangXuat;

        public FrmMain()
        {
            InitializeComponent();
            LuxuryDarkGoldTheme.Apply(this);
        }

        public bool DaYeuCauDangXuat => dangXuat;

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!CurrentUserSession.DaDangNhap)
            {
                MessageBox.Show("Phiên đăng nhập đã kết thúc. Vui lòng đăng nhập lại.", "Chưa đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeginInvoke(new Action(Close));
                return;
            }
            phienDangNhap = CurrentUserSession.HienTai;
            string tenHienThi = phienDangNhap.LaQuanTriVien ? "Admin" : phienDangNhap.HoTen;

            lblHoTen.Text = tenHienThi;
            lblTenDangNhap.Text = "@" + phienDangNhap.TenDangNhap;
            lblVaiTro.Text = phienDangNhap.LaQuanTriVien ? "QUẢN TRỊ VIÊN" : "NHÂN VIÊN";
            lblChaoMung.Text = "Xin chào, " + tenHienThi;

            TaoMenu();
            ApDungPhanQuyen();
            HienThiTongQuan();
        }

        private void TaoMenu()
        {
            flowMenu.Controls.Clear();
            mucQuanTri.Clear();

            FlowLayoutPanel nhomTongQuan = ThemNhomMenu("TỔNG QUAN", false, true);
            ThemNutMenu(nhomTongQuan, "Tổng quan", "Tổng quan hệ thống", false);

            FlowLayoutPanel nhomKinhDoanh = ThemNhomMenu("KINH DOANH", false, false);
            ThemNutMenu(nhomKinhDoanh, "Bán hàng", "Bán hàng", false);
            ThemNutMenu(nhomKinhDoanh, "Hóa đơn", "Quản lý hóa đơn", false);
            ThemNutMenu(nhomKinhDoanh, "Khách hàng", "Quản lý khách hàng", false);

            FlowLayoutPanel nhomHangHoa = ThemNhomMenu("HÀNG HÓA & DỊCH VỤ", false, false);
            ThemNutMenu(nhomHangHoa, "Sản phẩm", "Quản lý sản phẩm", false);
            ThemNutMenu(nhomHangHoa, "Nhập hàng", "Nhập hàng", false);
            ThemNutMenu(nhomHangHoa, "Thu mua Excel", "Import và tra cứu thu mua", false);
            ThemNutMenu(nhomHangHoa, "Bảo hành", "Quản lý bảo hành", false);

            FlowLayoutPanel nhomVanHanh = ThemNhomMenu("VẬN HÀNH", false, false);
            ThemNutMenu(nhomVanHanh, "Email", "Quản lý email", false);
            ThemNutMenu(nhomVanHanh, "Thống kê", "Thống kê", false);

            FlowLayoutPanel nhomQuanTri = ThemNhomMenu("QUẢN TRỊ", true, false);
            ThemNutMenu(nhomQuanTri, "Nhân viên", "Quản lý nhân viên", true);
            ThemNutMenu(nhomQuanTri, "Tài khoản", "Quản lý tài khoản và phân quyền", true);
            ThemNutMenu(nhomQuanTri, "Danh mục sản phẩm", "Quản lý danh mục sản phẩm", true);
            ThemNutMenu(nhomQuanTri, "Chất liệu", "Quản lý chất liệu và giá tham khảo", true);
            ThemNutMenu(nhomQuanTri, "Nhà cung cấp", "Quản lý nhà cung cấp", true);
            ThemNutMenu(nhomQuanTri, "Sao lưu / Phục hồi", "Sao lưu và phục hồi CSDL", true);
        }

        private FlowLayoutPanel ThemNhomMenu(string noiDung, bool chiDanhChoQuanTri, bool moSan)
        {
            var noiDungNhom = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.TopDown,
                Margin = Padding.Empty,
                Name = "pnlNhomMenu",
                Size = new Size(215, 0),
                Visible = moSan,
                WrapContents = false
            };

            var trangThai = new ThongTinNhomMenu(noiDungNhom, noiDung, moSan);
            var tieuDe = new Guna2Button
            {
                Animated = true,
                BorderRadius = 6,
                ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.DefaultButton,
                Cursor = Cursors.Hand,
                FillColor = Color.Transparent,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(172, 182, 194),
                HoverState = { FillColor = Color.FromArgb(36, 50, 68), ForeColor = Color.White },
                Margin = new Padding(10, 6, 10, 2),
                Name = "btnNhomMenu",
                Size = new Size(215, 34),
                Tag = trangThai,
                Text = (moSan ? "▾  " : "▸  ") + noiDung,
                TextAlign = HorizontalAlignment.Left,
                TextOffset = new Point(6, 0)
            };
            tieuDe.Click += btnNhomMenu_Click;

            if (chiDanhChoQuanTri)
            {
                mucQuanTri.Add(tieuDe);
                mucQuanTri.Add(noiDungNhom);
            }

            flowMenu.Controls.Add(tieuDe);
            flowMenu.Controls.Add(noiDungNhom);
            return noiDungNhom;
        }

        private void ThemNutMenu(
            FlowLayoutPanel nhom,
            string noiDung,
            string tieuDeTrang,
            bool chiDanhChoQuanTri)
        {
            var button = new Guna2Button
            {
                Animated = true,
                BorderRadius = 8,
                ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.DefaultButton,
                Cursor = Cursors.Hand,
                FillColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(235, 240, 245),
                HoverState = { FillColor = Color.FromArgb(45, 60, 80), ForeColor = Color.FromArgb(214, 182, 116) },
                Margin = new Padding(10, 1, 10, 2),
                Name = "btnNav" + nhom.Controls.Count,
                Size = new Size(205, 38),
                Tag = new ThongTinMenu(tieuDeTrang, chiDanhChoQuanTri),
                Text = "•  " + noiDung,
                TextAlign = HorizontalAlignment.Left,
                TextOffset = new Point(14, 0)
            };
            button.Click += btnMenu_Click;

            if (chiDanhChoQuanTri)
            {
                mucQuanTri.Add(button);
            }

            nhom.Controls.Add(button);
        }

        private void btnNhomMenu_Click(object sender, EventArgs e)
        {
            var button = sender as Guna2Button;
            var nhom = button?.Tag as ThongTinNhomMenu;
            if (nhom == null)
            {
                return;
            }

            bool seMo = !nhom.DangMo;
            foreach (Guna2Button tieuDeKhac in flowMenu.Controls.OfType<Guna2Button>())
            {
                var nhomKhac = tieuDeKhac.Tag as ThongTinNhomMenu;
                if (nhomKhac == null || nhomKhac == nhom)
                {
                    continue;
                }

                nhomKhac.DangMo = false;
                nhomKhac.NoiDung.Visible = false;
                tieuDeKhac.Text = "▸  " + nhomKhac.TieuDe;
            }

            nhom.DangMo = seMo;
            nhom.NoiDung.Visible = nhom.DangMo;
            button.Text = (nhom.DangMo ? "▾  " : "▸  ") + nhom.TieuDe;
        }

        private void ApDungPhanQuyen()
        {
            bool laQuanTriVien = phienDangNhap.LaQuanTriVien;
            foreach (Control control in mucQuanTri)
            {
                if (!laQuanTriVien)
                {
                    control.Visible = false;
                }
                else if (!(control is FlowLayoutPanel))
                {
                    control.Visible = true;
                }
            }
        }

        private void DatTrangThaiNutMenu(Guna2Button nutChon)
        {
            foreach (Control ctrl in flowMenu.Controls)
            {
                if (ctrl is FlowLayoutPanel panelNhom)
                {
                    foreach (Guna2Button btn in panelNhom.Controls.OfType<Guna2Button>())
                    {
                        if (btn == nutChon)
                        {
                            btn.FillColor = Color.FromArgb(214, 182, 116);
                            btn.ForeColor = Color.FromArgb(27, 39, 53);
                            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                        }
                        else
                        {
                            btn.FillColor = Color.Transparent;
                            btn.ForeColor = Color.FromArgb(235, 240, 245);
                            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                        }
                    }
                }
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            var button = sender as Guna2Button;
            var menu = button?.Tag as ThongTinMenu;
            if (menu == null)
            {
                return;
            }

            DatTrangThaiNutMenu(button);

            if (menu.ChiDanhChoQuanTri && !phienDangNhap.LaQuanTriVien)
            {
                MessageBox.Show(
                    "Bạn không có quyền sử dụng chức năng này.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (menu.TieuDeTrang == "Tổng quan hệ thống")
            {
                HienThiTongQuan();
                return;
            }

            if (menu.TieuDeTrang == "Quản lý nhân viên")
            {
                MoFormCon(new FrmNhanVien(), menu.TieuDeTrang, true);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý khách hàng")
            {
                MoFormCon(new FrmKhachHang(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Bán hàng")
            {
                MoFormCon(new FrmBanHang(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý hóa đơn")
            {
                MoFormCon(new FrmHoaDon(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý sản phẩm")
            {
                MoFormCon(new FrmSanPham(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Nhập hàng")
            {
                MoFormCon(new FrmNhapHang(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Import và tra cứu thu mua")
            {
                MoFormCon(new FrmThuMua(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý bảo hành")
            {
                MoFormCon(new FrmBaoHanh(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý email")
            {
                MoFormCon(new FrmQuanLyEmail(), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Thống kê")
            {
                MoFormCon(new FrmThongKe(true), menu.TieuDeTrang, false);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý tài khoản và phân quyền")
            {
                MoFormCon(new FrmTaiKhoan(), menu.TieuDeTrang, true);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý danh mục sản phẩm")
            {
                MoFormCon(new FrmDanhMuc(), menu.TieuDeTrang, true);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý chất liệu và giá tham khảo")
            {
                MoFormCon(new FrmChatLieu(), menu.TieuDeTrang, true);
                return;
            }

            if (menu.TieuDeTrang == "Quản lý nhà cung cấp")
            {
                MoFormCon(new FrmNhaCungCap(), menu.TieuDeTrang, true);
                return;
            }

            if (menu.TieuDeTrang == "Sao lưu và phục hồi CSDL")
            {
                MoFormCon(new FrmSaoLuuPhucHoi(), menu.TieuDeTrang, true);
                return;
            }

            DongFormConHienTai();
            pnlChaoMung.Visible = true;
            lblTieuDeTrang.Text = menu.TieuDeTrang;
            lblNoiDungChinh.Text = menu.TieuDeTrang;
            lblMoTa.Text = "Màn hình chức năng sẽ được bổ sung trong commit nghiệp vụ tiếp theo.";
        }

        private void MoFormCon(Form formCon, string tieuDeTrang, bool chiDanhChoQuanTri)
        {
            if (chiDanhChoQuanTri && !phienDangNhap.LaQuanTriVien)
            {
                MessageBox.Show(
                    "Bạn không có quyền sử dụng chức năng này.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                formCon.Dispose();
                return;
            }

            DongFormConHienTai();
            pnlChaoMung.Visible = false;
            lblTieuDeTrang.Text = tieuDeTrang;

            formConHienTai = formCon;
            formCon.TopLevel = false;
            formCon.FormBorderStyle = FormBorderStyle.None;
            formCon.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(formCon);
            formCon.BringToFront();
            formCon.Show();
        }

        private void DongFormConHienTai()
        {
            if (formConHienTai == null)
            {
                return;
            }

            formConHienTai.Close();
            formConHienTai.Dispose();
            formConHienTai = null;
        }

        private void HienThiTongQuan()
        {
            MoFormCon(new FrmThongKe(false), "Tổng quan hệ thống", false);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Bạn có chắc muốn đăng xuất?",
                    "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            dangXuat = true;
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dangXuat || e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            if (MessageBox.Show(
                    "Bạn có chắc muốn thoát ứng dụng?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private sealed class ThongTinMenu
        {
            public ThongTinMenu(string tieuDeTrang, bool chiDanhChoQuanTri)
            {
                TieuDeTrang = tieuDeTrang;
                ChiDanhChoQuanTri = chiDanhChoQuanTri;
            }

            public string TieuDeTrang { get; }
            public bool ChiDanhChoQuanTri { get; }
        }

        private sealed class ThongTinNhomMenu
        {
            internal ThongTinNhomMenu(FlowLayoutPanel noiDung, string tieuDe, bool dangMo)
            {
                NoiDung = noiDung;
                TieuDe = tieuDe;
                DangMo = dangMo;
            }

            internal FlowLayoutPanel NoiDung { get; private set; }
            internal string TieuDe { get; private set; }
            internal bool DangMo { get; set; }
        }

        private void flowMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

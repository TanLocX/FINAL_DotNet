using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmMain : Form
    {
        private readonly ThongTinPhienDangNhap phienDangNhap;
        private readonly List<Control> mucQuanTri = new List<Control>();
        private Form formConHienTai;
        private bool dangXuat;

        public FrmMain()
        {
            InitializeComponent();
            phienDangNhap = CurrentUserSession.HienTai;
        }

        public bool DaYeuCauDangXuat => dangXuat;

        private void FrmMain_Load(object sender, EventArgs e)
        {
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

            ThemTieuDeNhom("NGHIỆP VỤ", false);
            ThemNutMenu("Tổng quan", "Tổng quan hệ thống", false);
            ThemNutMenu("Bán hàng", "Bán hàng", false);
            ThemNutMenu("Hóa đơn", "Quản lý hóa đơn", false);
            ThemNutMenu("Khách hàng", "Quản lý khách hàng", false);
            ThemNutMenu("Sản phẩm", "Quản lý sản phẩm", false);
            ThemNutMenu("Nhập hàng", "Nhập hàng", false);
            ThemNutMenu("Thu mua", "Thu mua từ khách hàng", false);
            ThemNutMenu("Bảo hành", "Quản lý bảo hành", false);
            ThemNutMenu("Email", "Quản lý email", false);
            ThemNutMenu("Thống kê", "Thống kê", false);

            ThemTieuDeNhom("QUẢN TRỊ", true);
            ThemNutMenu("Nhân viên", "Quản lý nhân viên", true);
            ThemNutMenu("Tài khoản", "Quản lý tài khoản và phân quyền", true);
            ThemNutMenu("Danh mục sản phẩm", "Quản lý danh mục sản phẩm", true);
            ThemNutMenu("Chất liệu", "Quản lý chất liệu và giá tham khảo", true);
            ThemNutMenu("Nhà cung cấp", "Quản lý nhà cung cấp", true);
            ThemNutMenu("Sao lưu / Phục hồi", "Sao lưu và phục hồi CSDL", true);
        }

        private void ThemTieuDeNhom(string noiDung, bool chiDanhChoQuanTri)
        {
            var label = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(172, 182, 194),
                Margin = new Padding(18, 14, 6, 4),
                Size = new Size(190, 22),
                Text = noiDung,
                TextAlign = ContentAlignment.MiddleLeft
            };

            if (chiDanhChoQuanTri)
            {
                mucQuanTri.Add(label);
            }

            flowMenu.Controls.Add(label);
        }

        private void ThemNutMenu(string noiDung, string tieuDeTrang, bool chiDanhChoQuanTri)
        {
            var button = new Button
            {
                BackColor = Color.FromArgb(27, 39, 53),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.White,
                Margin = new Padding(10, 2, 10, 2),
                Padding = new Padding(14, 0, 0, 0),
                Size = new Size(195, 40),
                Tag = new ThongTinMenu(tieuDeTrang, chiDanhChoQuanTri),
                Text = noiDung,
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 75, 94);
            button.Click += btnMenu_Click;

            if (chiDanhChoQuanTri)
            {
                mucQuanTri.Add(button);
            }

            flowMenu.Controls.Add(button);
        }

        private void ApDungPhanQuyen()
        {
            bool laQuanTriVien = phienDangNhap.LaQuanTriVien;
            foreach (Control control in mucQuanTri)
            {
                control.Visible = laQuanTriVien;
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var menu = button?.Tag as ThongTinMenu;
            if (menu == null)
            {
                return;
            }

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

            if (menu.TieuDeTrang == "Quản lý sản phẩm")
            {
                MoFormCon(new FrmSanPham(), menu.TieuDeTrang, false);
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
            DongFormConHienTai();
            pnlChaoMung.Visible = true;
            lblTieuDeTrang.Text = "Tổng quan hệ thống";
            lblNoiDungChinh.Text = "HỆ THỐNG QUẢN LÝ CỬA HÀNG ĐÁ QUÝ PNJ";
            lblMoTa.Text = phienDangNhap.LaQuanTriVien
                ? "Bạn có quyền quản trị và sử dụng toàn bộ chức năng của hệ thống."
                : "Bạn đang sử dụng nhóm chức năng nghiệp vụ dành cho nhân viên.";
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
    }
}

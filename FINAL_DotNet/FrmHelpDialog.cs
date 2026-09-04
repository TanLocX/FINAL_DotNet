using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace FINAL_DotNet
{
    public sealed class FrmHelpDialog : Form
    {
        public FrmHelpDialog()
        {
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            Text = "Trung tâm trợ giúp & Phím tắt thu ngân - PNJ Manager";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(700, 580);
            BackColor = Color.FromArgb(248, 250, 252);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F1)
                {
                    Close();
                }
            };

            // Top Header
            var headerPanel = new Guna2Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                FillColor = Color.FromArgb(27, 39, 53)
            };

            var lblBrand = new Label
            {
                Text = "PNJ MANAGER — TRỢ GIÚP & VẬN HÀNH POS",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(214, 182, 116),
                Location = new Point(22, 14),
                AutoSize = true,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            var lblSubtitle = new Label
            {
                Text = "Bảng tra cứu phím tắt thu ngân và thông số hệ thống đang hoạt động",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(203, 213, 225),
                Location = new Point(23, 40),
                AutoSize = true,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            headerPanel.Controls.Add(lblBrand);
            headerPanel.Controls.Add(lblSubtitle);

            // Scrollable Content
            var contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20, 14, 20, 10),
                BackColor = Color.Transparent
            };

            // Card 1: Hotkeys
            var shortcutsCard = CreateSectionCard("1. PHÍM TẮT THU NGÂN (POS SHORTCUTS)", 205);
            AddShortcutRow(shortcutsCard, "F9", "Thanh toán đơn hàng", "Kiểm tra tồn kho, trừ kho an toàn, lưu hóa đơn và in phiếu thu", 38);
            AddShortcutRow(shortcutsCard, "F4", "Tạo đơn mới / Hủy giỏ", "Làm mới toàn bộ giỏ hàng để phục vụ lượt khách hàng tiếp theo", 78);
            AddShortcutRow(shortcutsCard, "F1", "Mở trợ giúp", "Mở nhanh bảng tra cứu phím tắt và thông tin hệ thống này bất kỳ lúc nào", 118);
            AddShortcutRow(shortcutsCard, "ESC", "Đóng cửa sổ", "Đóng nhanh các hộp thoại trợ giúp, đổi mật khẩu hoặc hủy tác vụ tạm", 158);
            contentPanel.Controls.Add(shortcutsCard);

            // Card 2: System Specs
            var systemInfoCard = CreateSectionCard("2. THÔNG TIN HỆ THỐNG & KẾT NỐI", 145);
            string serverName = "(localdb)\\MSSQLLocalDB";
            string databaseName = DatabaseConnection.GetDatabaseName();
            string userName = CurrentUserSession.DaDangNhap ? CurrentUserSession.HienTai.HoTen : "Chưa đăng nhập";
            string roleName = CurrentUserSession.DaDangNhap && CurrentUserSession.HienTai.LaQuanTriVien ? "Quản trị viên (Admin)" : "Nhân viên thu ngân";

            AddInfoRow(systemInfoCard, "Cơ sở dữ liệu:", $"{databaseName} (SQL Server LocalDB)", 38);
            AddInfoRow(systemInfoCard, "Máy chủ hiện hành:", serverName, 65);
            AddInfoRow(systemInfoCard, "Phiên đăng nhập:", $"{userName}  —  Vai trò: {roleName}", 92);
            AddInfoRow(systemInfoCard, "Phiên bản phần mềm:", "PNJ Retail POS & Management System v2.5", 119);
            contentPanel.Controls.Add(systemInfoCard);

            // Card 3: Operation Tips
            var tipsCard = CreateSectionCard("3. LƯU Ý VẬN HÀNH QUAN TRỌNG", 88);
            var lblTips = new Label
            {
                Text = "• Thu ngân: Luôn nhập số tiền khách đưa để hệ thống tự động tính tiền thối lại cho khách.\n• Quản trị viên: Định kỳ vào tab 'Sao lưu / Phục hồi' để xuất bản sao dữ liệu an toàn trước khi bảo trì.",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(18, 34),
                Size = new Size(615, 44),
                BackColor = Color.Transparent,
                UseMnemonic = false
            };
            tipsCard.Controls.Add(lblTips);
            contentPanel.Controls.Add(tipsCard);

            // Bottom Bar
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 54,
                BackColor = Color.White,
                Padding = new Padding(0, 8, 20, 8)
            };

            var btnClose = new Guna2Button
            {
                Text = "Đóng (ESC)",
                Size = new Size(115, 36),
                Location = new Point(560, 9),
                BorderRadius = 6,
                FillColor = Color.FromArgb(27, 39, 53),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.Click += (s, e) => Close();
            bottomPanel.Controls.Add(btnClose);

            Controls.Add(contentPanel);
            Controls.Add(bottomPanel);
            Controls.Add(headerPanel);
        }

        private static Guna2Panel CreateSectionCard(string title, int height)
        {
            var card = new Guna2Panel
            {
                Width = 655,
                Height = height,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderThickness = 1,
                BorderRadius = 8,
                FillColor = Color.White,
                Margin = new Padding(0, 0, 0, 12)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 140, 60),
                Location = new Point(16, 11),
                AutoSize = true,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };
            card.Controls.Add(lblTitle);
            return card;
        }

        private static void AddShortcutRow(Guna2Panel card, string key, string name, string description, int yPos)
        {
            var badge = new Guna2Button
            {
                Text = key,
                Size = new Size(54, 26),
                Location = new Point(18, yPos),
                BorderRadius = 5,
                BorderColor = Color.FromArgb(214, 182, 116),
                BorderThickness = 1,
                FillColor = Color.FromArgb(247, 240, 225),
                ForeColor = Color.FromArgb(137, 100, 28),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Default
            };

            var lblName = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(27, 39, 53),
                Location = new Point(82, yPos + 4),
                AutoSize = true,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            var lblDesc = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(236, yPos + 4),
                Size = new Size(405, 20),
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            card.Controls.Add(badge);
            card.Controls.Add(lblName);
            card.Controls.Add(lblDesc);
        }

        private static void AddInfoRow(Guna2Panel card, string label, string value, int yPos)
        {
            var lblCaption = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(18, yPos),
                Size = new Size(135, 20),
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(27, 39, 53),
                Location = new Point(155, yPos),
                Size = new Size(485, 20),
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            card.Controls.Add(lblCaption);
            card.Controls.Add(lblValue);
        }
    }
}

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
            Text = "Trợ giúp & Phím tắt hệ thống - PNJ Manager";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(680, 560);
            BackColor = Color.FromArgb(243, 245, 248);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F1)
                {
                    Close();
                }
            };

            var headerPanel = new Guna2Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                FillColor = Color.FromArgb(27, 39, 53)
            };

            var lblBrand = new Label
            {
                Text = "PNJ MANAGER — TRỢ GIÚP & VẬN HÀNH POS",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(214, 182, 116),
                Location = new Point(20, 12),
                AutoSize = true
            };

            var lblSubtitle = new Label
            {
                Text = "Bảng tra cứu phím tắt thu ngân và thông số hệ thống đang hoạt động",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(172, 182, 194),
                Location = new Point(22, 38),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBrand);
            headerPanel.Controls.Add(lblSubtitle);

            var contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(15, 10, 15, 10)
            };

            // Card 1: Hotkeys
            var shortcutsCard = CreateSectionCard("1. PHÍM TẮT THU NGÂN (POS SHORTCUTS)", 195);
            AddShortcutRow(shortcutsCard, "F9", "Thanh toán đơn hàng", "Kiểm tra tồn kho, trừ kho an toàn, tạo hóa đơn và in phiếu", 35);
            AddShortcutRow(shortcutsCard, "F4", "Tạo đơn mới / Hủy giỏ", "Làm mới toàn bộ giỏ hàng để phục vụ lượt khách hàng tiếp theo", 75);
            AddShortcutRow(shortcutsCard, "F1", "Mở trợ giúp", "Hiển thị nhanh bảng hướng dẫn và phím tắt này bất kỳ lúc nào", 115);
            AddShortcutRow(shortcutsCard, "ESC", "Đóng cửa sổ phụ", "Đóng các hộp thoại trợ giúp, đổi mật khẩu, xem báo cáo", 155);
            contentPanel.Controls.Add(shortcutsCard);

            // Card 2: System Specs
            var systemInfoCard = CreateSectionCard("2. THÔNG TIN HỆ THỐNG & KẾT NỐI", 145);
            string serverName = "(localdb)\\MSSQLLocalDB";
            string databaseName = DatabaseConnection.GetDatabaseName();
            string userName = CurrentUserSession.DaDangNhap ? CurrentUserSession.HienTai.HoTen : "Chưa đăng nhập";
            string roleName = CurrentUserSession.DaDangNhap && CurrentUserSession.HienTai.LaQuanTriVien ? "Quản trị viên (Admin)" : "Nhân viên thu ngân";

            AddInfoRow(systemInfoCard, "Cơ sở dữ liệu:", $"{databaseName} (SQL Server LocalDB)", 35);
            AddInfoRow(systemInfoCard, "Máy chủ hiện hành:", serverName, 65);
            AddInfoRow(systemInfoCard, "Phiên đăng nhập:", $"{userName} — Vai trò: {roleName}", 95);
            AddInfoRow(systemInfoCard, "Phiên bản phần mềm:", "PNJ Retail POS & Management Edition 2026.1", 125);
            contentPanel.Controls.Add(systemInfoCard);

            // Card 3: Operation Tips
            var tipsCard = CreateSectionCard("3. LƯU Ý VẬN HÀNH QUAN TRỌNG", 85);
            var lblTips = new Label
            {
                Text = "• Thu ngân: Luôn nhập số tiền khách đưa để hệ thống tự động tính tiền thừa trả khách.\n• Quản trị viên: Định kỳ vào tab 'Sao lưu / Phục hồi' để xuất bản sao dữ liệu an toàn.",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(16, 32),
                Size = new Size(600, 42)
            };
            tipsCard.Controls.Add(lblTips);
            contentPanel.Controls.Add(tipsCard);

            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.White
            };

            var btnClose = new Guna2Button
            {
                Text = "Đóng (ESC)",
                Size = new Size(110, 34),
                Location = new Point(540, 8),
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
                Width = 635,
                Height = height,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderThickness = 1,
                BorderRadius = 8,
                FillColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 140, 60),
                Location = new Point(14, 10),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);
            return card;
        }

        private static void AddShortcutRow(Guna2Panel card, string key, string name, string description, int yPos)
        {
            var badge = new Guna2Button
            {
                Text = key,
                Size = new Size(52, 24),
                Location = new Point(16, yPos),
                BorderRadius = 4,
                FillColor = Color.FromArgb(247, 240, 225),
                ForeColor = Color.FromArgb(137, 100, 28),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Enabled = false
            };

            var lblName = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(27, 39, 53),
                Location = new Point(78, yPos + 3),
                AutoSize = true
            };

            var lblDesc = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(230, yPos + 3),
                Size = new Size(390, 20),
                AutoEllipsis = true
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
                Location = new Point(16, yPos),
                Size = new Size(130, 20)
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(27, 39, 53),
                Location = new Point(150, yPos),
                Size = new Size(470, 20),
                AutoEllipsis = true
            };

            card.Controls.Add(lblCaption);
            card.Controls.Add(lblValue);
        }
    }
}

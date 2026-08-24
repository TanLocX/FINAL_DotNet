namespace FINAL_DotNet
{
    partial class FrmSaoLuuPhucHoi
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblMayChu;
        private System.Windows.Forms.Label lblCoSoDuLieu;
        private System.Windows.Forms.Label lblPhienBan;
        private System.Windows.Forms.Label lblQuyen;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.SplitContainer splitChinh;
        private System.Windows.Forms.TextBox txtThuMucSaoLuu;
        private System.Windows.Forms.TextBox txtTenFileSaoLuu;
        private System.Windows.Forms.Button btnTaoTenMoi;
        private System.Windows.Forms.Button btnSaoLuu;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.Label lblSoBanSao;
        private System.Windows.Forms.TextBox txtDuongDanPhucHoi;
        private System.Windows.Forms.Button btnPhucHoi;
        private System.Windows.Forms.Panel pnlTienTrinh;
        private System.Windows.Forms.Label lblTienTrinh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.lblMayChu = new System.Windows.Forms.Label();
            this.lblCoSoDuLieu = new System.Windows.Forms.Label();
            this.lblPhienBan = new System.Windows.Forms.Label();
            this.lblQuyen = new System.Windows.Forms.Label();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.splitChinh = new System.Windows.Forms.SplitContainer();
            this.txtThuMucSaoLuu = new System.Windows.Forms.TextBox();
            this.txtTenFileSaoLuu = new System.Windows.Forms.TextBox();
            this.btnTaoTenMoi = new System.Windows.Forms.Button();
            this.btnSaoLuu = new System.Windows.Forms.Button();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.lblSoBanSao = new System.Windows.Forms.Label();
            this.txtDuongDanPhucHoi = new System.Windows.Forms.TextBox();
            this.btnPhucHoi = new System.Windows.Forms.Button();
            this.pnlTienTrinh = new System.Windows.Forms.Panel();
            this.lblTienTrinh = new System.Windows.Forms.Label();

            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 248);
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmSaoLuuPhucHoi";
            this.Text = "Sao lưu và phục hồi CSDL";
            this.Load += new System.EventHandler(this.FrmSaoLuuPhucHoi_Load);

            this.splitChinh.Size = new System.Drawing.Size(1000, 492);
            this.splitChinh.SplitterDistance = 420;
            TaoThongTinMayChu();
            TaoKhuVucSaoLuu();
            TaoKhuVucPhucHoi();
            TaoTienTrinh();

            this.splitChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChinh.BackColor = System.Drawing.Color.FromArgb(225, 229, 234);
            this.splitChinh.SplitterWidth = 6;
            this.splitChinh.SplitterDistance = 420;
            this.splitChinh.Panel1.Padding = new System.Windows.Forms.Padding(14);
            this.splitChinh.Panel2.Padding = new System.Windows.Forms.Padding(14);

            this.Controls.Add(this.splitChinh);
            this.Controls.Add(this.pnlTienTrinh);
            this.Controls.Add(this.pnlThongTin);
        }

        private void TaoThongTinMayChu()
        {
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Height = 104;
            this.pnlThongTin.BackColor = System.Drawing.Color.White;
            this.pnlThongTin.Padding = new System.Windows.Forms.Padding(18, 12, 18, 10);
            var tieuDe = new System.Windows.Forms.Label { AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold), ForeColor = MauChu(), Location = new System.Drawing.Point(18, 11), Text = "QUẢN TRỊ BACKUP / RESTORE SQL SERVER" };
            var nhanMayChu = TaoNhan("Máy chủ:", 20, 51);
            var nhanCsdl = TaoNhan("CSDL:", 365, 51);
            CauHinhGiaTri(this.lblMayChu, 88, 49, 260, "--");
            CauHinhGiaTri(this.lblCoSoDuLieu, 417, 49, 250, "--");
            CauHinhGiaTri(this.lblPhienBan, 20, 74, 345, "--");
            CauHinhGiaTri(this.lblQuyen, 365, 74, 420, "Chưa kiểm tra quyền");
            CauHinhNut(this.btnTaiLai, "Làm mới", 850, 31, 120, MauXanh());
            this.btnTaiLai.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            this.pnlThongTin.Controls.AddRange(new System.Windows.Forms.Control[] { tieuDe, nhanMayChu, nhanCsdl, this.lblMayChu, this.lblCoSoDuLieu, this.lblPhienBan, this.lblQuyen, this.btnTaiLai });
        }

        private void TaoKhuVucSaoLuu()
        {
            var card = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill, BackColor = System.Drawing.Color.White, Padding = new System.Windows.Forms.Padding(18) };
            var tieuDe = new System.Windows.Forms.Label { AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold), ForeColor = MauChu(), Location = new System.Drawing.Point(18, 18), Text = "Tạo bản sao lưu" };
            var moTa = new System.Windows.Forms.Label { Location = new System.Drawing.Point(20, 55), Size = new System.Drawing.Size(350, 64), ForeColor = MauChuPhu(), Text = "Đường dẫn bên dưới thuộc máy chủ SQL, không phải máy đang chạy ứng dụng. SQL Server phải có quyền ghi vào thư mục này." };
            var lblThuMuc = TaoNhan("Thư mục trên máy chủ SQL", 20, 116);
            this.txtThuMucSaoLuu.Location = new System.Drawing.Point(20, 139);
            this.txtThuMucSaoLuu.Size = new System.Drawing.Size(350, 25);
            var lblTen = TaoNhan("Tên file .bak", 20, 177);
            this.txtTenFileSaoLuu.Location = new System.Drawing.Point(20, 200);
            this.txtTenFileSaoLuu.Size = new System.Drawing.Size(250, 25);
            CauHinhNut(this.btnTaoTenMoi, "Tên mới", 278, 197, 92, System.Drawing.Color.DimGray);
            this.btnTaoTenMoi.Click += new System.EventHandler(this.btnTaoTenMoi_Click);
            var luuY = new System.Windows.Forms.Label { Location = new System.Drawing.Point(20, 246), Size = new System.Drawing.Size(350, 68), ForeColor = System.Drawing.Color.FromArgb(143, 91, 30), Text = "Bản sao dùng COPY_ONLY và CHECKSUM, sau đó được kiểm tra bằng RESTORE VERIFYONLY. Thao tác không làm gián đoạn chuỗi backup định kỳ của SQL Server." };
            CauHinhNut(this.btnSaoLuu, "Sao lưu và xác minh", 20, 330, 350, MauXanh());
            this.btnSaoLuu.Enabled = false;
            this.btnSaoLuu.Click += new System.EventHandler(this.btnSaoLuu_Click);
            card.Controls.AddRange(new System.Windows.Forms.Control[] { tieuDe, moTa, lblThuMuc, this.txtThuMucSaoLuu, lblTen, this.txtTenFileSaoLuu, this.btnTaoTenMoi, luuY, this.btnSaoLuu });
            this.splitChinh.Panel1.Controls.Add(card);
        }

        private void TaoKhuVucPhucHoi()
        {
            var card = new System.Windows.Forms.TableLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, BackColor = System.Drawing.Color.White, Padding = new System.Windows.Forms.Padding(18), ColumnCount = 1, RowCount = 4 };
            card.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            card.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            card.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            card.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            card.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            var tieuDe = new System.Windows.Forms.Label { Dock = System.Windows.Forms.DockStyle.Fill, Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold), ForeColor = MauChu(), Text = "Lịch sử và phục hồi", TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            this.lblSoBanSao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoBanSao.ForeColor = MauChuPhu();
            this.lblSoBanSao.Text = "Chưa tải lịch sử";
            this.dgvLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSu.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            CauHinhLuoi(this.dgvLichSu);
            this.dgvLichSu.Columns.Add(TaoCot("Thời gian", "ThoiGianHienThi", 145));
            this.dgvLichSu.Columns.Add(TaoCot("Dung lượng", "KichThuocHienThi", 90));
            this.dgvLichSu.Columns.Add(TaoCot("Loại", "LoaiBanSao", 80));
            var cotDuongDan = TaoCot("Đường dẫn trên máy chủ", "DuongDan", 260);
            cotDuongDan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvLichSu.Columns.Add(cotDuongDan);
            this.dgvLichSu.SelectionChanged += new System.EventHandler(this.dgvLichSu_SelectionChanged);

            var pnlPhucHoi = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill };
            var lblDuongDan = TaoNhan("File .bak phục hồi (chọn từ lịch sử hoặc nhập đường dẫn máy chủ)", 0, 3);
            this.txtDuongDanPhucHoi.Location = new System.Drawing.Point(0, 27);
            this.txtDuongDanPhucHoi.Size = new System.Drawing.Size(500, 25);
            this.txtDuongDanPhucHoi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            var canhBao = new System.Windows.Forms.Label { Location = new System.Drawing.Point(0, 60), Size = new System.Drawing.Size(500, 42), Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right, ForeColor = System.Drawing.Color.Firebrick, Text = "Restore sẽ ngắt kết nối của cả nhóm.\r\nHệ thống luôn tạo một bản sao an toàn trước khi thay thế dữ liệu." };
            CauHinhNut(this.btnPhucHoi, "Phục hồi và khởi động lại", 0, 112, 500, System.Drawing.Color.Firebrick);
            this.btnPhucHoi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnPhucHoi.Enabled = false;
            this.btnPhucHoi.Click += new System.EventHandler(this.btnPhucHoi_Click);
            pnlPhucHoi.Controls.AddRange(new System.Windows.Forms.Control[] { lblDuongDan, this.txtDuongDanPhucHoi, canhBao, this.btnPhucHoi });
            card.Controls.Add(tieuDe, 0, 0);
            card.Controls.Add(this.lblSoBanSao, 0, 1);
            card.Controls.Add(this.dgvLichSu, 0, 2);
            card.Controls.Add(pnlPhucHoi, 0, 3);
            this.splitChinh.Panel2.Controls.Add(card);
        }

        private void TaoTienTrinh()
        {
            this.pnlTienTrinh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTienTrinh.Height = 54;
            this.pnlTienTrinh.BackColor = System.Drawing.Color.White;
            this.lblTienTrinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTienTrinh.Padding = new System.Windows.Forms.Padding(18, 0, 18, 0);
            this.lblTienTrinh.ForeColor = MauChuPhu();
            this.lblTienTrinh.Text = "Sẵn sàng.";
            this.lblTienTrinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTienTrinh.AutoEllipsis = true;
            this.pnlTienTrinh.Controls.Add(this.lblTienTrinh);
        }

        private static System.Windows.Forms.Label TaoNhan(string text, int x, int y) => new System.Windows.Forms.Label { AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold), ForeColor = MauChu(), Location = new System.Drawing.Point(x, y), Text = text };
        private static void CauHinhGiaTri(System.Windows.Forms.Label label, int x, int y, int width, string text) { label.Location = new System.Drawing.Point(x, y); label.Size = new System.Drawing.Size(width, 22); label.AutoEllipsis = true; label.ForeColor = MauChu(); label.Text = text; }
        private static void CauHinhNut(System.Windows.Forms.Button button, string text, int x, int y, int width, System.Drawing.Color color) { button.BackColor = color; button.FlatStyle = System.Windows.Forms.FlatStyle.Flat; button.FlatAppearance.BorderSize = 0; button.ForeColor = System.Drawing.Color.White; button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); button.Location = new System.Drawing.Point(x, y); button.Size = new System.Drawing.Size(width, 34); button.Text = text; button.UseVisualStyleBackColor = false; }
        private static void CauHinhLuoi(System.Windows.Forms.DataGridView grid) { grid.AllowUserToAddRows = false; grid.AllowUserToDeleteRows = false; grid.AllowUserToResizeRows = false; grid.AutoGenerateColumns = false; grid.BackgroundColor = System.Drawing.Color.White; grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; grid.ColumnHeadersHeight = 32; grid.EnableHeadersVisualStyles = false; grid.ColumnHeadersDefaultCellStyle.BackColor = MauChu(); grid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White; grid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold); grid.MultiSelect = false; grid.ReadOnly = true; grid.RowHeadersVisible = false; grid.RowTemplate.Height = 28; grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; }
        private static System.Windows.Forms.DataGridViewTextBoxColumn TaoCot(string tieuDe, string thuocTinh, int width) => new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = tieuDe, DataPropertyName = thuocTinh, Width = width, ReadOnly = true };
        private static System.Drawing.Color MauChu() => System.Drawing.Color.FromArgb(27, 39, 53);
        private static System.Drawing.Color MauChuPhu() => System.Drawing.Color.FromArgb(95, 106, 119);
        private static System.Drawing.Color MauXanh() => System.Drawing.Color.FromArgb(35, 125, 96);
    }
}

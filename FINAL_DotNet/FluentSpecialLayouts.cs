using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    /// <summary>
    /// Bố cục riêng cho các màn hình nghiệp vụ không thuộc mẫu CRUD thông thường.
    /// Chỉ thay đổi cách sắp xếp control; event, binding và nghiệp vụ được giữ nguyên.
    /// </summary>
    internal static class FluentSpecialLayouts
    {
        private const int Gap4 = 4;
        private const int Gap8 = 8;
        private const int Gap12 = 12;
        private const int Gap16 = 16;
        private const int InputHeight = 36;
        private const int ButtonHeight = 40;

        internal static void Apply(Form form)
        {
            AssignDesignerNames(form);
            switch (form.GetType().Name)
            {
                case "FrmBanHang":
                    ConfigureTransaction(form, "pnlDauHoaDon", "pnlDongBan", "pnlThongTinHoaDon", false);
                    break;
                case "FrmHoaDon":
                    ConfigureTransaction(form, "pnlDauHoaDon", "pnlDongBan", "pnlThongTinHoaDon", true);
                    break;
                case "FrmNhapHang":
                    ConfigureTransaction(form, "pnlDauPhieu", "pnlDongNhap", "pnlThongTinPhieu", false);
                    break;
                case "FrmBaoHanh":
                    ConfigureWarranty(form);
                    break;
                case "FrmSanPham":
                    ConfigureProducts(form);
                    break;
                case "FrmQuanLyEmail":
                    ConfigureEmail(form);
                    break;
                case "FrmThuMua":
                    ConfigurePurchase(form);
                    break;
                case "FrmSaoLuuPhucHoi":
                    ConfigureBackup(form);
                    break;
                case "FrmXemBaoCao":
                    form.Padding = new Padding(Gap8);
                    break;
            }
        }

        private static void AssignDesignerNames(Form form)
        {
            for (Type type = form.GetType(); type != null && type != typeof(object); type = type.BaseType)
            {
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    if (!typeof(Control).IsAssignableFrom(field.FieldType)) continue;
                    Control control = field.GetValue(form) as Control;
                    if (control != null && string.IsNullOrEmpty(control.Name)) control.Name = field.Name;
                }
            }
        }

        private static void ConfigureTransaction(Form form, string headerName, string lineName, string summaryName, bool invoiceHistoryOnly)
        {
            SplitContainer split = Find<SplitContainer>(form, "splitChinh");
            Panel filter = Find<Panel>(form, "pnlBoLoc");
            ConfigureWorkbench(split, 0.34F, 500);
            ReflowToolbar(filter, 132);
            ReflowToolbar(Find<Panel>(form, headerName), 72);
            ReflowToolbar(Find<Panel>(form, lineName), 128);
            ReflowSummary(Find<Panel>(form, summaryName), 104);
            ConfigureInvoiceMode(form, split, filter, invoiceHistoryOnly);
            Button[] footerButtons = form.GetType().Name == "FrmHoaDon"
                ? new[] { Find<Button>(form, "btnHuyHoaDon"), Find<Button>(form, "btnInHoaDon") }
                : form.GetType().Name == "FrmBanHang"
                    ? new[] { Find<Button>(form, "btnLuuHoaDon"), Find<Button>(form, "btnHoaDonMoi") }
                    : new Button[0];
            ReflowFooter(Find<Panel>(form, "pnlChan"), footerButtons);
            SetTabPadding(form);
        }

        private static void ConfigureInvoiceMode(Form form, SplitContainer split, Panel filter, bool invoiceHistoryOnly)
        {
            TabControl tabs = Find<TabControl>(form, "tabBanHang");
            TabPage createTab = Find<TabPage>(form, "tabLapHoaDon");
            TabPage historyTab = Find<TabPage>(form, "tabLichSu");
            Button save = Find<Button>(form, "btnLuuHoaDon");
            Button cancel = Find<Button>(form, "btnHuyHoaDon");
            Button report = Find<Button>(form, "btnInHoaDon");
            Button fresh = Find<Button>(form, "btnHoaDonMoi");

            if (invoiceHistoryOnly)
            {
                if (tabs != null && createTab != null && tabs.TabPages.Contains(createTab)) tabs.TabPages.Remove(createTab);
                if (tabs != null && historyTab != null) tabs.SelectedTab = historyTab;
                if (historyTab != null) historyTab.Text = "Chi tiết hóa đơn";
                if (filter != null) filter.Visible = true;
                if (split != null) split.Panel1Collapsed = false;
                if (save != null) save.Visible = false;
                if (fresh != null) fresh.Visible = false;
                if (cancel != null) cancel.Visible = true;
                if (report != null) report.Visible = true;
                return;
            }

            if (tabs != null && historyTab != null && tabs.TabPages.Contains(historyTab)) tabs.TabPages.Remove(historyTab);
            if (tabs != null && createTab != null) tabs.SelectedTab = createTab;
            if (createTab != null) createTab.Text = form.GetType().Name == "FrmNhapHang" ? "Lập phiếu nhập" : "Bán hàng tại quầy";
            if (form.GetType().Name != "FrmBanHang") return;

            if (filter != null) { filter.Visible = false; filter.Height = 0; }
            if (split != null) split.Panel1Collapsed = true;
            if (cancel != null) cancel.Visible = false;
            if (report != null) report.Visible = false;
            if (save != null) save.Visible = true;
            if (fresh != null) fresh.Visible = true;
        }

        private static void ConfigureWarranty(Form form)
        {
            ConfigureWorkbench(Find<SplitContainer>(form, "splitChinh"), 0.36F, 500);
            ReflowToolbar(Find<Panel>(form, "pnlBoLoc"), 132);
            ReflowToolbar(Find<Panel>(form, "pnlSanPhamDaBan"), 128);
            ReflowEditor(Find<Panel>(form, "pnlYeuCau"));
            ReflowSummary(Find<Panel>(form, "pnlThongTinXuLy"), 96);
            ReflowEditor(Find<Panel>(form, "pnlXuLy"));
            ReflowFooter(Find<Panel>(form, "pnlChan"));
            SetTabPadding(form);
        }

        private static void ConfigureProducts(Form form)
        {
            ConfigureWorkbench(Find<SplitContainer>(form, "splitChinh"), 0.40F, 480);
            ReflowToolbar(Find<Panel>(form, "pnlBoLoc"), 132);
            ReflowProductTable(Find<TableLayoutPanel>(form, "tableThongTin"),
                Find<PictureBox>(form, "picSanPham"), Find<CheckBox>(form, "chkDangKinhDoanh"));
            ReflowToolbar(Find<Panel>(form, "pnlNhapThanhPhan"), 128);
            ReflowFooter(Find<Panel>(form, "pnlChan"));
            SetTabPadding(form);
        }

        private static void ConfigureWorkbench(SplitContainer split, float leftRatio, int rightMinimum)
        {
            if (split == null) return;
            ConfigureVerticalSplit(split, 280, rightMinimum, leftRatio);
            split.Panel1.Padding = new Padding(Gap16, Gap12, Gap8, Gap12);
            split.Panel2.Padding = new Padding(Gap8, Gap12, Gap16, Gap12);
        }

        private static void ConfigureVerticalSplit(SplitContainer split, int panel1Minimum, int panel2Minimum, float ratio)
        {
            split.Panel1MinSize = 0;
            split.Panel2MinSize = 0;
            split.Orientation = Orientation.Vertical;
            split.SplitterWidth = Gap8;

            int available = split.Width - split.SplitterWidth;
            if (available >= panel1Minimum + panel2Minimum)
            {
                int target = Math.Max(panel1Minimum, Math.Min(available - panel2Minimum, (int)(available * ratio)));
                split.SplitterDistance = target;
                split.Panel1MinSize = panel1Minimum;
                split.Panel2MinSize = panel2Minimum;
            }
            else if (available > Gap16 * 4)
            {
                split.SplitterDistance = Math.Max(Gap16 * 2, Math.Min(available - Gap16 * 2, (int)(available * ratio)));
            }

            split.SizeChanged += delegate { SetSplitter(split, ratio); };
        }

        private static void SetSplitter(SplitContainer split, float ratio)
        {
            int available = split.Width - split.SplitterWidth;
            if (available < split.Panel1MinSize + split.Panel2MinSize) return;
            int target = (int)(available * ratio);
            int maximum = available - split.Panel2MinSize;
            split.SplitterDistance = Math.Max(split.Panel1MinSize, Math.Min(maximum, target));
        }

        private static void ReflowToolbar(Panel panel, int height)
        {
            if (panel == null || panel.Controls.Cast<Control>().Any(item => item.Name == "flpSpecialToolbar")) return;
            Control[] original = panel.Controls.Cast<Control>().ToArray();
            List<Control> inputs = original.Where(IsInput).OrderBy(item => item.Top).ThenBy(item => item.Left).ToList();
            List<Button> buttons = original.OfType<Button>().OrderBy(item => item.Left).ToList();
            List<Label> labels = original.OfType<Label>().OrderBy(item => item.Left).ToList();
            HashSet<Label> used = new HashSet<Label>();

            panel.Controls.Clear();
            panel.Height = height;
            panel.Padding = new Padding(Gap8, Gap4, Gap8, Gap4);
            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Name = "flpSpecialToolbar",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            foreach (Control input in inputs)
            {
                Label label = MatchLabel(input, labels, used) ?? new Label { Text = FriendlyLabel(input.Name) };
                used.Add(label);
                flow.Controls.Add(CreateField(label, input, CompactWidth(input), 58));
            }
            foreach (Label value in labels.Where(item => !used.Contains(item)).OrderBy(item => item.Top).ThenBy(item => item.Left).ToArray())
            {
                Label caption = labels
                    .Where(item => item != value && !used.Contains(item) && value.Top - item.Top >= 14 && value.Top - item.Top <= 32 && Math.Abs(value.Left - item.Left) < 32)
                    .OrderBy(item => value.Top - item.Bottom)
                    .FirstOrDefault();
                if (caption == null) continue;
                used.Add(caption);
                used.Add(value);
                flow.Controls.Add(CreateField(caption, value, 150, 58));
            }
            foreach (Button button in buttons)
            {
                button.Size = new Size(88, ButtonHeight);
                button.Margin = new Padding(Gap4, 18, Gap4, 0);
                flow.Controls.Add(button);
            }
            foreach (Label label in labels.Where(item => !used.Contains(item)))
            {
                label.AutoSize = false;
                label.Size = new Size(112, ButtonHeight);
                label.Margin = new Padding(Gap4, 18, Gap4, 0);
                label.Padding = new Padding(Gap8, 0, Gap8, 0);
                label.TextAlign = ContentAlignment.MiddleLeft;
                flow.Controls.Add(label);
            }
            panel.Controls.Add(flow);
        }

        private static void ReflowEditor(Panel panel)
        {
            if (panel == null || panel.Controls.Cast<Control>().Any(item => item.Name == "tlpSpecialEditor")) return;
            List<Control> inputs = panel.Controls.Cast<Control>().Where(IsInput)
                .OrderBy(item => item.Top).ThenBy(item => item.Left).ToList();
            if (inputs.Count == 0) return;
            panel.Controls.Clear();
            panel.Padding = new Padding(Gap8);
            int rows = (int)Math.Ceiling(inputs.Count / 2D);
            TableLayoutPanel table = NewGrid("tlpSpecialEditor", 2, rows);
            for (int index = 0; index < inputs.Count; index++)
            {
                table.Controls.Add(CreateField(new Label { Text = FriendlyLabel(inputs[index].Name) }, inputs[index], 0, 0),
                    index % 2, index / 2);
            }
            panel.Controls.Add(table);
        }

        private static void ReflowSummary(Panel panel, int height)
        {
            if (panel == null || panel.Controls.Cast<Control>().Any(item => item.Name == "tlpSpecialSummary")) return;
            Control[] content = panel.Controls.Cast<Control>().OrderBy(item => item.Top).ThenBy(item => item.Left).ToArray();
            List<Label> labels = content.OfType<Label>().ToList();
            List<FieldPair> pairs = new List<FieldPair>();
            HashSet<Control> used = new HashSet<Control>();
            foreach (Label value in labels.OrderBy(item => item.Top).ThenBy(item => item.Left))
            {
                Label caption = labels
                    .Where(item => item != value && !used.Contains(item) && value.Top - item.Top >= 14 && value.Top - item.Top <= 32 && Math.Abs(value.Left - item.Left) < 32)
                    .OrderBy(item => value.Top - item.Bottom)
                    .FirstOrDefault();
                if (caption == null) continue;
                pairs.Add(new FieldPair(caption, value));
                used.Add(caption);
                used.Add(value);
            }
            Control[] remaining = content.Where(item => !used.Contains(item)).ToArray();
            int fieldRows = Math.Max(1, (int)Math.Ceiling(pairs.Count / 3D));
            int occupiedInLastRow = pairs.Count % 3;
            bool shareLastRow = remaining.Length > 0 && occupiedInLastRow > 0 && remaining.Length <= 3 - occupiedInLastRow;
            int rows = fieldRows + (remaining.Length > 0 && !shareLastRow ? 1 : 0);
            panel.Controls.Clear();
            panel.Height = Math.Max(height, fieldRows * 58 + (remaining.Length > 0 && !shareLastRow ? 36 : 0) + Gap16);
            panel.Padding = new Padding(Gap8);
            TableLayoutPanel table = NewGrid("tlpSpecialSummary", 3, rows);
            table.RowStyles.Clear();
            for (int row = 0; row < fieldRows; row++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / fieldRows));
            if (remaining.Length > 0 && !shareLastRow) table.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            for (int index = 0; index < pairs.Count; index++)
                table.Controls.Add(CreateField(pairs[index].Label, pairs[index].Input, 0, 0), index % 3, index / 3);
            for (int index = 0; index < remaining.Length; index++)
            {
                Control control = remaining[index];
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(Gap4);
                if (control is Label) ((Label)control).TextAlign = ContentAlignment.MiddleLeft;
                int column = shareLastRow ? occupiedInLastRow + index : Math.Min(index, 2);
                int row = shareLastRow ? fieldRows - 1 : fieldRows;
                table.Controls.Add(control, column, row);
                if (remaining.Length == 1 && !shareLastRow) table.SetColumnSpan(control, 3);
            }
            panel.Controls.Add(table);
        }

        private static void ReflowFooter(Panel panel, params Button[] requestedButtons)
        {
            if (panel == null || panel.Controls.Cast<Control>().Any(item => item.Name == "flpSpecialFooter")) return;
            Button[] buttons = requestedButtons != null && requestedButtons.Any(item => item != null)
                ? requestedButtons.Where(item => item != null).OrderByDescending(item => item.Left).ToArray()
                : panel.Controls.OfType<Button>().OrderByDescending(item => item.Left).ToArray();
            Label status = panel.Controls.OfType<Label>().FirstOrDefault();
            panel.Controls.Clear();
            panel.Height = 72;
            panel.Padding = new Padding(Gap16, Gap8, Gap16, Gap8);
            FlowLayoutPanel actions = ButtonRow(buttons);
            actions.Name = "flpSpecialFooter";
            actions.Dock = DockStyle.Right;
            actions.Width = buttons.Length * 112;
            if (status != null)
            {
                status.Dock = DockStyle.Fill;
                status.TextAlign = ContentAlignment.MiddleLeft;
                status.Padding = new Padding(Gap8, 0, Gap8, 0);
                panel.Controls.Add(status);
            }
            panel.Controls.Add(actions);
            actions.BringToFront();
        }

        private static void ReflowProductTable(TableLayoutPanel table, PictureBox picture, CheckBox active)
        {
            if (table == null || table.Name == "tlpSpecialProduct") return;
            List<FieldPair> pairs = CollectPairs(table);
            if (pairs.Count == 0) return;
            table.SuspendLayout();
            table.Controls.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Clear();
            table.Name = "tlpSpecialProduct";
            table.ColumnCount = 3;
            table.RowCount = Math.Max(4, (int)Math.Ceiling(pairs.Count / 2D));
            table.Padding = new Padding(Gap8);
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            for (int row = 0; row < table.RowCount; row++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / table.RowCount));
            for (int index = 0; index < pairs.Count; index++)
            {
                table.Controls.Add(CreateField(pairs[index].Label, pairs[index].Input, 0, 0), index % 2, index / 2);
            }
            if (picture != null)
            {
                picture.Dock = DockStyle.Fill;
                picture.Margin = new Padding(Gap12);
                table.Controls.Add(picture, 2, 0);
                table.SetRowSpan(picture, table.RowCount - 1);
            }
            if (active != null)
            {
                active.Dock = DockStyle.Fill;
                active.Margin = new Padding(Gap12, Gap4, Gap12, Gap4);
                table.Controls.Add(active, 2, table.RowCount - 1);
            }
            table.ResumeLayout(true);
        }

        private static List<FieldPair> CollectPairs(TableLayoutPanel table)
        {
            List<FieldPair> result = new List<FieldPair>();
            for (int row = 0; row < table.RowCount; row++)
            {
                List<Control> items = table.Controls.Cast<Control>()
                    .Where(item => table.GetRow(item) == row && !(item is PictureBox) && !(item is CheckBox))
                    .OrderBy(item => table.GetColumn(item)).ToList();
                for (int index = 0; index < items.Count - 1; index++)
                {
                    Label label = items[index] as Label;
                    if (label == null || items[index + 1] is Label) continue;
                    result.Add(new FieldPair(label, items[index + 1]));
                    index++;
                }
            }
            return result;
        }

        private static void ConfigureEmail(Form form)
        {
            ConfigureSmtp(form);
            ConfigureSingleEmail(form);
            ConfigureBulkEmail(form);
            ConfigureTemplates(form);
            ConfigureEmailLog(form);
            Panel footer = Find<Panel>(form, "pnlChan");
            if (footer != null) { footer.Height = 44; footer.Padding = new Padding(Gap16, Gap4, Gap16, Gap4); }
        }

        private static void ConfigureSmtp(Form form)
        {
            Control first = Find<Control>(form, "txtMayChuSmtp");
            Panel card = first == null ? null : first.Parent as Panel;
            if (card == null) return;
            Control[] inputs = Names(form, "txtMayChuSmtp", "nudCongSmtp", "txtTaiKhoanSmtp", "txtMatKhauSmtp", "txtTenNguoiGui");
            BuildEditorCard(card, inputs, Find<CheckBox>(form, "chkSuDungSsl"), Find<Label>(form, "lblTrangThaiSmtp"),
                "Cấu hình máy chủ SMTP", Find<Button>(form, "btnLuuSmtp"));
        }

        private static void ConfigureSingleEmail(Form form)
        {
            Control first = Find<Control>(form, "cboKhachHangDon");
            Panel card = first == null ? null : first.Parent as Panel;
            if (card == null || card.Controls.Cast<Control>().Any(item => item.Name == "tlpSingleEmail")) return;
            Control customer = first;
            Control invoice = Find<Control>(form, "cboHoaDonDon");
            Control template = Find<Control>(form, "cboMauGuiDon");
            Control email = Find<Control>(form, "txtEmailDon");
            Control subject = Find<Control>(form, "txtTieuDeDon");
            Control body = Find<Control>(form, "txtNoiDungDon");
            ListBox files = Find<ListBox>(form, "lstTepDon");
            Button addFile = Find<Button>(form, "btnThemTepDon");
            Button removeFile = Find<Button>(form, "btnXoaTepDon");
            Button send = Find<Button>(form, "btnGuiDon");
            Label status = Find<Label>(form, "lblTrangThaiGuiDon");

            card.Controls.Clear(); card.Dock = DockStyle.Fill; card.Padding = new Padding(Gap12);
            TableLayoutPanel layout = NewGrid("tlpSingleEmail", 2, 5);
            layout.ColumnStyles.Clear(); layout.RowStyles.Clear();
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            TableLayoutPanel selectors = NewGrid("tlpEmailSelectors", 2, 1);
            selectors.Controls.Add(CreateField(new Label { Text = "Khách hàng" }, customer, 0, 0), 0, 0);
            selectors.Controls.Add(CreateField(new Label { Text = "Hóa đơn" }, invoice, 0, 0), 1, 0);
            layout.Controls.Add(selectors, 0, 0); layout.SetColumnSpan(selectors, 2);
            layout.Controls.Add(CreateField(new Label { Text = "Mẫu email" }, template, 0, 0), 0, 1);
            layout.Controls.Add(CreateField(new Label { Text = "Email người nhận" }, email, 0, 0), 1, 1);
            Control subjectHost = CreateField(new Label { Text = "Tiêu đề" }, subject, 0, 0);
            layout.Controls.Add(subjectHost, 0, 2); layout.SetColumnSpan(subjectHost, 2);
            layout.Controls.Add(CreateField(new Label { Text = "Nội dung" }, body, 0, 0), 0, 3);
            TableLayoutPanel attachment = NewGrid("tlpAttachments", 1, 2);
            attachment.RowStyles.Clear(); attachment.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); attachment.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            files.Dock = DockStyle.Fill; attachment.Controls.Add(files, 0, 0); attachment.Controls.Add(ButtonRow(addFile, removeFile), 0, 1);
            layout.Controls.Add(CreateField(new Label { Text = "Tệp đính kèm" }, attachment, 0, 0), 1, 3);
            Panel footer = StatusActions(status, send); layout.Controls.Add(footer, 0, 4); layout.SetColumnSpan(footer, 2);
            card.Controls.Add(layout);
        }

        private static void ConfigureBulkEmail(Form form)
        {
            TabPage tab = Find<TabPage>(form, "tabHangLoat");
            if (tab == null || tab.Controls.Cast<Control>().Any(item => item.Name == "tlpBulkEmail")) return;
            Control template = Find<Control>(form, "cboMauHangLoat");
            Control subject = Find<Control>(form, "txtTieuDeHangLoat");
            Control body = Find<Control>(form, "txtNoiDungHangLoat");
            Label token = Find<Label>(form, "lblToken");
            DataGridView grid = Find<DataGridView>(form, "dgvNguoiNhan");
            Label count = Find<Label>(form, "lblSoNguoiNhan");
            Button reload = Find<Button>(form, "btnTaiNguoiNhan");
            CheckBox schedule = Find<CheckBox>(form, "chkHenGio");
            Control time = Find<Control>(form, "dtpHenGio");
            Button send = Find<Button>(form, "btnGuiHangLoat");
            ProgressBar progress = Find<ProgressBar>(form, "progressHangLoat");
            Label status = Find<Label>(form, "lblTrangThaiHangLoat");

            tab.Controls.Clear(); tab.Padding = new Padding(Gap12);
            TableLayoutPanel layout = NewGrid("tlpBulkEmail", 2, 6);
            layout.ColumnStyles.Clear(); layout.RowStyles.Clear();
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layout.Controls.Add(CreateField(new Label { Text = "Mẫu email" }, template, 0, 0), 0, 0);
            layout.Controls.Add(CreateField(new Label { Text = "Tiêu đề" }, subject, 0, 0), 1, 0);
            Control bodyHost = CreateField(new Label { Text = "Nội dung" }, body, 0, 0); layout.Controls.Add(bodyHost, 0, 1); layout.SetColumnSpan(bodyHost, 2);
            token.Dock = DockStyle.Fill; token.Margin = new Padding(Gap8); layout.Controls.Add(token, 0, 2); layout.SetColumnSpan(token, 2);
            grid.Dock = DockStyle.Fill; layout.Controls.Add(grid, 0, 3); layout.SetColumnSpan(grid, 2);
            FlowLayoutPanel actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, BackColor = Color.Transparent };
            count.Size = new Size(130, ButtonHeight); count.TextAlign = ContentAlignment.MiddleLeft;
            reload.Size = new Size(126, ButtonHeight); schedule.Margin = new Padding(Gap12, Gap12, Gap8, 0);
            time.Size = new Size(176, InputHeight); time.Margin = new Padding(Gap4); send.Size = new Size(126, ButtonHeight);
            actions.Controls.AddRange(new Control[] { count, reload, schedule, time, send }); layout.Controls.Add(actions, 0, 4); layout.SetColumnSpan(actions, 2);
            TableLayoutPanel progressRow = NewGrid("tlpBulkProgress", 2, 1);
            progressRow.ColumnStyles.Clear(); progressRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F)); progressRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            progress.Dock = DockStyle.Fill; progress.Margin = new Padding(Gap4, Gap8, Gap12, Gap8); status.Dock = DockStyle.Fill; status.TextAlign = ContentAlignment.MiddleLeft;
            progressRow.Controls.Add(progress, 0, 0); progressRow.Controls.Add(status, 1, 0); layout.Controls.Add(progressRow, 0, 5); layout.SetColumnSpan(progressRow, 2);
            tab.Controls.Add(layout);
        }

        private static void ConfigureTemplates(Form form)
        {
            TabPage tab = Find<TabPage>(form, "tabMauEmail");
            Control name = Find<Control>(form, "txtTenMau");
            Panel card = name == null ? null : name.Parent as Panel;
            if (tab == null || card == null || tab.Controls.Cast<Control>().Any(item => item.Name == "tlpTemplates")) return;
            ListBox list = Find<ListBox>(form, "lstMauEmail");
            Control subject = Find<Control>(form, "txtTieuDeMau"); Control body = Find<Control>(form, "txtNoiDungMau");
            CheckBox active = Find<CheckBox>(form, "chkMauHoatDong");
            Button fresh = Find<Button>(form, "btnMauMoi"); Button defaults = Find<Button>(form, "btnTaoMauMacDinh");
            Button save = Find<Button>(form, "btnLuuMau"); Button disable = Find<Button>(form, "btnKhoaMau");
            tab.Controls.Clear(); tab.Padding = new Padding(Gap12);
            TableLayoutPanel root = NewGrid("tlpTemplates", 2, 2);
            root.ColumnStyles.Clear(); root.RowStyles.Clear(); root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F)); root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); root.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            list.Dock = DockStyle.Fill; list.Margin = new Padding(Gap4, Gap4, Gap12, Gap4); root.Controls.Add(list, 0, 0);
            BuildEditorCard(card, new[] { name, subject, body }, active, null, "Chi tiết mẫu email");
            card.Dock = DockStyle.Fill; root.Controls.Add(card, 1, 0); root.Controls.Add(ButtonRow(fresh, defaults), 0, 1); root.Controls.Add(ButtonRow(disable, save), 1, 1);
            tab.Controls.Add(root);
        }

        private static void ConfigureEmailLog(Form form)
        {
            TabPage tab = Find<TabPage>(form, "tabNhatKy"); DataGridView grid = Find<DataGridView>(form, "dgvNhatKy");
            if (tab == null || grid == null || tab.Controls.Cast<Control>().Any(item => item.Name == "pnlEmailLogFilter")) return;
            Control[] inputs = Names(form, "txtTimNhatKy", "dtpTuNgayNhatKy", "dtpDenNgayNhatKy", "cboLocLoaiGui", "cboLocTrangThaiNhatKy", "cboLocMauNhatKy");
            Button search = Find<Button>(form, "btnTimNhatKy"); Button reload = Find<Button>(form, "btnTaiLaiNhatKy"); Label count = Find<Label>(form, "lblSoNhatKy");
            tab.Controls.Clear(); tab.Padding = new Padding(Gap12);
            Panel filter = new Panel { Name = "pnlEmailLogFilter", Dock = DockStyle.Top, Height = 132, Padding = new Padding(Gap8) };
            FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = true, BackColor = Color.Transparent };
            foreach (Control input in inputs) flow.Controls.Add(CreateField(new Label { Text = FriendlyLabel(input.Name) }, input, CompactWidth(input), 58));
            foreach (Button button in new[] { search, reload }) { button.Size = new Size(96, ButtonHeight); button.Margin = new Padding(Gap4, 18, Gap4, 0); flow.Controls.Add(button); }
            count.Size = new Size(150, ButtonHeight); count.Margin = new Padding(Gap8, 18, Gap4, 0); count.TextAlign = ContentAlignment.MiddleRight; flow.Controls.Add(count);
            filter.Controls.Add(flow); grid.Dock = DockStyle.Fill; tab.Controls.Add(grid); tab.Controls.Add(filter);
        }

        private static void BuildEditorCard(Panel card, IEnumerable<Control> fields, CheckBox check, Label status, string title, params Button[] buttons)
        {
            if (card.Controls.Cast<Control>().Any(item => item.Name == "tlpEditorCard")) return;
            Control[] inputs = fields.Where(item => item != null).ToArray();
            card.Controls.Clear(); card.Dock = DockStyle.Fill; card.Padding = new Padding(Gap16);
            int fieldRows = (int)Math.Ceiling(inputs.Length / 2D);
            TableLayoutPanel layout = NewGrid("tlpEditorCard", 2, fieldRows + 3);
            layout.RowStyles.Clear(); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            for (int row = 0; row < fieldRows; row++) layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Math.Max(1, fieldRows)));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            Label heading = new Label { Name = "lblSpecialSectionTitle", Dock = DockStyle.Fill, Text = title, TextAlign = ContentAlignment.MiddleLeft };
            layout.Controls.Add(heading, 0, 0); layout.SetColumnSpan(heading, 2);
            for (int index = 0; index < inputs.Length; index++) layout.Controls.Add(CreateField(new Label { Text = FriendlyLabel(inputs[index].Name) }, inputs[index], 0, 0), index % 2, index / 2 + 1);
            if (check != null) { check.Dock = DockStyle.Fill; check.Margin = new Padding(Gap8); layout.Controls.Add(check, 0, fieldRows + 1); layout.SetColumnSpan(check, 2); }
            Panel footer = StatusActions(status, buttons); layout.Controls.Add(footer, 0, fieldRows + 2); layout.SetColumnSpan(footer, 2); card.Controls.Add(layout);
        }

        private static void ConfigurePurchase(Form form)
        {
            form.Padding = new Padding(Gap12);
            TabControl tabs = FindControls<TabControl>(form).FirstOrDefault();
            if (tabs != null) { tabs.Padding = new Point(Gap16, Gap8); foreach (TabPage tab in tabs.TabPages) tab.Padding = new Padding(Gap12); }
            foreach (FlowLayoutPanel flow in FindControls<FlowLayoutPanel>(form))
            {
                if (flow.Controls.OfType<Button>().Any())
                {
                    flow.Height = flow.Controls.Cast<Control>().Any(item => item.Controls.Count > 0) ? 128 : 104;
                    flow.WrapContents = true; flow.AutoScroll = false; flow.Padding = new Padding(Gap8);
                    foreach (Button button in flow.Controls.OfType<Button>()) button.Size = new Size(112, ButtonHeight);
                }
            }
            SplitContainer split = FindControls<SplitContainer>(form).FirstOrDefault();
            if (split != null) ConfigureVerticalSplit(split, 360, 300, 0.60F);
        }

        private static void ConfigureBackup(Form form)
        {
            Panel info = Find<Panel>(form, "pnlThongTin"); Panel progress = Find<Panel>(form, "pnlTienTrinh"); SplitContainer split = Find<SplitContainer>(form, "splitChinh");
            if (info != null) { info.Height = 112; info.Padding = new Padding(Gap16); }
            if (progress != null) { progress.Height = 56; progress.Padding = new Padding(Gap16, Gap8, Gap16, Gap8); }
            if (split != null) ConfigureVerticalSplit(split, 360, 440, 0.42F);
        }

        private static void SetTabPadding(Control root)
        {
            foreach (TabPage tab in FindControls<TabPage>(root)) tab.Padding = new Padding(Gap8);
        }

        private static TableLayoutPanel NewGrid(string name, int columns, int rows)
        {
            TableLayoutPanel table = new TableLayoutPanel { Name = name, Dock = DockStyle.Fill, ColumnCount = columns, RowCount = rows, BackColor = Color.Transparent };
            for (int column = 0; column < columns; column++) table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columns));
            for (int row = 0; row < rows; row++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rows));
            return table;
        }

        private static Control CreateField(Label label, Control input, int width, int height)
        {
            TableLayoutPanel field = new TableLayoutPanel { Dock = width <= 0 ? DockStyle.Fill : DockStyle.None, ColumnCount = 1, RowCount = 2, BackColor = Color.Transparent, Margin = new Padding(Gap4) };
            if (width > 0) field.Size = new Size(width, height > 0 ? height : 58);
            field.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); field.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F)); field.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            label.AutoSize = false; label.Dock = DockStyle.Fill; label.Margin = Padding.Empty; label.TextAlign = ContentAlignment.MiddleLeft;
            input.Dock = DockStyle.Fill; input.Margin = Padding.Empty; field.Controls.Add(label, 0, 0); field.Controls.Add(input, 0, 1); return field;
        }

        private static Panel StatusActions(Label status, params Button[] buttons)
        {
            Panel panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Padding = new Padding(Gap4) };
            FlowLayoutPanel flow = ButtonRow(buttons); flow.Dock = DockStyle.Right; flow.Width = buttons.Count(item => item != null) * 116;
            if (status != null) { status.Dock = DockStyle.Fill; status.TextAlign = ContentAlignment.MiddleLeft; panel.Controls.Add(status); }
            panel.Controls.Add(flow); flow.BringToFront(); return panel;
        }

        private static FlowLayoutPanel ButtonRow(params Button[] buttons)
        {
            FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, BackColor = Color.Transparent, Padding = new Padding(Gap4) };
            foreach (Button button in buttons.Where(item => item != null)) { button.Size = new Size(104, ButtonHeight); button.Margin = new Padding(Gap4); flow.Controls.Add(button); }
            return flow;
        }

        private static Label MatchLabel(Control input, IEnumerable<Label> labels, ISet<Label> used)
        {
            return labels.Where(label => !used.Contains(label) && label.Top <= input.Top && Math.Abs(label.Left - input.Left) < 28)
                .OrderBy(label => input.Top - label.Bottom).FirstOrDefault();
        }

        private static int CompactWidth(Control control)
        {
            string name = Normalize(control.Name);
            if (name.Contains("sanpham") || name.Contains("tukhoa") || name.Contains("tim")) return 154;
            if (name.Contains("khachhang") || name.Contains("nhacungcap")) return 150;
            if (name.Contains("phuongthuc")) return 132;
            if (control is DateTimePicker) return 116;
            if (control is NumericUpDown) return 88;
            if (control is ComboBox) return 126;
            return 132;
        }

        private static bool IsInput(Control control)
        {
            return control is TextBoxBase || control is ComboBox || control is DateTimePicker || control is NumericUpDown;
        }

        private static string FriendlyLabel(string name)
        {
            string key = Regex.Replace(name ?? string.Empty, "^(txt|cbo|dtp|nud|num|lst)", string.Empty, RegexOptions.IgnoreCase);
            Dictionary<string, string> map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "KhachHang", "Khách hàng" }, { "KhachHangDon", "Khách hàng" }, { "HoaDonDon", "Hóa đơn" },
                { "MauGuiDon", "Mẫu email" }, { "MayChuSmtp", "Máy chủ SMTP" }, { "CongSmtp", "Cổng SMTP" },
                { "TaiKhoanSmtp", "Tài khoản email" }, { "MatKhauSmtp", "Mật khẩu ứng dụng" }, { "TenNguoiGui", "Tên người gửi" },
                { "PhuongThucThanhToan", "Phương thức thanh toán" }, { "GiamGia", "Giảm giá (%)" }, { "NhaCungCap", "Nhà cung cấp" },
                { "GhiChu", "Ghi chú" }, { "SanPham", "Sản phẩm" }, { "SoLuong", "Số lượng" }, { "DonGiaNhap", "Đơn giá nhập" },
                { "HanBaoHanh", "Hạn bảo hành" }, { "TimSanPhamDaBan", "Tìm sản phẩm đã bán" }, { "SanPhamDaBan", "Sản phẩm đã bán" },
                { "NoiDungTiepNhan", "Nội dung tiếp nhận" }, { "NgayTraDuKien", "Ngày trả dự kiến" }, { "GhiChuTiepNhan", "Ghi chú tiếp nhận" },
                { "NoiDungXuLy", "Nội dung xử lý" }, { "TrangThaiXuLy", "Trạng thái xử lý" }, { "NgayTraDuKienXuLy", "Ngày trả dự kiến" },
                { "NgayTraThucTe", "Ngày trả thực tế" }, { "GhiChuXuLy", "Ghi chú xử lý" }, { "EmailDon", "Email người nhận" },
                { "TieuDeDon", "Tiêu đề" }, { "NoiDungDon", "Nội dung" }, { "MauHangLoat", "Mẫu email" }, { "TieuDeHangLoat", "Tiêu đề" },
                { "NoiDungHangLoat", "Nội dung" }, { "TenMau", "Tên mẫu" }, { "TieuDeMau", "Tiêu đề mẫu" }, { "NoiDungMau", "Nội dung mẫu" },
                { "TimNhatKy", "Từ khóa" }, { "TuNgayNhatKy", "Từ ngày" }, { "DenNgayNhatKy", "Đến ngày" },
                { "LocLoaiGui", "Loại gửi" }, { "LocTrangThaiNhatKy", "Trạng thái" }, { "LocMauNhatKy", "Mẫu email" }
            };
            string value; if (map.TryGetValue(key, out value)) return value;
            return Regex.Replace(key, "([a-z0-9])([A-Z])", "$1 $2");
        }

        private static Control[] Names(Control root, params string[] names)
        {
            return names.Select(name => Find<Control>(root, name)).Where(item => item != null).ToArray();
        }

        private static T Find<T>(Control root, string name) where T : Control
        {
            T control = FindControls<T>(root).FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
            if (control != null) return control;

            for (Type type = root.GetType(); type != null && type != typeof(object); type = type.BaseType)
            {
                FieldInfo field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (field == null || !typeof(T).IsAssignableFrom(field.FieldType)) continue;
                control = field.GetValue(root) as T;
                if (control == null) continue;
                if (string.IsNullOrEmpty(control.Name)) control.Name = name;
                return control;
            }

            return null;
        }

        private static IEnumerable<T> FindControls<T>(Control root) where T : Control
        {
            foreach (Control child in root.Controls)
            {
                T match = child as T; if (match != null) yield return match;
                foreach (T descendant in FindControls<T>(child)) yield return descendant;
            }
        }

        private static string Normalize(string value) { return (value ?? string.Empty).Replace("_", string.Empty).ToLowerInvariant(); }

        private sealed class FieldPair
        {
            internal FieldPair(Label label, Control input) { Label = label; Input = input; }
            internal Label Label { get; private set; }
            internal Control Input { get; private set; }
        }
    }
}
